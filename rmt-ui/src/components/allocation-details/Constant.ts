import { SxProps } from "@mui/material";

export const getTableRowDesign = () => {
  const sxProps: SxProps = {
    "&:last-child td, &:last-child th": { border: 0 },
  };
  return sxProps;
};
