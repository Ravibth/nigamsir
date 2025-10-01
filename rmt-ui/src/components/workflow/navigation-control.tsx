import { useNavigate } from "react-router-dom";
// import { getResourceAllocationDetailsByGuid } from "../../services/allocation/allocation.service";
import { ROUTE_URL } from "../../global/constant";
import { WORKFLOW_MODULE } from "../../global/workflow/workflow-constant";
import { TabsTitleEnum } from "../requestor-view/constant";
import { routeValueEncode } from "../../global/utils";

export const getLabelForNavigationMyApprovalPage = (params) => {
  if (
    WORKFLOW_MODULE.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT.toLowerCase() ===
    params?.data?.workflow?.module?.toLowerCase()
  ) {
    return `${params?.data?.workflow?.entity_meta_data?.SkillCode}_${params?.data?.workflow?.entity_meta_data?.SkillName}_${params?.data?.workflow?.entity_meta_data?.EmpId}`;
  } else if (
    WORKFLOW_MODULE.EMPLOYEE_ALLOCATION.toLowerCase() ===
    params?.data?.workflow?.module?.toLowerCase()
  ) {
    return `${
      params?.data?.workflow?.entity_meta_data?.JobName
        ? params?.data?.workflow?.entity_meta_data?.JobName
        : params?.data?.workflow?.entity_meta_data?.PipelineName
    }_${
      params?.data?.workflow?.entity_meta_data?.JobCode
        ? params?.data?.workflow?.entity_meta_data?.JobCode
        : params?.data?.workflow?.entity_meta_data?.PipelineCode
    }_${params?.data?.workflow?.entity_meta_data?.RequisitionId}`;
  } else {
    return undefined;
  }
};

export default function NavigationControl(props: any) {
  let { state, label, params } = props;
  const navigate = useNavigate();
  const workflow_task_id = async (e: string) => {
    let url = "";
    const encodeSkillName = encodeURIComponent(
      params.data.workflow.entity_meta_data.SkillName
    );
    if (
      WORKFLOW_MODULE.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT.toLowerCase() ===
      params.data.workflow.module.toLowerCase()
    ) {
      url = `${ROUTE_URL.skillReview}`;
    } else {
      url = `${ROUTE_URL.projectDetails}/${routeValueEncode(
        params.data.workflow.entity_meta_data.PipelineCode
      )}/${routeValueEncode(
        params.data.workflow.entity_meta_data.JobCode
      )}/${routeValueEncode(label)}?tab=${TabsTitleEnum.Allocations}`;
    }

    navigate(url, {
      state: state,
    });
  };

  // const getProjectDetailsByGuid = async (guid: string) => {
  //   // guid = "69b8e03f-0e86-41de-8c7c-98ed4bde75aa"; //Todo: remove this code.
  //   return await getResourceAllocationDetailsByGuid(guid);
  // };

  return (
    <>
      <a href="#" onClick={(e) => workflow_task_id(params.data.workflow)}>
        {getLabelForNavigationMyApprovalPage(params)}
      </a>
    </>
  );
}
