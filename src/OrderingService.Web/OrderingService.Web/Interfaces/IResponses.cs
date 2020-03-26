using System.Collections.Generic;

namespace OrderingService.Web.Interfaces
{
    public interface IResponse
    {
        public bool DidError { get; }
        public string ErrorMessage { get; }
    }

    public interface IResponse<out T> : IResponse
    {
        T Model { get; }
    }

    public interface IPagedResponse<out T> : IResponse
    {
        IEnumerable<T> Model { get; }
        int PagesCount { get; }
    }
}
