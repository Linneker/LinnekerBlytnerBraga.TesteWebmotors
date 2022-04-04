using Microsoft.Extensions.DependencyInjection;
using TesteWebmotors.Dominio.Interfaces;
using TesteWebmotors.Dominio.Interfaces.Servico;
using TesteWebmotors.Infraestrutura.Config;
using TesteWebmotors.InjecaoDependencia.Intaladores;
using TesteWebmotors.Repositorio.UnitOfWork;
using TesteWebmotors.Servico.Requisicao;
using TesteWebmotors.Servico.UnitOfWork;

namespace TesteWebmotors.InjecaoDependencia
{
    public static class Dependencia
    {
        public static IServiceCollection RegistrarDependencia(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<Context>();
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddTransient<IUnitOfWorkServico, UnitOfWorkServico>();

            serviceCollection.AddScoped<MakeRequisicao>();
            serviceCollection.AddScoped<ModelRequisicao>();
            serviceCollection.AddScoped<VersionRequisicao>();

            serviceCollection.InstalarRepositorio();
            serviceCollection.InstalarServico();

            return serviceCollection;
            
        }
    }
}
