using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions;

public class ExceptionManager
{
    // Return ApiException for Not Found (404)
    public static ApiException ReturnNotFound(string message, string errorDetails = null)
    {
        return new ApiException((int)HttpStatusCode.NotFound, message, errorDetails);
    }

    // Return ApiException for Bad Request (400)
    public static ApiException ReturnInvalidModel(string message, string errorDetails = null)
    {
        return new ApiException((int)HttpStatusCode.BadRequest, message, errorDetails);
    }

    // Return ApiException for Unauthorized Access (401)
    public static ApiException ReturnUnauthorized(string message, string errorDetails = null)
    {
        return new ApiException((int)HttpStatusCode.Unauthorized, message, errorDetails);
    }

    // Return ApiException for Forbidden (403)
    public static ApiException ReturnForbidden(string message, string errorDetails = null)
    {
        return new ApiException((int)HttpStatusCode.Forbidden, message, errorDetails);
    }

    // Return ApiException for Internal Server Error (500)
    public static ApiException ReturnInternalServerError(string message, string errorDetails = null)
    {
        return new ApiException((int)HttpStatusCode.InternalServerError, message, errorDetails);
    }

    // Return ApiException for Conflict (409)
    public static ApiException ReturnConflict(string message, string errorDetails = null)
    {
        return new ApiException((int)HttpStatusCode.Conflict, message, errorDetails);
    }

    // Return ApiException for Unprocessable Entity (422)
    public static ApiException ReturnUnprocessableEntity(string message, string errorDetails = null)
    {
        return new ApiException((int)HttpStatusCode.UnprocessableEntity, message, errorDetails);
    }

    // Return ApiException for Too Many Requests (429)
    public static ApiException ReturnTooManyRequests(string message, string errorDetails = null)
    {
        return new ApiException((int)HttpStatusCode.TooManyRequests, message, errorDetails);
    }
}
