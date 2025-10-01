import React, { useContext, useEffect, useMemo, useRef, useState } from "react";
import AgGridComponent from "../aggrid-component/aggrid-component";
import { Button, Grid, Tooltip } from "@mui/material";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import AssignAddEl from "../assign-delegate/assign-el";
import DeleteIcon from "@mui/icons-material/Delete";
import AssignAddDelegate from "../assign-delegate/assign-ad-delegate";
import _ from "lodash";
import CloseIcon from "@mui/icons-material/Close";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import { IProjectRoles } from "../../common/interfaces/IProjectRole";
import { ICellEditorParams } from "ag-grid-enterprise";
import { IsProjectInActiveOrClosed } from "../../global/utils";
import {
  hasPermissionForAddAndDeleteButton,
  hasPermissionForAdditionalDelegateChange,
  hasPermissionForAdditionalElChange,
} from "./utils";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";
import InfoIcon from "@mui/icons-material/Info";
import AddIcon from "@mui/icons-material/Add";
import { EPipelineStatus } from "../../common/enums/EProject";
import { RolesListMaster } from "../../common/enums/ERoles";

export interface IEmployeeDetails {
  id: number | null;
  name: string | null;
  emailId: string | null;
  projectRoleId: number | null;
  projectId: number | null;
  empCode: string | null;
  additionalElName: string | null;
  additionalDelegateName: string | null;
  additionalElEmail: string | null;
  additionalDelegateEmail: string | null;
  description: string | null;
  businessUnit: string | null;
  designation: string | null;
  expertise: string | null;
  fname: string | null;
  lname: string | null;
  roleOrder: number | null;
  role_list: string[] | null;
  serviceLine: string | null;
  supercoach: string | null;
  skills: string[] | null;
  smeg: string | null;
}

const AdditionalElAgGrid = (props: any) => {
  const { projectDetails, additionalElDBData, setAdditionalElDbData } = props;
  const gridRef: any = useRef();
  const userContext = useContext(UserDetailsContext);
  const [rowData, setRowData] = useState<IEmployeeDetails[]>([]);
  const {
    setProjectRoleAdditionalData,
    setProjectRoleAdditionalDBData,
    setProjectUiRolesData,
    projectUpdateDelegate,
  } = React.useContext(ProjectUpdateDetailsContext);
  const projectDetailsContext = React.useContext(ProjectUpdateDetailsContext);
  const isSuspended =
    projectDetails?.pipelineStatus === EPipelineStatus.Suspended ||
    projectDetails?.pipelineStatus === EPipelineStatus.Lost
      ? true
      : false;

  const addNewRow = () => {
    const newRowData = {
      name: "",
      emailId: "",
      empCode: "",
      designation: "",
      expertise: "",
      projectRoleId: 0,
      projectId: 0,
      description: "",
      additionalElEmail: "",
      additionalDelegateEmail: "",
      additionalElName: "",
      additionalDelegateName: "",
      // skills
    } as IEmployeeDetails;
    if (gridRef?.current?.api) {
      const res = gridRef?.current?.api?.applyTransaction({
        add: [newRowData],
        addIndex: gridRef?.current?.api?.getDisplayedRowCount(),
      });
      console.log(getAllRows());
      gridUpdateTrigger();
    }
  };

  const gridUpdateTrigger = () => {
    const currentGridData = getAllRows();
    generateUiRoleList(currentGridData);
    setProjectRoleAdditionalData(currentGridData);
  };

  const isRowEmpty = (row: any) => {
    return row.userName === "" && row.delegateUserName === "";
  };
  const deleteRow = (index: number, rowNode: any) => {
    if (gridRef?.current?.api) {
      // const rowNode = gridRef?.current?.api?.getDisplayedRowAtIndex(index);
      const res = gridRef?.current?.api?.applyTransaction({
        remove: [rowNode.data],
      });
      gridUpdateTrigger();
    }
  };

  const defaultColDef = {
    lockVisible: true,
  };
  function getValueFromServer(params: ICellEditorParams): Promise<string[]> {
    return new Promise((resolve) => {
      setTimeout(
        () =>
          resolve(["English", "Spanish", "French", "Portuguese", "(other)"]),
        1000
      );
    });
  }
  useEffect(() => {
    if (additionalElDBData && additionalElDBData.length > 0) {
      const currentData: IEmployeeDetails[] = additionalElDBData.map(
        (dbData: IProjectRoles) => {
          return {
            projectRoleId: dbData.id,
            projectId: dbData.projectId,
            additionalElName: dbData.userName,
            additionalDelegateName: dbData.delegateUserName,
            additionalElEmail: dbData.user,
            description: dbData.description,
            additionalDelegateEmail: dbData.delegateEmail,
          };
        }
      );
      setRowData(currentData);
      setProjectRoleAdditionalDBData(currentData);
      setProjectRoleAdditionalData(currentData);
      generateUiRoleList(currentData);
      // setGridRowData(currentData);
      //set data to ag-grid
      //show data over grid
      //set and change  the grid data
    }
  }, [additionalElDBData]);
  const generateUiRoleList = (currentData: IEmployeeDetails[]) => {
    const uiDataOfAgGrid = [];
    currentData.map((data) => {
      if (data.additionalDelegateEmail) {
        uiDataOfAgGrid.push(data.additionalDelegateEmail);
      }
      if (data.additionalElEmail) {
        uiDataOfAgGrid.push(data.additionalElEmail);
      }
    });
    projectUpdateDelegate.map((data) => {
      if (data.user && data.isactive === true) {
        uiDataOfAgGrid.push(data.user);
      }
    });
    setProjectUiRolesData(uiDataOfAgGrid);
  };

  const data1 = additionalElDBData?.find((i) => i?.projectId);
  const payload = {
    projectId: data1?.projectId,
    user: data1?.user,
    userName: data1?.userName,
    role: data1?.role,
    delegateUserName: data1?.delegateUserName,
    delegateEmail: data1?.delegateEmail,
    description: data1?.description,
    isActive: false,
  };
  const reqObj = {
    AddProjectUserRoles: [payload],
  };

  const checkAdditionalELDisability = (params: any) => {
    if (IsProjectInActiveOrClosed(projectDetails)) {
      return true;
    }

    const hasPermiss =
      hasPermissionForAdditionalElChange(
        userContext?.projectPermissionData?.projectRoles,
        userContext?.username,
        params
      ) || userContext.role.includes(RolesListMaster.SystemAdmin);
    return !hasPermiss;
  };

  const checkAdditionalDelegateDisability = (params: any) => {
    if (IsProjectInActiveOrClosed(projectDetails)) {
      return true;
    }

    const hasPermiss =
      hasPermissionForAdditionalDelegateChange(
        userContext?.projectPermissionData?.projectRoles,
        userContext?.username,
        params
      ) || userContext.role.includes(RolesListMaster.SystemAdmin);
    return !hasPermiss;
  };
  const columnDefs: any = useMemo(
    () => [
      {
        headerName: "Additional EL",
        headerTooltip: "Additional EL",
        field: "additionalElEmail",
        flex: 1,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        suppressMenu: true,
        tooltipField: "userName",
        menuTabs: ["filterMenuTab"],
        cellRenderer: function (params: any) {
          return (
            <div>
              <AssignAddEl
                projectDetails={projectDetails}
                cellParams={params}
                additionalElDBData={additionalElDBData}
                gridRef={gridRef}
                setAdditionalElDbData={setAdditionalElDbData}
                additionalElGridValue={[params]}
                gridRowData={rowData}
                setRowData={setRowData}
                rowIndex={params.rowIndex}
                rowNode={params.node}
                gridUpdateTrigger={gridUpdateTrigger}
                isDisabled={checkAdditionalELDisability(params)}
              />
            </div>
          );
        },
      },
      {
        headerName: "Additional Delegate",
        headerTooltip: "Additional Delegate",
        field: "additionalDelegateEmail",
        flex: 1,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        suppressMenu: true,
        tooltipField: "delegateUserName",
        menuTabs: ["filterMenuTab"],
        cellRenderer: function (params: any) {
          return (
            <div>
              <AssignAddDelegate
                projectDetails={projectDetails}
                cellParams={params}
                delegateDBData={additionalElDBData}
                gridRef={gridRef}
                setAdditionalElDbData={setAdditionalElDbData}
                additionalDelegateGridValue={[params]}
                gridRowData={rowData}
                setRowData={setRowData}
                rowIndex={params.rowIndex}
                rowNode={params.node}
                gridUpdateTrigger={gridUpdateTrigger}
                isDisabled={checkAdditionalDelegateDisability(params)}
              />
            </div>
          );
        },
      },
      {
        field: "",
        flex: 0.25,
        tooltipField: "",
        suppressMenu: true,
        hide: (() => {
          //projectRolesView change not required
          const isVisible = hasPermissionForAddAndDeleteButton(userContext?.projectPermissionData?.projectRoles) 
                            ||
                            userContext.role.includes(RolesListMaster.SystemAdmin) ; //7053
          //projectRolesView change not required

          return !isVisible;
        })(),
        cellRenderer: function (params: any) {
          if (isRowEmpty(params.data)) {
            return (
              <Tooltip title={"Remove"}>
                <CloseIcon
                  onClick={() => {
                    projectDetailsContext.setIsProjectDetailsDirty(true);
                    deleteRow(params.rowIndex, params.node);
                  }}
                />
              </Tooltip>
            );
          } else {
            return (
              <Tooltip title={"Remove Additional EL"}>
                <DeleteIcon
                  className="delete-icon-grid"
                  onClick={() => {
                    projectDetailsContext.setIsProjectDetailsDirty(true);
                    deleteRow(params.rowIndex, params.node);
                  }}
                />
              </Tooltip>
            );
          }
        },
      },
    ],
    //projectRolesView change not required
    [userContext?.projectPermissionData?.projectRoles]
  );
  const getAllRows = () => {
    let rowData = [];
    gridRef.current.api.forEachNode((node) => rowData.push(node.data));
    return rowData;
  };
  const cellValueChangeHandler = () => {
    console.log(`CellValue ,`, getAllRows());
  };

  return (
    <div>
      <Grid className="btn-container-grid">
        <span className="additional-header">
          <PersonOutlineOutlinedIcon style={{ marginRight: "3px" }} />
          {"Additional Engagement Leader & Delegate"}
          <Tooltip
            sx={{ marginLeft: "5px" }}
            className={"tool-requisition"}
            title={
              "Any changes to the assignment might have an impact on allocations of the project."
            }
            placement="bottom"
          >
            <InfoIcon />
          </Tooltip>
        </span>

        <Grid item xs={2}>
          {(hasPermissionForAddAndDeleteButton(
            //projectRolesView change not required
            userContext?.projectPermissionData?.projectRoles
          ) ||
            userContext.role.includes(RolesListMaster.SystemAdmin)) && (
            <Button
              className="add-el-btn"
              onClick={addNewRow}
              disabled={
                isSuspended || IsProjectInActiveOrClosed(projectDetails)
              }
            >
              <AddIcon className="addbtn-icon" />{" "}
              <span className="add-text">Add</span>
            </Button>
          )}
        </Grid>
      </Grid>
      <div className="assignElAggrid">
        <AgGridComponent
          gridComponentRef={gridRef}
          rowData={rowData}
          columnDefs={columnDefs}
          defaultColDef={defaultColDef}
          onCellValueChanged={cellValueChangeHandler}
          tooltipShowDelay={0}
          tooltipHideDelay={2000}
          isPageination={true}
          pageSize={18}
          suppressCsvExport={true}
          suppressContextMenu={true}
          suppressExcelExport={true}
          isFilterVisible={true}
          hideExport={true}
          suppressCellFocus={true}
          height={"320px"}
        ></AgGridComponent>
      </div>
    </div>
  );
};

export default AdditionalElAgGrid;
