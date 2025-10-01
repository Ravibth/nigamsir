using Newtonsoft.Json;
using RMT.Projects.Domain;

namespace RMT.Projects.API.Services
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
            // var nullObj = new UserDecorator()
            // {
            //     id = 0,
            //     email = "E124__Aayush.Garg@expdiginetdev.onmicrosoft.com",
            //     name = "",
            //     emp_code = "",
            //     designation = "",
            //     service_line = "",
            //     roles = new string[] { },
            //     role = "",
            //     supercoach_name = "",
            //     co_supercoach_name = "",
            //     employee_id = "",
            //     uemail_id = "Aayush.Garg@expdiginetdev.onmicrosoft.com",

            // };
            // return nullObj;

            return user;
        }
    }
}
