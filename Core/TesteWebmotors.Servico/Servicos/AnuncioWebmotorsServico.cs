using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Ajudantes;
using TesteWebmotors.Dominio.DTO.Api;
using TesteWebmotors.Dominio.DTO.ServicosExternos;
using TesteWebmotors.Dominio.Entidade;
using TesteWebmotors.Dominio.Interface.Repositorio;
using TesteWebmotors.Dominio.Interface.Servico;
using TesteWebmotors.Servico.Requisicao;

namespace TesteWebmotors.Servico.Servicos
{
    public class AnuncioWebmotorsServico : BaseServico<AnuncioWebmotors>, IAnuncioWebmotorsServico
    {
        private readonly IAnuncioWebmotorsRepositorio _anuncioWebmotorsRepositorio;
        private const string URL_SERVICO = "https://desafioonline.webmotors.com.br/api/OnlineChallenge/";
        private readonly MakeRequisicao _makeRequisicao;
        private readonly ModelRequisicao _modelRequisicao;
        private readonly VersionRequisicao _versionRequisicao;
        private readonly VehiclesRequisicao _vehiclesRequisição;

        public AnuncioWebmotorsServico(IAnuncioWebmotorsRepositorio anuncioWebmotorsRepositorio,
            MakeRequisicao makeRequisicao, ModelRequisicao modelRequisicao
            , VersionRequisicao versionRequisicao, VehiclesRequisicao vehiclesRequisição) : base(anuncioWebmotorsRepositorio)
        {
            _anuncioWebmotorsRepositorio = anuncioWebmotorsRepositorio;
            _makeRequisicao = makeRequisicao;
            _modelRequisicao = modelRequisicao;
            _versionRequisicao = versionRequisicao;
            _vehiclesRequisição = vehiclesRequisição;
        }

        public async Task AdicionarAssincrono(AnuncioWebmotorsDTO entidade)
        {
            AnuncioWebmotors anuncio = ParseAnuncioWebmotorsDTOToAnuncioWebmotors(entidade);
            await ValidaDados(anuncio);
            await _anuncioWebmotorsRepositorio.AdicionarAssincrono(anuncio);
        }

        public override async Task Atualizar(AnuncioWebmotors entidade)
        {
            await ValidaDados(entidade);
            _anuncioWebmotorsRepositorio.Atualizar(entidade);
        }

        public Task<List<AnuncioWebmotors>> Filtrar(FiltroAnuncioWebmotors anuncioWebmotors) => _anuncioWebmotorsRepositorio.Filtrar(anuncioWebmotors);

        public Task<AnuncioWebmotors> ObterPorMarca(string marca) => _anuncioWebmotorsRepositorio.ObterPorMarca(marca);

        public Task<List<AnuncioWebmotors>> Paginador(int pagina, int totalPorPagina)
        {
            return _anuncioWebmotorsRepositorio.Paginador(pagina, totalPorPagina);
        }

        private AnuncioWebmotors ParseAnuncioWebmotorsDTOToAnuncioWebmotors(AnuncioWebmotorsDTO entidade)
        {
            AnuncioWebmotors anuncio = new AnuncioWebmotors(entidade.Marca, entidade.Modelo, entidade.Versao, entidade.Ano,
                entidade.Quilometragem, entidade.Observacao);
            return anuncio;
        }


        private async Task ValidaDados(AnuncioWebmotors entidade)
        {
            int idMake = 0;
            int idModel = 0;
            var makes = await _makeRequisicao.ObterMakes(URL_SERVICO);
            if (!makes.Where(t => t.Name.Equals(entidade.Marca)).Any())
            {
                throw new Exception("Marca não encontrada!");
            }
            else
            {
                idMake = makes.Where(t => t.Name.Equals(entidade.Marca)).Select(t => t.Id).First();
            }
            var models = await _modelRequisicao.ObterModelsPorMakeId(URL_SERVICO, idMake);
            if (!models.Where(t => t.Name.Equals(entidade.Modelo)).Any())
            {
                throw new Exception("Modelo não encontrada!");
            }
            else
            {
                idModel = models.Where(t => t.Name.Equals(entidade.Modelo)).Select(t => t.Id).First();
            }

            var version = await _versionRequisicao.ObterVersionsPorModelId(URL_SERVICO, idModel);
            if (!version.Where(t => t.Name.Equals(entidade.Versao)).Any())
            {
                throw new Exception("Versão não encontrada!");
            }
            List<VehiclesDTO> vehiclesDTOs = new List<VehiclesDTO>();
            bool anoQuilometragemVeiculoEncontrado = false;
            int i = 1;
            do
            {
                vehiclesDTOs = await _vehiclesRequisição.ObterVehiclesPorPage(URL_SERVICO, i);
                anoQuilometragemVeiculoEncontrado = (vehiclesDTOs.Where(t => t.KM == entidade.Quilometragem
                    && t.YearModel == entidade.Ano
                    && t.Make.Equals(entidade.Marca) && t.Model.Equals(entidade.Modelo) && t.Version.Equals(entidade.Versao))
                    .Any());
                if (anoQuilometragemVeiculoEncontrado) break;

                i++;
            } while (vehiclesDTOs.Count > 0);

            if(!anoQuilometragemVeiculoEncontrado)
                throw new Exception("Não possuimos esse veiculo nesse ano com essa quilometragem!");

        }

    }
}
