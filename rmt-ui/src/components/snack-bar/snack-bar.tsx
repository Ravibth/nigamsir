import { Alert, IconButton, Snackbar, Tooltip } from "@mui/material";
import React from "react";
import "./snack-bar.css";
import CloseIcon from "@mui/icons-material/Close";

const SnackbarShared = (props: any) => {
  return (
    <>
      {props.severity ? (
        <Snackbar
          open={props.open}
          autoHideDuration={
            props.autoHideDuration ? props.autoHideDuration : 6000
          }
          onClose={props.handleClose}
          anchorOrigin={{ vertical: "top", horizontal: "center" }}
        >
          <Alert
            onClose={props.handleClose}
            severity={props.severity}
            sx={{ width: "200%" }}
            action={
              <IconButton
                aria-label="close"
                color="inherit"
                size="small"
                className="snackbar-cross"
                onClick={props.handleClose}
              >
                <Tooltip title="Close">
                  {/* <img className="close-icon-snackbar" src={CloseIcon}></img> */}
                  <CloseIcon />
                </Tooltip>
              </IconButton>
            }
          >
            {props.severity === "error" && (
              <div className="error-message-display-snackbar">
                {props.message ? (
                  <span>{props.message}</span>
                ) : (
                  <>
                    <span>Something went wrong!</span>
                    <span>
                      We could not process your request. Please try refreshing
                      the page & starting over.
                    </span>
                  </>
                )}
              </div>
            )}
            {props.severity === "warning" && <></>}
            {props.severity === "info" && <></>}
            {props.severity === "success" && (
              <>
                {props.message ? <span>{props.message}</span> : <span></span>}
              </>
            )}
          </Alert>
        </Snackbar>
      ) : (
        <></>
      )}
    </>
  );
};

export default SnackbarShared;
