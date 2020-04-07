using System.Diagnostics;
using Serilog;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OrderingService.Web.Code.Filters
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next){
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Log.Debug("{0} has been invoked", context.ActionDescriptor.DisplayName); 
            await next();
            stopwatch.Stop();
            Log.Debug("{0} has been executed for {1} ms", context.ActionDescriptor.DisplayName, stopwatch.Elapsed.Milliseconds);
        }        
    }
}