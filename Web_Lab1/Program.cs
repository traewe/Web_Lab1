using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web_Lab2.Repositories.Contracts;
using Web_Lab2.Repositories;
using FluentValidation.AspNetCore;
using Web_Lab2.Configurations;
using Web_Lab2.Validators;
using Web_Lab2.Options;
using Microsoft.IdentityModel.Tokens;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DataModelContext>(options => {
            options.UseSqlite("Data Source=sample.db");
        });

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<DogCreateDtoValidator>();

        builder.Services.AddControllers();

        builder.Services.AddOptions<ProgramAuthor>()
            .BindConfiguration("ProgramAuthor")
            .Validate(options =>
                !string.IsNullOrWhiteSpace(options.PhoneNumber) &&
                !string.IsNullOrWhiteSpace(options.Email),
                "PhoneNumber and Email cannot be empty.")
            .ValidateOnStart();

        builder.Services.AddMemoryCache();

        builder.Services.AddEndpointsApiExplorer();

        //builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options => {
            options.AddPolicy("AllowAll",
                b => b.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
        });

        builder.Services.AddAutoMapper(typeof(MapperConfig));

        builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        builder.Services.AddScoped<IDogRepository, DogRepository>();
        builder.Services.AddScoped<IDogShelterRepository, DogShelterRepository>();

        var app = builder.Build();

        /*if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }*/

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}