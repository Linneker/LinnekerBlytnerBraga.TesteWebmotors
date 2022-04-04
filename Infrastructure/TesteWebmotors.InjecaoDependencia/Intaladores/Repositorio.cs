using Microsoft.Extensions.DependencyInjection;
using TesteWebmotors.Dominio.Interface.Repositorio;
using TesteWebmotors.Repositorio.Repositorio;

namespace TesteWebmotors.InjecaoDependencia.Intaladores
{
    internal static class Repositorio
    {
        public static IServiceCollection InstalarRepositorio(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));
            serviceCollection.AddTransient<IAnuncioWebmotorsRepositorio, AnuncioWebmotorsRepositorio>();

            return serviceCollection;
        }

    }
}
