import { useContext, useEffect, useRef, useState } from "react";
import {
  IIndustryMappingPreference,
  IIndustryMappingPreferenceList,
  IIndustryMappingPreferenceOptions,
  IIndustryOption,
  IIndustryOptionList,
  IIndustryPreferenceProps,
  ISubIndustryOption,
  ISubIndustryOptionList,
} from "./interface";
import _ from "lodash";
import "./style.css";
import { getIndustryMasterFromWCGT } from "../../../../services/wcgt-master-services/wcgt-master-services";
import { IIndustryMasterList } from "../../../../common/interfaces/IIndustryMaster";
import AgGridComponent from "../../../aggrid-component/aggrid-component";
import ActionButton from "../../../actionButton/actionButton";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import { useForm } from "react-hook-form";
import { industryMappingPreferenceName } from "./constant";
import { Grid, IconButton, Tooltip, Typography } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import { DeleteIconSxProps, UpdateIconSxProps } from "../../../activeRequisitionsDeatils/activerequisition/requisitiontable/constant";
import { UserDetailsContext } from "../../../../contexts/userDetailsContext";
import { PreferenceCategories } from "../../constant";
import SectionHeader from "../../section-header/section-header";
import { SnackbarContext } from "../../../../contexts/snackbarContext";
import ControllerTextField from "../../../controllerInputs/controllerTextField";
import ControllerNumberTextField from "../../../controllerInputs/controllerNumbeTextfield";
import { EditRounded } from "@mui/icons-material";
import EditIndustryExperienceModal from "./EditIndustryExperienceModal";

const IndustryPreference = (props: IIndustryPreferenceProps) => {
  const [industryMaster, setIndustryMaster] = useState<IIndustryMasterList>([]);
  const [options, setOptions] =
    useState<IIndustryMappingPreferenceOptions>(null);
  const [openEditModal, setOpenEditModal] = useState(false);
  const [editingRow, setEditingRow] = useState<IIndustryMappingPreference | null>(null);
  const userContext = useContext(UserDetailsContext);
  const snackbarContext: any = useContext(SnackbarContext);

  const gridRef: any = useRef();
  const {
    handleSubmit,
    setValue,
    reset,
    getValues,
    trigger,
    control,
    formState: { errors, isDirty },
  } = useForm<any>({
    mode: "onTouched",
    defaultValues: {
      industry: null,
      subIndustry: null,
      yearsOfExp: 0,
      details: "",
    },
  });
  const GetIndustryMaster = async () => {
    try {
      return await getIndustryMasterFromWCGT();
    } catch (e) {
      throw e;
    }
  };
  useEffect(() => {
    Promise.all([GetIndustryMaster()]).then((response) => {
      const industryMaster: IIndustryMasterList = response[0];
      setIndustryMaster(industryMaster);
      const defaultOptions = GetDefaultValues(industryMaster);
      setOptions(defaultOptions);
    });
  }, []);

  useEffect(() => {
    if (props.employeePreference && props.employeePreference.length > 0) {
      const preferences: IIndustryMappingPreferenceList =
        props.employeePreference
          .filter(
            (e) =>
              e.category.toLowerCase().trim() ===
                PreferenceCategories.INDUSTRY_MAPPING.toLowerCase().trim() &&
              e.isActive === true
          )
          .map((e) => {
            return {
              id: e.id,
              employeeEmail: e.employeeEmail,
              preferenceOrder: e.preferenceOrder,
              isActive: e.isActive,
              category: e.category,
              industry: e.preferenceDetails.industry,
              subIndustry: e.preferenceDetails.subIndustry,
              year_of_experience: e.preferenceDetails.year_of_experience || 0,
              description: e.preferenceDetails.description || "",
            };
          });      
      props.setRowData(preferences);
    }
  }, [props.employeePreference]);

  const GetDefaultValues = (industryMaster: IIndustryMasterList) => {
    const industryOption: IIndustryOptionList = industryMaster.map((e) => {
      return {
        industry_id: e.industry_id,
        industry_name: e.industry_name,
      };
    });
    const uniqIndustryOptions: IIndustryOptionList = _.uniqWith(
      industryOption,
      (a, b) => a.industry_id === b.industry_id
    ).filter((a) => a && a.industry_id && a.industry_name);
    //   console.log(uniqBuOptions);
    const defaultOptions: IIndustryMappingPreferenceOptions = {
      industryOptions: uniqIndustryOptions,
      subIndustryOptions: [],
    };
    return defaultOptions;
  };

  const onIndustryChange = (selectedIndustry: IIndustryOption) => {
    // console.log(selectedBu);
    if (selectedIndustry === null) {
      reset();
      trigger();
      setOptions((prev) => {
        return {
          industryOptions: prev.industryOptions,
          subIndustryOptions: [],
        };
      });
      return;
    }
    const selectedIndustries = industryMaster.filter((item) => {
      return selectedIndustry.industry_id === item.industry_id;
    });
    const subIndustryOptions: ISubIndustryOptionList = selectedIndustries.map(
      (item) => {
        return {
          sub_industry_id: item.sub_industry_id,
          sub_industry_name: item.sub_industry_name,
        };
      }
    );
    const uniqSubIndustryOptions: ISubIndustryOptionList = _.uniqWith(
      subIndustryOptions,
      (a, b) => a.sub_industry_id === b.sub_industry_id
    ).filter((a) => a && a.sub_industry_id && a.sub_industry_name);
    setOptions((prev) => {
      return {
        industryOptions: prev.industryOptions,
        subIndustryOptions: uniqSubIndustryOptions,
      };
    });
    setValue(industryMappingPreferenceName.subIndustry, null);
  };
  const handleDeleteMap = (data, rowNode) => {
    // console.log(data, rowNode);
    if (gridRef?.current?.api) {
      // const rowNode = gridRef?.current?.api?.getDisplayedRowAtIndex(index);
      const res = gridRef?.current?.api?.applyTransaction({
        remove: [rowNode.data],
      });
      //   console.log(res);
      const currentRowData = [];
      gridRef.current.api.forEachNode((node) => currentRowData.push(node.data));
      //   console.log(currentRowData);
      props.setRowData(currentRowData);
    }
  };
  const handleEditMap = (data: IIndustryMappingPreference, rowNode: any) => {
    setEditingRow(data);
    setOpenEditModal(true);
};

  const ActionCellRenderer = (props: any) => (
    <div>
    <Tooltip title="Delete">
      <IconButton
        // disabled={isReadOnlyModeOn}
        onClick={(e) => {
          handleDeleteMap(props?.data, props.node);
        }}
      >
        <DeleteIcon fontSize="small" sx={DeleteIconSxProps} />
      </IconButton>
    </Tooltip>
    <Tooltip title="Edit">
      <IconButton
        // disabled={isReadOnlyModeOn}
        onClick={(e) => {
          handleEditMap(props?.data, props.node);
        }}
      >
        <EditRounded fontSize="small" sx={UpdateIconSxProps}/>
      </IconButton>
    </Tooltip>
    </div>
  );


  
  const colDef = [
    {
      field: "industry",
      headerName: "Industry",
      width: 400,
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      suppressMovable: true,
      sortable: true,
      unSortIcon: true,
      valueGetter: (params) => params?.data?.industry?.industry_name,
      cellRenderer: (props) => {
        const data: IIndustryMappingPreference = props.data;
        return data?.industry ? data.industry.industry_name : "";
      },
      tooltipValueGetter: (props) => {
        const data: IIndustryMappingPreference = props.data;
        return data?.industry ? data.industry.industry_name : "";
      },
    },
    {
      field: "subIndustry",
      headerName: "Sub Industry",
      width: 400,
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      suppressMovable: true,
      sortable: true,
      unSortIcon: true,
      valueGetter: (params) => params?.data?.subIndustry?.sub_industry_name,
      cellRenderer: (props) => {
        const data: IIndustryMappingPreference = props.data;
        return data?.subIndustry ? data.subIndustry.sub_industry_name : "";
      },
      tooltipValueGetter: (props) => {
        const data: IIndustryMappingPreference = props.data;
        return data?.subIndustry ? data.subIndustry.sub_industry_name : "";
      },
    },
    {
      field: "year_of_experience",
      headerName: "Years Of Experience",
      width: 200,
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      suppressMovable: true,
      sortable: true,
      unSortIcon: true,
      valueGetter: (params) => params?.data?.year_of_experience,
      cellRenderer: (props) => {
        const data: IIndustryMappingPreference = props.data;
        return data?.industry?.year_of_experience ?? "";
      },
      tooltipValueGetter: (props) => {
        const data: IIndustryMappingPreference = props.data;
        return data?.industry?.year_of_experience ?? "";
      },
    },
    {
      field: "description",
      headerName: "Details",
      width: 400,
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      suppressMovable: true,
      sortable: true,
      unSortIcon: true,
      valueGetter: (params) => params?.data?.description,
      cellRenderer: (props) => {
        const data: IIndustryMappingPreference = props.data;
        return data?.industry?.description ?? "";
      },
      tooltipValueGetter: (props) => {
        const data: IIndustryMappingPreference = props.data;
        return data?.industry?.description ?? "";
      },
    },
    {
      field: "action",
      headerName: "Action",
      width: 100,
      flex: 1,
      suppressMovable: true,
      cellRenderer: ActionCellRenderer,
      suppressMenu: true,
    },
  ];
  const IsAddDisabled = () => {
    if (
      props.maxNumberOfPreference <=
      gridRef?.current?.api?.getDisplayedRowCount()
    ) {
      return true;
    }
    return false;
  };
  const submitForm = (event) => {
    if (!event.industry && !event.subIndustry) {
      return;
    }
    const similarRows = props.rowData.filter((row) => {
      const isIndustrySame =
        (row.industry === null && event.industry === null) ||
        (event.industry !== null
          ? row?.industry?.industry_id === event?.industry?.industry_id
          : false);
      const isSubIndustrySame =
        (row.subIndustry === null && event.subIndustry === null) ||
        (event.subIndustry !== null
          ? row?.subIndustry?.sub_industry_id ===
            event?.subIndustry?.sub_industry_id
          : false);
      return isIndustrySame && isSubIndustrySame;
    });
        
    if (similarRows && similarRows.length > 0) {
      snackbarContext.displaySnackbar(
        "The selected preference are submitted already, please modify selected preference",
        "error"
      );
      return;
    }
    //console.log(event);
    event["category"] = PreferenceCategories.INDUSTRY_MAPPING;
    event["employeeEmail"] = userContext.username;
    event["year_of_experience"] = event.year_of_experience || 0;
    event["description"] = event.description || "";
    const newPreference: IIndustryMappingPreference = {
      category: PreferenceCategories.INDUSTRY_MAPPING,
      employeeEmail: userContext.username,
      industry: event.industry,
      subIndustry: event.subIndustry,            
      isActive: true,
      preferenceOrder: props.rowData.length + 1
    };
    newPreference.industry.year_of_experience = event.year_of_experience || 0;
    newPreference.industry.description = event.description || "";
    props.setRowData([...props.rowData, newPreference]);
    reset();
    trigger();
    const industryMappingOptions: IIndustryMappingPreferenceOptions = {
      industryOptions: options.industryOptions,
      subIndustryOptions: [],
    };
    setOptions(industryMappingOptions);
  };

  return (
    <Typography
      component={"div"}
      p={3}
      mt={3}
      mb={3}
      sx={{
        borderRadius: "10px",
        background: "#f2f5ff",
      }}
    >
      <SectionHeader
        title="Industry"
        tooltip="Please enter the Industry or Industry and Sub-Industry preference to express your preferred industries for project allocations. "
      />
      <Grid container spacing={4}>
        <Grid item xs={5}>
          <form onSubmit={handleSubmit(submitForm)}>
            <div className="container">
              <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                control={control}
                name={industryMappingPreferenceName.industry}
                required={false}
                multiple={false}
                filterSelectedOptions={true}
                label={"Industry"}
                getOptionLabel={(option: IIndustryOption) => {
                  // console.log(option);
                  return option.industry_name;
                }}
                options={
                  options?.industryOptions ? options?.industryOptions : []
                }
                freeSolo={false}
                // value={props.selectedCompetency}
                onChange={(e) => {
                  //   console.log(e);
                  onIndustryChange(e);
                }}
              />
            </div>
            <div className="container">
              <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                control={control}
                name={industryMappingPreferenceName.subIndustry}
                required={false}
                multiple={false}
                filterSelectedOptions={true}
                label={"Sub-Industry"}
                getOptionLabel={(option: ISubIndustryOption) => {
                  // console.log(option);
                  return option.sub_industry_name;
                }}
                options={
                  options?.subIndustryOptions ? options?.subIndustryOptions : []
                }
                freeSolo={false}
                // value={props.selectedCompetency}
                onChange={(e) => {}}
              />
            </div>
            <div className="container">
              <ControllerNumberTextField
                name={industryMappingPreferenceName.year_of_experience}
                control={control}
                defaultValue={""}
                required={true}
                label={"Years Of Experience"}
                error={errors.totalEfforts}
                onChange={(e: any) => {}}
              />
            </div>
            <div className="container">
              <ControllerTextField
                control={control}
                name={industryMappingPreferenceName.description}
                required={false}
                multiline={true}
                label={"Details"}
                options={[]}
                error={errors.comments ? true : false}
                onChange={(e) => {
                  //
                }}
              />
            </div>
            <div className="container">
              <ActionButton
                label={"Add"}
                onClick={function (e: any): void {}}
                disabled={IsAddDisabled()}
                type={"submit"}
                textTransform="none"
              />
            </div>
          </form>
        </Grid>
        <Grid item xs={7}>
          <AgGridComponent
            gridComponentRef={gridRef}
            rowData={props.rowData}
            columnDefs={colDef}
            tooltipShowDelay={0}
            tooltipHideDelay={2000}
            isPageination={true}
            pageSize={18}
            suppressCsvExport={true}
            suppressContextMenu={true}
            suppressExcelExport={true}
            isFilterVisible={true}
            hideExport={true}
            suppressCellFocus={true}
            height={"350px"}
          ></AgGridComponent>
        </Grid>
      </Grid>
      <EditIndustryExperienceModal
        open={openEditModal}
        onClose={() => setOpenEditModal(false)}
        editingRow={editingRow}
        options={options}
        onSave={(updatedRow) => {
          const updatedList = props.rowData.map((row) =>
            row.id === updatedRow.id ? updatedRow : row
          );
          props.setRowData(updatedList);
        }}
        industryMaster={industryMaster}
      />
    </Typography>
  );
};
export default IndustryPreference;
