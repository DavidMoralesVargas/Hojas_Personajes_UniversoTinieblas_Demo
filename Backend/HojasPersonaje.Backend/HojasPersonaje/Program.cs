using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades.ValidacionesDatos;
using HojasPersonaje.Repositorio.Implementaciones;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;

builder.Services.AddEndpointsApiExplorer();     
builder.Services.AddSwaggerGen();           

//Inyecciones de los validadores
builder.Services.AddValidatorsFromAssemblyContaining<BackgroundValidator>();

//Inyección de la clase contexto
builder.Services.AddDbContext<ClaseContexto>(x => x.UseSqlServer("name=DockerConnection"));


//Inyecciones de implementaciones e interfaces
builder.Services.AddScoped(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>));
builder.Services.AddScoped<IBackgroundRepositorio, BackgroundRepositorio>();




var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());


if (app.Environment.IsDevelopment())     //Esto es un condicional para indicar que solo cuando esté en modo de DESARROLLO o depuración, este podrá mostrar el Swagger, si se quita se podrá mostrar el Swagger para producción
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
