import { Grid } from "@mui/material";
import { Fragment, useEffect } from "react";
import ControllerSlider from "../../controllerInputs/controllerSlider";
import { useForm } from "react-hook-form";
import { preferenceOrder } from "./constant";
import "./style.css";

const SuggestionPerimeters = (props: any) => {
  const {
    currentAttribute,
    setExData,
    exData,
    isDisabled,
    requisitionFormExData,
  } = props;
  const { control, watch, getValues, setValue, handleSubmit } = useForm();

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

  const isSliderDisabled = (item: any): boolean => {
    var flag = true;
    const dataIndex = requisitionFormExData.findIndex(
      (prop: any) =>
        prop.configKey === item.configKey &&
        prop.attributeName === item.attributeName
    );
    if (requisitionFormExData[dataIndex]?.attributeValue == "false") {
      flag = false;
    }
    return flag;
  };
  // console.log(watch());
  return (
    <>
      <Grid container>
        {GetInOrder().map((item: any, index: number) => {
          return (
            <Fragment key={index}>
              <Grid item xs={4}>
                <div className="config-name">{item.configKeyDisplayText}</div>
              </Grid>
              <Grid item xs={4}>
                <ControllerSlider
                  name={item.configKey}
                  control={control}
                  min={1}
                  max={item?.configKeyDisplayText == "Skills" ? 9 : 10}
                  step={1}
                  size={"medium"}
                  track={"default"}
                  sx={{ width: "80%" }}
                  disabled={isDisabled || !isSliderDisabled(item)}
                  onChange={(data: any) => {
                    const temp: any = exData;
                    if (
                      item.attributeName.toString().trim().toUpperCase() ===
                      "ALL"
                    ) {
                      const manipulatedData: any = temp.map((e: any) => {
                        if (e.configKey === item.configKey) {
                          return {
                            ...e,
                            allValue: data,
                            attributeValue: data,
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
                      temp[dataIndex].attributeValue = data;
                      console.log(temp[dataIndex]);
                      console.log(temp);
                      setExData((prevValue: any) => [...temp]);
                    }
                  }}
                />
                {/* <span id="indicator">{`${item.attributeValue}/10`}</span> */}
              </Grid>
              <Grid xs={4}>
                {item?.configKeyDisplayText == "Skills" ? (
                  <span id="indicator">{`${item.attributeValue}/9`}</span>
                ) : (
                  <span id="indicator">{`${item.attributeValue}/10`}</span>
                )}
              </Grid>
            </Fragment>
          );
        })}
      </Grid>
    </>
  );
};

export default SuggestionPerimeters;
