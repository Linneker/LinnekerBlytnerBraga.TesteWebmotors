using Microsoft.AspNetCore.Mvc.Filters;
using TesteWebmotors.Dominio.Interfaces.Servico;

namespace TesteWebmotors.Web.Controllers
{
    public class UnitOfWorkFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            try
            {
                IUnitOfWorkServico baseApplication = (IUnitOfWorkServico)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWorkServico));
                bool salvo = baseApplication.Commit();
               
            }
            catch
            {
                throw;
            }
        }
    }
}
