using InventarioPresentacion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


using InventarioDataAccess.Entities;

using InventarioDataAccess;
using InventarioDataAccess.Repositories.Implementations;
using InventarioBussinessLogic.Interfaces;
using InventarioDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioPresentacion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentasController : Controller
    {
        private readonly IGeneralLogic<Ventas> _ventasServices;
        private readonly IGeneralLogic<Clientes> _clienteservices;
        private readonly IGeneralLogic<Productos> _productosServices;

        public VentasController(IGeneralLogic<Ventas> ventasServices, IGeneralLogic<Clientes> clienteservices, IGeneralLogic<Productos> productosServices)
        {
            _ventasServices = ventasServices;
            _clienteservices = clienteservices;
            _productosServices = productosServices;
        }

        [HttpGet]
        public async Task<IActionResult> VentasAsync()
        {
       
            IEnumerable<Clientes> clientes = await _clienteservices.GetAll();

            IEnumerable<Productos> productos = await _productosServices.GetAll();

            IEnumerable<Ventas> ventas = await _ventasServices.GetAll();


            VentasViewModel viewModel = new VentasViewModel
            {
                clientes = clientes,
                productos = productos,
                ventas = ventas
                 
            };

            // Pasa el modelo a la vista
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCrearVentas([FromBody] Ventas ventas)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Ventas", "Ventas");
            }

            var RegistroVenta = await _ventasServices.Create(ventas);
            var IDVenta = RegistroVenta.ID;

            if (RegistroVenta is not null)
            {
                return Json(new { success = true, message = "Actualización exitosa", idVenta = IDVenta });
            }
            else
            {
                return Json(new { success = false, message = "No se logró actualizar el registro" });
            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentasAsync(int id)
        {

            var resultado = await _ventasServices.Delete(id);

            if (resultado)
            {
                return Json(new { success = true, message = "Actualización exitosa", redirectTo = Url.Action("") }); // redirijimos a la misma pagina
            }
            else
            {
                return Json(new { success = false, message = "No se logró actualizar el registro" });
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateVentasAsync([FromBody] Ventas ventas)
        {
            var resultado = await _ventasServices.Update(ventas.ID, ventas);
            if (resultado is not null)
            {
                return Json(new { success = true, message = "Actualización exitosa", redirectTo = Url.Action("") }); // redirijimos a la misma pagina
            }
            else
            {
                return Json(new { success = false, message = "No se logró actualizar el registro" });
            }
        }
    }
}
