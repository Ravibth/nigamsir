import { SxProps } from "@mui/material";

export const Autocomplete: SxProps = {
  width: 200,
  marginTop: "13px",
  "& .MuiInputBase-root": {
    height: "40px",
  },
};

export const BoxSxProps: SxProps = {
  marginBottom: "1px",
};

export const BoxSecondSxProps: SxProps = {
  display: "flex",
  alignItems: "flex-end",
  paddingBottom: "19px",
};

export const SearchIconSxProps: SxProps = {
  color: "action.active",
  mr: 1,
  my: 0.5,
};

export const ButtonGridSXProps: SxProps = {
  display: "flex",
  justifyContent: "flex-end !important",
};
