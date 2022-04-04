using System;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync();
    }
}
