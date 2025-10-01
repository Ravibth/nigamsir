//using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using WCGT.Domain;

namespace WCGT.API.Services
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

            var stream = this.GetToken();
            if (stream != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenValue = handler.ReadToken(stream) as JwtSecurityToken;
                //var tokenValue = handler.ReadJwtToken(stream);

                this.Headers = new Dictionary<string, string>();
                foreach (var item in tokenValue.Payload)
                {
                    string value = Convert.ToString(item.Value);
                    //if (item.Key == "Role")
                    //{
                    //    if (item.Value.GetType() == typeof(JArray))
                    //    {
                    //        List<string> roles = new List<string>();
                    //        var roleArray = (JArray)item.Value;
                    //        foreach (var roleValue in roleArray)
                    //        {
                    //            roles.Add(roleValue.ToString());
                    //        }

                    //        value = string.Join("/", roles);
                    //    }
                    //}

                    this.Headers.Add(item.Key, value);
                }
            }
        }

        private Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets jwt token.
        /// </summary>
        /// <returns>jwt token string.</returns>
        public string GetToken()
        {
            string token = null;
            string authHeader = this.httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                token = authHeader.Split(" ")[1];
            }

            return token;
        }

        /// <summary>
        /// Gets email of the loggedin user.
        /// </summary>
        /// <returns>Email Id.</returns>
        public string GetEmail()
        {
            if (this.Headers != null)
            {
                if (this.Headers.TryGetValue(Constants.idtoken_header_email, out var idEmail))
                {
                    return idEmail;
                }
                else if (this.Headers.TryGetValue(Constants.accesstoken_header_email, out var accessEmail))
                {
                    return accessEmail;
                }
            }
            return string.Empty;
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


        /// <summary>
        /// Gets selected banner of the loggedin user.
        /// </summary>
        /// <returns>Banner.</returns>
        //public string GetSelectedBanner()
        //{
        //    string selectedBanner;
        //    this.Headers.TryGetValue(Constants.SelectedBanner, out selectedBanner);

        //    return selectedBanner;
        //}

        /// <summary>
        /// Gets primary banner of the loggedin user.
        /// </summary>
        /// <returns>Banner.</returns>
        //public string GetPrimaryBanner()
        //{
        //    return this.Headers[Constants.PrimaryBanner];
        //}

        /// <summary>
        /// Gets secondary banner of the loggedin user.
        /// </summary>
        /// <returns>Banner.</returns>
        //public string GetSecondaryBanner()
        //{
        //    string secondaryBanner;
        //    this.Headers.TryGetValue(Constants.SecondaryBanner, out secondaryBanner);
        //    return secondaryBanner;
        //}

        /// <summary>
        /// Gets Permissions of the loggedin user.
        /// </summary>
        /// <returns>List of permissions.</returns>
        //public List<Permission> GetPermissions()
        //{
        //    return JsonConvert.DeserializeObject<List<Permission>>(this.Headers[Constants.Permissions]);
        //}

        /// <summary>
        /// Gets roles of the loggedin user.
        /// </summary>
        /// <returns>List of roles.</returns>
        //public List<string> GetRoles()
        //{
        //    var roles = new List<string>();
        //    string rolesStr;
        //    this.Headers.TryGetValue(Constants.Role, out rolesStr);
        //    if (!string.IsNullOrWhiteSpace(rolesStr))
        //    {
        //        roles = rolesStr.Split(Constants.SeparatorPipe).ToList();
        //    }

        //    return roles;
        //}
    }
}
