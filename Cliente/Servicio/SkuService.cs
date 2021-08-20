using ClienteDTO.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Cliente.Servicio
{
    public class SkuService : ISkuService
    {
        public readonly HttpClient _httpClient;
        public SkuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<SkuDTO>> GetAllSkus(String filtro)
        {
            String uriApi = "api/allskus";
            if (!filtro.Equals("")) uriApi = "api/allskus/" + filtro;
           return JsonConvert.DeserializeObject<IEnumerable<SkuDTO>>(
           await _httpClient.GetStringAsync($""+uriApi));
        }

        public async Task<string> DelOrder(String filtro)
        {
            return await _httpClient.GetStringAsync("api/deleteOrder/" + filtro);
        }


        public async Task<IEnumerable<ExistenciaDTO>> GetAllExistencia(String filtro)
        {
            String uriApi = "api/displayOrder/";
            if (!filtro.Equals("")) uriApi = "api/displayOrder/" + filtro;
            return JsonConvert.DeserializeObject<IEnumerable<ExistenciaDTO>>(
            await _httpClient.GetStringAsync($"" + uriApi));
        }


        public async Task<bool> PostOrden(string url, OrdenesDTO enviar)
        {
            var enviarJSON = System.Text.Json.JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var response= await _httpClient.PostAsync(url, enviarContent);
            return response.IsSuccessStatusCode;
        }

    }
}
