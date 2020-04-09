using System.Collections.Generic;
using AutoMapper;
using OrderingService.Common;
using OrderingService.Common.Interfaces;

namespace OrderingService.Domain.Logic.Code
{
    public static class PagedResultExtension
    {
        public static IPagedResult<TDestination>
            ToPagedDto<TDestination, TSource>(this IPagedResult<TSource> pagedResult, IMapper mapper) =>
            new PagedResult<TDestination>(mapper.Map<IEnumerable<TDestination>>(pagedResult.Value), pagedResult.Total,
                pagedResult.PageSize, pagedResult.PageNumber);
    }
}