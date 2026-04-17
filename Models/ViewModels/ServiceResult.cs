namespace BepNha.Web.Models.ViewModels
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        public static ServiceResult Ok(string message = "Thành công")
            => new() { IsSuccess = true, Message = message };

        public static ServiceResult Fail(string message)
            => new() { IsSuccess = false, Message = message };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "Thành công")
            => new() { IsSuccess = true, Message = message, Data = data };

        public new static ServiceResult<T> Fail(string message)
            => new() { IsSuccess = false, Message = message };
    }
}
