using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderingService.Domain.Logic.Helpers
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, int pageSize, int pageNumber) =>
            query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
    }
}
