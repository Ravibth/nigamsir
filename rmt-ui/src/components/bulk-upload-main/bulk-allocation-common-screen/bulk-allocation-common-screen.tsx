import { useEffect, useState } from "react";
import { Box, IconButton, Modal, Typography } from "@mui/material";
import HighlightOffOutlinedIcon from "@mui/icons-material/HighlightOffOutlined";
import CommonAllocationWrapper from "../../common-allocation/commonAllocationWrapper";
import { EAllocationType } from "../../common-allocation/enum";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { IBulkUploadData } from "../interfaces";
import BackDropModal from "../../../common/back-drop-modal/backDropModal";

export interface IBulkAllocationCommonScreen {
  projectInfo: IProjectMaster;
  openCommonAllocationScreen: boolean;
  setOpenCommonAllocationScreen: (value: React.SetStateAction<boolean>) => void;
  bulkAllocationsUploaded: IBulkUploadData[];
}
const style = {
  width: "95%",
  zIndex: 20,
  height: "90vh",
  borderRadius: "15px",
};
const BulkAllocationCommonScreen = (props: IBulkAllocationCommonScreen) => {
  return (
    <BackDropModal
      open={props.openCommonAllocationScreen}
      onclose={props.setOpenCommonAllocationScreen}
      style={style}
      restrictOnClose={true}
    >
      <Box>
        <IconButton
          sx={{
            position: "absolute",
            top: 0,
            right: 0,
            zIndex: 1,
          }}
          onClick={() => props.setOpenCommonAllocationScreen(false)}
        >
          <HighlightOffOutlinedIcon />
        </IconButton>
        <Typography component="div" sx={{ marginTop: "20px" }}>
          <CommonAllocationWrapper
            back={function (): {} {
              props.setOpenCommonAllocationScreen(false);
              return;
            }}
            baseType={EAllocationType.BULK_ALLOCATION}
            projectInfo={props.projectInfo}
            bulkAllocationsUploaded={props.bulkAllocationsUploaded}
          />
        </Typography>
      </Box>
    </BackDropModal>

    // <Modal
    //   open={props.openCommonAllocationScreen}
    //   onClose={props.setOpenCommonAllocationScreen}
    //   slots={{ backdrop: "noscript" }}
    //   style={{
    //     width: "95%",
    //     height: "900px",
    //     //    top: "-17%", left: "3%"
    //   }}
    // >
    //   <Box sx={style}>
    //     <IconButton
    //       sx={{
    //         position: "absolute",
    //         top: 0,
    //         right: 0,
    //         zIndex: 1,
    //       }}
    //       onClick={() => props.setOpenCommonAllocationScreen(false)}
    //     >
    //       <HighlightOffOutlinedIcon />
    //     </IconButton>
    //     <Typography component="div" sx={{ marginTop: "20px" }}>
    //       <CommonAllocationWrapper
    //         back={function (): {} {
    //           props.setOpenCommonAllocationScreen(false);
    //           return;
    //         }}
    //         baseType={EAllocationType.BULK_ALLOCATION}
    //         projectInfo={props.projectInfo}
    //         bulkAllocationsUploaded={props.bulkAllocationsUploaded}
    //       />
    //     </Typography>
    //   </Box>
    // </Modal>
  );
};
export default BulkAllocationCommonScreen;
