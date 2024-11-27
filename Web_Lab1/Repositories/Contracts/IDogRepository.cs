using Web_Lab2.Entities;

namespace Web_Lab2.Repositories.Contracts
{
    public interface IDogRepository : IGenericRepository<Dog, int>
    {
        Task<Dog?> FindByNameAndBreedAsync(string name, string breed);
    }
}
 