using InventarioBussinessLogic.Interfaces;
using InventarioDataAccess.Entities;
using InventarioDataAccess.Repositories.Implementations;
using InventarioDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioBussinessLogic.Implementations
{
    public class ProductosServices : IGeneralLogic<Productos>
    {
        private readonly IRepository<Productos> _productosServices;

        public ProductosServices(IRepository<Productos> productosRepository)
        {
            _productosServices = productosRepository;
        }
        public Task<IQueryable<Productos>> GetAll()
        {
            return _productosServices.GetAll();
        }

        public async Task<IQueryable<Productos>> GetAllPereyra()
        {
            var productos = await _productosServices.GetAll();
            var productosPereyra = productos.Where(c => c.ID > 3).AsQueryable();
            return productosPereyra;
        }

        public async Task<IQueryable<Productos>> GetById(int id)
        {
            return await _productosServices.GetById(id);

        }

        public async Task<Productos> Create(Productos productos)
        {
            try
            {
                var NuevoProducto = await _productosServices.Create(productos).ConfigureAwait(false);
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
            return await _productosServices.Delete(id);

        }

        public async Task<Productos> Update(int id, Productos productos)
        {
            return await _productosServices.Update(id, productos);

        }

        public Task<bool> Delete2(Productos entity)
        {
            throw new NotImplementedException();
        }
    }
}
