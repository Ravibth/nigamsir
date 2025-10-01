export enum PREFERENCE_CATEGORY {
  INDUSTRY = "INDUSTRY",
  SUB_INDUSTRY = "SUB_INDUSTRY",
  BUISNESS_UNIT = "BUISNESS_UNIT", //
  EXPERTISE = "EXPERTISE", //
  SMEG = "SMEG", //
  REVENUE_UNIT = "REVENUE_UNIT", //
  LOCATION = "LOCATION",
  // ENGAGEMENT_LEADER = "ENGAGEMENT_LEADER",
}
export const Preference_Display_Order = [
  {
    title: "Location",
    category: PREFERENCE_CATEGORY.LOCATION.toString().toUpperCase(),
  },
  // {
  //   title: "Engagement Leader",
  //   category: PREFERENCE_CATEGORY.ENGAGEMENT_LEADER.toString().toUpperCase(),
  // },
  {
    title: "Industry",
    category: PREFERENCE_CATEGORY.INDUSTRY.toString().toUpperCase(),
  },
  {
    title: "Sub Industry",
    category: PREFERENCE_CATEGORY.SUB_INDUSTRY.toString().toUpperCase(),
  },
  {
    title: "Business Unit",
    category: PREFERENCE_CATEGORY.BUISNESS_UNIT.toString().toUpperCase(),
  },
  {
    title: "Expertise",
    category: PREFERENCE_CATEGORY.EXPERTISE.toString().toUpperCase(),
  },
  {
    title: "SMEG",
    category: PREFERENCE_CATEGORY.SMEG.toString().toUpperCase(),
  },
  // {
  //   title: "Revenue Unit",
  //   category: PREFERENCE_CATEGORY.REVENUE_UNIT.toString().toUpperCase(),
  // },
];
export interface MasterData {
  id: string;
  category: string;
  label: string;
}
export interface EmployeePreferenceData {
  id: string;
  employeeEmail: string;
  preferenceName: string;
  preferenceId: string;
  category: string;
  preferenceOrder: number;
  label: string;
  isActive: boolean;
}
