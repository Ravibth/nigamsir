import React, { useContext, useEffect, useState } from "react";
import Activeallocationtable from "./activeallocationtable/activeallocationtable";
import { LoaderContext } from "../../contexts/loaderContext";
import { SnackbarContext } from "../../contexts/snackbarContext";
import {
  GetActiveAllocationByPipeLineCode,
  ReleaseResourceByGuid,
} from "../../services/allocation/allocation.service";
import { IAllocationFilterData } from "./IAllocationFliterData";
import { filteredAllocationList } from "./utils";
import { IAllocationDetails } from "./IAllocationDetails";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { RolesListMaster } from "../../common/enums/ERoles";
import _ from "lodash";
import { EmployeeWorkFlowStatus } from "./activeallocationtable/constant";
import { getEmployeeAllocationStatus } from "../../global/workflow/workflow-utils";

const Activeallocation = (props: any) => {
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);
  const [allocationList, setAllocationList] = useState<
    Array<IAllocationDetails>
  >([]);
  // const [filterData, setFilterData] = useState<IAllocationFilterData>();
  const [startDate, setStartDate] = useState<string>();
  const [endDate, setEndDate] = useState<string>();
  const [submittedFilterData, setSubmittedFilterData] = useState<any>({});
  const [allocationSelected, setAllocationSelected] = useState([]);
  const [defaultSelectedNodes, setDefaultSelectedNodes] = useState<string>("");
  const userContext = React.useContext(UserDetailsContext);

  useEffect(() => {
    loaderContext.open(true);
  }, []);

  setTimeout(() => {
    loaderContext.open(false);
  }, 3000);

  const getAllAllocations = (pipelineCode: any, jobCode: any) => {
    return new Promise((resolve, reject) => {
      GetActiveAllocationByPipeLineCode(pipelineCode, jobCode, true).then(
        (allocation: any) => {
          resolve(allocation.data);
        }
      );
    }).catch((err) => {
      console.log(err);
    });
  };

  const fetchAllApi = () => {
    loaderContext.open(true);
    Promise.all([
      getAllAllocations(
        props?.projectDetails?.pipelineCode,
        props?.projectDetails?.jobCode
      ),
    ])
      .then((values) => {
        let allocation: any = values[0];

        setAllocationList([]);

        if (userContext.role.includes(RolesListMaster.SystemAdmin)) {
          setAllocationList(allocation);
        } else {
          if (
            //projectRolesView change
            //todo check zero index by saif
            userContext?.projectPermissionData?.projectRolesView &&
            userContext?.projectPermissionData?.projectRolesView.length === 1 &&
            userContext?.projectPermissionData?.projectRolesView[0]
              .toLowerCase()
              .trim() === RolesListMaster.Employee.toLocaleLowerCase().trim()
          ) {
            allocation = allocation.filter(
              (a) =>
                a.empEmail.toLowerCase().trim() ===
                userContext?.username.toLowerCase().trim()
            );
          }
          let projectRoles = _.uniq(
            userContext?.projectPermissionData?.projectRolesView
          );
          projectRoles = projectRoles.filter((n) => n);
          if (
            projectRoles.length == 1 &&
            projectRoles[0]?.toLocaleLowerCase()?.trim() ===
              RolesListMaster.Employee.toLocaleLowerCase().trim()
          ) {
            allocation = allocation.filter((a) =>
              EmployeeWorkFlowStatus?.includes(
                getEmployeeAllocationStatus(a?.allocationStatus)
                  ?.allocationStatus
              )
            );
          }
          setAllocationList(allocation);
        }
        if (props.navigationState?.workflow_task_id) {
          setDefaultSelectedNodes(props.navigationState?.workflow_task_id);
        }
        setAllocationSelected(allocation);
      })
      .catch(() => {
        snackbarContext.displaySnackbar("Error fetching allocations", "error");
        // loaderContext.open(false);
      });
  };

  useEffect(() => {
    fetchAllApi();
  }, [props?.projectDetails?.pipelineCode, userContext?.projectPermissionData]);

  const selectedDataByFilter = (filterData: IAllocationFilterData) => {
    setSubmittedFilterData((prevData: any) => {
      return {
        ...filterData,
      };
    });
    filterData.startDate = startDate as string;
    filterData.endDate = endDate as string;
    const filteredDataList = filteredAllocationList(
      allocationSelected,
      filterData
    );
    setAllocationList(filteredDataList);
  };

  const handleResetClick = (data: IAllocationFilterData) => {
    // setFilterData(data);
  };

  const handleStartDateChange = (date: string) => {
    setStartDate(date);
  };
  const handleEndDateChange = (date: string) => {
    setEndDate(date);
  };

  const releaseAllocationResource = (Id: string) => {
    return new Promise((resolve, reject) => {
      ReleaseResourceByGuid(Id)
        .then(() => {
          Promise.all([fetchAllApi()])
            .then(() => {
              //loaderContext.open(false);
            })
            .catch(() => {
              snackbarContext.displaySnackbar(
                "Error fetching allocations",
                "error"
              );
              // loaderContext.open(false);
            });
          resolve("");
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  return (
    <div>
      <Activeallocationtable
        allocationList={allocationList}
        {...props}
        submittedFilterData={submittedFilterData}
        getAllAllocations={getAllAllocations}
        fetchAllApi={fetchAllApi}
        handleResetClick={handleResetClick}
        handleStartDateChange={handleStartDateChange}
        handleEndDateChange={handleEndDateChange}
        selectedDataByFilter={selectedDataByFilter}
        // projectCode={props.projectDetails.projectCode}
        pipelineCode={props.projectDetails.pipelineCode}
        jobCode={props.projectDetails.jobCode}
        releaseAllocationResource={releaseAllocationResource}
        setAllocationList={setAllocationList}
        defaultSelectedNodes={defaultSelectedNodes}
        setDefaultSelectedNodes={setDefaultSelectedNodes}
      />
    </div>
  );
};

export default Activeallocation;
