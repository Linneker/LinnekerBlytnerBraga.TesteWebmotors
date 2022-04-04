using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Interfaces;
using TesteWebmotors.Dominio.Interfaces.Servico;

namespace TesteWebmotors.Api.Configurations.Filtler
{
    public class UnitOfWorkFilterAsync : Attribute, IAsyncResultFilter 
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            try
            {
                IUnitOfWorkServico baseApplication = (IUnitOfWorkServico)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWorkServico));
                bool salvo = await baseApplication.CommitAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
