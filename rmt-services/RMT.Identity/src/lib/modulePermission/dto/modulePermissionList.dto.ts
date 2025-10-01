import { IsBoolean, IsString } from 'class-validator';

export interface IModulePermissionsAccessDetails {
  create: boolean;

  update: boolean;

  read: boolean;

  delete: boolean;

  approve: boolean;
}

export class ModulePermissionListDto {
  module_name: string;
  module_display: string;
  is_assigned: boolean;
  permissions: IModulePermissionsAccessDetails;
}
