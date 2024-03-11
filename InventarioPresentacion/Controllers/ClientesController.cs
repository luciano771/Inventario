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
    public class ClientesController : Controller
    {
        private readonly IGeneralLogic<Clientes> _clientesServices;

        public ClientesController(IGeneralLogic<Clientes> clientesServices)
        {
            _clientesServices = clientesServices;
        }

        [HttpGet]
        public async Task<IActionResult> ClientesAsync()
        {
            var listaClientes = await  _clientesServices.GetAll();
            return View(listaClientes);
        }
        [HttpPost]
        public async Task<IActionResult> OnPostCrearCliente([FromForm] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                await _clientesServices.Create(clientes);
                return RedirectToAction("Clientes", "Clientes");
            }

            return View("Clientes");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClienteAsync(int id)
        {
 
            var resultado = await _clientesServices.Delete(id);

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
        public async Task<IActionResult> UpdateClientesAsync([FromBody] Clientes clientes)
        { 
            var resultado = await _clientesServices.Update(clientes.ID,clientes);
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
