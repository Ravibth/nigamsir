
using Newtonsoft.Json;
using RMT.Employee.Domain;



namespace RMT.Employee.API.Services
{
    public class UserAccessor: IUserAccessor
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
            var user = JsonConvert.DeserializeObject<UserDecorator>(httpContextAccessor.HttpContext.Request.Headers["userinfo"].ToString());
            return user;
        }
    }
}
