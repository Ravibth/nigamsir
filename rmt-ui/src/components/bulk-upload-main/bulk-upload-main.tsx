import React, { useContext, useEffect, useRef, useState } from "react";
import Upload from "../../common/images/upload.png";
import {
  FormControl,
  FormControlLabel,
  Radio,
  RadioGroup,
  Tooltip,
} from "@mui/material";
import RefreshIcon from "@mui/icons-material/Refresh";
import "./bulk-upload-main.css";
import ActionButton from "../actionButton/actionButton";
import ProgressBarDisplay from "../../common/progress-bar-display/progress-bar-display";
import * as constant from "./bulk-upload-constant";
import UploadAgGrid from "./upload-ag-grid";
import XLSX from "xlsx";
import { SnackbarContext } from "../../contexts/snackbarContext";
import DownloadTemplate from "./download-template";
import BackButton from "../backbutton/backbutton";
import {
  BulkUploadGridHeader,
  TypeOfBulkUpload,
  allocationHandleValidation,
  preAllocationHandleValidation,
  requisitionHandleValidation,
} from "./bulk-upload-constant";
import ConfirmationDialog from "../../common/confirmation-dialog/confirmation-dialog";
import {
  BulkUploadRequisition,
  UploadWcgtValidation,
} from "../../services/requisition/requisition";
import { maxPriorityValue } from "../../global/constant";
import BackUploadButton from "./back-btn";
import { AgGridReact } from "ag-grid-react";
import BulkUploadTitle from "./bulk-upload-title";
import BulkAllocationCommonScreen from "./bulk-allocation-common-screen/bulk-allocation-common-screen";
import { LoaderContext } from "../../contexts/loaderContext";
import { useNavigate } from "react-router-dom";
import {
  IsProjectInActiveOrClosed,
  routeValueEncode,
} from "../../global/utils";
import { TabsTitleEnum } from "../requestor-view/constant";
import useBlockRefreshAndBack from "../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import useBlockerCustom from "../../hooks/UnsavedChangesHook/useBlockerCustom";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import BulkUploadSubTitle from "./bulk-upload-sub-title";
import { GetNewDateWithNoonTimeZone } from "../../utils/date/dateHelper";

const BulkUploadMain = (props: any) => {
  const [dragActive, setDragActive] = useState<boolean>(false);
  const [fileSelected, setFileSelected]: any = useState();
  const [errors, setErrors]: any = useState<any[]>([]);
  const hiddenFileInput: any = React.useRef(null);
  const [gridRowData, setGridRowData]: any = useState([]);
  const snackbarContext: any = useContext(SnackbarContext);
  const [progressValue, setProgressValue] = useState(0);
  const [showBrowseComponent, setShowBrowseComponent] = useState<boolean>(true);
  const [agGridDisplay, setAGgridDisplay] = useState<boolean>(false);
  const { projectDetails } = props;
  const [openModal, setOpenModal] = useState(false);
  const [dataAfterValidation, setDataAfterValidation] = useState<any>([]);
  const gridComponentRef = useRef<AgGridReact | null>(null);
  const [selectedRadioValue, setSelectedRadioValue] = useState("requisition");
  const [openCommonAllocationScreen, setOpenCommonAllocationScreen] =
    useState<boolean>(false);
  const [finalBulkAllocationData, setFinalBulkAllocationData] = useState<any>(
    []
  );
  const loaderContext: any = useContext(LoaderContext);
  const compareHeaders = (
    fileHeaders: string[],
    predefinedHeaders: string[]
  ) => {
    if (
      fileHeaders.length !==
      predefinedHeaders.length - constant.defaultHeaders.length
    ) {
      return false;
    }

    const unmatchedHeaders = predefinedHeaders.filter((header) => {
      return (
        fileHeaders.find(
          (x) => x?.trim()?.toLowerCase() === header?.trim()?.toLowerCase()
        ) === undefined
      );
    });

    if (unmatchedHeaders.length > constant.defaultHeaders.length) {
      // console.log(unmatchedHeaders);
    }
    return unmatchedHeaders.length === constant.defaultHeaders.length;
  };

  const reloadPage = () => {
    setDragActive(false);
    setFileSelected();
    setGridRowData([]);
  };

  //drag and drop
  const handleDrag = function (e: any) {
    e.preventDefault();
    e.stopPropagation();
    if (e.type === "dragenter" || e.type === "dragover") {
      setDragActive(true);
    } else if (e.type === "dragleave") {
      setDragActive(false);
    }
  };

  const fileSelect = () => {
    reloadPage();
    hiddenFileInput.current.click();
  };

  const handleDrop = function (e: any) {
    e.preventDefault();
    e.stopPropagation();
    setDragActive(false);
    if (e.dataTransfer.files && e.dataTransfer.files[0]) {
      const file = e.dataTransfer.files[0];
      if (
        file.type !==
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
      ) {
        snackbarContext.displaySnackbar(
          "Please upload an Excel format file",
          "error"
        );
        return;
      }
      setFileSelected(file.name);
      const totalSize = file.size;
      let loadedBytes = 0;
      const reader = new FileReader();
      reader.onprogress = (e) => {
        if (e.lengthComputable) {
          loadedBytes = e.loaded;
          const progress = (loadedBytes / totalSize) * 100;
          setProgressValue(progress);
        }
      };
      reader.onload = (e) => {
        const bstr = e.target?.result as string;
        const workBook = XLSX.read(bstr, { type: "binary", cellDates: true });
        const workSheetName = workBook.SheetNames[0];
        const workSheet = workBook.Sheets[workSheetName];
        const fileData = XLSX.utils.sheet_to_json(workSheet, {
          header: 1,
          // raw: false,
          dateNF: "DD/MM/YYYY",
        });
        importDataToGrid(fileData);
        setProgressValue(100);
      };
      reader.readAsBinaryString(file);
      e.target.value = "";
    }
  };

  // file upload
  const gridApi = useRef<any>(null);
  const gridColumnApi = useRef<any>(null);
  const onGridReady = (params: any) => {
    gridApi.current = params.api;
    gridColumnApi.current = params.columnApi.autoSizeAllColumns();
  };

  const onRowDataChanged = (event: any) => {
    const columnIds: string[] = [];
    event.columnApi
      .getAllColumns()
      .forEach((col: any) => columnIds.push(col.colId));
    event.columnApi.autoSizeColumns(columnIds, false);
  };

  const validationCheck = async () => {
    try {
      loaderContext.open(true);
      const payloadData = getPayloadData();
      const response = await UploadWcgtValidation(payloadData);
      loaderContext.open(false);
      return response;
    } catch (error) {
      snackbarContext.displaySnackbar(
        "Validation Check Failed! Please try again",
        "error",
        6000
      );
      loaderContext.open(false);
    }
  };

  const handleFileChange = (event: any) => {
    const fileList = event.target.files;
    if (!fileList || fileList.length === 0) {
      snackbarContext.displaySnackbar("Please select a file", "error");
      return;
    }
    const file = fileList[0];
    const totalSize = file.size;
    let loadedBytes = 0;
    if (
      file.type !==
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    ) {
      snackbarContext.displaySnackbar(
        "Please upload an Excel format file",
        "error"
      );
      return;
    }
    // validationCheck();
    setFileSelected(file.name);
    const reader = new FileReader();
    reader.onprogress = (e) => {
      if (e.lengthComputable) {
        loadedBytes = e.loaded;
        const progress = (loadedBytes / totalSize) * 100;
        setProgressValue(progress);
      }
    };
    reader.onload = (e) => {
      const bstr = e.target?.result as string;
      const workBook = XLSX.read(bstr, { type: "binary", cellDates: true });
      const workSheetName = workBook.SheetNames[0];
      const workSheet = workBook.Sheets[workSheetName];
      const fileData = XLSX.utils.sheet_to_json(workSheet, {
        header: 1,
        // raw: false,
        dateNF: "DD/MM/YYYY",
      });
      importDataToGrid(fileData);
      setProgressValue(100);
    };
    reader.readAsBinaryString(file);
    event.target.value = "";
    setIsDirty(true);
  };

  const importDataToGrid = (fileData: any[]) => {
    const headers = fileData[0];
    let columnDefs = [];

    if (selectedRadioValue === "requisition") {
      columnDefs = constant.requisitionColumnDefs;
    } else {
      columnDefs = constant.allocationColumnDefs;
    }
    const predefinedHeaders = columnDefs.map(
      (column: any) => column.headerName
    );

    if (compareHeaders(headers, predefinedHeaders)) {
      // Headers match, populate the Ag-Grid
      const columnDefs = getColumnDefs(headers);
      const gridData = fileData.slice(3);
      const rowsToAdd = populateGridData(gridData, predefinedHeaders, headers);
      const rowSToAddedFinal = rowsToAdd.map((i) => {
        const emailId = i[BulkUploadGridHeader.EmailId];
        const empCode = i[BulkUploadGridHeader.EmpCode];
        const newEmailId = emailId ? `${empCode}__${emailId}` : null;

        return {
          ...i,
          "Email ID": newEmailId,
        };
      });
      setGridRowData(rowSToAddedFinal);
      setShowBrowseComponent(false);
      setAGgridDisplay(true);
    } else {
      snackbarContext.displaySnackbar(
        "Header mismatch! Please upload a valid file.",
        "error"
      );
    }
  };

  useEffect(() => {
    if (gridRowData && gridRowData.length > 0) {
      let checkValidation = async () => {
        try {
          const res: any = await validationCheck();
          setDataAfterValidation(res?.data);
        } catch (e) {
          throw e;
        }
      };
      checkValidation();
    }
  }, [gridRowData]);

  const navigate = useNavigate();
  const navigateToRequisitionsTab = () => {
    navigate(
      `/project-details/${routeValueEncode(
        projectDetails.pipelineCode
      )}/${routeValueEncode(projectDetails.jobCode)}?tab=${
        TabsTitleEnum.Requisitions
      }`
    );
  };

  const GetAllErrorsFormARow = (
    impObj: any,
    headers: any[],
    handleValidation,
    preHandleValidation?: Function
  ) => {
    let errors: any[] = [];
    headers.forEach((header) => {
      if (preHandleValidation) {
        let preAllocationErrors = preHandleValidation(header, impObj);
        if (preAllocationErrors) {
          errors = [];
          errors.push(
            "Invalid data uploaded. Pls refer to sample data for uploading correct data for Requisition/ Allocations"
          );
          return;
        }
      }
      handleValidation(header, impObj, errors);
    });
    return errors;
  };

  const getColumnDefs = (headers: string[]) => {
    return headers.map((header, index) => ({
      headerName: header,
      field: header,
      editable: index < 10,
    }));
  };
  const populateGridData = (
    gridData: any[],
    preDefinedHeaders: any[],
    headers: any[]
  ) => {
    return gridData.map((row) => {
      let impObj: any = {};
      let errorsPerRow;
      headers.forEach((header, idx) => {
        impObj[header] = row[idx];
      });
      if (selectedRadioValue === TypeOfBulkUpload.REQUISITION) {
        errorsPerRow = GetAllErrorsFormARow(
          impObj,
          headers,
          requisitionHandleValidation
        );
      } else if (selectedRadioValue === TypeOfBulkUpload.ALLOCATION) {
        errorsPerRow = GetAllErrorsFormARow(
          impObj,
          headers,
          allocationHandleValidation,
          preAllocationHandleValidation
        );
      }
      const anotherImpObj = {
        ...impObj,
      };

      if (errorsPerRow.length > 0) {
        let errStringArray: string[] = [];
        errorsPerRow.forEach((e, i) => {
          errStringArray.push(`${e}`);
        });

        anotherImpObj["Comments"] = errStringArray;
        anotherImpObj["Status"] = "Error";
      } else {
        anotherImpObj["Comments"] = [];
        anotherImpObj["Status"] = "Success";
      }

      if (anotherImpObj[BulkUploadGridHeader.StartDate]) {
        const startDate = new Date(
          anotherImpObj[BulkUploadGridHeader.StartDate]
        );
        startDate.setDate(startDate.getDate() + 1);
        anotherImpObj[BulkUploadGridHeader.StartDate] = startDate;
      }

      if (anotherImpObj[BulkUploadGridHeader.EndDate]) {
        const endDate = new Date(anotherImpObj[BulkUploadGridHeader.EndDate]);
        endDate.setDate(endDate.getDate() + 1);
        anotherImpObj[BulkUploadGridHeader.EndDate] = endDate;
      }
      return anotherImpObj;
    });
  };

  const getPayloadData = () => {
    const payloadData = gridRowData?.map((i: any, index: number) => {
      return {
        pipelineCode: projectDetails?.pipelineCode,
        jobCode: projectDetails?.jobCode,
        clientName: projectDetails?.clientName,
        projectName: projectDetails?.projectName,
        projectStartDate: projectDetails?.startDate,
        projectEndDate: projectDetails?.endDate,
        requisitionDescription: i[BulkUploadGridHeader.JobDescription],
        designationId: i[BulkUploadGridHeader.DesignationCode]?.toString(),
        competencyId: i[BulkUploadGridHeader.CompetencyId],
        competencyWeightage: i[BulkUploadGridHeader.CompetencyWeightage],
        offeringsWeightage: i[BulkUploadGridHeader.OfferingsWeightage],
        solutionsWeightage: i[BulkUploadGridHeader.SolutionsWeightage],
        designation:
          dataAfterValidation?.length > 0 &&
          dataAfterValidation[index]["designation"]
            ? dataAfterValidation[index]["designation"]
            : "",
        startDate: i[BulkUploadGridHeader.StartDate]
          ? GetNewDateWithNoonTimeZone(i[BulkUploadGridHeader.StartDate])
          : null,
        endDate: i[BulkUploadGridHeader.EndDate]
          ? GetNewDateWithNoonTimeZone(i[BulkUploadGridHeader.EndDate])
          : null,
        emailId:
          dataAfterValidation?.length > 0 &&
          dataAfterValidation[index]["emailId"]
            ? dataAfterValidation[index]["emailId"]
            : i[BulkUploadGridHeader.EmailId],

        empCode:
          dataAfterValidation?.length > 0 &&
          dataAfterValidation[index]["empCode"]
            ? dataAfterValidation[index]["empCode"]
            : i[BulkUploadGridHeader.EmpCode],
        locationCode: i[BulkUploadGridHeader.LocationCode],
        BusinessUnit: projectDetails?.bu,
        subIndustryWeight: i[BulkUploadGridHeader.SubIndustryWeightage],
        industryWeight: i[BulkUploadGridHeader.IndustryWeightage],
        subIndustryID: i[BulkUploadGridHeader.SubIndustryId]?.toString(),
        industryID: i[BulkUploadGridHeader.IndustryId]?.toString(),
        subIndustry:
          dataAfterValidation?.length > 0 &&
          dataAfterValidation[index]["subIndustry"]
            ? dataAfterValidation[index]["subIndustry"]
            : "",
        industry:
          dataAfterValidation?.length > 0 &&
          dataAfterValidation[index]["industry"]
            ? dataAfterValidation[index]["industry"]
            : "",
        sameClientExperienceWeightage:
          i[BulkUploadGridHeader.ClientExperienceWeightage],
        locations:
          dataAfterValidation?.length > 0 &&
          dataAfterValidation[index]["locations"]
            ? dataAfterValidation[index]["locations"]
            : "",
        locationWeightage: i[BulkUploadGridHeader.LocationWeightage],
        skillWeightage: i[BulkUploadGridHeader.SkillWeightage],
        selectedOption: selectedRadioValue,
        parameters: [
          {
            name: "offerings",
            priority:
              i?.parameters?.Value === maxPriorityValue
                ? "Must Have"
                : "Good To Have",
            value: i[BulkUploadGridHeader.OfferingsWeightage],
          },
          {
            name: "solutions",
            priority:
              i?.parameters?.Value === maxPriorityValue
                ? "Must Have"
                : "Good To Have",
            value: i[BulkUploadGridHeader.SolutionsWeightage],
          },
          {
            name: "competency",
            priority:
              i?.parameters?.Value === maxPriorityValue
                ? "Must Have"
                : "Good To Have",
            value: i[BulkUploadGridHeader.CompetencyWeightage],
          },
          {
            name: "Industry",
            priority:
              i?.parameters?.Value === maxPriorityValue
                ? "Must Have"
                : "Good To Have",
            value: i[BulkUploadGridHeader.IndustryWeightage],
          },
          {
            name: "Sub_Industry",
            priority:
              i?.parameters?.Value === maxPriorityValue
                ? "Must Have"
                : "Good To Have",
            value: i[BulkUploadGridHeader.SubIndustryWeightage],
          },
          {
            name: "Location",
            priority:
              i?.parameters?.Value === maxPriorityValue
                ? "Must Have"
                : "Good To Have",
            value: i[BulkUploadGridHeader.LocationWeightage],
          },
          {
            name: "Same_client",
            priority:
              i?.parameters?.Value === maxPriorityValue
                ? "Must Have"
                : "Good To Have",
            value: i[BulkUploadGridHeader.ClientExperienceWeightage],
          },
          {
            name: "Skills",
            priority:
              i?.parameters?.Value === maxPriorityValue
                ? "Must Have"
                : "Good To Have",
            value: i[BulkUploadGridHeader.SkillWeightage],
          },
        ],
        numberOfResources: i[BulkUploadGridHeader.NoOfResources],
        numberOfHours: i[BulkUploadGridHeader.NoOfHours],
        perDay: i[BulkUploadGridHeader.PerDay],
        skills:
          dataAfterValidation?.length > 0 &&
          dataAfterValidation[index]["skills"]
            ? dataAfterValidation[index]["skills"]
            : i[BulkUploadGridHeader.SkillId],
        skillId: i[BulkUploadGridHeader.SkillId],
        totalHours: i[BulkUploadGridHeader.NoOfHours],
        RequisitionStatus: "",
        pipelineName: projectDetails?.pipelineName,
        empName: "",
        status: i["Status"] === "Success" ? true : false,
        comments: i["Comments"],
      };
    });
    return payloadData;
  };

  const getFinalBulkData = () => {
    const finalBulkData = dataAfterValidation?.map((item) => ({
      pipelineCode: item.pipelineCode || "",
      jobCode: projectDetails.jobCode,
      jobName: projectDetails.jobName,
      clientName: item.clientName || "",
      projectName: item.projectName || "",
      projectStartDate: item.projectStartDate || "",
      projectEndDate: item.projectEndDate || "",
      requisitionDescription: item.requisitionDescription || "",
      designationId: item.designationId || "",
      competencyId: item.competencyId || "",
      competency: item.competency || "",
      offerings: projectDetails?.offerings || "",
      solutions: projectDetails?.solutions || "",
      designation: item.designation || "",
      grade: item.grade || "",
      startDate: item.startDate || "",
      endDate: item.endDate || "",
      emailId: item.emailId || "",
      empCode: item.empCode || "",
      locationCode: item.locationCode || "",
      BusinessUnit: item.businessUnit || "",
      expertise: item.expertise || "",
      subIndustryWeight: item.subIndustryWeight || "",
      subIndustryID: item.subIndustryID || "",
      subIndustry: item.subIndustry || "",
      industry: item.industry || "",
      sameClientExperienceWeightage: item.sameClientExperienceWeightage || "",
      locations: item.locations || "",
      locationWeightage: item.locationWeightage || "",
      competencyWeightage: item.competencyWeightage || "",
      offeringsWeightage: item.offeringsWeightage || "",
      solutionsWeightage: item.solutionsWeightage || "",
      skillWeightage: item.skillWeightage || "",
      selectedOption: selectedRadioValue,
      numberOfResources: item.numberOfResources || "",
      numberOfHours: item.totalHours || "",
      totalHours: item.totalHours || "",
      perDay: item.perDay || "",
      skills: item.skills || "",
      skillList: item.skillList || "",
      RequisitionStatus: "",
      pipelineName: item.pipelineName || "",
      empName: "",
      status: item.status,
      comments: item.comments || [],
      parameters: item.parameters.map((param) => ({
        name: param.name,
        value: param.value,
        isChecked: param.value > 0 ? true : false,
        priority:
          param.value === maxPriorityValue ? "Must Have" : "Good To Have",
      })),
    }));
    return finalBulkData;
  };

  const handleDataOnSubmit = async () => {
    const finalBulkData = getFinalBulkData();
    const finalData = finalBulkData?.filter((a) => a?.status === true);
    const allocationFinalData = finalData.map((item) => {
      const emailId = item.emailId;
      const empCode = item.empCode;
      const newEmailId = `${empCode}__${emailId}`;
      return {
        ...item,
        emailId: newEmailId,
      };
    });
    if (selectedRadioValue === "requisition") {
      try {
        loaderContext.open(true);
        await BulkUploadRequisition(finalData);
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Requisition Uploaded Successfully!",
          "success",
          6000
        );
        navigateToRequisitionsTab();
      } catch (error) {
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Error in Uploading Requisition",
          "error",
          6000
        );
      }
    } else {
      setFinalBulkAllocationData(allocationFinalData);
      setOpenCommonAllocationScreen(true);
    }
  };

  const handleSubmit = () => {
    handleDataOnSubmit();
  };

  const handleChange = (event) => {
    setSelectedRadioValue(event.target.value);
  };

  const hasValidRecords = dataAfterValidation?.filter(
    (record) => record.status === true
  );

  const [isDirty, setIsDirty] = useState<boolean>(false);
  useBlockRefreshAndBack(isDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isDirty);

  return (
    <div>
      {blocker.state === "blocked" && isDirty ? (
        <DialogBox
          showDialog={isDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <BulkAllocationCommonScreen
        projectInfo={projectDetails}
        openCommonAllocationScreen={openCommonAllocationScreen}
        setOpenCommonAllocationScreen={() => {
          setOpenCommonAllocationScreen(false);
          setFinalBulkAllocationData([]);
        }}
        bulkAllocationsUploaded={finalBulkAllocationData}
      />
      <ConfirmationDialog
        title="Confirm!"
        content={
          selectedRadioValue?.toLowerCase() === "requisition"
            ? "Are you sure you want to upload all valid requisitions?"
            : "Are you sure you want to upload all valid  allocations?"
        }
        noBtnLabel="No"
        yesBtnLabel="Yes"
        open={openModal}
        onConfirmationPopClose={() => {
          setOpenModal(false);
        }}
        handleYesClick={() => {
          handleSubmit();
          setOpenModal(false);
          setIsDirty(false);
        }}
      ></ConfirmationDialog>
      <div className="reload-button">
        <div className="file-name-container">
          {showBrowseComponent && <BackButton />}
          {agGridDisplay && <BackUploadButton />}
          {/* <span>{fileSelected}</span> */}
        </div>
        <div className="file-name-container">
          {showBrowseComponent && (
            <Tooltip title="Reload">
              <RefreshIcon onClick={() => window.location.reload()} />
            </Tooltip>
          )}
          {agGridDisplay && (
            <>
              <div className="submit-container">
                <ActionButton
                  label={"Submit"}
                  onClick={() => {
                    setOpenModal(true);
                  }}
                  type={"button"}
                  disabled={
                    gridRowData?.length === 0 || hasValidRecords?.length === 0
                  }
                />
              </div>
            </>
          )}
        </div>
      </div>
      <div className="bulk-upload-title">
        <BulkUploadTitle projectDetails={projectDetails} />
      </div>
      <div>
        <div>
          <BulkUploadSubTitle
            projectDetails={projectDetails}
            fileSelected={fileSelected}
            agGridDisplay={agGridDisplay}
            showBrowseComponent={showBrowseComponent}
          />
        </div>
      </div>
      {showBrowseComponent && (
        <div className="row">
          <div className="upload-container">
            <div className="upload-box-back">
              <span className="Upload-data">Upload</span>
              <FormControl>
                <RadioGroup
                  row
                  name="row-radio-buttons-group"
                  className="radio-group-main"
                  value={selectedRadioValue}
                  onChange={handleChange}
                >
                  <FormControlLabel
                    value="requisition"
                    control={<Radio />}
                    label="Requisition"
                  />
                  <FormControlLabel
                    value="allocation"
                    control={<Radio />}
                    disabled={projectDetails?.isPublishedToMarketPlace === true}
                    label="Allocation"
                  />
                </RadioGroup>
              </FormControl>
              <div
                className={`upload-box ${dragActive ? "drag-active" : ""}`}
                onDragEnter={handleDrag}
                onDragLeave={handleDrag}
                onDragOver={handleDrag}
                onDrop={handleDrop}
              >
                <div className="upload-content">
                  <div className="uploadArea">
                    {
                      <div>
                        <div className="upload-img">
                          <img src={Upload} alt="upload" />
                        </div>
                        <div className="upload-text">
                          <span> Drag & Drop your files here</span>
                        </div>
                        <input
                          type="file"
                          className="displayNone"
                          ref={hiddenFileInput}
                          onChange={handleFileChange}
                          onClick={(event: any) => {
                            event.target.value = null;
                          }}
                        />
                        <div className="browse-btn-main">
                          <ActionButton
                            label={"Browse"}
                            onClick={fileSelect}
                            type={"button"}
                            disabled={IsProjectInActiveOrClosed(projectDetails)}
                          />
                        </div>
                        <div className="warning-container">
                          <div>* Supported file formats: Xlsx</div>
                          <div>* Acceptable date formats: DD-MM-YYYY</div>
                          <div>
                            *Please read Guidance sheet for instructions for
                            filling in the file.
                          </div>
                        </div>
                      </div>
                    }
                  </div>
                </div>
              </div>
              <div className="progressDisplay">
                {fileSelected && <ProgressBarDisplay value={progressValue} />}
              </div>
              <div>
                <DownloadTemplate selectedRadioValue={selectedRadioValue} />
              </div>
            </div>
          </div>
          <div className="note-container">
            <div className="chrome-note">
              <span>Disclaimers -</span>
              <span>
                1. For best results, run this application on browser with screen
                resolution of 1920 * 1080 with 100% zoom
              </span>
              <span>
                2. To avoid any data loss, please do not refresh the browser and
                maintain the session
              </span>
              <span>3. Loss of connectivity will result in data loss</span>
            </div>
          </div>
        </div>
      )}
      {agGridDisplay && (
        <div className="m-t-40">
          <UploadAgGrid
            gridRowData={gridRowData}
            onGridReady={onGridReady}
            onRowDataChanged={onRowDataChanged}
            errors={errors}
            setErrors={setErrors}
            dataAfterValidation={dataAfterValidation}
            gridComponentRef={gridComponentRef}
            selectedRadioValue={selectedRadioValue}
          />
        </div>
      )}
    </div>
  );
};

export default BulkUploadMain;
