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

const EmployeePreferenceMaxItemConfig = (props: any) => {
  const { configurationType, handleIsEditable } = props;
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
  const handleCancelOpen = () => setCancelOpen(true);
  const handleCancelClose = () => setCancelOpen(false);
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
    // console.log(exData);
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
        // console.log(dbData);
        const marketplaceData: any =
          util.GetDataForEmployeeMaxNoOfPreference(dbData);
        // console.log(marketplaceData);
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
    // console.log(updatePayload);
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
    console.log(exData);
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
      businessUnits?.map((item: any, index: number) => {
        setValue(item + "AllDays", "");
      });
    }
  };

  const changeAcceptedHandler = (status: boolean) => {
    if (status === true) {
      submitDataHandler();
      businessUnits?.map((item: any, index: number) => {
        setValue(item + "AllDays", "");
      });
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
        />
        <UpdateConfigModal
          open={open}
          //   handleOpen={handleOpen}
          type="save"
          handleCloseModal={handleClose}
          changeAcceptedHandler={changeAcceptedHandler}
        />
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
                                  // sx={{ width: "60% !important" }}
                                  required={!isEditable}
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
                                    // sx={{ width: "60% !important" }}
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
        {/* <Grid container justifyContent={"space-between"}>
          <Grid item xs={4}>
            <Grid container alignItems={"center"}>
              <Grid item xs={8}>
                <div className="config-name">ALL</div>
              </Grid>
              <Grid item xs={3}>
                <ControllerNumberTextField
                  name={"All"}
                  control={control}
                  label={""}
                  size="small"
                  sx={{ width: "55% !important" }}
                  isUpDownNeeded={false}
                  min={0}
                  onBlur={true}
                  required={true}
                  error={errors["All"] ? true : false}
                  onChange={(data: any) => {
                    // console.log(data.target.value);
                    const temp: any = exData;
                    const updatedExData = temp.map((item: any) => {
                      return {
                        ...item,
                        // isUpdated: true,
                        isAll: true,
                        allValue: data.target.value,
                        attributeValue: data.target.value,
                      };
                    });
                    setExData(updatedExData);
                    setAllData(data.target.value);
                  }}
                  disabled={!isEditable || !isAll}
                />
              </Grid>
            </Grid>
          </Grid>
          <Grid item>
            <Tooltip
              title={
                getValues("IsAll")
                  ? "Enable Individual Modifications"
                  : "Apply For all"
              }
            >
              <div>
                <ControllerSwitch
                  name={"IsAll"}
                  control={control}
                  defaultChecked={false}
                  className={"all-activation-switch"}
                  disabled={isEditable ? false : true}
                  onChange={(value: any) => {
                    // console.log(value);
                    trigger();
                    const temp: any = exData;
                    const updatedExData = temp.map((item: any) => {
                      if (value) {
                        return {
                          ...item,
                          allValue: allData,
                          isAll: true,
                          // isUpdated: true,
                          attributeValue: allData,
                        };
                      } else {
                        return {
                          ...item,
                          isAll: false,
                        };
                      }
                    });
                    setExData(updatedExData);

                    setIsAll(value);
                  }}
                />
              </div>
            </Tooltip>
          </Grid>
        </Grid>
        <br />
        <Divider />
        <br />
        <Grid container spacing={2}>
          {exData.map((item: any, index: number) => {
            return (
              <Grid item xs={4} key={index}>
                <Grid container alignItems={"center"}>
                  <Grid item xs={8.1}>
                    <div className="config-name">{item.attributeName}</div>
                  </Grid>
                  <Grid item xs={3}>
                    <ControllerNumberTextField
                      name={item.attributeName}
                      control={control}
                      label={""}
                      isUpDownNeeded={false}
                      size="small"
                      min={0}
                      required={true}
                      sx={{ width: "60% !important" }}
                      error={errors[item.attributeName] ? true : false}
                      onChange={(data: any) => {
                        // console.log(data.target.value);
                        const temp: any = exData;
                        const dataIndex = temp.findIndex(
                          (prop: any) =>
                            prop.configKey === item.configKey &&
                            prop.attributeName === item.attributeName
                          // prop.id === item.id
                        );
                        temp[dataIndex].attributeValue = data.target.value;
                        // temp[dataIndex].isUpdated = true;
                        setExData(temp);
                      }}
                      disabled={!isEditable || isAll}
                    />
                  </Grid>
                </Grid>
              </Grid>
            );
          })}
        </Grid> */}
      </CustomAccordian>
    </form>
  );
};

export default EmployeePreferenceMaxItemConfig;
