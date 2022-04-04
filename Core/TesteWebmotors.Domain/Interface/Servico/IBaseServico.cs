using System.Linq;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Entidade;

namespace TesteWebmotors.Dominio.Interface.Servico
{
    public interface IBaseServico<Entity> where Entity : EntidadeBase
    {
        Task Atualizar(Entity entity);
        void Deletar(Entity entity);
        Task<Entity> ObterPorId(int id);
        Task<IQueryable<Entity>> ObterTodos();
    }
}
