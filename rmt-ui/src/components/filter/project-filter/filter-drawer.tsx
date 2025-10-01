import {
  Button,
  Drawer,
  List,
  ListItem,
  ListItemText,
  Divider,
  Typography,
  IconButton,
  Checkbox,
  Tooltip,
  Card,
  CardContent,
  Grid,
} from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import CloseIcon from "@mui/icons-material/Close";
import * as constant from "./constant";
import "./styles.css";
import { useForm } from "react-hook-form";
import ControllerAutocompleteFilteredOptionsTextfield from "../../controllers/controller-autocomplete-filtered-options-textfield";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../contexts/loaderContext";

const FilterDrawerComp = (props: any) => {
  const { defaultValue, openDrawer, setOpenDrawer } = props;
  const [isSuspended, setIsSuspended] = useState<boolean>(false);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);

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
    if (defaultValue && Object.keys(defaultValue).length > 0) {
      Object.keys(defaultValue).map((item) => {
        //console.log("Item ", item);
        setValue(item, defaultValue[item]);
      });
    }
  }, []);
  const onSubmit = (data: any) => {
    loaderContext.open(true);
    props.selectedDataByFilter(data);
    setOpenDrawer(false);
  };
  // console.log(getValues())
  watch();
  // console.log("watch filter ", );
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
              <ListItemText className="filter-header" primary="Filter By" />
              <IconButton
                onClick={() => {
                  setOpenDrawer(false);
                }}
              >
                <Tooltip title="close">
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
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="bu"
                    control={control}
                    defaultValue={[]}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    options={props?.filterData?.distinctBUSet}
                    onChange={() => {
                      setValue("offering", []);
                      setValue("solution", []);
                      // setValue("revenueUnit", []);
                    }}
                    label={"Business Unit"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="offering"
                    disabled={getValues("bu")?.length > 0 ? false : true}
                    control={control}
                    defaultValue={[]}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    options={
                      getValues("bu") && getValues("bu").length > 0
                        ? props?.filterData?.distinctOfferings
                            ?.filter(
                              (a) =>
                                a.bu && a.name && getValues("bu").includes(a.bu)
                            )
                            ?.map((b) => b.name)
                        : []
                    }
                    onChange={() => {
                      setValue("solution", []);
                      // setValue("revenueUnit", []);
                    }}
                    label={"Offering"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="solution"
                    disabled={getValues("offering")?.length > 0 ? false : true}
                    control={control}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={
                      getValues("offering") && getValues("offering").length > 0
                        ? props?.filterData?.distinctSolutions
                            ?.filter(
                              (a) =>
                                a.offerings &&
                                a.name &&
                                getValues("offering").includes(a.offerings)
                            )
                            ?.map((b) => b.name)
                        : []
                    }
                    onChange={() => {}}
                    label={"Solution"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="clientName"
                    control={control}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={props.filterData?.distinctClientNames}
                    onChange={() => {}}
                    label={"Client Name"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="industry"
                    control={control}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={props.filterData?.distinctIndustry}
                    onChange={() => {
                      setValue("subIndustry", []);
                    }}
                    label={"Industry"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="subIndustry"
                    disabled={getValues("industry")?.length > 0 ? false : true}
                    control={control}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={
                      getValues("industry") && getValues("industry").length > 0
                        ? props?.filterData?.distinctSubIndustry
                            ?.filter(
                              (a) =>
                                getValues("industry").includes(a.industry) &&
                                a.name != null
                            )
                            ?.map((b) => b.name)
                        : []
                    }
                    onChange={() => {}}
                    label={"Sub-Industry"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="pipeline"
                    control={control}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={props.filterData?.distinctPipelines}
                    onChange={() => {}}
                    label={"Pipeline Code"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="job"
                    control={control}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={props.filterData?.distinctJobs}
                    onChange={() => {}}
                    label={"Job Code"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="jobName"
                    control={control}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={props.filterData?.distinctJobNames}
                    onChange={() => {}}
                    label={"Job Name"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="status"
                    control={control}
                    multiple={true}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={props.filterData?.status}
                    onChange={() => {}}
                    label={"Project Status"}
                  />
                </ListItem>
                <ListItem className={"filter-control-container"}>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="projectType"
                    control={control}
                    multiple={false}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={props.filterData?.projectType}
                    onChange={() => {}}
                    label={"Project Charge Type"}
                  />
                </ListItem>
                <ListItem>
                  <ControllerAutocompleteFilteredOptionsTextfield
                    name="marketPlaceType"
                    control={control}
                    multiple={false}
                    sx={constant.AutocompleteSxProps}
                    defaultValue={[]}
                    options={props.filterData?.marketPlaceType}
                    onChange={() => {}}
                    label={"Marketplace"}
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
                  let data = {
                    bu: [],
                    experties: [],
                    sme: [],
                    offering: [],
                    solution: [],
                    revenueUnit: [],
                    clientName: [],
                    pipeline: [],
                    job: [],

                    status: [],
                    marketPlaceType: undefined,
                    projectType: [],
                    industry: [],
                    subIndustry: [],
                  };
                  reset({
                    bu: [],
                    experties: [],
                    sme: [],
                    offering: [],
                    solution: [],
                    revenueUnit: [],
                    clientName: [],
                    pipeline: [],
                    job: [],
                    jobName: [],

                    status: [],
                    marketPlaceType: undefined,
                    projectType: [],
                    industry: [],
                    subIndustry: [],
                  });
                  Object.keys(getValues()).map((item) => {
                    setValue(item, []);
                  });
                  props.selectedDataByFilter(data);
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

export default FilterDrawerComp;
