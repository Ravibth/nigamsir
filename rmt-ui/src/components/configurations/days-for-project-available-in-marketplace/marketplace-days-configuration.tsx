import React, { useContext, useEffect, useState } from "react";
import CustomAccordian from "../custom-accordian/custom-accordian";
import { useForm } from "react-hook-form";
import { Divider, Grid, Tooltip } from "@mui/material";
import * as util from "./util";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";
import { ConfigGroupEnum, ConfigTypeEnum, ExpertiseData } from "../constant";
import {
  UpdateConfiguration,
  getGonfigurationGroupByConfigGroupAndConfigType,
} from "../../../services/configuration-services/configuration.service";
import ControllerSwitch from "../../controllers/controller-switch";
import "./style.css";
import { toggleSwitch } from "./constant";
import UpdateConfigModal from "../update-config-modal/update-config-modal";
import { SnackbarContext } from "../../../contexts/snackbarContext";

const MarketplaceDaysConfiguration = (props: any) => {
  const { configurationType, handleIsEditable } = props;
  const [isOpen, setIsOpen] = useState(false);
  const [isEditable, setIsEditable] = useState(false);
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const snackbarContext: any = useContext(SnackbarContext);
  const [cancelOpen, setCancelOpen] = useState(false);
  const handleCancelOpen = () => setCancelOpen(true);
  const handleCancelClose = () => setCancelOpen(false);

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
        ConfigGroupEnum.DAYS_PROJECT_IN_MP_DB_GROUP.toString().toUpperCase(),
        configurationType
      ),
    ])
      .then((values) => {
        const dbData = values[0];
        // console.log(dbData);
        const marketplaceData: any = util.GetDataForMarketPlace(dbData);
        // console.log(marketplaceData);
        if (marketplaceData && marketplaceData.length > 0) {
          setAllData(marketplaceData[0].allValue);
          setIsAll(marketplaceData[0].isAll);
          setExData(marketplaceData);
          setIsEditable(false);
        }
      })
      .catch((ex) => {
        console.log(ex);
      });
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
  const handleSaveClick = (data: any) => {
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
  const changeAcceptedHandlerForCancel = (status: boolean) => {
    if (status === true) {
      GetDataHandler();
      setIsEditable(false);
    }
  };

  const changeAcceptedHandler = (status: boolean) => {
    if (status === true) {
      submitDataHandler();
      setIsEditable(() => false);
    }
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
    GetDataHandler();
    // Promise.all([
    //   GetGonfigurationGroupByConfigGroupAndConfigType(
    //     ConfigGroupEnum.DAYS_PROJECT_IN_MP_DB_GROUP.toString().toUpperCase(),
    //     configurationType
    //   ),
    // ])
    //   .then((values) => {
    //     const dbData = values[0];
    //     console.log(dbData);
    //     const marketplaceData: any = util.GetDataForMarketPlace(dbData);
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
  }, [configurationType]);
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
        // console.log(item.attributeValue);
        // if (item.attributeValue && !isNaN(item.attributeValue)) {
        //   console.log("Hi");
        //   clearErrors(item.attributeName);
        // } else {
        //   console.log("Hi1");
        //   setError(item.attributeName, { type: "required" });
        // }
        setValue(item.attributeName, item.attributeValue);
      });
    }
  }, [exData]);
  // console.log(errors);
  // console.log(getValues("Forensic"));
  return (
    <form onSubmit={handleSubmit(handleSaveClick)}>
      <CustomAccordian
        title={ConfigGroupEnum.DAYS_PROJECT_IN_MP_GROUP.toString().trim()}
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
        <Grid container justifyContent={"space-between"}>
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
                  sx={toggleSwitch}
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
        {/* <br />
        <Divider /> */}
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
                      sx={{ width: "82% !important" }}
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
        </Grid>
      </CustomAccordian>
    </form>
  );
};

export default MarketplaceDaysConfiguration;
