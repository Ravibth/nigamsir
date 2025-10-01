import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../../global/constant";

export const projectDetailModal: SxProps = {
  // backgroundColor: "#e6e8ec",
  backgroundColor: "#fff",
  position: "absolute",
  top: " 50%",
  left: "50%",
  maxWidth: "90%",
  width: "auto",
  maxHeight: "99vh",
  height: "auto",
  margin: "auto",
  transform: "translate(-50%, -50%)",
  boxShadow: "0px 2px 4px rgba(0.9, 0.9, 0.9, 0.9)",
  borderRadius: "10px",
  minWidth: "500px",
  minHeight: "500px",
};

export const MenuIconSxProps: SxProps = {
  mr: 1,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const Modalallocate: SxProps = {
  position: "absolute" as "absolute",
  top: "75%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "100%",
  height: "80%",
  bgcolor: "#fff",
  boxShadow: 24,
  p: 2,
  overflowY: "auto",
};

export const MenuOptions: SxProps = {
  // fontFamily: "GT Walsheim Pro ",
  fontSize: "17px",
};

export const ContextMenuInternalNames = {
  ViewDetails: "View Details",
  CreateRequisition: "Create Requisition",
  BulkUpload: "Bulk Upload",
  AllocateEmployee: "Allocate Employee",
  AllocateSameTeam: "Allocate Same Team",
  RollForwardAllocations: "Roll forward allocations",
  MoveToMarketplace: "Move To Marketplace",
  CalenderView: "Calender View",
  Allocations: "Allocations",
  Requisitions: "Requisitions",
  AssignNewCode: "Assign New Code",
};

export const AssignCodeModalBoxSxProps: SxProps = {
  bgcolor: "#F8F7FA",
  width: "90%",
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  //boxShadow: "0px 2px 4px rgba(0, 0, 0, 0.9)",
  boxShadow: "0px 5px 15px rgba(0, 0, 0, 0.9)",
  // height: '112px'
};

export enum EProjectType {
  RECURRING = "Recurring",
  NON_RECURRING = "Non-Recurring",
}
