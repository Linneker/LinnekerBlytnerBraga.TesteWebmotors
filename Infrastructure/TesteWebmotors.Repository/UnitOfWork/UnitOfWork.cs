using System.Threading.Tasks;
using TesteWebmotors.Dominio.Interfaces;
using TesteWebmotors.Infraestrutura.Config;

namespace TesteWebmotors.Repositorio.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public bool Commit() => _context.SaveChanges() > 0;

        public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;

        public void Dispose() => _context.Dispose();
    }
}
