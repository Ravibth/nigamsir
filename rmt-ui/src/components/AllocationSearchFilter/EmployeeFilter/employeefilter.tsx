import { Box, Button } from "@mui/material";
import { useState, useEffect } from "react";
import * as constant from "./constant";
import FilterAltOutlinedIcon from "@mui/icons-material/FilterAltOutlined";
import EmployeefilterDrawer from "./employeefilterdrawer";
import { IFilterData, IProjectRoles } from "../IFilterData";
import uniq from "lodash/uniq";
import { RolesListMaster } from "../../../common/enums/ERoles";
import MainFilter from "../../../common/images/MainFilter.png";

//Control is not used after common screen implementation
const Employeefilter = (props: any) => {
  const [filterData, setfilterData] = useState<IFilterData>();
  const [filterDefaultValue, setFilterDefaultValue] = useState<any>({
    experties: [],
    smeg: [],
    clientName: [],
    locations: [],
    job: [],
    elName: [],
    BusinessUnit: [],
  });

  useEffect(() => {
    if (props.openDrawer) retriveFilterOptions();
  }, [props.resourceList, props.openDrawer]);

  const retriveFilterOptions = () => {
    const projectDetails = props.projectCompleteDetail;
    const resourceList: Array<any> = props.resourceList;

    // const projectJobs: Array<IProjectJobCodes> =
    //   props.projectCompleteDetail?.projectJobCodes;
    // const jobCodes: Array<string> = projectJobs?.map((job) => job.jobCode);
    const jobCodes = [projectDetails?.jobCode];
    //projectRolesView change
    const projectRoles: Array<IProjectRoles> = projectDetails?.projectRolesView;
    const experties: Array<string> = resourceList
      ? resourceList.map((resource) => resource.expertise)
      : [];
    const locations: Array<string> = resourceList
      ? resourceList.map((resource) => resource.location)
      : [];
    const BusinessUnit: Array<string> = resourceList
      ? resourceList.map((resource) => resource.BusinessUnit)
      : [];
    const smeg: Array<string> = resourceList
      ? resourceList.map((resource) => resource.smeg)
      : [];
    const el: Array<string> = projectRoles
      // ?.filter((role) => role.role === "EL" || role.role === "EngagementLeader")
      ?.filter((role) => role.role === RolesListMaster.EngagementLeader)
      .map((role) => role.userName);

    const data: IFilterData = {
      locations: uniq(locations),
      job: jobCodes,
      startDate: "",
      endDate: "",
      email: [],
      experties: uniq(experties),
      smeg: uniq(smeg),
      BusinessUnit: uniq(BusinessUnit),
    };
    setfilterData(data);
  };

  return (
    <>
      <Button
        variant="outlined"
        sx={constant.filterIconButton}
        onClick={() => {
          props.setOpenDrawer(true);
        }}
      >
        {/* <FilterAltOutlinedIcon /> */}
        <img src={MainFilter} alt="upload" />
      </Button>
      {props.openDrawer ? (
        <Box mt={2} ml={2} mr={2} mb={2}>
          <EmployeefilterDrawer
            defaultValue={filterDefaultValue}
            selectedDataByFilter={props.selectedDataByFilter}
            filterData={filterData}
            openDrawer={props.openDrawer}
            setOpenDrawer={props.setOpenDrawer}
            handleResetClick={props.handleResetClick}
            handleStartDateChange={props.handleStartDateChange}
            handleEndDateChange={props.handleEndDateChange}
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

export default Employeefilter;
