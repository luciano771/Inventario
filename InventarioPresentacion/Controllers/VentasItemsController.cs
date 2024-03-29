using InventarioPresentacion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


using InventarioDataAccess.Entities;

using InventarioDataAccess;
using InventarioDataAccess.Repositories.Implementations;

using InventarioDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using InventarioBussinessLogic.Interfaces;
using InventarioPresentacion.Models;

namespace InventarioPresentacion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentasItemsController : Controller
    {

        private readonly IGeneralLogic<Ventas> _ventasServices;
        private readonly IGeneralLogic<Clientes> _clienteservices;
        private readonly IGeneralLogic<Productos> _productosServices;
        private readonly IGeneralLogic<VentasItems> _ventasItemsServices;

        public VentasItemsController(IGeneralLogic<Ventas> ventasServices, IGeneralLogic<Clientes> clienteservices, IGeneralLogic<Productos> productosServices, IGeneralLogic<VentasItems> ventasItemsServices)
        {
            _ventasServices = ventasServices;
            _clienteservices = clienteservices;
            _productosServices = productosServices;
            _ventasItemsServices = ventasItemsServices;
        }

        [HttpGet]
        public async Task<IActionResult> VentasItemsAsync(int id)
        {

            IEnumerable<Clientes> clientes = await _clienteservices.GetAll();

            IEnumerable<Productos> productos = await _productosServices.GetAll();

            IEnumerable<Ventas> ventas = await _ventasServices.GetAll();

            IEnumerable<VentasItems> ventasitems = await _ventasItemsServices.GetById(id);


            VentasViewModel viewModel = new VentasViewModel
            {
                clientes = clientes,
                productos = productos,
                ventas = ventas,
                ventasItems = ventasitems

            };

            // Pasa el modelo a la vista
            return View(viewModel);
        }


        [HttpGet("AltaVenta")]
        public async Task<IActionResult> VentasItemsCrearAsync()
        {

            IEnumerable<Clientes> clientes = await _clienteservices.GetAll();
             

            VentasViewModel viewModel = new VentasViewModel
            {
                clientes = clientes 
            };

            return View("SeleccionarCliente", viewModel);
        }

        [HttpGet("SeleccionarProductos")]
        public async Task<IActionResult> SeleccionarProductosAsync(int IDCliente)
        {

            IEnumerable<Productos> productos = await _productosServices.GetAll();
            IEnumerable<Clientes> clientes = await _clienteservices.GetAll();


            VentasViewModel viewModel = new VentasViewModel
            {
                productos = productos,
                clientes = clientes
            };

            return View("VentasItemsCrear", viewModel);
        }




        [HttpPost]
        public async Task<IActionResult> AltaItemVentaAsync([FromBody] List<VentasItems> ventasItems)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Ventas", "Ventas");
            }

            var ObjetoCreado = new VentasItems();

            foreach (VentasItems ventas in ventasItems)
            {
                ObjetoCreado = await _ventasItemsServices.Create(ventas);

                if (ObjetoCreado is null)
                {
                    return Json(new { success = true, message = "Error al insertar un item de ventas"});
                }
                
            }

            return Json(new { success = true, message = "Se insertaron todos los items de venta con exito", idVenta = ObjetoCreado.IDVenta });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItemVentaAsync([FromBody] VentasItems ventasItems)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Ventas", "Ventas");
            }

            var ObjetoCreado = await _ventasItemsServices.Update(ventasItems.ID, ventasItems);

            if (ObjetoCreado is null) 
            {
                return Json(new { success = true, message = "Error al insertar un item de ventas" });
            }

     
            return Json(new { success = true, message = "Se insertaron todos los items de venta con exito", idVenta = ObjetoCreado.IDVenta });
        }

        [HttpDelete("{id}/{idventa}")]
        public async Task<IActionResult> DeleteVentaItemsAsync(int id, int idventa)
        {
            var ventasItems = new VentasItems();

            ventasItems.ID = id; 
            ventasItems.IDVenta = idventa;

            var DeleteObject = await _ventasItemsServices.DeleteEntity
                (ventasItems);

            if (DeleteObject)
            {
                return Json(new { success = true, message = "Error al insertar un item de ventas" });
            }


            return Json(new { success = true, message = "Se insertaron todos los items de venta con exito"  });
        }


    }

}
