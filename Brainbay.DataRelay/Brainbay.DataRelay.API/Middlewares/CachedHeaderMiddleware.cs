using Brainbay.DataRelay.API.Filters;

namespace Brainbay.DataRelay.API.Middlewares;

public class CachedHeaderMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.OnStarting(() =>
        {
            if (context.Items.TryGetValue(CachedResultContextKeys.FromDatabase, out var flagObj) &&
                flagObj is bool fromDatabase)
            {
                context.Response.Headers[CachedHeaderConstants.FromDatabase] = fromDatabase.ToString().ToLower();
            }

            return Task.CompletedTask;
        });

        await next(context);
    }
}