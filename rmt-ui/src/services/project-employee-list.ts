import axios from "axios";
const baseurl = process.env.REACT_APP_PROJECT_MS;

// let response = "";
// await axios
//   .get(
//     baseurl +
//       `Project/GetProjectsForEmployee?employeeEmail='${employeeEmail}'`
//   )
//   .then((resp: any) => {
//     response = resp.data;
//   })
//   .catch((err: any) => {
//     response = "";
//   });
// return response;

export const getAllEmployeeProjectList = async (employeeEmail: string) => {
  let response = "";
  await axios
    .get(
      baseurl +
        `Project/GetProjectsForEmployee?employeeEmail='${employeeEmail}'`
    )
    .then((resp: any) => {
      response = resp.data;
    })
    .catch((err: any) => {
      response = "";
    });
  return response;
};

export const getProjectDetailsByCode = async (
  pipelineCode: string,
  jobCode: string
) => {
  let response = "";
  console.log(pipelineCode, jobCode);
  await axios
    .get(
      baseurl +
        `Project/GetProjectDetailsForRequestor?pipelineCode=${encodeURIComponent(
          pipelineCode
        )}&jobCode=${encodeURIComponent(jobCode)}`
    )
    .then((resp: any) => {
      response = resp.data;
    })
    .catch((err: any) => {
      response = "";
    });
  return response;
};
