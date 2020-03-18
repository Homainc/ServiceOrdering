using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IReviewService : IDisposable
    {
        IEnumerable<ReviewDTO> GetUserReviews(string userId);
        IOperationResult Create(ReviewDTO reviewDto);
        IOperationResult Delete(int reviewDto);
    }
}
