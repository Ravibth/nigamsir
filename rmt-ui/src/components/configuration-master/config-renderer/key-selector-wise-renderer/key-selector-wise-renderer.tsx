import {
  IConfigurationMainBreakupMetaValues,
  IConfigurationMaster,
} from "../../../../common/interfaces/IConfigurationMaster";
import BuWiseSelectorRenderer from "./bu-wise-selector-renderer/bu-wise-selector-renderer";
import Grid from "@mui/material/Grid";
import { useState } from "react";
import SelectedBuGridRenderer from "./selected-bu-grid-renderer/selected-bu-grid-renderer";
import { IBusinessUnitUniqueConfigTree } from "../../configuration-master-utils";

interface IKeySelectorWiseRendererProps {
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
const KeySelectorWiseRenderer = (props: IKeySelectorWiseRendererProps) => {
  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <BuWiseSelectorRenderer
          configurationGroupItem={props.configurationGroupItem}
          addUpdateConfigBreakupMaster={props.addUpdateConfigBreakupMaster}
          closeTheAccordion={props.closeTheAccordion}
          buTreeMapping={props.buTreeMapping}
          setSelectedBusinessUnit={props.setSelectedBusinessUnit}
          selectedBusinessUnit={props.selectedBusinessUnit}
        />
      </Grid>
      <Grid item xs={12}>
        <SelectedBuGridRenderer
          selectedBusinessUnit={props.selectedBusinessUnit}
          configurationGroupItem={props.configurationGroupItem}
          addUpdateConfigBreakupMaster={props.addUpdateConfigBreakupMaster}
          closeTheAccordion={props.closeTheAccordion}
        />
      </Grid>
    </Grid>
  );
};
export default KeySelectorWiseRenderer;
