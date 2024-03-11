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
    public class VentasRepository : IRepository<Ventas>
    {
        private readonly InventarioContext _dbContext;
        public VentasRepository(InventarioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Ventas> Create(Ventas ventas)
        {
            try
            {
                _dbContext.ventas.Add(ventas);
                await _dbContext.SaveChangesAsync();
                return ventas; // Asegúrate de que el modelo devuelto tiene el ID actualizado.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<bool>   Delete(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IQueryable<Ventas>> GetAll()
        {
            var ventasQuery = _dbContext.ventas.Include(v => v.clientes);
            return await Task.FromResult(ventasQuery);
        }


        public Task<IQueryable<Ventas>> GetById(int id)
        {
            var ventasQuery = _dbContext.ventas.Where(p => p.ID == id).AsQueryable();
            return Task.FromResult(ventasQuery);
        }
 
        public async Task<Ventas> Update(int id, Ventas ventas)
        {
            try
            {
                var ventasUpdate = await _dbContext.ventas.FindAsync(id);

                if (ventasUpdate is null)
                {
                    // Devolver NotFound u otro código/valor apropiado para indicar que no se encontró el objeto.
                    return null;
                }

                ventasUpdate.IDCliente = ventasUpdate.IDCliente;
                ventasUpdate.Fecha = ventasUpdate.Fecha;
                ventasUpdate.Total = ventas.Total;

                await _dbContext.SaveChangesAsync();

                return ventasUpdate;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.ToString(), "Error al actualizar el objeto VentasItems");
                return null;
            }
        }
    }
     
}
