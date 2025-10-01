import * as React from "react";
import Box from "@mui/material/Box";
import Modal from "@mui/material/Modal";
import ControllerTextField from "../controllerInputs/controllerTextField";
import { Grid, Tooltip } from "@mui/material";
import { useForm } from "react-hook-form";
import CloseIcon from "@mui/icons-material/Close";

import { useState } from "react";
import ActionButton from "../actionButton/actionButton";
import { AddJustificationText } from "../../services/project-list-services/project-list-services";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "50%",
  height: "auto",
  bgcolor: "background.paper",
  border: "1px solid #000",
  padding: "30px",
  borderRadius: "15px",
};

const AllocationJustificationModal = (props: any) => {
  const [justification] = useState<string>();
  const {
    control,
    formState: { errors },
    handleSubmit,
    getValues,
    setValue,
  } = useForm();

  const AddJustification = async (
    pipelineCode: string,
    jobCode: string,
    justificationText: string
  ) => {
    const response = await AddJustificationText(
      justificationText,
      pipelineCode,
      jobCode
    );
    if (response.status === 200) {
      setValue("justification", "");
      // props.closeJustificationModal();
      props.justificationSuccess();
      return true;
    } else {
      return false;
    }
  };

  const submitClick = () => {
    const pipelineCode = props?.projectDetails?.pipelineCode;
    const jobCode = props?.projectDetails?.jobCode;
    const text = getValues("justification");
    AddJustification(pipelineCode, jobCode, text);
  };

  return (
    <>
      <Modal
        open={props?.isModalOpen}
        onClose={(event, reason) => {
          if (reason === "backdropClick") {
            return;
          }
          props?.closeJustificationModal();
        }}
      >
        <form onSubmit={handleSubmit(submitClick)}>
          <Box sx={style}>
            <Grid>
              {" "}
              <div className="justification-text">
                <span>
                  Project is not in Marketplace. Please give justification for
                  allocation.{" "}
                </span>
                <span style={{ float: "right" }}>
                  <Tooltip title={"Close"}>
                    <CloseIcon
                      onClick={() => {
                        setValue("justification", "");
                        props.closeJustificationModal();
                      }}
                    />
                  </Tooltip>
                </span>
              </div>
            </Grid>
            <Grid
              item
              xs={12}
              // sx={{ paddingLeft: "23px" }}
            >
              <ControllerTextField
                name="justification"
                control={control}
                defaultValue={""}
                required={true}
                label={"Justification \u00A0"}
                error={errors.justification}
                value={justification}
                multiline={"multiline"}
                fullWidth={"fullWidth"}
                onChange={(e: any) => {}}
              />
            </Grid>
            <div className="justification-submit">
              {" "}
              <ActionButton
                label={"Submit"}
                type="submit"
                onClick={(e: any) => {
                  // submitClick();
                }}
                disabled={false}
              />
            </div>
          </Box>
        </form>
      </Modal>
    </>
  );
};
export default AllocationJustificationModal;
