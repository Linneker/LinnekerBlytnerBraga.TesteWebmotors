using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.DTO.ServicosExternos;

namespace TesteWebmotors.Servico.Requisicao
{
    public class VehiclesRequisicao
    {
        public async Task<List<VehiclesDTO>> ObterVehiclesPorPage(string url, int page)
        {
            var client = new RestClient(url);

            RestRequest request = new RestRequest($"Vehicles?Page={page}", Method.Get);

            var response = await client.GetAsync<List<VehiclesDTO>>(request);
            return response;
        }
    }
}
