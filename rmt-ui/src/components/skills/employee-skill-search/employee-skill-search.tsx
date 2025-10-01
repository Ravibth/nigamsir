import { GetUserSkillsWithProficiency } from "../../../services/skills/userSkills.service";
import Employeesearch from "../../AllocationSearchFilter/EmployeeSearch/employeesearch";
import { mergeSearchResults } from "../skills-search/util";

interface IEmployeeSkillSearch {
  setEmployeeSkillResults: React.Dispatch<React.SetStateAction<any[]>>;
}

const EmployeeSkillSearch = (props: IEmployeeSkillSearch) => {
  const employeeSelected = async (user: any) => {
    if (user) {
      const userSkillsFound = await GetUserSkillsWithProficiency(user.emailId);
      const userInfo = {
        ...user,
        email_id: user.emailId,
        business_unit: user.BusinessUnit,
      };
      const mergedResult = mergeSearchResults(userSkillsFound, [userInfo]);
      props.setEmployeeSkillResults(mergedResult);
    }
  };

  return (
    <Employeesearch
      employeeSelected={employeeSelected}
      showDifferentTypeSelections={false}
    />
  );
};
export default EmployeeSkillSearch;
