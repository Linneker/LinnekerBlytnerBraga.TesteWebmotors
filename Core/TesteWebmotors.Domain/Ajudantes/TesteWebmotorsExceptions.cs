using System;
using System.Collections.Generic;

namespace TesteWebmotors.Dominio.Ajudantes
{
    [Serializable]
    public class TesteWebmotorsMissingParamException : TesteWebmotorsException
    {
        public TesteWebmotorsMissingParamException(string errorCode, string message, string param) : base(errorCode, message, param) { }
        
    }

    [Serializable]
    public class TesteWebmotorsUnathorizedException : TesteWebmotorsException
    {
        public TesteWebmotorsUnathorizedException(string errorCode, string message, string param = null) : base(errorCode, message, param) { }
    }

    [Serializable]
    public class TesteWebmotorsForbiddenException : TesteWebmotorsException
    {
        public TesteWebmotorsForbiddenException(string errorCode, string message, string param = null) : base(errorCode, message, param) { }
    }

    [Serializable]
    public class TesteWebmotorsInvalidFormatException : TesteWebmotorsException
    {
        public TesteWebmotorsInvalidFormatException(string errorCode, string message, string param = null) : base(errorCode, message, param) { }
    }

    [Serializable]
    public class TesteWebmotorsNotFoundException : TesteWebmotorsException
    {
        public TesteWebmotorsNotFoundException(string errorCode, string message, string param = null) : base(errorCode, message, param) { }
    }

    [Serializable]
    public class TesteWebmotorsConflictException : TesteWebmotorsException
    {
        public TesteWebmotorsConflictException(string errorCode, string message, string param = null) : base(errorCode, message, param) { }
    }

    [Serializable]
    public class TesteWebmotorsBadRequestException : TesteWebmotorsException
    {
        public TesteWebmotorsBadRequestException(string errorCode, string message, string param = null) : base(errorCode, message, param) { }
    }

    [Serializable]
    public class TesteWebmotorsException : Exception
    {
        public string ErrorCode { get; set; }
        public string Param { get; set; }

        public TesteWebmotorsException() { }

        public TesteWebmotorsException(string message) : base(message) { }

        public TesteWebmotorsException(string errorCode, string message, string param) : this(message)
        {
            ErrorCode = errorCode;
            Param = param;
        }
    }

    [Serializable]
    public class TesteWebmotorsAggreateException : AggregateException
    {
        public string ErrorCode { get; set; }
        public string Param { get; set; }
        public TesteWebmotorsAggreateException(string message, IList<Exception> exceptions) : base(message, exceptions)
        {
        }

        public TesteWebmotorsAggreateException(string errorCode, string message, string param, IList<Exception> exceptions)
           : base(message, exceptions)
        {
            ErrorCode = errorCode;
            Param = param;
        }
    }
}
