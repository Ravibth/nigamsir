import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import { Grid, Paper, TextField } from "@mui/material";
import { useState } from "react";
import Allocationdate from "./allocationdates/allocationdate";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormControl from "@mui/material/FormControl";
import * as constant from "./constant";
import { useForm } from "react-hook-form";
import { Controller } from "react-hook-form";
import ControllerNumberTextField from "../controllerInputs/controllerNumbeTextfield";
import ControllerTextField from "../controllerInputs/controllerTextField";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 700,
  height: 350,
  bgcolor: "background.paper",
  border: "1px solid #000",
  padding: "30px",
};

export default function Updaterequisition(props: any) {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [selectedDate, setSelectedDate] = useState(null);
  const {
    control,
    formState: { errors },
    handleSubmit,
    watch,
    getValues,
  } = useForm();
  console.log(watch());
  const handleDateChange = (date: any) => {
    setSelectedDate(date);
  };

  return (
    <div>
      <Button onClick={handleOpen}>Open modal Updaterequisition</Button>
      <Modal open={open} onClose={handleClose}>
        <Box sx={style}>
          <Typography sx={constant.Title}>Allocate Employee</Typography>
          <form>
            <Grid container spacing={2} sx={{ padding: "20px" }}>
              <Grid item xs={4}>
                <Typography>Continuous Allocation</Typography>

                <Controller
                  name="allocationType"
                  control={control}
                  defaultValue="Yes" // Set default value as needed
                  render={({ field: any }) => (
                    <FormControl>
                      <RadioGroup
                        row
                        aria-labelledby="demo-row-radio-buttons-group-label"
                        name="row-radio-buttons-group"
                      >
                        <FormControlLabel
                          value="Yes"
                          control={<Radio />}
                          label="yes"
                        />
                        <FormControlLabel
                          value="no"
                          control={<Radio />}
                          label="no"
                        />
                      </RadioGroup>
                    </FormControl>
                  )}
                />
              </Grid>
              <Grid item xs={4}>
                <Paper>
                  {" "}
                  <ControllerTextField
                    name="description"
                    control={control}
                    defaultValue={""}
                    required={true}
                    label={"Description"}
                    error={errors.description}
                    onChange={(e: any) => {}}
                  />
                  {/* <TextField id="outlined-basic" variant="outlined" size="small" /> */}
                </Paper>
              </Grid>
              <Grid item xs={4}>
                <ControllerNumberTextField
                  name="noofhours"
                  sx={constant.Textbox}
                  control={control}
                  defaultValue={""}
                  required={true}
                  label={"No. of Hours"}
                  error={errors.effortsPerDay}
                  onChange={(e: any) => {}}
                  validate={() => {
                    return getValues("noofhours") > 0 &&
                      getValues("noofhours") < 9
                      ? true
                      : false;
                  }}
                />
              </Grid>
              <Grid>
                <Allocationdate control={control} errors={errors} />
              </Grid>
              <Grid container spacing={0}>
                <Grid item xs={6}>
                  <Button
                    variant="text"
                    onClick={handleClose}
                    className="btn"
                    sx={constant.Btncancel}
                  >
                    Cancel
                  </Button>
                </Grid>
                <Grid item xs={6}>
                  <Button
                    variant="text"
                    className="btn rmt-action-button"
                    sx={constant.Btn}
                  >
                    Save
                  </Button>
                </Grid>
              </Grid>
            </Grid>
          </form>
        </Box>
      </Modal>
    </div>
  );
}
