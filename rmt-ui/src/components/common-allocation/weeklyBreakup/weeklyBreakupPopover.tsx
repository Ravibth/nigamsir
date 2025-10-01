import {
  Typography,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Paper,
  Popover,
} from "@mui/material";
import { capitalizeFirstLetter } from "../../../global/utils";

const WeeklyBreakupPopover = (props: any) => {
  return (
    <>
      <Popover
        id="mouse-over-popover"
        sx={{ pointerEvents: "none" }}
        open={true}
        anchorEl={props.anchorEl}
        anchorOrigin={{
          vertical: "top",
          horizontal: "left",
        }}
        transformOrigin={{
          vertical: "bottom",
          horizontal: "left",
        }}
        onClose={props.handlePopoverClose}
        disableRestoreFocus
      >
        <Typography sx={{ p: 1 }}>
          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 300 }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <>
                    {Object.keys(props.weeklyData?.breakup).map(
                      (itemBreakup: any,index:number) => {
                        return (
                          <TableCell
                            style={{ fontWeight: "bold" }}
                            align="center"
                            key={index}
                          >
                            {capitalizeFirstLetter(itemBreakup)}
                          </TableCell>
                        );
                      }
                    )}
                  </>
                </TableRow>
              </TableHead>
              <TableBody>
                <TableRow
                  key={"row.name"}
                  sx={{
                    "&:last-child td, &:last-child th": { border: 0 },
                  }}
                >
                  <>
                    {Object.keys(props.weeklyData?.breakup).map(
                      (itemBreakup: any) => {
                        return (
                          <TableCell align="center">
                            {props.weeklyData?.breakup[itemBreakup]}
                          </TableCell>
                        );
                      }
                    )}
                  </>
                </TableRow>
                <TableRow
                  key={"row.name"}
                  sx={{
                    "&:last-child td, &:last-child th": { border: 0 },
                  }}
                >
                  <TableCell
                    colSpan={5}
                    // style={{ fontWeight: "bold" }}
                    align="right"
                  >
                    {/* {"Allocation Total (hrs) - "}
                    {"Available Total (hrs) - "} */}
                    {props.totalPlaceHolderText != undefined
                      ? props.totalPlaceHolderText
                      : "Weekly Total (hrs) - "}
                    {props.weeklyData?.weeklyTotal}
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </TableContainer>
        </Typography>
      </Popover>
    </>
  );
};

export default WeeklyBreakupPopover;
