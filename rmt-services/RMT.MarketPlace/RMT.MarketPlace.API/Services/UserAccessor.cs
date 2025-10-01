using Newtonsoft.Json;
using RMT.MarketPlace.Domain;
using RMT.MarketPlace.Infrastructure;
using System.Security.Authentication;

namespace RMT.MarketPlace.API.Services
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccessor"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Http Context Accessor.</param>
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            //if (!string.IsNullOrEmpty(httpContextAccessor.HttpContext.Request.Headers["Authorization"]))
            //{
               this.httpContextAccessor = httpContextAccessor;
            //}
            //else
            //{
            //    throw new UnauthorizedAccessException("UnAuthorizated request.");
            //}
        }

        public string GetToken()
        {
            return Convert.ToString(httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
        }

        /// <summary>
        /// Get Logged In User Info
        /// </summary>
        /// <returns></returns>
        public UserDecorator GetUser()
        {
            var user = JsonConvert.DeserializeObject<UserDecorator>(Convert.ToString(httpContextAccessor.HttpContext.Request.Headers["userinfo"]));
            return user;

            //var user = new UserDecorator()
            //{
            //    id = 0,
            //    email = "SystemObject",
            //    emp_code = "SystemObject",
            //    name = "SystemObject",
            //    role = "SystemObject",
            //    roles =[],
            //    designation = "SystemObject",
            //};
            //return user;

        }
    }
}
