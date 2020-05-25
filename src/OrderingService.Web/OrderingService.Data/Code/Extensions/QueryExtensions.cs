using System.Linq;

namespace OrderingService.Data.Code.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, int pageSize, int pageNumber) =>
            query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
    }
}
