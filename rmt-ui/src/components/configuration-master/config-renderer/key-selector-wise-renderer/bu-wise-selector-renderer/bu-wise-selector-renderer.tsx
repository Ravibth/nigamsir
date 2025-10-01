import {
  EKeySelectorsForMinBreakup,
  IConfigurationMainBreakupMetaValues,
  IConfigurationMaster,
  EKeySelectorSeparator,
} from "../../../../../common/interfaces/IConfigurationMaster";
import { useForm } from "react-hook-form";
import { BuWiseFormControls } from "../utils";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import { ConfigurationMasterGroupLabelSxProps } from "../../../style";
import ActionButton from "../../../../actionButton/actionButton";
import { HorizontalRightAlignSxProps } from "../../../../common-allocation/user-info-timeline-group/style";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import ControllerAutoCompleteChipsSimple from "../../../../controllerInputs/controllerAutoCompleteChipsSimple";
import { useEffect, useState } from "react";
import ConfigGlobalRenderer from "../../config-global-renderer/config-global-renderer";
import { IBusinessUnitUniqueConfigTree } from "../../../configuration-master-utils";

interface IBuWiseSelectorRendererProps {
  configurationGroupItem: IConfigurationMaster;
  addUpdateConfigBreakupMaster: (
    configurationMasterId: string,
    keySelector: string[],
    configurationMetaValues: IConfigurationMainBreakupMetaValues[],
    deleteMetaValues: boolean
  ) => Promise<boolean>;
  closeTheAccordion: () => void;
  buTreeMapping: IBusinessUnitUniqueConfigTree[];
  selectedBusinessUnit: string;
  setSelectedBusinessUnit: React.Dispatch<React.SetStateAction<string>>;
}
const BuWiseSelectorRenderer = (props: IBuWiseSelectorRendererProps) => {
  const [offeringOptions, setOfferingOptions] = useState<string[]>([]);
  const [selectorKey, setSelectorKey] = useState<string[]>([]);
  const [anyChangesDetected, setAnyChangedDetected] = useState<boolean>(false);
  const {
    control,
    setValue,
    getValues,
    handleSubmit,
    watch,
    formState: { errors, isDirty },
  } = useForm({
    mode: "onTouched",
  });

  watch();

  const submitUpdatedConfig = (finalData: any) => {
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
        selectorKey,
        updatedConfigurationMetaValues,
        false
      );
    } else {
      props.closeTheAccordion();
    }
  };

  const getUniqueOfferingsByBu = (businessUnitSelected: string) => {
    let offerings: string[] = [];
    const selectedBuTree = props.buTreeMapping.find(
      (buItem) => buItem.bu.toLowerCase() === businessUnitSelected.toLowerCase()
    );
    const alreadySelectedOfferings =
      props.configurationGroupItem.configurationMainBreakups
        .filter(
          (item) =>
            item.keySelector !== EKeySelectorsForMinBreakup.DEFAULT &&
            item.keySelector.split(EKeySelectorSeparator).length > 1 &&
            item.keySelector.split(EKeySelectorSeparator)[0]?.toLowerCase() ===
              businessUnitSelected.toLowerCase()
        )
        .map((item) => item.keySelector.split(EKeySelectorSeparator)[1]);

    if (selectedBuTree) {
      selectedBuTree.offerings.forEach((offeringItem) => {
        if (!alreadySelectedOfferings.includes(offeringItem)) {
          offerings.push(offeringItem);
        }
      });
    }

    offerings = offerings.filter((a) => a != undefined || a != null);
    setOfferingOptions(offerings);
  };

  const refreshOfferingOptions = (businessUnitSelected: string) => {
    setValue(BuWiseFormControls.Offerings, []);
    if (businessUnitSelected) {
      getUniqueOfferingsByBu(businessUnitSelected);
    } else {
      setOfferingOptions([]);
    }
  };

  const checkForSelectorKey = () => {
    const buValue = getValues(BuWiseFormControls.BusinessUnit);
    const offeringsValue = getValues(BuWiseFormControls.Offerings);

    if (buValue && offeringsValue.length > 0) {
      const finalSelectors = offeringsValue.map(
        (offeringItem) => `${buValue?.bu}|${offeringItem}`
      );
      setSelectorKey(finalSelectors);
    } else {
      setSelectorKey([]);
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

  useEffect(() => {
    checkForSelectorKey();
  }, [
    getValues(BuWiseFormControls.BusinessUnit),
    getValues(BuWiseFormControls.Offerings),
  ]);

  useEffect(() => {
    if (props.selectedBusinessUnit) {
      setValue(
        BuWiseFormControls.BusinessUnit,
        props.buTreeMapping.find(
          (item) => item.bu === props.selectedBusinessUnit
        )
      );
      refreshOfferingOptions(props.selectedBusinessUnit);
    }
  }, []);

  return (
    <Box sx={{ p: 2, backgroundColor: "white" }}>
      <form onSubmit={handleSubmit(submitUpdatedConfig)}>
        <Grid container spacing={2}>
          <Grid item xs={11} sx={ConfigurationMasterGroupLabelSxProps}>
            BU Wise Configuration
          </Grid>
          <Grid item xs={1} sx={HorizontalRightAlignSxProps}>
            <ActionButton
              label={"Save"}
              onClick={() => {}}
              disabled={!anyChangesDetected || Object.keys(errors).length > 0}
              type={"submit"}
            />
          </Grid>
          <Grid item xs={2}>
            <ControllerAutoCompleteTextFieldWithGetOptionsLabel
              name={BuWiseFormControls.BusinessUnit}
              control={control}
              required={true}
              options={props.buTreeMapping}
              label="Business Unit"
              defaultValue={""}
              error={errors[BuWiseFormControls.BusinessUnit] ? true : false}
              onChange={() => {
                const businessUnitValue = getValues(
                  BuWiseFormControls.BusinessUnit
                );
                props.setSelectedBusinessUnit(
                  businessUnitValue ? businessUnitValue?.bu : ""
                );
                refreshOfferingOptions(
                  businessUnitValue ? businessUnitValue?.bu : ""
                );
              }}
              filterSelectedOptions={true}
              getOptionLabel={(option: IBusinessUnitUniqueConfigTree) =>
                option.bu
              }
              isOptionEqualToValue={(
                option: IBusinessUnitUniqueConfigTree,
                value: IBusinessUnitUniqueConfigTree
              ) => option?.bu === value?.bu}
            />
          </Grid>
          <Grid item xs={4}>
            <ControllerAutoCompleteChipsSimple
              name={BuWiseFormControls.Offerings}
              control={control}
              required={true}
              defaultValue={[]}
              options={offeringOptions}
              label="Offerings"
              error={errors[BuWiseFormControls.Offerings] ? true : false}
              onChange={() => {}}
              multiple={true}
              sortBy={(option: any) => {
                return option?.sort((a, b) => (`${a}` > `${b}` ? 1 : -1));
              }}
            />
          </Grid>
          <Grid item xs={6} />
          <Grid item xs={12}>
            {selectorKey.length > 0 && (
              <ConfigGlobalRenderer
                configurationGroupItem={props.configurationGroupItem}
                selectorKey={selectorKey}
                control={control}
                errors={errors}
                getValues={getValues}
                anyChangesDetected={anyChangesDetected}
                checkForAnyChangesInTheForm={checkForAnyChangesInTheForm}
              />
            )}
          </Grid>
        </Grid>
      </form>
    </Box>
  );
};
export default BuWiseSelectorRenderer;
