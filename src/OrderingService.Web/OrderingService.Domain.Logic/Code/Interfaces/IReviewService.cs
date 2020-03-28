using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IReviewService
    {
        Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid userId, int pageSize, int pageNumber, CancellationToken token);
        Task<ReviewDTO> CreateAsync(ReviewDTO reviewDto, CancellationToken token);
        Task<ReviewDTO> DeleteAsync(int reviewDto, CancellationToken token);
    }
}
