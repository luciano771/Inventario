using BussinesLogic.Implementations;
using BussinessLogic.Implementations;
using BussinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 
 


//Conexion a la base
builder.Services.AddDbContext<InventarioContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("API"));
    options.LogTo(Console.WriteLine);
});



// Registrando las interfaces y sus implementaciones
builder.Services.AddScoped<IRepository<Clientes>, ClientesRepository>();
builder.Services.AddScoped<IRepository<Ventas>, VentasRepository>();
builder.Services.AddScoped<IGeneralLogic<Clientes>, ClientesServices>();
builder.Services.AddScoped<IGeneralLogic<Ventas>, VentasServices>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();
 
 
app.UseSwagger();
app.UseSwaggerUI();
 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
