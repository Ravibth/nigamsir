import { Button, Grid, IconButton, Tooltip, Typography } from "@mui/material";
import NavigateActionButton from "../../actionButton/navigateActionButton";
import FmdGoodOutlinedIcon from "@mui/icons-material/FmdGoodOutlined";
import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import {
  getDateFormate,
  IsProjectInActiveOrClosed,
  routeValueEncode,
} from "../../../global/utils";
import moment from "moment";
import { getDateInMomentFormat } from "../../../utils/date/dateHelper";
import SaveAsSharpIcon from "@mui/icons-material/SaveAsSharp";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import {
  RequisitionSnackbarMessagesAndLabels,
  DownloadTemplate,
  marginRight5,
  projectSXProps,
  RequisitionButtons,
  RequisitionHeaderContainerSxProps,
  RequisitionHeaderSxProps,
  ProjectDetailContainerSxProps,
  SaveSxProps,
} from "../utils";
import { useNavigate } from "react-router-dom";
import UploadFileIcon from "@mui/icons-material/UploadFile";
import FileDownloadOutlinedIcon from "@mui/icons-material/FileDownloadOutlined";
import { XLSX_FILE_URL_REQUISITION_ } from "../../bulk-upload-main/bulk-upload-constant";
import "../utils.css";

interface IRequisitionFormHeader {
  projectInfo: IProjectMaster;
  isReadOnlyModeOn: boolean;
  setIsReadOnlyModeOn: React.Dispatch<React.SetStateAction<boolean>>;
  updatePermissions?: boolean;
  // isSaveButtonDisabled: boolean;
  isUpdateModeOn: boolean;
}

const RequisitionFormHeader = (props: IRequisitionFormHeader) => {
  const navigate = useNavigate();

  const backButtonClick = () => {
    navigate(
      `/project-details/${routeValueEncode(
        props.projectInfo.pipelineCode
      )}/${routeValueEncode(props.projectInfo.jobCode)}?tab=4`
    );
  };

  const openBulkUploadHandler = (
    event: any,
    pipelineCode: string,
    jobCode: string
  ) => {
    event.preventDefault();
    if (!IsProjectInActiveOrClosed(props.projectInfo)) {
      navigate(
        `/bulk-upload/${routeValueEncode(pipelineCode)}/${routeValueEncode(
          jobCode
        )}`
      );
    }
  };

  const downloadFileAtURL = (url: any) => {
    try {
      if (url) {
        const fileName = url.split("/")?.pop();
        const aTag = document.createElement("a");
        aTag.href = url;
        aTag.setAttribute("download", fileName);
        document.body.appendChild(aTag);
        aTag.click();
        aTag.remove();
      }
    } catch (err) {
      //
    }
  };
  return (
    <>
      <Grid container spacing={2} sx={RequisitionHeaderContainerSxProps}>
        <Grid item xs={2} className="requisition-header-title">
          <Typography component={"span"} sx={RequisitionHeaderSxProps}>
            {`${
              props.isUpdateModeOn
                ? RequisitionSnackbarMessagesAndLabels.UpdateRequisition
                : RequisitionSnackbarMessagesAndLabels.CreateRequisition
            }`}
          </Typography>
        </Grid>
        {!props.projectInfo?.jobCode && (
          <Grid item xs={7} sx={projectSXProps}>
            <Typography component="div">
              <span className="fw-600 data-container job-style">
                {`${
                  props.projectInfo?.pipelineName
                    ? props.projectInfo?.pipelineName
                    : ""
                }`}
              </span>
            </Typography>
          </Grid>
        )}
        {props.projectInfo?.jobCode && (
          <Grid item xs={7} sx={projectSXProps}>
            <Typography component="div">
              <span className="fw-600 data-container job-style">
                {`${
                  props.projectInfo?.jobName ? props.projectInfo?.jobName : ""
                }`}
              </span>
            </Typography>
          </Grid>
        )}
        {!props.isUpdateModeOn ? (
          <>
            <Grid item xs={1.5} className="requisition-header-main">
              <Typography
                component={"span"}
                sx={DownloadTemplate}
                onClick={(e) => {
                  openBulkUploadHandler(
                    e,
                    props.projectInfo.pipelineCode,
                    props.projectInfo.jobCode
                  );
                }}
              >
                <UploadFileIcon fontSize="medium" sx={marginRight5} />
                <Typography component={"span"}>
                  {RequisitionSnackbarMessagesAndLabels.BulkUpload}
                </Typography>
              </Typography>
            </Grid>
            <Grid item xs={1.5} className="requisition-header-main">
              <Typography
                component={"span"}
                sx={DownloadTemplate}
                onClick={() => {
                  downloadFileAtURL(XLSX_FILE_URL_REQUISITION_);
                }}
              >
                <FileDownloadOutlinedIcon fontSize="medium" sx={marginRight5} />
                <Typography component={"span"}>
                  {RequisitionSnackbarMessagesAndLabels.DownloadTemplate}
                </Typography>
              </Typography>
            </Grid>
          </>
        ) : (
          <></>
        )}
      </Grid>
      <Grid container sx={ProjectDetailContainerSxProps}>
        <Grid item xs={3} sx={{ mt: 1 }}>
          <NavigateActionButton
            className="backButton btn-back"
            onClick={backButtonClick}
            label={RequisitionSnackbarMessagesAndLabels.Back}
          />
        </Grid>
        <Grid item xs={2}>
          <Typography component="div">
            <span className="fw-600 data-container">
              <FmdGoodOutlinedIcon className="icon-requisition-location" />
              {`${RequisitionSnackbarMessagesAndLabels.Location}: ${
                props.projectInfo?.location ? props.projectInfo?.location : ""
              }`}
            </span>
          </Typography>
        </Grid>
        <Grid item xs={2}>
          <Typography component="div">
            <span className="fw-600 data-container">
              <CalendarTodayIcon className="icon-requisition" />
              {`${RequisitionSnackbarMessagesAndLabels.StartDate}: ${
                props.projectInfo?.startDate
                  ? getDateFormate(props.projectInfo?.startDate)
                  : ""
              }`}
            </span>
          </Typography>
        </Grid>
        <Grid item xs={2}>
          <Typography component="div">
            <span className="fw-600 data-container">
              <CalendarTodayIcon className="icon-requisition" />
              {`${RequisitionSnackbarMessagesAndLabels.EndDate}: ${
                props.projectInfo?.endDate
                  ? getDateFormate(props.projectInfo?.endDate)
                  : ""
              }`}
            </span>
          </Typography>
        </Grid>
        {!props.isReadOnlyModeOn ? (
          <Grid item xs={3} sx={RequisitionButtons}>
            <Button
              type="submit"
              variant="contained"
              disabled={
                moment(
                  getDateInMomentFormat(props?.projectInfo?.endDate)
                ).isBefore(moment(getDateInMomentFormat(new Date()))) ||
                props?.updatePermissions === false ||
                IsProjectInActiveOrClosed(props?.projectInfo)
                //  ||
                // props.isSaveButtonDisabled
              }
              sx={SaveSxProps}
              onClick={() => {}}
            >
              Save
            </Button>
          </Grid>
        ) : (
          <Grid item xs={3} sx={RequisitionButtons}>
            <Tooltip title="Edit requisition" placement="bottom">
              <IconButton
                className="edit-icon"
                disabled={
                  props?.updatePermissions === false ||
                  IsProjectInActiveOrClosed(props?.projectInfo)
                }
                onClick={() => {
                  props.setIsReadOnlyModeOn(false);
                }}
              >
                <SaveAsSharpIcon />
              </IconButton>
            </Tooltip>
          </Grid>
        )}
      </Grid>
    </>
  );
};

export default RequisitionFormHeader;
