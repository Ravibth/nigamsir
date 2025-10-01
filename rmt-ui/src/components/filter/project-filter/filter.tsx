import { Box, Button } from "@mui/material";
import FilterDrawerComp from "./filter-drawer";
import { useState } from "react";
import * as constant from "./constant";
import FilterAltOutlinedIcon from "@mui/icons-material/FilterAltOutlined";
import MainFilter from "../../../common/images/MainFilter.png";

const FilterComp = (props: any) => {
  return (
    <>
      {props.filterData && (
        <Button
          variant="outlined"
          sx={constant.filterIconButton}
          onClick={() => {
            props.setOpenDrawer(true);
          }}
        >
          <img src={MainFilter} alt="upload" />
        </Button>
      )}
      {props.openDrawer ? (
        <Box mt={2} ml={2} mr={2} mb={2}>
          <FilterDrawerComp
            defaultValue={props.defaultValue}
            selectedDataByFilter={props.selectedDataByFilter}
            filterData={props.filterData}
            openDrawer={props.openDrawer}
            setOpenDrawer={props.setOpenDrawer}
            handleResetClick={props.handleResetClick}
          />
        </Box>
      ) : (
        <></>
      )}
    </>
  );
};

export default FilterComp;
