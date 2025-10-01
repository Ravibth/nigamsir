import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";

export const DrawerSxProps: SxProps = {
  zIndex: 1300,
};

export const CloseButtonSxProps: SxProps = {
  borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  marginLeft: "20px",
  textTransform: "initial",
  "&:hover": {
    borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  },
  fontFamily: GT_DESIGN_PARAMETERS.GtFontFamily,
  borderRadius: "10px !important",
  padding: "4px 25px !important",
};
export const ApplyFilterButtonSxProps: SxProps = {
  backgroundColor: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  // marginLeft: "40px",
  textTransform: "initial",
  "&:hover": {
    borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColor,
    backgroundColor: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
  fontFamily: GT_DESIGN_PARAMETERS.GtFontFamily,
  borderRadius: "10px !important",
  padding: "0px 25px !important",
};

export const DividerSxProps: SxProps = {
  borderBottomWidth: 2,
  margin: "10px",
};

export const TypographySxProps: SxProps = {
  color: "black",
  fontSize: "14px",
};

export const AutocompleteSxProps: SxProps = {
  width: "300px",
  backgroundColor: "#F2F5FF !important",
};

export const filterIconButton: SxProps = () => {
  return {
    color: "#4f2d7f",
    fontSize: "14px",
    textTransform: "initial",
    borderRadius: "40px",
    borderColor: "#4f2d7f",
    "&:hover": {
      borderColor: "#4f2d7f",
      // border: "2px solid",
      backgroundColor: "#E9F7FB",
    },
  };
};

export const styleModel: SxProps = () => {
  return {
    position: "absolute" as "absolute",
    top: "52%",
    left: "15%",
    transform: "translate(-50%, -50%)",
    width: 390,
    bgcolor: "background.paper",
    border: "2px solid #000",
    boxShadow: 24,
    p: 4,
  };
};

export const top100Films = [
  { title: "The Shawshank Redemption", year: 1994 },
  { title: "The Godfather", year: 1972 },
  { title: "The Godfather: Part II", year: 1974 },
];

export const experties = [
  { title: "Corporate intelligence" },
  { title: "DGTL" },
];

export const sme = [
  { title: "Corporate intelligence North" },
  { title: "Corporate intelligence South" },
];

export const clientName = [{ title: "GT" }, { title: "Adani" }];

export const pipelineCode = [{ title: "P001" }, { title: "P002" }];

export const jobCode = [{ title: "J001" }, { title: "J002" }];

export const jobName = [{ title: "J001" }, { title: "J002" }];

export const pipelineName = [{ title: "J001" }, { title: "J002" }];
