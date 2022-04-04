using System;
using System.Collections.Generic;

namespace TesteWebmotors.Api.Configurations
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {

        }
        public ErrorResponse(string errorCode, string param, string message, Exception? innerException)
        {
            ErrorCode = errorCode;
            Param = param;
            Message = message;
            InnerException = innerException;
        }

        public string ErrorCode { get; set; }
        public string Param { get; set; }
        public string Message { get; set; }
        public Exception? InnerException { get; set; }
    }
}
