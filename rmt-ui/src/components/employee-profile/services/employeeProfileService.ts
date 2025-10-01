import axios, { AxiosResponse } from "axios";
import { EmployeeProfile } from "../interfaces/employeeProfile";
const baseurl = process.env.REACT_APP_EMPLOYEE_PREFERENCE;

export const getEmployeeProfile = async (email: string): Promise<EmployeeProfile> => {
  try {
    const response: AxiosResponse<EmployeeProfile> = await axios.get(
      `${baseurl}Employee/get-employee-profile/${encodeURIComponent(email)}`
    );

    return response.data;
  } catch (err) {
    throw err;
  }
};


export const SaveEmployeeProfile = async (userEmail, tempData, profileData, updateProfileData, updateLinkedInURL) => {
  try {


    const payload = {
      employee_email: userEmail,
      about_me: tempData.aboutMe,
      industry_experience: tempData.industry,
      project_experience: tempData.project,
      linkedin_url: tempData.linkedIn,
      modified_by: userEmail,
      year_of_experience: tempData.yearsOfExp,
      qualification_update: tempData.qualification_update,
      present_work_location: tempData.present_work_location,
      experience_outside_gt:tempData.experience_outside_gt,
    };

    console.log("payload")
    console.log(payload)

    const response = await axios.post(baseurl + `Employee/update-employee-profile`, payload);


    if (response.status !== 200) {
      throw new Error('Failed to update profile');
    }

    if (profileData) {
      updateProfileData({
        ...profileData,
        about_me: tempData.aboutMe,
        industry_experience: tempData.industry,
        project_experience: tempData.project,
        modified_at: new Date().toISOString(),
        year_of_experience: tempData.yearsOfExp,
      });
    }

    updateLinkedInURL(tempData.linkedIn);

    return true;
  } catch (err) {
    console.error('Error updating profile:', err);
    return false;
  }
};


