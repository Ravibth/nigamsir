import React, {
  Dispatch,
  SetStateAction,
  createContext,
  useState,
} from "react";
import { IAllUserAllocationEntries } from "../interface";
import { IProjectMaster } from "../../../common/interfaces/IProject";

export interface IUserSelectedAllocationContext {
  allUserSelectionEntries: IAllUserAllocationEntries[];
  setAllUserSelectionEntries: Dispatch<
    SetStateAction<IAllUserAllocationEntries[]>
  >;
  usersSelected: IAllUserAllocationEntries[];
  setUsersSelected: Dispatch<SetStateAction<IAllUserAllocationEntries[]>>;
  projectInfo: IProjectMaster;
  setProjectInfo: Dispatch<SetStateAction<IProjectMaster>>;
  removeUserFromTimeline: string;
  setRemoveUserFromTimeline: React.Dispatch<React.SetStateAction<string>>;
}

const initialState: IUserSelectedAllocationContext = {
  allUserSelectionEntries: [],
  setAllUserSelectionEntries: (d: IAllUserAllocationEntries[]) => {},
  usersSelected: [],
  setUsersSelected: (d: IAllUserAllocationEntries[]) => {},
  projectInfo: undefined,
  setProjectInfo: (d: IProjectMaster) => {},
  removeUserFromTimeline: "",
  setRemoveUserFromTimeline: (d: string) => {},
};

export const UserSelectedAllocationContext = createContext(initialState);

export const UserSelectedAllocationState: React.FC<{
  children: React.ReactNode;
}> = ({ children }) => {
  const [allUserSelectionEntries, setAllUserSelectionEntries] = useState<
    IAllUserAllocationEntries[]
  >([]);
  const [usersSelected, setUsersSelected] = useState<
    IAllUserAllocationEntries[]
  >([]);
  const [projectInfo, setProjectInfo] = useState<IProjectMaster>(undefined);
  const [removeUserFromTimeline, setRemoveUserFromTimeline] =
    useState<string>("");

  return (
    <UserSelectedAllocationContext.Provider
      value={{
        allUserSelectionEntries,
        setAllUserSelectionEntries,
        usersSelected,
        setUsersSelected,
        projectInfo,
        setProjectInfo,
        removeUserFromTimeline,
        setRemoveUserFromTimeline,
      }}
    >
      {children}
    </UserSelectedAllocationContext.Provider>
  );
};
