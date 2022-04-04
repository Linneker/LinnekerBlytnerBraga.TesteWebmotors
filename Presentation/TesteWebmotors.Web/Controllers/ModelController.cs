using Microsoft.AspNetCore.Mvc;
using TesteWebmotors.Dominio.DTO.ServicosExternos;
using TesteWebmotors.Servico.Requisicao;

namespace TesteWebmotors.Web.Controllers
{
    public class ModelController : Controller
    {
        private readonly ModelRequisicao _modelRequisicao;
        private readonly ILogger<ModelController> _logger;

        public ModelController(ModelRequisicao modelRequisicao, ILogger<ModelController> logger)
        {
            _modelRequisicao = modelRequisicao;
            _logger = logger;
        }

        [HttpGet]
        public async Task<JsonResult> ObterModelPeloMakeId(int makeId)
        {
            var model = await _modelRequisicao.ObterModelsPorMakeId("https://desafioonline.webmotors.com.br/api/OnlineChallenge/", makeId);
            return Json(model);
        }
    }
}
