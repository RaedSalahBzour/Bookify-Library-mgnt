using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions;

public class ApiException : Exception
{
    public int StatusCode { get; private set; }
    public string ErrorDetails { get; private set; }

    public ApiException(int statusCode, string message, string errorDetails = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorDetails = errorDetails;
    }

    public ApiException(int statusCode, string message, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

}
