import { isEmpty, isNull } from "lodash";
import * as service from "../../services/project-employee-list";
import * as gc from "../../global/constant";
import moment from "moment";
import { getBudgetOverview } from "../../services/budget/budget.service";

export const getProjectTitles = async (
  pipelineCode: string,
  jobCode: string
) => {
  const projectTitle = await service.getProjectDetailsByCode(
    pipelineCode,
    jobCode
  );
  return projectTitle;
};
export const GetSubtitleForEmployee = (projectDetails: any) => {
  if (!isEmpty(projectDetails)) {
    // const projectJobCode = projectDetails?.projectJobCodes?.filter(
    //   (item: any) => item.isActive
    // );
    let data: any[] = [];
    if (projectDetails.jobCode && projectDetails.jobCode.length === 0) {
      data = [
        {
          title: "Pipeline Code",
          value: projectDetails.pipelineCode,
        },
        {
          title: "Allocated On",
          value: projectDetails.createdBy,
        },
        { title: "Total Efforts", value: 40 },
      ];
    } else {
      data = [
        {
          title: "Job Code",
          value: projectDetails?.jobCode,
        },
        {
          title: "Allocated On",
          value: projectDetails?.createdBy,
        },
        { title: "Total Efforts", value: 40 },
      ];
    }
    return data;
  } else {
    return [];
  }
};
export const GetSubtitleForRecruitor = (projectDetails: any) => {
  if (
    !isNull(projectDetails) &&
    projectDetails != "" &&
    Object.keys(projectDetails).length > 0
  ) {
    let data: any[] = [];
    if (projectDetails.jobCode && projectDetails.jobCode.length > 0) {
      data = [
        {
          title: "Job Code",
          value: projectDetails.jobCode,
        },
      ];
    } else {
      data = [
        {
          title: "Pipeline Code ",
          value: projectDetails.pipelineCode,
        },
      ];
    }

    // let data: any[] = [];
    // if (projectJobCode.length == 0) {
    //   data = [
    //     {
    //       title: "Pipeline Code",
    //       value: projectDetails.pipelineCode,
    //     },
    //   ];
    // } else {
    //   data = [
    //     {
    //       title: "Job Code",
    //       value: projectJobCode[0]?.jobCode,
    //     },
    //   ];
    // }

    return data;
  } else {
    return [];
  }
};

export const fetchBudgetOverallData = (
  jobCode: Array<any>,
  startDate: string,
  endDate: string
): Promise<any> => {
  const request = {
    JobCodes: jobCode,
    StartDate: startDate,
    EndDate: endDate,
  };
  return new Promise((resolve, reject) => {
    getBudgetOverview(request)
      .then((overall) => {
        const status =
          overall?.data[0]?.percentageCost <= 100 ? " In-Budget" : "Out-Budget";
        resolve(overall);
      })
      .catch(() => {
        reject("");
      });
  });
};
