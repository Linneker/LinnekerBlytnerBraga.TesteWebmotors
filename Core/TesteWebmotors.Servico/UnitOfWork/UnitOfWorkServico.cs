using System.Threading.Tasks;
using TesteWebmotors.Dominio.Interfaces;
using TesteWebmotors.Dominio.Interfaces.Servico;

namespace TesteWebmotors.Servico.UnitOfWork
{
    public class UnitOfWorkServico : IUnitOfWorkServico
    {
        private readonly IUnitOfWork _context;

        public UnitOfWorkServico(IUnitOfWork context)
        {
            _context = context;
        }

        public bool Commit() => _context.Commit();

        public async Task<bool> CommitAsync() => await _context.CommitAsync();

        public void Dispose() => _context.Dispose();
    }
}
