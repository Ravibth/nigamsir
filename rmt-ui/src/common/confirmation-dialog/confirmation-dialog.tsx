import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import { Grid, IconButton, Tooltip } from "@mui/material";
import * as constant from "./constant";
import CloseIcon from "@mui/icons-material/Close";
import { HorizontalCenterAlignSxProps } from "../../components/common-allocation/user-info-timeline-group/style";
const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "50%",
  // width: 500,
  bgcolor: "background.paper",
  borderRadius: "15px",
  // border: "1px solid #000",
  // padding: "6px",
};
export default function ConfirmationDialog(props: any) {
  const { title, content, noBtnLabel, yesBtnLabel, open } = props;
  const handleClose = () => {
    props.onConfirmationPopClose();
  };

  return (
    <div>
      <Modal open={open} onClose={handleClose}>
        <Box sx={style}>
          <Grid container>
            <Grid
              item
              xs={12}
              sx={{ display: "flex", justifyContent: "flex-end" }}
            >
              <Tooltip title="Close">
                <IconButton onClick={handleClose}>
                  <CloseIcon />
                </IconButton>
              </Tooltip>
            </Grid>

            <Grid
              xs={12}
              item
              alignItems="center"
              justifyContent="center"
              sx={{ margin: "13x", ...HorizontalCenterAlignSxProps }}
            >
              <Typography sx={constant.modalTitleSxProps}>{title}</Typography>
            </Grid>
            <Grid
              item
              xs={12}
              sx={{ margin: "13px", ...HorizontalCenterAlignSxProps }}
            >
              <Typography sx={constant.modalContentSxProps}>
                {content}
              </Typography>
            </Grid>
            <Grid container spacing={0}>
              <Grid
                item
                xs={12}
                sx={{
                  display: "flex",
                  justifyContent: "center",
                  marginBottom: "20px",
                }}
              >
                <Button
                  variant="outlined"
                  className="btn"
                  sx={constant.BtnNo}
                  onClick={handleClose}
                >
                  {noBtnLabel}
                </Button>
                <Button
                  variant="outlined"
                  className="btn"
                  sx={constant.BtnYes}
                  onClick={() => props.handleYesClick()}
                >
                  {yesBtnLabel}
                </Button>
              </Grid>
              {/* <Grid
                item
                xs={1}
              ></Grid> */}
            </Grid>
          </Grid>
        </Box>
      </Modal>
    </div>
  );
}
