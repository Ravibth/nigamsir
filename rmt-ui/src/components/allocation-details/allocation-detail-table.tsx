import {
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import React from "react";
import * as constant from './Constant';
import { IAllocationDetailProps } from "./IAllocationDetailProps";
import { IAllocationDetail } from "./IAllocationDetail";

const AllocationDetailTable = (props: IAllocationDetailProps) => {
    const {allocationData} = props
  return (
    <>
      <TableContainer>
        <Table  aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Allocation Status</TableCell>
              <TableCell align="left">Effort</TableCell>
              <TableCell align="left">Start date</TableCell>
              <TableCell align="left">End date</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {allocationData?.map((data : IAllocationDetail , index:number) => (
              <TableRow
                key={index}
                sx={constant.getTableRowDesign()}
              >
                <TableCell component="th" scope="row">
                  Allocation {index + 1}
                </TableCell>
                <TableCell align="left">{data.allocationHours} hrs/day</TableCell>
                <TableCell align="left">{data.startDate}</TableCell>
                <TableCell align="left">{data.endDate}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
};

export default AllocationDetailTable;
