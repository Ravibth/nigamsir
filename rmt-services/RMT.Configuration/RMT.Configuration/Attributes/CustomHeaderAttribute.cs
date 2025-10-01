using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace RMT.Configuration.API.Attributes
{
    public class CustomHeaderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var HeaderKeyName = "HeaderKeyName";
            context.HttpContext.Request.Headers.TryGetValue("HeaderKeyName", out StringValues headerValue);
            if (context.HttpContext.Items.ContainsKey(HeaderKeyName))
            {
                context.HttpContext.Items[HeaderKeyName] = headerValue;
            }
            else
            {
                context.HttpContext.Items.Add(HeaderKeyName, $"{headerValue}-received");
            }
        }
    }
}
