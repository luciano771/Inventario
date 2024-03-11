using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDataAccess.Entities
{
    public class VentasItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDVenta { get; set; }
        public int IDProducto { get; set; }
        public int PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public int PrecioTotal { get; set; }

        [ForeignKey("IDProducto")]
        public virtual Productos? productos { get; set; }

        [ForeignKey("IDVenta")]
        public virtual Ventas? ventas { get; set; }


    }
}
