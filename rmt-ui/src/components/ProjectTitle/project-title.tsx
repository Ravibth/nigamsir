/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-unused-vars */
import { Fragment, useContext, useEffect, useState } from "react";
import MainTitle from "./main-title/MainTitle";
import * as Utils from "./utils";
import { ISubtitleProps } from "./Subtitle/interface/ISubtitleProps";
import Subtitle from "./Subtitle/subtitle";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import React from "react";

const ProjectTitle = (props: any) => {
  const { projectDetails } = props;
  const isEmployee = useContext(UserDetailsContext)?.isEmployee;
  const [titleData, setTitleData] = useState([] as any);
  useEffect(() => {
    getProjectTitles();
  }, []);

  const getProjectTitles = async () => {
    const titleData = Utils.GetSubtitleForEmployee(projectDetails);
    setTitleData((prevState: any) => [...titleData]);
  };

  const SubTitleProps: ISubtitleProps = {
    subtitleProperties: isEmployee
      ? Utils.GetSubtitleForEmployee(projectDetails)
      : Utils.GetSubtitleForRecruitor(projectDetails),
    jobPage: !isEmployee ? "https://www.grantthornton.in/" : undefined,
    budgetStatus: projectDetails?.budgetStatus,
    projectDetails: projectDetails,
  };

  return (
    <Fragment>
      <MainTitle {...props} />
      <Subtitle {...SubTitleProps} />
    </Fragment>
  );
};

export default ProjectTitle;
