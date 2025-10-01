import { useContext } from "react";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../../contexts/snackbarContext";
import Grid from "@mui/material/Grid";
import {
  IConfigurationMaster,
  EConfigTypeEnum,
  IConfigurationMainBreakupMetaValues,
  EKeySelectorsForMinBreakup,
} from "../../../common/interfaces/IConfigurationMaster";
import KeySelectorWiseRenderer from "./key-selector-wise-renderer/key-selector-wise-renderer";
import {
  AddUpdateConfigurationMasterList,
  IAddUpdateConfigurationMasterListRequestPayload,
} from "../../../services/configuration-services/configuration.service";
import DefaultConfig from "./config-global-renderer/default-config";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../contexts/loaderContext";
import { IBusinessUnitUniqueConfigTree } from "../configuration-master-utils";

interface IConfigRendererProps {
  configurationGroupItem: IConfigurationMaster;
  configTypeToUse: EConfigTypeEnum;
  setSelectedConfigurationAccordion: React.Dispatch<
    React.SetStateAction<string>
  >;
  buTreeMapping: IBusinessUnitUniqueConfigTree[];
  refreshConfigScreen: () => Promise<boolean>;
  selectedBusinessUnit: string;
  setSelectedBusinessUnit: React.Dispatch<React.SetStateAction<string>>;
}

const ConfigRenderer = (props: IConfigRendererProps) => {
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);

  const closeTheAccordion = () => {
    props.setSelectedConfigurationAccordion("");
  };

  const addUpdateConfigBreakupMaster = (
    configurationMasterId: string,
    keySelector: string[],
    configurationMetaValues: IConfigurationMainBreakupMetaValues[],
    deleteMetaValues: boolean
  ): Promise<boolean> => {
    loaderContext.open(true);
    const payload: IAddUpdateConfigurationMasterListRequestPayload[] =
      keySelector.map((keySelectedItem) => {
        return {
          configurationMasterId: configurationMasterId,
          keySelector: keySelectedItem,
          configurationMetaValues: configurationMetaValues,
          isActive: !deleteMetaValues,
        };
      });
    return new Promise<boolean>((resolve, reject) => {
      //console.log("config payload", payload);
      AddUpdateConfigurationMasterList(payload)
        .then(async (e) => {
          snackbarContext.displaySnackbar(
            "Configuration updated successfully",
            SnackbarSeverity.SUCCESS
          );
          await props.refreshConfigScreen();
          loaderContext.open(false);
          resolve(true);
        })
        .catch(() => {
          snackbarContext.displaySnackbar(
            "Error updating configuration",
            SnackbarSeverity.ERROR
          );
          loaderContext.open(false);
          resolve(false);
        });
    });
  };

  return (
    <Grid container spacing={2}>
      {props.configurationGroupItem.globalDefaultDisplay && (
        <Grid item xs={12}>
          <DefaultConfig
            configurationGroupItem={props.configurationGroupItem}
            addUpdateConfigBreakupMaster={addUpdateConfigBreakupMaster}
            closeTheAccordion={closeTheAccordion}
            selectorKey={[EKeySelectorsForMinBreakup.DEFAULT]}
          />
        </Grid>
      )}
      {props.configurationGroupItem.selectorWiseDisplay && (
        <Grid item xs={12}>
          <KeySelectorWiseRenderer
            configurationGroupItem={props.configurationGroupItem}
            addUpdateConfigBreakupMaster={addUpdateConfigBreakupMaster}
            closeTheAccordion={closeTheAccordion}
            buTreeMapping={props.buTreeMapping}
            selectedBusinessUnit={props.selectedBusinessUnit}
            setSelectedBusinessUnit={props.setSelectedBusinessUnit}
          />
        </Grid>
      )}
    </Grid>
  );
};
export default ConfigRenderer;
