import {
  FieldErrors,
  UseFormSetValue,
  Control,
  FieldValues,
  UseFormGetValues,
  UseFormReset,
} from "react-hook-form";
import {
  EBaseRequisitionFormMainControlForm,
  IGlobalOptionsForParameters,
  ResourceSpecificationHeader,
} from "../utils";
import RequisitionDates from "./requisition-dates/requisition-dates";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { Grid, Typography } from "@mui/material";
import RequisitionParameters from "./requisition-parameters/requisition-parameters";
import Accordion from "@mui/material/Accordion";
import AccordionSummary from "@mui/material/AccordionSummary";
import AccordionDetails from "@mui/material/AccordionDetails";
import ExpandCircleDownIcon from "@mui/icons-material/ExpandCircleDown";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";

interface IRequisitionFormBreakups {
  index: number;
  isReadOnlyModeOn: boolean;
  control: Control<FieldValues, any>;
  reset: UseFormReset<any>;
  getValues: UseFormGetValues<FieldValues>;
  setValue: UseFormSetValue<FieldValues>;
  errors: FieldErrors;
  isUpdateModeOn: boolean;
  projectInfo: IProjectMaster;
  trigger: any;
  field: any;
  optionsMaster: IGlobalOptionsForParameters;
}

const RequisitionFormBreakups = (props: IRequisitionFormBreakups) => {
  const getControlName = (type: string, errorType?: boolean) => {
    if (
      type === EBaseRequisitionFormMainControlForm.offerings ||
      type === EBaseRequisitionFormMainControlForm.solutions
    ) {
      return type;
    } else {
      return errorType
        ? `${EBaseRequisitionFormMainControlForm.allDetails}[${props.index}].${type}`
        : `${EBaseRequisitionFormMainControlForm.allDetails}.${props.index}.${type}`;
    }
  };

  const getParameterControlError = (
    categoryItemToCheck: EBaseRequisitionFormMainControlForm
  ): boolean => {
    if (props.errors[EBaseRequisitionFormMainControlForm.allDetails]) {
      const errorsOfCurrentFieldItems =
        props.errors[EBaseRequisitionFormMainControlForm.allDetails][
          props.index
        ];
      if (errorsOfCurrentFieldItems) {
        return errorsOfCurrentFieldItems[categoryItemToCheck] ? true : false;
      }
    }
    return false;
  };

  return (
    <Grid container spacing={2}>
      <Grid
        item
        xs={12}
        // sx={{ backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor2 }}
        sx={{
          backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor2,
          ml: 2,
          // mr: 3,
          pt: 3,
          pb: 3,
        }}
      >
        <RequisitionDates
          isReadOnlyModeOn={props.isReadOnlyModeOn}
          control={props.control}
          getValues={props.getValues}
          setValue={props.setValue}
          errors={props.errors}
          isUpdateModeOn={props.isUpdateModeOn}
          projectInfo={props.projectInfo}
          getControlName={getControlName}
          trigger={props.trigger}
          getParameterControlError={getParameterControlError}
        />
      </Grid>
      <Grid item xs={12}>
        <Accordion
          defaultExpanded
          sx={{ backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor2 }}
        >
          <AccordionSummary
            expandIcon={
              <ExpandCircleDownIcon
                fontSize={"large"}
                sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}
              />
            }
            aria-controls="panel1-content"
            id="panel1-header"
          >
            <Typography component="span" sx={ResourceSpecificationHeader}>
              Select Resource Specification
            </Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Grid container spacing={2}>
              <RequisitionParameters
                isReadOnlyModeOn={props.isReadOnlyModeOn}
                control={props.control}
                getValues={props.getValues}
                setValue={props.setValue}
                errors={props.errors}
                isUpdateModeOn={props.isUpdateModeOn}
                projectInfo={props.projectInfo}
                getControlName={getControlName}
                trigger={props.trigger}
                field={props.field}
                fieldData={props.getValues(getControlName(""))}
                optionsMaster={props.optionsMaster}
                index={props.index}
                getParameterControlError={getParameterControlError}
                reset={props.reset}
              />
            </Grid>
          </AccordionDetails>
        </Accordion>

        {/* <Grid item xs={12} sx={ResourceSpecificationHeader}>
          Select Resource Specification
        </Grid>
        <Grid item xs={12}>
          <Grid container spacing={2}>
            <RequisitionParameters
              isReadOnlyModeOn={props.isReadOnlyModeOn}
              control={props.control}
              getValues={props.getValues}
              setValue={props.setValue}
              errors={props.errors}
              isUpdateModeOn={props.isUpdateModeOn}
              projectInfo={props.projectInfo}
              getControlName={getControlName}
              trigger={props.trigger}
              field={props.field}
              fieldData={props.getValues(getControlName(""))}
              optionsMaster={props.optionsMaster}
              index={props.index}
              getParameterControlError={getParameterControlError}
              reset={props.reset}
            />
          </Grid>
        </Grid> */}
      </Grid>
    </Grid>
  );
};

export default RequisitionFormBreakups;
