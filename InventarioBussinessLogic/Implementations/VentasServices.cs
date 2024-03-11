using InventarioBussinessLogic.Interfaces;
using InventarioDataAccess.Entities;
using InventarioDataAccess.Repositories.Implementations;
using InventarioDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioBussinessLogic.Implementations
{
    public class VentasServices : IGeneralLogic<Ventas>
    {
        private readonly IRepository<Ventas> _ventasServices;

        public VentasServices(IRepository<Ventas> ventasServices)
        {
            _ventasServices = ventasServices;
        }
        public Task<IQueryable<Ventas>> GetAll()
        {
            return _ventasServices.GetAll();
        }

        public async Task<IQueryable<Ventas>> GetAllPereyra()
        {
            var productos = await _ventasServices.GetAll();
            var productosPereyra = productos.Where(c => c.ID > 3).AsQueryable();
            return productosPereyra;
        }

        public async Task<IQueryable<Ventas>> GetById(int id)
        {
            return await _ventasServices.GetById(id);

        }

        public async Task<Ventas> Create(Ventas ventas)
        {
            try
            {
                var NuevoProducto = await _ventasServices.Create(ventas).ConfigureAwait(false);
                return NuevoProducto;
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
            return await _ventasServices.Delete(id);

        }

        public async Task<Ventas> Update(int id, Ventas ventas)
        {
            return await _ventasServices.Update(id, ventas);

        }

        public Task<bool> Delete2(Ventas entity)
        {
            throw new NotImplementedException();
        }
    }
}
