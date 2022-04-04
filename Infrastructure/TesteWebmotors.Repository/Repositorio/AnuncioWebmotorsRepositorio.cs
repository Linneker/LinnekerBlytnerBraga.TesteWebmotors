using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Ajudantes;
using TesteWebmotors.Dominio.Entidade;
using TesteWebmotors.Dominio.Interface.Repositorio;
using TesteWebmotors.Infraestrutura.Config;

namespace TesteWebmotors.Repositorio.Repositorio
{
    public class AnuncioWebmotorsRepositorio : BaseRepositorio<AnuncioWebmotors>, IAnuncioWebmotorsRepositorio
    {
        public AnuncioWebmotorsRepositorio(Context context) : base(context)
        {
        }

        public async Task<AnuncioWebmotors> ObterPorMarca(string marca)
        {
            var resultado = await (from awm in _dbSet
                                   where awm.Marca.Equals(marca)
                                   select awm).AsNoTracking().FirstOrDefaultAsync();
            return resultado;
        }

        public async Task<List<AnuncioWebmotors>> Paginador(int pagina, int totalPorPagina) => await _dbSet.Skip(pagina).Take(totalPorPagina).ToListAsync();

        public async Task<List<AnuncioWebmotors>> Filtrar(FiltroAnuncioWebmotors anuncioWebmotors)
        {
            var result = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(anuncioWebmotors.Marca))
            {
                result = result.Where(t => t.Marca.Equals(anuncioWebmotors.Marca));
            }
            if (!string.IsNullOrWhiteSpace(anuncioWebmotors.Modelo))
            {
                result = result.Where(t => t.Modelo.Equals(anuncioWebmotors.Modelo));
            }
            if (!string.IsNullOrWhiteSpace(anuncioWebmotors.Versao))
            {
                result = result.Where(t => t.Versao.Equals(anuncioWebmotors.Versao));
            }
            if (!string.IsNullOrWhiteSpace(anuncioWebmotors.Observacao))
            {
                result = result.Where(t => t.Observacao.Contains(anuncioWebmotors.Observacao));
            }
            if (anuncioWebmotors.Ano is not null)
            {
                result = result.Where(t => t.Ano == anuncioWebmotors.Ano);
            }
            if (anuncioWebmotors.Quilometragem is not null)
            {
                result = result.Where(t => t.Quilometragem == anuncioWebmotors.Quilometragem);
            }
      
            return await result.ToListAsync();
        }

    }
}
