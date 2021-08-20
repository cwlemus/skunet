﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteDTO.DTO
{
    public class SkuDTO
    {
        [JsonProperty("skuNumber")]
        public int SkuNumber { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }
        [JsonProperty("stock")]
        public int Stock { get; set; }
    }
}
