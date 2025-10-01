import { SxProps } from "@mui/material";

export const getSchedulerColor = (color: string) => {
  const sxProps: SxProps = {
    height: "30px",
    width: "30px",
    borderRadius: "50%",
    backgroundColor: `${color}`,
  };
  return sxProps;
};
