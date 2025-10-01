import React, { useState } from 'react';
import {
  Popover,
  Typography,
  TableContainer,
  Table,
  TableBody,
  TableRow,
  TableCell,
  Paper,
} from '@mui/material';

interface WeeklyItemPopoverProps {
  id: string;
  groupId?: string;
  displayContent: React.ReactNode;
  popoverContent: {
    weeklyBreakup: Record<string, string | number>;
    total: number;
    totalLabel?: string;
    customRows?: React.ReactNode[];
  };
  children?: React.ReactNode;
}

const WeeklyPopover: React.FC<WeeklyItemPopoverProps> = ({
  id,
  groupId,
  displayContent,
  popoverContent,
  children,
}) => {
  const [popoverOpen, setPopoverOpen] = useState(false);
  const [popoverAnchorEl, setPopoverAnchorEl] = useState<HTMLElement | null>(null);

  const handlePopoverOpen = (event: React.MouseEvent<HTMLElement>) => {
    setPopoverAnchorEl(event.currentTarget);
    setPopoverOpen(true);
  };

  const handlePopoverClose = () => {
    setPopoverOpen(false);
    setPopoverAnchorEl(null);
  };

  return (
    <>
      <div
        id={id}
        data-group={groupId}
        onMouseEnter={handlePopoverOpen}
        onMouseLeave={handlePopoverClose}
      >
        {displayContent}
      </div>
      {popoverContent && 
        <Popover        
          id={`mouse-over-popover-${id}`}
          sx={{ pointerEvents: "none", zIndex:1600 }}
          open={popoverOpen}
          anchorEl={popoverAnchorEl}
          anchorOrigin={{
            vertical: "bottom",
            horizontal: "left",
          }}
          transformOrigin={{
            vertical: "top",
            horizontal: "left",
          }}
          onClose={handlePopoverClose}
          disableRestoreFocus
        >
          <Typography sx={{ p: 1 }}>
            <TableContainer component={Paper}>
              <Table sx={{ minWidth: 300 }} aria-label="weekly allocation details">
                <TableBody>
                  {popoverContent.customRows && popoverContent.customRows.map((row, index) => (
                    <React.Fragment key={`custom-row-${index}`}>{row}</React.Fragment>
                  ))}
                  
                  <TableRow sx={{ "&:last-child td, &:last-child th": { border: 0 } }}>
                    {Object.keys(popoverContent.weeklyBreakup).map((day) => (
                      <TableCell
                        key={`day-${day}`}
                        align="center"
                        style={{ fontWeight: "bold" }}
                      >
                        {day.slice(0, 3)}
                      </TableCell>
                    ))}
                  </TableRow>

                  <TableRow sx={{ "&:last-child td, &:last-child th": { border: 0 } }}>
                    {Object.values(popoverContent.weeklyBreakup).map((value, index) => (
                      <TableCell key={`value-${index}`} align="center">
                        {value}
                      </TableCell>
                    ))}
                  </TableRow>

                  <TableRow>
                    <TableCell
                      style={{ fontWeight: "bold" }}
                      align="right"
                      colSpan={Object.keys(popoverContent.weeklyBreakup).length}
                    >
                      {popoverContent.totalLabel || 'Total'}: {popoverContent.total}
                    </TableCell>
                  </TableRow>
                </TableBody>
              </Table>
            </TableContainer>
          </Typography>
        </Popover>
      }
      {children}
    </>
  );
};

export default WeeklyPopover;