using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain.Logic.Code.Exceptions;
using Serilog;

namespace OrderingService.Web.Code.Filters
{
    public class LogicExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case LogicNotFoundException notFoundEx:
                    Log.Debug("Logic error: {0}", notFoundEx.Message);
                    context.Result = new NotFoundObjectResult(new
                    {
                        ErrorMessage = notFoundEx.Message
                    });
                    break;
                case LogicException ex:
                    Log.Debug("Logic error: {0}", ex.Message);
                    context.Result = new BadRequestObjectResult(new
                    {
                        ErrorMessage = ex.Message
                    });
                    break;
            }
        }
    }
}
