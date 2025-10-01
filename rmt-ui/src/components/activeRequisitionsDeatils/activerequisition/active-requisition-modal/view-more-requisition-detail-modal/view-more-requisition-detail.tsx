import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  Chip,
  Grid,
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
import { ViewMoreDetailModalSXProps } from "../constant";
import CloseIcon from "@mui/icons-material/Close";
import { getCurrentTimeZoneDate } from "../../../../../utils/date/dateHelper";
import { SkillsChip } from "../../requisitiontable/constant";

const ViewMoreRequisitionDetail = (props: any) => {
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
              <span className="modal-heading">Requisition Details</span>

              <span>
                <Tooltip title={"Close"}>
                  <IconButton
                    onClick={() => {
                      props.handleCloseViewDetailModal();
                    }}
                  >
                    <CloseIcon />
                  </IconButton>
                </Tooltip>
              </span>
            </Typography>

            {/* <Box sx={{ mt: 2 }}>
              {props.requisitionsList.map((row: any) => (
                <>
                  <Grid container xs={12} sx={{ mb: 2 }}>
                    <Grid xs={3}>
                      <Typography>Designation:</Typography>
                      <span> {row.designation}</span>
                    </Grid>
                    <Grid xs={3}>
                      <Typography>
                        No. of resources:{" "}
                        <span> {row?.demands?.totalDemands}</span>
                      </Typography>
                    </Grid>
                    <Grid xs={3}>
                      <Typography>
                        Start date:{" "}
                        <span>
                          {getCurrentTimeZoneDate(
                            new Date(row.startDate)
                          ).toLocaleString("en-US", {
                            dateStyle: "medium",
                          })}
                        </span>
                      </Typography>
                    </Grid>
                    <Grid xs={3}>
                      <Typography>
                        End date:{" "}
                        <span>
                          {getCurrentTimeZoneDate(
                            new Date(row.endDate)
                          ).toLocaleString("en-US", {
                            dateStyle: "medium",
                          })}
                        </span>
                      </Typography>
                    </Grid>
                  </Grid>
                  <Grid container xs={12} sx={{ mb: 2 }}>
                    <Grid xs={3}>
                      <Typography>
                        No. of hours: <span>{row.totalHours}</span>
                      </Typography>
                    </Grid>
                    <Grid xs={6}>
                      <Typography>
                        Skills:{" "}
                        <span>
                          {Array.from(
                            {
                              length: Math.ceil(
                                row.requisitionSkill.length / 2
                              ),
                            },
                            (_, index) => (
                              <span
                                key={index}
                              
                              >
                               
                                {row.requisitionSkill
                                  .map((item: any) => item.skillName)
                                  .join(", ")}
                              </span>
                            )
                          )}
                        </span>
                      </Typography>
                    </Grid>
                    <Grid xs={3}>
                    
                    </Grid>
                  </Grid>
                  <Grid container xs={12} sx={{ mb: 2 }}>
                    <Grid xs={3}>
                      <Typography>
                        Must have: <span></span>
                      </Typography>
                    </Grid>
                  </Grid>

                </>
              ))}
            </Box> */}
            <>
              <TableContainer>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                  <TableHead>
                    <TableRow>
                      <TableCell>Designation</TableCell>
                      <TableCell>No. of resources</TableCell>
                      <TableCell>Start date</TableCell>
                      <TableCell>End date</TableCell>
                      <TableCell>No. of hours</TableCell>
                      <TableCell>Skills</TableCell>
                      <TableCell>Must Have</TableCell>
                      <TableCell>Good to have</TableCell>
                    </TableRow>
                  </TableHead>
                  {props.selectedRowData && (
                    <TableBody>
                      {/* {props.requisitionsList.map((row: any) => ( */}
                      <>
                        <TableRow
                          key={props.selectedRowData.designation}
                          sx={{
                            "&:last-child td, &:last-child th": { border: 0 },
                            minHeight: "20px !important",
                          }}
                        >
                          <TableCell component="th" scope="row">
                            {props.selectedRowData.designation}
                          </TableCell>
                          <TableCell component="th" scope="row">
                            {props.selectedRowData?.demands?.totalDemands}
                          </TableCell>
                          <TableCell>
                            {getCurrentTimeZoneDate(
                              new Date(props.selectedRowData.startDate)
                            ).toLocaleString("en-US", {
                              dateStyle: "medium",
                            })}
                          </TableCell>
                          <TableCell>
                            {" "}
                            {getCurrentTimeZoneDate(
                              new Date(props.selectedRowData.endDate)
                            ).toLocaleString("en-US", {
                              dateStyle: "medium",
                            })}
                          </TableCell>
                          <TableCell>
                            {props.selectedRowData.totalHours}
                          </TableCell>
                          <TableCell>
                            <span>
                              {Array.from(
                                {
                                  length: Math.ceil(
                                    props.selectedRowData.requisitionSkill
                                      .length / 2
                                  ),
                                },
                                (_, index) => (
                                  <span key={index}>
                                    {props.selectedRowData.requisitionSkill
                                      .map((item: any) => item.skillName)
                                      .join(", ")}
                                  </span>
                                )
                              )}
                            </span>
                          </TableCell>
                          <TableCell>
                            {props.selectedRowData.requisitionParameters
                              .filter(
                                (item: any) =>
                                  parseInt(item.requisitonWeight) === 10
                              )
                              .map((item: any) =>
                                props.categoryValue(props.selectedRowData, item)
                              )}
                          </TableCell>
                          <TableCell>
                            <Typography>
                              {props.selectedRowData.requisitionParameters
                                .filter(
                                  (item: any) =>
                                    parseInt(item.requisitonWeight) < 10
                                )
                                .map((item: any, index: any) =>
                                  props.categoryValue(
                                    props.selectedRowData,
                                    item
                                  )
                                )}
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

export default ViewMoreRequisitionDetail;
