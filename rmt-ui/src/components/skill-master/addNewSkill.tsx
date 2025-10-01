/* eslint-disable react-hooks/exhaustive-deps */
import {
  Button,
  Grid,
  IconButton,
  Link,
  Tooltip,
  Typography,
} from "@mui/material";
import { useContext, useEffect, useRef, useState } from "react";
import { RequisitionHeaderSxProps } from "../create-requisition-main/constant";
import "./skill.css";
import { useForm } from "react-hook-form";
import ControllerAutoCompleteTextField from "../controllerInputs/controllerAutoCompleteTextField";
import ControllerTextField from "../controllerInputs/controllerTextField";
import AgGridComponent from "../aggrid-component/aggrid-component";
import DeleteIcon from "@mui/icons-material/Delete";
import * as constant from "../../components/activeRequisitionsDeatils/activerequisition/requisitiontable/constant";
import AddSkillMapping, {
  EAddNewSkillMappingForm,
  IAddNewSkillMappingForm,
} from "./addSkillMapping";
import {
  SnackbarContext,
  SnackbarContextProps,
} from "../../contexts/snackbarContext";
import { LoaderContext } from "../../contexts/loaderContext";
import {
  addNewSkill,
  getSkillByName,
  getSkillCategoryMaster,
  updateSkill,
} from "../../services/skills/skill.service";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import * as GlobalConstant from "../../global/constant";
import _ from "lodash";
import { IskillMapping } from "./Interface/IskillMapping";
import { Iskill } from "./Interface/Iskill";
import ConfirmationDialog from "../../common/confirmation-dialog/confirmation-dialog";
import InfoIcon from "@mui/icons-material/Info";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import useBlockRefreshAndBack from "../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import useBlockerCustom from "../../hooks/UnsavedChangesHook/useBlockerCustom";
import { ICompetencyMaster } from "../../common/interfaces/ICompetencyMaster";
import ActionButton from "../actionButton/actionButton";
import RateReviewOutlinedIcon from "@mui/icons-material/RateReviewOutlined";
import { RequisitionButtons } from "./utils";

enum EAddNewSkillMasterForm {
  SkillName = "skillName",
  SkillCategory = "skillCategory",
  SkillDescription = "skillDescription",
  Basic = "basic",
  Intermediate = "intermediate",
  Advanced = "advanced",
  Expert = "expert",
  Mapping = "mapping",
}
interface IAddNewSkillMasterForm {
  [EAddNewSkillMasterForm.SkillName]: string;
  [EAddNewSkillMasterForm.SkillCategory]: string;
  [EAddNewSkillMasterForm.SkillDescription]: string;
  [EAddNewSkillMasterForm.Basic]: string;
  [EAddNewSkillMasterForm.Intermediate]: string;
  [EAddNewSkillMasterForm.Advanced]: string;
  [EAddNewSkillMasterForm.Expert]: string;
  [EAddNewSkillMasterForm.Mapping]: IskillMapping[];
}

const AddNewSkill = (props: any) => {
  const {
    control,
    setValue,
    handleSubmit,
    getValues,
    setError,
    formState: { errors, isDirty },
  } = useForm<IAddNewSkillMasterForm>({ mode: "onTouched" });
  const gridRef: any = useRef();
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [skillCategoryOption, setSkillCategoryOption] = useState<Array<string>>(
    []
  );
  const [skillDataOpened, setSkillDataOpened] = useState<any>(null);
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const [rowData, setRowData] = useState<Array<IskillMapping>>([]);
  const [isMapEmpty, setIsMapEmpty] = useState<Boolean>(false);
  const [isSkillNameError, setIsSkillNameError] = useState<Boolean>(false);
  const [skillCode, setSkillCode] = useState<string>();
  const [isGridChange, setIsGridChange] = useState<boolean>(false);
  const [isConfirmationModal, setIsConfirmationModal] =
    useState<boolean>(false);
  const [formData, setFormData] = useState<Iskill>();
  const [isUpdateMode, setIsUpdateMode] = useState<boolean>(false);
  const [skillMappingToEdit, setSkillMappingToEdit] =
    useState<IAddNewSkillMappingForm | null>(null);
  const [defaultSkillDataMapping, setDefaultSkillDataMapping] = useState<any[]>(
    []
  );

  // Route Block
  useBlockRefreshAndBack(isDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isDirty);

  //----- Route Block -------//

  const handleDeleteMap = (data, rowNode) => {
    if (gridRef?.current?.api) {
      const res = gridRef?.current?.api?.applyTransaction({
        remove: [rowNode.data],
      });
      const currentRowData = [];
      gridRef.current.api.forEachNode((node) => currentRowData.push(node.data));
      setRowData(currentRowData);
      setIsMapEmpty(currentRowData.length === 0);
    }
  };

  const handleEditMapping = (rowNode) => {
    const nodeDataToEdit: IskillMapping = rowNode.data;
    const dataToSetForEdit: IAddNewSkillMappingForm = {
      [EAddNewSkillMappingForm.Competency]: nodeDataToEdit.competency,
      [EAddNewSkillMappingForm.Designation]: nodeDataToEdit.designation,
    };
    setSkillMappingToEdit(dataToSetForEdit);
    setIsModalOpen(true);
  };

  const SkillMappingActionRender = (event: any) => {
    return (
      <>
        <Tooltip title="Edit Skill">
          <IconButton
            disabled={props.isReadOnlyModeOn}
            onClick={(e) => {
              handleEditMapping(event.node);
            }}
          >
            <RateReviewOutlinedIcon
              fontSize="small"
              sx={constant.DeleteIconSxProps}
            />
          </IconButton>
        </Tooltip>
        <Tooltip title="Delete Skill">
          <IconButton
            disabled={props.isReadOnlyModeOn}
            onClick={(e) => {
              handleDeleteMap(event?.data, event.node);
            }}
          >
            <DeleteIcon fontSize="small" sx={constant.DeleteIconSxProps} />
          </IconButton>
        </Tooltip>
      </>
    );
  };

  const [colDefs, setColDefs] = useState<any[]>();
  useEffect(() => {
    setColDefs([
      {
        field: "competency",
        headerName: "Competency",
        flex: 1,
        filter: "agTextColumnFilter",
        sortable: true,
        valueGetter: (props) => {
          const competency: ICompetencyMaster = props.data.competency;
          return competency.competency;
        },
        tooltipValueGetter: (props) => {
          const competency: ICompetencyMaster = props.data.competency;
          return competency.competency;
        },
      },
      {
        field: "designation",
        headerName: "Designation",
        flex: 1,
        filter: "agTextColumnFilter",
        sortable: true,
      },
      {
        field: "action",
        headerName: "Action",
        flex: 0.6,
        cellRenderer: SkillMappingActionRender,
        suppressMenu: true,
      },
    ]);
  }, [props.isEditMode]);

  const handleModelClose = () => {
    setSkillMappingToEdit(null);
    setIsModalOpen(false);
  };

  const handleFormSubmit = async (e: IAddNewSkillMasterForm) => {
    if (
      e[EAddNewSkillMasterForm.Mapping] &&
      e[EAddNewSkillMasterForm.Mapping].length > 0
    ) {
      const data = {
        skillName: e[EAddNewSkillMasterForm.SkillName],
        skillCategory: e[EAddNewSkillMasterForm.SkillCategory],
        basic: e[EAddNewSkillMasterForm.Basic],
        intermediate: e[EAddNewSkillMasterForm.Intermediate],
        expert: e[EAddNewSkillMasterForm.Expert],
        advanced: e[EAddNewSkillMasterForm.Advanced],
        description: e[EAddNewSkillMasterForm.SkillDescription],
        createdBy: props?.userName,
        isEnable: true,
        mapping: rowData,
      };
      setFormData(data);
      setIsConfirmationModal(true);
    } else {
      setIsMapEmpty(true);
    }
  };

  const submitDataOnConfirm = () => {
    const isMapDataEmpty =
      gridRef?.current?.api.getDisplayedRowCount() > 0 ? false : true;
    setIsMapEmpty(isMapDataEmpty);
    if (!isMapDataEmpty) {
      submitSkill(formData);
    }
  };

  useEffect(() => {
    setValue(EAddNewSkillMasterForm.Mapping, rowData);
  }, [rowData]);

  const setSkillData = (data: any) => {
    setValue(EAddNewSkillMasterForm.SkillName, data?.skillName);
    setValue(EAddNewSkillMasterForm.SkillDescription, data?.description);
    setValue(EAddNewSkillMasterForm.Basic, data?.basic);
    setValue(EAddNewSkillMasterForm.Intermediate, data?.intermediate);
    setValue(EAddNewSkillMasterForm.Advanced, data?.advanced);
    setValue(EAddNewSkillMasterForm.Expert, data?.expert);
    setValue(EAddNewSkillMasterForm.SkillCategory, data?.skillCategory);
    const updatedSkillMapping = data?.skill_Mapping?.map((i) => {
      return {
        ...i,
        competency: {
          competencyId: i.competencyId,
          competency: i.competency,
        },
      };
    });
    setRowData(updatedSkillMapping);
    setDefaultSkillDataMapping(updatedSkillMapping);
  };

  const fetchSkill = async (skillName: string) => {
    loaderContext.open(true);
    Promise.all([getSkillByName(skillName)])
      .then((response: any) => {
        setSkillData(response[0]?.data);
        setSkillDataOpened(response[0]?.data);
        setSkillCode(response[0]?.data?.skillCode);
        loaderContext.open(false);
      })
      .catch(() => {
        snackbarContext.displaySnackbar("Something went Wrong.", "error");
        loaderContext.open(false);
      });
  };

  const checkIfMappingDataHasChanged = () => {
    let isChanged = false;
    if (rowData.length !== defaultSkillDataMapping.length) {
      isChanged = true;
    } else {
      const noItemAltered = defaultSkillDataMapping.every((defaultItem) =>
        rowData.some(
          (newItem) =>
            defaultItem?.competency &&
            newItem?.competency &&
            defaultItem?.competency?.competencyId ===
              newItem?.competency?.competencyId &&
            defaultItem?.designation.length === newItem?.designation.length &&
            defaultItem?.designation?.every((defaultDesignation) =>
              newItem?.designation.some(
                (newDesignation) => newDesignation === defaultDesignation
              )
            )
        )
      );
      isChanged = !noItemAltered;
    }
    setIsGridChange(isChanged);
  };

  useEffect(() => {
    checkIfMappingDataHasChanged();
  }, [rowData]);

  const fetchSkillCategoryMaster = () => {
    loaderContext.open(true);
    Promise.all([getSkillCategoryMaster()])
      .then((response: any) => {
        const category = response[0]?.data?.map((c) => c.categoryName);
        setSkillCategoryOption(category);
        loaderContext.open(false);
      })
      .catch(() => {
        snackbarContext.displaySnackbar("Something went Wrong.", "error");
        loaderContext.open(false);
      });
  };

  const submitSkill = (payload: Iskill) => {
    loaderContext.open(true);
    if (props?.isEditMode) {
      updateSkillData(payload);
    } else {
      Promise.all([addNewSkill(payload)])
        .then((response: any) => {
          if (response) {
            snackbarContext.displaySnackbar("Skill Created.", "success");
            props?.navigateGrid(true, "", true, false);
          }
          loaderContext.open(false);
        })
        .catch(() => {
          snackbarContext.displaySnackbar("Something went Wrong.", "error");
          loaderContext.open(false);
        });
    }
  };

  const updateSkillData = (payload: Iskill) => {
    loaderContext.open(true);
    payload.skillCode = skillCode;
    Promise.all([updateSkill(payload)])
      .then((response: any) => {
        if (response) {
          snackbarContext.displaySnackbar("Skill Updated.", "success");
          props?.navigateGrid(true, "", true, false);
        }
        loaderContext.open(false);
      })
      .catch(() => {
        snackbarContext.displaySnackbar("Something went Wrong.", "error");
        loaderContext.open(false);
      });
  };

  const handleMappingData = (data: IskillMapping) => {
    if (skillMappingToEdit && data) {
      const finalDataRow = rowData?.map((itemRow: IskillMapping) => {
        if (
          itemRow?.competency?.competencyId ===
            skillMappingToEdit[EAddNewSkillMappingForm.Competency]
              .competencyId &&
          itemRow?.designation?.every((designationItem) =>
            skillMappingToEdit[EAddNewSkillMappingForm.Designation]?.includes(
              designationItem
            )
          ) &&
          itemRow?.designation?.length ===
            skillMappingToEdit[EAddNewSkillMappingForm.Designation]?.length
        ) {
          return data;
        } else {
          return itemRow;
        }
      });

      setRowData(finalDataRow);
      setIsMapEmpty(finalDataRow.length === 0);
    } else {
      if (gridRef?.current?.api) {
        const res = gridRef?.current?.api?.applyTransaction({
          add: [data],
        });
      }

      const currentRowData = [];
      gridRef.current.api.forEachNode((node) => currentRowData.push(node.data));
      setRowData(currentRowData);
      gridRef?.current?.api.getDisplayedRowCount() > 0
        ? setIsMapEmpty(false)
        : setIsMapEmpty(true);
    }

    setIsModalOpen(false);
    setSkillMappingToEdit(null);
    setIsGridChange(true);
  };

  const isSkillPresent = () => {
    const skillName: string = getValues(EAddNewSkillMasterForm.SkillName);
    if (skillName.trim() !== "") {
      Promise.all([getSkillByName(skillName)])
        .then((response: any) => {
          if (response[0]?.data) {
            setError(EAddNewSkillMasterForm.SkillName, {
              type: "custom",
              message: "Skill already exists.",
            });
            setIsSkillNameError(true);
          } else {
            setIsSkillNameError(false);
          }
        })
        .catch(() => {
          snackbarContext.displaySnackbar("Something went Wrong.", "error");
        });
    }
  };

  useEffect(() => {
    if (props?.skillName && props?.skillName?.trim() !== "") {
      fetchSkill(props?.skillName);
      setIsUpdateMode(true);
    }
    fetchSkillCategoryMaster();
    gridRef?.current?.api?.refreshCells();

    return () => {
      setIsUpdateMode(false);
    };
  }, [props?.skillName]);

  return (
    <>
      {blocker.state === "blocked" && isDirty ? (
        <DialogBox
          showDialog={isDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <ConfirmationDialog
        title="Confirm!"
        content="Are you sure you want to proceed?"
        noBtnLabel="No"
        yesBtnLabel="Yes"
        open={isConfirmationModal}
        onConfirmationPopClose={() => {
          setIsConfirmationModal(false);
        }}
        handleYesClick={() => {
          submitDataOnConfirm();
        }}
      />
      {isModalOpen && (
        <AddSkillMapping
          open={isModalOpen}
          handleModelClose={handleModelClose}
          handleMappingData={handleMappingData}
          snackbarContext={snackbarContext}
          skillMappingToEdit={skillMappingToEdit}
        />
      )}
      <div className="skill-master-heading requisition-header-title">
        <Typography component={"span"} sx={RequisitionHeaderSxProps}>
          {isUpdateMode ? <>View-Update Skill</> : <>Add New Skill</>}
        </Typography>
      </div>
      <Grid container>
        <Grid item xs={12}>
          <form onSubmit={handleSubmit(handleFormSubmit)}>
            <Grid container spacing={2} sx={{ p: 2 }}>
              <Grid item xs={1}>
                <Button
                  className="backbtn"
                  sx={{
                    color:
                      GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
                  }}
                  startIcon={
                    <ArrowBackIosNewIcon
                      className="backarrow"
                      sx={{
                        color:
                          GlobalConstant.GT_DESIGN_PARAMETERS
                            .GtPrimaryColorPurple,
                      }}
                    />
                  }
                  onClick={() => {
                    props?.navigateGrid(true, "", true, false);
                  }}
                >
                  Back
                </Button>
              </Grid>
              <Grid item xs={3}>
                {(props.isReadOnlyModeOn || props?.isEditMode) && (
                  <Typography component="div">
                    <span className="fw-600">Skill Code: </span>
                    {skillCode}
                  </Typography>
                )}
              </Grid>
              {!props?.isReadOnlyModeOn && (isDirty || isGridChange) && (
                <>
                  <Grid item xs={7} />
                  <Grid item xs={1} sx={RequisitionButtons}>
                    <ActionButton
                      label={"Save"}
                      onClick={() => {
                        setIsMapEmpty(rowData.length === 0);
                      }}
                      disabled={false}
                      type={"submit"}
                    />
                  </Grid>
                </>
              )}
              {props?.isReadOnlyModeOn &&
                !props?.isEditMode &&
                skillDataOpened?.isEnable && (
                  <>
                    <Grid item xs={7} />
                    <Grid item xs={1} sx={RequisitionButtons}>
                      <ActionButton
                        type="submit"
                        onClick={() => {
                          props?.updateEditMode(true);
                        }}
                        disabled={false}
                        label={"Edit"}
                      ></ActionButton>
                    </Grid>
                  </>
                )}

              <Grid item xs={12}>
                <hr />
              </Grid>
              <Grid item xs={4}>
                <ControllerTextField
                  name={EAddNewSkillMasterForm.SkillName}
                  control={control}
                  freeSolo={false}
                  defaultValue={""}
                  readOnly={props.isReadOnlyModeOn}
                  disabled={props.isReadOnlyModeOn || props?.isEditMode}
                  error={errors[EAddNewSkillMasterForm.SkillName]}
                  onBlur={() => {
                    isSkillPresent();
                  }}
                  required={true}
                  label={"Skill Name "}
                  onChange={(e) => {}}
                  helperText={isSkillNameError ? "*Skill already exists." : " "}
                />
              </Grid>
              <Grid item xs={4}>
                <ControllerAutoCompleteTextField
                  name={EAddNewSkillMasterForm.SkillCategory}
                  control={control}
                  freeSolo={false}
                  defaultValue={""}
                  options={skillCategoryOption}
                  readOnly={props.isReadOnlyModeOn}
                  disabled={props.isReadOnlyModeOn || props?.isEditMode}
                  error={errors[EAddNewSkillMasterForm.SkillCategory]}
                  required={true}
                  label={"Skill Category"}
                  helperText={" "}
                  onChange={(e: any) => {}}
                />
              </Grid>
              <Grid item xs={4} />
              <Grid item xs={8}>
                <ControllerTextField
                  name={EAddNewSkillMasterForm.SkillDescription}
                  error={errors[EAddNewSkillMasterForm.SkillDescription]}
                  control={control}
                  required={true}
                  label={"Skill Description"}
                  defaultValue=""
                  onChange={(e) => {}}
                  readOnly={props.isReadOnlyModeOn}
                  disabled={props.isReadOnlyModeOn}
                />
              </Grid>
              <Grid item xs={12}>
                <div className="competency-heading-simple">
                  Competency Definition
                  <span>
                    <Tooltip
                      title={
                        "Define each of the skill competency levels for understanding of required employee skills at a  competency level."
                      }
                    >
                      <InfoIcon className={"infoIconStyle"} />
                    </Tooltip>
                  </span>
                </div>
              </Grid>

              <Grid item xs={11.95} sx={{ mt: -2, ml: -3, mb: 1 }}>
                <hr className="form-separator-hr" />
              </Grid>
              <Grid item xs={4}>
                <ControllerTextField
                  name={EAddNewSkillMasterForm.Basic}
                  control={control}
                  error={errors[EAddNewSkillMasterForm.Basic]}
                  required={true}
                  label={"Starting"}
                  defaultValue=""
                  onChange={(e) => {}}
                  helperText={" "}
                  readOnly={props.isReadOnlyModeOn}
                  disabled={props.isReadOnlyModeOn}
                />
              </Grid>
              <Grid item xs={4}>
                <ControllerTextField
                  name={EAddNewSkillMasterForm.Intermediate}
                  error={errors[EAddNewSkillMasterForm.Intermediate]}
                  control={control}
                  helperText={" "}
                  required={true}
                  label={"Building"}
                  defaultValue=""
                  onChange={(e) => {}}
                  readOnly={props.isReadOnlyModeOn}
                  disabled={props.isReadOnlyModeOn}
                />
              </Grid>
              <Grid item xs={4} />

              <Grid item xs={4}>
                <ControllerTextField
                  name={EAddNewSkillMasterForm.Advanced}
                  error={errors[EAddNewSkillMasterForm.Advanced]}
                  control={control}
                  required={true}
                  label={"Skilled"}
                  defaultValue=""
                  onChange={(e) => {}}
                  readOnly={props.isReadOnlyModeOn}
                  disabled={props.isReadOnlyModeOn}
                />
              </Grid>
              <Grid item xs={4}>
                <ControllerTextField
                  name={EAddNewSkillMasterForm.Expert}
                  error={errors[EAddNewSkillMasterForm.Expert]}
                  control={control}
                  required={true}
                  label={"Excelled"}
                  defaultValue=""
                  onChange={(e) => {}}
                  readOnly={props.isReadOnlyModeOn}
                  disabled={props.isReadOnlyModeOn}
                />
              </Grid>
              <Grid item xs={4} />
            </Grid>
          </form>
        </Grid>
        <Grid item xs={12} sx={{ mt: -1.6 }}>
          <Grid container spacing={2} sx={{ p: 2 }}>
            <Grid item xs={12}>
              <div className="competency-heading-simple">
                Designation Mapping
              </div>
            </Grid>
            <Grid item xs={11.95} sx={{ mt: -2, ml: -3, mb: 1 }}>
              <hr className="form-separator-hr" />
            </Grid>
            {!props.isReadOnlyModeOn && (
              <Grid item xs={6}>
                <span className="skill-map-error">
                  {isMapEmpty ? "*Please map at-least one designation. " : ""}
                </span>
              </Grid>
            )}
            {!props.isReadOnlyModeOn && (
              <Grid item xs={6} sx={RequisitionButtons}>
                <span className="add-skill-link">
                  <Link
                    href="#"
                    onClick={() => {
                      setIsModalOpen(true);
                    }}
                  >
                    Add Tag(+)
                  </Link>
                </span>
              </Grid>
            )}
            <Grid item xs={12}>
              <AgGridComponent
                gridComponentRef={gridRef}
                rowData={rowData}
                columnDefs={colDefs}
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
        </Grid>
      </Grid>
    </>
  );
};

export default AddNewSkill;
