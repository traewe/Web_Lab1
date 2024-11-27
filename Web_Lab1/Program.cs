using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using Web_Lab2.Controllers;
using Web_Lab2.Entities;
using Microsoft.EntityFrameworkCore;
using Web_Lab2.Repositories.Contracts;
using Web_Lab2.Repositories;
using FluentValidation.AspNetCore;
using Web_Lab2.Configurations;
using Web_Lab2.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataModelContext>(options => {
    options.UseSqlite("Data Source=sample.db");
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<DogCreateDtoValidator>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();