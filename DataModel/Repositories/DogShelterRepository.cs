using DataModel.Repositories.Contracts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Repositories
{
    internal class DogShelterRepository : GenericRepository<DogShelter, int>, IDogShelterRepository
    {
        public DogShelterRepository(DataModelContext context)
            : base(context) { }

        public Task<DogShelter?> FindByNameAndAdressAsync(string name, string address) => _context.Set<DogShelter>()
            .FirstOrDefaultAsync(s => s.Name == name && s.Address == address);
    }
}
