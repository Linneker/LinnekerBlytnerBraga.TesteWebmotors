using Microsoft.AspNetCore.Mvc;
using TesteWebmotors.Servico.Requisicao;

namespace TesteWebmotors.Web.Controllers
{
    public class VersionController : Controller
    {
        private readonly VersionRequisicao _versionRequisicao;
        private readonly ILogger<VersionController> _logger;

        public VersionController(VersionRequisicao versionRequisicao, ILogger<VersionController> logger)
        {
            _versionRequisicao = versionRequisicao;
            _logger = logger;
        }

        [HttpGet]
        public async Task<JsonResult> ObterVersionPeloMakeId(int modeloId)
        {
            var version = await _versionRequisicao.ObterVersionsPorModelId("https://desafioonline.webmotors.com.br/api/OnlineChallenge/", modeloId);
            return Json(version);
        }

    }
}
