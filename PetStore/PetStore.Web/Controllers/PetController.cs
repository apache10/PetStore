using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetStore.Services;
using PetStore.Services.Repository.IReopository;

namespace PetStore.Web.Controllers
{
    public class PetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PetController> _logger;

        public PetController(IUnitOfWork unitOfWork, ILogger<PetController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Pet> objPetList = _unitOfWork.Pet.GetAll().ToList();
            return View(objPetList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pet obj)
        {
            // Duplicate check
            var petsFromDb = _unitOfWork.Pet.GetAll(u => u.Name == obj.Name);
            foreach (Pet pet in petsFromDb)
            {
                if (pet?.Name == obj.Name && pet?.DOB == obj.DOB)
                {
                    ModelState.AddModelError("Name", "This name and date of birth already exists, please select different name");
                }
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Pet.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Pet created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Pet? petFromDb = _unitOfWork.Pet.Get(u => u.Id == id, null, true);

            if (petFromDb == null)
            {
                return NotFound();
            }
            return View(petFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Pet obj)
        {
            // Duplicate check re-write to find duplicate better 
            var petsFromDb = _unitOfWork.Pet.GetAll(u => u.Name == obj.Name);
            foreach (Pet pet in petsFromDb)
            {
                if (pet?.Name == obj.Name && pet?.DOB == obj.DOB && pet?.Id != obj?.Id)
                {
                    ModelState.AddModelError("Name", "Pet with this name and date of birth already exists, please select different name");
                }
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Pet.Update(obj);
                _logger.LogInformation("Updated Pet: " + JsonConvert.SerializeObject(obj));
                _unitOfWork.Save();
                TempData["success"] = "Pet updated successfully";
                return RedirectToAction("Index");
            }
           
            return View();

        }

     
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Pet> objPetList = _unitOfWork.Pet.GetAll().ToList();
            return Json(new { data = objPetList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var petToBeDeleted = _unitOfWork.Pet.Get(u => u.Id == id);
            if (petToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Pet.Remove(petToBeDeleted);
            _unitOfWork.Save();
            //log with Id also delete view
            _logger.LogInformation("Pet has been deleted of an id:" + id);
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
