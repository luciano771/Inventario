using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioDataAccess.Entities
{
    public class Ventas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDCliente { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        [ForeignKey("IDCliente")]
        public virtual Clientes? clientes { get; set; }
    }
}
