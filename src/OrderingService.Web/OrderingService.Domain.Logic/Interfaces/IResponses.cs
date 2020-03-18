using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public enum ResponseResult
    {
        Success,
        NotFound,
        ValidationError,
        InternalError
    }
    public interface IResponse<T>
    {
        string Message { get; set; }
        bool DidError { get; }
        string ErrorMessage { get; set; }
        ResponseResult ResponseResult { get; set; }
        T Model { get; set; }
    }
}
