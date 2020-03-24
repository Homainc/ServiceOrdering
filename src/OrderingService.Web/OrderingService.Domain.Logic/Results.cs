using System.Collections.Generic;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic
{
    public class Result : IResult
    {
        public bool DidError => ErrorMessage != null;
        public string ErrorMessage { get; }

        public Result() {}
        public Result(string errorMessage) => ErrorMessage = errorMessage;
    }

    public class Result<T> : IResult<T>
    {
        public bool DidError => ErrorMessage != null;
        public string ErrorMessage { get; }
        public T Value { get; }

        public Result(T value) => Value = value;
        public Result(string errorMessage) => ErrorMessage = errorMessage;
    }

    public class PagedResult<T> : IPagedResult<T>
    {
        public bool DidError => ErrorMessage != null;
        public string ErrorMessage { get; }
        public IEnumerable<T> Value { get; }

        public PagedResult(IEnumerable<T> value) => Value = value;
        public PagedResult(string errorMessage) => ErrorMessage = errorMessage;
    }
}
