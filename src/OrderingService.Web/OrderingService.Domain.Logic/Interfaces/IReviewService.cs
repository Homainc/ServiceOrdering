using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IReviewService : IDisposable
    {
        Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid userId, int pageSize, int pageNumber, CancellationToken token);
        Task<IResult<ReviewDTO>> CreateAsync(ReviewDTO reviewDto, CancellationToken token);
        Task<IResult<ReviewDTO>> DeleteAsync(int reviewDto, CancellationToken token);
    }
}
