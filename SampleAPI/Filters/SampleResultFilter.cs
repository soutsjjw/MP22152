using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleAPI.Filters
{
    public class SampleResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.WriteAsync("Sample Result in \r\n");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            context.HttpContext.Response.WriteAsync("Sample Result out \r\n");
        }
    }

    public class SampleAsyncResultFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await context.HttpContext.Response.WriteAsync("Async Sample Result in \r\n");

            var resultContext = await next();

            await context.HttpContext.Response.WriteAsync("Async Sample Result out \r\n");
        }
    }
}