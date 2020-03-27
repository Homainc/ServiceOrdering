using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private IReviewService ReviewService { get; }
        public ReviewController(IReviewService reviewService) => ReviewService = reviewService;

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserReviewsAsync([FromQuery] string id, [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1, CancellationToken token = default) =>
            Ok(await ReviewService.GetPagedReviewsAsync(new Guid(id), pageSize, pageNumber, token));

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] [CustomizeValidator(RuleSet = "Create")]
            ReviewDTO reviewDto,
            CancellationToken token = default) => Ok(await ReviewService.CreateAsync(reviewDto, token));

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id, CancellationToken token) =>
            Ok(await ReviewService.DeleteAsync(id, token));
    }
}
