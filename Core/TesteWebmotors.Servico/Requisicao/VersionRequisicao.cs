using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.DTO.ServicosExternos;

namespace TesteWebmotors.Servico.Requisicao
{
    public class VersionRequisicao
    {
        public async Task<List<VersionDTO>> ObterVersionsPorModelId(string url, int modeloId)
        {
            var client = new RestClient(url);

            RestRequest request = new RestRequest($"Version?ModelID={modeloId}", Method.Get);

            var response = await client.GetAsync<List<VersionDTO>>(request);
            response.RemoveAll(t => t.Name.Length > 45);
            return response;
        }
    }
}
