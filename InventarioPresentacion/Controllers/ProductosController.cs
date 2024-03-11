using InventarioPresentacion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


using InventarioDataAccess.Entities;

using InventarioDataAccess;
using InventarioDataAccess.Repositories.Implementations;

using InventarioDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using InventarioBussinessLogic.Interfaces;

namespace InventarioPresentacion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : Controller
    {
        private readonly IGeneralLogic<Productos> _productosServices;

        public ProductosController(IGeneralLogic<Productos> productosServices)
        {
            _productosServices = productosServices;
        }

        [HttpGet]
        public async Task<IActionResult> ProductosAsync()
        {
            IEnumerable<Productos> productos = await _productosServices.GetAll();
             
            VentasViewModel viewModel = new VentasViewModel
            {
                productos = productos
            };

            // Pasa el modelo a la vista 
            return View(viewModel);
        }
   


        [HttpPost]
        public async Task<IActionResult> OnPostCrearProducto([FromForm] Productos productos)
        {
            if (ModelState.IsValid)
            {
                await _productosServices.Create(productos);
                return RedirectToAction("Productos", "Productos");
            }

            return View("Productos");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductosAsync(int id)
        {

            var resultado = await _productosServices.Delete(id);

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
        public async Task<IActionResult> UpdateClientesAsync([FromBody] Productos productos)
        {
            var resultado = await _productosServices.Update(productos.ID, productos);
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
