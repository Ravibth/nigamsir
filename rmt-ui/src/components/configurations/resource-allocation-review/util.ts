export const GetDataForResourceAllocation = (dbData: any[]) => {
  let reviewData: any[] = [];
  dbData.forEach((data, index) => {
    console.log(data);
    data.projectConfigurations.map((item: any, idx: number) => {
      let valueTypeJson = JSON.parse(data.valueType);
      let allValueJson = JSON.parse(data.allValue);
      let attributeValueJson =
        item.attributeValue !== "" ? JSON.parse(item.attributeValue) : "";
      //     : `
      //     {"activationStatus":"false","noOfDays":"12"}
      // `
      let allValueObj = {
        activationStatus: JSON?.parse(
          allValueJson?.activationStatus?.toLowerCase()
        ),
        noOfDays: allValueJson.noOfDays,
      };
      let attributeValueObj =
        item.attributeValue !== ""
          ? {
              activationStatus: JSON?.parse(
                attributeValueJson?.activationStatus?.toLowerCase()
              ),
              noOfDays: attributeValueJson.noOfDays,
            }
          : "";
      let attributeValueEmptyObj = {
        activationStatus: false,
        noOfDays: "",
      };
      reviewData.push({
        id: item.id,
        configId: item.configId,
        configGroup: data.configGroup,
        configGroupDisplayText: data.configGroupDisplay,
        isAll: data.isAll,
        allValue: allValueObj,
        attributeName: item.attributeName,
        configKey: data.configKey,
        configKeyDisplayText: data.congigDisplayText,
        configType: data.configType,
        valueType: valueTypeJson,
        attributeValue:
          item.attributeValue !== "" ? attributeValueObj : allValueObj,
      });
    });
  });
  console.log("...", reviewData);
  // reviewData = dbData.map((data: any) => {
  //   let valueTypeJson = JSON.parse(data.configurationGroup.valueType);
  //   let allValueJson = JSON.parse(data.configurationGroup.allValue);
  //   let attributeValueJson = JSON.parse(data.attributeValue);
  //   let allValueObj = {
  //     activationStatus: JSON.parse(allValueJson.activationStatus.toLowerCase()),
  //     noOfDays: parseInt(allValueJson.noOfDays),
  //   };
  //   let attributeValueObj = {
  //     activationStatus: JSON.parse(
  //       attributeValueJson.activationStatus.toLowerCase()
  //     ),
  //     noOfDays: parseInt(attributeValueJson.noOfDays),
  //   };
  //   return {
  //     id: data.id,
  //     configId: data.configId,
  //     configGroup: data.configurationGroup.configGroup,
  //     configGroupDisplayText: data.configurationGroup.configGroupDisplay,
  //     isAll: data.configurationGroup.isAll,
  //     allValue: allValueObj,
  //     attributeName: data.attributeName,
  //     configKey: data.configurationGroup.configKey,
  //     configKeyDisplayText: data.configurationGroup.congigDisplayText,
  //     configType: data.configurationGroup.configType,
  //     valueType: valueTypeJson,
  //     attributeValue: attributeValueObj,
  //   };
  // });
  return reviewData.sort((a, b) =>
    a.attributeName.localeCompare(b.attributeName)
  );
};

export const GetUpdatePayload = (
  resourceAllocationData: any[],
  configurationType: string
) => {
  let resourceAllocationPayload: any[] = [];
  let bank: any[] = [];

  resourceAllocationPayload = resourceAllocationData.map((item: any) => {
    let allValueObj = {
      activationStatus: item.allValue.activationStatus.toString(),
      noOfDays: item.allValue.noOfDays.toString(),
    };
    let attributeValueObj = {
      activationStatus: item.attributeValue.activationStatus.toString(),
      noOfDays: item.attributeValue.noOfDays.toString(),
    };

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
        valueType: JSON.stringify(item.valueType),
        configType: item.configType,
        isAll: item.isAll,
        allValue: JSON.stringify(allValueObj),
        projectConfigurations: [
          {
            id: item.id,
            configId: item.configId,
            attributeName: item.attributeName,
            attributeValue: JSON.stringify(attributeValueObj),
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
        attributeValue: JSON.stringify(attributeValueObj),
      });
    }

    // return {
    //   id: data.id,
    //   configId: data.configId,
    //   attributeName: data.attributeName,
    //   attributeValue: JSON.stringify(attributeValueObj),
    //   configurationGroup: {
    //     id: data.configId,
    //     configGroup: data.configGroup,
    //     configGroupDisplay: data.configGroupDisplayText,
    //     configKey: data.configKey,
    //     congigDisplayText: data.configKeyDisplayText,
    //     valueType: JSON.stringify(data.valueType),
    //     configType: data.configType,
    //     isAll: data.isAll,
    //     allValue: JSON.stringify(allValueObj),
    //   },
    // };
  });
  console.log(bank);
  let updatePayload = {
    configrationGroupDtos: bank,
    configurationType: configurationType,
  };
  return updatePayload;
};
