using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IReviewService
    {
        IOperationResult Create(ReviewDTO reviewDto);
        IOperationResult Delete(ReviewDTO reviewDto);
    }
}
