using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Interface.Servico;
using TesteWebmotors.Servico.Requisicao;
using TesteWebmotors.Web.Models;

namespace TesteWebmotors.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnuncioWebmotorsServico _service;

        public HomeController(IAnuncioWebmotorsServico service, ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            MakeRequisicao makeRequisicao = new MakeRequisicao();
            var resultado = await makeRequisicao.ObterMakes("https://desafioonline.webmotors.com.br/api/OnlineChallenge/");

            ModelRequisicao modelRequisicao = new ModelRequisicao();
            var resultadoModel = await makeRequisicao.ObterMakes("https://desafioonline.webmotors.com.br/api/OnlineChallenge/");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}