
using BussinessLogic.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {

        private readonly IGeneralLogic<Ventas> _generalLogic;

        public VentasController(IGeneralLogic<Ventas> GeneralLogic)
        {
            _generalLogic = GeneralLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientesPereyraAsync()
        {
            var clientesPereyra = await _generalLogic.GetAllAsync();
            return Ok(clientesPereyra.ToList());
        }
 
    }
}
