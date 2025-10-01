using Microsoft.EntityFrameworkCore;
using RMT.Configuration.Domain;
using RMT.Configuration.Domain.DTO.Request;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using RMT.Configuration.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Configuration.Infrastructure.Repositories
{
    public class NavigationRepository : INavigationRepository
    {
        private readonly ConfigurationDbContext _configurationDbContext;
        public NavigationRepository(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public async Task<List<MenuMaster>> GetAllMenuMaster()
        {
            return await _configurationDbContext.MenuMaster.Where(a => a.IsActive == true).OrderBy(o => o.Order).ToListAsync();
        }

        public async Task<List<RoleMenu>> GetRoleMenuByFilter(string role, Int64 menuId)
        {
            List<RoleMenu> result;

            result = await (from rolemenu in _configurationDbContext.RoleMenu
                            join rolemaster in _configurationDbContext.MenuMaster
                            on rolemenu.MenuId equals rolemaster.Id
                            where rolemaster.IsActive == true && rolemenu.IsActive == true
                            && (rolemenu.Role.ToLower() == role.ToLower() || string.IsNullOrEmpty(role))
                            && (rolemenu.MenuId == menuId || menuId == 0)
                            orderby rolemaster.Order
                            select rolemenu)
                            .ToListAsync();
            return result;
        }

        public async Task<List<MenuMaster>> GetMenuByRoles(List<string> _roles)
        {
            List<MenuMaster> result;

            if (!(_roles != null || _roles.Count > 0))
            {
                result = await (from rolemenu in _configurationDbContext.RoleMenu
                                join rolemaster in _configurationDbContext.MenuMaster
                                on rolemenu.MenuId equals rolemaster.Id
                                select rolemaster)
                               .Distinct()
                               .OrderBy(o => o.Order).ToListAsync();
            }
            else
            {
                result = await (from rolemenu in _configurationDbContext.RoleMenu
                          join rolemaster in _configurationDbContext.MenuMaster
                          on rolemenu.MenuId equals rolemaster.Id
                          where rolemaster.IsActive == true && rolemenu.IsActive == true
                          && _roles.Contains(rolemenu.Role)
                          select rolemaster)
                                .Distinct().OrderBy(o => o.Order).ToListAsync();


                //await _configurationDbContext.MenuMaster
                //.Where(x => x.IsActive == true)
                //.Select(s => new MenuMaster()
                //{
                //    InternalName = s.InternalName,
                //    DisplayName = s.DisplayName,
                //    RoleMenus = s.RoleMenus.Where(rm => rm.IsActive == true && _roles.Contains(rm.Role)).ToList()

                //})
                ////.Distinct()
                //.OrderBy(a=>a.Order).ToListAsync();



            }
            return result;

        }

        public async Task<MenuMaster> UpdateMenuMasterItem(MenuMaster entityObj)
        {
            MenuMaster curEntity = await _configurationDbContext.MenuMaster
                .Where(e => e.InternalName.ToLower() == entityObj.InternalName.ToLower()).FirstOrDefaultAsync();
            //MenuMaster curEntity = await _configurationDbContext.MenuMaster.Where(e => string.Compare(e.InternalName, entityObj.InternalName, StringComparison.CurrentCultureIgnoreCase) == 0).FirstOrDefaultAsync();

            if (curEntity != null)
            {
                //update entry 
                curEntity.MenuType = entityObj.MenuType;
                curEntity.InternalName = entityObj.InternalName;
                curEntity.DisplayName = entityObj.DisplayName;
                curEntity.ParentId = entityObj.ParentId;
                curEntity.Order = entityObj.Order;
                curEntity.MenuType = entityObj.MenuType;
                curEntity.Path = entityObj.Path;
                curEntity.Description = entityObj.Description;
                curEntity.Is_Expandable = entityObj.Is_Expandable;
                curEntity.IsActive = entityObj.IsActive;
                curEntity.ModifiedAt = entityObj.ModifiedAt;
                curEntity.ModifiedBy = entityObj.ModifiedBy;

                _configurationDbContext.MenuMaster.Update(curEntity);
            }
            else
            {
                //add entry
                _configurationDbContext.MenuMaster.Add(entityObj);
            }
            await _configurationDbContext.SaveChangesAsync();

            MenuMaster responseEntity = await _configurationDbContext.MenuMaster
                .Where(e => e.InternalName.ToLower() == entityObj.InternalName.ToLower()).FirstOrDefaultAsync();
            return responseEntity;

        }

        public async Task<RoleMenu> UpdateRoleMenuItem(RoleMenu entityObj)
        {
            RoleMenu curEntity = await _configurationDbContext.RoleMenu
                .Where(e => e.Role.ToLower() == entityObj.Role.ToLower() && e.MenuId == entityObj.MenuId
            ).FirstOrDefaultAsync();

            if (curEntity != null)
            {
                //update entry 
                curEntity.Role = entityObj.Role;
                curEntity.MenuId = entityObj.MenuId;
                curEntity.IsActive = entityObj.IsActive;
                curEntity.ModifiedAt = entityObj.ModifiedAt;
                curEntity.ModifiedBy = entityObj.ModifiedBy;

                _configurationDbContext.RoleMenu.Update(curEntity);
            }
            else
            {
                //add entry
                _configurationDbContext.RoleMenu.Add(entityObj);
            }
            await _configurationDbContext.SaveChangesAsync();

            RoleMenu responseEntity = await _configurationDbContext.RoleMenu
                .Where(e => e.Role.ToLower() == entityObj.Role.ToLower() && e.MenuId == entityObj.MenuId
            ).FirstOrDefaultAsync();
            return responseEntity;

        }


        public async Task<List<ContextMenuMaster>> GetAllContextMenuMaster()
        {
            return await _configurationDbContext.ContextMenuMaster.Where(a => a.IsActive == true).ToListAsync();
        }

        public async Task<List<RoleContextMenu>> GetRoleContextMenuByFilter(string role, Int64 menuId)
        {
            List<RoleContextMenu> result;

            result = await (from rolemenu in _configurationDbContext.RoleContextMenu
                            join rolemaster in _configurationDbContext.ContextMenuMaster
                            on rolemenu.ContextMenuId equals rolemaster.Id
                            where rolemaster.IsActive == true && rolemenu.IsActive == true
                            && (rolemenu.Role.ToLower() == role.ToLower() || string.IsNullOrEmpty(role))
                            && (rolemenu.ContextMenuId == menuId || menuId == 0)
                            select rolemenu)
                            .ToListAsync();
            return result;
        }

        public async Task<List<ContextMenuMaster>> GetContextMenuByRoles(List<string> _roles, UserDecorator userDecorator)
        {
            List<ContextMenuMaster> result;

            if (!(_roles != null || _roles.Count > 0))
            {
                result = await (from rolemenu in _configurationDbContext.RoleContextMenu
                                join rolemaster in _configurationDbContext.ContextMenuMaster
                                on rolemenu.ContextMenuId equals rolemaster.Id
                                select rolemaster)
                               .Distinct().OrderBy(o => o.Order).ToListAsync();
            }
            else
            {
                result = await (from rolemenu in _configurationDbContext.RoleContextMenu
                                join rolemaster in _configurationDbContext.ContextMenuMaster
                                on rolemenu.ContextMenuId equals rolemaster.Id
                                where rolemaster.IsActive == true && rolemenu.IsActive == true
                                && _roles.Contains(rolemenu.Role)
                                select rolemaster)
                                .Distinct().OrderBy(o => o.Order).ToListAsync();
            }
            return result;

        }

        public async Task<ContextMenuMaster> UpdateContextMenuMasterItem(ContextMenuMaster entityObj)
        {
            ContextMenuMaster curEntity = await _configurationDbContext.ContextMenuMaster
                .Where(e => e.InternalName.ToLower() == entityObj.InternalName.ToLower()).FirstOrDefaultAsync();
            //MenuMaster curEntity = await _configurationDbContext.MenuMaster.Where(e => string.Compare(e.InternalName, entityObj.InternalName, StringComparison.CurrentCultureIgnoreCase) == 0).FirstOrDefaultAsync();

            if (curEntity != null)
            {
                //update entry 
                curEntity.InternalName = entityObj.InternalName;
                curEntity.DisplayName = entityObj.DisplayName;
                curEntity.Order = entityObj.Order;
                curEntity.Description = entityObj.Description;
                curEntity.IsActive = entityObj.IsActive;
                curEntity.ModifiedAt = entityObj.ModifiedAt;
                curEntity.ModifiedBy = entityObj.ModifiedBy;

                _configurationDbContext.ContextMenuMaster.Update(curEntity);
            }
            else
            {
                //add entry
                _configurationDbContext.ContextMenuMaster.Add(entityObj);
            }
            await _configurationDbContext.SaveChangesAsync();

            ContextMenuMaster responseEntity = await _configurationDbContext.ContextMenuMaster
                .Where(e => e.InternalName.ToLower() == entityObj.InternalName.ToLower()).FirstOrDefaultAsync();
            return responseEntity;

        }

        public async Task<RoleContextMenu> UpdateRoleContextMenuItem(RoleContextMenu entityObj)
        {
            RoleContextMenu curEntity = await _configurationDbContext.RoleContextMenu
                .Where(e => e.Role.ToLower() == entityObj.Role.ToLower() && e.ContextMenuId == entityObj.ContextMenuId
            ).FirstOrDefaultAsync();

            if (curEntity != null)
            {
                //update entry 
                curEntity.Role = entityObj.Role;
                curEntity.ContextMenuId = entityObj.ContextMenuId;
                curEntity.IsActive = entityObj.IsActive;
                curEntity.ModifiedAt = entityObj.ModifiedAt;
                curEntity.ModifiedBy = entityObj.ModifiedBy;

                _configurationDbContext.RoleContextMenu.Update(curEntity);
            }
            else
            {
                //add entry
                _configurationDbContext.RoleContextMenu.Add(entityObj);
            }
            await _configurationDbContext.SaveChangesAsync();

            RoleContextMenu responseEntity = await _configurationDbContext.RoleContextMenu
                .Where(e => e.Role.ToLower() == entityObj.Role.ToLower() && e.ContextMenuId == entityObj.ContextMenuId
            ).FirstOrDefaultAsync();
            return responseEntity;

        }

    }
}
