using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string? message, IDictionary<string, string[]> errors) : base(message)
    {
        Errors = errors;
    }
    public IDictionary<string, string[]> Errors { get; }
}
