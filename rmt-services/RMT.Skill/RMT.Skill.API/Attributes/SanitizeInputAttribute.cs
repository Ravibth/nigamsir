using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Text.Encodings.Web;

namespace RMT.Skill.API.Attributes
{
    /// <summary>
    /// Sanitizes (HTML encodes) all strings on the model.
    /// </summary>
    /// <remarks>Use sparingly. Ideally don't use this and instead encode when outputting the values.
    /// This is used because we don't have control of other applications that may consume the data.</remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SanitizeInputAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ActionArguments != null && actionContext.ActionArguments.Count > 0)
            {
                var requestParams = actionContext.ActionArguments;

                foreach (var requestParam in requestParams)
                {
                    if (requestParam.Value != null && requestParam.GetType().GetGenericTypeDefinition().Namespace != typeof(List<>).Namespace)
                    {
                        var properties = requestParam.Value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Where(x => x.CanRead && x.CanWrite && x.PropertyType == typeof(string) && x.GetGetMethod(true).IsPublic && x.GetSetMethod(true).IsPublic);
                        if (properties != null && properties.Any())
                        {
                            foreach (var propertyInfo in properties)
                            {
                                var val = Convert.ToString(propertyInfo.GetValue(requestParam.Value));
                                if (!string.IsNullOrEmpty(val))
                                {
                                    propertyInfo.SetValue(requestParam.Value, HtmlEncoder.Default.Encode(val as string));
                                }
                            }
                        }
                        else
                        {
                            if (requestParam.Value.GetType() == typeof(string))
                            {
                                requestParams[requestParam.Key] = HtmlEncoder.Default.Encode(requestParam.Value as string);
                            }
                        }
                    }
                }
            }
        }
    }
}
