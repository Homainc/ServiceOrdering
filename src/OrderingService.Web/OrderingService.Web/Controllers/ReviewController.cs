using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger<ReviewController> Logger { get; }
        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            ReviewService = reviewService;
            Logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserReviewsAsync([FromQuery] string id, [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1, CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(GetUserReviewsAsync));
            IPagedResponse<ReviewDTO> response;
            var result = await ReviewService.GetPagedReviewsAsync(new Guid(id), pageSize, pageNumber, token);
            if (result.DidError)
            {
                response = new PagedResponse<ReviewDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
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
            Logger.LogDebug("{0} has been invoked", nameof(CreateAsync));
            IResponse<ReviewDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<ReviewDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            var result = await ReviewService.CreateAsync(reviewDto, token);
            if (result.DidError)
            {
                response = new Response<ReviewDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
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
            Logger.LogDebug("{0} has been invoked", nameof(DeleteAsync));
            IResponse<ReviewDTO> response;
            var result = await ReviewService.DeleteAsync(id, token);
            if (result.DidError)
            {
                response = new Response<ReviewDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<ReviewDTO>(result.Value);
            return Ok(response);
        }
    }
}
