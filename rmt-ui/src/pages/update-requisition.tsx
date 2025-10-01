/* eslint-disable react-hooks/exhaustive-deps */
import React, { Fragment, useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { LoaderContext } from "../contexts/loaderContext";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../contexts/snackbarContext";
import { getRequisitionDetailsByRequisitionId } from "../services/requisition/requisition";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../contexts/userDetailsContext";
import { RolesListMaster } from "../common/enums/ERoles";
import { IProjectMaster } from "../common/interfaces/IProject";
import {
  GetProjectRoleAndPermission,
  ShowUnAuthorizedView,
} from "../global/utils";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import RequisitionWrapper from "../components/requisition/requisition-wrapper";
import { IRequisitionMaster } from "../common/interfaces/IRequisition";
import { GetProjectByCode } from "../services/project-list-services/project-list-services";
import { ICheckboxOption } from "../components/requisition/utils";

const UpdateRequisitionForm = () => {
  const rolesWhoCanAccess = [
    RolesListMaster.Delegate,
    RolesListMaster.ResourceRequestor,
    RolesListMaster.AdditionalEl,
    RolesListMaster.AdditionalDelegate,
  ];
  const [parameterOptions, setParameterOptions] = useState<ICheckboxOption[]>(
    []
  );
  const [projectDetails, setProjectDetails] = useState(null);
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);

  const [requisitionInfo, setRequisitionInfo] =
    useState<IRequisitionMaster | null>(null);
  const { pipelineCode, jobCode, requisitionId } = useParams();
  const [updatePermissions, setUpdatePermissions] = useState<boolean>(false);

  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const navigate = useNavigate();

  const isUpdatingAllowedForUser = (projectInfo: IProjectMaster): boolean => {
    if (userDetailsContext.role.includes(RolesListMaster.SystemAdmin)) {
      return true;
    }

    const permissions =
      userDetailsContext.modulePermissionsState[MODULE_NAME_ENUM.Requisition];

    //projectRolesView change
    const loggedInUserRoleInProject = projectInfo?.projectRolesView?.filter(
      (item: any) =>
        item.user.trim().toLowerCase() ===
          userDetailsContext.username?.trim().toLowerCase() &&
        rolesWhoCanAccess.includes(item.applicationRole)
    );
    if (
      loggedInUserRoleInProject &&
      loggedInUserRoleInProject?.length > 0 &&
      permissions?.update
    ) {
      return true;
    }
    return false;
  };

  useEffect(() => {
    if (pipelineCode && requisitionId) {
      loaderContext.open(true);
      Promise.all([fetchProjectDetailsByCode(pipelineCode, jobCode)])
        .then(() => {
          return fetchRequisitionDetailsByCode(requisitionId);
        })
        .then(() => {
          loaderContext.open(false);
        })
        .catch((err) => {
          loaderContext.open(false);
          snackbarContext.displaySnackbar(
            "Error Fetching Details",
            SnackbarSeverity.ERROR
          );
        });
    } else {
      navigate(-1);
      snackbarContext.displaySnackbar(
        "Invalid Details",
        SnackbarSeverity.ERROR
      );
    }
  }, []);

  const fetchProjectDetailsByCode = (
    pipelineCode: string,
    jobCode: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      GetProjectByCode(pipelineCode, jobCode)
        .then(async (response: any) => {
          const isActionAllowed = isUpdatingAllowedForUser(response.data);
          setProjectDetails(response.data);

          if (isActionAllowed === true) {
            setUpdatePermissions(true);
            setProjectDetails(response.data);
            var hasPermission = false;
            if (response.data) {
              hasPermission = await GetProjectRoleAndPermission(
                response.data,
                userDetailsContext,
                pipelineCode,
                jobCode,
                MODULE_NAME_ENUM.Requisition,
                PERMISSION_TYPE.Create
              );
            }
            if (hasPermission === true) {
              //Current User has permission to access the page
            } else {
              //Show Unauthorized access error for the page
              ShowUnAuthorizedView(navigate);
            }
          } else {
            setUpdatePermissions(false);
          }
          resolve([]);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  const fetchRequisitionDetailsByCode = (
    requisitionId: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      getRequisitionDetailsByRequisitionId(requisitionId, true)
        .then(async (response: any) => {
          if (!response.data) {
            ShowUnAuthorizedView(navigate);
            resolve([]);
          } else {
            if (
              response.data?.hasPermissionToEdit === false ||
              response.data?.requisitionStatus?.toLowerCase().trim() !==
                "pending" ||
              response.data?.resourceAllocations?.length > 0
            ) {
              setUpdatePermissions(false);
              resolve([]);
            }

            setRequisitionInfo(response.data);
            resolve([]);
          }
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  return (
    <Fragment>
      {/* <CreateRequisitionMain
        projectDetails={projectDetails}
        requisitionDetails={requisitionDetails}
        setRequisitionDetails={setRequisitionDetails}
        updatePermissions={updatePermissions}
        parameterOptions={parameterOptions}
      /> */}
      {requisitionInfo && projectDetails && (
        <RequisitionWrapper
          projectInfo={projectDetails}
          requisitionDetails={requisitionInfo}
          updatePermissions={updatePermissions}
          parameterOptions={[]}
          navigateToUpdateRequisitionOnSubmission={function (
            pipelineCode: string,
            requisitionId: string,
            jobCode?: string
          ): void {}}
        />
      )}
    </Fragment>
  );
};
export default UpdateRequisitionForm;
