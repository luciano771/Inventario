using Microsoft.AspNetCore.Mvc;
using DataAccess;
using BussinesLogic;
using BussinesLogic.Repositories.Interfaces;
using BussinesLogic.Repositories.Implementations;

using DataAccess.Entities;
using DataAccess.Repositories.Implementations;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly IClientesServices _clientesServices;

        public ClientesController(IClientesServices clientesServices)
        {
            _clientesServices = clientesServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientesPereyraAsync()
        {
            var clientesPereyra = await _clientesServices.GetAllClientesPereyra();
            return Ok(clientesPereyra.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> GetAllClientesAsync()
        {
            var clientes = await _clientesServices.GetAllClientesAsync();
            return Ok(clientes.ToList());
        }
    }
}
