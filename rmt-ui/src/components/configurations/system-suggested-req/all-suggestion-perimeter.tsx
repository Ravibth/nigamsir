import { Grid } from "@mui/material";
import React, { Fragment, useEffect, useState } from "react";
import ControllerSlider from "../../controllerInputs/controllerSlider";
import { useForm } from "react-hook-form";
import { GetAllDataForSystemSuggestedConfig } from "./util";

const AllSuggestionPerimeter = (props: any) => {
  const { exData, setExData, isDisabled } = props;
  const { control, handleSubmit, setValue, getValues } = useForm();
  const [allData, setAllData] = useState([]);
  useEffect(() => {
    if (exData && exData.length > 0) {
      const getAllData: any = GetAllDataForSystemSuggestedConfig(exData);
      setAllData(getAllData);
    }
  }, [exData]);
  useEffect(() => {
    if (allData && allData.length > 0) {
      allData.map((item: any) => {
        setValue(item.configKey, item.allValue);
      });
    }
  }, [allData]);
  return (
    <>
      <Grid container>
        {allData &&
          allData.length > 0 &&
          allData.map((item: any, index: number) => {
            return (
              <Fragment key={index}>
                <Grid item xs={4}>
                  {item.configKeyDisplayText}
                </Grid>
                <Grid item xs={8}>
                  <ControllerSlider
                    name={item.configKey}
                    control={control}
                    min={1}
                    max={10}
                    step={1}
                    disabled={isDisabled}
                    sx={{ width: "20%" }}
                    onChange={(data: any) => {
                      const temp: any = exData;
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
                            isAll: true,
                          };
                        }
                      });
                      setExData(manipulatedData);
                    }}
                  />
                </Grid>
              </Fragment>
            );
          })}
      </Grid>
    </>
  );
};

export default AllSuggestionPerimeter;
