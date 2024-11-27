using Web_Lab2.Repositories.Contracts;
using Web_Lab2.Entities;

namespace Web_Lab2.Repositories
{
    internal class DogRepository : GenericRepository<Dog, int>, IDogRepository
    {
        public DogRepository(DataModelContext context)
            : base(context) { }
    }
}
