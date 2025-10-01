import { Grid } from "@mui/material";
import React, { useEffect, useState } from "react";
import ControllerSwitch from "../../controllers/controller-switch";
import { useForm } from "react-hook-form";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";
import "./style.css";
import ControllerSwitchConfig from "./controllerSwitch";

const AllocarionReviewData = (props: any) => {
  const {
    index,
    exData,
    setExData,
    item,
    isEditable,
    control,
    errors,
    isAll,
    allActivation,
    setValue,
  } = props;

  const [textDisable, setTextDisable] = useState(false);
  const [CheckValue, setCheckValue] = useState(false);
  //   console.log(props);
  useEffect(() => {
    setTextDisable(item.attributeValue.activationStatus);
    // props?.setValue(
    //   item.attributeName + "activation",
    //   item?.attributeValue?.activationStatus
    // );
    setCheckValue(item?.attributeValue?.activationStatus);
    //console.log("acttivation status ", item.attributeValue.activationStatus);
  }, [item]);
  return (
    <>
      <Grid item xs={10} key={index}>
        <Grid container alignItems={"center"}>
          <Grid item xs={6}>
            <div className="config-name">{item.attributeName}</div>
          </Grid>
          <Grid item xs={2}>
            <ControllerSwitchConfig
              name={item.attributeName + "activation"}
              control={control}
              // defaultChecked={item.attributeValue}
              // disabled={!isEditable || isAll}
              defaultChecked={CheckValue}
              disabled={!props.allActivation || !isEditable}
              onChange={(value: any) => {
                const temp: any = [...exData];
                console.log(props.allActivation);
                const dataIndex = temp.findIndex(
                  (pro: any) =>
                    pro.configKey === item.configKey &&
                    pro.attributeName === item.attributeName
                );
                if (dataIndex >= 0) {
                  // temp[dataIndex].attributeValue.activationStatus = value;
                  //   temp[dataIndex].isUpdated = true;
                  const changeObj = temp[dataIndex];
                  temp[dataIndex] = {
                    ...changeObj,
                    attributeValue: {
                      activationStatus: value,
                      noOfDays: changeObj.attributeValue.noOfDays,
                    },
                  };
                }
                // setActivator({id: item.id, activationStatus: });
                console.log(temp);
                setExData([]);
                setExData((prevData: any) => [...temp]);
                setCheckValue(value);
                // setExData([]);
                // setExData([]);
              }}
            />
          </Grid>
          <Grid item xs={3} className="config-textfield-colr">
            <ControllerNumberTextField
              name={item.attributeName + "days"}
              control={control}
              size="small"
              // sx={{ width: "60% !important" }}
              label={""}
              isUpDownNeeded={false}
              min={0}
              required={
                isEditable && !isAll && item.attributeValue.activationStatus
              }
              error={errors[item.attributeName + "days"] ? true : false}
              onChange={(data: any) => {
                console.log(data.target.value);
                const temp: any = exData;
                const dataIndex = temp.findIndex(
                  (prop: any) =>
                    prop.configKey === item.configKey &&
                    prop.attributeName === item.attributeName
                );
                // const allValue = temp[dataIndex].allValue;
                // temp[dataIndex].attributeValue.noOfDays = data.target.value;
                // temp[dataIndex].allValue = allValue;
                const changeObj = temp[dataIndex];
                temp[dataIndex] = {
                  ...changeObj,
                  attributeValue: {
                    activationStatus: changeObj.attributeValue.activationStatus,
                    noOfDays: data.target.value,
                  },
                };
                // temp[dataIndex].isUpdated = true;

                setExData((prevData: any) => [...temp]);
              }}
              disabled={
                !(
                  isEditable
                  // !isAll &&
                  // item.attributeValue.activationStatus
                ) ||
                !CheckValue ||
                !props.allActivation
              }
            />
          </Grid>
        </Grid>
      </Grid>
    </>
  );
};

export default AllocarionReviewData;
