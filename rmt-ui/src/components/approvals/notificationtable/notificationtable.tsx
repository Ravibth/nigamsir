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

function createData(projectname: string) {
  return {
    projectname,
  };
}

const rows = [
  createData(
    "Project Name 1 start date has been extended from june 15 2023 to august 29 2023"
  ),
  createData(
    "Project Name 1 start date has been extended from june 15 2023 to august 29 2023"
  ),
  createData(
    "Project Name 1 start date has been extended from june 15 2023 to august 29 2023"
  ),
  // Add more rows as needed
];

export default function Notificationtable(props: any) {
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
    handleClosePopup();
  };

  const handleClick = (event: any) => {
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
