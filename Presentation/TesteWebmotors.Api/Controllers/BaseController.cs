using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteWebmotors.Api.Configurations.Filtler;
using TesteWebmotors.Dominio.Entidade;
using TesteWebmotors.Dominio.Interface.Servico;

namespace TesteWebmotors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entidade> : ControllerBase where Entidade : EntidadeBase
    {
        protected readonly IBaseServico<Entidade> _baseService;
        public BaseController(IBaseServico<Entidade> service)
        {
            _baseService = service;
        }

        [HttpPost("Create")]
        [UnitOfWorkFilter]
        public void AdicionarAsync(Entidade entity) => _baseService.Adicionar(entity);

        [HttpGet("Get/{id}")]
        public Task<Entidade> ObterPorId(int id) => _baseService.ObterPorId(id);

        [HttpGet]
        public async Task<IQueryable<Entidade>> GetAsync() => await _baseService.ObterTodos();

        [HttpGet("ToList")]
        public async Task<List<Entidade>> ToList() => (await _baseService.ObterTodos()).ToList();


        [HttpPut("Update")]
        [UnitOfWorkFilter]
        public void Update(Entidade entity) => _baseService.Atualizar(entity);

        
        [HttpDelete("Remove")]
        [UnitOfWorkFilter]
        public void Remove(Entidade entity) => _baseService.Deletar(entity);

        [HttpDelete("{id}")]
        [UnitOfWorkFilter]
        public void DeletePorId(int id)
        {
            var entity = _baseService.ObterPorId(id);
            _baseService.Deletar(entity.Result);
        }

    }
}
