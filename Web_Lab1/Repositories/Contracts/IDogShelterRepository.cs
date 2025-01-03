﻿using Web_Lab2.Entities;

namespace Web_Lab2.Repositories.Contracts
{
    public interface IDogShelterRepository : IGenericRepository<DogShelter, int>
    {
        Task<DogShelter?> FindByNameAndAddressAsync(string name, string adress);
    }
}
