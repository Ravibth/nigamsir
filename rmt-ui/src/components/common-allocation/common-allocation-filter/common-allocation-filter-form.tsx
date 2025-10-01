import {
  Box,
  Card,
  CardContent,
  Drawer,
  Grid,
  IconButton,
  ListItem,
  ListItemText,
  Tooltip,
} from "@mui/material";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { IAllUserAllocationEntries } from "../interface";
import {
  DefaultCommonAllocationFillerControlValues,
  ECommonAllocationFilterControl,
  ICommonAllocationFilterControl,
  ICommonAllocationFilterOptions,
} from "./utils";
import { useForm } from "react-hook-form";
import { IUserInfo } from "../../system-suggestions/availability-view/constants";
import { useEffect, useState } from "react";
import ControllerAutoCompleteChipsSimple from "../../controllerInputs/controllerAutoCompleteChipsSimple";
import CloseIcon from "@mui/icons-material/Close";
import BackActionButton from "../../actionButton/backactionButton";
import ActionButton from "../../actionButton/actionButton";
import uniq from "lodash/uniq";
import "./common-allocation-filter.css";
import { AutocompleteSxProps } from "../../filter/project-filter/constant";
import { IAllocateFormSkills } from "../common-allocation-modal-form/utils";

export interface ICommonAllocationFilterForm {
  projectInfo: IProjectMaster;
  isFilterOpen: boolean;
  setOpenFilter: React.Dispatch<React.SetStateAction<boolean>>;
  allUserAllocationEntries: IAllUserAllocationEntries[];
  filteredValues: ICommonAllocationFilterControl;
  setFilteredValues: React.Dispatch<
    React.SetStateAction<ICommonAllocationFilterControl>
  >;
  resourcesList: IUserInfo[];
  skillOptions: IAllocateFormSkills[];
}
const CommonAllocationFilterForm = (props: ICommonAllocationFilterForm) => {
  const { handleSubmit, control, setValue, reset } =
    useForm<ICommonAllocationFilterControl>({
      mode: "onTouched",
      defaultValues: DefaultCommonAllocationFillerControlValues,
    });

  const [options, setOptions] = useState<ICommonAllocationFilterOptions>({
    location: [],
    skills: [],
    businessUnit: [],
    competency: [],
  });

  const getOptionsFromUserInfos = () => {
    const tempLocationOptions = props.resourcesList.map(
      (item) => item.location
    );
    const tempBusinessUnitOptions = props.resourcesList.map(
      (item) => item.businessUnit
    );

    const tempCompetencyOptions = props.resourcesList.map(
      (item) => item.competency
    );

    setOptions({
      location: uniq(tempLocationOptions),
      skills: props.skillOptions,
      competency: uniq(tempCompetencyOptions),
      businessUnit: uniq(tempBusinessUnitOptions),
    });
  };

  const autoPopulateData = () => {
    Object.keys(props.filteredValues).forEach((key: any) => {
      setValue(key, props.filteredValues[key]);
    });
  };

  useEffect(() => {
    if (props.isFilterOpen) {
      getOptionsFromUserInfos();
      autoPopulateData();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.resourcesList, props.isFilterOpen, props.skillOptions]);

  const onSubmit = (data: ICommonAllocationFilterControl) => {
    props.setFilteredValues(data);
    props.setOpenFilter(false);
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
              <ListItemText
                className="filter-header-common"
                primary="Filter By"
              />
              <IconButton
                onClick={() => {
                  props.setOpenFilter(false);
                }}
              >
                <Tooltip title="close">
                  <CloseIcon />
                </Tooltip>
              </IconButton>
            </ListItem>
          </Grid>
          <Card
            style={{
              margin: "10px",
              border: "1px solid lightgray",
              borderRadius: "10px",
            }}
          >
            <CardContent>
              <Grid container spacing={2} sx={{ p: 2, maxWidth: "350px" }}>
                <Grid item xs={12}>
                  <ControllerAutoCompleteChipsSimple
                    name={ECommonAllocationFilterControl.location}
                    control={control}
                    freeSolo={false}
                    multiple={true}
                    defaultValue={[]}
                    label={"Location"}
                    options={options.location}
                    onChange={() => {}}
                    sx={AutocompleteSxProps}
                  />
                </Grid>
                <Grid item xs={12}></Grid>
                <Grid item xs={12}>
                  <ControllerAutoCompleteChipsSimple
                    name={ECommonAllocationFilterControl.businessUnit}
                    control={control}
                    multiple={true}
                    freeSolo={false}
                    defaultValue={[]}
                    label={"Business Unit"}
                    options={options.businessUnit}
                    onChange={() => {}}
                    sx={AutocompleteSxProps}
                  />
                </Grid>
                <Grid item xs={12}></Grid>
                <Grid item xs={12}>
                  <ControllerAutoCompleteChipsSimple
                    name={ECommonAllocationFilterControl.competency}
                    multiple={true}
                    control={control}
                    freeSolo={false}
                    defaultValue={[]}
                    label={"Competency"}
                    options={options.competency}
                    onChange={() => {}}
                    sx={AutocompleteSxProps}
                  />
                </Grid>
                <Grid item xs={12}></Grid>
                <Grid item xs={12}>
                  <ControllerAutoCompleteChipsSimple
                    name={ECommonAllocationFilterControl.skills}
                    control={control}
                    multiple={true}
                    freeSolo={false}
                    defaultValue={[]}
                    label={"Skills"}
                    options={options.skills}
                    onChange={() => {}}
                    getOptionLabel={(option) => {
                      return `${option.skillCode}--${option.skillName}`;
                    }}
                    sx={AutocompleteSxProps}
                  />
                </Grid>
                <Grid item xs={12}></Grid>
              </Grid>
            </CardContent>
          </Card>
          <Grid className="filter-btn-container">
            <Grid item xs={4}>
              <ActionButton
                label={"Apply Filters"}
                onClick={function (e: any): void {}}
                disabled={false}
                type={"submit"}
                textTransform="none"
              />
            </Grid>
            <Grid item xs={1}></Grid>
            <Grid item xs={4}>
              <BackActionButton
                label={"Reset"}
                textTransform="none"
                onClick={function (e: any): void {
                  reset();
                }}
              />
            </Grid>
          </Grid>
        </form>
      </Drawer>
    </Box>
  );
};

export default CommonAllocationFilterForm;
