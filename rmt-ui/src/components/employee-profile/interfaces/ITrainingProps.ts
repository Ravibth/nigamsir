import { EmployeeProfile } from "./employeeProfile";


export interface ITrainingProps extends EmployeeProfile {
    tempData: any;
    setTempData: React.Dispatch<React.SetStateAction<any>>;
    onSave: (section: string) => void;
    isEditable:boolean;
}