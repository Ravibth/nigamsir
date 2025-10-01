import React, {
  Dispatch,
  SetStateAction,
  createContext,
  useState,
} from "react";
export interface IProjectUpdateDetailsContext {
  id: number;
  modifiedBy: string;
  modifiedAt: string;
  projectUpdateDemands: any[];
  projectUpdateEL: any[];
  projectUpdateDelegate: any[];
  projectUpdateSkills: any[];
  projectUpdateDescription: string;
  projectUpdateEndDate: string;
  projectUpdateIsEndDateChanged: boolean;
  projectUpdateIsEndDateUpdated: boolean;
  projectRoleAdditionalData: any[];
  projectRoleAdditionalDBData: any[];
  projectUiRolesData: any[];
  isSelectedDateInvalid: boolean;
  isProjectDetailsDirty: boolean;
  confidential: boolean;
  setId: Dispatch<SetStateAction<number>>;
  setModifiedBy: Dispatch<SetStateAction<string>>;
  setModifiedAt: Dispatch<SetStateAction<string>>;
  setProjectUpdateDemands: Dispatch<SetStateAction<any[]>>;
  setProjectUpdateEL: Dispatch<SetStateAction<any[]>>;
  setProjectUpdateDelegate: Dispatch<SetStateAction<any[]>>;
  setProjectUpdateSkills: Dispatch<SetStateAction<any[]>>;
  setProjectUpdateDescription: Dispatch<SetStateAction<string>>;
  setProjectUpdateEndDate: Dispatch<SetStateAction<any>>;
  setProjectUpdateIsEndDateChanged: Dispatch<SetStateAction<boolean>>;
  setProjectUpdateIsEndDateUpdated: Dispatch<SetStateAction<boolean>>;
  setProjectRoleAdditionalData: Dispatch<SetStateAction<any[]>>;
  setProjectRoleAdditionalDBData: Dispatch<SetStateAction<any[]>>;
  setProjectUiRolesData: Dispatch<SetStateAction<any[]>>;
  setIsProjectDetailsDirty: Dispatch<SetStateAction<boolean>>;
  setIsSelectedDateInvalid: Dispatch<SetStateAction<boolean>>;
  setConfidential: Dispatch<SetStateAction<boolean>>;
}
const initialState: IProjectUpdateDetailsContext = {
  id: 0,
  modifiedBy: "",
  modifiedAt: "",
  projectUpdateDemands: [],
  projectUpdateEL: [],
  projectUpdateDelegate: [],
  projectUpdateSkills: [],
  projectUpdateDescription: "",
  projectUpdateEndDate: "",
  projectUpdateIsEndDateChanged: false,
  projectUpdateIsEndDateUpdated: false,
  projectRoleAdditionalData: [],
  projectRoleAdditionalDBData: [],
  projectUiRolesData: [],
  isProjectDetailsDirty: false,
  isSelectedDateInvalid: false,
  confidential: false,
  setId: (d: any) => {},
  setModifiedBy: (d: any) => {},
  setModifiedAt: (d: any) => {},
  setProjectUpdateDemands: (d: any) => {},
  setProjectUpdateEL: (d: any) => {},
  setProjectUpdateDelegate: (d: any) => {},
  setProjectUpdateSkills: (d: any) => {},
  setProjectUpdateDescription: (d: any) => {},
  setProjectUpdateEndDate: (d: any) => {},
  setProjectUpdateIsEndDateChanged: (d: boolean) => {},
  setProjectUpdateIsEndDateUpdated: (d: boolean) => {},
  setProjectRoleAdditionalData: (d: any) => {},
  setProjectRoleAdditionalDBData: (d: any) => {},
  setProjectUiRolesData: (d: any) => {},
  setIsProjectDetailsDirty: (d: any) => {},
  setIsSelectedDateInvalid: (d: any) => {},
  setConfidential: (d: any) => {},
  
};
export const ProjectUpdateDetailsContext =
  createContext<IProjectUpdateDetailsContext>(initialState);

export const ProjectUpdateDetailsState = (props) => {
  const [id, setId] = useState(0);
  const [modifiedBy, setModifiedBy] = useState("");
  const [modifiedAt, setModifiedAt] = useState("");
  const [projectUpdateDemands, setProjectUpdateDemands] = useState([]);
  const [projectUpdateEL, setProjectUpdateEL] = useState([]);
  const [projectUpdateDelegate, setProjectUpdateDelegate] = useState([]);
  const [projectUpdateSkills, setProjectUpdateSkills] = useState([]);
  const [projectUpdateDescription, setProjectUpdateDescription] = useState("");
  const [projectUpdateEndDate, setProjectUpdateEndDate] = useState("");
  const [projectUpdateIsEndDateChanged, setProjectUpdateIsEndDateChanged] =
    useState(false);
  const [projectUpdateIsEndDateUpdated, setProjectUpdateIsEndDateUpdated] =
    useState(false);
  const [projectRoleAdditionalData, setProjectRoleAdditionalData] = useState(
    []
  );
  const [projectUiRolesData, setProjectUiRolesData] = useState([]);
  const [projectRoleAdditionalDBData, setProjectRoleAdditionalDBData] =
    useState([]);
  const [isProjectDetailsDirty, setIsProjectDetailsDirty] =
    useState<boolean>(false);
  const [isSelectedDateInvalid, setIsSelectedDateInvalid] =
    useState<boolean>(false);
  const [confidential, setConfidential] = useState(false);
  return (
    <ProjectUpdateDetailsContext.Provider
      value={{
        id,
        modifiedBy,
        modifiedAt,
        projectUpdateDemands,
        projectUpdateDelegate,
        projectUpdateEL,
        projectUpdateSkills,
        projectUpdateDescription,
        projectUpdateEndDate,
        projectUpdateIsEndDateChanged,
        projectUpdateIsEndDateUpdated,
        projectRoleAdditionalData,
        projectRoleAdditionalDBData,
        projectUiRolesData,
        isProjectDetailsDirty,
        isSelectedDateInvalid,
        confidential,
        setId,
        setModifiedAt,
        setModifiedBy,
        setProjectUpdateDemands,
        setProjectUpdateDelegate,
        setProjectUpdateEL,
        setProjectUpdateSkills,
        setProjectUpdateDescription,
        setProjectUpdateEndDate,
        setProjectUpdateIsEndDateChanged,
        setProjectUpdateIsEndDateUpdated,
        setProjectRoleAdditionalData,
        setProjectRoleAdditionalDBData,
        setProjectUiRolesData,
        setIsProjectDetailsDirty,
        setIsSelectedDateInvalid,
        setConfidential
      }}
    >
      {props.children}
    </ProjectUpdateDetailsContext.Provider>
  );
};
