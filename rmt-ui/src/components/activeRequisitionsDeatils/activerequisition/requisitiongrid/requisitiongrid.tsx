import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import * as constant from "./constant";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import { IconButton, Menu, MenuItem, TableCell } from "@mui/material";
import { useState } from "react";

function createData(
  employeename: string,
  employeeid: string,
  designation: string,
  startdate: string,
  enddate: string,
  location: string,
  expertise: string,
  smegroup: string,
  taskrequires: string,
  skills: string
) {
  return {
    employeename,
    employeeid,
    designation,
    startdate,
    enddate,
    location,
    expertise,
    smegroup,
    taskrequires,
    skills,
  };
}

const rows = [];

export default function Requisitiontable() {
  const [anchorEl, setAnchorEl] = useState(null);

  const handleClick = (event: any) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell sx={constant.Headings}>Employee name</TableCell>
            <TableCell sx={constant.Headings}>Employee ID</TableCell>
            <TableCell sx={constant.Headings}>Deignation</TableCell>
            <TableCell sx={constant.Headings}>Start date</TableCell>
            <TableCell sx={constant.Headings}>End date</TableCell>
            <TableCell sx={constant.Headings}>Location</TableCell>
            <TableCell sx={constant.Headings}>Expertise</TableCell>
            <TableCell sx={constant.Headings}>SME Group</TableCell>
            <TableCell sx={constant.Headings}>This task requires</TableCell>
            <TableCell sx={constant.Headings}>Skills</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row) => (
            <TableRow
              key={row.employeename}
              sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {row.employeename}
              </TableCell>
              <TableCell sx={constant.DataRows}>{row.employeeid}</TableCell>
              <TableCell sx={constant.DataRows}>{row.designation}</TableCell>
              <TableCell sx={constant.DataRows}>{row.startdate}</TableCell>
              <TableCell sx={constant.DataRows}>{row.enddate}</TableCell>
              <TableCell sx={constant.DataRows}>{row.location}</TableCell>
              <TableCell sx={constant.DataRows}>{row.expertise}</TableCell>
              <TableCell sx={constant.DataRows}>{row.smegroup}</TableCell>
              <TableCell sx={constant.DataRows}>{row.taskrequires}</TableCell>
              <TableCell sx={constant.DataRows}>{row.skills}</TableCell>
              <TableCell sx={constant.DataRows}>
                <IconButton onClick={handleClick}>
                  <MoreVertIcon />
                </IconButton>
                <Menu
                  anchorEl={anchorEl}
                  open={Boolean(anchorEl)}
                  onClose={handleClose}
                  PaperProps={{
                    style: {
                      transform: "translateX(-50%)",
                      boxShadow: "none", // Remove the shadow
                      border: "1px solid rgba(216,216,216,255)", // Add a grey border
                      backgroundColor: "rgba(238,238,238,255)", // Set the background color
                    },
                  }}
                >
                  <MenuItem onClick={handleClose}>Show calendar view</MenuItem>
                  <MenuItem onClick={handleClose}>Update Allocation</MenuItem>
                  <MenuItem onClick={handleClose}>Release Employee</MenuItem>
                </Menu>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
