import { Tooltip, Typography } from "@mui/material";
import CommonAllocationMain from "./commonAllocationMain/commonAllocationMain";
import {
  IUserSelectedAllocationContext,
  UserSelectedAllocationState,
} from "./context/users-selected-allocation";
import {
  IAllUserAllocationEntries,
  ICommonAllocationWrapperProps,
} from "./interface";
import UserInfoTimelineGroup from "./user-info-timeline-group/user-info-timeline-group";
import PriorityHighIcon from "@mui/icons-material/PriorityHigh";
import "./commonAllocationWrapper.css";
import { useContext, useEffect, useState } from "react";
import { IProjectMaster } from "../../common/interfaces/IProject";
import { getProjectCompleteDetails } from "../../services/project-list-services/project-list-services";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";

export const getUserAvailabilityInfoComponent = (
  userItem: IAllUserAllocationEntries,
  index: number
) => {
  return <UserInfoTimelineGroup userAllocationEntry={userItem} index={index} />;
};

export enum EReturnTypeForCheckingUserErrors {
  TooltipJSXElement = "TooltipJSXElement",
  BooleanValue = "BooleanValue",
}

export const checkIfUserHasAnyErrors = (
  usersSelectedAllocationContext: IUserSelectedAllocationContext,
  email: string,
  returnType: EReturnTypeForCheckingUserErrors
) => {
  const userToBeChecked =
    usersSelectedAllocationContext.allUserSelectionEntries.find(
      (user) => user.email === email
    );
  let userErrors = [];
  if (userToBeChecked) {
    if (!userToBeChecked.available) {
      userErrors.push("User is not available for selected time period.");
    }
    if (userToBeChecked.skills.length === 0) {
      userErrors.push("User skills are missing.");
    }
    if (!userToBeChecked.description) {
      userErrors.push("Description is missing.");
    }
    if (userToBeChecked.allocations.length === 0) {
      userErrors.push("User Allocation periods are missing.");
    }
  }

  if (returnType === EReturnTypeForCheckingUserErrors.BooleanValue) {
    return userErrors.length > 0;
  } else if (
    returnType === EReturnTypeForCheckingUserErrors.TooltipJSXElement
  ) {
    if (userErrors.length > 0) {
      return (
        <Tooltip
          title={
            <Typography sx={{ fontSize: "medium" }}>
              <>
                {userErrors.map((item) => (
                  <li>{item}</li>
                ))}
              </>
            </Typography>
          }
        >
          <PriorityHighIcon fontSize="small" color="error" />
        </Tooltip>
      );
    } else {
      return <></>;
    }
  }
};

const CommonAllocationWrapper = (props: ICommonAllocationWrapperProps) => {
  const [projectInfo, setProjectInfo] = useState<IProjectMaster | null>(null);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);

  useEffect(() => {
    setProjectInfo(props.projectInfo);
  }, [props.projectInfo]);
  const refreshProjectInfo = async () => {
    loaderContext.open(true);
    await getProjectCompleteDetails(
      props.projectInfo.pipelineCode,
      props.projectInfo.jobCode
    )
      .then((response) => {
        const data = response.data;
        setProjectInfo(data);
      })
      .catch((err) => {
        console.log(err);
        // throw err;
      });
    loaderContext.open(false);
  };

  return (
    <>
      {props.projectInfo && (
        <UserSelectedAllocationState>
          <CommonAllocationMain
            {...props}
            projectInfo={projectInfo}
            refreshProjectInfo={refreshProjectInfo}
          />
        </UserSelectedAllocationState>
      )}
    </>
  );
};
export default CommonAllocationWrapper;
