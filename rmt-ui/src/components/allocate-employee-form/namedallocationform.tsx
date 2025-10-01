import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import { Grid, SxProps } from "@mui/material";
import { useState, useEffect } from "react";
import ShowSkill from "./showskills/showskills";
import Allocationdate from "./allocationdates/allocationdate";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormControl from "@mui/material/FormControl";
import * as constant from "./constant";
import { useForm } from "react-hook-form";
import { Controller } from "react-hook-form";
import ControllerNumberTextField from "../controllerInputs/controllerNumbeTextfield";
import AddCircleOutlinedIcon from "@mui/icons-material/AddCircleOutlined";
import * as GlobalConstant from "../../global/constant";
import ControllerTextField from "../controllerInputs/controllerTextField";
import { IAllocation } from "../update-allocation/entity/IAllocations";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 750,
  height: "auto",
  bgcolor: "background.paper",
  border: "1px solid #000",
  padding: "30px",
};
export const MenuIconSxProps: SxProps = {
  mr: 1,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};
export default function NamedAlloctionForm(props: any) {
  const {
    isModelOpen,
    closeModel,
    totalEfforts,
    allocationData,
    entries,
    description,
    isContinuousAllocation,
  } = props;

  const [employeeAllocation, setEmployeeAllocation] = useState([] as any);
  const [selectedDate, setSelectedDate] = useState(null);
  // const [isContinuousAllocation, setIsContinuousAllocation] = useState(
  //   props?.isContinuousAllocation
  // );

  const {
    control,
    formState: { errors },
    handleSubmit,
    watch,
    getValues,
    setValue,
  } = useForm();
  const handleDateChange = (date: any) => {
    setSelectedDate(date);
  };

  console.log("watch:", watch());

  const initialEntry = {
    startDate: null,
    endDate: null,
    totalWokingDays: null,
    effort: null,
  };

  useEffect(() => {
    // setIsContinuousAllocation(props.isContinuousAllocation);
    setValue("description", description);
  }, [allocationData, props.description]);

  useEffect(() => {
    setValue("totalEfforts", totalEfforts);
    setValue("entries", entries);
    setValue("isContinuousAllocation", isContinuousAllocation);
    if (entries.length > 0) {
      setEmployeeAllocation(entries.filter((a: IAllocation) => a.isactive));
    }
  }, [entries]);

  const onAllocationClick = (data: any) => {
    props.onAllocationClick(data);
  };

  const handleAddEntry = () => {
    props.handleAddEntry();
  };

  return (
    <div>
      <Modal
        open={isModelOpen}
        onClose={(event, reason) => {
          if (reason === "backdropClick") {
            return;
          }
          closeModel();
        }}
      >
        <Box sx={style}>
          <Typography
            sx={{ paddingBottom: "30px", fontSize: "20px", fontStyle: "bold" }}
          >
            Allocate Employee
          </Typography>
          <form onSubmit={handleSubmit(onAllocationClick)}>
            <Grid container>
              <Grid item xs={4} sx={{ paddingLeft: "23px" }}>
                <Typography>Continuous Allocation</Typography>
                <Controller
                  name="allocationType"
                  control={control}
                  defaultValue="Yes"
                  render={({ field: any }) => (
                    <FormControl>
                      <RadioGroup
                        row
                        aria-labelledby="demo-row-radio-buttons-group-label"
                        name="row-radio-buttons-group"
                        onChange={props.handleContinuousAllocationChange}
                        // defaultValue={allocationData.isContinuousAllocation}
                        value={isContinuousAllocation ? "Yes" : "No"}
                      >
                        <FormControlLabel
                          value="Yes"
                          control={<Radio />}
                          label="Yes"
                        />
                        <FormControlLabel
                          value="No"
                          control={<Radio />}
                          label="No"
                        />
                      </RadioGroup>
                    </FormControl>
                  )}
                />
              </Grid>
              <Grid item xs={4}>
                <ControllerTextField
                  name="description"
                  control={control}
                  defaultValue={""}
                  required={true}
                  label={"Description"}
                  error={errors.description}
                  value={description}
                  onChange={(e: any) => {}}
                />
              </Grid>
              <Grid item xs={3.5}>
                <ControllerNumberTextField
                  name="totalEfforts"
                  sx={constant.Textbox}
                  control={control}
                  defaultValue={""}
                  required={true}
                  label={"Total indicative effort hours"}
                  error={errors.totalEfforts}
                  min={1}
                  max={2}
                  onChange={(e: any) => {}}
                  value={totalEfforts}
                  validate={() => {
                    return getValues("totalEfforts") > 0 ? true : false;
                  }}
                />
              </Grid>
              {props?.isSkillShow && (
                <Grid
                  item
                  xs={9}
                  sm={9}
                  md={9}
                  lg={9}
                  xl={9}
                  sx={{ padding: "17px" }}
                >
                  <ShowSkill
                    control={control}
                    errors={errors}
                    skills={props.skills}
                  />
                </Grid>
              )}
            </Grid>
            <Grid>
              {employeeAllocation.map((entry: IAllocation) => (
                <Grid key={entry?.index}>
                  <Allocationdate
                    isContinuousAllocation={isContinuousAllocation}
                    index={entry?.index}
                    control={control}
                    entry={entry}
                    errors={errors}
                    entries={entries}
                    handleStartDateChange={(index: number, date: any) =>
                      props.handleStartDateChange(entry?.index, date)
                    }
                    handleEndDateChange={(index: number, date: any) =>
                      props.handleEndDateChange(index, date)
                    }
                    handleEffortChange={(index: any, value: any) =>
                      props.handleEffortChange(entry?.index, value)
                    }
                    handleDeleteEntry={(index: number) => {
                      props.handleDeleteEntry(entry?.index, entry);
                    }}
                    effortValidate={() => {
                      return getValues("effortsPerDay") > 0 &&
                        getValues("effortsPerDay") < 9
                        ? true
                        : false;
                    }}
                  />
                </Grid>
              ))}
            </Grid>
            <Grid>
              {isContinuousAllocation &&
                GlobalConstant.APP_CONFIG.add_more_limit >
                  entries.filter((a: IAllocation) => a.isactive).length && (
                  <Typography component="div">
                    <Button
                      variant="text"
                      className="btn"
                      onClick={handleAddEntry}
                    >
                      <AddCircleOutlinedIcon
                        fontSize="small"
                        sx={MenuIconSxProps}
                      />{" "}
                      Add more
                    </Button>
                  </Typography>
                )}
            </Grid>
            <Grid container spacing={0}>
              <Grid item xs={6}>
                <Button
                  variant="text"
                  onClick={() => {
                    closeModel();
                  }}
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
                  sx={constant.BtnAllocate}
                  type="submit"
                >
                  Allocate
                </Button>
              </Grid>
            </Grid>
          </form>
        </Box>
      </Modal>
    </div>
  );
}
