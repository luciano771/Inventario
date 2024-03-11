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
    public class ProductosRepository : IRepository<Productos>
    {
        private readonly InventarioContext _dbContext;
        public ProductosRepository(InventarioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Productos> Create(Productos productos)
        {
            try
            {
                _dbContext.productos.Add(productos);
                await _dbContext.SaveChangesAsync();
                return productos; // Asegúrate de que el modelo devuelto tiene el ID actualizado.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var productos = await _dbContext.productos.FindAsync(id);
            if (productos == null)
            {
                return false;
            }

            _dbContext.productos.Remove(productos);
            int affectedRows = await _dbContext.SaveChangesAsync();

            return affectedRows > 0;
        }

        public Task<IQueryable<Productos>> GetAll()
        {
            var clientessQuery = _dbContext.productos.AsQueryable();
            return Task.FromResult(clientessQuery);
        }

        public Task<IQueryable<Productos>> GetById(int id)
        {
            var clientesById = _dbContext.productos
                                             .Where(c => c.ID == id).AsQueryable();
            return Task.FromResult(clientesById); ;
        }

        public async Task<Productos> Update(int id, Productos productos)
        {
            try
            {
                var producto = await _dbContext.productos.FindAsync(id);
                if (producto != null)
                {
                    producto.Nombre = productos.Nombre;
                    producto.precio = productos.precio;
                    producto.categoria = productos.categoria;

                    await _dbContext.SaveChangesAsync();
                    return productos;
                }
                return producto;

            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
     
}