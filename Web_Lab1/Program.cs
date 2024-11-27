using Microsoft.EntityFrameworkCore;
using DataModel.Repositories;
using System.Reflection;

namespace Web_Lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddDbContext<DataModelContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            RepositoryServiceCollectionExtensions.AddRepositories(builder.Services);

            

            var app = builder.Build();

            app.MapControllers();
            app.Run();

        }
    }
}