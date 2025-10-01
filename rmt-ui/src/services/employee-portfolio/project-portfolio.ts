// import { IProjectMaster } from "../../common/interfaces/IProject";

// interface ProjectFilter {
//   userEmail: string;
//   role: string;
//   limit: number;
//   pagination: number;
// }

// export const PorjectPortfolio = async (filter: ProjectFilter): Promise<IProjectMaster[]> => {
//   console.log("Using dummy data with filter:", filter);

//   const dummyData: IProjectMaster[] = [
//     {
//       id: 101,
//       jobName: "PC101",
//       jobCode: "PC101",
//       pipelineCode: "PC101",
//       pipelineName: "Pipeline Name 1",
//       clientName: "Adani Enterprises Ltd",
//       expertise: "Audit-PIE",
//       sme: "Attest Service-PIE",
//       startDate: new Date("2023-09-05T11:44:42.586626+05:30"),
//       endDate: new Date("2023-09-15T11:44:42.586628+05:30"),
//       createdDate: new Date("2023-09-05T11:44:42.586629+05:30"),
//       description: "Dummy project description for testing purposes.",
//       projectAllocationStatus: "ALLOCATION_COMPLETED",
//       location: "Mumbai",
//       pipelineStage: "Approved",
//       projectType: "Non-recurring",
//       pipelineStatus: "WON",
//       chargableType: "Chargable",
//       revenueUnit: "Tax Audit",
//       industry: "Auto and Auto Component",
//       subindustry: "Food distributors",
//       budgetStatus: "In Budget",
//       projectFulFilledDemands: 0,
//       isActive: true,
//       createdBy: "System",
//       modifiedBy: "System",
//       isRollover: false,
//       rolloverDays: 0,      
//     },
//     // Add more dummy entries if needed
//   ];

//   return dummyData;
// };

import axios from "axios";
import { IProjectMaster } from "../../common/interfaces/IProject";
import { IResourceAllocationMaster } from "../../common/interfaces/IAllocation";

const baseurl = process.env.REACT_APP_BASEAPIURL;

interface ProjectFilter {
  userEmail: string;  
  limit: number;
  pagination: number;
}

export const PorjectPortfolio = async (EmpEmail: string,startdateproject:Date,enddateproject:Date,submittedFilterData: any ) => {
  try {
    const payload = {
      "EmpEmail": EmpEmail,
      "startDate": startdateproject,  
      "endDate": enddateproject,
      "clientName": submittedFilterData?.clientname?.map((a) => a.id) || null,
      "clientGroup": submittedFilterData?.clientgroupname?.map((a) => a.id) || null     
    }
    const response = await axios.post(
      `${baseurl}ResourceAllocation/GetAllocationsByEmailorClients`,
      payload     
    );
    startdateproject.setHours(0, 0, 0, 0);
    enddateproject.setHours(23, 59, 59, 999);
    const data: IResourceAllocationMaster[] = response?.data || [];
    let resultdata  = data.map((e) => {
      const allocationDays = e.resourceAllocationDays ?? [];
      let result = allocationDays.filter(x => new Date(x.allocationDate) >= startdateproject && new Date(x.allocationDate) <= enddateproject)  ?? [];    
      return {
        ...e,resourceAllocationDays:result
      }
    }).filter(e => e.resourceAllocationDays?.length > 0);    
    return resultdata;
  } catch (error) {
    throw error;
  }
};
