using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private IReviewService ReviewService { get; }
        public ReviewController(IReviewService reviewService)
        {
            ReviewService = reviewService;
        }

        [HttpGet("{id}")]
        public IEnumerable<ReviewDTO> GetUserReviews(string id) => 
            ReviewService.GetUserReviews(id);

        [HttpPost]
        public IOperationResult Create(ReviewDTO reviewDto) =>
            ReviewService.Create(reviewDto);

        [HttpDelete("{id:int}")]
        public IOperationResult Delete(int id) =>
            ReviewService.Delete(id);
    }
}
