using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteDTO.DTO
{
    public class ExistenciaDTO
    {
        public int IdOrden { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int Stock { get; set; }
    }
}
