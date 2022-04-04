using System;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.Interfaces.Servico
{
    public interface IUnitOfWorkServico : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync();
    }
}
