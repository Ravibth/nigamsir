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

const ReportDashboardFilterForm = (props) => {
  const isCapacityReportTab =
    props.currentReportTab == "1" || props.toggleValue == 1;

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
  const [options, setOptions] = useState({
    businessUnit: [],
    offering: [],
    solution: [],
    competency: [],
    location: [],
    designation: [],
    // grade: [],
  });
  const getOptionsFromUserInfos = () => {
    let tempBusinessUnit = uniq(
      props.currenBuTreeMapping.map((item) => item.bu)
    );
    const tempResult = uniq(
      props.currentCompetencyMapping.map((item) => {
        var _t1 = props.buTreeMappingMaster.filter(
          (x) => x.bu_id === item.buId
        );
        if (_t1 && _t1.length > 0) {
          return _t1[0].bu;
        } else return;
      })
    );
    tempBusinessUnit = [...tempBusinessUnit, ...tempResult];
    let tempLocation = uniq(
      props.locationData.map((item) => item.location_name)
    );
    let tempDesignation = uniq(
      props.designation.map((item) => item.designation_name)
    );
    // const tempGrade = props.designation.map((item) => item.grade);
    let tempOffering = [];
    let tempSolution = [];
    let tempCompetencies = [];
    if (
      props.filterParameters[EReportDashboardFilterControl.businessUnit] &&
      props.filterParameters[EReportDashboardFilterControl.businessUnit]
        .length > 0
    ) {
      tempOffering = uniq(
        props.currenBuTreeMapping
          .filter((item) =>
            props.filterParameters[
              EReportDashboardFilterControl.businessUnit
            ].includes(item.bu)
          )
          .map((item) => item.offering)
      );
      const buIdsParams = uniq(
        props.buTreeMappingMaster
          .filter((x) =>
            props.filterParameters[
              EReportDashboardFilterControl.businessUnit
            ].some((e) => e === x.bu)
          )
          .map((t) => t.bu_id)
      );
      tempCompetencies = uniq(
        props.currentCompetencyMapping
          .filter(
            (cm) => buIdsParams.includes(cm.buId)
            // buIdsParams.filter(
            //   (l) => l.toLowerCase().trim() === cm.buId.toLowerCase().trim()
            // ).length > 0
          )
          .map((x) => x.competency)
      );
    }
    if (
      props.filterParameters[EReportDashboardFilterControl.offering] &&
      props.filterParameters[EReportDashboardFilterControl.offering].length > 0
    ) {
      tempSolution = uniq(
        props.currenBuTreeMapping
          .filter((item) =>
            props.filterParameters[
              EReportDashboardFilterControl.offering
            ].includes(item.offering)
          )
          .map((item) => item.solution)
      );
    }
    tempBusinessUnit.push("");
    tempBusinessUnit.push(undefined);
    tempBusinessUnit = tempBusinessUnit.filter((data) => data?.length > 0);
    tempOffering = tempOffering.filter((data) => data?.length > 0);
    tempSolution = tempSolution.filter((data) => data?.length > 0);
    tempCompetencies = tempCompetencies.filter((data) => data?.length > 0);
    tempLocation = tempLocation.filter((data) => data?.length > 0);
    tempDesignation = tempDesignation.filter((data) => data?.length > 0);

    setOptions({
      businessUnit: uniq(tempBusinessUnit),
      offering: uniq(tempOffering),
      solution: uniq(tempSolution),
      competency: uniq(tempCompetencies),
      location: uniq(tempLocation),
      designation: uniq(tempDesignation),
      // grade: uniq(tempGrade),
    });
  };
  const autoPopulateFilterData = () => {
    Object.keys(props.filterParameters).forEach((key: any) => {
      setValue(key, props.filterParameters[key]);
    });
  };
  useEffect(() => {
    if (props.isFilterOpen && props.currenBuTreeMapping && props.locationData) {
      getOptionsFromUserInfos();
      autoPopulateFilterData();
    }
  }, [
    props.currenBuTreeMapping,
    props.locationData,
    props.isFilterOpen,
    props.currentCompetencyMapping,
  ]);

  const checkIfIsFilterApplied = (data) => {
    if (data && Object.values(data).find((v: any) => v && v.length > 0)) {
      props.setisFilterApplied(true);
      return true;
    } else {
      props.setisFilterApplied(false);
      return false;
    }
  };

  const onSubmit = (data: IReportDashboardFilterControl) => {
    checkIfIsFilterApplied(data);
    props.setFilterParameters(data);
    props.setOpenFilter(false);
  };
  const onBuChange = (selectedBU: string[]) => {
    try {
      let selectedBuTreeMapping = [];
      let buOption = [];
      let competencyOption = [];
      let offeringOpions = [];

      if (selectedBU.length) {
        selectedBuTreeMapping = props.currenBuTreeMapping.filter((data) =>
          selectedBU.includes(data.bu)
        );
        const selectedBuForCompetency = props.buTreeMappingMaster.filter((x) =>
          selectedBU.includes(x.bu)
        );
        competencyOption = uniq(
          props.currentCompetencyMapping
            .filter(
              (item) =>
                selectedBuForCompetency.filter((x) => x.bu_id === item.buId)
                  .length > 0
            )
            .map((x) => x.competency)
        );
        competencyOption = competencyOption.filter((data) => data?.length > 0);

        buOption = uniq(selectedBuTreeMapping.map((data) => data.bu));
        buOption = buOption.filter((data) => data?.length > 0);
        offeringOpions = uniq(
          selectedBuTreeMapping.map((data) => data.offering)
        );
        offeringOpions = offeringOpions.filter((data) => data?.length > 0);

        setOptions((prevOptions) => {
          return {
            ...prevOptions,
            bu: buOption,
            offering: offeringOpions,
            competency: competencyOption,
          };
        });
      } else {
        setOptions((prevOptions) => {
          return {
            ...prevOptions,
            offering: [],
            solution: [],
            competency: [],
          };
        });
      }
      setValue("offering", []);
      setValue("solution", []);
      setValue("competency", []);
    } catch (e) {}
  };
  const onOfferingDataChange = (offering: string[]) => {
    try {
      let solutionOption = [];
      let filteredOffering = [];
      if (offering.length) {
        filteredOffering = props.currenBuTreeMapping.filter((data) => {
          return offering.includes(data.offering);
        });
        solutionOption = uniq(filteredOffering.map((data) => data.solution));
        solutionOption = solutionOption.filter((data) => data?.length > 0);
        setOptions((prevOptions) => {
          return { ...prevOptions, solution: solutionOption };
        });
      } else {
        setOptions((prevOptions) => {
          return { ...prevOptions, solution: [] };
        });
      }
      setValue("solution", []);
    } catch (err) {}
  };
  const resetButton = () => {
    props.setisFilterApplied(false);
    reset();
  };
  return (
    <>
      {props.isEmployeeViewGraph}
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
                <ControllerAutoCompleteChipsSimple
                  name={EReportDashboardFilterControl.businessUnit}
                  control={control}
                  freeSolo={false}
                  multiple={true}
                  defaultValue={[]}
                  label={"Business Unit"}
                  options={options.businessUnit}
                  onChange={() => {
                    const buData: string[] = getValues(
                      EReportDashboardFilterControl.businessUnit
                    );
                    onBuChange(buData);
                  }}
                />
              </Grid>
              <Grid item xs={12}>
                <Divider sx={DividerSxProps} />
              </Grid>
            </Grid>
            {/* {!isCapacityReportTab && (
              <Grid container spacing={2} sx={{ p: 2, width: "400px" }}>
                <Grid item xs={12}>
                  <ControllerAutoCompleteChipsSimple
                    name={EReportDashboardFilterControl.offering}
                    control={control}
                    freeSolo={false}
                    multiple={true}
                    defaultValue={[]}
                    label={"Offering"}
                    options={options.offering}
                    onChange={() => {
                      const offeringData: string[] = getValues(
                        EReportDashboardFilterControl.offering
                      );
                      onOfferingDataChange(offeringData);
                    }}
                  />
                </Grid>
                <Grid item xs={12}>
                  <Divider sx={DividerSxProps} />
                </Grid>
              </Grid>
            )}
            {!isCapacityReportTab && (
              <Grid container spacing={2} sx={{ p: 2, width: "400px" }}>
                <Grid item xs={12}>
                  <ControllerAutoCompleteChipsSimple
                    name={EReportDashboardFilterControl.solution}
                    control={control}
                    freeSolo={false}
                    multiple={true}
                    defaultValue={[]}
                    label={"Solution"}
                    options={options.solution}
                    // onChange={() => {
                    //   //
                    // }}
                  />
                </Grid>
                <Grid item xs={12}>
                  <Divider sx={DividerSxProps} />
                </Grid>
              </Grid>
            )} */}
            <Grid container spacing={2} sx={{ p: 2, width: "400px" }}>
              <Grid item xs={12}>
                <ControllerAutoCompleteChipsSimple
                  name={EReportDashboardFilterControl.competency}
                  control={control}
                  freeSolo={false}
                  multiple={true}
                  defaultValue={[]}
                  label={"Competency"}
                  options={options.competency}
                  // onChange={() => {
                  //   //
                  // }}
                />
              </Grid>
              <Grid item xs={12}>
                <Divider sx={DividerSxProps} />
              </Grid>
            </Grid>
            <Grid container spacing={2} sx={{ p: 2, width: "400px" }}>
              <Grid item xs={12}>
                <ControllerAutoCompleteChipsSimple
                  name={EReportDashboardFilterControl.location}
                  control={control}
                  freeSolo={false}
                  multiple={true}
                  defaultValue={[]}
                  label={"Location"}
                  options={options.location}
                  onChange={() => {
                    //
                  }}
                />
              </Grid>
              <Grid item xs={12}>
                <Divider sx={DividerSxProps} />
              </Grid>
            </Grid>
            {!props.isEmployeeViewGraph ? (
              <>
                <Grid container spacing={2} sx={{ p: 2, width: "400px" }}>
                  <Grid item xs={12}>
                    <ControllerAutoCompleteChipsSimple
                      name={EReportDashboardFilterControl.designation}
                      control={control}
                      freeSolo={false}
                      multiple={true}
                      defaultValue={[]}
                      label={"Designation"}
                      options={options.designation}
                      onChange={() => {
                        //
                      }}
                    />
                  </Grid>
                  <Grid item xs={12}>
                    <Divider sx={DividerSxProps} />
                  </Grid>
                </Grid>
              </>
            ) : (
              <></>
            )}
            {/* {!props.isEmployeeViewGraph ? (
              <>
                <Grid container spacing={2} sx={{ p: 2, width: "400px" }}>
                  <Grid item xs={12}>
                    <ControllerAutoCompleteChipsSimple
                      name={EReportDashboardFilterControl.grade}
                      control={control}
                      freeSolo={false}
                      multiple={true}
                      defaultValue={[]}
                      label={"Grade"}
                      options={options.grade}
                      onChange={() => {
                        //
                      }}
                    />
                  </Grid>
                  <Grid item xs={12}>
                    <Divider sx={DividerSxProps} />
                  </Grid>
                </Grid>
              </>
            ) : (
              <></>
            )} */}

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
    </>
  );
};

export default ReportDashboardFilterForm;
