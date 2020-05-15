using System;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IReviewService
    {
        Task<IPagedResult<ReviewDto>> GetPagedReviewsAsync(Guid employeeId, int pageSize, int pageNumber);
        Task<ReviewDto> CreateAsync(ReviewCreateDto reviewDto);
        Task<ReviewDto> DeleteAsync(int reviewDto);
    }
}
