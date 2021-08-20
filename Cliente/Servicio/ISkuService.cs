using ClienteDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.Servicio
{
    interface ISkuService
    {
       Task<IEnumerable<SkuDTO>> GetAllSkus(String filtro);
       Task<IEnumerable<ExistenciaDTO>> GetAllExistencia(String filtro);
       Task<bool> PostOrden(string url, OrdenesDTO enviar);
       Task<string> DelOrder(String filtro);        
    }
}
