import React, { useEffect, useState } from "react";
import { Grid, Tooltip, Typography } from "@mui/material";
import PortfolioFilterComp from "../filter/portfolio-filter-comp";
import "./style.css";
import { VerticalCenterAlignSxProps } from "../../common-allocation/user-info-timeline-group/style";
import FilterAltIcon from "@mui/icons-material/FilterAlt";

const PortfoliMainFilter = (props: any) => {
  const [openDrawer, setOpenDrawer] = useState(false);

  const [isFilterApplied, setIsFilterApplied] = useState<boolean>(false);
  const checkIfIsFilterApplied = () => {
    if (
      props.submittedFilterData &&
      Object.values(props.submittedFilterData).find(
        (v: any) => v && v.length > 0
      )
    ) {
      setIsFilterApplied(true);
      return true;
    } else {
      setIsFilterApplied(false);
      return false;
    }
  };

  useEffect(() => {
    checkIfIsFilterApplied();
  }, [openDrawer]);
  return (    
      <>
      <Grid item className="filter-icon-margin"> 
        {props?.filterOptions?.employees?.length > 0 || props?.filterOptions?.designations?.length > 0 ?
        <PortfolioFilterComp
          filterData={props.filterData}
          handleResetClick={props.handleResetClick}
          defaultValue={props.defaultValue}
          openDrawer={openDrawer}
          setOpenDrawer={setOpenDrawer}
          selectedDataByFilter={(e) => props.selectedDataByFilter(e)}
          {...props}
        /> : ""}
      </Grid>          
              <Grid item>
          {isFilterApplied && (
            <Typography component="span" sx={VerticalCenterAlignSxProps}>
              <Tooltip title="Filters applied">
                <FilterAltIcon fontSize="large" />
              </Tooltip>
            </Typography>
          )}
        </Grid>
      </>
  );
};
export default PortfoliMainFilter;
