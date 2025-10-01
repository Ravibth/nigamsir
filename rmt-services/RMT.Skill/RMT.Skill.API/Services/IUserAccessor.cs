using RMT.Skill.Domain;

namespace RMT.Skill.API.Services
{
    public interface IUserAccessor
    {
        /// <summary>
        /// Gets jwt token.
        /// </summary>
        /// <returns>Jwt token string.</returns>
        string GetToken();

        /// <summary>
        /// Gets email of the loggedin user.
        /// </summary>
        /// <returns>email Id.</returns>
        string GetEmail();
        UserDecorator GetUser();

    }
}
