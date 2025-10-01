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
import React, { useEffect, useState } from "react";
import {
  DividerSxProps,
  EReportDashboardFilterControl,
  IReportDashboardFilterControl,
  IReportDashboardFilterOptions,
} from "./uitls";
import moment from "moment";
import { GT_DESIGN_PARAMETERS } from "../../../../../../global/constant";
import { IBUTreeMapping } from "../../../../../../common/interfaces/IBuTreeMapping";
import { IWCGTLocationMaster } from "../../../../../../common/interfaces/IWCGTLocationMaster";
import BackActionButton from "../../../../../actionButton/backactionButton";
import ActionButton from "../../../../../actionButton/actionButton";
import ControllerCalendar from "../../../../../controllerInputs/controlerCalendar";

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

  applyFilter: (data) => {};
}

const ReportDashboardFilterForm = (props) => {
  const {
    handleSubmit,
    control,
    setValue,
    reset,
    getValues,
    formState: { errors },
  } = useForm<IReportDashboardFilterControl>({
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

  useEffect(() => {
    if (
      props.isFilterOpen //&&
      // props.buTreeMappingMaster &&
      // props.locationData &&
      // props.buTreeMappingMaster.length > 0 &&
      // props.locationData.length > 0
    ) {
      //getOptionsFromUserInfos();
      autoPopulateFilterData();
    }
  }, [props.buTreeMappingMaster, props.locationData, props.isFilterOpen]);
  const getOptionsFromUserInfos = () => {
    //console.log(" props.buTreeMappingMaster===>", props.buTreeMappingMaster);
    const tempBusinessUnit = props.buTreeMappingMaster.map((item) => item.bu);
    const tempExpertise = props.buTreeMappingMaster.map(
      (item) => item.expertise
    );
    const tempSmeg = props.buTreeMappingMaster.map((item) => item.sme_group);
    const tempLocation = props.locationData.map((item) => item.location_name);
    setOptions({
      businessUnit: uniq(tempBusinessUnit),
      expertise: uniq(tempExpertise),
      smeg: uniq(tempSmeg),
      location: uniq(tempLocation),
    });
  };
  const autoPopulateFilterData = () => {
    Object.keys(props.filterParameters).forEach((key: any) => {
      setValue(key, props.filterParameters[key]);
    });
  };

  const onSubmit = (data: IReportDashboardFilterControl) => {
    props.applyFilter(data);
    console.log(data);
    //props.setFilterParameters(data);
    props.setOpenFilter(false);
  };
  // const onBuChange = (selectedBU: string[]) => {
  //     console.log('Business_Unit_Selected , ', selectedBU);
  //     const selectedBuTreeMapping = props.buTreeMappingMaster.filter((data) =>
  //         selectedBU.includes(data.bu),
  //     );
  //     console.log('Filtered Values after select mapping , ', selectedBuTreeMapping);
  //     const expertiesOpions = uniq(selectedBuTreeMapping.map((data) => data.expertise));
  //     const smegOptions = uniq(selectedBuTreeMapping.map((data) => data.sme_group));
  //     setOptions((prevOptions) => {
  //         return { ...prevOptions, expertise: expertiesOpions, smeg: smegOptions };
  //     });
  // };
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
              <ListItemText primary="Filter by" />
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
                // error={errors?.startDate ? true : false}
                // validate={(e) => {
                //   return (
                //     new Date(e) >= new Date(props.projectInfo?.startDate) &&
                //     new Date(e) <= new Date(props.projectInfo?.endDate)
                //   );
                // }}
                onChange={(date: any) => {
                  //console.log("start_date ", date);
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
                  // trigger();
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
                error={errors?.startDate ? true : false}
                // validate={(e) => {
                //   return (
                //     new Date(e) >= new Date(props.projectInfo?.startDate) &&
                //     new Date(e) <= new Date(props.projectInfo?.endDate)
                //   );
                // }}
                onChange={(date: any) => {
                  // updateAllUserAllocationsAccordingToBaseInfos();
                  // trigger();
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
                  onClick={function (e: any): void {
                    reset();
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

export default ReportDashboardFilterForm;
