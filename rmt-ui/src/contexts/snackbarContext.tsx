import React, { createContext, useState } from "react";
import SnackbarShared from "../components/snack-bar/snack-bar";

export enum SnackbarSeverity {
  SUCCESS = "success",
  ERROR = "error",
  WARNING = "warning",
}

//User Allocations Messages
export enum SnackbarMessages {
  UserAllocationRequestSubmitted = "User Allocation Request Submitted!",
  UserAllocationRequestFailed = "User Allocation Request Failed!",
  AllocatingUserFailed = "Error Allocating User",
  AllocatingUserSubmitted = "Successfully Allocated User",
  ErrorFetchingUserTimelines = "Error Fetching User Timelines",
}

export const SnackbarContext: any = createContext<SnackbarContextProps>({
  displaySnackbar: () => {},
  closeSnackbar: () => {},
});

export interface SnackbarContextProps {
  displaySnackbar: (
    messageArg: string,
    severityArg: string,
    autoHideDuration?: number
  ) => void;
  closeSnackbar: () => void;
}
interface SnackbarStateProps {
  children: React.ReactNode;
}
export const SnackbarState: React.FC<SnackbarStateProps> = (props) => {
  const [openSnackbar, setOpenSnackbar] = useState<boolean>(false);
  const [autoHideDuration, setautoHideDuration] = useState<number>(6000);
  const [severity, setseverity] = useState<string>("success");
  const [message, setmessage] = useState<string>("");
  const handleClose = () => {
    setOpenSnackbar(false);
    setTimeout(() => {
      setseverity("");
      setmessage("");
    }, 100);
  };

  const displaySnackbar = (
    messageArg: string,
    severityArg: string,
    autoHideDurationArg: number = 6000
  ) => {
    setmessage(messageArg);
    setseverity(severityArg);
    setOpenSnackbar(true);
    setautoHideDuration(autoHideDurationArg);
  };

  const closeSnackbar = () => {
    handleClose();
  };

  return (
    <SnackbarContext.Provider
      value={{
        displaySnackbar,
        closeSnackbar,
      }}
    >
      <SnackbarShared
        open={openSnackbar}
        autoHideDuration={autoHideDuration}
        severity={severity}
        message={message}
        handleClose={handleClose}
      />
      {props.children}
    </SnackbarContext.Provider>
  );
};
