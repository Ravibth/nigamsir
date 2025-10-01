import Grid from "@mui/material/Grid";
import {
  IConfigurationMaster,
  EKeySelectorsForMinBreakup,
  IConfigurationMainBreakupMetaValues,
  ETextFieldRendererControlTypes,
} from "../../../../common/interfaces/IConfigurationMaster";
import ControllerNumberTextField from "../../../controllerInputs/controllerNumbeTextfield";
import React from "react";
import {
  VerticalCenterAlignSxProps,
  HorizontalRightAlignSxProps,
} from "../../../common-allocation/user-info-timeline-group/style";
import { ConfigurationMasterGroupLabelSxProps } from "../../style";
import ActionButton from "../../../actionButton/actionButton";

interface IConfigGlobalRendererProps {
  configurationGroupItem: IConfigurationMaster;
  selectorKey: string[];
  control: any;
  errors: any;
  getValues: any;
  anyChangesDetected: boolean;
  checkForAnyChangesInTheForm: () => void;
}
const ConfigGlobalRenderer = (props: IConfigGlobalRendererProps) => {
  const TextFieldRenderer = (
    breakupMetaDefaultValues: IConfigurationMainBreakupMetaValues
  ) => {
    const controlTypeForValue = props.configurationGroupItem.schemaValues.find(
      (schemaItem) =>
        schemaItem.key.toLowerCase() ===
        breakupMetaDefaultValues.key.toLowerCase()
    );
    if (controlTypeForValue) {
      switch (controlTypeForValue.controlType?.toLowerCase()) {
        case ETextFieldRendererControlTypes.INTEGER:
          return (
            <ControllerNumberTextField
              name={breakupMetaDefaultValues.key}
              control={props.control}
              label={breakupMetaDefaultValues.displayKey}
              onChange={() => {
                props.checkForAnyChangesInTheForm();
              }}
              allowNegative={true}
              defaultValue={breakupMetaDefaultValues.value}
              error={props.errors[breakupMetaDefaultValues.key] ? true : false}
              validate={(updatedValue) => {
                const regex = new RegExp(controlTypeForValue.validationRegEx);
                if (regex) {
                  return regex.test(updatedValue);
                }
              }}
            />
          );
        default:
          return <></>;
      }
    }
    return <></>;
  };

  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        {props.selectorKey.includes(EKeySelectorsForMinBreakup.DEFAULT) && (
          <Grid container spacing={2}>
            <Grid item xs={11} sx={ConfigurationMasterGroupLabelSxProps}>
              Global Configuration
            </Grid>
            <Grid item xs={1} sx={HorizontalRightAlignSxProps}>
              <ActionButton
                label={"Save"}
                onClick={() => {}}
                disabled={
                  !props.anyChangesDetected ||
                  Object.keys(props.errors).length > 0
                }
                type={"submit"}
              />
            </Grid>
          </Grid>
        )}
      </Grid>
      {props.configurationGroupItem.configurationMainBreakups
        .filter(
          (defaultValueItemMainBreakup) =>
            defaultValueItemMainBreakup.keySelector.toLowerCase() ===
            EKeySelectorsForMinBreakup.DEFAULT.toLowerCase()
        )
        .map((defaultValueItemMainBreakup) => {
          return (
            <Grid item xs={12} key={defaultValueItemMainBreakup.id}>
              <Grid container spacing={2}>
                {defaultValueItemMainBreakup.configurationMainBreakupMetaValues.map(
                  (breakupMetaDefaultValues) => {
                    return (
                      <React.Fragment key={breakupMetaDefaultValues.key}>
                        <Grid item xs={2} sx={VerticalCenterAlignSxProps}>
                          {TextFieldRenderer(breakupMetaDefaultValues)}
                        </Grid>
                      </React.Fragment>
                    );
                  }
                )}
              </Grid>
            </Grid>
          );
        })}
    </Grid>
  );
};
export default ConfigGlobalRenderer;
