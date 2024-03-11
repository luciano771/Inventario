using InventarioBussinessLogic.Implementations;
using InventarioBussinessLogic.Interfaces;
using InventarioDataAccess.Data;
using InventarioDataAccess.Entities;
using InventarioDataAccess.Repositories.Implementations;
using InventarioDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Conexion a la base
builder.Services.AddDbContext<InventarioContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("API"));
    options.LogTo(Console.WriteLine);
});


// Registrando las interfaces y sus implementacioness
builder.Services.AddScoped<IRepository<Clientes>, ClientesRepository>();
builder.Services.AddScoped<IRepository<Ventas>, VentasRepository>();
builder.Services.AddScoped<IRepository<Productos>, ProductosRepository>();
builder.Services.AddScoped<IRepository<VentasItems>, VentasItemsRepository>();
builder.Services.AddScoped<IGeneralLogic<Clientes>, ClientesServices>();
builder.Services.AddScoped<IGeneralLogic<Ventas>, VentasServices>();
builder.Services.AddScoped<IGeneralLogic<Productos>, ProductosServices>();
builder.Services.AddScoped<IGeneralLogic<VentasItems>, VentasItemsServices>();


builder.Services.AddControllersWithViews();

 

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
