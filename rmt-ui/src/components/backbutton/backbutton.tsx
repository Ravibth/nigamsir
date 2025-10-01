import { Button } from "@mui/material";
import React from "react";
import * as GlobalConstant from "../../global/constant";
import { useNavigate } from "react-router-dom";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import "./style.css";

const BackButton = () => {
  const navigate = useNavigate();
  const navigateToBack = () => {
    navigate(-1);
  };
  return (
    <div className="btnDiv">
      <Button
        className="backbtn"
        sx={{ color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}
        startIcon={
          <ArrowBackIosNewIcon
            className="backarrow"
            sx={{
              color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
            }}
          />
        }
        onClick={navigateToBack}
      >
        Back
      </Button>
    </div>
  );
};

export default BackButton;
