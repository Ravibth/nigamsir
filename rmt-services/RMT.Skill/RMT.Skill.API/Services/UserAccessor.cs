//using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.JsonWebTokens;
using RMT.Skill.API.Constant;
using Newtonsoft.Json;
using RMT.Skill.Domain;

namespace RMT.Skill.API.Services
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
            if (this.Headers.ContainsKey(Constants.idtoken_header_email))
            {
                return this.Headers[Constants.idtoken_header_email];
            }
            else if (this.Headers.ContainsKey(Constants.accesstoken_header_email))
            {
                return this.Headers[Constants.accesstoken_header_email];

            }
            return "";
        }

        public UserDecorator GetUser()
        {
            var user = JsonConvert.DeserializeObject<UserDecorator>(httpContextAccessor.HttpContext.Request.Headers["userinfo"].ToString());
            return user;
        }

    }
}
