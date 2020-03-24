using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;
using OrderingService.Web.Interfaces;

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserReviewsAsync([FromQuery] string id, CancellationToken token = default)
        {
            IPagedResponse<ReviewDTO> response;
            var result = await ReviewService.GetUserReviewsAsync(new System.Guid(id), token);
            if (result.DidError)
            {
                response = new PagedResponse<ReviewDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new PagedResponse<ReviewDTO>(result.Value);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Create")]
            ReviewDTO reviewDto,
            CancellationToken token = default)
        {
            IResponse<ReviewDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<ReviewDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            var result = await ReviewService.CreateAsync(reviewDto, token);
            if (result.DidError)
            {
                response = new Response<ReviewDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<ReviewDTO>(result.Value);
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id, CancellationToken token)
        {
            IResponse<ReviewDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<ReviewDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            var result = await ReviewService.DeleteAsync(id, token);
            if (result.DidError)
            {
                response = new Response<ReviewDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<ReviewDTO>(result.Value);
            return Ok(response);
        }
    }
}
