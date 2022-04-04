using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Entidade;
using TesteWebmotors.Dominio.Interface.Repositorio;
using TesteWebmotors.Infraestrutura.Config;

namespace TesteWebmotors.Repositorio.Repositorio
{
    public class BaseRepositorio<Entidade> : IBaseRepositorio<Entidade> where Entidade : EntidadeBase
    {
        protected readonly Context _context;
        protected readonly DbSet<Entidade> _dbSet;
        public BaseRepositorio(Context context)
        {
            _context = context;
            _dbSet = _context.Set<Entidade>();
        }
        public void Adicionar(Entidade entity) => _dbSet.Add(entity);
        public async Task AdicionarAssincrono(Entidade entity) => await _dbSet.AddAsync(entity);
        public void Atualizar(Entidade entity) => _dbSet.Update(entity);
        public void Deletar(Entidade entity) => _dbSet.Remove(entity);
        public async Task<Entidade> ObterPorId(int id) => await _dbSet.Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task<IQueryable<Entidade>> ObterTodos()
        {
            var result = _dbSet.AsQueryable<Entidade>();
            return await Task.FromResult(result);
        }

    }
}
