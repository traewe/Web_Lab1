using DataModel.Repositories.Contracts;
using DataModel.Entities;

namespace DataModel.Repositories
{
    internal class DogRepository : GenericRepository<Dog, int>, IDogRepository
    {
        public DogRepository(DataModelContext context)
            : base(context) { }
    }
}
