import { SetStateAction, useEffect, useState } from "react";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { IAllUserAllocationEntries } from "../interface";
import CommonAllocationFilterForm from "./common-allocation-filter-form";
import { IUserInfo } from "../../system-suggestions/availability-view/constants";
import { Button } from "@mui/material";
import { ICommonAllocationFilterControl, filterIconButton } from "./utils";
import MainFilter from "../../../common/images/MainFilter.png";
import { IAllocateFormSkills } from "../common-allocation-modal-form/utils";

export interface ICommonAllocationFilterProps {
  projectInfo: IProjectMaster;
  isFilterOpen: boolean;
  filteredValues: ICommonAllocationFilterControl;
  setOpenFilter: React.Dispatch<React.SetStateAction<boolean>>;
  setFilteredValues: React.Dispatch<
    React.SetStateAction<ICommonAllocationFilterControl>
  >;
  allUserAllocationEntries: IAllUserAllocationEntries[];
}

const CommonAllocationFilter = (props: ICommonAllocationFilterProps) => {
  const [resourcesList, setResourcesList] = useState<IUserInfo[]>([]);
  const [skillOptions, setSkillOptions] = useState<IAllocateFormSkills[]>([]);

  const updateResourceList = () => {
    const tempData: IUserInfo[] = props.allUserAllocationEntries.map(
      (item) => item.userInfo
    );
    setResourcesList(tempData);
    const skillsList = [];
    props.allUserAllocationEntries.forEach((item) => {
      if (item.skills) {
        skillsList.push(...item.skills);
      }
    });
    setSkillOptions(skillsList);
  };

  useEffect(() => {
    if (props.isFilterOpen) {
      updateResourceList();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.isFilterOpen]);

  return (
    <>
      <Button
        variant="outlined"
        sx={filterIconButton}
        onClick={() => {
          props.setOpenFilter(true);
        }}
      >
        <img src={MainFilter} alt="upload" />
      </Button>
      {props.isFilterOpen && (
        <CommonAllocationFilterForm
          projectInfo={props.projectInfo}
          isFilterOpen={props.isFilterOpen}
          setOpenFilter={function (value: SetStateAction<boolean>): void {
            props.setOpenFilter(value);
          }}
          resourcesList={resourcesList}
          allUserAllocationEntries={props.allUserAllocationEntries}
          filteredValues={props.filteredValues}
          setFilteredValues={props.setFilteredValues}
          skillOptions={skillOptions}
        />
      )}
    </>
  );
};
export default CommonAllocationFilter;
