import React, { useEffect, useState } from 'react'
import { EmployeeProfile, EmployeeQualification } from '../interfaces/employeeProfile';

export default function useTrainingEduData(mainProfileData: EmployeeProfile,qualification_type:string) {

  const [data, setData] = useState<EmployeeQualification[]>();

  useEffect(() => {
    
       if (!mainProfileData?.employee_qualification) return;
      let temp: EmployeeQualification[] = mainProfileData.employee_qualification.filter(x => x.qualification_type == qualification_type)
        .map(item => {
          const parts = item.institute_location_name.split(',');

          return {
            ...item,
            institute_name: parts[0]?.trim() || '-',
            institute_location_name: parts[1]?.trim() || '-',
          }

        })      
      setData(temp);
    
  }, [mainProfileData.employee_qualification, qualification_type]);

  return {data};
}