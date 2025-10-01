import {
  Button,
  Drawer,
  List,
  ListItem,
  ListItemText,
  Typography,
  IconButton,
  CardContent,
  Card,
  Tooltip,
} from "@mui/material";
import React, { useState } from "react";
import CloseIcon from "@mui/icons-material/Close";
import * as constant from "../../AllocationSearchFilter/EmployeeFilter/constant";
import { useForm } from "react-hook-form";
import ControllerAutocompleteFilteredOptionsTextfield from "../../controllers/controller-autocomplete-filtered-options-textfield";
import _ from "lodash";
import { IBudgetFilterData, EBudgetFilterData } from "./budgetFilter";

const BudgetFilterDrawer = (props: any) => {
  const { openDrawer, setOpenDrawer } = props;
  const {
    handleSubmit,
    formState: {},
    control,
    reset,
  } = useForm<IBudgetFilterData>({
    mode: "onTouched",
  });

  const resetData: IBudgetFilterData = {
    location: [],
    competency: [],
    designation: [],
    grade: [],
    businessUnit: [],
  };

  const [key, setKey] = useState(0);

  const onSubmit = (data: any) => {
    let currentFilter = props.filterDefaultValue;
    props.selectedDataByFilter(currentFilter);
    setOpenDrawer(false);
  };

  const handleRefresh = () => {
    setKey(key + 1); // Changing the key to force a re-render
  };

  const onReset = () => {
    props.setFilterDefaultValue(resetData);
    reset(resetData);
    handleRefresh();
    props.selectedDataByFilter(resetData);
  };

  const ddlOnChange = (e, name) => {
    let temp = props.filterDefaultValue;
    temp[name] = e ? e : [];
    props.setFilterDefaultValue(temp);
  };

  return (
    <React.Fragment>
      <Drawer
        open={openDrawer}
        onClose={() => setOpenDrawer(false)}
        sx={constant.DrawerSxProps}
        key={key}
      >
        <List>
          <form className="stacks" onSubmit={handleSubmit(onSubmit)}>
            <ListItem>
              <ListItemText className="filter-header" primary="Filter By" />
              <IconButton
                onClick={() => {
                  setOpenDrawer(false);
                }}
              >
                <Tooltip title={"close"}>
                  <CloseIcon />
                </Tooltip>
              </IconButton>
            </ListItem>
            <Card
              style={{
                margin: "10px",
                border: "1px solid lightgray",
                borderRadius: "10px",
              }}
            >
              <CardContent>
                <ListItem>
                  <Typography
                    sx={constant.TypographySxProps}
                    style={{ fontWeight: 500 }}
                  >
                    {props?.projectName}{" "}
                  </Typography>
                </ListItem>

                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name={EBudgetFilterData.location}
                    control={control}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={props.filterDefaultValue?.location}
                    options={
                      props.filterData?.location
                        ? _.uniq(props.filterData?.location)
                        : []
                    }
                    onChange={(e) => {
                      ddlOnChange(e, EBudgetFilterData.location);
                    }}
                    label={"Location"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name={EBudgetFilterData.grade}
                    control={control}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={props.filterDefaultValue?.grade}
                    options={
                      props.filterData?.grade
                        ? _.uniq(props.filterData?.grade)
                        : []
                    }
                    onChange={(e) => {
                      ddlOnChange(e, EBudgetFilterData.grade);
                    }}
                    label={"Grade"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name={EBudgetFilterData.designation}
                    control={control}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={props.filterDefaultValue?.designation}
                    options={
                      props.filterData?.designation
                        ? _.uniq(props.filterData?.designation)
                        : []
                    }
                    onChange={(e) => {
                      ddlOnChange(e, EBudgetFilterData.designation);
                    }}
                    label={"Designation"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name={EBudgetFilterData.businessUnit}
                    control={control}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={props.filterDefaultValue?.businessUnit}
                    options={
                      props.filterData?.businessUnit
                        ? _.uniq(props.filterData?.businessUnit)
                        : []
                    }
                    onChange={(e) => {
                      ddlOnChange(e, EBudgetFilterData.businessUnit);
                    }}
                    label={"Business Unit"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name={EBudgetFilterData.competency}
                    control={control}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={props.filterDefaultValue?.competency}
                    options={
                      props.filterData?.competency
                        ? _.uniq(props.filterData?.competency)
                        : []
                    }
                    onChange={(e) => {
                      ddlOnChange(e, EBudgetFilterData.competency);
                    }}
                    label={"Competency"}
                  />
                </ListItem>
              </CardContent>
            </Card>
            <div className="filter-btn-container">
              <Button
                variant="contained"
                type="submit"
                className="rmt-action-button"
                sx={constant.ApplyFilterButtonSxProps}
              >
                Apply Filters
              </Button>
              <Button
                sx={constant.CloseButtonSxProps}
                variant="outlined"
                type="button"
                onClick={() => {
                  onReset();
                }}
              >
                Reset
              </Button>
            </div>
          </form>
        </List>
      </Drawer>
    </React.Fragment>
  );
};

export default BudgetFilterDrawer;
