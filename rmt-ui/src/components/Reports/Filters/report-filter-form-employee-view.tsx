import {
  Box,
  Divider,
  Drawer,
  Grid,
  IconButton,
  ListItem,
  ListItemText,
} from "@mui/material";
import { useForm } from "react-hook-form";
import uniq from "lodash/uniq";
import CloseIcon from "@mui/icons-material/Close";
import React, { useEffect, useState, useContext } from "react";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";

import {
  DividerSxProps,
  EReportDashboardFilterControl,
  IReportDashboardFilterControl,
  IReportDashboardFilterOptions,
} from "./uitls";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";
import ControllerAutoCompleteChipsSimple from "../../controllerInputs/controllerAutoCompleteChipsSimple";
import BackActionButton from "../../actionButton/backactionButton";
import ActionButton from "../../actionButton/actionButton";
import { IBUTreeMapping } from "../../../common/interfaces/IBuTreeMapping";
import { IWCGTLocationMaster } from "../../../common/interfaces/IWCGTLocationMaster";
import ControllerCalendar from "../../controllerInputs/controlerCalendar";
import moment from "moment";

export interface IReportDashboardFilterForm {
  setOpenFilter: React.Dispatch<React.SetStateAction<boolean>>;
  isFilterOpen: boolean;
  buTreeMappingMaster: IBUTreeMapping[];
  locationData: IWCGTLocationMaster[];
  filterParameters: IReportDashboardFilterControl;
  setFilterParameters: React.Dispatch<
    React.SetStateAction<IReportDashboardFilterControl>
  >;
  GetFilterDefaultValueOnTheBasisOfRole: (
    userRole?: string
  ) => IReportDashboardFilterControl;
}

const ReportDashboardFilterFormEmployeeView = (props) => {
  const userDetailsContext = useContext(UserDetailsContext);
  console.log("userDetailsContext", userDetailsContext);
  const {
    handleSubmit,
    control,
    setValue,
    reset,
    getValues,
    formState: { errors },
    trigger,
  } = useForm({
    mode: "onTouched",
    defaultValues: props.GetFilterDefaultValueOnTheBasisOfRole(),
    //controlType Create and provide
  });
  const [options, setOptions] = useState<IReportDashboardFilterOptions>({
    businessUnit: [],
    expertise: [],
    smeg: [],
    location: [],
  });
  const getOptionsFromUserInfos = () => {
    const tempBusinessUnit = props.buTreeMappingMaster.map((item) => item.bu);
    const tempExpertise = props.buTreeMappingMaster.map(
      (item) => item.expertise
    );
    const tempSmeg = props.buTreeMappingMaster.map((item) => item.sme_group);
    const tempLocation = props.locationData.map((item) => item.location_name);

    // let tempDesignation = props.designation
    //     .filter((item) => item.designation_name == userDetailsContext.designation)
    //     .map((item) => item.designation_name);
    setOptions({
      businessUnit: uniq(tempBusinessUnit),
      expertise: uniq(tempExpertise),
      smeg: uniq(tempSmeg),
      location: uniq(tempLocation),
    });
  };
  const autoPopulateFilterData = () => {
    // console.log("filter Params Populated ", props.filterParameters);
    Object.keys(props.filterParameters).forEach((key: any) => {
      setValue(key, props.filterParameters[key]);
    });
  };
  useEffect(() => {
    if (
      props.isFilterOpen &&
      props.buTreeMappingMaster &&
      props.locationData &&
      props.buTreeMappingMaster.length > 0 &&
      props.locationData.length > 0
    ) {
      getOptionsFromUserInfos();
      autoPopulateFilterData();
    }
  }, [props.buTreeMappingMaster, props.locationData, props.isFilterOpen]);

  const onSubmit = (data: any) => {
    //console.log(data);
    props.setFilterParameters(data);
    props.setOpenFilter(false);
    props.setisFilterApplied(true);
  };
  const onBuChange = (selectedBU: string[]) => {
    //console.log("Business_Unit_Selected , ", selectedBU);
    let selectedBuTreeMapping = [];
    let expertiesOpions = [];
    let smegOptions = [];
    if (selectedBU.length) {
      selectedBuTreeMapping = props.buTreeMappingMaster.filter((data) =>
        selectedBU.includes(data.bu)
      );
      // console.log(
      //   "Filtered Values after select mapping , ",
      //   selectedBuTreeMapping
      // );
      expertiesOpions = uniq(
        selectedBuTreeMapping.map((data) => data.expertise)
      );
      smegOptions = uniq(selectedBuTreeMapping.map((data) => data.sme_group));
    }

    setOptions((prevOptions) => {
      return { ...prevOptions, expertise: expertiesOpions, smeg: smegOptions };
    });
  };

  const resetButton = () => {
    props.setisFilterApplied(false);
    reset();
  };
  return (
    <Box mt={2} ml={2} mr={2} mb={2}>
      <Drawer
        open={props.isFilterOpen}
        onClose={() => props.setOpenFilter(false)}
        sx={{ zIndex: 1300 }}
      >
        <form className="stacks" onSubmit={handleSubmit(onSubmit)}>
          <Grid item xs={12}>
            <ListItem>
              <ListItemText primary="Filter By" />
              <IconButton
                onClick={() => {
                  props.setOpenFilter(false);
                }}
              >
                <CloseIcon
                  sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColor }}
                />
              </IconButton>
            </ListItem>
          </Grid>

          <Grid container spacing={2} sx={{ p: 2, width: "400px" }}>
            <Grid item xs={12}>
              <ControllerCalendar
                name={EReportDashboardFilterControl.start_date}
                control={control}
                defaultValue={null}
                label={"Start Date"}
                maxDate={moment(
                  getValues(EReportDashboardFilterControl.end_date)
                ).toDate()}
                error={errors?.start_date ? true : false}
                minDate={moment(
                  getValues(EReportDashboardFilterControl.end_date)
                )
                  .add(-1, "year")
                  .toDate()}
                onChange={(date: any) => {
                  // console.log("start_date ", date);
                  // console.log(
                  //   new Date(
                  //     getValues(
                  //       EReportDashboardFilterControl.start_date
                  //     )?.setFullYear(
                  //       getValues(
                  //         EReportDashboardFilterControl.start_date
                  //       )?.getFullYear() + 1
                  //     )
                  //   )
                  // );
                  // updateAllUserAllocationsAccordingToBaseInfos();
                  trigger(EReportDashboardFilterControl.end_date);
                }}
              />
            </Grid>
            <Grid item xs={12}>
              <Divider sx={DividerSxProps} />
            </Grid>
          </Grid>
          <Grid container spacing={2} sx={{ p: 2, width: "400px" }}>
            <Grid item xs={12}>
              <ControllerCalendar
                name={EReportDashboardFilterControl.end_date}
                control={control}
                defaultValue={null}
                label={"End Date"}
                maxDate={moment(
                  getValues(EReportDashboardFilterControl.start_date)
                )
                  .add(1, "year")
                  .toDate()}
                minDate={moment(
                  getValues(EReportDashboardFilterControl.start_date)
                ).toDate()}
                error={errors?.end_date ? true : false}
                // validate={(e) => {
                //   return (
                //     new Date(e) >= new Date(props.projectInfo?.startDate) &&
                //     new Date(e) <= new Date(props.projectInfo?.endDate)
                //   );
                // }}
                onChange={(date: any) => {
                  // updateAllUserAllocationsAccordingToBaseInfos();
                  trigger(EReportDashboardFilterControl.start_date);
                }}
              />
            </Grid>
            <Grid item xs={12}>
              <Divider sx={DividerSxProps} />
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid container spacing={2}>
              <Grid item xs={3} />
              <Grid item xs={3}>
                <BackActionButton
                  label={"Reset"}
                  textTransform="none"
                  onClick={function (e: any): void {
                    resetButton();
                    onSubmit(getValues());
                  }}
                />
              </Grid>
              <Grid item xs={3}>
                <ActionButton
                  label={"Apply"}
                  onClick={function (e: any): void {}}
                  disabled={false}
                  type={"submit"}
                  textTransform="none"
                />
              </Grid>
              <Grid item xs={3} />
            </Grid>
          </Grid>
        </form>
      </Drawer>
    </Box>
  );
};

export default ReportDashboardFilterFormEmployeeView;
