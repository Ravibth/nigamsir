import * as gc from "../../global/constant";

export const GetRemainingDesignations = (
  allDesignations: any[],
  currentDesignation: any[]
) => {
  const designationsInUse = currentDesignation.map((obj) => obj.label?.trim());
  return allDesignations.filter(
    (designation) => !designationsInUse.includes(designation)
  );
};
export const GetAnother = (
  allDesignations: any[],
  currentDesignation: any[]
) => {
  const currDesig = currentDesignation
    ?.filter((d: any) => d.isactive)
    .map((desig) => desig.label?.trim());
  const itemList = allDesignations?.filter(
    (designation) => !currDesig?.includes(designation.label)
  );
  return itemList;
};

export const getProjectSkills = (projectDetails: any) => {
  const skills: any = [];
  projectDetails &&
    projectDetails?.map((item: any) => {
      skills.push({
        id: item.id,
        label: item?.skillName ? item?.skillName : item?.label,
        isactive: item?.isActive ? item?.isActive : item?.isactive,
      });
    });
  return skills;
};

export const getAllSkills = (masterList: any) => {
  const DelegateLeadersData: any = [];
  masterList
    ?.filter(
      (ELItem: any) => ELItem.recordType == gc.ROLE_TYPE.skill.toString()
    )
    .map((item: any) => {
      DelegateLeadersData.push({
        id: item.id,
        label: item.recordDisplayName,
        role: item.role,
        internalName: item.recordInternalName,
        isactive: true,
      });
    });
  return DelegateLeadersData;
};
