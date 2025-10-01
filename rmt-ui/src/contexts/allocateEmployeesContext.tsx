import React, { createContext, useState } from "react";
import { IUserInfo } from "../components/system-suggestions/availability-view/constants";
export const AllocateEmployeesContext: any = createContext({});

export const AllocateEmployeesState = (props: any) => {
  const [selectedUsers, setSelectedUsers] = useState<IUserInfo[]>([]);

  return (
    <AllocateEmployeesContext.Provider
      value={{
        selectedUsers,
        setSelectedUsers,
      }}
    >
      {props.children}
    </AllocateEmployeesContext.Provider>
  );
};
