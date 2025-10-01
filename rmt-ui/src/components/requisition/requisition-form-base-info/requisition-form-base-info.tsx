import ControllerTextField from "../../controllerInputs/controllerTextField";
import {
  EBaseRequisitionFormMainControlForm,
  IGlobalOptionsForParameters,
  DescriptionControlSxProps,
  AutocompleteControlSxProps,
} from "../utils";
import { Grid, Tooltip, Typography } from "@mui/material";
import InfoIcon from "@mui/icons-material/Info";
import {
  Control,
  FieldErrors,
  UseFormSetValue,
  FieldValues,
  UseFormGetValues,
} from "react-hook-form";
import { NO_OF_RESOURCE_CONFIG_VALUE } from "../../../global/constant";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import { IDesignationMaster } from "../../../common/interfaces/IDesignationMaster";

interface IRequisitionFormBaseInfo {
  isReadOnlyModeOn: boolean;
  control: Control<FieldValues, any>;
  getValues: UseFormGetValues<FieldValues>;
  setValue: UseFormSetValue<FieldValues>;
  errors: FieldErrors;
  isUpdateModeOn: boolean;
  optionsMaster: IGlobalOptionsForParameters;
  appendRemoveResourcesFields: () => void;
}

// const IOSSwitch = styled((props: SwitchProps) => (
//   <Switch focusVisibleClassName=".Mui-focusVisible" disableRipple {...props} />
// ))(({ theme }) => ({
//   width: 42,
//   height: 26,
//   padding: 0,
//   "& .MuiSwitch-switchBase": {
//     padding: 0,
//     margin: 2,
//     transitionDuration: "300ms",
//     "&.Mui-checked": {
//       transform: "translateX(16px)",
//       color: "#fff",
//       "& + .MuiSwitch-track": {
//         backgroundColor: theme.palette.mode === "dark" ? "#2ECA45" : "#00a7b5 ",
//         opacity: 1,
//         border: 0,
//       },
//       "&.Mui-disabled + .MuiSwitch-track": {
//         opacity: 0.5,
//       },
//     },
//     "&.Mui-focusVisible .MuiSwitch-thumb": {
//       color: "#33cf4d",
//       border: "6px solid #fff",
//     },
//     "&.Mui-disabled .MuiSwitch-thumb": {
//       color:
//         theme.palette.mode === "light"
//           ? theme.palette.grey[100]
//           : theme.palette.grey[600],
//     },
//     "&.Mui-disabled + .MuiSwitch-track": {
//       opacity: theme.palette.mode === "light" ? 0.5 : 0.3,
//     },
//   },
//   "& .MuiSwitch-thumb": {
//     boxSizing: "border-box",
//     width: 22,
//     height: 22,
//   },
//   "& .MuiSwitch-track": {
//     borderRadius: 26 / 2,
//     backgroundColor: theme.palette.mode === "light" ? "#787878" : "#39393D",
//     opacity: 1,
//     transition: theme.transitions.create(["background-color"], {
//       duration: 500,
//     }),
//   },
// }));

const RequisitionFormBaseInfo = (props: IRequisitionFormBaseInfo) => {
  return (
    <Grid container spacing={2} sx={{ pl: 2, pr: 2 }}>
      <Grid item xs={2}>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <ControllerAutoCompleteTextFieldWithGetOptionsLabel
              name={EBaseRequisitionFormMainControlForm.designation}
              control={props?.control}
              freeSolo={false}
              defaultValue={""}
              options={props.optionsMaster.designation.sort((a, b) =>
                `${a?.grade || ""} | ${a?.designation_name || ""}` >
                `${b?.grade || ""} | ${b?.designation_name || ""}`
                  ? 1
                  : -1
              )}
              readOnly={props.isReadOnlyModeOn}
              disabled={props.isReadOnlyModeOn}
              filterSelectedOptions={false}
              error={
                props?.errors[EBaseRequisitionFormMainControlForm.designation]
                  ? true
                  : false
              }
              getOptionLabel={(option: IDesignationMaster) =>
                option
                  ? `${option?.grade || ""} | ${option?.designation_name || ""}`
                  : ""
              }
              label={"Designation"}
              required={true}
              onChange={(e: any) => {
                // props.onDesignationChange(e);
              }}
              sx={AutocompleteControlSxProps}
            />
          </Grid>
        </Grid>
      </Grid>
      {!props.isUpdateModeOn && (
        <>
          <Grid item xs={2}>
            <ControllerNumberTextField
              sx={DescriptionControlSxProps}
              name={EBaseRequisitionFormMainControlForm.numberOfResources}
              label={"No. of Resources"}
              control={props?.control}
              defaultValue="1"
              min={NO_OF_RESOURCE_CONFIG_VALUE.min}
              max={NO_OF_RESOURCE_CONFIG_VALUE.max}
              required={true}
              inputProps={{
                min: 1,
              }}
              onChange={(e: any) => {
                props.appendRemoveResourcesFields();
                // props.onNumberOfResourcesChange(parseInt(e.target.value));
              }}
              error={
                props?.errors[
                  EBaseRequisitionFormMainControlForm.numberOfResources
                ]
                  ? true
                  : false
              }
              validate={(e) =>
                e >= NO_OF_RESOURCE_CONFIG_VALUE.min &&
                e <= NO_OF_RESOURCE_CONFIG_VALUE.max
              }
            />
          </Grid>
          {/* <Grid item xs={2}>
            <Typography component="div" className={"resources-switch"}>
              <span className="resources-switch-label">
                All Resources have similar details
              </span>
              <Typography component="div">
                <Controller
                  name={
                    EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails
                  }
                  control={props?.control}
                  defaultValue={true}
                  render={({ field }) => (
                    <>
                      No
                      <IOSSwitch
                        defaultChecked
                        onChange={(e) => {
                          props.appendRemoveResourcesFields();
                          props.setValue(
                            EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails,
                            e.target.checked
                          );
                        }}
                        sx={{ margin: "0px 10px" }}
                      />
                      Yes
                    </>
                  )}
                />
              </Typography>
            </Typography>
          </Grid> */}
        </>
      )}
      <Grid item xs={7.5}>
        <ControllerTextField
          sx={DescriptionControlSxProps}
          name={EBaseRequisitionFormMainControlForm.description}
          control={props?.control}
          required={true}
          label={"Task Description"}
          error={
            props?.errors[EBaseRequisitionFormMainControlForm.description]
              ? true
              : false
          }
          defaultValue=""
          readOnly={props.isReadOnlyModeOn}
          disabled={props.isReadOnlyModeOn}
          onChange={(e: any) => {}}
        />
      </Grid>
      <Grid item xs={0.5}>
        <Tooltip
          placement="right"
          title={
            "Description of tasks the resource is expected to perform on the job"
          }
        >
          <InfoIcon className="infoIconStyle" />
        </Tooltip>
      </Grid>
    </Grid>
  );
};

export default RequisitionFormBaseInfo;
