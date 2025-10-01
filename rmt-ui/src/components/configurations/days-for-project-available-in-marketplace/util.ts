export const GetDataForMarketPlace = (marketplaceDbData: any[]) => {
  let marketplacePreferenceData: any[] = [];
  marketplaceDbData.forEach((data, index) => {
    // console.log(data);
    data.projectConfigurations.map((item: any, idx: number) => {
      marketplacePreferenceData.push({
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
  // console.log(marketplacePreferenceData);
  // marketplacePreferenceData = marketplaceDbData.map((data: any) => {
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
  return marketplacePreferenceData.sort((a, b) =>
    a.attributeName.localeCompare(b.attributeName)
  );
};

export const GetUpdatePayload = (
  marketplacePreferenceData: any[],
  configurationType: string
) => {
  let marketplaceUpdatePayload: any[] = [];
  let groupMap = new Map();
  let bank: any[] = [];
  // console.log(marketplacePreferenceData);

  marketplacePreferenceData.map((item: any) => {
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
  // console.log(groupMap);
  // marketplacePreferenceData?.map((data: any) => {
  //   if (groupMap && groupMap.size > 0 && groupMap.has(data.configKey)) {
  //     let d = groupMap.get(data.configKey);
  //     let item = {
  //       id: data.id,
  //       configId: data.configId,
  //       attributeName: data.attributeName,
  //       attributeValue: data.attributeValue,
  //     };
  //     console.log(d);
  //     // d?.push(item);
  //     groupMap.set(data.configKey, d);
  //   } else {
  //     let blank: any[] = [];
  //     let item = {
  //       id: data.configId,
  //       configGroup: data.configGroup,
  //       configGroupDisplay: data.configGroupDisplayText,
  //       configKey: data.configKey,
  //       congigDisplayText: data.configKeyDisplayText,
  //       valueType: data.valueType,
  //       configType: data.configType,
  //       isAll: data.isAll,
  //       allValue: data.allValue,
  //       projectConfigurations: [
  //         {
  //           id: data.id,
  //           configId: data.configId,
  //           attributeName: data.attributeName,
  //           attributeValue: data.attributeValue,
  //         },
  //       ],
  //     };
  //     groupMap.set(data.configKey, blank.push(item));
  //   }
  //   console.log(groupMap);
  // });
  // console.log(groupMap);
  // marketplaceUpdatePayload = marketplacePreferenceData.map((data: any) => {
  //   return {
  //     id: data.id,
  //     configId: data.configId,
  //     attributeName: data.attributeName,
  //     attributeValue: data.attributeValue,
  //     configurationGroup: {
  //       id: data.configId,
  //       configGroup: data.configGroup,
  //       configGroupDisplay: data.configGroupDisplayText,
  //       configKey: data.configKey,
  //       congigDisplayText: data.configKeyDisplayText,
  //       valueType: data.valueType,
  //       configType: data.configType,
  //       isAll: data.isAll,
  //       allValue: data.allValue,
  //     },
  //   };
  // });
  return updatePayload;
};
