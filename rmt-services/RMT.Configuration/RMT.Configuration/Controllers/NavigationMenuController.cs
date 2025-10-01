using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Configuration.API.Service;
using RMT.Configuration.Application.DTOs.ApplicationConfigurationDTOs;
using RMT.Configuration.Application.DTOs.MasterDataDTOs;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Handlers.CommandHandlers;
using RMT.Configuration.Application.Handlers.QueryHandlers;
using RMT.Configuration.Domain;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RMT.Configuration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationMenuController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;
        public NavigationMenuController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpPost("UpdateMenuMaster")]
        public async Task<MenuMasterDTO> UpdateMenuMaster([FromBody] MenuMasterDTO createMenuMasterDTO)
        {
            try
            {
                var _cmd = new UpdateMenuMasterCommand()
                {
                    InternalName = createMenuMasterDTO.InternalName,
                    DisplayName = createMenuMasterDTO.DisplayName,
                    ParentId = createMenuMasterDTO.ParentId,
                    Order = createMenuMasterDTO.Order,
                    MenuType = createMenuMasterDTO.MenuType,
                    Path = createMenuMasterDTO.Path,
                    Description = createMenuMasterDTO.Description,
                    Is_Expandable = createMenuMasterDTO.Is_Expandable,
                    IsActive = createMenuMasterDTO.IsActive,

                    CreatedAt = createMenuMasterDTO.CreatedAt,
                    ModifiedAt = createMenuMasterDTO.ModifiedAt,
                    CreatedBy = createMenuMasterDTO.CreatedBy,
                    ModifiedBy = createMenuMasterDTO.ModifiedBy,
                };

                var result = await _mediator.Send(_cmd);

                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllMenuMaster")]
        public async Task<List<MenuMaster>> GetAllMenuMaster()
        {
            try
            {
                var _query = new GetAllMenuMasterQuery()
                {
                };
                var result = await _mediator.Send(_query);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetRoleMenuByFilter")]
        public async Task<List<RoleMenuDTO>> GetRoleMenuByFilter(string? role, Int64 menuId)
        {
            try
            {
                var _query = new GetRoleMenuByQuery()
                {
                    _role = role == null ? "" : role,
                    _menuId = menuId
                };
                var result = await _mediator.Send(_query);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetMenuByRoles")]
        public async Task<List<MenuMasterDTO>> GetMenuByRoles(string[] roleNames)
        {
            try
            {
                var _query = new GetMenuByRolesQuery()
                {
                    _roles = roleNames.ToList()
                };
                var result = await _mediator.Send(_query);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateRoleMenuItem")]
        public async Task<RoleMenuDTO> UpdateRoleMenuItem([FromBody] RoleMenuDTO roleMenuDTO)
        {
            try
            {
                var _cmd = new UpdateRoleMenuCommand()
                {
                    Role = roleMenuDTO.Role,
                    MenuId = roleMenuDTO.MenuId,
                    IsActive = roleMenuDTO.IsActive,
                    CreatedAt = roleMenuDTO.CreatedAt,
                    ModifiedAt = roleMenuDTO.ModifiedAt,
                    CreatedBy = roleMenuDTO.CreatedBy,
                    ModifiedBy = roleMenuDTO.ModifiedBy,
                };

                var result = await _mediator.Send(_cmd);

                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }


        [HttpPost("UpdateContextMenuMaster")]
        public async Task<ContextMenuMasterDTO> UpdateContextMenuMaster([FromBody] ContextMenuMasterDTO createMenuMasterDTO)
        {
            try
            {
                var _cmd = new UpdateContextMenuMasterCommand()
                {
                    InternalName = createMenuMasterDTO.InternalName,
                    DisplayName = createMenuMasterDTO.DisplayName,
                    Order = createMenuMasterDTO.Order,
                    Description = createMenuMasterDTO.Description,
                    IsActive = createMenuMasterDTO.IsActive,

                    CreatedAt = createMenuMasterDTO.CreatedAt,
                    ModifiedAt = createMenuMasterDTO.ModifiedAt,
                    CreatedBy = createMenuMasterDTO.CreatedBy,
                    ModifiedBy = createMenuMasterDTO.ModifiedBy,
                };

                var result = await _mediator.Send(_cmd);

                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllContextMenuMaster")]
        public async Task<List<ContextMenuMasterDTO>> GetAllContextMenuMaster()
        {
            try
            {
                var _query = new GetAllContextMenuMasterQuery()
                {
                };
                var result = await _mediator.Send(_query);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetRoleContextMenuByFilter")]
        public async Task<List<RoleContextMenuDTO>> GetRoleContextMenuByFilter(string? role, Int64 menuId)
        {
            try
            {
                var _query = new GetRoleContextMenuByQuery()
                {
                    _role = role == null ? "" : role,
                    _menuId = menuId
                };
                var result = await _mediator.Send(_query);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetContextMenuByRoles")]
        public async Task<List<ContextMenuMasterDTO>> GetContextMenuByRoles(string[] roleNames)
        {
            try
            {
                UserDecorator userDecorator = _userAccessor.GetUser();
                var roles = roleNames.ToList();

                if (userDecorator.roles.Any(m => m == Constants.UserRoles.SystemAdmin))
                {
                    roles.Add(Constants.UserRoles.SystemAdmin);
                }
                if (userDecorator.roles.Any(m => m == Constants.UserRoles.Admin))
                {
                    roles.Add(Constants.UserRoles.Admin);
                }
                if (userDecorator.roles.Any(m => m == Constants.UserRoles.CEOCOO))
                {
                    roles.Add(Constants.UserRoles.CEOCOO);
                }
                var _query = new GetContextMenuByRolesQuery()
                {
                    _roles = roles,
                    userDecorator = userDecorator
                };
                var result = await _mediator.Send(_query);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateRoleContextMenuItem")]
        public async Task<RoleContextMenuDTO> UpdateRoleContextMenuItem([FromBody] RoleContextMenuDTO roleMenuDTO)
        {
            try
            {
                var _cmd = new UpdateRoleContextMenuCommand()
                {
                    Role = roleMenuDTO.Role,
                    ContextMenuId = roleMenuDTO.ContextMenuId,
                    IsActive = roleMenuDTO.IsActive,
                    CreatedAt = roleMenuDTO.CreatedAt,
                    ModifiedAt = roleMenuDTO.ModifiedAt,
                    CreatedBy = roleMenuDTO.CreatedBy,
                    ModifiedBy = roleMenuDTO.ModifiedBy,
                };

                var result = await _mediator.Send(_cmd);

                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// HandleException
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        private object HandleException(Exception ex)
        {
            Guid guid = Guid.NewGuid();
            this.LogException(ex, guid);
            throw new BadHttpRequestException($"{ex.Message}-errorid:{guid}", StatusCodes.Status400BadRequest);//, ex);
        }


    }
}
