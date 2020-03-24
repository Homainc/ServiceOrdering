using System.Collections.Generic;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IResult
    {
        bool DidError { get; }
        string ErrorMessage { get; }
    }
    public interface IResult<out T> : IResult
    {
        T Value { get; }
    }

    public interface IPagedResult<out T> : IResult
    {
        IEnumerable<T> Value { get; }
    }
}
