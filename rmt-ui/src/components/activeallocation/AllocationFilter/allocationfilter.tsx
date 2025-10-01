import React, { useEffect, useState } from "react";
import * as constant from "./constant";
import FilterAltOutlinedIcon from "@mui/icons-material/FilterAltOutlined";
import { Box, Button } from "@mui/material";
import Allocationfilterdrawer from "./allocationfilterdrawer";
import { IAllocationFilterData } from "../IAllocationFliterData";
import MainFilter from "../../../common/images/MainFilter.png";

const Allocationfilter = (props: any) => {
  const [openDrawer, setOpenDrawer] = useState(false);
  const [filterData, setfilterData] = useState<IAllocationFilterData>();
  const [filterDefaultValue, setFilterDefaultValue] = useState<any>({
    designation: [],
    expertise: [],
    employeeName: [],
    sme: [],
    businessUnit: [],
    revenueUnit: [],
  });

  useEffect(() => {
    setFilterDefaultValue(props.submittedFilterData);
  }, [props.submittedFilterData]);

  return (
    <>
      <Button
        variant="outlined"
        sx={constant.filterIconButton}
        onClick={() => {
          setOpenDrawer(true);
        }}
      >
        {/* <FilterAltOutlinedIcon /> */}
        <img src={MainFilter} alt="upload" />
      </Button>
      {openDrawer ? (
        <Box mt={2} ml={2} mr={2} mb={2}>
          <Allocationfilterdrawer
            defaultValue={filterDefaultValue}
            openDrawer={openDrawer}
            filterData={filterData}
            setOpenDrawer={setOpenDrawer}
            handleResetClick={props.handleResetClick}
            handleStartDateChange={props.handleStartDateChange}
            handleEndDateChange={props.handleEndDateChange}
            selectedDataByFilter={props.selectedDataByFilter}
          />
        </Box>
      ) : (
        <></>
      )}
    </>
  );
};

export default Allocationfilter;
