using System.Threading.Tasks;
using TesteWebmotors.Dominio.Ajudantes;
using TesteWebmotors.Dominio.Entidade;

namespace TesteWebmotors.Dominio.Interface.Servico
{
    public interface IAnuncioWebmotorsServico: IBaseServico<AnuncioWebmotors>
    {
        Task<AnuncioWebmotors> ObterPorMarca(string marca);
        Task<List<AnuncioWebmotors>> Paginador(int pagina,int totalPorPagina);
        Task<List<AnuncioWebmotors>> Filtrar(FiltroAnuncioWebmotors anuncioWebmotors);

    }
}
