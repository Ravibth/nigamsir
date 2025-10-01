import {
  Box,
  IconButton,
  Modal,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Tooltip,
  Typography,
} from "@mui/material";
import React from "react";
import CloseIcon from "@mui/icons-material/Close";
import { ViewMoreDetailModalSXProps } from "../../../activeRequisitionsDeatils/activerequisition/active-requisition-modal/constant";
import { getCurrentTimeZoneDate } from "../../../../utils/date/dateHelper";

const ViewMoreAllocationDetail = (props: any) => {
  const tableHeader = [
    "Employee name",
    "Employee Id",
    "Designation",
    "Start date",
    "End date",
    "Allocated hours",
    "Skills",
    "Service Line",
    "Industry",
  ];

  return (
    <div>
      <Modal
        open={props.isViewMorePopOpen}
        onClose={props.handleCloseViewDetailModal}
      >
        <Box sx={ViewMoreDetailModalSXProps}>
          <>
            <Typography
              className="assign-modal-header"
              id="modal-modal-title"
              variant="h5"
              component="h5"
            >
              <span className="modal-heading">Allocation Details</span>

              <span>
                <Tooltip title={"Close"}>
                  <IconButton>
                    <CloseIcon
                      onClick={() => {
                        props.handleCloseViewDetailModal();
                      }}
                    />
                  </IconButton>
                </Tooltip>
              </span>
            </Typography>
            <>
              <TableContainer>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                  <TableHead>
                    <TableRow>
                      {tableHeader.map((header, index) => (
                        <TableCell key={index}>{header}</TableCell>
                      ))}
                    </TableRow>
                  </TableHead>
                  {props.selectedRowData && (
                    <TableBody>
                      {/* {props.alloctionsList.map((row: any) => ( */}
                      <>
                        <TableRow
                          key={props.selectedRowData.empEmail}
                          sx={{
                            "&:last-child td, &:last-child th": { border: 0 },
                            minHeight: "20px !important",
                          }}
                        >
                          <TableCell component="th" scope="row">
                            {props.selectedRowData.empName}
                          </TableCell>
                          <TableCell component="th" scope="row">
                            {props.selectedRowData.empEmail}
                          </TableCell>
                          <TableCell component="th" scope="row">
                            {props.selectedRowData.designation}
                          </TableCell>
                          <TableCell>
                            {getCurrentTimeZoneDate(
                              new Date(props.selectedRowData.startDate)
                            ).toLocaleString("en-US", {
                              dateStyle: "medium",
                            })}
                          </TableCell>
                          <TableCell>
                            {getCurrentTimeZoneDate(
                              new Date(props.selectedRowData.endDate)
                            ).toLocaleString("en-US", {
                              dateStyle: "medium",
                            })}
                          </TableCell>
                          <TableCell>
                            {props.selectedRowData.confirmedPerDayHours}
                          </TableCell>
                          <TableCell>
                            {props.selectedRowData.skills.map(
                              (item: any, index: number) => (
                                <span key={item.id}>
                                  {item.skillName}
                                  {index <
                                    props.selectedRowData.skills.length - 1 &&
                                    ", "}
                                </span>
                              )
                            )}
                          </TableCell>
                          <TableCell>
                            {/* {row.requisitionParameters
                              .filter(
                                (item: any) =>
                                  parseInt(item.requisitonWeight) === 10
                              )
                              .map((item: any) =>
                                props.categoryValue(row, item)
                              )} */}
                          </TableCell>
                          <TableCell>
                            <Typography>
                              {/* {row.requisitionParameters
                                .filter(
                                  (item: any) =>
                                    parseInt(item.requisitonWeight) < 10
                                )
                                .map((item: any, index: any) =>
                                  props.categoryValue(row, item)
                                )} */}
                            </Typography>
                          </TableCell>
                        </TableRow>
                      </>
                      {/* ))} */}
                    </TableBody>
                  )}
                </Table>
              </TableContainer>
            </>
          </>
        </Box>
      </Modal>
    </div>
  );
};

export default ViewMoreAllocationDetail;
