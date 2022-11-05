using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleAPI.Filters
{
    public class SampleActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.WriteAsync("Sample Action in \r\n");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.WriteAsync("Sample Action out \r\n");
        }
    }

    public class SampleAsyncActionFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await context.HttpContext.Response.WriteAsync("Async Sample Action in \r\n");

            var resultContext = await next();

            await context.HttpContext.Response.WriteAsync("Async Sample Action out \r\n");
        }
    }
}