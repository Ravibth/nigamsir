import { Box, Card, CardContent, Grid } from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import ViewItems from "./view-item/view-items";
import { IProjectViewProps } from "./IProjectViewProps";
import { useNavigate } from "react-router-dom";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import ActionButton from "../actionButton/actionButton";
import "./style.css";
import { getBudgetOverview } from "../../services/budget/budget.service";
import { GetProjectDetailsAsPerPipelineCodeAndUserRole } from "../../services/project-list-services/project-list-services";
import { GetPropertyValueByTitle, ProjectViewDetails } from "./constant";
import BackActionButton from "../actionButton/backactionButton";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";
import { TabsTitleEnum } from "../requestor-view/constant";
import { routeValueEncode } from "../../global/utils";
import { ProjectChargeableType } from "../project-types/constant";
import {
  DATE_TIME_INITIAL_VALUE,
  ProjectCategories,
} from "../../global/constant";
import { getProjectViewFieldsJson } from "../ProjectDetail/role-based-view-field";
import * as GC from "../../global/constant";
import moment from "moment";
import { getDateWithoutTime } from "../../utils/date/dateHelper";

const ProjectView = (props: IProjectViewProps) => {
  const navigate = useNavigate();

  const [projectData, setProjectData] = useState<ProjectViewDetails | null>();
  const navigateToProjectDetails = () =>
    navigate(
      `/project-details/${routeValueEncode(
        props.PipelineCode
      )}/${routeValueEncode(props.jobCode)}?tab=${TabsTitleEnum.DetailView}`
    );
  const { isEmployee } = React.useContext(UserDetailsContext);
  const [budgetStatus, setBudgetStatus] = useState<string>();
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [projectDetailViewTitles, setProjectDetailViewTitles] = useState([]);

  const {
    PipelineCode,
    StartDate,
    EndDate,
    closeHandler,
    jobCode,
    isLoading,
    setIsLoading,
    role,
  } = props;

  const fetchBudgetOverallData = (
    jobCode: Array<any>,
    startDate: string,
    endDate: string
  ): Promise<any> => {
    const request = {
      JobCodes: jobCode,
      StartDate: startDate,
      EndDate: endDate,
    };
    return new Promise((resolve, reject) => {
      getBudgetOverview(request)
        .then((overall) => {
          const status =
            overall?.data[0]?.percentageCost <= 100
              ? " In-Budget"
              : "Out-Budget";
          setBudgetStatus(status);
          resolve(overall);
        })
        .catch(() => {
          reject("");
        });
    });
  };

  const FetchProjectDetails = (pipelineCode: string, jobCode: string) => {
    return new Promise((resolve, reject) => {
      GetProjectDetailsAsPerPipelineCodeAndUserRole(pipelineCode, jobCode)
        .then((response) => {
          if (response.data.jobCode != null) {
            if (
              response.data.chargableType.toLowerCase() ==
              ProjectChargeableType.Chargable.toLowerCase()
            ) {
              getProjectViewFields(ProjectCategories.Chargeable);
            } else {
              getProjectViewFields(ProjectCategories.NonChargeable);
            }
          } else {
            getProjectViewFields(ProjectCategories.Pipeline);
          }
          resolve(response.data);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  const getProjectViewFields = (projectType: string) => {
    const _field = getProjectViewFieldsJson(role[0], projectType);
    setProjectDetailViewTitles(_field);
  };

  useEffect(() => {
    loaderContext.open(true);
    setIsLoading(true);
    Promise.all([FetchProjectDetails(PipelineCode, jobCode)])
      .then((values) => {
        loaderContext.open(false);
        setIsLoading(false);
        const projectData: any = values[0];
        let projectInformation = {
          ...projectData,
          jobId: jobCode,
        } as ProjectViewDetails;
        setProjectData(projectInformation);
        Promise.all([
          fetchBudgetOverallData(
            [{ key: PipelineCode, value: jobCode }],
            moment(props?.StartDate).format(GC.dateFormatYMD),
            moment(props?.EndDate).format(GC.dateFormatYMD)
          ),
        ]).then((budgetValues) => {
          if (budgetValues) {
            //@ts-ignore
            projectInformation["Budget"] = budgetValues[0]?.data[0]
              ?.budgetedCost
              ? budgetValues[0]?.data[0]?.budgetedCost
              : 0;
            setProjectData(projectInformation);
          }
        });
      })
      .catch((err) => {
        setIsLoading(false);
        loaderContext.open(false);
        throw err;
      });
  }, [PipelineCode, jobCode, StartDate, EndDate]);

  const closeClickHandler = () => {
    closeHandler();
  };
  const currentDate = new Date();
  // console.log(getDateWithoutTime(EndDate));
  // console.log(getDateWithoutTime(currentDate));
  return !isLoading ? (
    <Box
      position="relative"
      display="flex"
      flexDirection="column"
      padding="0px 20px"
    >
      <Grid item>
        <Grid container justifyContent={"center"} p={1.3}>
          {EndDate &&
            EndDate.toString() != DATE_TIME_INITIAL_VALUE &&
            getDateWithoutTime(currentDate) > getDateWithoutTime(EndDate) && (
              <span className="end-date-warning">
                {` * Project has crossed the end date`}
              </span>
            )}
          {EndDate?.toString() == DATE_TIME_INITIAL_VALUE && (
            <span className="end-date-warning">{` * End Date missing!!`}</span>
          )}
        </Grid>

        <Grid item xs={12}>
          <Card style={{ border: "1px solid lightgray" }}>
            <CardContent className="card-content-main">
              <Grid item xs={12}>
                <Grid container columns={{ lg: 6, sm: 12, xs: 6 }} spacing={3}>
                  {projectDetailViewTitles.map((title, index) => {
                    let Value = GetPropertyValueByTitle(title, projectData);
                    return (
                      <Grid
                        key={index}
                        item
                        lg={!isEmployee ? 2 : 2}
                        sm={3}
                        xs={3}
                      >
                        <ViewItems
                          title={title}
                          value={
                            Value
                              ? Value === ProjectCategories.InBudget
                                ? ProjectCategories.InBudget
                                : Value === ProjectCategories.OverBudget ||
                                  Value === ProjectCategories.OutBudget ||
                                  Value === ProjectCategories.BudgetExceeded
                                ? ProjectCategories.OverBudget
                                : Value
                              : " - "
                          }
                        ></ViewItems>
                      </Grid>
                    );
                  })}
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} sx={{ marginBottom: "20px", marginTop: "20px" }}>
          <Grid container item xs={12} className="main-pop-container">
            <Grid item xs={3} className="view-more-btn">
              <ActionButton
                label={"View More details"}
                type="submit"
                disabled={false}
                onClick={(e: any) => {
                  navigateToProjectDetails();
                }}
              />
            </Grid>
            <Grid item xs={2}>
              <BackActionButton label={"Close"} onClick={closeClickHandler} />
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </Box>
  ) : (
    <></>
  );
};

export default ProjectView;
