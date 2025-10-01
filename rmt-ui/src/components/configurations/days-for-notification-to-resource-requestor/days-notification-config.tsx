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
import UpdateConfigModal from "../update-config-modal/update-config-modal";
import "./style.css";
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

const DaysNotificationConfig = (props: any) => {
  const { configurationType, handleIsEditable } = props;
  const [isOpen, setIsOpen] = useState(false);
  const [isEditable, setIsEditable] = useState(false);
  const [cancelOpen, setCancelOpen] = useState(false);
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const handleCancelOpen = () => setCancelOpen(true);
  const handleCancelClose = () => setCancelOpen(false);
  const [groupedData, setGroupedData] = useState<any>();
  const [businessUnits, setBusinessUnits] = useState<any>();
  const snackbarContext: any = useContext(SnackbarContext);
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
  const handleAccordianOpenClick = () => {
    setIsOpen(true);
  };
  const handleAccordianCloseClick = () => {
    setIsOpen(false);
  };
  const handleEditClick = () => {
    setIsEditable(true);
  };
  const handleCancelClick = () => {
    handleCancelOpen();
    // setIsEditable(false);
    // GetDataHandler();
  };
  useEffect(() => {
    handleIsEditable(isEditable);
  }, [isEditable]);
  const GetDataHandler = () => {
    Promise.all([
      GetGonfigurationGroupByConfigGroupAndConfigType(
        ConfigGroupEnum.DAYS_NOTIFICATION_FOR_RESOURCE_REQUESTOR_DB_GROUP.toString().toUpperCase(),
        configurationType
      ),
    ])
      .then((values) => {
        const dbData = values[0];
        // console.log(dbData);
        const marketplaceData: any = util.GetDataForNotification(dbData);
        // console.log(marketplaceData);
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

  const submitDataHandler = () => {
    const updatePayload: any = util.GetUpdatePayload(exData, configurationType);
    // console.log(updatePayload);
    Promise.all([UpdateProjectConfiguration(updatePayload)])
      .then((values) => {
        let data = values[0];
      })
      .catch((err) => {
        console.log(err);
      });
  };
  const changeAcceptedHandlerForCancel = (status: boolean) => {
    if (status === true) {
      GetDataHandler();
      setIsEditable(false);
      businessUnits?.map((item: any, index: number) => {
        setValue(item + "AllDays", "");
      });
    }
  };
  const changeAcceptedHandler = (status: boolean) => {
    if (status === true) {
      submitDataHandler();
      setIsEditable(() => false);
      businessUnits?.map((item: any, index: number) => {
        setValue(item + "AllDays", "");
      });
    }
  };
  const handleSaveClick = (data: any) => {
    // console.log(data);
    // console.log(exData);
    handleOpen();
  };
  const UpdateProjectConfiguration = (data: any): Promise<any> => {
    return new Promise((resolve, reject) => {
      UpdateConfiguration(data)
        .then((resp) => {
          // console.log(resp.data);
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
  const GetGonfigurationGroupByConfigGroupAndConfigType = (
    configGroup: string,
    configType: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      getGonfigurationGroupByConfigGroupAndConfigType(configGroup, configType)
        .then((resp: any) => {
          // console.log("Promise Response ", resp);
          resolve(resp.data);
        })
        .catch((err) => {
          //console.log("Promise Error ", err);
          reject(err);
        });
    });
  };
  useEffect(() => {
    // Promise.all([
    //   GetGonfigurationGroupByConfigGroupAndConfigType(
    //     ConfigGroupEnum.DAYS_NOTIFICATION_FOR_RESOURCE_REQUESTOR_DB_GROUP.toString().toUpperCase(),
    //     configurationType
    //   ),
    // ])
    //   .then((values) => {
    //     const dbData = values[0];
    //     console.log(dbData);
    //     const marketplaceData: any = util.GetDataForNotification(dbData);
    //     console.log(marketplaceData);
    //     if (marketplaceData && marketplaceData.length > 0) {
    //       setAllData(marketplaceData[0].allValue);
    //       setIsAll(marketplaceData[0].isAll);
    //       setExData(marketplaceData);
    //       setIsEditable(false);
    //     }
    //   })
    //   .catch((ex) => {
    //     console.log(ex);
    //   });
    GetDataHandler();
  }, [props]);
  useEffect(() => {
    setValue("All", allData);
  }, [allData]);
  useEffect(() => {
    setValue("IsAll", isAll);
  }, [isAll]);
  useEffect(() => {
    // console.log(exData);
    if (exData && exData.length > 0) {
      exData.map((item: any) => {
        setValue(item.attributeName, item.attributeValue);
      });
    }
  }, [exData]);
  return (
    <form onSubmit={handleSubmit(handleSaveClick)}>
      <CustomAccordian
        title={ConfigGroupEnum.DAYS_NOTIFICATION_FOR_RESOURCE_REQUESTOR.toString().trim()}
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
        />
        <UpdateConfigModal
          open={open}
          // handleOpen={handleOpen}
          type="save"
          handleCloseModal={handleClose}
          changeAcceptedHandler={changeAcceptedHandler}
        />

        {/* <br />
        <Divider /> */}
        <br />

        <Grid container spacing={2}>
          {businessUnits?.map((item: any, index: number) => {
            return (
              <Grid item xs={5} key={index}>
                <Grid container alignItems={"center"}>
                  <TreeView
                    defaultCollapseIcon={<MinusSquare />}
                    defaultExpandIcon={<PlusSquare />}
                    defaultEndIcon={<CloseSquare />}
                  >
                    <div className="config-container">
                      <TreeItem
                        nodeId={index.toString()}
                        label={
                          <div>
                            <Grid container alignItems={"center"}>
                              <div className="config-bu-name"> {item} </div>

                              <Grid
                                item
                                xs={3}
                                className="config-textfield-colr"
                              >
                                <ControllerNumberTextField
                                  name={item + "AllDays"}
                                  control={control}
                                  label={""}
                                  isUpDownNeeded={false}
                                  min={0}
                                  size={"small"}
                                  sx={{ width: "82% !important" }}
                                  required={isEditable}
                                  error={
                                    errors[item + "AllDays"] ? true : false
                                  }
                                  onChange={(data: any) => {
                                    if (data.target.value?.trim() != "") {
                                      console.log(data.target.value);
                                      const temp: any = exData;
                                      const modifiedAllExData = temp.map(
                                        (i: any) => {
                                          if (i?.bu == item) {
                                            return {
                                              ...i,
                                              // isUpdated: true,
                                              isAll: true,
                                              allValue: data.target.value,
                                              attributeValue: data.target.value,
                                            };
                                          } else {
                                            return i;
                                          }
                                        }
                                      );
                                      setExData(modifiedAllExData);
                                      setAllData(data.target.value);
                                    }
                                  }}
                                  disabled={!isEditable}
                                />
                              </Grid>
                            </Grid>
                          </div>
                        }
                        className="bu-name"
                      >
                        {groupedData[item]?.map(
                          (expertise: any, index: number) => {
                            return (
                              <div className="config-name">
                                <Grid item xs={8}>
                                  {expertise.attributeName}
                                </Grid>

                                <Grid item xs={3}>
                                  <ControllerNumberTextField
                                    name={expertise.attributeName}
                                    control={control}
                                    label={""}
                                    isUpDownNeeded={false}
                                    size="small"
                                    min={0}
                                    required={true}
                                    sx={{ width: "82% !important" }}
                                    error={
                                      errors[expertise.attributeName]
                                        ? true
                                        : false
                                    }
                                    onChange={(data: any) => {
                                      // console.log(data.target.value);
                                      const temp: any = exData;
                                      const dataIndex = temp.findIndex(
                                        (prop: any) =>
                                          prop.configKey ===
                                            expertise.configKey &&
                                          prop.attributeName ===
                                            expertise.attributeName
                                        // prop.id === item.id
                                      );
                                      temp[dataIndex].attributeValue =
                                        data.target.value;
                                      // temp[dataIndex].isUpdated = true;
                                      setExData(temp);
                                    }}
                                    disabled={!isEditable}
                                  />
                                </Grid>
                              </div>
                            );
                          }
                        )}
                      </TreeItem>
                    </div>
                  </TreeView>
                </Grid>
              </Grid>
            );
          })}
        </Grid>
      </CustomAccordian>
    </form>
  );
};

export default DaysNotificationConfig;
