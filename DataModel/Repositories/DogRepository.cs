using DataModel.Repositories.Contracts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Repositories
{
    internal class DogRepository : GenericRepository<Dog, int>, IDogRepository
    {
        public DogRepository(DataModelContext context)
            : base(context) { }

        public Task<Dog?> FindByNameAndBreedAsync(string name, string breed) => _context.Set<Dog>()
            .FirstOrDefaultAsync(s => s.Name == name && s.Breed == breed);
    }
}
