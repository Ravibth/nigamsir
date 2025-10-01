import { Grid, FormControlLabel, Checkbox, Tooltip } from "@mui/material";
import moment from "moment";
import ControllerCalendar from "../../../controllerInputs/controlerCalendar";
import {
  Control,
  FieldErrors,
  UseFormSetValue,
  FieldValues,
  Controller,
  UseFormGetValues,
} from "react-hook-form";
import { IProjectMaster } from "../../../../common/interfaces/IProject";
import {
  EBaseRequisitionFormMainControlForm,
  ParameterTypographyContinerMt1,
} from "../../utils";
import ControllerOnlyNumberTextField from "../../../controllerInputs/controllerOnlyNumberTextfield";
import * as GC from "../../../../global/constant";
import {
  calculateDaysBetweenTwoDates,
  GetNewDateWithNoonTimeZone,
} from "../../../../utils/date/dateHelper";
import InfoIcon from "@mui/icons-material/Info";

interface IRequisitionDates {
  isReadOnlyModeOn: boolean;
  control: Control<FieldValues, any>;
  getValues: UseFormGetValues<FieldValues>;
  setValue: UseFormSetValue<FieldValues>;
  errors: FieldErrors;
  isUpdateModeOn: boolean;
  projectInfo: IProjectMaster;
  getControlName: (type: string) => string;
  trigger: any;
  getParameterControlError: (
    categoryItemToCheck: EBaseRequisitionFormMainControlForm
  ) => boolean;
}

const RequisitionDates = (props: IRequisitionDates) => {
  const effortValidate = () => {
    const updatedValues: any = props.getValues();
    const effort = parseInt(
      props.getValues(
        props.getControlName(EBaseRequisitionFormMainControlForm.noOfHours)
      )
    );
    if (
      props.getValues(
        props.getControlName(
          EBaseRequisitionFormMainControlForm.isPerDayHourAllocation
        )
      )
    ) {
      const isValid =
        effort >= GC.DEFAULT_ALLOCATION_HOUR.min &&
        effort <= GC.DEFAULT_ALLOCATION_HOUR.max;
      return isValid;
    } else {
      const start_date = props.getValues(
        props.getControlName(EBaseRequisitionFormMainControlForm.startDate)
      );
      const end_date = props.getValues(
        props.getControlName(EBaseRequisitionFormMainControlForm.endDate)
      );
      const isValid =
        effort >= GC.DEFAULT_ALLOCATION_HOUR.min &&
        effort <=
          GC.DEFAULT_ALLOCATION_HOUR.max *
            calculateDaysBetweenTwoDates(start_date, end_date);
      return isValid;
    }
  };

  return (
    <Grid container spacing={2}>
      <Grid item xs={1.97}>
        <ControllerCalendar
          name={props.getControlName(
            EBaseRequisitionFormMainControlForm.startDate
          )}
          control={props.control}
          minDate={
            moment(
              GetNewDateWithNoonTimeZone(props?.projectInfo?.startDate)
            ).isBefore(GetNewDateWithNoonTimeZone())
              ? GetNewDateWithNoonTimeZone()
              : GetNewDateWithNoonTimeZone(props?.projectInfo?.startDate)
          }
          shouldDisableWeekends={true}
          maxDate={GetNewDateWithNoonTimeZone(props?.projectInfo?.endDate)}
          label={"Start Date"}
          validate={(value) => {
            const changedDate = GetNewDateWithNoonTimeZone(value);
            const currentDate = GetNewDateWithNoonTimeZone();
            const projectStartDate = GetNewDateWithNoonTimeZone(
              props.projectInfo.startDate
            );
            return (
              moment(changedDate).isSameOrAfter(moment(currentDate)) &&
              moment(changedDate).isSameOrAfter(moment(projectStartDate)) &&
              changedDate.getDay() !== 0 &&
              changedDate.getDay() !== 6
            );
          }}
          ignoreTimeZone={true}
          error={props.getParameterControlError(
            EBaseRequisitionFormMainControlForm.startDate
          )}
          onChange={(date: any) => {
            props.trigger(
              props.getControlName(
                EBaseRequisitionFormMainControlForm.startDate
              )
            );
            props.trigger(
              props.getControlName(
                EBaseRequisitionFormMainControlForm.noOfHours
              )
            );
          }}
          required={true}
          readOnly={props.isReadOnlyModeOn}
          disabled={props.isReadOnlyModeOn}
        />
      </Grid>

      <Grid item xs={1.99}>
        <ControllerCalendar
          name={props.getControlName(
            EBaseRequisitionFormMainControlForm.endDate
          )}
          control={props.control}
          label={"End Date"}
          validate={(value) => {
            const changedEndDate = GetNewDateWithNoonTimeZone(value);
            const startDate = GetNewDateWithNoonTimeZone(
              props.getValues(
                props.getControlName(
                  EBaseRequisitionFormMainControlForm.startDate
                )
              )
            );
            const projectEndDate = GetNewDateWithNoonTimeZone(
              props.projectInfo.endDate
            );

            return (
              moment(changedEndDate).isSameOrAfter(moment(startDate)) &&
              moment(changedEndDate).isSameOrBefore(moment(projectEndDate)) &&
              changedEndDate.getDay() !== 0 &&
              changedEndDate.getDay() !== 6
            );
          }}
          minDate={props.getValues(
            props.getControlName(EBaseRequisitionFormMainControlForm.startDate)
          )}
          maxDate={GetNewDateWithNoonTimeZone(props?.projectInfo?.endDate)}
          shouldDisableWeekends={true}
          ignoreTimeZone={true}
          error={props.getParameterControlError(
            EBaseRequisitionFormMainControlForm.endDate
          )}
          onChange={(date: any) => {
            props.trigger(
              props.getControlName(EBaseRequisitionFormMainControlForm.endDate)
            );
            props.trigger(
              props.getControlName(
                EBaseRequisitionFormMainControlForm.noOfHours
              )
            );
          }}
          required={true}
          readOnly={props.isReadOnlyModeOn}
          disabled={props.isReadOnlyModeOn}
        />
      </Grid>

      <Grid item xs={2}>
        <ControllerOnlyNumberTextField
          name={props.getControlName(
            EBaseRequisitionFormMainControlForm.noOfHours
          )}
          control={props.control}
          label={"Effort (hrs)"}
          defaultValue=""
          required={true}
          validate={() => effortValidate()}
          error={props.getParameterControlError(
            EBaseRequisitionFormMainControlForm.noOfHours
          )}
          min={GC.DEFAULT_ALLOCATION_HOUR.min}
          max={
            props.getValues(
              props.getControlName(
                EBaseRequisitionFormMainControlForm.isPerDayHourAllocation
              )
            )
              ? GC.DEFAULT_ALLOCATION_HOUR.max
              : undefined
          }
          onChange={(date: any) => {
            props.trigger(
              props.getControlName(
                EBaseRequisitionFormMainControlForm.noOfHours
              )
            );
          }}
          readOnly={props.isReadOnlyModeOn}
          disabled={props.isReadOnlyModeOn}
        />
      </Grid>

      <Grid item xs={1.5}>
        <Controller
          name={props.getControlName(
            EBaseRequisitionFormMainControlForm.isPerDayHourAllocation
          )}
          control={props.control}
          defaultValue={false}
          render={({ field }) => (
            <FormControlLabel
              sx={ParameterTypographyContinerMt1}
              key={"Hours/Day"}
              control={
                <Checkbox
                  checked={field.value}
                  disabled={props.isReadOnlyModeOn}
                  onChange={(data: any) => {
                    field.onChange(data);
                    // updateEntity();
                    props.trigger(
                      props.getControlName(
                        EBaseRequisitionFormMainControlForm.noOfHours
                      )
                    );
                  }}
                  name="gilad"
                />
              }
              label={"Hours/Day"}
            />
          )}
        />
        <Tooltip
          placement="right"
          title={
            "Effort(hr) indicates hours required from the resource in the defined period i.e. *8 entered would be 8 hrs in the entire period. By checking Hours/Day, it defines the number of hours a resource is required for each day in the defined period. For E.g. now 8 hrs would mean 8 hrs per day. "
          }
        >
          <InfoIcon className="infoIconStyle" />
        </Tooltip>
      </Grid>
    </Grid>
  );
};

export default RequisitionDates;
