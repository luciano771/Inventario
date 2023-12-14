using DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Inicio Conexion a la base
builder.Services.AddDbContext<VentasContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("API"));
    options.LogTo(Console.WriteLine);
});
//Fin Conexion a la base

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


 
 

var app = builder.Build();
 

//crear scope para que se ejecute cada vez que se ejecuta la app
using (var scope = app.Services.CreateScope()) 
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DB.VentasContext>();
    dataContext.Database.Migrate();
}
//fin scope

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
