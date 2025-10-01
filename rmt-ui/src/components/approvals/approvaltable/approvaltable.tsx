import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import * as constant from "./constant";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import {
  Button,
  Chip,
  Grid,
  Pagination,
  Popover,
  TableCell,
  TextField,
} from "@mui/material";
import { useEffect, useState } from "react";
import ActionButton from "../../actionButton/actionButton";
import TablePagination from "@mui/material/TablePagination"; // Import TablePagination
import CheckIcon from "@mui/icons-material/Check";
import AccessTimeIcon from "@mui/icons-material/AccessTime";

function createData(
  projectname: string,
  projectid: string,
  resourcename: string,
  designation: string,
  startdate: string,
  enddate: string,
  totalhours: number,
  deudate: string
) {
  return {
    projectname,
    projectid,
    resourcename,
    designation,
    startdate,
    enddate,
    totalhours,
    deudate,
  };
}

const rows = [
  createData(
    "ProjectName1",
    "PC101",
    "Jatin Kumar",
    "Sr Consultant",
    "11-2-2023",
    "11-9-2023",
    160,
    "2 days left"
  ),
  createData(
    "ProjectName2",
    "PC102",
    "John Doe",
    "Software Engineer",
    "11-2-2023",
    "11-9-2023",
    120,
    "Approved"
  ),
  createData(
    "ProjectName3",
    "PC103",
    "Alice Smith",
    "UI Designer",
    "11-2-2023",
    "11-9-2023",
    80,
    "1 day left"
  ),
  createData(
    "ProjectName3",
    "PC103",
    "Alice Smith",
    "UI Designer",
    "11-2-2023",
    "11-9-2023",
    80,
    "Time out"
  ),
  createData(
    "ProjectName1",
    "PC101",
    "Jatin Kumar",
    "Sr Consultant",
    "11-2-2023",
    "11-9-2023",
    160,
    "2 days left"
  ),
  createData(
    "ProjectName2",
    "PC102",
    "John Doe",
    "Software Engineer",
    "11-2-2023",
    "11-9-2023",
    120,
    "Approved"
  ),
  createData(
    "ProjectName3",
    "PC103",
    "Alice Smith",
    "UI Designer",
    "11-2-2023",
    "11-9-2023",
    80,
    "1 day left"
  ),
  createData(
    "ProjectName3",
    "PC103",
    "Alice Smith",
    "UI Designer",
    "11-2-2023",
    "11-9-2023",
    80,
    "Time out"
  ),
  createData(
    "ProjectName1",
    "PC101",
    "Jatin Kumar",
    "Sr Consultant",
    "11-2-2023",
    "11-9-2023",
    160,
    "2 days left"
  ),
  createData(
    "ProjectName2",
    "PC102",
    "John Doe",
    "Software Engineer",
    "11-2-2023",
    "11-9-2023",
    120,
    "Approved"
  ),
  createData(
    "ProjectName3",
    "PC103",
    "Alice Smith",
    "UI Designer",
    "11-2-2023",
    "11-9-2023",
    80,
    "1 day left"
  ),
  createData(
    "ProjectName3",
    "PC103",
    "Alice Smith",
    "UI Designer",
    "11-2-2023",
    "11-9-2023",
    80,
    "Time out"
  ),
  createData(
    "ProjectName1",
    "PC101",
    "Jatin Kumar",
    "Sr Consultant",
    "11-2-2023",
    "11-9-2023",
    160,
    "2 days left"
  ),
  createData(
    "ProjectName2",
    "PC102",
    "John Doe",
    "Software Engineer",
    "11-2-2023",
    "11-9-2023",
    120,
    "Approved"
  ),
  // Add more rows as needed
];

export default function Approvaltable(props: any) {
  const [anchorEl, setAnchorEl] = useState(null);
  const [openPopup, setOpenPopup] = useState(false);
  const [rejectReason, setRejectReason] = useState("");
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(8);

  const open = Boolean(anchorEl);

  const handleClosePopup = () => {
    setOpenPopup(false);
  };

  const handleReasonChange = (event: any) => {
    setRejectReason(event.target.value);
  };

  const handleReject = () => {
    // console.log("Rejection Reason:", rejectReason);
    handleClosePopup();
  };

  const handleClick = (event: any) => {
    // console.log("Clicked");
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleChangePage = (event: any, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: any) => {
    setRowsPerPage(parseInt(event.target.value, 8));
    setPage(0);
  };

  const renderDueDateChip = (dueDate: string) => {
    let chipColor = "";
    let icon = null;

    if (dueDate.includes("Approved")) {
      chipColor = "#b3d9b3";
      icon = <CheckIcon sx={{ color: "white" }} />;
    } else if (dueDate.includes("Time out")) {
      chipColor = "#B8ABCB";
      icon = <AccessTimeIcon />;
    } else if (dueDate.includes("day left")) {
      const daysLeft = parseInt(dueDate.split(" ")[0], 10);
      if (daysLeft >= 2) {
        chipColor = "black";
      } else {
        chipColor = "#ef9a9a";
      }
    }

    return (
      <Chip
        label={dueDate}
        sx={{
          backgroundColor: chipColor,
          color: "white",
          borderRadius: "30px",
          width: "100%",
          height: "35px",
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          marginTop: "10px",
          "& .MuiSvgIcon-root": {
            fill: "white", // Set the icon color to white
          },
        }}
        //icon={icon}
      />
    );
  };

  const indexOfLastRow = (page + 1) * rowsPerPage;
  const indexOfFirstRow = indexOfLastRow - rowsPerPage;
  const currentRows = rows.slice(indexOfFirstRow, indexOfLastRow);

  return (
    <div style={{ marginLeft: "25px" }}>
      <Grid container sx={{ paddingTop: "30px" }}>
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 650 }} aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell sx={constant.Headings}>Project name</TableCell>
                <TableCell sx={constant.Headings}>Project ID</TableCell>
                <TableCell sx={constant.Headings}>Resource name</TableCell>
                <TableCell sx={constant.Headings}>Designation</TableCell>
                <TableCell sx={constant.Headings}>Start date</TableCell>
                <TableCell sx={constant.Headings}>End date</TableCell>
                <TableCell sx={constant.Headings}>Total Hours</TableCell>
                <TableCell sx={constant.Headings}>Due Date</TableCell>
                <TableCell sx={constant.Headings}>Action</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {currentRows.map((row, index) => (
                <TableRow
                  key={index}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                >
                  <TableCell sx={constant.DataRows}>
                    {row.projectname}
                  </TableCell>
                  <TableCell sx={constant.DataRows}>{row.projectid}</TableCell>

                  <TableCell sx={constant.DataRows}>
                    {" "}
                    <a // href={row.skillLink}
                      target="_blank"
                      rel="noopener noreferrer"
                      style={{ textDecoration: "none" }}
                    >
                      <span style={{ textDecoration: "underline" }}>
                        {row.resourcename}
                      </span>
                    </a>
                  </TableCell>
                  <TableCell sx={constant.DataRows}>
                    {row.designation}
                  </TableCell>
                  <TableCell sx={constant.DataRows}>{row.startdate}</TableCell>
                  <TableCell sx={constant.DataRows}>{row.enddate}</TableCell>
                  <TableCell sx={constant.DataRows}>{row.totalhours}</TableCell>
                  <TableCell sx={constant.DataRows}>
                    {renderDueDateChip(row.deudate)}
                  </TableCell>
                  <TableCell sx={constant.DataRows}>
                    <div style={{ display: "flex", gap: "16px" }}>
                      <ActionButton
                        label={"Approve"}
                        onClick={function (e: any): void {}}
                        disabled={false}
                        type={"button"}
                      />
                      <Button
                        sx={{
                          width: "100%",
                          color: "#4f2d7f",
                          borderColor: "#4f2d7f",
                          // fontFamily: "GT Walsheim Pro, Medium",
                        }}
                        variant="outlined"
                        onClick={handleClick}
                      >
                        Reject
                      </Button>

                      <Popover
                        open={open}
                        anchorEl={anchorEl}
                        onClose={handleClose}
                        anchorOrigin={{
                          vertical: "bottom",
                          horizontal: "left",
                        }}
                      >
                        <div style={{ padding: "16px" }}>
                          <TextField
                            label="Provide a reason for rejection"
                            variant="outlined"
                            fullWidth
                            value={rejectReason}
                            onChange={handleReasonChange}
                          />
                          <ActionButton
                            label={"Submit"}
                            onClick={handleReject}
                            disabled={false}
                            type={"button"}
                          />
                        </div>
                      </Popover>
                    </div>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Grid>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Pagination
          count={Math.ceil(rows.length / rowsPerPage)}
          page={page + 1}
          onChange={(event, newPage) => handleChangePage(null, newPage - 1)}
          size="large"
          sx={{
            "& .Mui-selected": {
              color: "white",
              backgroundColor: "#4f2d7f",
              borderRadius: "50%",
              width: "38px",
              height: "38px",
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              marginTop: "10px",
            },
          }}
        />
      </div>
    </div>
  );
}
