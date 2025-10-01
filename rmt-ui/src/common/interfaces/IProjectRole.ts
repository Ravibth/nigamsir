export interface IProjectRoles {
  id?: number;
  projectId?: number;
  user?: string;
  userName?: string;
  role?: string;
  delegateUserName?: string;
  delegateEmail?: string;
  description: string;
  isActive?: boolean;
  createdBy?: string;
  modifiedBy?: string;
  createdAt?: Date;
  modifiedAt?: Date;
  roleOrder?: number;
}
