using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using TesteWebmotors.Dominio.Ajudantes;

namespace TesteWebmotors.Api.Configurations.Filtler
{
    public class ErrorHandlingMiddleware 
    {

        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await Excessao(context,ex);
            }
        }

        public Task Excessao(HttpContext context, Exception exception)
        {
            var code = PegaStatusCode(exception);

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result); 
        }

        public static ErrorResponse ExceptionToErrorResponse(Exception ex)
        {
            return ex is not TesteWebmotorsException webmotorsExceptions ? new ErrorResponse { ErrorCode = "server_error", Message = ex.Message } :
                new ErrorResponse
                {
                    ErrorCode = webmotorsExceptions.ErrorCode,
                    Param = webmotorsExceptions.Param,
                    Message = webmotorsExceptions.Message,
                    InnerException = webmotorsExceptions.InnerException,
                };
        }

        public static HttpStatusCode PegaStatusCode(Exception exception)
        {
            return exception switch
            {
                TesteWebmotorsConflictException => HttpStatusCode.Conflict,
                TesteWebmotorsUnathorizedException => HttpStatusCode.Unauthorized,
                TesteWebmotorsForbiddenException => HttpStatusCode.Forbidden,
                TesteWebmotorsNotFoundException => HttpStatusCode.NotFound,
                TesteWebmotorsMissingParamException => HttpStatusCode.UnprocessableEntity,
                TesteWebmotorsInvalidFormatException => HttpStatusCode.UnprocessableEntity,
                TesteWebmotorsBadRequestException => HttpStatusCode.BadRequest,
                TesteWebmotorsException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }
}
