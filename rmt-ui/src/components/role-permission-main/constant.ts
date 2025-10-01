import { SxProps } from "@mui/material";
import { RolesListMaster } from "../../common/enums/ERoles";

export const AssignRoleButtonSXProps: SxProps = {
  color: "#fff",
  padding: "6px 12px",
  borderRadius: "10px",
  textTransform: "capitalize",
  fontSize: "16px",
};

export const ModalDialogSXProps: SxProps = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "50%",
  bgcolor: "#fff",
  boxShadow: 24,
  p: 3,
  borderRadius: "15px",
};

export const ApplyConfirmationModalSXProps: SxProps = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "25%",
  bgcolor: "#fff",
  boxShadow: 24,
  p: 3,
  borderRadius: "15px",
};

export interface IAllRoleList {
  role_name?: string;
  isAssigned?: boolean;
  isDisabled?: boolean;
}

export interface IUsersInfo {
  roles: string[];
}

enum EPermissions {
  Assign = "Assign",
  Unassign = "Unassign",
}

export const rolePermission = [
  {
    role_name: RolesListMaster.Admin,
    permissions: [
      {
        Name: RolesListMaster.Employee,
        Perm: [],
      },
      { Name: RolesListMaster.ResourceRequestor, Perm: [] },
      { Name: RolesListMaster.AdditionalEl, Perm: [] },
      {
        Name: RolesListMaster.AdditionalDelegate,
        Perm: [],
      },
      { Name: RolesListMaster.Delegate, Perm: [] },
      { Name: RolesListMaster.Reviewer, Perm: [] },
      { Name: RolesListMaster.Admin, Perm: [] },
      {
        Name: RolesListMaster.Leaders,
        Perm: [],
      },
      {
        Name: RolesListMaster.CEOCOO,
        Perm: [EPermissions.Assign, EPermissions.Unassign],
      },
      { Name: RolesListMaster.SystemAdmin, Perm: [] },
    ],
  },
  {
    role_name: RolesListMaster.SystemAdmin,
    permissions: [
      {
        Name: RolesListMaster.Employee,
        Perm: [],
      },
      {
        Name: RolesListMaster.ResourceRequestor,
        Perm: [],
      },
      {
        Name: RolesListMaster.AdditionalEl,
        Perm: [],
      },
      {
        Name: RolesListMaster.AdditionalDelegate,
        Perm: [],
      },
      {
        Name: RolesListMaster.Delegate,
        Perm: [],
      },
      {
        Name: RolesListMaster.Reviewer,
        Perm: [],
      },
      {
        Name: RolesListMaster.Admin,
        Perm: [EPermissions.Assign, EPermissions.Unassign],
      },
      {
        Name: RolesListMaster.CEOCOO,
        Perm: [EPermissions.Assign, EPermissions.Unassign],
      },
      {
        Name: RolesListMaster.Leaders,
        Perm: [],
      },
      { Name: RolesListMaster.SystemAdmin, Perm: [] },
    ],
  },
];
