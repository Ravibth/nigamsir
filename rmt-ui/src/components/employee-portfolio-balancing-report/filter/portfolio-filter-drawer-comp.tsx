import {
  Button,
  Drawer,
  List,
  ListItem,
  ListItemText,
  IconButton,
  Tooltip,
  Card,
  CardContent,
} from "@mui/material";
import { useContext, useEffect, useMemo, useCallback } from "react";
import CloseIcon from "@mui/icons-material/Close";
import * as constant from "./constant";
import { useForm } from "react-hook-form";
import ControllerAutocompleteFilteredOptionsTextfield from "../../controllers/controller-autocomplete-filtered-options-textfield";
import { LoaderContext, LoaderContextProps } from "../../../contexts/loaderContext";
import { IPortfolioFiltersOptions } from "../../../common/interfaces/IPortfolioFiltersOptions";
import { uniqBy } from 'lodash';
import ControllerOnlyNumberTextField from "../../controllerInputs/controllerOnlyNumberTextfield";
import { IDropDownOptions } from "../../skills/skills-search/util";

interface PortfolioFilterDrawerProps {
  defaultValue: any;
  openDrawer: boolean;
  setOpenDrawer: (open: boolean) => void;
  selectedDataByFilter: (data: any) => void;
  submittedFilterData?: any;
  filterOptions?: IPortfolioFiltersOptions;
}


const PortfolioFilterDrawerComp = ({
  defaultValue,
  openDrawer,
  setOpenDrawer,
  selectedDataByFilter,
  submittedFilterData,
  filterOptions,
}: PortfolioFilterDrawerProps) => {
  const loaderContext = useContext(LoaderContext);
  type FilterField = Exclude<keyof typeof options, 'availability'>;
  const { control, getValues, setValue, reset,watch, handleSubmit } = useForm({
    mode: "onTouched",
    defaultValues: {
      availability: 0,
      grade: [],
      designation: [],
      employeename: [],
      location: [] ,
      supercoach: [],
      cosupercoach: [],
      clientname: [],
      clientgroupname: [],

    },
  });
  const watchedValues = watch();
  const options = useMemo(() => {
    if (!filterOptions) return {
      availability: 0,
      grade: [],
      designation: [],
      employeename: [],
      location: [] ,
      supercoach: [],
      cosupercoach: [],
      clientname: [],
      clientgroupname: [],
    };
   
    const sortByLabel = (a: IDropDownOptions, b: IDropDownOptions) => a?.label?.localeCompare(b?.label);
    const sortStrings = (a: string, b: string) => 
      (a || '').localeCompare(b || '', undefined, { sensitivity: 'base' });

    const grades = filterOptions.designations?.reduce((acc, d) => {
      if (d?.grade && !acc.includes(d.grade)) acc.push(d.grade);
      return acc;
    }, [] as string[]);

    return {

      availability: 0,
      grade: grades?.sort(sortStrings) || [],

      designation: uniqBy(
        filterOptions.designations?.map(d => ({
          id: d.designation_id,
          label: d.designation_name,
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),
      
      employeename: uniqBy(
        filterOptions.employees?.map(e => ({
          id: e.employee_mid,
          label: `${e.name} - ${e.employee_mid}__${e.email_id}`,
        })) || [],
        item => item.id
      ),
     
      location: uniqBy(
      filterOptions.locations?.map(e => ({
        id: e.location_id,
        label: e.location_name,
      })) || [],
      item => item.id
    ),      
      
      supercoach: uniqBy(
        filterOptions.supercoaches?.map(s => ({
          id: s.employee_mid,
          label: `${s.name} - ${s.employee_mid}__${s.email_id}`,
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),
      
      cosupercoach: uniqBy(
        filterOptions.cosupercoaches?.map(c => ({
          id: c.employee_mid,
          label: `${c.name} - ${c.employee_mid}__${c.email_id}`,
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),
      
      clientname: uniqBy(
        filterOptions.clients?.map(c => ({
          id: c.client_id,
          label: c.job_client,
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),
      
      clientgroupname: uniqBy(
        filterOptions.clients?.map(c => ({
          id: c.client_group_code,
          label: c.client_group_name,
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),
    };
  }, [filterOptions]);


  const filterUnselectedOptions = useCallback((fieldName: FilterField) => {
    const selected = (getValues(fieldName) as IDropDownOptions[]) ?? [];
    let fieldOptions = options[fieldName] as IDropDownOptions[] || [];

    if (fieldName === "grade") {
      const selectedGrades = getValues("grade") as string[] || [];
      if (selectedGrades.length > 0) {
        return fieldOptions.filter(
         option => !selected.some(selectedItem => selectedItem === option)
        )
      }
    }
  
    if (fieldName === "designation") {
      const selectedGrades = getValues("grade") as string[] || [];
      if (selectedGrades.length > 0) {
        fieldOptions = fieldOptions.filter(option => {
          const match = filterOptions?.designations?.find(d => d.designation_id === option.id);
          return match && selectedGrades.includes(match.grade);
        });
      }
    }
  
      if (fieldName === "employeename") {
        const selectedDesignations = getValues("designation") as IDropDownOptions[] || [];
        const selectedDesignationIds = selectedDesignations.map(d => d.id);

        if (selectedDesignationIds.length > 0) {
          fieldOptions = fieldOptions.filter(option => {
            // Find the employee object for this option
            const emp = filterOptions?.employees?.find(e => e.employee_mid === option.id);
            // Only include if employee exists and their designation_id is NOT in selectedDesignationIds
            return emp && selectedDesignationIds.includes(emp.designation_id);
          });
        }
      }
    
     if (fieldName === "location") {
        const selectedlocation = getValues("location") as IDropDownOptions[] || [];
        const selectedLocationsIds = selectedlocation.map(d => d.id);

        if (selectedlocation.length > 0) {
          fieldOptions = fieldOptions.filter(option => {
            const match = filterOptions?.locations?.find(l => l.location_id === option.id);
            return match && !selectedLocationsIds.includes(match.location_id);
          });
        }
      }

  
    if (fieldName === "supercoach") {
      const selectedEmployees = getValues("supercoach") as IDropDownOptions[] || [];
      const selectedEmployeeIds = selectedEmployees.map(e => e.id);
  
      if (selectedEmployees.length > 0) {
        fieldOptions = fieldOptions.filter(option => {
          // Find employees who match the selected employee ids
          const matchingEmployees = filterOptions?.employees?.filter(emp =>
            selectedEmployeeIds.includes(emp.employee_mid)
          );
          // Then check if this supercoach is assigned to any of them
          return !matchingEmployees?.some(emp => emp.supercoach_mid === option.id);
        });
      }
    }

    if (fieldName === "cosupercoach") {
      const selectedEmployees = getValues("cosupercoach") as IDropDownOptions[] || [];
      const selectedEmployeeIds = selectedEmployees.map(e => e.id);
  
      if (selectedEmployeeIds.length > 0) {
        fieldOptions = fieldOptions.filter(option => {
          // Find employees who match the selected employee ids
          const matchingEmployees = filterOptions?.employees?.filter(emp =>
            !selectedEmployeeIds.includes(emp.employee_mid)
          );
          // Then check if this supercoach is assigned to any of them
          return matchingEmployees?.some(emp => emp.reporting_partner_mid === option.id);
        });
      }
    }

    if (fieldName === "clientgroupname") {
      const selectedClients = getValues("clientname") as IDropDownOptions[] || [];
      const selectedClientIds = selectedClients.map(c => c.id);
  
      if (selectedClientIds.length > 0) {
        fieldOptions = fieldOptions.filter(option => {
          const matchingClients = filterOptions?.clients?.filter(c =>
            selectedClientIds.includes(c.client_id)
          );
          return matchingClients?.some(c => c.client_group_code === option.id);
        });
      }
    }
  
    return fieldOptions.filter(
      option => !selected.some(selectedItem => selectedItem.id === option.id)
    );
  }, [getValues, options, filterOptions]);
  

  const filteredOptions = useMemo(() => {
    // Only include keys that are valid FilterField (exclude "availability")
    const filterFields: FilterField[] = [
      "grade",
      "designation",
      "employeename",
      "location",
      "supercoach",
      "cosupercoach",
      "clientname",
      "clientgroupname",
    ];
    return filterFields.reduce((acc, fieldName) => {
      acc[fieldName] = filterUnselectedOptions(fieldName);
      return acc;
    }, {} as Record<FilterField, IDropDownOptions[]>);
  }, [watchedValues, filterUnselectedOptions]);

  useEffect(() => {
    if (defaultValue) {
      reset(defaultValue);
    }
  }, [defaultValue, reset]);

  useEffect(() => {
    if (submittedFilterData) {
      reset(Object.keys(submittedFilterData).length > 0 ? submittedFilterData : defaultValue);
    }
  }, [submittedFilterData, defaultValue, reset]);

  const onSubmit = useCallback((data: any) => {
    //loaderContext.open(true);
    selectedDataByFilter(data);
    setOpenDrawer(false);
   // loaderContext.open(false);
  }, [loaderContext, selectedDataByFilter, setOpenDrawer]);

  const handleReset = useCallback(() => {
    selectedDataByFilter({
      availability: 0,
      grade: [],
      designation: [],
      employeename: [],
      location: [],
      supercoach: [],
      cosupercoach: [],
      clientname: [],
      clientgroupname: [],
    });
  }, [selectedDataByFilter]);

  const onChange = (controlName) => {
  //   if (controlName == "grade") {
  //     setValue("designation", []);
  //     setValue("employeename", []);
  //     setValue("location", []);
  //     setValue("supercoach", []);
  //     setValue("cosupercoach", []);       
  //   }

  if (controlName === "designation") {
    if (getValues("employeename") != null && getValues("employeename").length > 0) {
      const selectedDesignations = getValues("designation") as IDropDownOptions[] || [];
      const selectedDesignationIds = selectedDesignations.map(d => d.id);
      const selectedEmployees = getValues("employeename") as IDropDownOptions[] || [];
      const filteredEmployees = selectedEmployees.filter(emp => {
        const empObj = filterOptions?.employees?.find(e => e.employee_mid === emp.id);
        return empObj && selectedDesignationIds.includes(empObj.designation_id);
      });
      setValue("employeename", filteredEmployees);
    }
  }
    
  //   setValue("location", []);
  //   setValue("supercoach", []);
  //   setValue("cosupercoach", []);
  // }

  //   if (controlName == "employeename") { 
  //     setValue("location", []);      
  //   }

    if (controlName === "clientname") {
      const selectedClients = getValues("clientname") as IDropDownOptions[] || [];
      const selectedClientIds = selectedClients.map(c => c.id);
      const selectedGroups = getValues("clientgroupname") as IDropDownOptions[] || [];
      const filteredGroups = selectedGroups.filter(group => {
        // Find all clients that belong to this group
        const matchingClients = filterOptions?.clients?.filter(c =>
          selectedClientIds.includes(c.client_id)
        );
        // Keep group if any selected client belongs to it
        return matchingClients?.some(c => c.client_group_code === group.id);
      });
      setValue("clientgroupname", filteredGroups);   
    }
  
    setValue(controlName, getValues(controlName), { shouldDirty: true });
  }

  const renderAutocompleteField = (name: keyof typeof options, label: string) => (
    <ControllerAutocompleteFilteredOptionsTextfield
      name={name}
      control={control}
      defaultValue={[]}
      multiple={true}
      sx={constant.AutocompleteSxProps}
      options={filteredOptions[name]}
      onChange={() => {
        // Force re-filtering by causing a state update
        onChange(name)
      }}
      label={label}
    />
  );

  return (
    <Drawer
      open={openDrawer}
      onClose={() => setOpenDrawer(false)}
      sx={constant.DrawerSxProps}
    >
      <List>
        <form onSubmit={handleSubmit(onSubmit)}>
          <ListItem>
            <ListItemText className="filter-header" primary="Filter By" />
            <IconButton onClick={() => setOpenDrawer(false)}>
              <Tooltip title="close">
                <CloseIcon />
              </Tooltip>
            </IconButton>
          </ListItem>
          <Card sx={{ margin: "10px", border: "1px solid lightgray", borderRadius: "10px" }}>
            <CardContent>
              <ListItem className="filter-control-container">
                <ControllerOnlyNumberTextField
                  name="availability"
                  control={control}
                  label="Availability"
                />
              </ListItem>
              <ListItem className="filter-control-container">
                {renderAutocompleteField("grade", "Grade")}
              </ListItem>
              <ListItem className="filter-control-container">
                {renderAutocompleteField("designation", "Designation")}
              </ListItem>
              <ListItem className="filter-control-container">
                {renderAutocompleteField("employeename", "Employee Name")}
              </ListItem>
               <ListItem className="filter-control-container">
                {renderAutocompleteField("location", "Location")}
              </ListItem>
              <ListItem className="filter-control-container">
                {renderAutocompleteField("supercoach", "Supercoach")}
              </ListItem>
              <ListItem className="filter-control-container">
              {renderAutocompleteField("cosupercoach", "Co-Supercoach")}
              </ListItem>
              <ListItem className="filter-control-container">
              {renderAutocompleteField("clientname", "Client Name")}
              </ListItem>
              <ListItem className="filter-control-container">
              {renderAutocompleteField("clientgroupname", "Client Group Name")}
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
              onClick={handleReset}
            >
              Reset
            </Button>
          </div>
        </form>
      </List>
    </Drawer>
  );
};

export default PortfolioFilterDrawerComp;