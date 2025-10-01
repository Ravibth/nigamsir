import { useEffect, useState, useContext } from "react";
import { IRequisitionMaster } from "../../../common/interfaces/IRequisition";
import SystemSuggestionsLayout, {
  ViewTypeOfSuggestions,
} from "../system-suggestions-layout";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { getRequisitionDetailsByRequisitionId } from "../../../services/requisition/requisition";
import ActionButton from "../../actionButton/actionButton";
import { Grid } from "@mui/material";
import {
  LoaderContextProps,
  LoaderContext,
} from "../../../contexts/loaderContext";
import {
  CheckUserHasRoleToAllocateOnProject,
  IsPermissionExistForProject,
} from "../../../global/utils";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../../../contexts/userDetailsContext";
import { MODULE_NAME_ENUM } from "../../../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../../../common/access-control-guard/access-control";

interface IRequisitionFormSystemSuggestions {
  requisitionId: string;
  projectInfo: IProjectMaster;
}

const RequisitionFormSystemSuggestions = (
  props: IRequisitionFormSystemSuggestions
) => {
  const [fetchingSuggestions, setFetchingSuggestions] =
    useState<boolean>(false);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [hasPermissionToAllocate, setHasPermissionToAllocate] =
    useState<boolean>(false);

  const [requisitionDetails, setRequisitionDetails] =
    useState<IRequisitionMaster | null>(null);

  const userContext: IUserDetailsContext = useContext(UserDetailsContext);

  useEffect(() => {
    const perm =
      CheckUserHasRoleToAllocateOnProject(
        props.projectInfo.projectRoles
          .filter(
            (item) =>
              item.user.toLowerCase() === userContext.username.toLowerCase() ||
              item?.delegateEmail?.toLowerCase() ===
                userContext.username.toLowerCase()
          )
          .map((item) => item.role),
        userContext.role
      ) &&
      IsPermissionExistForProject(
        userContext.projectPermissionData?.permissions,
        MODULE_NAME_ENUM.Allocation,
        PERMISSION_TYPE.Create,
        userContext.role
      );
    setHasPermissionToAllocate(perm);
  }, []);

  useEffect(() => {
    if (props.requisitionId) {
      loaderContext.open(true);
      Promise.all([fetchRequisitionDetails()])
        .then(() => {
          loaderContext.open(false);
        })
        .catch(() => {
          loaderContext.open(false);
        });
    }
  }, [props.requisitionId]);

  const fetchRequisitionDetails = (): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      getRequisitionDetailsByRequisitionId(props.requisitionId)
        .then((resp) => {
          setRequisitionDetails(resp.data);
          resolve(true);
        })
        .catch((err) => {
          resolve(false);
        });
    });
  };

  return (
    <>
      {!fetchingSuggestions && hasPermissionToAllocate && (
        <Grid container spacing={2}>
          <Grid item xs={11} />
          <Grid item xs={1}>
            <ActionButton
              label={"Search"}
              onClick={function (e: any): void {
                setFetchingSuggestions(true);
              }}
              disabled={fetchingSuggestions}
              type={"button"}
            />
          </Grid>
        </Grid>
      )}
      {fetchingSuggestions && (
        <SystemSuggestionsLayout
          requisitionId={props.requisitionId}
          requisitionDetails={requisitionDetails}
          projectInfo={props.projectInfo}
          showBackActionButton={false}
          resetActiveRequisitions={() => {}}
          baseViewType={ViewTypeOfSuggestions.GridView}
          showSuggestionsViewToggle={true}
          openCommonAllocationInPopup={true}
          useCustomFilter={false}
          useGridFilter={true}
          useLazyLoading={false}
        />
      )}
    </>
  );
};

export default RequisitionFormSystemSuggestions;
