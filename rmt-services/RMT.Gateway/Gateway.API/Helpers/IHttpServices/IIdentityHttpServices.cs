using Gateway.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.IHttpServices
{

    public interface IIdentityHttpServices
    {
        Task<UserInfoDTO> GetUserInfo(string email);

        Task<UserInfoV4DTO> GetUserV4Info(string email);

        Task<List<ModulePermissionDTO>> GetUserModulePermissions(string email);

        Task<List<ModulePermissionDTO>> GetUserModulePermissionsByRole(List<string> roles);
        Task<SupercoachDelegate> GetSupercoachDelegateByUserMid(string supercoach_mid);

    }
}
