using DataModel.Repositories.Contracts;
using DataModel.Repositories;
using DataModel.Repositories;

namespace DataModel.Repositories
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
            .AddTransient<IDogRepository, DogRepository>()
            .AddTransient<IDogShelterRepository, DogShelterRepository>();
    }
}
