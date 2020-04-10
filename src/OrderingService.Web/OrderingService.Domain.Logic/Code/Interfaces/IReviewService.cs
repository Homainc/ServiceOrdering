using System;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IReviewService
    {
        Task<IPagedResult<ReviewDTO>> GetPagedReviewsAsync(Guid employeeId, int pageSize, int pageNumber);
        Task<ReviewDTO> CreateAsync(ReviewDTO reviewDto);
        Task<ReviewDTO> DeleteAsync(int reviewDto);
    }
}
