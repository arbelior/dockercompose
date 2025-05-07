namespace drushim.Models.Response
{
    public class BaseResponse<T>
    {
        public bool IsSuccessfull { get; set; }
        public string? error { get; set; }
        public T? Data { get; set; }
 
        public static BaseResponse<T> Success(T data)
        {
            return new BaseResponse<T>
            {
                IsSuccessfull = true,
                Data = data,
                error = null
            };
        }

        public static BaseResponse<T> Fail(string error)
        {
            return new BaseResponse<T>
            {
                IsSuccessfull = false,
                Data = default(T),
                error = error
            };
        }
    }
}
