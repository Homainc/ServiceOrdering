using System.Collections.Generic;

namespace OrderingService.Common.Interfaces
{
    public interface IPagedResult<out T>
    {
        IEnumerable<T> Value { get; }
        int PagesCount { get; }
        int PageSize { get; }
        int PageNumber { get; }
        int Total { get; }
    }
}