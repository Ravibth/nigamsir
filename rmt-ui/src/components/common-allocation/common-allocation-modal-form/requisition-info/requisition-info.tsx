import React from "react";
import { IRequisitionMaster } from "../../../../common/interfaces/IRequisition";
import { Grid } from "@mui/material";
import RequisitionDetails from "../../../RequisitionsDeatils/requisition-details";
export interface IRequisitionInfoProps {
  requisition: IRequisitionMaster;
}
const RequisitionInfo = (props: IRequisitionInfoProps) => {
  return (
    <Grid container>
      <Grid item xs={12}>
        <RequisitionDetails
          requisitionDetail={props.requisition}
        ></RequisitionDetails>
      </Grid>
    </Grid>
  );
};
export default RequisitionInfo;
