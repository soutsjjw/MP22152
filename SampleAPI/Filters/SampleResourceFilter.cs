using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleAPI.Filters
{
    public class SampleResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.WriteAsync("Sample Resource in \r\n");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            context.HttpContext.Response.WriteAsync("Sample Resource out \r\n");
        }
    }

    public class SampleAsyncResourceFilter : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            await context.HttpContext.Response.WriteAsync("Async Sample Resource in \r\n");

            var resultContext = await next();

            await context.HttpContext.Response.WriteAsync("Async Sample Resource out \r\n");
        }
    }
}