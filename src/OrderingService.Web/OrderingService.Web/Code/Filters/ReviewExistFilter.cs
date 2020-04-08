using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Code.Filters
{
    public class ReviewExistFilter : ActionFilterAttribute
    {
        private readonly IReviewService _reviewService;

        public ReviewExistFilter(IReviewService reviewService) => _reviewService = reviewService;
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["id"] is int id)
            {
                if (!await _reviewService.AnyReviewByIdAsync(id))
                {
                    context.Result = new NotFoundObjectResult(new
                    {
                        ErrorMessage = $"Review with id {id} not found!"
                    });
                }
                else
                    await next();
            }
            else
                await next();
        }
    }
}
