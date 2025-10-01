import { SxProps } from "@mui/material";

export const DrawerSxProps: SxProps = {
  zIndex: 1300,
};

export const CloseButtonSxProps: SxProps = {
  borderColor: " #4f2d7f",
  color: "#b5a7c9",
  marginLeft: " 40px",
  textTransform: "initial",
};
export const ApplyFilterButtonSxProps: SxProps = {
  backgroundColor: "#4f2d7f",
  marginLeft: "40px",
  textTransform: "initial",
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
  backgroundColor: "rgb(246, 243, 243)",
};

export const filterIconButton: SxProps = () => {
  return {
    color: "#4f2d7f",
    // fontFamily: "GT Walsheim Pro ",
    fontSize: "14px",
    textTransform: "initial",
    borderRadius: "20px",
    borderColor: "#B8B8B8",
    height: "35px",
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
  { label: "Audit" },
  { label: "Consulting" },
  { label: "Taxation" },
];

export const designation = [
  { label: "Consultant" },
  { label: "Sr. Consultant" },
  { label: "Reviewer" },
];

export const sme = [{ label: "Support" }, { label: "Auditting" }];

export const clientName = [{ label: "GT" }, { label: "Adani" }];

export const pipelineCode = [{ title: "P001" }, { title: "P002" }];

export const jobCode = [{ title: "J001" }, { title: "J002" }];

export const jobName = [{ title: "J001" }, { title: "J002" }];

export const pipelineName = [{ title: "J001" }, { title: "J002" }];
