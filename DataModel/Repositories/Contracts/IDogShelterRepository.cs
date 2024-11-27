using DataModel.Entities;

namespace DataModel.Repositories.Contracts
{
    public interface IDogShelterRepository : IGenericRepository<DogShelter, int>
    {
        Task<DogShelter?> FindByNameAndAdressAsync(string name, string adress);
    }
}
