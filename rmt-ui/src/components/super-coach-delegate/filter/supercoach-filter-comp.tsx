import { Box, Button } from "@mui/material";
import { useEffect, useState } from "react";
import * as constant from "./constant";
import MainFilter from "../../../common/images/MainFilter.png";
import SupercoachFilterDrawerComp from "./supercoach-filter-drawer-comp";
import { getFilerOption } from "../util";
import { ISupercochFiltersOptions } from "../../../common/interfaces/ISupercochFiltersOptions";
import "./styles.css";

interface SuperCoachFilterProps {
  defaultValue?: Record<string, any>;
  selectedDataByFilter: (data: Record<string, any>) => void;
  filterData?: any[];
  handleResetClick?: () => void;
  isShowFilter?: boolean;
  showFilterOptions?: () => void;
  onCloseFliterOption?: () => void;
  // Add other props as needed
}

const SuperCoachFilter = (props: SuperCoachFilterProps) => {
  const [filterOptions, setFilterOptions] = useState<ISupercochFiltersOptions>({
    businessunit: [],
    competency: [],
    designations: [],
    employees: [],
    grade: [],
    location: []
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getFilterOptions = async () => {
      try {
        setLoading(true);
        const [bu, competency, designation, location, employees] = await getFilerOption();
        
        setFilterOptions({
          businessunit: bu || [],
          competency: competency || [],
          designations: designation || [],
          // Extract grades from designations if needed
          grade: designation?.map(d => d.grade).filter(Boolean) || [],
          location: location || [],
          employees: employees || []
        });
      } catch (error) {
        console.error("Error fetching filter options:", error);
      } finally {
        setLoading(false);
      }
    };

    getFilterOptions();
  }, []);

  if (loading) {
    return <Box>Loading filter options...</Box>;
  }

  return (
  <>
    <div className="outlined-box" style={{ display: props.isShowFilter ? "block" : "none" }}>
      <SupercoachFilterDrawerComp
        defaultValue={props.defaultValue}
        selectedDataByFilter={props.selectedDataByFilter}
        submittedFilterData={props.filterData}
        openDrawer={props.isShowFilter}
        filterOptions={filterOptions}
        onCloseFliterOption={props.onCloseFliterOption}
        handleResetClick={props.handleResetClick}
      />
    </div>
    {!props.isShowFilter && (
      <Button
        variant="outlined"
        sx={constant.filterIconButton}
        onClick={props.showFilterOptions}
      >
        <img src={MainFilter} alt="upload" />
      </Button>
    )}
  </>
);

};

export default SuperCoachFilter;