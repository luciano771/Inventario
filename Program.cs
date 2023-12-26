using BussinesLogic.Repositories.Implementations;
using BussinesLogic.Repositories.Interfaces;
using DataAccess.Data;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Registrando la interfaz y su implementación concreta
 


// Inicio Conexion a la base
builder.Services.AddDbContext<IventarioContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("API"));
    options.LogTo(Console.WriteLine);
});
//Fin Conexion a la base


builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IClientesServices, ClientesServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


 
 

var app = builder.Build();
 

//crear scope para que se ejecute cada vez que se ejecuta la app
//using (var scope = app.Services.CreateScope()) 
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<IventarioContext>();
//    dataContext.Database.Migrate();
//}
//fin scope

// Configure the HTTP request pipeline.
 
app.UseSwagger();
app.UseSwaggerUI();
 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
