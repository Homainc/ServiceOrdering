using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain.Logic.Code.Exceptions;
using Serilog;

namespace OrderingService.Web.Code.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NotFoundLogicException notFoundEx:
                    Log.Debug("Logic not found error: {0}", notFoundEx.Message);
                    context.Result = new NotFoundObjectResult(new ValidationProblemDetails
                    {
                        Title = "Not found",
                        Detail = notFoundEx.Message,
                        Status = StatusCodes.Status404NotFound,
                        Errors = { { char.ToLowerInvariant(notFoundEx.ParamName[0]) + notFoundEx.ParamName.Substring(1), new[] { notFoundEx.Message } } }
                    });
                    break;

                case FieldLogicException fieldEx:
                    Log.Debug("Logic field error: {0} - {1}", fieldEx.ParamName, fieldEx.Message);
                    context.Result = new BadRequestObjectResult(new ValidationProblemDetails
                    {
                        Title = "Incorrect parameters",
                        Detail = fieldEx.Message,
                        Status = StatusCodes.Status400BadRequest,
                        Errors = { { char.ToLower(fieldEx.ParamName[0]) + fieldEx.ParamName.Substring(1), new[] { fieldEx.Message } } }
                    });
                    break;

                case LogicException ex:
                    Log.Debug("Logic error: {0}", ex.Message);
                    context.Result = new BadRequestObjectResult(new ValidationProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = ex.Message,
                        Detail = ex.Message
                    });
                    break;
            }
        }
    }
}
