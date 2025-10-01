import React, { useContext, useEffect, useState } from "react";
import CustomAccordian from "../custom-accordian/custom-accordian";
import { Grid } from "@mui/material";
import { useForm } from "react-hook-form";
import UpdateConfigModal from "../update-config-modal/update-config-modal";
import { ToggleSwitch } from "../requisition-form/toggleSwitch";
import {
  UpdateConfiguration,
  getGonfigurationGroupByConfigGroupAndConfigType,
} from "../../../services/configuration-services/configuration.service";
import * as util from "./util";
import groupBy from "lodash/groupBy";
import { LoaderContext } from "../../../contexts/loaderContext";
import { SnackbarContext } from "../../../contexts/snackbarContext";

const PreferenceScreenToggleParameters = (props: any) => {
  const { configurationType, handleIsEditable, setIsDirty } = props;
  const [isOpen, setIsOpen] = useState(false);
  const [isEditable, setIsEditable] = useState(false);
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [groupedData, setGroupedData] = useState<any>();
  const [businessUnits, setBusinessUnits] = useState<any>();

  const { setValue, handleSubmit } = useForm({
    mode: "onTouched",
  });

  const [exData, setExData] = useState([]);
  const [allData, setAllData] = useState<boolean>(false);
  const [isAll, setIsAll] = useState(false);
  const [cancelOpen, setCancelOpen] = useState(false);
  const handleCancelOpen = () => setCancelOpen(true);
  const handleCancelClose = () => setCancelOpen(false);
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);

  useEffect(() => {
    GetDataHandler();
  }, [props]);

  useEffect(() => {
    setValue("All", allData);
  }, [allData]);

  useEffect(() => {
    setValue("IsAll", isAll);
  }, [isAll]);

  useEffect(() => {
    console.log(exData);
    if (exData && exData.length > 0) {
      exData.map((item: any) => {
        setValue(item.attributeName, item.attributeValue);
      });
    }
  }, [exData]);

  const handleAccordianCloseClick = () => {
    setIsOpen(false);
  };

  const handleAccordianOpenClick = () => {
    setIsOpen(true);
  };

  const handleCancelClick = () => {
    handleCancelOpen();
  };
  const handleEditClick = () => {
    setIsDirty(true);
    setIsEditable(true);
  };

  const handleSaveClick = (data: any) => {
    handleOpen();
  };

  const createHireachy = (allocationReviewData: any) => {
    if (props.buExpertiesList) {
      allocationReviewData.forEach((element: any) => {
        element["bu"] = props.buExpertiesList[element?.attributeName]
          ? props.buExpertiesList[element?.attributeName]
          : "";
      });
      const groupedData = groupBy(allocationReviewData, "bu");
      const keys = Object.keys(groupedData);
      // keys = [...keys, ...keys];
      setBusinessUnits(keys);
      setGroupedData(groupedData);
    }
  };

  const GetDataHandler = () => {
    Promise.all([
      GetGonfigurationGroupByConfigGroupAndConfigType(
        props.configGroup,
        configurationType
      ),
    ])
      .then((values) => {
        const dbData = values[0];

        console.log(dbData[0].allValue);
        setValue(props.fieldName, dbData[0].allValue);
        const marketplaceData: any =
          util.GetDataForEmployeeMaxNoOfPreference(dbData);
        console.log(marketplaceData);
        createHireachy(marketplaceData);
        if (marketplaceData && marketplaceData.length > 0) {
          setAllData(marketplaceData[0].allValue);
          setIsAll(marketplaceData[0].isAll);
          setExData(marketplaceData);
          // setIsEditable(false);
        }
      })
      .catch((ex) => {
        console.log(ex);
      });
  };

  const changeAcceptedHandlerForCancel = (status: boolean) => {
    if (status === true) {
      GetDataHandler();
      setIsEditable(false);
    }
  };
  useEffect(() => {
    handleIsEditable(isEditable);
  }, [isEditable]);

  const submitDataHandler = () => {
    const updatePayload: any = util.GetUpdatePayload(exData, configurationType);
    //console.log("updatePayload", updatePayload);
    Promise.all([UpdateProjectConfiguration(updatePayload)])
      .then((values) => {
        let data = values[0];
        setIsEditable(() => false);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const UpdateProjectConfiguration = (data: any): Promise<any> => {
    return new Promise((resolve, reject) => {
      UpdateConfiguration(data)
        .then((resp) => {
          //console.log(resp.data);
          resolve(resp.data);
        })
        .catch((err) => {
          console.log(err);
          reject(err);
        });
    });
  };

  const changeAcceptedHandler = (status: boolean) => {
    if (status === true) {
      submitDataHandler();
    }
  };

  const GetGonfigurationGroupByConfigGroupAndConfigType = (
    configGroup: string,
    configType: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      getGonfigurationGroupByConfigGroupAndConfigType(configGroup, configType)
        .then((resp: any) => {
          //console.log("Promise Response ", resp);
          resolve(resp.data);
        })
        .catch((err) => {
          console.log("Promise Error ", err);
          reject(err);
        });
    });
  };

  const handleToggleChange = (newValue) => {
    // Update the state
    const temp = [...exData];
    const modifiedAllExData = temp.map((item) => ({
      ...item,
      isAll: true,
      allValue: newValue,
      attributeValue: newValue,
    }));
    setExData(modifiedAllExData);
    setAllData(newValue);

    // Call the API to save the data
    saveData(modifiedAllExData);
  };

  const saveData = (data) => {
    const updatePayload = util.GetUpdatePayload(data, configurationType);
    loaderContext.open(true);

    UpdateProjectConfiguration(updatePayload)
      .then(() => {
        // Update successful
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Configuration changes have been successfully updated",
          "success",
          6000
        );
      })
      .catch((error) => {
        // Handle error
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Error in Updated Configurations",
          "error",
          6000
        );
      });
  };

  return (
    <form onSubmit={handleSubmit(handleSaveClick)}>
      <CustomAccordian
        title={props.title}
        configNote={props.configNote}
        isOpen={isOpen}
        isEditable={isEditable}
        handleAccordianCloseClick={handleAccordianCloseClick}
        handleAccordianOpenClick={handleAccordianOpenClick}
        handleCancelClick={handleCancelClick}
        // handleSaveClick={handleSaveClick}
        handleEditClick={handleEditClick}
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
          //   handleOpen={handleOpen}
          type="save"
          handleCloseModal={handleClose}
          changeAcceptedHandler={changeAcceptedHandler}
          setIsDirty={setIsDirty}
        />
        <Grid container spacing={2}>
          <Grid item xs={6}>
            <div className="config-container main-preference-container-config main-el-container-config ">
              <Grid container alignItems={"center"}>
                <Grid item xs={7}>
                  <div className="label-cofig"> {props.fieldName}</div>
                </Grid>

                <Grid item xs={1.5}>
                  <ToggleSwitch
                    checked={allData}
                    // disabled={!isEditable}
                    onChange={handleToggleChange}
                  >
                    Toggle switch
                  </ToggleSwitch>
                </Grid>
              </Grid>
            </div>
            {/* </TreeView> */}
          </Grid>
        </Grid>
      </CustomAccordian>
    </form>
  );
};

export default PreferenceScreenToggleParameters;
