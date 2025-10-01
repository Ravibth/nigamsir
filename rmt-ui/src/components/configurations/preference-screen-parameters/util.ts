export const GetDataForEmployeeMaxNoOfPreference = (
  employeeMaxNoOfPreferenceDbData: any[]
) => {
  let employeeMaxNoOfPreferenceData: any[] = [];
  employeeMaxNoOfPreferenceDbData.forEach((data, index) => {
    console.log(data);
    data.projectConfigurations.map((item: any, idx: number) => {
      employeeMaxNoOfPreferenceData.push({
        id: item.id,
        configId: item.configId,
        configGroup: data.configGroup,
        configGroupDisplayText: data.configGroupDisplay,
        isAll: data.isAll,
        allValue: data.allValue,
        attributeName: item.attributeName,
        configKey: data.configKey,
        configKeyDisplayText: data.congigDisplayText,
        configType: data.configType,
        valueType: data.valueType,
        attributeValue: item.attributeValue
          ? item.attributeValue
          : data.allValue,
      });
    });
  });
  console.log(employeeMaxNoOfPreferenceData);

  return employeeMaxNoOfPreferenceData.sort((a, b) =>
    a.attributeName.localeCompare(b.attributeName)
  );
};

export const GetUpdatePayload = (
  employeeMaxNoOfPreferenceData: any[],
  configurationType: string
) => {
  let bank: any[] = [];
  //   console.log(employeeMaxNoOfPreferenceData);

  employeeMaxNoOfPreferenceData.map((item: any) => {
    let index = bank.findIndex(
      (data: any) =>
        data?.configKey?.toString().toUpperCase().trim() ===
        item?.configKey?.toString().toUpperCase().trim()
    );
    if (index === -1) {
      let data = {
        id: item.configId,
        configGroup: item.configGroup,
        configGroupDisplay: item.configGroupDisplayText,
        configKey: item.configKey,
        congigDisplayText: item.configKeyDisplayText,
        valueType: item.valueType,
        configType: item.configType,
        isAll: item.isAll,
        allValue: item.allValue,
        projectConfigurations: [
          {
            id: item.id,
            configId: item.configId,
            attributeName: item.attributeName,
            attributeValue: item.attributeValue,
          },
        ],
      };
      bank.push(data);
    } else {
      let ex = bank[index].projectConfigurations;
      ex.push({
        id: item.id,
        configId: item.configId,
        attributeName: item.attributeName,
        attributeValue: item.attributeValue,
      });
    }
  });
  console.log(bank);
  let updatePayload = {
    configrationGroupDtos: bank,
    configurationType: configurationType,
  };
  return updatePayload;
};
