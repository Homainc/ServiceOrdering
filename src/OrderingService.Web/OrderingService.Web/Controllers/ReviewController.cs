using System.Threading;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetUserReviewsAsync(string id, CancellationToken token = default) => 
            await ReviewService.GetUserReviewsAsync(new System.Guid(id), token).ToHttpResponseAsync();

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ReviewDTO reviewDto, CancellationToken token = default) =>
            await ReviewService.CreateAsync(reviewDto, token).ToHttpResponseAsync();

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token) =>
            await ReviewService.DeleteAsync(id, token).ToHttpResponseAsync();
    }
}
