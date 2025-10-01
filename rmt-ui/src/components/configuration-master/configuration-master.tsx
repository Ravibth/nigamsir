import { Typography, Grid } from "@mui/material";
import { RequisitionHeaderSxProps } from "../create-requisition-main/constant";
import Accordion from "@mui/material/Accordion";
import AccordionSummary from "@mui/material/AccordionSummary";
import AccordionDetails from "@mui/material/AccordionDetails";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";
import ExpandCircleDownIcon from "@mui/icons-material/ExpandCircleDown";
import {
  ConfigurationMasterGroupLabelSxProps,
  ConfigurationAccordionStyleSxProps,
} from "./style";
import InfoIconWithTooltip from "../../common/info-icon-with-tooltip/info-icon-with-tooltip";
import ConfigRenderer from "./config-renderer/config-renderer";
import { useContext, useEffect, useState } from "react";
import {
  getAllBusinessMaster,
  GetConfigurationMasterList,
} from "../../services/configuration-services/configuration.service";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../contexts/snackbarContext";
import {
  EConfigurationMasterGroups,
  IConfigurationMaster,
  EConfigTypeEnum,
} from "../../common/interfaces/IConfigurationMaster";
import {
  getFlattenedBuTreeMapping,
  IBusinessUnitUniqueConfigTree,
} from "./configuration-master-utils";

const ConfigurationMaster = () => {
  const [selectedConfigurationAccordion, setSelectedConfigurationAccordion] =
    useState<EConfigurationMasterGroups | string>("");
  const [selectedBusinessUnit, setSelectedBusinessUnit] = useState<string>("");

  const [configurationsMaster, setConfigurationMaster] = useState<
    IConfigurationMaster[]
  >([]);
  const [configTypeToUse, setConfigTypeToUse] = useState<EConfigTypeEnum>(
    EConfigTypeEnum.OFFERINGS
  );
  const [buTreeMapping, setBuTreeMapping] = useState<
    IBusinessUnitUniqueConfigTree[]
  >([]);

  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);

  const refreshConfigScreen = async (): Promise<boolean> => {
    loaderContext.open(true);
    return await Promise.all([
      fetchAllConfigurationMaster(),
      fetchBuTreeMappingMaster(),
    ])
      .then(() => {
        loaderContext.open(false);
        return true;
      })
      .catch(() => {
        loaderContext.open(false);
        return true;
      });
  };

  const handleAccordionChange =
    (panel: EConfigurationMasterGroups) =>
    (event: React.SyntheticEvent, newExpanded: boolean) => {
      setSelectedConfigurationAccordion(newExpanded ? panel : "");
    };

  const fetchAllConfigurationMaster = async (): Promise<
    IConfigurationMaster[]
  > => {
    return new Promise<IConfigurationMaster[]>((resolve, reject) => {
      GetConfigurationMasterList()
        .then((configResp) => {
          setConfigurationMaster(() => []);
          setTimeout(() => {
            setConfigurationMaster(() => configResp);
          }, 100);
          resolve(configResp);
        })
        .catch((err) => {
          snackbarContext.displaySnackbar(
            "Error fetching configurations",
            SnackbarSeverity.ERROR
          );
          resolve([]);
        });
    });
  };

  const fetchBuTreeMappingMaster = async (): Promise<
    IBusinessUnitUniqueConfigTree[]
  > => {
    return new Promise<IBusinessUnitUniqueConfigTree[]>((resolve, reject) => {
      getAllBusinessMaster()
        .then((buTree) => {
          const flattenBuTree = getFlattenedBuTreeMapping(buTree.data);
          setBuTreeMapping(flattenBuTree);
          resolve(flattenBuTree);
        })
        .catch((err) => {
          snackbarContext.displaySnackbar(
            "Error fetching Business Units Data",
            SnackbarSeverity.ERROR
          );
          resolve([]);
        });
    });
  };

  useEffect(() => {
    refreshConfigScreen();
  }, []);

  return (
    <>
      <Typography
        component={"div"}
        className="skill-master-heading requisition-header-title"
      >
        <Typography component={"span"} sx={RequisitionHeaderSxProps}>
          Configuration
        </Typography>
      </Typography>
      <Grid container spacing={2} sx={ConfigurationAccordionStyleSxProps}>
        {configurationsMaster.map((configurationGroupItem) => {
          return (
            <Grid item xs={12} key={configurationGroupItem.configGroup}>
              <Accordion
                sx={{
                  backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor2,
                }}
                slotProps={{ transition: { unmountOnExit: true } }}
                onChange={handleAccordionChange(
                  configurationGroupItem.configGroup
                )}
                expanded={
                  selectedConfigurationAccordion ===
                  configurationGroupItem.configGroup
                }
                //Check with aayush /depesh > Temp Added to build package
                component={null}
              >
                <AccordionSummary
                  expandIcon={
                    <ExpandCircleDownIcon
                      fontSize={"large"}
                      sx={{
                        color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
                      }}
                    />
                  }
                  aria-controls="panel1-content"
                  id="panel1-header"
                >
                  <Typography
                    component="span"
                    sx={ConfigurationMasterGroupLabelSxProps}
                  >
                    {configurationGroupItem.configGroupDisplay}
                  </Typography>
                  <Typography component="span">
                    <InfoIconWithTooltip
                      title={configurationGroupItem.configNote}
                    />
                  </Typography>
                </AccordionSummary>
                <AccordionDetails>
                  <ConfigRenderer
                    configurationGroupItem={configurationGroupItem}
                    configTypeToUse={configTypeToUse}
                    setSelectedConfigurationAccordion={
                      setSelectedConfigurationAccordion
                    }
                    refreshConfigScreen={refreshConfigScreen}
                    buTreeMapping={buTreeMapping}
                    selectedBusinessUnit={selectedBusinessUnit}
                    setSelectedBusinessUnit={setSelectedBusinessUnit}
                  />
                </AccordionDetails>
              </Accordion>
            </Grid>
          );
        })}
      </Grid>
    </>
  );
};
export default ConfigurationMaster;
