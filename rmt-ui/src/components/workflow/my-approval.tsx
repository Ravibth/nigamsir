import { useContext, useEffect, useRef, useState } from "react";
import AgGridComponent from "../aggrid-component/aggrid-component";
import * as constant from "./constant";
import * as util from "./util";
import "./style.css";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { AgGridReact } from "ag-grid-react";
import {
  IsPermissionExistForApp,
  ShowUnAuthorizedView,
} from "../../global/utils";
import { PERMISSION_TYPE } from "../../common/access-control-guard/access-control";
import { MODULE_NAME_ENUM } from "../../common/module-permission/module-permission";
import { useNavigate } from "react-router-dom";

export default function MyApproval(props: any) {
  const [myApprovalTasks, setMyApprovalTasks] = useState([]);
  const userDetailsContext = useContext(UserDetailsContext);
  const gridRef: any = useRef();
  const gridComponentRef = useRef<AgGridReact | null>(null);
  const navigate = useNavigate();
  useEffect(() => {
    var hasPermission = IsPermissionExistForApp(
      userDetailsContext.modulePermissionsState,
      MODULE_NAME_ENUM.Pending_Requests,
      PERMISSION_TYPE.Read
    );
    if (hasPermission) {
      //Current User has permission to access the page
    } else {
      //Show Unauthorized access error for the page
      ShowUnAuthorizedView(navigate);
    }

    util.getMyAllPendingTask(userDetailsContext.username).then((data) => {
      // Todo to change line with 16.
      // util.getMyTask("").then((data) => {
      setMyApprovalTasks(data);
    });
  }, []);
  return (
    <>
      <div className="my-approval-title">My Approval</div>
      <div id="myapprovalgrid" className="approvalaggrid">
        <AgGridComponent
          ref={gridRef}
          gridComponentRef={gridComponentRef}
          rowData={myApprovalTasks}
          columnDefs={constant.columnDefs}
          defaultColDef={constant.defaultColDef}
          tooltipShowDelay={0}
          tooltipHideDelay={2000}
          isPageination={true}
          pageSize={18}
          suppressCsvExport={false}
          suppressContextMenu={false}
          suppressExcelExport={false}
          isFilterVisible={true}
          hideExport={false}
          suppressCellFocus={true}
        ></AgGridComponent>
      </div>
    </>
  );
}
