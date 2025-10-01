import { Control } from "react-hook-form";
import { IBusinessUnitExpertiseOptions, IMySkillsForm } from "../interfaces";
import { Grid, Tooltip } from "@mui/material";
import { EMySkillsForm } from "../enums";
import React from "react";
import InfoIcon from "@mui/icons-material/Info";
import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";

export interface IAddUpdateSkillFilterProps {
  control: Control<IMySkillsForm, any>;
  businessUnitExpertiseOptions: IBusinessUnitExpertiseOptions;
  updateSkillsFilterOptions: () => void;
  selectedCompetency: ICompetencyMaster;
}
const AddUpdateSkillFilter = (props: IAddUpdateSkillFilterProps) => {
  return (
    <React.Fragment>
      {/* <Grid item xs={1.3} sx={AddUpdateSkillFilterLabel}>
        Filters
      </Grid> */}
      <Grid item xs={11.5}>
        <ControllerAutoCompleteTextFieldWithGetOptionsLabel
          control={props.control}
          name={EMySkillsForm.competency}
          required={false}
          multiple={false}
          filterSelectedOptions={true}
          label={"Competency"}
          getOptionLabel={(option: ICompetencyMaster) => {
            console.log(option);
            return option.competency;
          }}
          options={props.businessUnitExpertiseOptions.competencyOptions}
          freeSolo={false}
          value={props.selectedCompetency}
          onChange={(e) => {
            props.updateSkillsFilterOptions();
          }}
        />
      </Grid>
      <Grid item xs={0.5}>
        <span>
          <Tooltip
            placement="right"
            title={
              "Use the Competency filter to find skills tagged to a specific competency, or leave it as blank to view all skills in the 'Skill Name' field."
            }
          >
            <InfoIcon className="infoIconStyle" />
          </Tooltip>
        </span>
      </Grid>
    </React.Fragment>
  );
};
export default AddUpdateSkillFilter;
