import { DataTypes } from 'sequelize';

export class ModulePermissionDTO {
  public moduleName: DataTypes.StringDataType;
  public permissionName: DataTypes.StringDataType;
}
