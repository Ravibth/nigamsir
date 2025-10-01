import React, { useContext, useEffect, useState } from "react";
import CustomAccordian from "../custom-accordian/custom-accordian";
import { useForm } from "react-hook-form";
import { ConfigGroupEnum } from "../constant";
import {
  Autocomplete,
  Box,
  Grid,
  Tab,
  Tabs,
  TextField,
  Tooltip,
  tabsClasses,
} from "@mui/material";
import CustomTabPanel from "../custom-tab/custom-tab-panel";
import "./style.css";
import {
  GetDataForSystemSuggestedConfig,
  GetUpdatePayloadForSystemSuggestedConfig,
  getAllAttributTypes,
} from "../system-suggested-req/util";
import {
  UpdateConfiguration,
  getGonfigurationGroupByConfigGroupAndConfigType,
} from "../../../services/configuration-services/configuration.service";
import UpdateConfigModal from "../update-config-modal/update-config-modal";
import { InfoRounded } from "@mui/icons-material";

import groupBy from "lodash/groupBy";
import uniq from "lodash/uniq";
import RequisitionFormPerimeters from "./requisition-perimeters";
import Loader from "../../loader/loader";
import { SnackbarContext } from "../../../contexts/snackbarContext";

const RequisitionForm = (props: any) => {
  const { configurationType, handleIsEditable, setIsDirty } = props;
  const [panelValue, setPanelValue] = useState("");
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [groupedData, setGroupedData] = useState<any>();
  const [businessUnits, setBusinessUnits] = useState<any>();
  const [expertise, setExpertise] = useState<any>();
  const [selectedUnit, setSelectedUnit] = useState<any>();
  const snackbarContext: any = useContext(SnackbarContext);
  const [cancelOpen, setCancelOpen] = useState(false);
  const handleCancelOpen = () => setCancelOpen(true);
  const handleCancelClose = () => setCancelOpen(false);

  const [isOpen, setIsOpen] = useState(false);
  const [isEditable, setIsEditable] = useState(false);
  const { handleSubmit } = useForm();
  const [exData, setExData] = useState([]);
  const handleChange = (event: React.SyntheticEvent, newValue: string) => {
    setPanelValue(newValue);
  };
  const handleAccordianOpenClick = () => {
    setIsOpen(true);
  };
  const handleAccordianCloseClick = () => {
    setIsOpen(false);
  };
  const handleEditClick = () => {
    setIsDirty(true);
    setIsEditable(true);
  };
  const handleCancelClick = () => {
    handleCancelOpen();
    // GetDataHandler();
    // setIsEditable(false);
  };
  const handleSaveClick = () => {
    // setIsEditable(false);
    // console.log(exData);
    handleOpen();
  };
  useEffect(() => {
    handleIsEditable(isEditable);
  }, [isEditable]);

  const createHireachy = (allocationReviewData: any) => {
    if (props.buExpertiesList) {
      allocationReviewData.forEach((element: any) => {
        element["bu"] = props.buExpertiesList[element?.attributeName]
          ? props.buExpertiesList[element?.attributeName]
          : "";
      });
      const groupedData = groupBy(allocationReviewData, "bu");
      const keys = Object.keys(groupedData)?.filter(
        (group: string) => group?.trim() !== ""
      );
      // keys = [...keys, ...keys];
      setBusinessUnits(keys);
      setGroupedData(groupedData);
    }
  };

  const GetDataHandler = () => {
    Promise.all([
      GetRequisitionFormData(
        ConfigGroupEnum.REQUISITION_FORM_DB_GROUP.toString(),
        configurationType
      ),
    ])
      .then((values) => {
        const systemSuggestedConfigData: any = GetDataForSystemSuggestedConfig(
          values[0]
        );
        console.log(systemSuggestedConfigData);
        createHireachy(systemSuggestedConfigData);
        setExData(systemSuggestedConfigData);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const submitDataHandler = () => {
    const actualExData: any = exData?.filter(
      (item: any) =>
        item.attributeName.toString().toUpperCase().trim() !== "ALL"
    );
    const updatePayload = GetUpdatePayloadForSystemSuggestedConfig(
      actualExData,
      configurationType
    );
    console.log(updatePayload);
    Promise.all([UpdateSystemSuggestedConfigurationData(updatePayload)])
      .then((values) => {
        console.log(values[0]);
        setIsEditable(() => false);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const changeAcceptedHandlerForCancel = (status: boolean) => {
    if (status === true) {
      GetDataHandler();
      setIsEditable(false);
    }
  };
  const changeAcceptedHandler = (status: boolean) => {
    if (status === true) {
      submitDataHandler();
    }
  };
  const GetRequisitionFormData = (
    configGroup: string,
    configType: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      getGonfigurationGroupByConfigGroupAndConfigType(configGroup, configType)
        .then((resp) => {
          // console.log(resp.data);
          resolve(resp.data);
        })
        .catch((err) => {
          console.log(err);
          reject(err);
        });
    });
  };

  const UpdateSystemSuggestedConfigurationData = (updatePayload: any) => {
    return new Promise((resolve, reject) => {
      UpdateConfiguration(updatePayload)
        .then((resp: any) => {
          //console.log(resp.data);
          snackbarContext.displaySnackbar(
            "Configuration changes have been successfully updated",
            "success",
            6000
          );
          resolve(resp.data);
        })
        .catch((err) => {
          console.log(err);
          reject(err);
        });
    });
  };
  useEffect(() => {
    //console.log("System-suggest-main");
    GetDataHandler();
  }, [props]);

  const buSelection = (businessUnit: string) => {
    if (groupedData?.[businessUnit]?.length) {
      const experties = groupedData?.[businessUnit].map(
        (item: any) => item.attributeName
      );
      const unique = uniq(experties);
      if (unique?.length > 0) {
        setPanelValue(unique[0] as string);
      }
      setExpertise(unique);
    }
  };
  return (
    <div>
      <form onSubmit={handleSubmit(handleSaveClick)}>
        <CustomAccordian
          isOpen={isOpen}
          isEditable={isEditable}
          hideEdit={!(businessUnits && businessUnits.length > 0)}
          title={ConfigGroupEnum.REQUISITION_FORM.toString().trim()}
          configNote={props.configNote}
          handleAccordianOpenClick={handleAccordianOpenClick}
          handleAccordianCloseClick={handleAccordianCloseClick}
          handleEditClick={handleEditClick}
          handleCancelClick={handleCancelClick}
        >
          <UpdateConfigModal
            open={cancelOpen}
            type="cancel"
            handleCloseModal={handleCancelClose}
            changeAcceptedHandler={changeAcceptedHandlerForCancel}
            setIsDirty={setIsDirty}
          />
          <UpdateConfigModal
            open={open}
            // handleOpen={handleOpen}
            type="save"
            handleCloseModal={handleClose}
            changeAcceptedHandler={changeAcceptedHandler}
            setIsDirty={setIsDirty}
          />
          <Grid container alignItems={"center"} spacing={2}>
            <Grid item xs={9}>
              <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
                <Tabs
                  value={panelValue}
                  onChange={handleChange}
                  variant="scrollable"
                  scrollButtons
                  className="tabs-container"
                  // component={"div"}
                  aria-label="visible arrows tabs example"
                  sx={{
                    [`& .${tabsClasses.scrollButtons}`]: {
                      "&.Mui-disabled": { opacity: 0.3 },
                    },
                  }}
                >
                  {expertise?.map((item: any, index: number) => {
                    return (
                      <Tab className="tab-label" label={item} value={item} />
                    );
                  })}
                </Tabs>
              </Box>
            </Grid>
            <Grid item xs={3}>
              <div className="select-container">
                {businessUnits && businessUnits.length > 0 ? (
                  <Autocomplete
                    disablePortal
                    id="combo-box-demo"
                    value={selectedUnit}
                    // options={getAllAttributTypes(businessUnits)}
                    options={businessUnits}
                    sx={{
                      width: "90% !important",
                    }}
                    renderInput={(params) => (
                      <TextField {...params} label="Type And Select" />
                    )}
                    // size="small"
                    onInputChange={(data, newValue: any) => {
                      setSelectedUnit(newValue);
                      buSelection(newValue);
                      if (newValue === "Deals") {
                        setPanelValue("NDO");
                      }
                    }}
                    ListboxProps={{
                      style: {
                        maxHeight: "250px",
                      },
                    }}
                  />
                ) : (
                  <Loader small={true} />
                )}
              </div>
            </Grid>
          </Grid>

          {getAllAttributTypes(exData).map((item: any, index: number) => {
            console.log(item);
            return (
              <>
                <CustomTabPanel
                  key={index}
                  value={panelValue.toString().trim().toUpperCase()}
                  index={item.toString().trim().toUpperCase()}
                >
                  {item.toString().trim().toUpperCase() === "ALL" && (
                    <Tooltip
                      title="Modifications will impact All Tabs"
                      placement="right"
                    >
                      <InfoRounded className="information-icon" />
                    </Tooltip>
                  )}
                  <RequisitionFormPerimeters
                    suggestionPerimeter={exData}
                    currentAttribute={item}
                    setExData={setExData}
                    exData={exData}
                    isDisabled={!isEditable}
                    // control={control}
                    // setValue={setValue}
                    // getValues={getValues}
                    // watch={watch}
                  />
                </CustomTabPanel>
              </>
            );
          })}
        </CustomAccordian>
      </form>
    </div>
  );
};

export default RequisitionForm;
