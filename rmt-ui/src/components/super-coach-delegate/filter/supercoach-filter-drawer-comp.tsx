import {
  Button,
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
import { uniqBy } from 'lodash';
import { ISupercochFiltersOptions } from "../../../common/interfaces/ISupercochFiltersOptions";

interface SupercoachFilterDrawerProps {
  defaultValue: Record<string, any>;
  openDrawer: boolean;
  selectedDataByFilter: (data: Record<string, any>) => void;
  submittedFilterData?: Record<string, any>;
  filterOptions?: ISupercochFiltersOptions;
  handleResetClick?: ()=> void;
  onCloseFliterOption: ()=> void;
}

export interface IDropDownOptions {
  label: string;
  id: string;
  labelId: string;
  isActive: boolean;
  grade?: string;
  buId?:string;
  designation_id?: string
}


type FilterFields = 'businessunit' | 'competency' | 'grade' | 'employees' | 'designation' |  'location'  ;

const filterFieldLabelPairs: { field: FilterFields; label: string }[] = [
  { field: 'businessunit', label: 'Business Unit' },
  { field: 'competency', label: 'Competency' },
  { field: 'grade', label: 'Grade' },
  { field: 'designation', label: 'Designation' },
  { field: 'employees', label: 'Employees' },
  { field: 'location', label: 'Location' },
];

type FormValues = Record<FilterFields, IDropDownOptions[] | string[]>;

const defaultFormValues: FormValues = {
  businessunit: [],
  competency: [],
  grade: [],
  designation: [],
  employees:[],
  location: []   
};

const SupercoachFilterDrawerComp = ({
  defaultValue = defaultFormValues,
  selectedDataByFilter,
  submittedFilterData,
  filterOptions,
  handleResetClick = () => {}, 
  onCloseFliterOption
}: SupercoachFilterDrawerProps) => {
  const loaderContext = useContext(LoaderContext);
  
  const { control, getValues, setValue, reset, watch, handleSubmit } = useForm<FormValues>({
    mode: "onTouched",
    defaultValues: defaultFormValues,
  });

  const watchedValues = watch();

  const isAnyFieldSelected = useMemo(() => {
    const values = getValues();
    return Object.values(values).some(fieldValue => 
      Array.isArray(fieldValue) ? fieldValue.length > 0 : !!fieldValue
    );
  }, [watchedValues]);

  const options = useMemo(() => {
    if (!filterOptions) return {
      businessunit: [],
      competency: [],
      grade: [],
      designation: [],
      employees:[],
      location: []   
    };
   
    const sortByLabel = (a: IDropDownOptions, b: IDropDownOptions) => 
      a?.label?.localeCompare(b?.label);
    const sortStrings = (a: string, b: string) => 
      (a || '').localeCompare(b || '', undefined, { sensitivity: 'base' });

    const grades = filterOptions.designations?.reduce((acc, d) => {
      if (d?.grade && !acc.includes(d.grade)) acc.push(d.grade);
      return acc;
    }, [] as string[]);

    return {
      businessunit:  uniqBy(
        filterOptions.businessunit?.map(d => ({
          id: d.bu,
          label: d.bu
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel), 

      competency: uniqBy(
        filterOptions.competency?.map(d => ({
          id: d.competencyId,
          label: d.competency,
          buId: d.buId
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),
     
      employees: uniqBy(
        filterOptions.employees?.map(e => ({
          id: e.employee_mid,
          label: `${e.name} - ${e.employee_mid}__${e.email_id}`,
          designation_id: e.designation_id,
        })) || [],
        item => item.id
      ),

      location: uniqBy(
        filterOptions.location?.map(d => ({
          id: d.location_id,
          label: d.location_name,
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),
     
      grade: uniqBy(
        filterOptions.designations?.map(d => ({
          id: d.grade,
          label: d.grade,
          designation_id: d.designation_id
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),

      designation: uniqBy(
        filterOptions.designations?.map(d => ({
          id: d.designation_id,
          label: d.designation_name,
          grade: d.grade
        })) || [],
        item => `${item.id}_${item.label}`
      ).sort(sortByLabel),      
    };
  }, [filterOptions]);

  const filterUnselectedOptions = useCallback((fieldName: FilterFields) => {
    const selected = getValues(fieldName) as IDropDownOptions[] || [];
    let fieldOptions = (options[fieldName] ?? []) as IDropDownOptions[];

    if (fieldName === "competency") {
    const selectedBusinessUnits = getValues("businessunit") as IDropDownOptions[];
      if (selectedBusinessUnits?.length > 0) {
        const selectedBuIds = filterOptions.businessunit.filter(option=> selectedBusinessUnits.some(selectedItem => selectedItem.id === option.bu))?.map(bu => bu.bu_id);
        fieldOptions = fieldOptions.filter(option => {
          return selectedBuIds.includes(option.buId);
        });
      }
    }

    if (fieldName === "designation") {
    const selectedGrade = getValues("grade") as IDropDownOptions[];
      if (selectedGrade?.length > 0) {
        const selectedIds = selectedGrade.map(a => a.id);
        fieldOptions = fieldOptions.filter(option => {
          return selectedIds.includes(option.grade);
        });
      }
    }

     if (fieldName === "employees") {
    const selectedDesignation = getValues("designation") as IDropDownOptions[];
      if (selectedDesignation?.length > 0) {
        const selectedIds = selectedDesignation.map(a => a.id);
        fieldOptions = fieldOptions.filter(option => {
          return selectedIds.includes(option.designation_id); 
        });
      }
    }

    return fieldOptions.filter(
      option => !selected.some(selectedItem => selectedItem.id === option.id)
    );
  }, [getValues, options, filterOptions]);

  const filteredOptions = useMemo(() => {
    const result: Record<FilterFields, IDropDownOptions[]> = {
      businessunit: [],
      competency: [],
      grade: [],
      designation: [],
      employees:[],
      location: []   
    };

    (Object.keys(options) as FilterFields[]).forEach(key => {
      result[key] = filterUnselectedOptions(key) ?? [];
    });

    return result;
  }, [watchedValues, filterUnselectedOptions]);

  useEffect(() => {
    if (defaultValue) {
      reset(defaultValue);
    }
  }, [defaultValue, reset]);

  useEffect(() => {
    if (submittedFilterData) {
      reset(Object.keys(submittedFilterData)?.length > 0 ? submittedFilterData : defaultValue);
    }
  }, [submittedFilterData, defaultValue, reset]);

  const onSubmit = useCallback((data: FormValues) => {
    selectedDataByFilter(data);    
    
  }, [selectedDataByFilter]);

  const handleReset = useCallback(() => {
    reset(defaultFormValues);
    handleResetClick();
  }, [selectedDataByFilter]);

  const handleFieldChange = (fieldName: FilterFields) => {
    // Clear dependent fields when parent field changes
    const clearDependentFields: Record<FilterFields, FilterFields[]> = {
      businessunit: ['competency'],
      competency: [],
      grade: ['designation'],
      designation: [],    
      employees: [],
      location: []    
    };
    clearDependentFields[fieldName].forEach(field => {
      setValue(field, []);
    });

    setValue(fieldName, getValues(fieldName), { shouldDirty: true });
  };

  const renderAutocompleteField = (name: FilterFields, label: string) => (
    <ControllerAutocompleteFilteredOptionsTextfield
      name={name}
      control={control}
      defaultValue={[]}
      multiple={true}
      sx={constant.AutocompleteSxProps}
      options={filteredOptions[name] || []}
      onChange={() => handleFieldChange(name)}
      label={label}
    />
  );

  return (
    <List>
      <form onSubmit={handleSubmit(onSubmit)}>
        <ListItem>
          <ListItemText className="filter-header" primary="Filter By" />
          <IconButton onClick={onCloseFliterOption}>
            <Tooltip title="close">
              <CloseIcon />
            </Tooltip>
          </IconButton>
        </ListItem>
        <Card sx={{ margin: "10px", border: "1px solid lightgray", borderRadius: "10px",overflow: "auto",maxHeight: "660px" }}>
          <CardContent>
              {filterFieldLabelPairs.map(({ field, label }) => (
            <ListItem key={field} className="filter-control-container">
              {renderAutocompleteField(field, label)}
            </ListItem>
          ))}
          </CardContent>
        </Card>
        <div className="filter-btn-container">
          <Button
            variant="contained"
            type="submit"
            className="rmt-action-button"
            sx={constant.ApplyFilterButtonSxProps}
            disabled={!isAnyFieldSelected}
          >
            Apply Filter
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
  );
};

export default SupercoachFilterDrawerComp;