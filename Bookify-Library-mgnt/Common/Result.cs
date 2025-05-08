namespace Bookify_Library_mgnt.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new();
        public T Data { get; set; }

        public static Result<T> Ok(T data)
        {
            return new Result<T> { IsSuccess = true, Data = data };
        }

        public static Result<T> Fail(List<string> errors)
        {
            return new Result<T> { IsSuccess = false, Errors = errors };
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T> { IsSuccess = false, Errors = new List<string> { error } };
        }
    }
}
