using InventarioDataAccess.Entities;

namespace InventarioPresentacion.Models
{
    public class VentasViewModel
    {
        public IEnumerable<Ventas> ventas { get; set; }
        public IEnumerable<VentasItems> ventasItems { get; set; }
        public IEnumerable<Clientes> clientes { get; set; }
        public IEnumerable<Productos> productos { get; set; }
        public IEnumerable<VentasItems> productosAgregados { get; set; }
    }
}
