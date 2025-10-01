import { useEffect, useState, useRef } from "react";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import { useForm } from "react-hook-form";
import {
  IConfigurationMainBreakupMetaValues,
  EKeySelectorsForMinBreakup,
  IConfigurationMaster,
  EKeySelectorSeparator,
} from "../../../../../common/interfaces/IConfigurationMaster";
import AgGridComponent from "../../../../aggrid-component/aggrid-component";
import { getAgGridFilterType, GridColumnDefsControl } from "../utils";
import { IconButton, Tooltip, Typography } from "@mui/material";
import RateReviewOutlinedIcon from "@mui/icons-material/RateReviewOutlined";
import DeleteOutlinedIcon from "@mui/icons-material/DeleteOutlined";
import { ConfigGridActionIcons } from "../../../style";
import CustomCellEditor from "./custom-cell-editor";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import ConfirmationDialog from "../../../../../common/confirmation-dialog/confirmation-dialog";

interface ISelectedBuGridRendererProps {
  selectedBusinessUnit: string;
  configurationGroupItem: IConfigurationMaster;
  addUpdateConfigBreakupMaster: (
    configurationMasterId: string,
    keySelector: string[],
    configurationMetaValues: IConfigurationMainBreakupMetaValues[],
    deleteMetaValues: boolean
  ) => Promise<boolean>;
  closeTheAccordion: () => void;
}
const SelectedBuGridRenderer = (props: ISelectedBuGridRendererProps) => {
  const [gridRowData, setGridRowData] = useState<any[]>([]);
  const [columnDefs, setColumnDefs] = useState<any[]>([]);
  const configGridRef = useRef();
  const [currentEditableRow, setCurrentEditableRow] = useState<string>("");
  const [openConfirmationForDelete, setOpenConfirmationForDelete] = useState<
    any | null
  >(null);

  useEffect(() => {
    getColumnDefsForConfig();
  }, [currentEditableRow]);

  useEffect(() => {
    getRowDataForConfig();
  }, [props.selectedBusinessUnit]);

  const getRowDataForConfig = () => {
    const valuesToRender =
      props.configurationGroupItem.configurationMainBreakups
        .filter(
          (item) =>
            item.keySelector !== EKeySelectorsForMinBreakup.DEFAULT &&
            item.keySelector.split(EKeySelectorSeparator).length > 1 &&
            item.keySelector.split(EKeySelectorSeparator)[0]?.toLowerCase() ===
              props.selectedBusinessUnit.toLowerCase()
        )
        .map((item) => {
          let finalObj = item;
          finalObj[GridColumnDefsControl.Offering] = item.keySelector.split(
            EKeySelectorSeparator
          )[1];
          item.configurationMainBreakupMetaValues.forEach((metaItem) => {
            finalObj[metaItem.key] = metaItem.value;
          });
          return finalObj;
        });
    setGridRowData(valuesToRender);
  };

  const getColumnDefsForConfig = () => {
    const finalColumnDefs = [];
    finalColumnDefs.push(defaultColumnDefs[0]);
    props.configurationGroupItem.schemaValues.forEach((schemaItem) => {
      finalColumnDefs.push({
        headerName: schemaItem.keyDisplay,
        field: schemaItem.key,
        flex: 1,
        editable: (editableParams) =>
          editableParams.node.id === currentEditableRow,
        cellEditor: CustomCellEditor,
        cellEditorParams: {
          configGridRef,
          schemaItem,
        },
        cellDataType: false,
        filter: getAgGridFilterType(schemaItem.controlType),
        sortable: true,
        unSortIcon: true,
        tooltipField: schemaItem.key,
        headerTooltip: schemaItem.keyDisplay,
      });
    });
    finalColumnDefs.push(defaultColumnDefs[1]);
    setColumnDefs(finalColumnDefs);
  };

  const onCellValueChanged = (params) => {
    if (!params.newValue) {
      alert("Invalid value. Please enter a valid number.");
      params.node.setDataValue(params.colDef.field, params.oldValue);
    }
  };

  const submitUpdatedConfig = async (finalData: any, deleteMeta: boolean) => {
    const checkIfSomeValuesHasChanged = true;
    if (checkIfSomeValuesHasChanged) {
      const updatedConfigurationMetaValues: IConfigurationMainBreakupMetaValues[] =
        props.configurationGroupItem.schemaValues.map((schemaItem) => {
          return {
            key: schemaItem.key,
            displayKey: schemaItem.keyDisplay,
            value: finalData[schemaItem.key] ?? "",
          };
        });
      const response = await props.addUpdateConfigBreakupMaster(
        props.configurationGroupItem.id,
        [finalData.keySelector],
        updatedConfigurationMetaValues,
        deleteMeta
      );
      if (response) {
        setCurrentEditableRow("");
      }
    } else {
      props.closeTheAccordion();
    }
  };

  const editDeleteActionCellRender = (cellRendererProps: any) => {
    return (
      <>
        {cellRendererProps.node.id === currentEditableRow ? (
          <>
            <Typography component={"span"}>
              <IconButton
                className="edit-icon"
                disabled={false}
                onClick={(e) => {
                  submitUpdatedConfig(cellRendererProps.node.data, false);
                }}
              >
                <Tooltip title="Save" placement="bottom">
                  <CheckCircleIcon
                    fontSize="small"
                    sx={ConfigGridActionIcons}
                  />
                </Tooltip>
              </IconButton>
            </Typography>
          </>
        ) : (
          <>
            <Typography component={"span"}>
              <IconButton
                className="edit-icon"
                disabled={
                  currentEditableRow &&
                  cellRendererProps.node.id !== currentEditableRow
                    ? true
                    : false
                }
                onClick={(e) => {
                  setCurrentEditableRow(cellRendererProps.node.id);
                }}
              >
                <Tooltip title="Edit" placement="bottom">
                  <RateReviewOutlinedIcon
                    fontSize="small"
                    sx={ConfigGridActionIcons}
                  />
                </Tooltip>
              </IconButton>
            </Typography>
            <Typography component={"span"}>
              <IconButton
                disabled={
                  currentEditableRow &&
                  cellRendererProps.node.id !== currentEditableRow
                    ? true
                    : false
                }
                onClick={() => {
                  setCurrentEditableRow(cellRendererProps.node.id);
                  setOpenConfirmationForDelete(cellRendererProps.node.data);
                }}
              >
                <Tooltip title="Delete" placement="bottom">
                  <DeleteOutlinedIcon
                    fontSize="small"
                    sx={ConfigGridActionIcons}
                  />
                </Tooltip>
              </IconButton>
            </Typography>
          </>
        )}
      </>
    );
  };

  const defaultColumnDefs = [
    {
      headerName: "Offering",
      field: GridColumnDefsControl.Offering,
      flex: 1,
      filter: "agTextColumnFilter",
      sortable: true,
      unSortIcon: true,
      tooltipField: GridColumnDefsControl.Offering,
      order: 1,
      headerTooltip: "Offering",
    },
    {
      headerName: "Action",
      field: "action",
      flex: 1,
      filter: false,
      sortable: false,
      unSortIcon: false,
      suppressMenu: true,
      cellRenderer: editDeleteActionCellRender,
      cellRendererParams: {},
      order: 100,
    },
  ];

  return (
    <>
      {openConfirmationForDelete && (
        <ConfirmationDialog
          title={"Delete Configuration"}
          content={"Are you sure you want to delete this configuration?"}
          noBtnLabel="Cancel"
          yesBtnLabel="Confirm"
          open={openConfirmationForDelete ? true : false}
          handleYesClick={(e) => {
            submitUpdatedConfig(openConfirmationForDelete, true);
            setOpenConfirmationForDelete(null);
            setCurrentEditableRow("");
          }}
          onConfirmationPopClose={(e) => {
            setOpenConfirmationForDelete(null);
            setCurrentEditableRow("");
          }}
        />
      )}
      <Box sx={{ p: 2, backgroundColor: "white" }}>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <AgGridComponent
              columnDefs={columnDefs}
              gridComponentRef={configGridRef}
              rowData={gridRowData}
              isPageination={true}
              pageSize={10}
              height={"30vh"}
              tooltipShowDelay={0}
              tooltipHideDelay={2000}
              cellValueChanged={onCellValueChanged}
            />
          </Grid>
        </Grid>
      </Box>
    </>
  );
};
export default SelectedBuGridRenderer;
