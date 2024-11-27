using Web_Lab2.Repositories.Contracts;

namespace Web_Lab2.Repositories
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
            .AddTransient<IDogRepository, DogRepository>()
            .AddTransient<IDogShelterRepository, DogShelterRepository>();
    }
}
