using Web_Lab2.Repositories.Contracts;
using Web_Lab2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Web_Lab2.Repositories
{
    internal class DogShelterRepository : GenericRepository<DogShelter, int>, IDogShelterRepository
    {
        public DogShelterRepository(DataModelContext context)
            : base(context) { }

        public Task<DogShelter?> FindByNameAndAddressAsync(string name, string address) => _context.Set<DogShelter>()
            .Include(s => s.Dogs)
            .FirstOrDefaultAsync(s => s.Name == name && s.Address == address);

        public override async Task<DogShelter?> FindAsync(int key)
        {
            return await _context.Set<DogShelter>()
            .Include("Dogs")
            .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id").Equals(key));
        }
    }
}
