using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Services.Repository.IReopository
{
    public interface IPetRepository : IRepository<Pet>
    {
        void Update(Pet obj);
    }
}
