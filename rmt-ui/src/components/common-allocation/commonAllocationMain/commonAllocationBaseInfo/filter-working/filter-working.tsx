import { useContext, useEffect, useState } from "react";
import { IProjectMaster } from "../../../../../common/interfaces/IProject";
import CommonAllocationFilter from "../../../common-allocation-filter/common-allocation-filter";
import {
  DefaultCommonAllocationFillerControlValues,
  ICommonAllocationFilterControl,
} from "../../../common-allocation-filter/utils";
import { IAllUserAllocationEntries, IUserTimeline } from "../../../interface";
import { FilterUserTimelinesForSameTeam } from "../../../utils";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../../../contexts/loaderContext";

export interface IFilterWorkingProps {
  allUserAllocationEntries: IAllUserAllocationEntries[];
  projectInfo: IProjectMaster;
  userTimelines: IUserTimeline;
  setUserTimelines: React.Dispatch<React.SetStateAction<IUserTimeline>>;
  setIsFilterApplied: React.Dispatch<React.SetStateAction<boolean>>;
  isFilterApplied: boolean;
}
const FilterWorking = (props: IFilterWorkingProps) => {
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [filteredValues, setFilteredValues] =
    useState<ICommonAllocationFilterControl>(
      DefaultCommonAllocationFillerControlValues
    );
  const [isFilterOpen, setOpenFilter] = useState<boolean>(false);
  const checkIsFilterApplied = () => {
    if (Object.values(filteredValues).find((v) => v.length > 0)) {
      return true;
    } else {
      return false;
    }
  };
  useEffect(() => {
    if (checkIsFilterApplied()) {
      props.setIsFilterApplied(true);
    } else {
      props.setIsFilterApplied(false);
    }
    if (filteredValues) {
      updateTimelinesForFilteredValues();
    }
  }, [filteredValues]);

  const updateTimelinesForFilteredValues = () => {
    loaderContext.open(true);
    const tempTimelines = FilterUserTimelinesForSameTeam(
      filteredValues,
      props.allUserAllocationEntries,
      props.userTimelines
    );
    props.setUserTimelines(tempTimelines);
    loaderContext.open(false);
  };

  return (
    <CommonAllocationFilter
      projectInfo={props.projectInfo}
      filteredValues={filteredValues}
      isFilterOpen={isFilterOpen}
      setOpenFilter={function (value: React.SetStateAction<boolean>): void {
        setOpenFilter(value);
      }}
      setFilteredValues={function (
        value: React.SetStateAction<ICommonAllocationFilterControl>
      ): void {
        setFilteredValues(value);
      }}
      allUserAllocationEntries={props.allUserAllocationEntries}
    />
  );
};
export default FilterWorking;
