export const getFilterDataForRequisition = (requisitionDBData: any[]) => {
  const clientNameSet = new Set();
  const expertiseSet = new Set();
  const smeSet = new Set();
  const pipelineSet = new Set();
  const jobSet = new Set();

  requisitionDBData.forEach((data) => {
    if (data.clientName) {
      clientNameSet.add(data.clientName);
    }
    if (data.expertise) {
      expertiseSet.add(data.expertise);
    }
    if (data.sme) {
      smeSet.add(data.sme);
    }
    if (data.pipelineCode) {
      pipelineSet.add(data.pipelineCode + " - " + data.pipelineName);
    }
    // if (isEmployee) {
    //   if (data.jobCode) {
    //     jobSet.add(data.jobCode);
    //   }
    // }
    else {
      if (
        data.projectCode &&
        data.pipelineCode &&
        data.projectCode !== data.pipelineCode
      ) {
        let jobCode = data.projectCode;
        jobSet.add(jobCode);
      } else if (
        data.projectCode &&
        data.pipelineCode &&
        data.projectCode.toString().toUpperCase() ===
          data.pipelineCode.toString().toUpperCase()
      ) {
        let jobCode = data.projectCode;
        jobSet.add(jobCode);
      }
    }
  });
  const distinctClientNames = Array.from(clientNameSet);
  const distinctExpertises = Array.from(expertiseSet);
  const distinctSmes = Array.from(smeSet);
  const distinctPipelines = Array.from(pipelineSet);
  const distinctJobs = Array.from(jobSet);
  return {
    distinctClientNames,
    distinctExpertises,
    distinctSmes,
    distinctPipelines,
    distinctJobs,
  };
};

export const filteredRequisitionList = (
  requisitionList: any[],
  filterParameters: any
) => {
  if (
    filterParameters &&
    (filterParameters.designation_name === undefined ||
      filterParameters.designation_name.length === 0) &&
    (filterParameters.experties === undefined ||
      filterParameters.experties.length === 0) &&
    (filterParameters.sme === undefined || filterParameters.sme.length === 0) &&
    (filterParameters.startDate === undefined ||
      filterParameters.startDate.length === 0) &&
    (filterParameters.endDate === undefined ||
      filterParameters.endDate.length === 0) &&
    (filterParameters.businessUnit === undefined ||
      filterParameters.businessUnit.length === 0) &&
    (filterParameters.revenueUnit === undefined ||
      filterParameters.revenueUnit.length === 0)
  ) {
    return requisitionList;
  }
  let requisitiontListWithFilter = requisitionList;
  const list = requisitiontListWithFilter.filter((data) => {
    if (
      // (filterParameters.designation &&
      //   filterParameters.designation.findIndex(
      //     (e: any) =>
      //       e.label.toUpperCase().trim() ===
      //       data.designation.toUpperCase().trim()
      //   ) !== -1)
      (filterParameters.designation_name &&
        filterParameters.includes(data.designation_name.toUpperCase())) ||
      (filterParameters.experties &&
        filterParameters.experties.includes(data.experties)) ||
      (filterParameters.sme && filterParameters.sme.includes(data.sme)) ||
      (filterParameters.startDate &&
        filterParameters.startDate <= data.startDate &&
        filterParameters.endDate &&
        filterParameters.endDate >= data.endDate) ||
      (filterParameters.businessUnit &&
        filterParameters.businessUnit.includes(data.businessUnit)) ||
      (filterParameters.revenueUnit &&
        filterParameters.revenueUnit.includes(data.revenueUnit))
    ) {
      return true;
    } else {
      return false;
    }
  });
  return list;
};
