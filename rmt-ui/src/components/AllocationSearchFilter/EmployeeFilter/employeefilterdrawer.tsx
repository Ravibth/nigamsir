import {
  Button,
  Drawer,
  List,
  ListItem,
  ListItemText,
  Divider,
  Typography,
  IconButton,
  Grid,
  Paper,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import CloseIcon from "@mui/icons-material/Close";
import * as constant from "./constant";
//import "./styles.css";
import { useForm } from "react-hook-form";

import ControllerAutocompleteFilteredOptionsTextfield from "../../controllers/controller-autocomplete-filtered-options-textfield";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";
import { IFilterData } from "../IFilterData";
import ControllerCalendar from "../../controllerInputs/controlerCalendar";

let jobCodeValue: any;
let appliedFilterValue: any;
const EmployeefilterDrawer = (props: any) => {
  const { defaultValue, openDrawer, setOpenDrawer } = props;
  const [filterValue, setFilterValue] = useState<IFilterData>();
  const [locationOption, setLocationsOption] = useState<Array<string>>([]);
  const [expertiseOption, setExpertiseOption] = useState<Array<string>>([]);
  const [BusinessUnitOption, setBusinessUnitOption] = useState<Array<string>>(
    []
  );
  const [smegOption, setSmegOption] = useState<Array<string>>([]);
  const {
    register,
    handleSubmit,
    formState: { errors },
    control,
    reset,
    resetField,
    getValues,
    setValue,
    watch,
    trigger,
  } = useForm({
    mode: "onTouched",
  });

  useEffect(() => {
    const selectedLocation = getValues("locations");
    if (selectedLocation == undefined && props.filterData?.locations) {
      const filteredLocation = props.filterData?.locations?.filter(
        (x: any) => x
      );
      setLocationsOption(filteredLocation);
    }
    if (expertiseOption.length == 0 && props.filterData?.experties) {
      const filteredExpertise = props.filterData?.experties?.filter(
        (x: any) => x
      );
      setExpertiseOption(filteredExpertise);
    }
    if (BusinessUnitOption.length == 0 && props.filterData?.BusinessUnit) {
      const filteredBusinessUnit = props.filterData?.BusinessUnit?.filter(
        (x: any) => x
      );
      setBusinessUnitOption(filteredBusinessUnit);
    }
    if (smegOption.length == 0 && props.filterData?.smeg) {
      const filteredSmeg = props.filterData?.smeg?.filter((x: any) => x);
      setSmegOption(filteredSmeg);
    }
  }, [props.filterData]);

  useEffect(() => {
    if (appliedFilterValue) {
      Object.keys(appliedFilterValue).map((item) => {
        if (appliedFilterValue[item]) {
          setValue(item, appliedFilterValue[item]);
        }
      });
      if (appliedFilterValue?.startDate == undefined) {
        setValue("startDate", new Date(props?.defaultStartDate));
      }
      if (appliedFilterValue?.endDate == undefined) {
        setValue("endDate", new Date(props?.defaultEndDate));
      }
    } else {
      setValue("startDate", new Date(props?.defaultStartDate));
      setValue("endDate", new Date(props?.defaultEndDate));
    }
  }, []);

  const onSubmit = (data: any) => {
    setFilterValue(data);
    appliedFilterValue = data;
    jobCodeValue = getValues("job");
    props.selectedDataByFilter(data);
    setOpenDrawer(false);
  };

  const onReset = () => {
    const data = {
      // experties: [],
      //  sme: [],
      //   clientName: [],
      locations: [],
      job: [],
      //   elName: [],
      startDate: "",
      endDate: "",
    };
    appliedFilterValue = null;
    reset({
      //   experties: [],
      //    sme: [],
      //    clientName: [],
      locations: [],
      job: [],
      //    elName: [],
      startDate: "",
      endDate: "",
    });
    Object.keys(getValues()).map((item) => {
      setValue(item, []);
    });
    setValue("startDate", props?.defaultStartDate);
    setValue("endDate", props?.defaultEndDate);
    props.handleResetClick(data);
    setOpenDrawer(false);
  };

  const isFilterApplied = () => {
    if (
      props?.filterData?.job?.length ||
      props?.filterData?.locations?.length ||
      (props?.filterData?.startDate &&
        props?.filterData?.endDate &&
        props?.filterData?.startDate != "" &&
        props?.filterData?.endDate != "")
    ) {
      return true;
    }
    return false;
  };

  return (
    <React.Fragment>
      <Drawer
        open={openDrawer}
        onClose={() => setOpenDrawer(false)}
        sx={constant.DrawerSxProps}
      >
        <List>
          <form className="stacks" onSubmit={handleSubmit(onSubmit)}>
            <ListItem>
              <ListItemText primary="Filter By" />
              <IconButton
                onClick={() => {
                  setOpenDrawer(false);
                }}
              >
                {" "}
                <CloseIcon
                  sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColor }}
                />
              </IconButton>
            </ListItem>

            {/* <ListItem>
              <Typography sx={constant.TypographySxProps}>Project Name </Typography>
            </ListItem> */}
            <ListItem>
              <Typography
                sx={constant.TypographySxProps}
                style={{ fontWeight: 500 }}
              >
                {props?.projectName}{" "}
              </Typography>
            </ListItem>

            <Divider component="li" sx={constant.DividerSxProps} />

            <ListItem>
              <Typography sx={constant.TypographySxProps}>Job Code </Typography>
            </ListItem>
            <ListItem>
              <ControllerAutocompleteFilteredOptionsTextfield
                name="job"
                control={control}
                sx={constant.AutocompleteSxProps}
                //   defaultValue={[]}
                options={props.filterData?.job ? props.filterData?.job : []}
                onChange={(e: any) => {
                  //   jobCodeValue = e[0] ? e[0] : ""
                  // setIsReset(false);
                }}
                textfieldVariant="standard"
                value={jobCodeValue}
              />
            </ListItem>

            <Divider component="li" sx={constant.DividerSxProps} />
            {/* <ListItem>
              <Typography sx={constant.TypographySxProps}>
                Client Name
              </Typography>
            </ListItem>
            <ListItem>
              <ControllerAutocompleteFilteredOptionsTextfield
                name="clientName"
                control={control}
                sx={constant.AutocompleteSxProps}
                defaultValue={[]}
                options={
                  props.filterData?.clientName
                    ? props.filterData?.clientName
                    : []
                }
                onChange={() => {
                  // setIsReset(false);
                }}
                textfieldVariant="standard"
              />
            </ListItem> */}
            <ListItem>
              <Typography sx={constant.TypographySxProps}>
                Business Unit
              </Typography>
            </ListItem>
            <ListItem>
              <ControllerAutocompleteFilteredOptionsTextfield
                name="BusinessUnit"
                control={control}
                defaultValue={[]}
                sx={constant.AutocompleteSxProps}
                options={BusinessUnitOption}
                onChange={() => {
                  // setIsReset(false);
                }}
                textfieldVariant="standard"
              />
            </ListItem>
            <Divider sx={constant.DividerSxProps} />
            <ListItem>
              <Typography sx={constant.TypographySxProps}>Expertise</Typography>
            </ListItem>
            <ListItem>
              <ControllerAutocompleteFilteredOptionsTextfield
                name="experties"
                control={control}
                defaultValue={[]}
                sx={constant.AutocompleteSxProps}
                options={expertiseOption}
                onChange={() => {
                  // setIsReset(false);
                }}
                textfieldVariant="standard"
              />
            </ListItem>
            <Divider sx={constant.DividerSxProps} />
            <ListItem>
              <Typography sx={constant.TypographySxProps}>SMEG</Typography>
            </ListItem>
            <ListItem>
              <ControllerAutocompleteFilteredOptionsTextfield
                name="smeg"
                control={control}
                defaultValue={[]}
                sx={constant.AutocompleteSxProps}
                options={smegOption}
                onChange={() => {
                  // setIsReset(false);
                }}
                textfieldVariant="standard"
              />
            </ListItem>
            <Divider sx={constant.DividerSxProps} />

            <ListItem>
              <Typography sx={constant.TypographySxProps}>Period</Typography>
            </ListItem>
            <ListItem sx={constant.AutocompleteSxProps}>
              <Grid item xs={6.5}>
                <Paper>
                  {" "}
                  <ControllerCalendar
                    name={"startDate"}
                    control={control}
                    label={"From"}
                    required={false}
                    defaultValue={new Date(props?.defaultStartDate)}
                    onChange={(date: any) => {
                      setValue("startDate", new Date(date));
                      props.handleStartDateChange(date);
                    }}
                    //defaultValue={entry?.confirmedAllocationStartDate}
                    // maxDate={entry?.confirmedAllocationEndDate}
                  />
                </Paper>
              </Grid>
              <Grid item xs={6.5}>
                <Paper>
                  <ControllerCalendar
                    name={"endDate"}
                    defaultValue={new Date(props?.defaultEndDate)}
                    control={control}
                    label={"To"}
                    //    error={errors.endDate}
                    onChange={(date: any) => {
                      setValue("endDate", new Date(date));
                      props.handleEndDateChange(date);
                    }}
                    //defaultValue={entry?.confirmedAllocationStartDate}
                    // maxDate={entry?.confirmedAllocationEndDate}
                  />
                </Paper>
              </Grid>
            </ListItem>
            <Divider sx={constant.DividerSxProps} />
            {/* <ListItem>
              <Typography sx={constant.TypographySxProps}>SME</Typography>
            </ListItem>
            <ListItem>
              <ControllerAutocompleteFilteredOptionsTextfield
                name="sme"
                control={control}
                sx={constant.AutocompleteSxProps}
                defaultValue={[]}
                options={props.filterData?.sme ? props.filterData?.sme : []}
                onChange={() => {
                  // setIsReset(false);
                }}
                textfieldVariant="standard"
              />
            </ListItem> */}

            <ListItem>
              <Typography sx={constant.TypographySxProps}>Location </Typography>
            </ListItem>
            <ListItem>
              <ControllerAutocompleteFilteredOptionsTextfield
                name="locations"
                control={control}
                sx={constant.AutocompleteSxProps}
                defaultValue={[]}
                options={locationOption}
                onChange={() => {
                  // setIsReset(false);
                }}
                textfieldVariant="standard"
              />
            </ListItem>

            <Divider component="li" sx={constant.DividerSxProps} />

            {/* <ListItem>
              <Typography sx={constant.TypographySxProps}>EL Name </Typography>
            </ListItem>
            <ListItem>
              <ControllerAutocompleteFilteredOptionsTextfield
                name="elName"
                control={control}
                sx={constant.AutocompleteSxProps}
                defaultValue={[]}
                options={
                  props.filterData?.elName ? props.filterData?.elName : []
                }
                onChange={() => {
                  // setIsReset(false);
                }}
                textfieldVariant="standard"
              />
            </ListItem>
            <Divider component="li" sx={constant.DividerSxProps} /> */}

            <Button
              sx={constant.CloseButtonSxProps}
              variant="outlined"
              type="button"
              onClick={() => {
                onReset();
                // onSubmit(data);
                //  props.selectedDataByFilter(data);
              }}
            >
              Reset
            </Button>
            <Button
              variant="contained"
              type="submit"
              className="rmt-action-button"
              // onClick={(e: any) => {
              //   setOpenDrawer(false);
              // }}
              sx={constant.ApplyFilterButtonSxProps}
            >
              Apply
            </Button>
          </form>
        </List>
      </Drawer>
    </React.Fragment>
  );
};

export default EmployeefilterDrawer;
