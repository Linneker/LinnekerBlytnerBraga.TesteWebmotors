using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Interface.Servico;
using TesteWebmotors.Servico.Servicos;

namespace TesteWebmotors.InjecaoDependencia.Intaladores
{
    internal static class Servico
    {
        public static IServiceCollection InstalarServico(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IBaseServico<>), typeof(BaseServico<>));
            serviceCollection.AddTransient<IAnuncioWebmotorsServico, AnuncioWebmotorsServico>();

            return serviceCollection;
        }
    }
}
