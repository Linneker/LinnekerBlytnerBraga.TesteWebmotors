using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Entidade;
using TesteWebmotors.Dominio.Interface.Repositorio;
using TesteWebmotors.Dominio.Interface.Servico;

namespace TesteWebmotors.Servico.Servicos
{
    public class BaseServico<Entidade> : IBaseServico<Entidade> where Entidade : EntidadeBase 
    {
        private readonly IBaseRepositorio<Entidade> _repositorioBase;

        public BaseServico(IBaseRepositorio<Entidade> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public virtual async Task Atualizar(Entidade entidade) => _repositorioBase.Atualizar(entidade);

        public virtual void Deletar(Entidade entidade) => _repositorioBase.Deletar(entidade);

        public virtual Task<Entidade> ObterPorId(int id) => _repositorioBase.ObterPorId(id);

        public virtual Task<IQueryable<Entidade>> ObterTodos() => _repositorioBase.ObterTodos();
    }
}
