using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IReviewService : IDisposable
    {
        IResponse<IEnumerable<ReviewDTO>> GetUserReviews(Guid userId);
        IResponse<ReviewDTO> Create(ReviewDTO reviewDto);
        IResponse<ReviewDTO> Delete(int reviewDto);
    }
}
