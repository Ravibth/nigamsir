import React, {
  Dispatch,
  SetStateAction,
  createContext,
  useState,
} from "react";
import Loader from "../components/loader/loader";

export interface LoaderContextProps {
  open: Dispatch<SetStateAction<boolean>>;
  isOpen: boolean;
}

interface LoaderStateProps {
  children: React.ReactNode;
}
export const LoaderContext: any = createContext<LoaderContextProps>({
  open: () => {},
  isOpen: false,
});

export const LoaderState: React.FC<LoaderStateProps> = (props) => {
  const [openLoader, open] = useState<boolean>(false);

  return (
    <LoaderContext.Provider
      value={{
        open,
        isOpen: openLoader,
      }}
    >
      {openLoader ? <Loader /> : <></>}
      {props.children}
    </LoaderContext.Provider>
  );
};
