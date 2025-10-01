using RMT.Configuration.Domain.DTO.Request;
using RMT.Configuration.Domain.Entities;

namespace RMT.Configuration.Domain.Repositories
{
    public interface INavigationRepository
    {
        Task<List<MenuMaster>> GetAllMenuMaster();

        Task<List<RoleMenu>> GetRoleMenuByFilter(string role, Int64 menuId);

        Task<List<MenuMaster>> GetMenuByRoles(List<string> _roles);

        Task<MenuMaster> UpdateMenuMasterItem(MenuMaster entityObj);

        Task<RoleMenu> UpdateRoleMenuItem(RoleMenu entityObj);


        Task<List<ContextMenuMaster>> GetAllContextMenuMaster();

        Task<List<RoleContextMenu>> GetRoleContextMenuByFilter(string role, Int64 menuId);

        Task<List<ContextMenuMaster>> GetContextMenuByRoles(List<string> _roles,UserDecorator userDecorator);

        Task<ContextMenuMaster> UpdateContextMenuMasterItem(ContextMenuMaster entityObj);

        Task<RoleContextMenu> UpdateRoleContextMenuItem(RoleContextMenu entityObj);

    }
}
