using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class Response<T> : IResponse<T>
    {
        public string Message { get; set; }
        public bool DidError => ResponseResult != ResponseResult.Success;
        public string ErrorMessage { get; set; }
        public ResponseResult ResponseResult { get; set; }
        public T Model { get; set; }

        public static Response<T> Success(T obj, string msg = null) => new Response<T>
        {
            Message = msg,
            ResponseResult = ResponseResult.Success,
            Model = obj
        };

        public static Response<T> NotFound(string errMsg) => new Response<T>
        {
            ErrorMessage = errMsg,
            ResponseResult = ResponseResult.NotFound
        };

        public static Response<T> ValidationError(string errMsg) => new Response<T>
        {
            ErrorMessage = errMsg,
            ResponseResult = ResponseResult.ValidationError
        };
    }
}
