import { Box } from "@mui/material";
import React from "react";
import CloseIcon from "@mui/icons-material/Close";
import "./unauthorized-user-prompt-modal.css";
import { useNavigate } from "react-router-dom";
import ActionButton from "../actionButton/actionButton";

const UnauthorizedUserPromptModal = (props: any) => {
  const navigate = useNavigate();
  const { handleClose } = props;

  const closeHandle = () => {
    handleClose();
    navigate("/");
  };
  return (
    <Box className={"box-style-unauthorized-user-prompt-modal"}>
      <div>
        <div className="row">
          <div
            className="col-2 close-icon-unauthorized-user-prompt-modal"
            title="Close"
          >
            <CloseIcon
              onClick={() => {
                closeHandle();
              }}
            />
          </div>
          <div className="col-2"></div>
          <div className={"col-8 heading-unauthorized-user-prompt-modal"}>
            <span>We are sorry!</span>
          </div>
        </div>
        <div className={"modal-description-unauthorized-user-prompt-modal"}>
          <div>
            <span>
              <span>
                <span className="display-inner-description-user-prompt-modal">
                  <span>
                    Either you do not have permission to access the requested
                    page or the requested page does not exist.
                  </span>
                  <br />
                  <span>Please contact Admin</span>
                </span>
              </span>
              {/* <span>
                <span>
                  You do not have permission to access requested page. Contact
                  Admin for further assistance
                </span>
              </span> */}
            </span>
          </div>
        </div>
        <div className="go-todashboard-unauthorized-user-prompt-modal">
          <ActionButton
            label={"Ok"}
            onClick={closeHandle}
            type={"button"}
            disabled={false}
          />
        </div>
      </div>
    </Box>
  );
};

export default UnauthorizedUserPromptModal;
