using BussinesLogic.Repositories.Interfaces;
using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {

        private readonly IVentasServices _ventasService;

        public VentasController(IVentasServices ventasService)
        {
            _ventasService = ventasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientesPereyraAsync()
        {
            var clientesPereyra = await _ventasService.GetAllVentasWithClientesAsync();
            return Ok(clientesPereyra.ToList());
        }
 
    }
}
