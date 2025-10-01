import React, {
  Dispatch,
  SetStateAction,
  createContext,
  useState,
} from "react";
import { IBUTreeMappingListByMID } from "../common/interfaces/IBUTreeMappingListByMID";

export interface ProjectPermissionData {
  pipelineCode: string;
  jobCode: string;
  projectRoles: any[];
  projectRolesView: any[];
  permissions: any[];
}
export interface IUserDetailsContext {
  name: string;
  username: string;
  serviceLine: string;
  designation: string;
  grade?: string;
  role: string[];
  modulePermissionsState: string;
  editMode: boolean;
  pageName: string;
  isEmployee: boolean;
  isDelegate: boolean;
  navList: any[];
  employee_id: string;
  competency: string;
  competencyId: string;
  buTreeMappingListByMID: IBUTreeMappingListByMID;
  setIsEmployee: Dispatch<SetStateAction<boolean>>;
  setPageName: Dispatch<SetStateAction<string>>;
  setName: Dispatch<SetStateAction<string>>;
  setUserName: Dispatch<SetStateAction<string>>;
  setserviceLine: Dispatch<SetStateAction<string>>;
  setDesignation: Dispatch<SetStateAction<string>>;
  setGrade: Dispatch<SetStateAction<string>>;
  setRole: Dispatch<SetStateAction<any[]>>;
  setEditMode: Dispatch<SetStateAction<boolean>>;
  dispatchModulePermissions: Dispatch<SetStateAction<string>>;
  setIsDelegate: Dispatch<SetStateAction<boolean>>;
  setNavList: Dispatch<SetStateAction<any[]>>;
  projectPermissionData: ProjectPermissionData;
  setProjectPermissionData: Dispatch<SetStateAction<ProjectPermissionData>>;
  setEmployeeId: Dispatch<SetStateAction<string>>;
  setCompetency: Dispatch<SetStateAction<string>>;
  setCompetencyId: Dispatch<SetStateAction<string>>;
  setBuTreeMappingListByMID: Dispatch<SetStateAction<IBUTreeMappingListByMID>>;
}
const initialState: IUserDetailsContext = {
  name: "",
  username: "",
  serviceLine: "",
  designation: "",
  grade: "",
  role: [],
  modulePermissionsState: "",
  editMode: false,
  isEmployee: true,
  pageName: "",
  isDelegate: false,
  navList: [],
  employee_id: "",
  competency: "",
  competencyId: "",
  buTreeMappingListByMID: null,
  setIsEmployee: (d: any) => {},
  setDesignation: (d: any) => {},
  setGrade: (d: any) => {},
  setEditMode: (d: any) => {},
  setName: (d: any) => {},
  dispatchModulePermissions: (d: any) => {},
  setPageName: (d: any) => {},
  setserviceLine: (d: any) => {},
  setRole: (d: any) => {},
  setUserName: (d: any) => {},
  setIsDelegate: (d: any) => {},
  setNavList: (d: any) => {},
  projectPermissionData: null,
  setProjectPermissionData: (d: ProjectPermissionData) => {},
  setEmployeeId: (d: any) => {},
  setCompetency: (d: any) => {},
  setCompetencyId: (d: any) => {},
  setBuTreeMappingListByMID: (d: IBUTreeMappingListByMID) => {},
};
export const UserDetailsContext =
  createContext<IUserDetailsContext>(initialState);

export const UserDetailsState: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [name, setName] = useState("");
  const [username, setUserName] = useState("");
  const [serviceLine, setserviceLine] = useState("");
  const [pageName, setPageName] = useState("");
  const [competency, setCompetency] = useState("");
  const [competencyId, setCompetencyId] = useState("");
  const [buTreeMappingListByMID, setBuTreeMappingListByMID] = useState(null);
  const [isEmployee, setIsEmployee] = useState(false);
  const [designation, setDesignation] = useState("");
  const [grade, setGrade] = useState("");
  const [role, setRole] = useState<string[]>([]);
  const [editMode, setEditMode] = useState(false);
  const [modulePermissionsState, dispatchModulePermissions] = useState("");
  const [isDelegate, setIsDelegate] = useState(false);
  const [navList, setNavList] = useState([]);
  const [projectPermissionData, setProjectPermissionData] = useState(null);
  const [employee_id, setEmployeeId] = useState("");
  return (
    <UserDetailsContext.Provider
      value={{
        name,
        username,
        isDelegate,
        buTreeMappingListByMID,
        serviceLine,
        designation,
        grade,
        role,
        modulePermissionsState,
        editMode,
        pageName,
        isEmployee,
        navList,
        employee_id,
        competency,
        competencyId,
        setIsDelegate,
        setIsEmployee,
        setPageName,
        setName,
        setUserName,
        setserviceLine,
        setDesignation,
        setGrade,
        setRole,
        setEditMode,
        dispatchModulePermissions,
        setNavList,
        projectPermissionData,
        setProjectPermissionData,
        setEmployeeId,
        setCompetency,
        setCompetencyId,
        setBuTreeMappingListByMID,
      }}
    >
      {children}
    </UserDetailsContext.Provider>
  );
};
