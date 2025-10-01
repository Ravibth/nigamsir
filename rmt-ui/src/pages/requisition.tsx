import { Fragment, useContext, useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../contexts/userDetailsContext";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../contexts/snackbarContext";
import {
  EConfigurationConfigGroup,
  GetExpertiesConfigurationByExpertiesNameAndConfigGroup,
} from "../services/configuration-services/configuration.service";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import {
  GetProjectRoleAndPermission,
  routeValueEncode,
  ShowUnAuthorizedView,
} from "../global/utils";
import RequisitionWrapper from "../components/requisition/requisition-wrapper";
import { ConfigBuOfferingKeyCreator } from "../common/interfaces/IConfigurationMaster";
import { GetProjectByCode } from "../services/project-list-services/project-list-services";
import { ICheckboxOption } from "../components/requisition/utils";


const CreateRequisition = (props: any) => {
  const [parameterOptions, setParameterOptions] = useState<ICheckboxOption[]>(
    []
  );
  const [navigateToUpdate, setNavigateToUpdate] = useState<boolean>(false);

  const [projectDetails, setProjectDetails] = useState(null);
  const { pipelineCode, jobCode } = useParams();
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const navigate = useNavigate();

  useEffect(() => {
    getProjectDetails();
  }, []);

  const getProjectDetails = () => {
    //************ Get Project details */
    return new Promise((resolve, reject) => {
      GetProjectByCode(pipelineCode, jobCode).then(async (response: any) => {
        await fetchTheListOfParameters(response.data);
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
      });
    });
  };

  const fetchTheListOfParameters = (projectInfo): Promise<any> => {
    return new Promise((resolve, reject) => {
      const keySelector = ConfigBuOfferingKeyCreator(
        projectInfo.bu,
        projectInfo.offerings
      );
      GetExpertiesConfigurationByExpertiesNameAndConfigGroup(
        keySelector,
        EConfigurationConfigGroup.RequisitionForm
      )
        .then((resp) => {
          modifyParameterOptions(resp.data);
          resolve(resp);
        })
        .catch((er) => {
          snackbarContext.displaySnackbar(
            "Error fetching parameters",
            SnackbarSeverity.ERROR
          );
        });
    });
  };

  const modifyParameterOptions = (parameters: any[]) => {
    const finalParameters: ICheckboxOption[] = parameters
      .filter((item) => item.attributeValue.toLowerCase() !== "false")
      .map((item) => {
        return {
          name: item.configurationGroup.configKey,
          label: item.configurationGroup.congigDisplayText,
        };
      });
    setParameterOptions(finalParameters);
  };

  const navigateToUpdateRequisitionOnSubmission = (
    pipelineCode: string,
    requisitionId: string,
    jobCode?: string
  ) => {
    setNavigateToUpdate(true);
    setTimeout(() => {
      navigate(
        `/update-requisition/${routeValueEncode(
          pipelineCode
        )}/${routeValueEncode(jobCode)}/${requisitionId}`
      );
    }, 50);
  };

  return (
    <Fragment>
      {!navigateToUpdate && (
        <RequisitionWrapper
          projectInfo={projectDetails}
          parameterOptions={parameterOptions}
          navigateToUpdateRequisitionOnSubmission={
            navigateToUpdateRequisitionOnSubmission
          }
        />
      )}
    </Fragment>
  );
};
export default CreateRequisition;
