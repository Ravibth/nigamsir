using Newtonsoft.Json;
using RMT.Configuration.Domain;

namespace RMT.Configuration.API.Service
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
            return httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
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
            var user = JsonConvert.DeserializeObject<UserDecorator>(httpContextAccessor.HttpContext.Request.Headers["userinfo"].ToString());
            return user;
        }
    }
}
