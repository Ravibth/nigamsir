import { EProjectChargeType } from "../../../common/enums/EProject";
import {
  ResourceRequestorsList,
  ReviewersList,
  RolesListMaster,
} from "../../../common/enums/ERoles";
import { WCGTURLEnum } from "../constant";

const baseurlUat360 = process.env.REACT_APP_WCGT_BASEURL;

export const GetWCGTURLByPipelineCodeJobCodeAndUserProjectRole = (
  userProjectRole: any[],
  projectChargeType: string,
  pipelineCode: string,
  jobCode?: string | null
) => {
  if (
    pipelineCode &&
    pipelineCode.length > 0 &&
    jobCode &&
    jobCode.length > 0 &&
    projectChargeType.toLowerCase().trim() ===
      EProjectChargeType.CHARGABLE.toLowerCase().trim()
  ) {
    //CHARGABLE JOB RESOURCE REQUESTOR

    if (
      userProjectRole.some((d) =>
        ResourceRequestorsList?.map((e) =>
          e?.toString()?.toLowerCase().trim()
        ).includes(d?.toLowerCase().trim())
      )
    ) {
      // console.log("CHARGABLE JOB RESOURCE REQUESTOR URL");
      return baseurlUat360 + WCGTURLEnum.WCGT_RR_CHARGABLE_JOB_URL;
    } else if (
      userProjectRole.some((d) =>
        ReviewersList.map((e) => e?.toString().toLowerCase().trim()).includes(
          d?.toLowerCase().trim()
        )
      )
    ) {
      console.log("CHARGABLE JOB REVIEWER URL");
      return baseurlUat360 + WCGTURLEnum.WCGT_REVIEWER_CHARGABLE_JOB_URL;
    } else {
      return null;
    }
  } else if (
    pipelineCode &&
    pipelineCode.length > 0 &&
    jobCode &&
    jobCode.length > 0 &&
    projectChargeType.toLowerCase().trim() ===
      EProjectChargeType.NON_CHARGABLE.toLowerCase().trim()
  ) {
    if (
      userProjectRole.some((d) =>
        ResourceRequestorsList.map((e) =>
          e.toString().toLowerCase().trim()
        ).includes(d.toLowerCase().trim())
      )
    ) {
      console.log("NON-CHARGABLE JOB RESOURCE REQUESTOR URL");
      //   console.log(
      //     "Part 1",
      //     userProjectRole.map((e) => ResourceRequestorsList.filter(e).length > 0)
      //   );
      //   userProjectRole.forEach((item) => {
      //     console.log(
      //       "Part 3",
      //       ResourceRequestorsList.filter((e) => e === item)
      //     );
      //   });
      //   console.log(
      //     "Part 2",
      //     userProjectRole.map((e) => ResourceRequestorsList.includes(e))
      //   );
      return baseurlUat360 + WCGTURLEnum.WCGT_RR_NON_CHARGABLE_JOB_URL;
    } else if (
      userProjectRole.some((d) =>
        ReviewersList.map((e) => e.toString().toLowerCase().trim()).includes(
          d.toLowerCase().trim()
        )
      )
    ) {
      console.log("NON CHARGABLE JOB REVIEWER URL");
      return baseurlUat360 + WCGTURLEnum.WCGT_REVIEWER_NON_CHARGABLE_JOB_URL;
    } else {
      return null;
    }
  } else if (
    pipelineCode &&
    pipelineCode.length > 0 &&
    (!jobCode || jobCode?.length === 0)
  ) {
    if (
      userProjectRole.some((d) =>
        ResourceRequestorsList.map((e) =>
          e.toString().toLowerCase().trim()
        ).includes(d.toLowerCase().trim())
      )
    ) {
      console.log("PIPELINE RESOURCE REQUESTOR URL");
      return baseurlUat360 + WCGTURLEnum.WCGT_RR_PIPELINE_URL;
    } else if (
      userProjectRole.some((d) =>
        ReviewersList.map((e) => e.toString().toLowerCase().trim()).includes(
          d.toLowerCase().trim()
        )
      )
    ) {
      console.log("PIPELINE CHARGABLE JOB REVIEWER URL");
      return baseurlUat360 + WCGTURLEnum.WCGT_REVIEWER_PIPELINE_URL;
    } else {
      return null;
    }
  }
  return null;
};
