using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class LogSku
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLog { get; set; }

        [ForeignKey("IX_LogSkus_IdSkuNumber")]
        public Sku IdSkuNumber { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public Status IdStatus { get; set; }

    }
}
