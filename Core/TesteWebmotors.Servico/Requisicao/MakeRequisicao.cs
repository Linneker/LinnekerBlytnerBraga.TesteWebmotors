using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.DTO.ServicosExternos;

namespace TesteWebmotors.Servico.Requisicao
{
    public class MakeRequisicao
    {
        public async Task<List<MakeDTO>> ObterMakes(string url)
        {
            var client = new RestClient(url);

            RestRequest request = new RestRequest("Make", Method.Get);
            
            var response = await client.GetAsync<List<MakeDTO>>(request);
            response.RemoveAll(t => t.Name.Length > 45);
            return response;
        }
    }
}
