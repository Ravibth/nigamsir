import React, { useContext, useEffect, useRef, useState } from "react";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import { useForm } from "react-hook-form";
import {
  IBuOfferingsMaster,
  IBuOfferingsMasterList,
} from "../../../../common/interfaces/IBuOfferingsMaster";
import { getAllBuOfferings } from "../../../../services/wcgt-master-services/wcgt-master-services";
import {
  IBuMappingPreference,
  IBuMappingPreferenceList,
  IBuMappingPreferenceOptions,
  IBuMappingPreferenceProps,
  IBuOption,
  IBuOptionList,
  IOfferingOption,
  IOfferingOptionList,
  ISolutionOption,
  ISolutionOptionList,
} from "./interface";
import { buMappingPreferenceName } from "./constant";
import DeleteIcon from "@mui/icons-material/Delete";
import _ from "lodash";
import ActionButton from "../../../actionButton/actionButton";
import AgGridComponent from "../../../aggrid-component/aggrid-component";
import { Grid, IconButton, Tooltip, Typography } from "@mui/material";
import { DeleteIconSxProps } from "../../../activeRequisitionsDeatils/activerequisition/requisitiontable/constant";
import { PreferenceCategories } from "../../constant";
import { UserDetailsContext } from "../../../../contexts/userDetailsContext";
import "./style.css";
import SectionHeader from "../../section-header/section-header";
import { SnackbarContext } from "../../../../contexts/snackbarContext";
const BuMappingPreference = (props: IBuMappingPreferenceProps) => {
  const [buTreeMappingMaster, setBuTreeMappingMaster] =
    useState<IBuOfferingsMasterList>([]);
  const [options, setOptions] = useState<IBuMappingPreferenceOptions>(null);
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
      businessUnit: null,
      offering: null,
      solution: null,
    },
  });
  const getBuTeeMappingMaster = async () => {
    try {
      return await getAllBuOfferings();
    } catch (e) {
      throw e;
    }
  };
  useEffect(() => {
    Promise.all([getBuTeeMappingMaster()]).then((response) => {
      const buMaster: IBuOfferingsMasterList = response[0];
      setBuTreeMappingMaster(buMaster);
      const defaultOptions = GetDefaultValues(buMaster);
      console.log(defaultOptions);
      setOptions(defaultOptions);
    });
  }, []);

  useEffect(() => {
    if (props.employeePreference && props.employeePreference.length > 0) {
      const preferences: IBuMappingPreferenceList = props.employeePreference
        .filter(
          (e) =>
            e.category.toLowerCase().trim() ===
              PreferenceCategories.BU_TREE_MAPPING.toLowerCase().trim() &&
            e.isActive === true
        )
        .map((e) => {
          return {
            id: e.id,
            employeeEmail: e.employeeEmail,
            preferenceOrder: e.preferenceOrder,
            isActive: e.isActive,
            category: e.category,
            businessUnit: e.preferenceDetails.businessUnit,
            offering: e.preferenceDetails.offering,
            solution: e.preferenceDetails.solution,
          };
        });
      props.setRowData(preferences);
    }
  }, [props.employeePreference]);

  const GetDefaultValues = (buMaster: IBuOfferingsMasterList) => {
    console.log(buMaster);
    const buOption: IBuOptionList = buMaster.map((e) => {
      return {
        buId: e.bu_id,
        buMid: e.bu_mid,
        buName: e.bu,
      };
    });
    const uniqBuOptions: IBuOptionList = _.uniqWith(
      buOption,
      (a, b) => a.buId === b.buId
    ).filter((x) => x && x.buId && x.buName);
    const defaultOptions: IBuMappingPreferenceOptions = {
      buOptions: uniqBuOptions,
      offeringOptions: [],
      solutionOptions: [],
    };
    return defaultOptions;
  };
  const IsAddDisabled = () => {
    if (
      props.maxNumberOfPreference <=
      gridRef?.current?.api?.getDisplayedRowCount()
    ) {
      return true;
    }
    return false;
  };

  const onBuChange = (selectedBu: IBuOption) => {
    if (selectedBu === null) {
      reset({
        businessUnit: null,
        offering: null,
        solution: null,
      });
      setOptions((prev) => {
        return {
          buOptions: prev.buOptions,
          offeringOptions: [],
          solutionOptions: [],
        };
      });
      trigger();
      return;
    }
    const selectedBUs = buTreeMappingMaster.filter((item) => {
      return selectedBu.buId === item.bu_id;
    });
    const offeringOptions: IOfferingOptionList = selectedBUs.map((item) => {
      return {
        offeringId: item.offering_id,
        offeringName: item.offering,
        offeringMid: item.offering_mid,
      };
    });
    const uniqOfferingOptions: IOfferingOptionList = _.uniqWith(
      offeringOptions,
      (a, b) => a.offeringId === b.offeringId
    ).filter((x) => x && x.offeringId && x.offeringName);
    setOptions((prev) => {
      return {
        buOptions: prev.buOptions,
        offeringOptions: uniqOfferingOptions,
        solutionOptions: prev.solutionOptions,
      };
    });
    setValue(buMappingPreferenceName.offering, null);
    setValue(buMappingPreferenceName.solution, null);
  };
  const onOfferingChange = (selectedOffering: IOfferingOption) => {
    if (selectedOffering === null) {
      reset({
        businessUnit: getValues(buMappingPreferenceName.businessUnit),
        offering: null,
        solution: null,
      });
      setOptions((prev) => {
        return {
          buOptions: prev.buOptions,
          offeringOptions: prev.offeringOptions,
          solutionOptions: [],
        };
      });
      trigger();
      return;
    }
    const selectedOfferings = buTreeMappingMaster.filter((item) => {
      return selectedOffering.offeringId === item.offering_id;
    });
    const solutionOptions: ISolutionOptionList = selectedOfferings.map(
      (item) => {
        return {
          solutionId: item.solution_id,
          solutionName: item.solution,
          solutionMid: item.solution_mid,
        };
      }
    );
    const uniqSolutionOptions: ISolutionOptionList = _.uniqWith(
      solutionOptions,
      (a, b) => a.solutionId === b.solutionId
    ).filter((x) => x && x.solutionId && x.solutionName);
    setOptions((prev) => {
      return {
        buOptions: prev.buOptions,
        offeringOptions: prev.offeringOptions,
        solutionOptions: uniqSolutionOptions,
      };
    });
    setValue(buMappingPreferenceName.solution, null);
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
      field: "businessUnit",
      headerName: "Business Unit",
      width: 400,
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      suppressMovable: true,
      sortable: true,
      unSortIcon: true,
      valueGetter: (params) => params?.data?.businessUnit?.buName,
      tooltipValueGetter: (props) => {
        const data: IBuMappingPreference = props.data;
        return data?.businessUnit ? data.businessUnit.buName : "";
      },
      cellRenderer: (props) => {
        const data: IBuMappingPreference = props.data;
        return data?.businessUnit ? data.businessUnit.buName : "";
      },
    },
    {
      field: "offering",
      headerName: "Offering",
      width: 600,
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      suppressMovable: true,
      sortable: true,
      unSortIcon: true,
      valueGetter: (params) => params?.data?.offering?.offeringName,
      tooltipValueGetter: (props) => {
        const data: IBuMappingPreference = props.data;
        return data?.offering ? data.offering.offeringName : "";
      },
      cellRenderer: (props) => {
        const data: IBuMappingPreference = props.data;
        return data?.offering ? data.offering.offeringName : "";
      },
    },
    {
      field: "solution",
      headerName: "Solution",
      width: 600,
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      suppressMovable: true,
      sortable: true,
      unSortIcon: true,
      valueGetter: (params) => params?.data?.solution?.solutionName,
      tooltipValueGetter: (props) => {
        const data: IBuMappingPreference = props.data;
        return data?.solution ? data.solution.solutionName : "";
      },
      cellRenderer: (props) => {
        const data: IBuMappingPreference = props.data;
        return data?.solution ? data.solution.solutionName : "";
      },
    },
    {
      field: "action",
      flex: 1,
      suppressMovable: true,
      headerName: "Action",
      width: 400,
      cellRenderer: DeleteCellRender,
      suppressMenu: true,
    },
  ];

  const submitForm = (event) => {
    //console.log(event);
    if (!event.businessUnit && !event.offerings && !event.solution) {
      return;
    }
    const similarRows = props.rowData.filter((row) => {
      const isBuSame =
        (row.businessUnit === null && event.businessUnit === null) ||
        (event.businessUnit !== null
          ? row?.businessUnit?.buId === event?.businessUnit?.buId
          : false);
      const isOfferingSame =
        (row.offering === null && event.offering === null) ||
        (event.offering !== null
          ? row?.offering?.offeringId === event?.offering?.offeringId
          : false);
      const isSolutionSame =
        (row.solution === null && event.solution === null) ||
        (event.solution !== null
          ? row?.solution?.solutionId === event?.solution?.solutionId
          : false);
      return isBuSame && isOfferingSame && isSolutionSame;
    });
    console.log(similarRows);
    if (similarRows && similarRows.length > 0) {
      snackbarContext.displaySnackbar(
        "The selected preference are submitted already, please modify selected preference",
        "error"
      );
      return;
    }
    event["category"] = PreferenceCategories.BU_TREE_MAPPING;
    event["employeeEmail"] = userContext.username;
    props.setRowData([...props.rowData, event]);
    reset({
      businessUnit: null,
      offering: null,
      solution: null,
    });
    trigger();
    const buMappingOptions: IBuMappingPreferenceOptions = {
      buOptions: options.buOptions,
      offeringOptions: [],
      solutionOptions: [],
    };
    setOptions(buMappingOptions);
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
        title="Offering & Solution"
        tooltip="Please enter the BU>Offering or BU>Offering> Solution preference to express your preferred project types for allocations."
      />
      <Grid container spacing={4}>
        <Grid item xs={5}>
          <div>
            <form onSubmit={handleSubmit(submitForm)}>
              <div className="container">
                <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                  control={control}
                  name={buMappingPreferenceName.businessUnit}
                  required={false}
                  multiple={false}
                  filterSelectedOptions={true}
                  label={"Business Unit"}
                  getOptionLabel={(option: IBuOption) => {
                    // console.log(option);
                    return option.buName;
                  }}
                  options={options?.buOptions ? options?.buOptions : []}
                  freeSolo={false}
                  // value={props.selectedCompetency}
                  onChange={(e) => {
                    //   console.log(e);
                    onBuChange(e);
                  }}
                />
              </div>
              <div className="container">
                <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                  control={control}
                  name={buMappingPreferenceName.offering}
                  required={false}
                  multiple={false}
                  filterSelectedOptions={true}
                  label={"Offering"}
                  getOptionLabel={(option: IOfferingOption) => {
                    // console.log(option);
                    return option.offeringName;
                  }}
                  options={
                    options?.offeringOptions ? options?.offeringOptions : []
                  }
                  freeSolo={false}
                  // value={props.selectedCompetency}
                  onChange={(e) => {
                    onOfferingChange(e);
                    //   props.updateSkillsFilterOptions();
                  }}
                />
              </div>
              <div className="container">
                <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                  control={control}
                  name={buMappingPreferenceName.solution}
                  required={false}
                  multiple={false}
                  filterSelectedOptions={true}
                  label={"Solution"}
                  getOptionLabel={(option: ISolutionOption) => {
                    // console.log(option);
                    return option.solutionName;
                  }}
                  options={
                    options?.solutionOptions ? options?.solutionOptions : []
                  }
                  freeSolo={false}
                  // value={props.selectedCompetency}
                  onChange={(e) => {
                    //   props.updateSkillsFilterOptions();
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
          </div>
        </Grid>
        <Grid item xs={7}>
          <div>
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
          </div>
        </Grid>
      </Grid>
    </Typography>
  );
};

export default BuMappingPreference;
