using System.Linq;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Entidade;

namespace TesteWebmotors.Dominio.Interface.Repositorio
{
    public interface IBaseRepositorio<Entity> where Entity : EntidadeBase 
    {
        void Adicionar(Entity entity);
        Task AdicionarAssincrono(Entity entity);
        void Atualizar(Entity entity);
        void Deletar(Entity entity);
        Task<Entity> ObterPorId(int id);
        Task<IQueryable<Entity>> ObterTodos(); 
    }
}
