using InventarioDataAccess.Data;
using InventarioDataAccess.Entities;
using InventarioDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDataAccess.Repositories.Implementations
{
    public class VentasItemsRepository : IRepository<VentasItems>
    {
        private readonly InventarioContext _dbContext;
        public VentasItemsRepository(InventarioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VentasItems> Create(VentasItems ventasItems)
        {
            try
            {
                 

                var ventasItemsNuevo = new VentasItems();
                var Producto = await _dbContext.productos.FindAsync(ventasItems.IDProducto);

                ventasItemsNuevo.IDVenta = ventasItems.IDVenta;
                ventasItemsNuevo.IDProducto = ventasItems.IDProducto;
                ventasItemsNuevo.Cantidad = ventasItems.Cantidad;
                ventasItemsNuevo.PrecioUnitario = Producto.precio;
                ventasItemsNuevo.PrecioTotal = Producto.precio * ventasItems.Cantidad;

                _dbContext.ventasitems.Add(ventasItemsNuevo);
                await _dbContext.SaveChangesAsync();
                return ventasItemsNuevo; // Asegúrate de que el modelo devuelto tiene el ID actualizado.



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IQueryable<VentasItems>> GetAll()
        {
            var ventasItemsQuery = _dbContext.ventasitems
                .Include(v => v.ventas)        
                    .ThenInclude(v => v.clientes)  // Include de la tabla de clientes
                .Include(v => v.productos);
            return await Task.FromResult(ventasItemsQuery);
        }




        public async Task<bool> Delete(int id)
        {
            var productos = await _dbContext.ventasitems.FindAsync(id);
            if (productos == null)
            {
                return false;
            }

            _dbContext.ventasitems.Remove(productos);
            int affectedRows = await _dbContext.SaveChangesAsync();

            return affectedRows > 0;
        }

    

        public async  Task<IQueryable<VentasItems>> GetById(int id)
        {
            var ventasItemsQuery = _dbContext.ventasitems
                  .Include(v => v.ventas)
                    .ThenInclude(v => v.clientes)   
                .Include(v => v.productos)
              .Where(p => p.IDVenta == id);
            return await Task.FromResult(ventasItemsQuery);
        }

        public async Task<VentasItems> Update(int id, VentasItems ventasItems)
        {
            try
            {
                var ventasItemsUpdate = await _dbContext.ventasitems.FindAsync(id);
                var Producto = await _dbContext.productos.FindAsync(ventasItems.IDProducto);

                if (ventasItemsUpdate is null)
                {
                    // Devolver NotFound u otro código/valor apropiado para indicar que no se encontró el objeto.
                    return null;
                }

                ventasItemsUpdate.IDProducto = ventasItems.IDProducto;
                ventasItemsUpdate.Cantidad = ventasItems.Cantidad;
                ventasItemsUpdate.PrecioUnitario = Producto.precio;
                ventasItemsUpdate.PrecioTotal = Producto.precio * ventasItems.Cantidad;

                await _dbContext.SaveChangesAsync();

                return ventasItemsUpdate;
            }
            catch (DbUpdateException ex)
            { 
                Console.WriteLine(ex.ToString(), "Error al actualizar el objeto VentasItems");
                return null;
            }
        }

    }
}
