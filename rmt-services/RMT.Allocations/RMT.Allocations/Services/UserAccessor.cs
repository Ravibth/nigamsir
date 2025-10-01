using Newtonsoft.Json;
using RMT.Allocation.Domain;
using RMT.Allocation.Infrastructure;
using System.Security.Authentication;

namespace RMT.Allocations.API.Services
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
            this.httpContextAccessor = httpContextAccessor;
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
            string userInfo = Convert.ToString(httpContextAccessor.HttpContext.Request.Headers["userinfo"]);
            if (string.IsNullOrEmpty(userInfo))
            {
                return null;
            }
            var user = JsonConvert.DeserializeObject<UserDecorator>(Convert.ToString(httpContextAccessor.HttpContext.Request.Headers["userinfo"]));
            return user;
        }
    }
}
