using DataModel.Entities;

namespace DataModel.Repositories.Contracts
{
    public interface IDogRepository : IGenericRepository<Dog, int>
    {
        Task<Dog?> FindByNameAndBreedAsync(string name, string breed);
    }
}
 