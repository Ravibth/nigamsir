import React, {
  Dispatch,
  SetStateAction,
  createContext,
  useState,
} from "react";

// To refresh start date, end date and efforts validations for their minimum and maximum
export interface AllocationContextProps {
  updated: any;
  isUpdated: Dispatch<SetStateAction<boolean>>;
}
const initialState: AllocationContextProps = {
  updated: function (value: any): void {},
  isUpdated: (d: any) => {},
};

export const AllocationContext =
  createContext<AllocationContextProps>(initialState);

export const AllocationContextState: React.FC<{
  children: React.ReactNode;
}> = ({ children }) => {
  const [updated, isUpdated] = useState<boolean>(false);
  return (
    <AllocationContext.Provider
      value={{
        isUpdated,
        updated,
      }}
    >
      {children}
    </AllocationContext.Provider>
  );
};
