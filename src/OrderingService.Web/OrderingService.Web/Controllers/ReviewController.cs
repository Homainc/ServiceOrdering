using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code;

namespace OrderingService.Web.Controllers
{
    public class ReviewController : AbstractApiController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService) => _reviewService = reviewService;

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserReviewsAsync([FromRoute] string id, [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1, CancellationToken token = default) =>
            Ok(await _reviewService.GetPagedReviewsAsync(new Guid(id), pageSize, pageNumber, token));

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] [CustomizeValidator(RuleSet = "Create")]
            ReviewDTO reviewDto,
            CancellationToken token = default)
        {
            reviewDto.ClientId = new Guid(User.Identity.Name);
            return Ok(await _reviewService.CreateAsync(reviewDto, token));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken token) =>
            Ok(await _reviewService.DeleteAsync(id, token));
    }
}
