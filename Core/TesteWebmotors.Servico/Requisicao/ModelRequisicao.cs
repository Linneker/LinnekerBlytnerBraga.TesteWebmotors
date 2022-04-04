using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.DTO.ServicosExternos;

namespace TesteWebmotors.Servico.Requisicao
{
    public class ModelRequisicao
    {
        public async Task<List<ModelDTO>> ObterModelsPorMakeId(string url,int makeId)
        {
            var client = new RestClient(url);

            RestRequest request = new RestRequest($"Model?MakeID={makeId}", Method.Get);

            var response = await client.GetAsync<List<ModelDTO>>(request);
            response.RemoveAll(t => t.Name.Length > 45);
            return response;
        }
    }
}
