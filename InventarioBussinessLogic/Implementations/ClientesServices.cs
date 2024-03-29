using InventarioBussinessLogic.Interfaces;
using InventarioBussinessLogic.Implementations;
using InventarioDataAccess.Entities;
using InventarioDataAccess.Repositories.Implementations;
using InventarioDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace InventarioBussinessLogic.Implementations
{
    public class ClientesServices : IGeneralLogic<Clientes>
    {
        private readonly IRepository<Clientes> _clientesRepository;

        public ClientesServices(IRepository<Clientes> clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }
         
        public Task<IQueryable<Clientes>> GetAll()
        {
            return _clientesRepository.GetAll();
        }
         

        public async Task<IQueryable<Clientes>> GetById(int id)
        {
            return await _clientesRepository.GetById(id);

        }

        public async Task<Clientes> Create(Clientes clientes)
        {
            try
            {
                var NuevoCliente = await _clientesRepository.Create(clientes).ConfigureAwait(false);
                return NuevoCliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el cliente: {ex.Message}");
                // Puedes manejar la excepción según tus necesidades, como registrarla, notificar al usuario, etc.
                // También puedes lanzar la excepción nuevamente si es necesario propagarla a un nivel superior.
                throw;
            }
        }

       
        public async Task<bool> Delete(int id)
        {
            return await _clientesRepository.Delete(id);

        }

        public async Task<Clientes> Update(int id, Clientes clientes)
        {
            return await _clientesRepository.Update(id, clientes);

        }

        public Task<bool> DeleteEntity(Clientes entity)
        {
            throw new NotImplementedException();
        }
    }
}
 
