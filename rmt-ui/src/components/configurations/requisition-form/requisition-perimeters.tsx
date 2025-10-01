import { Grid } from "@mui/material";
import React, { Fragment, useEffect } from "react";
import { useForm } from "react-hook-form";
import { preferenceOrder } from "./constant";
import "./style.css";
import { ToggleSwitch } from "./toggleSwitch";

const RequisitionFormPerimeters = (props: any) => {
  const { currentAttribute, setExData, exData, isDisabled } = props;
  const { setValue } = useForm();

  // TODO: Handle All Item value case
  // TODO: All case placement
  // TODO: Create a custom method for that
  // TODO: Disabled setting
  // Todo: All Activation disactivation settings
  // TODO:
  useEffect(() => {
    if (currentAttribute && exData && exData.length > 0) {
      // console.log(getValues());
      // console.log(exData);
      // console.log(exData);
      exData
        ?.filter(
          (d: any) =>
            d?.attributeName.toString().trim().toUpperCase() ===
            currentAttribute.toString().trim().toUpperCase()
        )
        .map((item: any) => setValue(item.configKey, item.attributeValue));
    }
  }, [exData, currentAttribute]);
  const GetInOrder = () => {
    const currentAttribueData = exData?.filter(
      (d: any) =>
        d.attributeName.toString().trim().toUpperCase() ===
        currentAttribute.toString().trim().toUpperCase()
    );
    const indexMap = new Map();
    preferenceOrder.map((item, index) => {
      indexMap.set(index, item.toString().trim().toUpperCase());
    });
    let formattedCurrentAttribueData: any[] = [];
    indexMap.forEach((values, keys) => {
      const indexData = currentAttribueData.findIndex(
        (item: any) =>
          item.configKey.toUpperCase().trim() === values.toUpperCase().trim()
      );
      if (indexData !== -1) {
        formattedCurrentAttribueData.push(currentAttribueData[indexData]);
      }
    });
    return formattedCurrentAttribueData;
  };

  const onToggleChanges = (item, data) => {
    const temp: any = exData;

    if (item.attributeName.toString().trim().toUpperCase() === "ALL") {
      const manipulatedData: any = temp.map((e: any) => {
        if (e.configKey === item.configKey) {
          return {
            ...e,
            allValue: data.toString(),
            attributeValue: data.toString(),
            isAll: true,
          };
        } else {
          return {
            ...e,
          };
        }
      });
      setExData(manipulatedData);
    } else {
      const dataIndex = temp.findIndex(
        (prop: any) =>
          prop.configKey === item.configKey &&
          prop.attributeName === item.attributeName
      );
      temp[dataIndex].attributeValue = data.toString();
      if (item.configKey.toLowerCase() == "Industry".toLowerCase() && !data) {
        const sub_industry_dataIndex = temp.findIndex(
          (prop: any) =>
            prop?.configKey?.toLowerCase() ===
              "Sub_Industry".toLocaleLowerCase() &&
            prop.attributeName === item.attributeName
        );
        temp[sub_industry_dataIndex].attributeValue = false;
      }
      setExData((prevValue: any) => [...temp]);
    }
  };

  const isIndustryDisabled = (item) => {
    if (item.configKey.toLowerCase() == "Sub_Industry".toLowerCase()) {
      const industry_dataIndex = exData.findIndex(
        (prop: any) =>
          prop?.configKey?.toLowerCase() === "Industry".toLocaleLowerCase() &&
          prop.attributeName === item.attributeName
      );
      return exData[industry_dataIndex].attributeValue === "false"
        ? true
        : false;
    }
    return false;
  };

  return (
    <>
      <Grid container>
        {GetInOrder().map((item: any, index: number) => {
          //console.log("Configurations:", item);
          return (
            <Fragment key={index}>
              <Grid item xs={4}>
                <div className="config-name">{item.configKeyDisplayText}</div>
              </Grid>
              <Grid item xs={4}>
                <ToggleSwitch
                  checked={item.attributeValue}
                  disabled={
                    item?.configKeyDisplayText === "Skills"
                      ? true
                      : isIndustryDisabled(item)
                      ? true
                      : isDisabled
                  }
                  onChange={(data: any) => {
                    onToggleChanges(item, data);
                  }}
                >
                  Toggle switch
                </ToggleSwitch>
              </Grid>
              <Grid xs={4}>
                {/* <span id="indicator">{`${item.attributeValue}/10`}</span> */}
                {/* {item.attributeValue == 10 && (
                  <span id="indicator">{`Must have`}</span>
                )} */}
              </Grid>
            </Fragment>
          );
        })}
      </Grid>
    </>
  );
};

export default RequisitionFormPerimeters;
