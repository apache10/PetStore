using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetStore.Services.Data;
using PetStore.Services.Repository.IReopository;

namespace PetStore.Services.Repository
{
    public class PetRepositroy : Repository<Pet>, IPetRepository
    {
        private ApplicationDbContext _db;
        public PetRepositroy(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Pet obj)
        {
            _db.Pets.Update(obj);
        }
    }
}
