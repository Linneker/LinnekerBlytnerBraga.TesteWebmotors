using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteWebmotors.Api.Configurations.Filtler;
using TesteWebmotors.Dominio.Ajudantes;
using TesteWebmotors.Dominio.DTO.Api;
using TesteWebmotors.Dominio.Entidade;
using TesteWebmotors.Dominio.Interface.Servico;

namespace TesteWebmotors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioWebmotorsController : BaseController<AnuncioWebmotors>
    {
        private readonly IAnuncioWebmotorsServico _service;
        public AnuncioWebmotorsController(IAnuncioWebmotorsServico service) : base(service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        [UnitOfWorkFilter]
        public async Task AdicionarAsync(AnuncioWebmotorsDTO entity)
        {
            await _service.AdicionarAssincrono(entity);
        }
        [HttpGet("ObterPorMarca/{marca}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnuncioWebmotors))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorMarca(string marca)
        {
            var resultado = await _service.ObterPorMarca(marca);
            if (resultado is not null)
                return Ok(resultado);
            else
                return NotFound();
        }


        [HttpGet("Filtrar/{id}/{marcar}/{modelo}/{versao}/{ano}/{quilometragem}/{observacao}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnuncioWebmotors))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Filtrar(int? id,
            string? marca,
            string? modelo, string? versao, int? ano, int? quilometragem, string? observacao)
        {
            FiltroAnuncioWebmotors filtro = new FiltroAnuncioWebmotors(id, marca, modelo, versao, ano, quilometragem, observacao);
            var resultado = await _service.Filtrar(filtro);
            if (resultado is not null)
                return Ok(resultado);
            else
                return NotFound();
        }


        [HttpGet("Paginador/{pagina}/{totalPorPagina}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnuncioWebmotors))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Paginador(int pagina, int totalPorPagina)
        {
            var resultado = await _service.Paginador(pagina, totalPorPagina);
            if (resultado is not null)
                return Ok(resultado);
            else
                return NotFound();
        }
    }
}
