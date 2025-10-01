import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import {
  Autocomplete,
  Divider,
  Grid,
  IconButton,
  Paper,
  TextField,
} from "@mui/material";
import { useState } from "react";
import * as constant from "./constant";
import { useForm } from "react-hook-form";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import CloseIcon from "@mui/icons-material/Close";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 690,
  bgcolor: "background.paper",
  // border: "1px solid #000",
  padding: "30px",
};

export default function ConfirmationDialog(props: any) {
  const { title, content, noBtnLabel, yesBtnLabel } = props;
  const [open, setOpen] = React.useState(true);
  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    props.onConfirmationPopClose();
  };
  const [selectedDate, setSelectedDate] = useState(null);
  const {
    control,
    formState: { errors },
    handleSubmit,
    watch,
    getValues,
  } = useForm();
  const handleDateChange = (date: any) => {
    setSelectedDate(date);
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
              <IconButton onClick={handleClose}>
                {" "}
                <CloseIcon />
              </IconButton>
            </Grid>

            <form>
              <Grid
                xs={12}
                container
                alignItems="center"
                justifyContent="center"
                sx={{ margin: "20px" }}
              >
                <Typography sx={{ fontSize: "30px" }}>{title}</Typography>
                <InfoOutlinedIcon sx={{ color: "red", fontSize: "58px" }} />
              </Grid>
              <Grid xs={12} sx={{ margin: "60px" }}>
                <Typography sx={{ fontSize: "30px" }}>{content}</Typography>
              </Grid>
              <Grid container spacing={0} sx={{ margin: "50px" }}>
                <Grid item xs={6}>
                  <Button
                    variant="outlined"
                    className="btn"
                    sx={constant.BtnNo}
                    onClick={handleClose}
                  >
                    {noBtnLabel}
                  </Button>
                </Grid>
                <Grid item xs={6}>
                  <Button
                    variant="outlined"
                    className="btn"
                    sx={constant.BtnYes}
                    onClick={() => props.handleYesClick()}
                  >
                    {yesBtnLabel}
                  </Button>
                </Grid>
              </Grid>
            </form>
          </Grid>
        </Box>
      </Modal>
    </div>
  );
}
