import React, { useContext, useEffect, useState } from "react";
import CustomAccordian from "../custom-accordian/custom-accordian";
import { useForm } from "react-hook-form";
import { Divider, Grid, Tooltip } from "@mui/material";
import ControllerSwitch from "../../controllers/controller-switch";
import { ConfigGroupEnum } from "../constant";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";
import * as util from "./util";
import {
  UpdateConfiguration,
  getGonfigurationGroupByConfigGroupAndConfigType,
} from "../../../services/configuration-services/configuration.service";
import AllocarionReviewData from "./allocarion-review-data";
import UpdateConfigModal from "../update-config-modal/update-config-modal";
import groupBy from "lodash/groupBy";
import { TreeView } from "@mui/x-tree-view/TreeView";
import { TreeItem } from "@mui/x-tree-view/TreeItem";
import TreeViewConfig from "../tree-view-config/treeviewConfig";
import "../tree-view-config/treeview.css";
// @ts-ignore
import {
  MinusSquare,
  PlusSquare,
  CloseSquare,
  StyledTreeItem,
  // @ts-ignore
} from "../../treeviewComponent/treeViewComponent.tsx";
import ControllerSwitchConfig from "./controllerSwitch";
import { SnackbarContext } from "../../../contexts/snackbarContext";

const AllocationReviewConfig = (props: any) => {
  const {
    configurationType,
    handleIsEditable,
    title,
    configGroup,
    setIsDirty,
  } = props;
  const [isOpen, setIsOpen] = useState(false);
  const [isEditable, setIsEditable] = useState(false);
  const [groupedData, setGroupedData] = useState<any>();
  const [businessUnits, setBusinessUnits] = useState<any>();
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [expandedNodes, setExpandedNodes] = useState([]);

  const [cancelOpen, setCancelOpen] = useState(false);
  const handleCancelOpen = () => setCancelOpen(true);
  const handleCancelClose = () => setCancelOpen(false);

  const {
    control,
    watch,
    getValues,
    trigger,
    setValue,
    handleSubmit,
    getFieldState,
    formState: { errors, isDirty, touchedFields },
  } = useForm({
    mode: "onTouched",
  });
  const [exData, setExData] = useState([]);
  const [allDays, setAllDays] = useState();
  const [allActivation, setAllActivation] = useState(false);
  const [allActivationDeal, setAllActivationDeal] = useState(false);
  const [isAll, setIsAll] = useState(false);
  const [isAllBuSelected, setIsAllBuSelected] = useState({});
  const snackbarContext: any = useContext(SnackbarContext);
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
    // setIsEditable(false);
    // GetDataHandler();
    handleCancelOpen();
  };
  useEffect(() => {
    handleIsEditable(isEditable);
  }, [isEditable]);

  const submitDataHandler = () => {
    //console.log("update for allocation-review-config", exData);
    const updatePayload = util.GetUpdatePayload(exData, configurationType);
    //console.log("update for allocation-review-config", updatePayload);
    Promise.all([UpdateResourceReviewConfig(updatePayload)])
      .then((values) => {
        //console.log(values[0]);
        setIsEditable(() => false);
      })
      .catch((err) => {
        console.log(err);
      });
  };
  const GetDataHandler = () => {
    Promise.all([GetResourceAllocationReview(configGroup, "EXPERTISE")]).then(
      (values) => {
        //console.log(values[0]);
        const allocationReviewData: any = util.GetDataForResourceAllocation(
          values[0]
        );
        //console.log(allocationReviewData);
        createHireachy(allocationReviewData);
        if (allocationReviewData && allocationReviewData.length > 0) {
          //console.log("allocation review data", allocationReviewData[0]);
          setAllDays((prev) => allocationReviewData[0].allValue.noOfDays);
          // setAllActivation(allocationReviewData[0].allValue.activationStatus);
          // setAllActivationDeal(allocationReviewData[0].allValue.activationStatus);
          setIsAll(allocationReviewData[0].isAll);
          setExData(allocationReviewData);
          // setIsEditable(false);
        }
        // console.log(allocationReviewData);
      }
    );
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
      let obj = {};
      keys.forEach((key) => {
        obj[key] = false;
      });
      setIsAllBuSelected(obj);

      setGroupedData(groupedData);
    }
  };

  const handleSaveClick = (data: any) => {
    console.log(exData);
    handleOpen();
  };
  console.log(errors);

  const UpdateResourceReviewConfig = (data: any): Promise<any> => {
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

  const GetResourceAllocationReview = (
    configGroup: string,
    configType: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      getGonfigurationGroupByConfigGroupAndConfigType(configGroup, configType)
        .then((resp: any) => {
          // console.log(resp.data);
          resolve(resp.data);
        })
        .catch((err) => {
          console.log(err);
          reject(err);
        });
    });
  };

  useEffect(() => {
    GetDataHandler();
  }, [props]);

  let isDisabled = true;

  // useEffect(() => {
  //   console.log("alldayschange", allDays);
  //  if(businessUnits)
  //  {
  //   businessUnits.forEach((element :any) => {
  //     setValue(element+"AllDays", allDays);
  //   });
  //  }

  // }, [allDays,businessUnits]);

  useEffect(() => {
    setValue("AllActivation", allActivation);
  }, [allActivation]);

  useEffect(() => {
    setValue("All", isAll);
  }, [isAll]);

  useEffect(() => {
    //console.log("...exData1...", exData);
    // if (exData && exData.length > 0) {
    exData.map((item: any) => {
      let activationName = item.attributeName + "activation";
      let daysName = item.attributeName + "days";
      setValue(activationName, item.attributeValue.activationStatus);
      setValue(daysName, item.attributeValue.noOfDays);
    });
    //console.log("...exdata2...", exData);
    // }
  }, [exData]);

  const handleNodeToggle = (event, nodeId) => {
    const isIconClick =
      event.target.tagName.toLowerCase() === "path" ||
      event.target.tagName.toLowerCase() === "svg";
    if (isIconClick) {
      setExpandedNodes(nodeId);
    }
  };

  return (
    <form onSubmit={handleSubmit(handleSaveClick)}>
      <CustomAccordian
        title={title}
        configNote={props.configNote}
        isOpen={isOpen}
        isEditable={isEditable}
        handleAccordianOpenClick={handleAccordianOpenClick}
        handleAccordianCloseClick={handleAccordianCloseClick}
        handleEditClick={handleEditClick}
        handleCancelClick={handleCancelClick}
        // handleSaveClick={handleSaveClick}
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

        {/* <Divider /> */}
        <br />
        <div className="main-data-container">
          <Grid container spacing={2}>
            <TreeView
              defaultCollapseIcon={<MinusSquare />}
              defaultExpandIcon={<PlusSquare />}
              defaultEndIcon={<CloseSquare />}
              expanded={expandedNodes}
              onNodeToggle={handleNodeToggle}
            >
              <div className="config-container">
                {businessUnits?.map((item: any, index: number) => {
                  //console.log("item", item);
                  return (
                    <>
                      <div className="bu-container ml-15">
                        <TreeItem
                          nodeId={index.toString()}
                          label={
                            <div>
                              <Grid container alignItems={"center"}>
                                <div className="config-bu-name"> {item} </div>
                                <Grid item xs={2}>
                                  <ControllerSwitchConfig
                                    name={item}
                                    control={control}
                                    defaultChecked={
                                      isEditable == false
                                        ? isEditable
                                        : item == "ESG & Risk Consulting"
                                        ? allActivationDeal
                                        : isAllBuSelected[item]
                                    }
                                    disabled={!isEditable}
                                    onChange={(value: any) => {
                                      console.log(value);
                                      const temp: any = exData;
                                      const modifiedAllExData = temp.map(
                                        (i: any) => {
                                          if (i?.bu == item) {
                                            return {
                                              ...i,
                                              // allValue: {
                                              //   activationStatus: value,
                                              //   noOfDays: i.allValue.noOfDays,
                                              // },
                                              // attributeValue: {
                                              //   activationStatus: value,
                                              //   noOfDays:
                                              //     i.attributeValue.noOfDays,
                                              // },
                                              // isAll: true,
                                            };
                                          } else {
                                            return i;
                                          }
                                        }
                                      );
                                      console.log(
                                        "Modified Data ",
                                        modifiedAllExData
                                      );
                                      setExData(modifiedAllExData);
                                      // setAllActivation(value);
                                      if (item == "ESG & Risk Consulting") {
                                        setAllActivationDeal(value);
                                      } else {
                                        setAllActivation(value);
                                        let obj = isAllBuSelected;
                                        obj[item] = value;
                                        setIsAllBuSelected(obj);
                                      }
                                    }}
                                  />
                                </Grid>
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
                                    required={!isEditable}
                                    error={
                                      errors[item + "AllDays"] ? true : false
                                    }
                                    onChange={(data: any) => {
                                      console.log(data.target.value);
                                      const temp: any = exData;
                                      const modifiedAllExData = temp.map(
                                        (i: any) => {
                                          if (i?.bu == item) {
                                            return {
                                              ...i,
                                              allValue: {
                                                activationStatus:
                                                  i.allValue.activationStatus,
                                                noOfDays: data.target.value,
                                              },
                                              attributeValue: {
                                                activationStatus:
                                                  i.attributeValue
                                                    .activationStatus,
                                                noOfDays: data.target.value,
                                              },
                                              isAll: true,
                                            };
                                          } else {
                                            return i;
                                          }
                                        }
                                      );

                                      setExData(modifiedAllExData);
                                      setAllDays(data.target.value);
                                    }}
                                    disabled={
                                      isEditable == false
                                        ? true
                                        : item == "ESG & Risk Consulting"
                                        ? !allActivationDeal
                                        : !isAllBuSelected[item]
                                    }
                                    // if (item == "ESG & Risk Consulting") {
                                    //   setAllActivationDeal(value);
                                    // } else {
                                    //   setAllActivation(value);
                                    // }
                                    // isEditable={isEditable}
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
                                <div className="experties-name">
                                  {/* <TreeItem key ={expertise+index.toString()} nodeId={expertise+index.toString()} label={expertise?.attributeName}> */}
                                  <AllocarionReviewData
                                    index={index}
                                    item={expertise}
                                    exData={exData}
                                    setExData={setExData}
                                    isEditable={isEditable}
                                    control={control}
                                    errors={errors}
                                    isAll={isAll}
                                    allActivation={
                                      item == "ESG & Risk Consulting"
                                        ? allActivationDeal
                                        : isAllBuSelected[item]
                                    }
                                  />
                                  {/* </TreeItem> */}
                                </div>
                              );
                            }
                          )}
                        </TreeItem>
                      </div>
                    </>
                  );
                })}
              </div>
            </TreeView>
          </Grid>
        </div>
      </CustomAccordian>
    </form>
  );
};

export default AllocationReviewConfig;
