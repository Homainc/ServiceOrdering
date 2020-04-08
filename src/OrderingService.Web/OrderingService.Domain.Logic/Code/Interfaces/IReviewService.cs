using System;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IReviewService
    {
        Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid userId, int pageSize, int pageNumber);
        Task<ReviewDTO> CreateAsync(ReviewDTO reviewDto);
        Task<ReviewDTO> DeleteAsync(int reviewDto);

        Task<bool> AnyReviewByIdAsync(int id);
    }
}
