using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteDTO.DTO
{
    public class OrdenesDTO
    {
        [JsonProperty("idorden")]
        public int IdOrden { get; set; }
        [JsonProperty("skuNumber")]
        public int SkuNumber { get; set; }        
        public int Cantidad { get; set; }        
    }
}
