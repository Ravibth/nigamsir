import { Box, Button, ButtonGroup } from "@mui/material";
// import React, { useEffect, useState } from "react";
import * as constant from "./constant";

const ProjectTypesComp = (props: any) => {
  const { handleProjectTypeClick, selectProjectType } = props;
  const handleAllView = () => {
    // handleProjectTypeClick(
    //   constant.PROJECT_CHARGE_TYPE.ALL.toString().toUpperCase()
    // );
    handleProjectTypeClick(constant.PROJECT_TYPE.ALL.toString().toUpperCase());
  };
  // const handleChargeableView = () => {
  //   handleProjectTypeClick(
  //     constant.PROJECT_CHARGE_TYPE.CHARGABLE.toString().toUpperCase()
  //   );
  // };
  // const handleNonchargeableView = () => {
  //   handleProjectTypeClick(
  //     constant.PROJECT_CHARGE_TYPE.NON_CHARGABLE.toString().toUpperCase()
  //   );

  const handleOpenView = () => {
    handleProjectTypeClick(constant.PROJECT_TYPE.OPEN.toString());
  };
  const handleCloseView = () => {
    handleProjectTypeClick(constant.PROJECT_TYPE.CLOSE.toString());
  };
  return (
    <>
      <Box mt={2} mb={2} ml={0.5} mr={0.5}>
        <ButtonGroup
          className="rmt-prjtype-buttongrp"
          variant="outlined"
          aria-label="outlined button group"
          fullWidth
        >
          <Button
            className="rmt-prjtype-button"
            onClick={handleAllView}
            sx={constant.GetSxPropsForButton(selectProjectType, 0)}
          >
            All
          </Button>
          <Button
            className="rmt-prjtype-button"
            onClick={handleOpenView}
            sx={constant.GetSxPropsForButton(selectProjectType, 1)}
          >
            Open
          </Button>

          <Button
            className="rmt-prjtype-button"
            onClick={handleCloseView}
            sx={constant.GetSxPropsForButton(selectProjectType, 2)}
          >
            Closed
          </Button>
        </ButtonGroup>
      </Box>
    </>
  );
};

export default ProjectTypesComp;
