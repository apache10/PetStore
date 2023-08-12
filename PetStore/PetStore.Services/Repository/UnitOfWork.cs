using PetStore.Services.Data;
using PetStore.Services.Repository.IReopository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IPetRepository Pet { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Pet = new PetRepositroy(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
