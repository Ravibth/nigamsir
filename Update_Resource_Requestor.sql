-- use Identity DB
update "USER_ROLE" set "role" = 'ResourceRequestor' where "role"= 'Resource Requestor';
update "ROLE" set 'role_name' ='ResourceRequestor' where "role_name" = 'Resource Requestor';
update "ROLE_MODULE_PERMISSION" set "role" ='ResourceRequestor' where "role" = 'Resource Requestor';

-- use Project DB
Update "ProjectRoles" set "Role" ='ResourceRequestor' where "Role" ='Resource Requestor';

-- use configuration DB
update "RoleContextMenu" set "Role"='ResourceRequestor' where "Role" ='Resource Requestor';
update "RoleMenu" set "Role" ='ResourceRequestor' where "Role" ='Resource Requestor';