import React, { useContext, useEffect, useState } from "react";
import CustomAccordian from "../custom-accordian/custom-accordian";
import { useForm } from "react-hook-form";
import { Divider, Grid, Tooltip } from "@mui/material";
import * as util from "./util";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";
import { ConfigGroupEnum } from "../constant";
import {
  UpdateConfiguration,
  getGonfigurationGroupByConfigGroupAndConfigType,
} from "../../../services/configuration-services/configuration.service";
import ControllerSwitch from "../../controllers/controller-switch";
import "./style.css";
import UpdateConfigModal from "../update-config-modal/update-config-modal";

import groupBy from "lodash/groupBy";
import { TreeView } from "@mui/x-tree-view/TreeView";
import { TreeItem } from "@mui/x-tree-view/TreeItem";
import TreeViewConfig from "../tree-view-config/treeviewConfig";
import "../tree-view-config/treeview.css";

import {
  MinusSquare,
  PlusSquare,
  CloseSquare,
  StyledTreeItem,
  // @ts-ignore
} from "../../treeviewComponent/treeViewComponent.tsx";
import { SnackbarContext } from "../../../contexts/snackbarContext";

const PreferenceScreenParameterConfig = (props: any) => {
  const { configurationType, handleIsEditable, setIsDirty } = props;
  const [isOpen, setIsOpen] = useState(false);
  const [isEditable, setIsEditable] = useState(false);
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [groupedData, setGroupedData] = useState<any>();
  const [businessUnits, setBusinessUnits] = useState<any>();

  const {
    control,
    watch,
    getValues,
    setValue,
    handleSubmit,
    clearErrors,
    setError,
    trigger,
    formState: { errors, isDirty },
  } = useForm({
    mode: "onTouched",
  });
  const [exData, setExData] = useState([]);
  const [allData, setAllData] = useState("");
  const [isAll, setIsAll] = useState(false);
  const [cancelOpen, setCancelOpen] = useState(false);
  const snackbarContext: any = useContext(SnackbarContext);
  const handleCancelOpen = () => setCancelOpen(true);
  const handleCancelClose = () => setCancelOpen(false);

  useEffect(() => {
    GetDataHandler();
    //console.log("---------GetDataHandler-------------------");
    // }, [props]);
  }, [props.configGroup]);

  useEffect(() => {
    setValue("All", allData);
  }, [allData]);

  useEffect(() => {
    setValue("IsAll", isAll);
  }, [isAll]);

  useEffect(() => {
    //console.log(exData);
    if (exData && exData.length > 0) {
      exData.map((item: any) => {
        setValue(item.attributeName, item.attributeValue);
      });
    }
  }, [exData]);

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
        setValue(props.fieldName, dbData[0].allValue);
        const marketplaceData: any =
          util.GetDataForEmployeeMaxNoOfPreference(dbData);
        createHireachy(marketplaceData);
        if (marketplaceData && marketplaceData.length > 0) {
          setAllData(marketplaceData[0].allValue);
          setIsAll(marketplaceData[0].isAll);
          // setIsEditable(false);

          setExData(marketplaceData);
        }
      })
      .catch((ex) => {
        console.log(ex);
      });
  };
  useEffect(() => {
    handleIsEditable(isEditable);
  }, [isEditable]);

  const submitDataHandler = () => {
    const updatePayload: any = util.GetUpdatePayload(exData, configurationType);
    Promise.all([UpdateProjectConfiguration(updatePayload)])
      .then((values) => {
        let data = values[0];
        setIsEditable(() => false);
      })
      .catch((err) => {
        console.log(err);
      });
  };
  const handleSaveClick = (data: any) => {
    handleOpen();
  };
  const UpdateProjectConfiguration = (data: any): Promise<any> => {
    return new Promise((resolve, reject) => {
      UpdateConfiguration(data)
        .then((resp) => {
          console.log(resp.data);
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
          <Grid item xs={5}>
            <div className="config-container main-preference-container-config">
              <Grid container alignItems={"center"}>
                <Grid item xs={3}>
                  <div className="label-cofig"> {props.fieldName}</div>
                </Grid>

                <Grid item xs={1.5} className="config-pre-textfield">
                  <ControllerNumberTextField
                    name={props.fieldName}
                    control={control}
                    label={""}
                    isUpDownNeeded={true}
                    min={props.min}
                    max={props.max}
                    size={"small"}
                    sx={{ width: "95% !important", margin: "20% !important" }}
                    required={isEditable}
                    error={errors[props.fieldName] ? true : false}
                    onChange={(data: any) => {
                      if (data.target.value?.trim() != "") {
                        console.log(data.target.value);
                        const temp: any = exData;
                        const modifiedAllExData = temp.map((i: any) => {
                          return {
                            ...i,
                            isAll: true,
                            allValue: data.target.value,
                            attributeValue: data.target.value,
                          };
                        });
                        setExData(modifiedAllExData);
                        setAllData(data.target.value);
                      }
                    }}
                    disabled={!isEditable}
                  />
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

export default PreferenceScreenParameterConfig;
