// import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
// import {
//   Box,
//   Button,
//   Checkbox,
//   Grid,
//   Modal,
//   Paper,
//   Typography,
// } from "@mui/material";
// import { useNavigate, useParams, useSearchParams } from "react-router-dom";
// import * as GlobalConstant from "../../../global/constant";
// import * as constant from "./Constant";
// import React, { useContext, useEffect, useState } from "react";
// import { UserDetailsContext } from "../../../contexts/userDetailsContext";
// import * as Utils from "./util";
// import { ProjectUpdateDetailsContext } from "../../../contexts/projectDetailsContext";
// import { SnackbarContext } from "../../../contexts/snackbarContext";
// import ActionButton from "../../actionButton/actionButton";
// import { GetAllRequisitionByProjectCode } from "../../../services/allocation/getAllRequisitionByProjectCode";
// import ConfirmationDialog from "../../../common/confirmation-dialog/confirmation-dialog";
// import {
//   getActiveFieldForMarketPlace,
//   setIsRequisitionCreationAllowed,
// } from "../../../services/project-list-services/project-list-services";
// import ProjectView from "../../project-view/project-view";
// import { getHoursLabel } from "../../../global/utils";
// import moment from "moment";
// import ControllerCalendar from "../../controllerInputs/controlerCalendar";
// import { useForm } from "react-hook-form";
// import { addProjectToMarketPlace } from "../../../services/marketPlace/addProjectToMarketPlace";
// import { createOrUpdatePublishedFieldForMarketPlace } from "../../../services/project-published-for-marketPlace/createOrUpdatePublishedFieldForMarketPlace";
// import { getPublishedFieldByprojectCodeForMarketPlace } from "../../../services/project-published-for-marketPlace/getPublishedFieldByprojectCodeForMarketPlace";

import { Checkbox } from "@mui/material";
import Grid from "@mui/material/Grid";
import { useState } from "react";

// const label = { inputProps: { "aria-label": "Checkbox demo" } };
// let activeFieldMap = {} as any;

const CheckBoxComponent = (props: any) => {
  const { inputPros, checked, displayName, disabled, handleChangeForCheckBox } = props;
  const [isChecked, setIsChecked] = useState(checked);

  return (
    <Grid container className="check-container">
      <Grid item xs={2}>
        <Checkbox
          checked={isChecked}
          {...{
            inputProps: inputPros.inputProps,
          }}
          disabled={disabled} 
          onChange={(e: any) => {
            setIsChecked(!isChecked);
            handleChangeForCheckBox(e);
          }}
        />
      </Grid>
      <Grid
        item
        xs={10}
        // style={{ wordBreak: "break-word" }}
        className="masking-field-name"
      >
        {displayName}
      </Grid>

      {/* <Grid item xs={7}></Grid> */}
    </Grid>
  );
};

export default CheckBoxComponent;
