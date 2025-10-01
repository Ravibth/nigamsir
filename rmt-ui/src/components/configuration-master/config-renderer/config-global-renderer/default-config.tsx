import { Box } from "@mui/material";
import { useForm } from "react-hook-form";
import {
  IConfigurationMaster,
  EKeySelectorsForMinBreakup,
  IConfigurationMainBreakupMetaValues,
} from "../../../../common/interfaces/IConfigurationMaster";
import ConfigGlobalRenderer from "./config-global-renderer";
import { useState } from "react";

interface IDefaultConfigProps {
  configurationGroupItem: IConfigurationMaster;
  addUpdateConfigBreakupMaster: (
    configurationMasterId: string,
    keySelector: string[],
    configurationMetaValues: IConfigurationMainBreakupMetaValues[],
    deleteMetaValues: boolean
  ) => Promise<boolean>;
  closeTheAccordion: () => void;
  selectorKey: string[];
}
const DefaultConfig = (props: IDefaultConfigProps) => {
  const {
    control,
    handleSubmit,
    getValues,
    formState: { errors, isDirty },
  } = useForm({
    mode: "onTouched",
  });

  const [anyChangesDetected, setAnyChangedDetected] = useState<boolean>(false);

  const submitDefaultConfig = (finalData: any) => {
    const checkIfSomeValuesHasChanged = true;
    if (checkIfSomeValuesHasChanged) {
      const updatedConfigurationMetaValues: IConfigurationMainBreakupMetaValues[] =
        props.configurationGroupItem.schemaValues.map((schemaItem) => {
          return {
            key: schemaItem.key,
            displayKey: schemaItem.keyDisplay,
            value: finalData[schemaItem.key] ?? "",
          };
        });

      props.addUpdateConfigBreakupMaster(
        props.configurationGroupItem.id,
        props.selectorKey,
        updatedConfigurationMetaValues,
        false
      );
    } else {
      props.closeTheAccordion();
    }
  };

  const checkForAnyChangesInTheForm = () => {
    const formData = getValues();
    const previousData =
      props.configurationGroupItem.configurationMainBreakups.find(
        (metaValueItem) =>
          EKeySelectorsForMinBreakup.DEFAULT === metaValueItem.keySelector
      );

    if (previousData) {
      const nonMatchedData =
        previousData.configurationMainBreakupMetaValues.some(
          (breakupItems) => breakupItems.value !== formData[breakupItems.key]
        );
      setAnyChangedDetected(nonMatchedData);
    }
  };

  return (
    <Box
      sx={{
        p: 2,
        backgroundColor: "white",
      }}
    >
      <form onSubmit={handleSubmit(submitDefaultConfig)}>
        <ConfigGlobalRenderer
          configurationGroupItem={props.configurationGroupItem}
          selectorKey={[EKeySelectorsForMinBreakup.DEFAULT]}
          control={control}
          errors={errors}
          getValues={getValues}
          anyChangesDetected={anyChangesDetected}
          checkForAnyChangesInTheForm={checkForAnyChangesInTheForm}
        />
      </form>
    </Box>
  );
};
export default DefaultConfig;
