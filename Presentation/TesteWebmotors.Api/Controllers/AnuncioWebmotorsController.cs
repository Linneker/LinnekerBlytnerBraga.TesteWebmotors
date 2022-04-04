using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

    }
}
