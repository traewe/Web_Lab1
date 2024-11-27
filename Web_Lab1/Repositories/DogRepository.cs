using Web_Lab2.Repositories.Contracts;
using Web_Lab2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Web_Lab2.Repositories
{
    internal class DogRepository : GenericRepository<Dog, int>, IDogRepository
    {
        public DogRepository(DataModelContext context)
            : base(context) { }

        public Task<Dog?> FindByNameAndBreedAsync(string name, string breed) => _context.Set<Dog>()
            .FirstOrDefaultAsync(s => s.Name == name && s.Breed == breed);
    }
}
