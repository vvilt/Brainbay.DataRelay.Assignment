using Brainbay.DataRelay.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Brainbay.DataRelay.API.Filters;

public class UnwrapCachedResultAttribute : ActionFilterAttribute
{

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value != null)
        {
            var type = objectResult.Value.GetType();

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(CachedResult<>))
            {
                var (value, fromDatabase) = CachedResultUnwrapper.Unwrap(objectResult.Value);

                context.HttpContext.Items[CachedResultContextKeys.FromDatabase] = fromDatabase;

                objectResult.Value = value;
            }
        }
        base.OnResultExecuting(context);
    }
}