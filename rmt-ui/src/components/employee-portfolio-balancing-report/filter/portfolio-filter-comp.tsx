import { Box, Button } from "@mui/material";
import { useState } from "react";
import * as constant from "./constant";
import FilterAltOutlinedIcon from "@mui/icons-material/FilterAltOutlined";
import MainFilter from "../../../common/images/MainFilter.png";
import PortfolioFilterDrawerComp from "./portfolio-filter-drawer-comp";

const PortfolioFilterComp = (props: any) => {
  return (
    <>
      {/* {props.filterData && ( */}
        <Button
          variant="outlined"
          sx={constant.filterIconButton}
          onClick={() => {
            props.setOpenDrawer(true);
          }}
        >
          <img src={MainFilter} alt="upload" />
        </Button>
     {/* )} */}
      {props.openDrawer ? (
        <Box mt={4} ml={4} mr={4} mb={4}>          
          <PortfolioFilterDrawerComp
            defaultValue={props?.defaultValue}
            selectedDataByFilter={props?.selectedDataByFilter}
            filterData={props.filterData|| []}
            openDrawer={props.openDrawer}
            setOpenDrawer={props.setOpenDrawer}
            handleResetClick={props.handleResetClick}
            {...props}
          />
        </Box>
      ) : (
        <></>
      )}
    </>
  );
};

export default PortfolioFilterComp;
