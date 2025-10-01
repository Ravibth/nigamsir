import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const RequisitionHeaderSxProps: SxProps = {
  fontSize: "20px",
  fontWeight: "550",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
};

export enum requistion_label {
  requistion_created_successfully_msg = "Requisition created successfully.",
  requistion_error_msg = "Error in creating new requisition!",
  requistion_update_msg = "Requisition updated successfully",
  requistion_error_update_msg = "Error in updating Requisition",
  update_requisition = "Update Requisition",
  create_requisition = "Create Requisition",
  bulk_upload = "Bulk Upload",
  download_template = "Download Template",
  start_date = "Start Date",
  end_date = "End Date",
  location = "Location",
  back = "BACK",
  skill_error_msg = "Please select required Expertise skills  for the requisition.",
  resource_details_error = "Some of the resource details are missing, please review the form.",
  skills = "Skills",
  goodtohave = "Good to have",
  resource_effort_hours_error = "Please enter valid effort hours for the requisition.",
}
