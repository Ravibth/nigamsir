/* eslint-disable react-hooks/exhaustive-deps */
import { Button } from "@mui/material";
import React, { useState, useEffect, SetStateAction } from "react";
import MainFilter from "../../../common/images/MainFilter.png";
import SystemSuggestionsFilterForm from "./system-suggestion-filter-form";
import { IRequisitionMaster } from "../../../common/interfaces/IRequisition";
import {
  IFilterParameters,
  filterIconButton,
  DefaultFilterParameters,
  EFilterType,
} from "./constant";

export interface ISystemSuggestionsFilterProps {
  isFilterOpen: boolean;
  setOpenFilter: React.Dispatch<React.SetStateAction<boolean>>;
  requisitionDetails: IRequisitionMaster;
  filterValues: any;
  setFilterValues: React.Dispatch<any>;
}
const SystemSuggestionsFilter = (props: ISystemSuggestionsFilterProps) => {
  const [filtersList, setFiltersList] = useState<IFilterParameters[]>([]);

  const updateFilterParameterList = () => {
    const parametersToAlwaysShow = DefaultFilterParameters.filter(
      (item) => item.isAlwaysVisible
    );

    const finalFilterParameters =
      props.requisitionDetails.requisitionParameters.map((item) => {
        const matchingItem = DefaultFilterParameters.find(
          (filter) => filter?.key?.toLowerCase() === item?.category?.toLowerCase()
        );
        if (matchingItem) {
          matchingItem.isVisible = true;
          matchingItem.min = 0;
          matchingItem.max = item.requisitionWeight;
          return matchingItem;
        } else {
          return {
            key: item.category,
            label: item.category.trim().split("_").join(" "),
            isVisible: true,
            filterType: EFilterType.Slider,
            min: 0,
            max: item.requisitionWeight,
          };
        }
      });

    setFiltersList([...parametersToAlwaysShow, ...finalFilterParameters]);
  };

  useEffect(() => {
    if (props.isFilterOpen) {
      updateFilterParameterList();
    }
  }, [props.isFilterOpen]);

  useEffect(() => {
    return () => {
      setFiltersList([]);
    };
  }, []);

  return (
    <React.Fragment>
      <Button
        onClick={() => {
          props.setOpenFilter(true);
        }}
        variant="outlined"
        sx={filterIconButton}
      >
        <img src={MainFilter} alt="upload" />
      </Button>
      {props.isFilterOpen && (
        <SystemSuggestionsFilterForm
          isFilterOpen={props.isFilterOpen}
          setOpenFilter={function (value: SetStateAction<boolean>): void {
            props.setOpenFilter(value);
          }}
          filtersList={filtersList}
          setFilterValues={props.setFilterValues}
          filterValues={props.filterValues}
        />
      )}
    </React.Fragment>
  );
};

export default SystemSuggestionsFilter;
