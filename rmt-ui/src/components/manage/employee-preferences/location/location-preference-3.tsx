import { useContext, useEffect, useRef, useState } from "react";
import {
  ILocationOption,
  ILocationPreference,
  ILocationPreferenceList,
  ILocationPreferenceOptions,
  ILocationPreferenceProps,
} from "./interface";
import DeleteIcon from "@mui/icons-material/Delete";
import "./style.css";
import { getLocationMasterFromWCGT } from "../../../../services/wcgt-master-services/wcgt-master-services";
import { IWCGTLocationMasterList } from "../../../../common/interfaces/IWCGTLocationMaster";
import { PreferenceCategories } from "../../constant";
import AgGridComponent from "../../../aggrid-component/aggrid-component";
import ActionButton from "../../../actionButton/actionButton";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import { useForm } from "react-hook-form";
import { locationPreferenceName } from "./constant";
import { Grid, IconButton, Tooltip, Typography } from "@mui/material";
import { DeleteIconSxProps } from "../../../activeRequisitionsDeatils/activerequisition/requisitiontable/constant";
import { UserDetailsContext } from "../../../../contexts/userDetailsContext";
import SectionHeader from "../../section-header/section-header";
import { SnackbarContext } from "../../../../contexts/snackbarContext";

const LocationPreference = (props: ILocationPreferenceProps) => {
  const [locationMaster, setLocationMaster] = useState<IWCGTLocationMasterList>(
    []
  );
  const snackbarContext: any = useContext(SnackbarContext);
  const [options, setOptions] = useState<ILocationPreferenceOptions>(null);
  const gridRef: any = useRef();
  const userContext = useContext(UserDetailsContext);

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
      location: null,
    },
  });
  const getLocationMaster = async () => {
    try {
      return await getLocationMasterFromWCGT();
    } catch (e) {
      throw e;
    }
  };
  useEffect(() => {
    Promise.all([getLocationMaster()]).then((response) => {
      const locationMaster: IWCGTLocationMasterList = response[0];
      setLocationMaster(locationMaster);
      setOptions({
        locationOptions: locationMaster,
      });
      //   const defaultOptions = GetDefaultValues(buMaster);
      //   setOptions(defaultOptions);
    });
  }, []);

  useEffect(() => {
    if (props.employeePreference && props.employeePreference.length > 0) {
      const preferences: ILocationPreferenceList = props.employeePreference
        .filter(
          (e) =>
            e.category.toLowerCase().trim() ===
              PreferenceCategories.LOCATION.toLowerCase().trim() &&
            e.isActive === true
        )
        .map((e) => {
          return {
            id: e.id,
            employeeEmail: e.employeeEmail,
            preferenceOrder: e.preferenceOrder,
            isActive: e.isActive,
            category: e.category,
            location: e.preferenceDetails.location,
          };
        });
      console.log(preferences);
      props.setRowData(preferences);
    }
  }, [props.employeePreference]);

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

  const DeleteCellRender = (props: any) => (
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
  );

  const colDef = [
    {
      field: "location",
      headerName: "Location",
      width: 400,
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      suppressMovable: true,
      sortable: true,
      unSortIcon: true,
      valueGetter: (params) => params.data.location?.location_name || "",
      cellRenderer: (props) => {
        const data: ILocationPreference = props.data;
        return data?.location ? data.location.location_name : "";
      },
    },
    {
      field: "action",
      flex: 1,
      headerName: "Action",
      width: 400,
      suppressMovable: true,
      cellRenderer: DeleteCellRender,
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
  // useEffect(() => {
  //   IsAddDisabled();
  // }, [props.maxNumberOfPreference]);
  const submitForm = (event) => {
    if (!event.location) {
      return;
    }
    const similarRows = props.rowData.filter((row) => {
      const isLocationSame =
        (row.location === null && event.location === null) ||
        (event.location !== null
          ? row?.location?.location_id === event?.location?.location_id
          : false);
      return isLocationSame;
    });
    console.log(similarRows);
    if (similarRows && similarRows.length > 0) {
      snackbarContext.displaySnackbar(
        "The selected preference are submitted already, please modify selected preference",
        "error"
      );
      return;
    }
    //console.log(event);
    event["category"] = PreferenceCategories.LOCATION;
    event["employeeEmail"] = userContext.username;
    props.setRowData([...props.rowData, event]);
    reset();
    trigger();
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
        title="Location"
        tooltip="Please enter the locations to express your preference of work locations for allocations"
      />
      <Grid container spacing={4}>
        <Grid item xs={5}>
          <form onSubmit={handleSubmit(submitForm)}>
            <div className="container">
              <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                control={control}
                name={locationPreferenceName.location}
                required={false}
                multiple={false}
                filterSelectedOptions={true}
                label={"Location"}
                getOptionLabel={(option: ILocationOption) => {
                  // console.log(option);
                  return option.location_name;
                }}
                options={
                  options?.locationOptions ? options?.locationOptions : []
                }
                freeSolo={false}
                // value={props.selectedCompetency}
                onChange={(e) => {
                  //   console.log(e);
                  // onBuChange(e);
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
    </Typography>
  );
};

export default LocationPreference;
