import { createContext, useState } from "react";
import { IGetAllMySkillsResponse } from "../../../../services/skills/userSkills.service";

export interface IMySkillGridContextProps {
  currentEditingField: IGetAllMySkillsResponse | null;
  setCurrentEditingField: React.Dispatch<
    React.SetStateAction<IGetAllMySkillsResponse | null>
  >;
}

const initialState: IMySkillGridContextProps = {
  currentEditingField: null,
  setCurrentEditingField: (d: any) => {},
};

export const MySkillsGridContext =
  createContext<IMySkillGridContextProps>(initialState);

export const MySkillsGridState: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [currentEditingField, setCurrentEditingField] =
    useState<IGetAllMySkillsResponse | null>(null);

  return (
    <>
      <MySkillsGridContext.Provider
        value={{
          currentEditingField,
          setCurrentEditingField,
        }}
      >
        {children}
      </MySkillsGridContext.Provider>
    </>
  );
};
