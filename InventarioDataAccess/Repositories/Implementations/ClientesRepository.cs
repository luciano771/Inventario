using InventarioDataAccess.Data;
using InventarioDataAccess.Entities;
using InventarioDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDataAccess.Repositories.Implementations
{
    public class ClientesRepository : IRepository<Clientes>
    {
        private readonly InventarioContext _dbContext;
        public ClientesRepository(InventarioContext dbContext) 
        { 
            _dbContext = dbContext;  
        }

        public async Task<Clientes> Create(Clientes cliente)
        {    

            try
            {
                
                _dbContext.clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();
                return cliente; // Asegúrate de que el modelo devuelto tiene el ID actualizado.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<bool> Delete(int id)
        {
            var cliente = await _dbContext.clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;  
            }

            _dbContext.clientes.Remove(cliente);
            int affectedRows = await _dbContext.SaveChangesAsync();

            return affectedRows > 0;
        }
 
        public Task<IQueryable<Clientes>> GetAll()
        {
            var clientessQuery = _dbContext.clientes.AsQueryable();
            return Task.FromResult(clientessQuery);
        }

        public Task<IQueryable<Clientes>> GetById(int id)
        {
            var clientesById =  _dbContext.clientes
                                              .Where(c => c.ID == id).AsQueryable();
            return Task.FromResult(clientesById);
        }

        public async Task<Clientes> Update(int id, Clientes clientes)
        {
            try
            {
                var cliente = await _dbContext.clientes.FindAsync(id);
                if (cliente != null)
                {
                    cliente.cliente = clientes.cliente;
                    cliente.telefono = clientes.telefono;
                    cliente.correo = clientes.correo;

                    await _dbContext.SaveChangesAsync();
                    return cliente;
                }
                return cliente;

            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
 
