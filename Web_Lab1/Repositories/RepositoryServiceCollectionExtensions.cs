using Web_Lab2.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Web_Lab2.Repositories
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
            .AddScoped<IDogRepository, DogRepository>()
            .AddScoped<IDogShelterRepository, DogShelterRepository>();
    }
}
