import * as React from "react";
// import Box from "@mui/material/Box";
// import Button from "@mui/material/Button";
// import Typography from "@mui/material/Typography";
// import Modal from "@mui/material/Modal";
// import { Grid } from "@mui/material";
// import { useState, useEffect, useContext } from "react";
// import ShowSkill from "./showskills/showskills";
// import Allocationdate from "./allocationdates/allocationdate";
// import Radio from "@mui/material/Radio";
// import RadioGroup from "@mui/material/RadioGroup";
// import FormControlLabel from "@mui/material/FormControlLabel";
// import FormControl from "@mui/material/FormControl";
// import * as constant from "./constant";
// import { useForm } from "react-hook-form";
// import { Controller } from "react-hook-form";
// import ControllerNumberTextField from "../controllerInputs/controllerNumbeTextfield";
// import AddCircleOutlinedIcon from "@mui/icons-material/AddCircleOutlined";
// import * as GlobalConstant from "../../global/constant";
// import ControllerTextField from "../controllerInputs/controllerTextField";
// import { IAllocation } from "../update-allocation/entity/IAllocations";
// import "./style.css";
// import { format } from "react-string-format";
// import * as GC from "../../global/constant";
// import * as util from "../update-allocation/util";
// import { LoaderContext } from "../../contexts/loaderContext";
// import ConfirmationDialog from "../../common/confirmation-dialog/confirmation-dialog";
// import "./../../App.css";
// import { AllocateActionButton } from "./allocate-action-button/allocateactionbutton";

// const style = {
//   position: "absolute" as "absolute",
//   top: "50%",
//   left: "50%",
//   transform: "translate(-50%, -50%)",
//   width: "50%",
//   height: "auto",
//   bgcolor: "background.paper",
//   border: "1px solid #000",
//   padding: "30px",
// };

// enum ConfirmationDialogType {
//   ALLOCATE = "ALLOCATE",
//   DRAFT = "DRAFT",
// }

function AllocateEmployeeForm(props: any) {
  // const {
  //   isModelOpen,
  //   closeModel,
  //   resourceAllocationData,
  //   description,
  //   emailIds,
  //   requisitions,
  //   isReqTotalEffortValidation,
  //   employeeInfo,
  //   requisitionId,
  //   isRequisitionDetailShow,
  //   employeesName,
  //   resourceAllocation,
  // } = props;
  // const loaderContext: any = useContext(LoaderContext);
  // const [employeeAllocation, setEmployeeAllocation] = useState([] as any);
  // const [entries, setEntries] = useState([] as any);
  // const [totalEfforts, setTotalEfforts] = useState(0);
  // const [isEnableAllocateButton, setIsEnableAllocateButton] = useState(false);
  // const [isContinuousAllocation, setIsContinuousAllocation] = useState(true);
  // const [allocationData, setAllocationData] = useState({} as any);
  // const [isWorkflowRunning, setIsWorkflowRunning] = useState(false);
  // const [isViewMode, setIsViewMode] = useState(false);
  // const [isRequisitionDateExpire, setIsRequisitionDateExpire] = useState(false);
  // const {
  //   control,
  //   formState: { errors },
  //   handleSubmit,
  //   getValues,
  //   setValue,
  // } = useForm();

  // const [selectedIndex, setSelectedIndex] = useState(-1);
  // const [isPerDayHourAllocation, setIsPerDayHourAllocation] = useState(false);
  // const [isConfirContiAllocationPop, setIsConfirContiAllocationPop] =
  //   useState(false);
  // const [isConfirmationPopOpen, setIsConfirmationPopOpen] = useState(false);
  // const [
  //   isConfirmationPopOpenForUpdateAllocation,
  //   setIsConfirmationPopOpenForUpdateAllocation,
  // ] = useState<string>("");

  // useEffect(() => {
  //   setAllocationData(resourceAllocationData);
  //   setIsContinuousAllocation(resourceAllocationData?.isContinuousAllocation);
  //   isWorkflowRunningForGuid(resourceAllocationData?.guid).then(
  //     (flag: boolean) => {
  //       setIsViewMode(flag);
  //       if (requisitions && new Date(requisitions.endDate) < new Date()) {
  //         setIsViewMode(true);
  //         setIsRequisitionDateExpire(true);
  //       }
  //     }
  //   );
  // }, [resourceAllocationData]);

  // const isWorkflowRunningForGuid = async (guid: string) => {
  //   try {
  //     if (resourceAllocationData?.guid) {
  //       const _isWorkflowRunning: boolean = await util.isWorkflowRunning(
  //         resourceAllocationData?.guid
  //       );
  //       setIsWorkflowRunning(_isWorkflowRunning);
  //       return _isWorkflowRunning;
  //     } else {
  //       return false;
  //     }
  //   } catch (ex) {
  //     // console.log(ex);
  //   }
  // };

  // useEffect(() => {
  //   setValue("description", description);
  // }, [allocationData, props.description]);

  // useEffect(() => {
  //   setEntries(resourceAllocation);
  // }, [resourceAllocation]);

  // useEffect(() => {
  //   const _entries = entries.filter((a: IAllocation) => a.isactive);
  //   if (_entries.length > 0) {
  //     setEmployeeAllocation(isContinuousAllocation ? [_entries[0]] : _entries);
  //   }
  //   const _totalEffort = util.getTotalEffort(_entries, {});
  //   setValue("totalEfforts", _totalEffort);
  //   setValue("entries", entries);
  //   setValue("isContinuousAllocation", isContinuousAllocation);
  // }, [entries]);

  // useEffect(() => {
  //   const _entries = entries.filter((a: IAllocation) => a.isactive);
  //   if (_entries.length > 0) {
  //     setEmployeeAllocation(isContinuousAllocation ? [_entries[0]] : _entries);
  //   }
  // }, [isContinuousAllocation]);

  // const onAllocationClick = (data: any) => {
  //   props.onAllocationClick(data);
  // };

  // const saveAsDraft = (data: any) => {
  //   // console.log("save as draft", data);
  // };

  // const effortValidate = () => {
  //   if (isReqTotalEffortValidation)
  //     return parseInt(requisitions?.totalHours) >= totalEfforts;
  //   else return true;
  // };

  // /*********************** Common method ********************/
  // const handleEffortChange = async (index: number, value: any) => {
  //   let updatedEntries = [...entries];
  //   loaderContext.open(true);
  //   updatedEntries[index].confirmedPerDayHours = value;
  //   const _totalEffort = util.getTotalEffort(
  //     updatedEntries,
  //     updatedEntries[index]
  //   );
  //   const validateDate = await util.isVaildDateAllocation(
  //     isContinuousAllocation,
  //     allocationData,
  //     updatedEntries,
  //     index,
  //     emailIds
  //   );
  //   updatedEntries = validateDate.allocaions;
  //   if (
  //     updatedEntries[index].isPerDayHourAllocation &&
  //     validateDate.totalWorkingDays
  //   ) {
  //     updatedEntries[index].totalWorkingDays = validateDate.totalWorkingDays;
  //     setTotalEfforts(
  //       _totalEffort +
  //         parseInt(updatedEntries[index].confirmedPerDayHours) *
  //           validateDate.totalWorkingDays
  //     );
  //   } else setTotalEfforts(_totalEffort);
  //   setIsEnableAllocateButton(validateDate.isVaild);
  //   updateTotalEfforts(updatedEntries);
  //   setEntries([]);
  //   setEntries(updatedEntries);
  //   loaderContext.open(false);
  // };
  // const updateTotalEfforts = (updatedEntries: any) => {
  //   const _totalEffortHours = util.getTotalEffort(updatedEntries, {});
  //   setValue("totalEfforts", _totalEffortHours);
  //   setTotalEfforts(_totalEffortHours);
  // };
  // const handleStartDateChange = async (index: number, date: any) => {
  //   let updatedEntries = [...entries];
  //   loaderContext.open(true);
  //   updatedEntries[index].confirmedAllocationStartDate = date;
  //   const validateDate = await util.isVaildDateAllocation(
  //     isContinuousAllocation,
  //     allocationData,
  //     updatedEntries,
  //     index,
  //     emailIds
  //   );
  //   updatedEntries = validateDate.allocaions;
  //   setEntries([]);
  //   setEntries(updatedEntries);
  //   setIsEnableAllocateButton(validateDate.isVaild);
  //   updateTotalEfforts(updatedEntries);
  //   loaderContext.open(false);
  // };

  // const handleEndDateChange = async (index: number, date: any) => {
  //   let updatedEntries = [...entries];
  //   loaderContext.open(true);
  //   updatedEntries[index].confirmedAllocationEndDate = date;
  //   const validateDate = await util.isVaildDateAllocation(
  //     isContinuousAllocation,
  //     allocationData,
  //     updatedEntries,
  //     index,
  //     emailIds
  //   );
  //   updatedEntries = validateDate.allocaions;
  //   setEntries([]);
  //   setEntries(updatedEntries);
  //   setIsEnableAllocateButton(validateDate.isVaild);
  //   updateTotalEfforts(updatedEntries);

  //   loaderContext.open(false);
  // };
  // // ****************** Delete Allocation ****************************/
  // const handleDeleteEntry = (index: number, data: any) => {
  //   setSelectedIndex(data.index);
  //   setIsConfirmationPopOpen(true);
  // };
  // const deleteAllocation = () => {
  //   const updatedEntries = [...entries];
  //   updatedEntries[selectedIndex].isactive = false;
  //   loaderContext.open(true);
  //   setEntries([]);
  //   setEntries(updatedEntries);
  //   setIsConfirmationPopOpen(false);
  //   setSelectedIndex(-1);
  //   setIsEnableAllocateButton(util.isEnableAllocateBtn());
  //   updateTotalEfforts(updatedEntries);
  //   loaderContext.open(false);
  // };

  // //****************** Delete Allocation ****************************/
  // //****************** Allocation *************************************/
  // const handleContinuousAllocationChange = (event: any) => {
  //   if (entries.length > 1) {
  //     setIsConfirContiAllocationPop(true);
  //   } else {
  //     onChangeContinuousAllocation();
  //   }
  // };
  // const onChangeContinuousAllocation = () => {
  //   let newAllocationData = allocationData;
  //   newAllocationData.isContinuousAllocation =
  //     !newAllocationData.isContinuousAllocation;
  //   if (entries.length > 0) {
  //     entries
  //       .filter((a: IAllocation) => a.isactive)
  //       .map((element: IAllocation, index: number) => {
  //         if (index === 0) {
  //           setTotalEfforts(
  //             element.confirmedPerDayHours ? element.confirmedPerDayHours : 0
  //           );
  //         } else {
  //           element.isactive = false;
  //         }
  //       });
  //   } else {
  //     setEntries([constant.initialEntry]);
  //   }
  //   // setValue("isContinuousAllocation", !getValues("isContinuousAllocation"));
  //   setIsContinuousAllocation(newAllocationData.isContinuousAllocation);
  //   setAllocationData(newAllocationData);
  //   setIsConfirContiAllocationPop(false);
  // };
  // const handleChangeHoursPerDay = (index: number, data: any) => {
  //   const updatedEntries = [...entries];
  //   // updatedEntries[data.index].confirmedPerDayHours = 0;
  //   updatedEntries[data.index].isPerDayHourAllocation =
  //     !updatedEntries[data.index].isPerDayHourAllocation;
  //   setEntries([]);
  //   setEntries(updatedEntries);
  //   setIsPerDayHourAllocation(!isPerDayHourAllocation);
  //   // setTotalEfforts(util.getTotalEffort(updatedEntries, {}));
  //   updateTotalEfforts(updatedEntries);
  // };
  // /*********************** Add New entry  */
  // const handleAddEntry = () => {
  //   const emptyEntries = JSON.parse(JSON.stringify(constant.initialEntry));
  //   if (entries.length > 0)
  //     emptyEntries.index = entries[entries.length - 1].index + 1;
  //   setEntries([...entries, emptyEntries]);
  // };

  // function isEnableSubmitBtn(): boolean {
  //   return (
  //     !isEnableAllocateButton ||
  //     isWorkflowRunning ||
  //     requisitions?.totalHours < totalEfforts ||
  //     new Date(resourceAllocationData?.requisitions?.endDate) <= new Date()
  //   );
  // }

  // const openAndCheckForConfirmationToSubmit = (data) => {
  //   setIsConfirmationPopOpenForUpdateAllocation(
  //     ConfirmationDialogType.ALLOCATE
  //   );
  // };

  // return (
  //   <>
  //     <Modal
  //       open={isModelOpen}
  //       onClose={(event, reason) => {
  //         if (reason === "backdropClick") {
  //           return;
  //         }
  //         closeModel();
  //       }}
  //     >
  //       <form onSubmit={handleSubmit(openAndCheckForConfirmationToSubmit)}>
  //         <Box sx={style}>
  //           <Grid container spacing={3}>
  //             <Grid
  //               item
  //               xs={12}
  //               sx={{
  //                 // paddingBottom: "30px",
  //                 fontSize: "20px",
  //                 fontStyle: "bold",
  //               }}
  //             >
  //               Allocate{" "}
  //               {employeesName?.length > 0
  //                 ? " - " + employeesName?.join(", ")
  //                 : "Employee"}
  //             </Grid>

  //             <Grid item xs={12}>
  //               <ul className="form-error-container">
  //                 {isWorkflowRunning && (
  //                   <li className="error_msg">
  //                     {GlobalConstant.message.error.workflow_running_msg}
  //                   </li>
  //                 )}
  //                 {isRequisitionDateExpire && (
  //                   <li className="error_msg">
  //                     {GlobalConstant.message.error.requisition_date_expire_msg}
  //                   </li>
  //                 )}
  //               </ul>
  //             </Grid>

  //             {props?.showDescription === false ? (
  //               <></>
  //             ) : (
  //               <Grid
  //                 item
  //                 xs={12}
  //                 // sx={{ : "23px" }}
  //               >
  //                 <ControllerTextField
  //                   name="description"
  //                   control={control}
  //                   defaultValue={""}
  //                   required={true}
  //                   label={"Description"}
  //                   error={errors.description}
  //                   value={description}
  //                   multiline={"multiline"}
  //                   fullWidth={"fullWidth"}
  //                   onChange={(e: any) => {}}
  //                   disabled={isViewMode}
  //                 />
  //               </Grid>
  //             )}
  //             <Grid
  //               item
  //               xs={3}
  //               // sx={{ paddingLeft: "23px" }}
  //             >
  //               <Typography>Continuous Allocation</Typography>
  //               <Controller
  //                 name="allocationType"
  //                 control={control}
  //                 defaultValue="Yes"
  //                 render={({ field: any }) => (
  //                   <FormControl>
  //                     <RadioGroup
  //                       row
  //                       aria-labelledby="demo-row-radio-buttons-group-label"
  //                       name="row-radio-buttons-group"
  //                       onChange={(e) => handleContinuousAllocationChange(e)}
  //                       value={isContinuousAllocation ? "Yes" : "No"}
  //                     >
  //                       <FormControlLabel
  //                         value="Yes"
  //                         disabled={isViewMode}
  //                         control={<Radio />}
  //                         label="Yes"
  //                       />
  //                       <FormControlLabel
  //                         value="No"
  //                         disabled={isViewMode}
  //                         control={<Radio />}
  //                         label="No"
  //                       />
  //                     </RadioGroup>
  //                   </FormControl>
  //                 )}
  //               />
  //             </Grid>
  //             <Grid item xs={3}>
  //               <ControllerNumberTextField
  //                 name="totalEfforts"
  //                 sx={constant.Textbox}
  //                 control={control}
  //                 defaultValue={""}
  //                 required={true}
  //                 label={"Total Efforts"}
  //                 error={errors.totalEfforts}
  //                 disabled={true}
  //                 max={requisitions?.totalHours}
  //                 validate={() => effortValidate()}
  //                 onChange={(e: any) => {}}
  //                 value={totalEfforts}
  //               />
  //               {parseInt(requisitions?.totalHours) < totalEfforts && (
  //                 <span className="error_msg">
  //                   {format(
  //                     GC.ERROR_MSG.total_effort_error_msg,
  //                     requisitions?.totalHours
  //                   )}
  //                   {/* Total Effort should not be greater than{" "}
  //                   {requisitions?.totalHours} hours. */}
  //                 </span>
  //               )}
  //             </Grid>
  //             {props?.isSkillShow && (
  //               <Grid
  //                 item
  //                 xs={9}
  //                 sm={9}
  //                 md={9}
  //                 lg={9}
  //                 xl={9}
  //                 // sx={{ padding: "17px" }}
  //               >
  //                 <ShowSkill
  //                   control={control}
  //                   errors={errors}
  //                   skills={props.skills}
  //                   getSkills={props.getSkills}
  //                   disabled={isViewMode}
  //                 />
  //                 {/* <ShowSkill
  //                   control={control}
  //                   errors={errors}
  //                 />
  //                 <ShowSkill control={control} errors={errors} /> */}
  //               </Grid>
  //             )}

  //             {employeeAllocation.length > 0 &&
  //               employeeAllocation.map((entry: IAllocation) => (
  //                 <Grid item xs={12} key={entry.id}>
  //                   <Allocationdate
  //                     isDeleteBtnEnabale={employeeAllocation.length > 1}
  //                     requisitions={requisitions}
  //                     isPerDayHourAllocation={isPerDayHourAllocation}
  //                     isContinuousAllocation={isContinuousAllocation}
  //                     index={entry?.index}
  //                     control={control}
  //                     entry={entry}
  //                     errors={errors}
  //                     entries={entries}
  //                     isViewMode={isViewMode}
  //                     handleChangeHoursPerDay={(index: number, date: any) => {
  //                       handleChangeHoursPerDay(
  //                         entry?.index ? entry.index : 0,
  //                         entry
  //                       );
  //                     }}
  //                     handleStartDateChange={(index: number, date: any) =>
  //                       handleStartDateChange(
  //                         entry?.index ? entry.index : 0,
  //                         date
  //                       )
  //                     }
  //                     handleEndDateChange={(index: number, date: any) =>
  //                       handleEndDateChange(index, date)
  //                     }
  //                     handleEffortChange={(index: any, value: any) =>
  //                       handleEffortChange(
  //                         entry?.index ? entry.index : 0,
  //                         value
  //                       )
  //                     }
  //                     handleDeleteEntry={(index: number) => {
  //                       handleDeleteEntry(
  //                         entry?.index ? entry.index : 0,
  //                         entry
  //                       );
  //                     }}
  //                     effortValidate={() => {
  //                       return getValues("effortsPerDay") > 0 &&
  //                         getValues("effortsPerDay") < 9
  //                         ? true
  //                         : false;
  //                     }}
  //                   />
  //                 </Grid>
  //               ))}
  //             <Grid item xs={12}>
  //               {!isViewMode &&
  //                 !isContinuousAllocation &&
  //                 GlobalConstant.APP_CONFIG.add_more_limit >
  //                   entries.filter((a: IAllocation) => a.isactive).length && (
  //                   <Typography component="div">
  //                     <Button
  //                       variant="text"
  //                       disabled={isViewMode}
  //                       className="btn labelColor"
  //                       onClick={() => handleAddEntry()}
  //                     >
  //                       <AddCircleOutlinedIcon
  //                         fontSize="small"
  //                         sx={GlobalConstant.MenuIconSxProps}
  //                       />{" "}
  //                       Add more
  //                     </Button>
  //                   </Typography>
  //                 )}
  //             </Grid>
  //           </Grid>
  //           <AllocateActionButton
  //             isEnableSubmitBtn={props.isEnableSubmitBtn || isEnableSubmitBtn()}
  //             closeModel={(e) => closeModel(e)}
  //             resourceAllocationData={props?.resourceAllocationData}
  //             saveAsDraft={() => {
  //               setIsConfirmationPopOpenForUpdateAllocation(
  //                 ConfirmationDialogType.DRAFT
  //               );
  //             }}
  //             onSubmitBtnClick={(e) => {
  //               openAndCheckForConfirmationToSubmit(getValues());
  //             }}
  //           ></AllocateActionButton>
  //         </Box>
  //       </form>
  //     </Modal>
  //     <div>
  //       <ConfirmationDialog
  //         title="Allocation Delete"
  //         content="Do you want to delete this allocation?"
  //         noBtnLabel="No"
  //         yesBtnLabel="Yes"
  //         open={isConfirmationPopOpen}
  //         onConfirmationPopClose={() => {
  //           setIsConfirmationPopOpen(false);
  //         }}
  //         handleYesClick={() => {
  //           deleteAllocation();
  //         }}
  //       ></ConfirmationDialog>

  //       <ConfirmationDialog
  //         title="Continuous Allocation"
  //         content="Your data will be lose. Are your sure want to change continuous allocation?"
  //         noBtnLabel="No"
  //         yesBtnLabel="Yes"
  //         open={isConfirContiAllocationPop}
  //         onConfirmationPopClose={() => {
  //           setIsConfirContiAllocationPop(false);
  //         }}
  //         handleYesClick={() => {
  //           onChangeContinuousAllocation();
  //         }}
  //       ></ConfirmationDialog>

  //       <ConfirmationDialog
  //         title="Update Allocation"
  //         content="Do you want to update this allocation?"
  //         noBtnLabel="No"
  //         yesBtnLabel="Yes"
  //         open={isConfirmationPopOpenForUpdateAllocation}
  //         onConfirmationPopClose={() => {
  //           setIsConfirmationPopOpenForUpdateAllocation("");
  //         }}
  //         handleYesClick={() => {
  //           if (
  //             ConfirmationDialogType.DRAFT ===
  //             isConfirmationPopOpenForUpdateAllocation
  //           ) {
  //             saveAsDraft(getValues());
  //           } else if (
  //             ConfirmationDialogType.ALLOCATE ===
  //             isConfirmationPopOpenForUpdateAllocation
  //           ) {
  //             onAllocationClick(getValues());
  //           }
  //         }}
  //       ></ConfirmationDialog>
  //     </div>
  //   </>
  // );
  return <></>;
}
export default React.memo(AllocateEmployeeForm);
