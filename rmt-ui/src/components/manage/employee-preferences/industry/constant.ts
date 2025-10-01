import { SxProps } from "@mui/material";

export const industryMappingPreferenceName = {
  industry: "industry",
  subIndustry: "subIndustry",
  year_of_experience: "year_of_experience",
  description: "description",
};
export const BtnCancelSx: SxProps = {
  borderColor: "#725799",
  color:  "#725799",
  marginLeft: "20px",
  textTransform: "initial",
  "&:hover": {
    borderColor: "#4f2d7f !important",
    backgroundColor: "#fff !important",
    color: "#4f2d7f !important",
  },
  fontFamily: "GT Walsheim Pro, Medium",
  borderRadius: "10px !important",
};

export const BtnSaveSx: SxProps = {
  backgroundColor: "#725799",
  // marginLeft: "40px",
  textTransform: "initial",
  "&:hover": {
    borderColor: "#4f2d7f !important",
    backgroundColor: "#4f2d7f !important",
    color: "#fff !important",
  },
  fontFamily: "GT Walsheim Pro, Medium",
  borderRadius: "10px !important",
  marginLeft: "10px",
  color: "white",
  fontSize: "17px",
  border: "1px solid #725799",
};