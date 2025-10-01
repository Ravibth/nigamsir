import { Grid, IconButton } from "@mui/material";
import React, { useEffect } from "react";
import DeleteIcon from "@mui/icons-material/Delete";
import { useForm } from "react-hook-form";
import ControllerAutocompleteFilteredOptionsTextfield from "../../controllers/controller-autocomplete-filtered-options-textfield";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";

const AssignResourcesForm = React.memo((props: any) => {
  const { data, chooseDesignations } = props;
  const {
    control,
    watch,
    setValue,
    formState: { errors, isDirty },
  } = useForm({ mode: "onTouched" });
  const autoPopulate = (params: any) => {
    if (params && Object.keys(params).length > 0) {
      Object.keys(params).forEach((key) => {
        // console.log(key, params[key]);
        setValue(key, params[key]);
      });
    }
  };
  useEffect(() => {
    // console.log(props);
    autoPopulate(data);
  }, [data]);
  return (
    <React.Fragment>
      <Grid container>
        <Grid item xs={3}>
          <ControllerAutocompleteFilteredOptionsTextfield
            name="label"
            control={control}
            multiple={false}
            options={chooseDesignations.map((e: any) => e.label)}
            defaultValue={[]}
            onChange={() => {}}
            required={true}
            error={errors.label ? true : false}
          />
        </Grid>
        <Grid item xs={2}>
          <ControllerNumberTextField
            name="noOfResources"
            defaultValue={""}
            control={control}
            onChange={() => {}}
            required={true}
            error={errors.noOfResources ? true : false}
          />
        </Grid>
        <Grid item xs={5}>
          {/* <AssignSkills
            index={index}
            {...ProjectSkillsProps}
            {...SkillsProps}
            {...props}
            currentSkills={getProjectSkills(data.projectDemandSkills)}
            handleSkillChange={(value: any) => {
              handleSkillChange(value, data);
            }}
          ></AssignSkills> */}
          {/* <ControllerAutoCompleteChipsSimple
            name="projectDemandSkills"
            defaultValue={[]}
            options={}
            control={control}
            onChange={() => {}}
            multiple={true}
            required={true}
            error={errors.projectDemandSkills ? true : false}
          /> */}
        </Grid>
        <Grid item xs={0.5}>
          <IconButton
            onClick={() => {
              //   handleDeleteClick(data);
            }}
          >
            <DeleteIcon className="delete-icon" />
          </IconButton>
        </Grid>
      </Grid>
    </React.Fragment>
  );
});

export default AssignResourcesForm;
