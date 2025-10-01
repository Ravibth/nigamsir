import axios from 'axios';
import { getAllCompetency, getAllSupercoachLists, getBU_Exp_SME_RUFromWcgt, getDesignationList, getEmployeeLists, getLocationMasterFromWCGT } from '../../services/wcgt-master-services/wcgt-master-services';
import { getAllUsers } from '../../services/role-permission-service/role-permission-service';
import { DelegateViewData } from './additional-delegate';
const baseurl = process.env.REACT_APP_IDENTITY;

export interface ISuperCoachModel {
    supercoach_mid: string;
    email_id: string;
    name: string;
    designation: string | null;
    location: string | null;
    business_unit: string | null;
    competency: string | null;
    grade: string | null;
    allocation_delegate_mid: string | null;
    allocation_delegate_email: string | null;
    allocation_delegate_name: string | null;
    skill_delegate_mid: string | null;
    skill_delegate_email: string | null;
    skill_delegate_name: string | null;
}

export const getSuperCoachList = async (payload?: any): Promise<ISuperCoachModel[]> => { 
  try {     
      const business_unit = payload?.businessunit?.map(a => a.label) ?? [];
      const competency = payload?.competency?.map(a => a.label) ?? [];
      const designation = payload?.designation?.map(a => a.label) ?? [];
      const grade = payload?.grade?.map(a => a.label) ?? [];
      const location = payload?.location?.map(a => a.label) ?? [];
      const employee_mid = payload?.employees?.map(a => a.id) ?? [];
    const resp = await axios.post(
      baseurl + "identity/user/supercoach-and-delegates-list",
      {
        competency,
        business_unit,
        designation,
        grade,
        location,
        employee_mid
      }      
    );
    return resp.data;
  } catch (error) {
    console.log("Error fetching super coach list:", error);
    throw error;
  }
};

export const saveSuperCoachDelegate = async function saveSuperCoachDelegate(payload){
  const response = await axios.post(
        baseurl+'identity/user/add-supercoach-delegate',
        payload
    );

    return response;
}

export const getFilerOption =async ()=> {
  return await Promise.all([getBU_Exp_SME_RUFromWcgt(),getAllCompetency(), getDesignationList(), getLocationMasterFromWCGT(), getAllSupercoachLists()]);
}

  // Get All Users
export const getListIOfAllUsers = (): Promise<DelegateViewData[]> => {
  return new Promise((resolve, reject) => {
    getAllUsers()
      .then((resp) => {
        resolve(resp as DelegateViewData[]); // Cast if `getAllUsers()` isn't typed
      })
      .catch((err) => {
        console.error("Error Fetching Users", err);
        reject(err); // Important: Propagate the error
      });
  });
};

    
