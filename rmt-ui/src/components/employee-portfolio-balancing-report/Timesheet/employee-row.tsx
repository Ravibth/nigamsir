import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import "./style.css";
import {
  IEmployeeLeaveHolidayAndAvailablity,
  IEmployeeModel,
} from "../../../common/interfaces/IEmployeeModel";
import { getAvailablityPercentage } from "../util/util";
import { HR_ABBR } from "../../../global/constant";
import { routeToEmployeeProfile } from "../../../global/utils";

interface EmployeeRowProps {
  employee: {
    email_id: string;
    email_id_uid: string;
    employee_mid: string;
    name: string;
    totalTimelineAllocationHrs: number;
    totalTimelineLeaveHrs: number;
    totalTimelineHolidayHrs: number;
    totalTimelineAvailablity: number;
  };
  isExpanded: boolean;
  onToggle: (employeeId: string) => void;
}

const employeeRowStyle={
  cursor:'pointer', color: "blue", textDecoration: "underline"
}

const EmployeeRow: React.FC<EmployeeRowProps> = ({
  employee,
  isExpanded,
  onToggle,
}) => {
  return (
    <div className="employee-row">
      <div className="employee-info">
        <div style={employeeRowStyle} className="employee-name" onClick={()=>{routeToEmployeeProfile(`/employee-profile/${employee.email_id_uid}`);}}>{employee.name}</div>
        <div className="employee-availability">
          {employee.totalTimelineAvailablity -
            (employee.totalTimelineHolidayHrs +
              employee.totalTimelineLeaveHrs +
              employee.totalTimelineAllocationHrs)}{" "}
          {HR_ABBR} -{" "}
          {getAvailablityPercentage(
            employee.totalTimelineAvailablity,
            employee.totalTimelineAllocationHrs,
            employee.totalTimelineLeaveHrs,
            employee.totalTimelineHolidayHrs
          )}
          %
        </div>
      </div>
      <button
        onClick={() => onToggle(employee.employee_mid)}
        className="toggle-btn"
        aria-label="Toggle details"
      >
        {isExpanded ? <KeyboardArrowDownIcon /> : <KeyboardArrowRightIcon />}
      </button>
    </div>
  );
};

export default EmployeeRow;
