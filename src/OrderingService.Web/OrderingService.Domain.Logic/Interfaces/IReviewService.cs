using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IReviewService : IDisposable
    {
        Task<IResponse<IEnumerable<ReviewDTO>>> GetUserReviewsAsync(Guid userId, CancellationToken token);
        Task<IResponse<ReviewDTO>> CreateAsync(ReviewDTO reviewDto, CancellationToken token);
        Task<IResponse<ReviewDTO>> DeleteAsync(int reviewDto, CancellationToken token);
    }
}
