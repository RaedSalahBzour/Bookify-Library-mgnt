using Microsoft.AspNetCore.Identity;

namespace Bookify_Library_mgnt.Common
{
    public static class ErrorMessages
    {
        public static string NotFound(string id) => $"Record with ID '{id}' was not found.";
        public static string EmailAlreadyExists(string email) => $"Email '{email}' is already registered.";
        public static string UsernameAlreadyExists(string username) => $"Username '{username}' is already taken.";
        public static string ValidationFailed => "Validation failed. See error details.";
        public static string OperationFailed(string operation) => $"{operation} failed. Please try again.";
    }
}
