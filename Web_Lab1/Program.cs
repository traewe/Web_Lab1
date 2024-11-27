using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Web_Lab2.Repositories;
using Web_Lab2.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataModelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

RepositoryServiceCollectionExtensions.AddRepositories(builder.Services);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
