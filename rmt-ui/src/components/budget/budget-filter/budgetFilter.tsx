import { Box, Button } from "@mui/material";
import { useState, useEffect } from "react";
import * as constant from "../../AllocationSearchFilter/EmployeeFilter/constant";
import uniq from "lodash/uniq";
import BudgetFilterDrawer from "./budgetFilterDrawer";
import MainFilter from "../../../common/images/MainFilter.png";

export enum EBudgetFilterData {
  location = "location",
  competency = "competency",
  designation = "designation",
  grade = "grade",
  businessUnit = "businessUnit",
}

export interface IBudgetFilterData {
  [EBudgetFilterData.location]: string[];
  [EBudgetFilterData.competency]: string[];
  [EBudgetFilterData.designation]: string[];
  [EBudgetFilterData.grade]: string[];
  [EBudgetFilterData.businessUnit]: string[];
}

const BudgetFilter = (props: any) => {
  const [openDrawer, setOpenDrawer] = useState(false);
  const [filterData, setFilterData] = useState<IBudgetFilterData>();
  const [filterDefaultValue, setFilterDefaultValue] =
    useState<IBudgetFilterData>({
      location: [],
      competency: [],
      designation: [],
      grade: [],
      businessUnit: [],
    });

  useEffect(() => {
    retrieveFilterOptions();
  }, [props.identityData]);

  const retrieveFilterOptions = () => {
    const resourceList: Array<any> = props.identityData;
    const competency: Array<string> = resourceList
      ? resourceList?.map((resource) => resource.competency)
      : [];
    const location: Array<string> = resourceList
      ? resourceList?.map((resource) => resource.location)
      : [];
    const designation: Array<string> = resourceList
      ? resourceList?.map((resource) => resource.designation)
      : [];
    const grade: Array<string> = resourceList
      ? resourceList
          ?.filter((resource) => resource?.grade)
          ?.map((resource) => resource?.grade)
      : [];
    const businessUnit: Array<string> = resourceList
      ? resourceList
          ?.filter((resource) => resource?.businessUnit)
          ?.map((resource) => resource?.businessUnit)
      : [];

    const data: IBudgetFilterData = {
      location: uniq(location),
      competency: uniq(competency),
      designation: uniq(designation),
      grade: uniq(grade),
      businessUnit: uniq(businessUnit),
    };
    setFilterData(data);
  };
  return (
    <>
      <Button
        variant="outlined"
        sx={constant.filterIconButton}
        onClick={() => {
          setOpenDrawer(true);
        }}
      >
        <img src={MainFilter} alt="upload" className={"filter-icon"} />
      </Button>
      {openDrawer ? (
        <Box mt={2} ml={2} mr={2} mb={2}>
          <BudgetFilterDrawer
            filterDefaultValue={filterDefaultValue}
            setFilterDefaultValue={setFilterDefaultValue}
            selectedDataByFilter={props.selectedDataByFilter}
            filterData={filterData}
            openDrawer={openDrawer}
            setOpenDrawer={setOpenDrawer}
            handleResetClick={props.handleResetClick}
            projectName={props?.projectCompleteDetail?.projectName}
            defaultStartDate={props?.projectCompleteDetail?.startDate}
            defaultEndDate={props?.projectCompleteDetail?.endDate}
          />
        </Box>
      ) : (
        <></>
      )}
    </>
  );
};

export default BudgetFilter;
