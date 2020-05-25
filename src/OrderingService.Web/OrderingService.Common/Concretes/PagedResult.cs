using System.Collections.Generic;
using OrderingService.Common.Interfaces;

namespace OrderingService.Common.Concretes
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public IEnumerable<T> Value { get; }
        public int PagesCount => Total / PageSize + (Total % PageSize == 0 ? 0 : 1);
        public int PageSize { get; }
        public int PageNumber { get; }
        public int Total { get; }

        public PagedResult(IEnumerable<T> value, int total, int pageSize = 0, int pageNumber = 0)
        {
            Value = value;
            Total = total;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}