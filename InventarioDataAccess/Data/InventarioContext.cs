using InventarioDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InventarioDataAccess.Data
{
    public class InventarioContext : DbContext
    {

        public InventarioContext(DbContextOptions<InventarioContext> options) : base(options)
        {

        }

        public DbSet<Ventas> ventas { get; set; }
        public DbSet<Clientes> clientes { get; set; }
        public DbSet<VentasItems> ventasitems { get; set; }
        public DbSet<Productos> productos { get; set; }

       


    }



}