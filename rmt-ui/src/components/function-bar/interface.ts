export interface IRoleSearchProps {
  setSearchRoles: React.Dispatch<React.SetStateAction<string[]>>;
}

export interface IRoleOption {
  roleDisplayName: string;
  roleName: string;
}

export type IRoleOptions = IRoleOption[];
