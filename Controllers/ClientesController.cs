using Microsoft.AspNetCore.Mvc;
using DataAccess;
using BussinesLogic;
using BussinesLogic.Implementations;

using DataAccess.Entities;
using DataAccess.Repositories.Implementations;
using BussinessLogic.Interfaces;

namespace Inventario.Controllers
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clientesServices.GetById(id);
            if (cliente != null)
            {
                return Ok(cliente.ToList());
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClientesAsync()
        {
            var clientes = await _clientesServices.GetAll();
            return Ok(clientes.ToList());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Clientes clientes)
        {
            var resultado = await _clientesServices.Update(id, clientes);

            if (resultado is not null)
            {
                return Ok(resultado);
            }
            return NotFound("No se pudo encontrar el cliente para actualizar");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var resultado = await _clientesServices.Delete(id);

            if (resultado) { return Ok(resultado); }  return NotFound("No se logro eliminar el registro");
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(Clientes cliente)
        {
            var resultado = await _clientesServices.Create(cliente);

            if (resultado != null) { return Ok(resultado); }
            return NotFound("No se logro crear el registro");
        }
    }
}
