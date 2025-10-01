import { Button } from "@mui/material";
import React, { useEffect, useState } from "react";
import FilterListIcon from "@mui/icons-material/FilterList";
import { filterIconButton } from "./uitls";
import ReportDashboardFilterForm from "./report-filter-form";
// import {
//   loadBUTreeMappingData,
//   loadLocationMaster,
// } from "./get-filter-service/get-filters";
// import { IBUTreeMapping } from '../../../../../common/interfaces/IBuTreeMapping';
// import { IWCGTLocationMaster } from '../../../../../common/interfaces/IWCGTLocationMaster';

const ReportFilter = (props) => {
  //   const [buTreeMappingMaster, setBuTreeMappingMaster] =
  // useState<IBUTreeMapping[]>();
  //   const [locationMaster, setLocationMaster] = useState<IWCGTLocationMaster[]>();

  useEffect(() => {
    // Promise.all([loadBUTreeMappingData(), loadLocationMaster()])
    //   .then((result) => {
    //     const buMappingData: IBUTreeMapping[] = result[0];
    //     const locationData: IWCGTLocationMaster[] = result[1];
    //     console.log("buMappingData",buMappingData)
    //     setBuTreeMappingMaster(buMappingData);
    //     setLocationMaster(locationData);
    //   })
    //   .catch((error) => {
    //     console.log(error);
    //   });
  }, []);
  return (
    <>
      <Button
        variant="outlined"
        sx={filterIconButton}
        onClick={() => {
          props.setOpenFilter(true);
        }}
      >
        <FilterListIcon />
      </Button>
      {props.isFilterOpen && (
        <>
          <ReportDashboardFilterForm
            setOpenFilter={props.setOpenFilter}
            isFilterOpen={props.isFilterOpen}
            // buTreeMappingMaster={buTreeMappingMaster}
            // locationData={locationMaster}
            filterParameters={props.filterParameters}
            setFilterParameters={props.setFilterParameters}
            GetFilterDefaultValueOnTheBasisOfRole={
              props.GetFilterDefaultValueOnTheBasisOfRole
            }
            applyFilter={props.applyFilter}
          />
        </>
      )}
    </>
  );
};

export default ReportFilter;
