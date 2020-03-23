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
        public IActionResult GetUserReviews(string id) => 
            ReviewService.GetUserReviews(new System.Guid(id)).ToHttpResponse();

        [HttpPost]
        public IActionResult Create(ReviewDTO reviewDto) =>
            ReviewService.Create(reviewDto).ToHttpResponse();

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id) =>
            ReviewService.Delete(id).ToHttpResponse();
    }
}
