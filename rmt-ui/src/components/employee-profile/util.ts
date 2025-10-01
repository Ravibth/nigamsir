import { EmployeeProfile } from "./interfaces/employeeProfile";
//import {IndustryExperience} from "./types/employeeProfile";

export const formatQualifications = (profileData: EmployeeProfile, type) => {
  if (!profileData?.employee_qualification) return '';
  return profileData.employee_qualification
    .filter(q => q.qualification_type === type && q.is_published == true)
    .map(q => q.qualification)
    .join(', ');
};

export const formatLanguages = (profileData: EmployeeProfile) => {
  if (!profileData?.employee_language) return '';
  return profileData.employee_language
    .map(l => l.language_name)
    .join(', ');
};

export const IndustryColumns = [
  { header: 'Industry', field: 'industry_name', width: 3 },
  { header: 'Sub Industry', field: 'sub_industry_name', width: 3 },
  { header: 'Years of Experience', field: 'year_of_experience', width: 2, },//append: ' Years' },
  { header: 'Details', field: 'description', width: 4 }
];

export const projectColumns = [
  { header: 'Client Group', field: 'client_group', width: 2 },
  { header: 'Client Name', field: 'client_name', width: 2 },
  { header: 'Business Unit', field: 'business_unit', width: 2 },
  { header: 'Offering', field: 'offering', width: 2 },
  { header: 'Industry', field: 'industry', width: 2 },
  { header: 'Sub-Industry', field: 'sub_industry', width: 2 }
];

export const formatLink = (url) => {
  if (!url) return '#';
  return url.startsWith('http') ? url : `https://${url}`;
};