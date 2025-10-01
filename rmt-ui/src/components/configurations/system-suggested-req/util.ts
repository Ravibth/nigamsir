export const getAllAttributTypes = (suggestionConfigData: any[]) => {
  const attributNameSet = new Set();
  attributNameSet.add("ALL");
  suggestionConfigData?.map((item) =>
    attributNameSet.add(item.attributeName?.toString()?.trim()?.toUpperCase())
  );
  const sortedSet = Array.from(attributNameSet).sort();
  const newSorted = sortedSet?.filter((data) => data !== "ALL");
  return ["ALL", ...newSorted];
};

export const GetDataForSystemSuggestedConfig = (
  systemSuggestedDbData: any[]
) => {
  let systemSuggestedConfigData: any[] = [];

  console.log(systemSuggestedDbData);

  systemSuggestedDbData.forEach((data, index) => {
    console.log(data);
    data.projectConfigurations.map((item: any, idx: number) => {
      systemSuggestedConfigData.push({
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

  // systemSuggestedConfigData = systemSuggestedDbData.map((data) => {
  //   return {
  //     id: data.id,
  //     configId: data.configId,
  //     configGroup: data.configurationGroup.configGroup,
  //     configGroupDisplayText: data.configurationGroup.configGroupDisplay,
  //     isAll: data.configurationGroup.isAll,
  //     allValue: data.configurationGroup.allValue,
  //     attributeName: data.attributeName,
  //     configKey: data.configurationGroup.configKey,
  //     configKeyDisplayText: data.configurationGroup.congigDisplayText,
  //     configType: data.configurationGroup.configType,
  //     valueType: data.configurationGroup.valueType,
  //     attributeValue: data.attributeValue,
  //   };
  // });
  const data = GetAllDataForSystemSuggestedConfig(systemSuggestedConfigData);

  return [...data, ...systemSuggestedConfigData];
};

export const GetAllDataForSystemSuggestedConfig = (
  systemSuggestionData: any[]
) => {
  const dataMap = new Map();
  systemSuggestionData.map((item: any) => {
    dataMap.set(item.configKey, {
      allValue: item.allValue,
      configGroup: item.configGroup,
      id: 0,
      attributeName: "ALL",
      configGroupDisplayText: item.configGroupDisplayText,
      configId: item.configId,
      configKey: item.configKey,
      configKeyDisplayText: item.configKeyDisplayText,
      configType: item.configType,
      attributeValue: item.allValue,
      valueType: item.valueType,
      isAll: item.isAll,
    });
  });
  console.log(Array.from(dataMap.values()));
  return Array.from(dataMap.values());
};

export const GetUpdatePayloadForSystemSuggestedConfig = (
  systemSuggestedData: any[],
  configurationType: string
) => {
  let updatedPayload: any[] = [];

  let bank: any[] = [];
  console.log(systemSuggestedData);

  systemSuggestedData.map((item: any) => {
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
        isAll: false,
        allValue: item.allValue,
        projectConfigurations: [
          {
            id: item.id,
            configId: item.configId,
            attributeName: item.attributeName,
            //attributeValue: item.attributeValue,
            //if value changed from false to true set to default value
            attributeValue:
              item.attributeValue == "true"
                ? (item.allValue as string)
                : item.attributeValue,
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
        //attributeValue: item.attributeValue,
        //if value changed from false to true set to default value
        attributeValue:
          item.attributeValue == "true"
            ? (item.allValue as string)
            : item.attributeValue,
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
