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
                IUnitOfWork baseApplication = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
                bool salvo = await baseApplication.CommitAsync();
                if (!salvo)
                    throw (new Exception("Problema ao salvar elemento"));
                else
                    await next();
            }
            catch
            {
                throw;
            }
        }
    }
}
