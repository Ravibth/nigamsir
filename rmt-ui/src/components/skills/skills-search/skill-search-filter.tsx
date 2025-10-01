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
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";
import ControllerAutoCompleteChipsSimple from "../../controllerInputs/controllerAutoCompleteChipsSimple";
import BackActionButton from "../../actionButton/backactionButton";
import ActionButton from "../../actionButton/actionButton";
import { IBUTreeMapping } from "../../../common/interfaces/IBuTreeMapping";
import { EReportDashboardFilterControl, IReportDashboardFilterControl } from "../../Reports/Filters/uitls";
import { DividerSxProps } from "../../appbar/constant";

// export interface IReportDashboardFilterForm {
//   setOpenFilter: React.Dispatch<React.SetStateAction<boolean>>;
//   isFilterOpen: boolean;
//   buTreeMappingMaster: IBUTreeMapping[];  
//   filterParameters: IReportDashboardFilterControl;
//   setFilterParameters: React.Dispatch<
//     React.SetStateAction<IReportDashboardFilterControl>
//   >;
//   GetFilterDefaultValueOnTheBasisOfRole: (
//     userRole?: string
//   ) => IReportDashboardFilterControl;
// }

const SkillSearchFilterForm = (props) => {
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
    competency: [],    
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
    let tempCompetencies = [];
    if (
      props.filterParameters[EReportDashboardFilterControl.businessUnit] &&
      props.filterParameters[EReportDashboardFilterControl.businessUnit]
        .length > 0
    ) {
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
    // tempBusinessUnit.push("");
    // tempBusinessUnit.push(undefined);
    tempBusinessUnit = tempBusinessUnit.filter((data) => data?.length > 0);
    tempCompetencies = tempCompetencies.filter((data) => data?.length > 0);    
    setOptions({
      businessUnit: uniq(tempBusinessUnit),            
      competency: uniq(tempCompetencies),                  
    });
  };
  const autoPopulateFilterData = () => {
    Object.keys(props.filterParameters).forEach((key: any) => {
      setValue(key, props.filterParameters[key]);
    });
  };
  useEffect(() => {
    if (props.isFilterOpen && props.currenBuTreeMapping) {
      getOptionsFromUserInfos();
      autoPopulateFilterData();
    }
  }, [
    props.currenBuTreeMapping,    
    props.isFilterOpen,
    props.currentCompetencyMapping,
    props.buTreeMappingMaster,
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
        setOptions((prevOptions) => {
          return {
            ...prevOptions,
            bu: buOption,            
            competency: competencyOption,
          };
        });
      } else {
        setOptions((prevOptions) => {
          return {
            ...prevOptions,
            competency: [],
          };
        });
      }            
      setValue("competency", []);
    } catch (e) {}
  };
  const resetButton = () => {
    props.setisFilterApplied(false);
    reset();
    props.reset();
  };
  return (
    <>      
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
                  required = {true}
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
    </>
  );
};

export default SkillSearchFilterForm;
