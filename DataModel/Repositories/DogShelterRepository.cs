using DataModel.Repositories.Contracts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Repositories
{
    internal class DogShelterRepository : GenericRepository<DogShelter, int>, IDogShelterRepository
    {
        public DogShelterRepository(DataModelContext context)
            : base(context) { }

        public Task<DogShelter?> FindByDogIdAsync(int dogId) => _context.Set<DogShelter>()
            .Include(shelter => shelter.Dogs)
            .FirstOrDefaultAsync(shelter => shelter.Dogs.Any(dog => dog.Id == dogId));
    }
}
