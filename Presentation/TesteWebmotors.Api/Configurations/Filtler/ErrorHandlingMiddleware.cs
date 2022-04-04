using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using TesteWebmotors.Dominio.Ajudantes;

namespace TesteWebmotors.Api.Configurations.Filtler
{
    public class ErrorHandlingMiddleware : ExceptionFilterAttribute, IExceptionFilter
    {

        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var errorResponse = ExceptionToErrorResponse(exception);

            var result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)GetStatusCode(exception)
            };
            _logger.LogError("Unhandled exception occurred while executing request: {ex}", context.Exception);
            context.Result = result;
        }

        public static ErrorResponse ExceptionToErrorResponse(Exception ex)
        {
            return ex is not TesteWebmotorsException eProcessosExceptions ? new ErrorResponse { ErrorCode = "server_error", Message = ex.Message } :
                new ErrorResponse
                {
                    ErrorCode = eProcessosExceptions.ErrorCode,
                    Param = eProcessosExceptions.Param,
                    Message = eProcessosExceptions.Message,
                    InnerException = eProcessosExceptions.InnerException,
                };
        }

        private static HttpStatusCode GetStatusCode(Exception exception)
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
