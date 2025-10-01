import React, { useEffect, useState } from "react";
import {
  Button,
  Drawer,
  List,
  Stack,
  ListItem,
  ListItemText,
  Divider,
  Typography,
  IconButton,
  Grid,
  Paper,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import * as constant from "./constant";
import ControllerCalendar from "../../../controllerInputs/controlerCalendar";
import { useForm } from "react-hook-form";
import ControllerAutocompleteFilteredOptionsTextfield from "../../../controllers/controller-autocomplete-filtered-options-textfield";
import { IRequisitionFilterData } from "../../IRequisitionFilterData";

const RequisitionfilterDrawer = (props: any) => {
  const { defaultValue, openDrawer, setOpenDrawer, designationList } = props;
  const [filterValue, setFilterValue] = useState<IRequisitionFilterData>();
  const {
    handleSubmit,
    formState: { errors },
    control,
    reset,
    getValues,
    setValue,
    watch,
  } = useForm({
    mode: "onTouched",
  });

  useEffect(() => {
    if (isFilterApplied()) {
      setValue("designation", filterValue?.designation);
      setValue("experties", filterValue?.experties);
      setValue("sme", filterValue?.sme);
      setValue("revenueUnit", filterValue?.revenueUnit);
      setValue("revenueUnit", filterValue?.revenueUnit);
      setValue("startDate", "");
      setValue("endDate", "");
    } else if (defaultValue && Object.keys(defaultValue).length > 0) {
      Object.keys(defaultValue).map((item) => {
        setValue(item, defaultValue[item]);
      });
    }
  }, []);

  const onSubmit = (data: any) => {
    setFilterValue(data);
    props.selectedDataByFilter(data);
    setOpenDrawer(false);
  };

  const onReset = () => {
    const data = {
      designation: [],
      experties: [],
      sme: [],
      startDate: "",
      endDate: "",
      businessUnit: [],
      revenueUnit: [],
    };
    reset({
      designation: [],
      experties: [],
      sme: [],
      startDate: "",
      endDate: "",
      businessUnit: [],
      revenueUnit: [],
    });
    Object.keys(getValues()).map((item) => {
      setValue(item, []);
    });
    console.log(watch());
    props.handleResetClick(data);
  };

  const isFilterApplied = () => {
    if (
      filterValue?.designation?.length ||
      filterValue?.experties?.length ||
      filterValue?.businessUnit?.length ||
      filterValue?.revenueUnit?.length ||
      filterValue?.sme?.length
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
        <div className="stacks">
          <Stack
            direction="row"
            justifyContent="center"
            alignItems="center"
            spacing={2}
          >
            <List>
              <div>
                <form onSubmit={handleSubmit(onSubmit)}>
                  <ListItem>
                    <ListItemText primary="Filter By" />
                    <IconButton
                      onClick={() => {
                        setOpenDrawer(false);
                      }}
                    >
                      {" "}
                      <CloseIcon />
                    </IconButton>
                  </ListItem>
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      Designation
                    </Typography>
                  </ListItem>
                  <ListItem>
                    <ControllerAutocompleteFilteredOptionsTextfield
                      name="designation"
                      multiple={true}
                      control={control}
                      sx={constant.AutocompleteSxProps}
                      defaultValue={[]}
                      options={designationList ? designationList : []}
                      onChange={() => {
                        // setIsReset(false);
                      }}
                      textfieldVariant="standard"
                    />
                  </ListItem>
                  <Divider component="li" sx={constant.DividerSxProps} />
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      Period
                    </Typography>
                  </ListItem>
                  <ListItem
                    sx={{ ...constant.AutocompleteSxProps, ml: "15px" }}
                  >
                    <Grid xs={6.5}>
                      <Paper>
                        <ControllerCalendar
                          name={"startDate"}
                          control={control}
                          label={"From"}
                          defaultValue={""}
                          onChange={(date: any) => {
                            setValue("startDate", new Date(date));
                            props.handleStartDateChange(date);
                          }}
                          required={false}
                        />
                      </Paper>
                    </Grid>
                    <Grid xs={6.5}>
                      <Paper>
                        <ControllerCalendar
                          name={"endDate"}
                          defaultValue={""}
                          control={control}
                          label={"To"}
                          onChange={(date: any) => {
                            setValue("endDate", new Date(date));
                            props.handleEndDateChange(date);
                          }}
                          required={false}
                        />
                      </Paper>
                    </Grid>
                  </ListItem>
                  <Divider component="li" sx={constant.DividerSxProps} />
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      Business Unit
                    </Typography>
                  </ListItem>
                  <ListItem>
                    <ControllerAutocompleteFilteredOptionsTextfield
                      name="businessUnit"
                      multiple={true}
                      control={control}
                      sx={constant.AutocompleteSxProps}
                      defaultValue={[]}
                      options={constant.experties ? constant.experties : []}
                      onChange={() => {
                        // setIsReset(false);
                      }}
                      textfieldVariant="standard"
                    />
                  </ListItem>
                  <Divider component="li" sx={constant.DividerSxProps} />
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      Expertise
                    </Typography>
                  </ListItem>
                  <ListItem>
                    <ControllerAutocompleteFilteredOptionsTextfield
                      name="experties"
                      multiple={true}
                      control={control}
                      sx={constant.AutocompleteSxProps}
                      defaultValue={[]}
                      options={constant.experties ? constant.experties : []}
                      onChange={() => {
                        // setIsReset(false);
                      }}
                      textfieldVariant="standard"
                    />
                  </ListItem>
                  <Divider component="li" sx={constant.DividerSxProps} />
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      SMEG
                    </Typography>
                  </ListItem>
                  <ListItem>
                    <ControllerAutocompleteFilteredOptionsTextfield
                      name="sme"
                      multiple={true}
                      control={control}
                      sx={constant.AutocompleteSxProps}
                      defaultValue={[]}
                      options={constant.sme ? constant.sme : []}
                      onChange={() => {
                        // setIsReset(false);
                      }}
                      textfieldVariant="standard"
                    />
                  </ListItem>

                  <Divider component="li" sx={constant.DividerSxProps} />
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      Revenue Unit
                    </Typography>
                  </ListItem>
                  <ListItem>
                    <ControllerAutocompleteFilteredOptionsTextfield
                      name="revenueUnit"
                      multiple={true}
                      control={control}
                      sx={constant.AutocompleteSxProps}
                      defaultValue={[]}
                      options={constant.clientName ? constant.clientName : []}
                      onChange={() => {
                        // setIsReset(false);
                      }}
                      textfieldVariant="standard"
                    />
                  </ListItem>

                  <ListItem>
                    <Button
                      sx={constant.CloseButtonSxProps}
                      variant="outlined"
                      onClick={() => {
                        onReset();
                      }}
                    >
                      Reset
                    </Button>
                    <Button
                      type="submit"
                      variant="contained"
                      className="rmt-action-button"
                      sx={constant.ApplyFilterButtonSxProps}
                    >
                      Apply
                    </Button>
                  </ListItem>
                </form>
              </div>
            </List>
          </Stack>
        </div>
      </Drawer>
    </React.Fragment>
  );
};

export default RequisitionfilterDrawer;
