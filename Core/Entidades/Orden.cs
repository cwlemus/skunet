using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrden { get; set; }        
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public Status IdStatus { get; set; }

        [ForeignKey("IX_Ordens_IdSkuNumber")]
        public virtual Sku IdSkuNumber { get; set; }
        
        
    }
}
