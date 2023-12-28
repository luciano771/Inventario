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
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly IGeneralLogic<Clientes> _clientesServices;

        public ClientesController(IGeneralLogic<Clientes> clientesServices)
        {
            _clientesServices = clientesServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientesPereyraAsync()
        {
            var clientesPereyra = await _clientesServices.GetAllPereyra();
            return Ok(clientesPereyra.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> GetAllClientesAsync()
        {
            var clientes = await _clientesServices.GetAllAsync();
            return Ok(clientes.ToList());
        }
    }
}
