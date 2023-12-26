using Microsoft.AspNetCore.Mvc;
using DataAccess;
using BussinesLogic;
using BussinesLogic.Repositories.Interfaces;
using DataAccess.Entities;

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
        public async Task<IActionResult> GetAllClientesAsync()
        {
            var clientes = await _clientesServices.GetAllClientesAsync();
            return Ok(clientes.ToList());
        }
    }
}
