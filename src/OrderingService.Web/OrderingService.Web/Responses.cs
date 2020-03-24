using System.Collections.Generic;
using OrderingService.Web.Interfaces;

namespace OrderingService.Web
{
    public class Response : IResponse
    {
        public bool DidError => ErrorMessage != null;
        public string ErrorMessage { get; }
        public Response() { }
        public Response(string errorMessage) => ErrorMessage = errorMessage;
    }

    public class Response<T> : IResponse<T>
    {
        public T Model { get; }
        public bool DidError => ErrorMessage != null;
        public string ErrorMessage { get; }
        public Response(string errorMessage) => ErrorMessage = errorMessage;
        public Response(T model) => Model = model;
    }

    public class PagedResponse<T> : IPagedResponse<T>
    {
        public IEnumerable<T> Model { get; }
        public bool DidError => ErrorMessage != null;
        public string ErrorMessage { get; }
        public PagedResponse(string errorMessage) => ErrorMessage = errorMessage;
        public PagedResponse(IEnumerable<T> model) => Model = model;
    }
}
