/* eslint-disable jsx-a11y/anchor-is-valid */
import React from "react";
import FileDownloadOutlinedIcon from "@mui/icons-material/FileDownloadOutlined";
import {
  DownloadTemplateIcon,
  XLSX_FILE_URL_ALLOCATION,
  XLSX_FILE_URL_REQUISITION_,
} from "./bulk-upload-constant";

const DownloadTemplate = (props: any) => {
  const downloadFileAtURL = (url: any) => {
    if (url) {
      const fileName = url?.split("/").pop();
      const aTag = document.createElement("a");
      aTag.href = url;
      aTag.setAttribute("download", fileName);
      document.body.appendChild(aTag);
      aTag.click();
      aTag.remove();
    }
  };
  return (
    <div className="template-container">
      <FileDownloadOutlinedIcon sx={DownloadTemplateIcon} />
      <a
        onClick={() => {
          downloadFileAtURL(
            props?.selectedRadioValue?.toLowerCase() === "requisition"
              ? XLSX_FILE_URL_REQUISITION_
              : XLSX_FILE_URL_ALLOCATION
          );
        }}
      >
        Download Template
      </a>
    </div>
  );
};

export default DownloadTemplate;
