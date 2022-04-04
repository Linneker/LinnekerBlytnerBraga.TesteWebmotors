using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using TesteWebmotors.Dominio.Ajudantes;
using TesteWebmotors.Dominio.DTO.ServicosExternos;
using TesteWebmotors.Dominio.Entidade;
using TesteWebmotors.Dominio.Interface.Servico;
using TesteWebmotors.Servico.Requisicao;
using TesteWebmotors.Web.Configurations.Filtler;
using TesteWebmotors.Web.Models;

namespace TesteWebmotors.Web.Controllers
{
    public class AnuncioWebmotorsController : Controller
    {
        private readonly ILogger<AnuncioWebmotorsController> _logger;
        private readonly IAnuncioWebmotorsServico _service;
        private readonly MakeRequisicao _makeRequisicao;
        private static List<MakeDTO> _makes;
        private readonly ModelRequisicao _modelRequisicao;
        private readonly VersionRequisicao _versionRequisicao;
        private const string URL_SERVICO = "https://desafioonline.webmotors.com.br/api/OnlineChallenge/";

        public AnuncioWebmotorsController(IAnuncioWebmotorsServico service,
            ILogger<AnuncioWebmotorsController> logger,
            MakeRequisicao makeRequisicao, ModelRequisicao modelRequisicao, VersionRequisicao versionRequisicao)
        {
            _logger = logger;
            _service = service;
            _makeRequisicao = makeRequisicao;
            _modelRequisicao = modelRequisicao;
            _versionRequisicao = versionRequisicao;
        }

        // GET: AnuncioWebmotorsController
        public async Task<ActionResult> Index()
        {
            AnuncioFiltroViewModel anuncioFiltroViewModel = new AnuncioFiltroViewModel();
            _makes = await _makeRequisicao.ObterMakes(URL_SERVICO);
            List<SelectListItem> selectItens = _makes.Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString(),
            }).ToList();
            selectItens.Add(new SelectListItem()
            {
                Text = "...SELECIONE UM VALOR...",
                Value = "0",
                Selected = true
            });
            ViewBag.MarcasIndex = selectItens;
            var dados = await _service.ObterTodos();
            anuncioFiltroViewModel.Anuncios = dados;
            return View(anuncioFiltroViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilter]
        public async Task<ActionResult> Index(AnuncioFiltroViewModel filtroAnuncioWebmotors)
        {

            _makes = await _makeRequisicao.ObterMakes(URL_SERVICO);
            List<SelectListItem> selectItens = _makes.Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = t.Id.ToString(),
                Selected = false
            }).ToList();
            selectItens.Add(new SelectListItem()
            {
                Text = "...SELECIONE UM VALOR...",
                Value = "0",
                Selected = true
            });
            ViewBag.MarcasIndex = selectItens;
            AnuncioFiltroViewModel anuncioFiltroViewModel = new AnuncioFiltroViewModel();
            var model = await _modelRequisicao.ObterModelsPorMakeId(URL_SERVICO, int.Parse(filtroAnuncioWebmotors.Filtro.Marca));

            FiltroAnuncioWebmotors filtros = new FiltroAnuncioWebmotors(filtroAnuncioWebmotors.Filtro.Id,
                filtroAnuncioWebmotors.Filtro.Marca is null ? null : _makes.Where(t => t.Id.Equals(int.Parse(filtroAnuncioWebmotors.Filtro.Marca))).Select(t => t.Name).FirstOrDefault(),
                filtroAnuncioWebmotors.Filtro.Modelo is null ? null : model.Where(t => t.Id.Equals(int.Parse(filtroAnuncioWebmotors.Filtro.Modelo))).Select(t => t.Name).FirstOrDefault(),
                filtroAnuncioWebmotors.Filtro.Versao, filtroAnuncioWebmotors.Filtro.Ano, filtroAnuncioWebmotors.Filtro.Quilometragem, filtroAnuncioWebmotors.Filtro.Observacao);

            var dados = await _service.Filtrar(filtros);
            anuncioFiltroViewModel.Anuncios = dados;

            return View(anuncioFiltroViewModel);
        }

        // GET: AnuncioWebmotorsController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnuncioWebmotorsController1/Create
        public async Task<ActionResult> Create()
        {
            _makes = await _makeRequisicao.ObterMakes(URL_SERVICO);
            List<SelectListItem> selectItens = _makes.Select(t => new SelectListItem() { Text = t.Name, Value = t.Id.ToString() }).ToList();
            ViewBag.Marcas = selectItens;

            return View();
        }

        // POST: AnuncioWebmotorsController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilterAsync]
        public async Task<ActionResult> Create(AnuncioWebmotorsViewModel collection)
        {
            try
            {
                var model = await _modelRequisicao.ObterModelsPorMakeId(URL_SERVICO, int.Parse(collection.Marca));
                AnuncioWebmotors awm = new AnuncioWebmotors(_makes.Where(t => t.Id.Equals(int.Parse(collection.Marca))).Select(t => t.Name).FirstOrDefault(), model.Where(t => t.Id.Equals(int.Parse(collection.Modelo))).Select(t => t.Name).FirstOrDefault(), collection.Versao, collection.Ano, collection.Quilometragem, collection.Observacao);

                await _service.AdicionarAssincrono(awm);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnuncioWebmotorsController1/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var collection = await _service.ObterPorId(id);
            _makes = await _makeRequisicao.ObterMakes(URL_SERVICO);
            List<SelectListItem> selectItens = _makes.Select(t => new SelectListItem() { 
                Text = t.Name, 
                Value = t.Id.ToString(),
                Selected = t.Name.Equals(collection.Marca)
            }).ToList();
            ViewBag.MarcasEditar = selectItens;

            var model = await _modelRequisicao.ObterModelsPorMakeId(URL_SERVICO, _makes.Where(t => t.Name.Equals(collection.Marca)).Select(t => t.Id).FirstOrDefault());
            List<SelectListItem> selectItensModel = model.Select(t => new SelectListItem() 
            { 
                Text = t.Name, 
                Value = t.Id.ToString(),
                Selected = t.Name.Equals(collection.Modelo)
            }).ToList();
            ViewBag.ModelosEditar = selectItensModel;

            var version = await _versionRequisicao.ObterVersionsPorModelId(URL_SERVICO, model.Where(t => t.Name.Equals(collection.Modelo)).Select(t => t.Id).FirstOrDefault());
            List<SelectListItem> selectItensVersion = version.Select(t => new SelectListItem() { 
                Text = t.Name, 
                Value = t.Name.ToString(),
                Selected = t.Name.Equals(collection.Versao)
            }).ToList();
            ViewBag.VersoesEditar = selectItensVersion;
            
            AnuncioWebmotorsViewModel awm = new AnuncioWebmotorsViewModel(collection.Id, collection.Marca, collection.Modelo, collection.Versao, collection.Ano, collection.Quilometragem, collection.Observacao);
            return View(awm);
        }

        // POST: AnuncioWebmotorsController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilter]
        public async Task<ActionResult> Edit(int id, AnuncioWebmotorsViewModel collection)
        {
            try
            {
                var model = await _modelRequisicao.ObterModelsPorMakeId(URL_SERVICO, int.Parse(collection.Marca));
                AnuncioWebmotors awm = new AnuncioWebmotors(id,_makes.Where(t => t.Id.Equals(int.Parse(collection.Marca))).Select(t => t.Name).FirstOrDefault(), model.Where(t => t.Id.Equals(int.Parse(collection.Modelo))).Select(t => t.Name).FirstOrDefault(), collection.Versao, collection.Ano, collection.Quilometragem, collection.Observacao);
                _service.Atualizar(awm);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: AnuncioWebmotorsController1/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var collection = await _service.ObterPorId(id);
            AnuncioWebmotorsViewModel awm = new AnuncioWebmotorsViewModel(collection.Id, collection.Marca, collection.Modelo, collection.Versao, collection.Ano, collection.Quilometragem, collection.Observacao);
            return View(awm);
        }

        // POST: AnuncioWebmotorsController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UnitOfWorkFilter]
        public async Task<ActionResult> Delete(int id, AnuncioWebmotorsViewModel collection)
        {
            try
            {
                AnuncioWebmotors awm = null;
                if (collection.Marca is null)
                {
                    awm = await _service.ObterPorId(id);
                }
                else { 
                    awm = new AnuncioWebmotors(id, collection.Marca, collection.Modelo, collection.Versao, collection.Ano, collection.Quilometragem, collection.Observacao);
                }
                _service.Deletar(awm);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
