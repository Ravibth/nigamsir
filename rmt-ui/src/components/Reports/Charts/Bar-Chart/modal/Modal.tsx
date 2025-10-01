import { Box, Grid, Modal, Tooltip, Typography } from "@mui/material";
import React, { useEffect } from "react";
import CloseIcon from "@mui/icons-material/Close";
import { IModalProps } from "./IModal";
import "./style.scss";
import TabularChart from "../tabular-Chart/tabular-chart-data";
const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "90%",
  bgcolor: "background.paper",
  // border: "2px solid #000",
  borderRadius: "15px",
  boxShadow: 24,
  p: 4,
};
const CustomReportModal = (props: IModalProps) => {
  const { isOpen, setIsOpen, selectedChartData, modalRowData, colDef } = props;
  useEffect(() => {
    // if (isOpen === true && selectedChartData.chartName && selectedChartData.rowName) {
    //     console.log('useEffect for Tabular data ', isOpen, selectedChartData);
    //     //colDef Definition
    // }
  }, [selectedChartData, isOpen]);
  return (
    <div>
      <Modal
        open={isOpen}
        onClose={() => {
          setIsOpen(false);
        }}
      >
        <Box>
          <Box sx={style}>
            <Typography
              className="assign-modal-header"
              id="modal-modal-title"
              variant="h5"
              component="h5"
            >
              <span className="modal-heading">
                {/* {selectedChartData?.chartName?.toUpperCase().trim() || ''} */}
              </span>

              <span>
                <Tooltip title={"Close"}>
                  <CloseIcon
                    className="Close-Icon"
                    onClick={() => {
                      setIsOpen(false);
                    }}
                  />
                </Tooltip>
              </span>
            </Typography>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              {/* DATA OF THE RESPECTIVE REPORT WILL BE SHONE HERE OF THE RESPECTIVE{" "}
              {selectedChartData?.rowName?.toUpperCase().trim() || ""} */}
              <TabularChart rowData={modalRowData} colDef={colDef} />
            </Typography>
          </Box>
        </Box>
      </Modal>
    </div>
  );
};

export default CustomReportModal;
