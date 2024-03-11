using InventarioBussinessLogic.Interfaces;
using InventarioDataAccess.Entities;
using InventarioDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioBussinessLogic.Implementations
{
    public class VentasItemsServices : IGeneralLogic<VentasItems>
    {
        private readonly IRepository<VentasItems> _ventasItemsRepository;
        private readonly IRepository<Ventas> _ventasRepository;

        public VentasItemsServices(IRepository<VentasItems> ventasItemsRepository, IRepository<Ventas> ventasRepository)
        {
            _ventasItemsRepository = ventasItemsRepository;
            _ventasRepository = ventasRepository;   
        }
        public async Task<VentasItems> Create(VentasItems ventasItems)
        {
            var ObjetoCreado = await _ventasItemsRepository.Create(ventasItems);
            var venta = new Ventas();

            var ventaItemQuery = await _ventasItemsRepository.GetById(ObjetoCreado.IDVenta);

            int precioTotal = 0;

            foreach (VentasItems item in ventaItemQuery)
            {
                precioTotal += item.PrecioTotal;
            }

            venta.Total = precioTotal;
            var UpdateVentas = await _ventasRepository.Update(ObjetoCreado.IDVenta, venta);
            return ObjetoCreado;

        }

      

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();

        }

        public Task<IQueryable<VentasItems>> GetAll()
        {
            return  _ventasItemsRepository.GetAll();
        }

        public Task<IQueryable<VentasItems>> GetAllPereyra()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<VentasItems>> GetById(int id)
        {
            var QueryResult = _ventasItemsRepository.GetById(id);
            return QueryResult;
        }

        public async Task<VentasItems> Update(int id, VentasItems ventasItems)
        {
            var UpdateVentasItems = await _ventasItemsRepository.Update(id, ventasItems);
            var venta = new Ventas();

            var ventaItemQuery = await _ventasItemsRepository.GetById(UpdateVentasItems.IDVenta);

            int precioTotal = 0;

            foreach (VentasItems item in ventaItemQuery)
            {
                precioTotal += item.PrecioTotal;
            }

            venta.Total = precioTotal;
            var UpdateVentas = await _ventasRepository.Update(UpdateVentasItems.IDVenta, venta);
            return UpdateVentasItems;
           
        }

        public async Task<bool> DeleteVentaItem(int idventaItem, int idventa)
        {
            var DeleteVentaItem = await _ventasItemsRepository.Delete(idventaItem);

            if (DeleteVentaItem)
            {
                var venta = new Ventas();

                var ventaItemQuery = await _ventasItemsRepository.GetById(idventa);

                int precioTotal = 0;

                foreach (VentasItems item in ventaItemQuery)
                {
                    precioTotal += item.PrecioTotal;
                }

                venta.Total = precioTotal;
                var UpdateVentas = await _ventasRepository.Update(idventa, venta);
                return DeleteVentaItem;
            }

            
            return DeleteVentaItem;

        }

        public async Task<bool> Delete2(VentasItems ventasItems)
        {
            var DeleteVentaItem = await _ventasItemsRepository.Delete(ventasItems.ID);

            if (DeleteVentaItem)
            {
                var venta = new Ventas();

                var ventaItemQuery = await _ventasItemsRepository.GetById(ventasItems.IDVenta);

                int precioTotal = 0;

                foreach (VentasItems item in ventaItemQuery)
                {
                    precioTotal += item.PrecioTotal;
                }

                venta.Total = precioTotal;
                var UpdateVentas = await _ventasRepository.Update(ventasItems.IDVenta, venta);
                return DeleteVentaItem;
            }


            return DeleteVentaItem;

        }
    }
}
