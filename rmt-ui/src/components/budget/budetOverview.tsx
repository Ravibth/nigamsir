import { ToggleButton, ToggleButtonGroup } from "@mui/material";
import { useContext, useEffect, useState } from "react";

import "./BudgetOverview.css";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import BudgetDesignationTable from "./budgetDesignationTable";
import BudgetOverall from "./budgetOverall";
import BudgetResourceWiseGraph from "./budgetResourceWiseGraph";
import {
  getBudgetDesignationWise,
  getBudgetOverview,
  getProjectBudget,
  getResourceAllocationDayGroup,
  resourceActualPlannedGraph,
} from "../../services/budget/budget.service";
import * as _ from "lodash";
import moment from "moment";
import BudgetAllocatedActualGraph from "./budgetAllocatedActualGraph";
import { GetExpertiesConfigurationByExpertiesNameAndConfigGroup } from "../../services/configuration-services/configuration.service";
import { ConfigGroupEnum } from "../configurations/constant";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";
import { SnackbarContext } from "../../contexts/snackbarContext";
import BudgetWidget from "./budgetWidget";
import { errorMessage } from "../../services/log-services/log-service";
import { ProjectChargeableType } from "../project-types/constant";
import { getWCGTJobByJobCode } from "../../services/wcgt-master-services/wcgt-master-services";
import * as GC from "../../global/constant";
import {
  EBudgetFilterData,
  IBudgetFilterData,
} from "./budget-filter/budgetFilter";

const BudgetOverview = (props: any) => {
  const [toggleValue, setToggleValue] = useState<string>("cost");
  const [totalNoOfResource, setTotalNoOfResource] = useState<number>(0);
  const [jobFee, setJobFee] = useState<number>(0);
  const [overAllData, setOverAllData] = useState<any>({
    totalBudgetHrs: 0,
    totalBudgetCost: 0,
    totalOriginalBudgetCost: 0,
    totalOriginalBudgetHrs: 0,
    totalAllocatedhrs: 0,
    totalAllocatedCost: 0,
    totalTimesheetHrs: 0,
    totalTImesheetCost: 0,
  });
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);
  const [designationData, setDesignationData] = useState<any>();
  const [isBudgetNotAllocated, setIsBudgetNotAllocated] =
    useState<boolean>(true);
  const [resourceWiseGraphData, setResourceWiseGraphData] = useState<any>();
  const [resourceWiseFilterData, setResourceWiseFilterData] = useState<any>();
  const [appliedFilters, setAppliedFilters] = useState<any>();
  const [coldef, setColdefs] = useState<any>();
  const [budgetLimitConfig, setBudgetLimitConfig] = useState<number>(0);
  const [totalPlannedBudget, setTotalPlannedBudget] = useState<any>();
  const [jobBudget, setJobBudget] = useState<number>();
  const [initialPageLoaded, setInitialPageLoaded] = useState<boolean>(false);

  useEffect(() => {
    loaderContext.open(true);
    setInitialPageLoaded(false);
    createColDef(toggleValue);
    Promise.all([
      fetchPlannedActualGraph(
        props.projectDetails?.pipelineCode,
        props.projectDetails?.jobCode,
        "daily",
        moment(props?.projectDetails?.startDate).format(GC.dateFormatYMD),
        moment(props?.projectDetails?.endDate).format(GC.dateFormatYMD)
      ),

      fetchBudgetDesignationWise(
        props.projectDetails?.pipelineCode,
        props.projectDetails?.jobCode
      ),

      fetchBudgetOverallData(
        [
          {
            key: props.projectDetails?.pipelineCode,
            value: props.projectDetails?.jobCode,
          },
        ],
        moment(props?.projectDetails?.startDate).format(GC.dateFormatYMD),
        moment(props?.projectDetails?.endDate).format(GC.dateFormatYMD)
      ),

      fetchResourceWiseGraphData(
        props?.projectDetails?.pipelineCode,
        props.projectDetails?.jobCode,
        moment(props?.projectDetails?.startDate).format(GC.dateFormatYMD),
        moment(props?.projectDetails?.endDate).format(GC.dateFormatYMD)
      ),

      fetchConfigDefaultLimit(),

      fetchProjectBudget(
        props.projectDetails?.pipelineCode,
        props.projectDetails?.jobCode
      ),
    ])
      .then(() => {
        loaderContext.open(false);
        setInitialPageLoaded(true);
      })
      .catch(() => {
        loaderContext.open(false);
        setInitialPageLoaded(true);
      });
  }, []);

  useEffect(() => {
    createColDef(toggleValue);
    if (
      props.projectDetails?.chargableType ==
        ProjectChargeableType.NonChargable &&
      toggleValue == "hours"
    ) {
      setIsBudgetNotAllocated(true);
    } else if (
      props.projectDetails?.chargableType ===
        ProjectChargeableType.NonChargable &&
      toggleValue === "cost" &&
      jobBudget
    ) {
      setIsBudgetNotAllocated(false);
    }
  }, [toggleValue, isBudgetNotAllocated]);

  //No usage found
  // const calculateOverAll = (designationDataP: Array<any>) => {
  //   let totalBudgetHrs = 0;
  //   let totalBudgetCost = 0;
  //   let totalAllocatedhrs = 0;
  //   let totalAllocatedCost = 0;
  //   let totalTimesheetHrs = 0;
  //   let totalTImesheetCost = 0;

  //   designationDataP.forEach((element) => {
  //     totalBudgetHrs += element.budgetHrs;
  //     totalBudgetCost += element.budgetCost;
  //     totalAllocatedhrs += element.allocatedHrs;
  //     totalAllocatedCost += element.allocatedCost;
  //     totalTimesheetHrs += element.timesheetHrs;
  //     totalTImesheetCost += element.timesheetCost;
  //   });
  // };

  // Date Change
  const dateRangeChange = async (startDate, endDate) => {
    if (startDate && endDate) {
      loaderContext.open(true);
      await fetchResourceWiseGraphData(
        props?.projectDetails?.pipelineCode,
        props.projectDetails?.jobCode,
        moment(startDate).format(GC.dateFormatYMD),
        moment(endDate).format(GC.dateFormatYMD)
      );
      loaderContext.open(false);
      if (resourceWiseFilterData?.length) {
        selectedDataByFilter(appliedFilters);
      }
    }
  };

  const currencyFormatter = (currency) => {
    var sansDec = currency?.toFixed(0);
    var formatted = sansDec.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return `${formatted}`;
  };

  const createColDef = (toggleValue) => {
    const columnDefs: any = [
      {
        headerName: "Grade",
        field: "grade",
        flex: 0.75,
        minWidth: 200,
        sort: "asc",
        sortable: true,
        tooltipField: "grade",
      },
      {
        headerName: "Budget(Hours)",
        field: "budgetHrs",
        flex: 0.75,
        minWidth: 200,
        sortable: true,
        tooltipField: "Budget(Hours)",
        hide: toggleValue == "cost" ? true : false,
        valueFormatter: (params) =>
          isBudgetNotAllocated ||
          props.projectDetails?.chargableType ==
            ProjectChargeableType.NonChargable
            ? "-"
            : currencyFormatter(params.data.budgetHrs),
      },
      {
        headerName: "Budget(Cost)",
        field: "budgetCost",
        flex: 0.75,
        minWidth: 200,
        sortable: true,
        tooltipField: "Budget(Cost)",
        hide: toggleValue == "cost" ? false : true,
        valueFormatter: (params) =>
          isBudgetNotAllocated ||
          props.projectDetails?.chargableType ==
            ProjectChargeableType.NonChargable
            ? "-"
            : currencyFormatter(params.data.budgetCost),
      },
      {
        headerName: "Original Budget(Hours)",
        field: "originalBudgetHrs",
        flex: 0.75,
        minWidth: 200,
        sortable: true,
        tooltipField: "Original Budget(Hours)",
        hide: toggleValue === "cost" ? true : false,
        valueFormatter: (params) =>
          isBudgetNotAllocated ||
          props.projectDetails?.chargableType ===
            ProjectChargeableType.NonChargable
            ? "-"
            : currencyFormatter(params.data.originalBudgetHrs),
      },
      {
        headerName: "Original Budget(Cost)",
        field: "originalBudgetCost",
        flex: 0.75,
        minWidth: 200,
        sortable: true,
        tooltipField: "Original Budget(Cost)",
        hide: toggleValue === "cost" ? false : true,
        valueFormatter: (params) =>
          isBudgetNotAllocated ||
          props.projectDetails?.chargableType ===
            ProjectChargeableType.NonChargable
            ? "-"
            : currencyFormatter(params.data.originalBudgetCost),
      },
      {
        headerName: "Allocated (Cost)",
        field: "allocatedCost",
        flex: 0.75,
        minWidth: 200,
        sortable: true,
        tooltipField: "Allocated(Cost)",
        hide: toggleValue == "cost" ? false : true,
        valueFormatter: (params) =>
          currencyFormatter(params.data.allocatedCost),
      },
      {
        headerName: "Allocated (Hours)",
        field: "allocatedHrs",
        flex: 0.75,
        minWidth: 200,
        sortable: true,
        tooltipField: "Allocated(Hours)",
        hide: toggleValue == "cost" ? true : false,
        valueFormatter: (params) => currencyFormatter(params.data.allocatedHrs),
      },
      {
        headerName: "Actual(Cost)",
        field: "timesheetCost",
        flex: 0.75,
        minWidth: 200,
        sortable: true,
        tooltipField: "Actual(Cost)",
        hide: toggleValue == "cost" ? false : true,
        valueFormatter: (params) =>
          currencyFormatter(params.data.timesheetCost),
      },
      {
        headerName: "Actual(Hours)",
        field: "timesheetHrs",
        flex: 0.75,
        minWidth: 200,
        sortable: true,
        tooltipField: "Actual(Hours)",
        hide: toggleValue == "cost" ? true : false,
        valueFormatter: (params) => currencyFormatter(params.data.timesheetHrs),
      },
    ];
    setColdefs(columnDefs);
  };

  // Done
  const setPlannedActualOption = async (value: string, startDate, endDate) => {
    loaderContext.open(true);
    await Promise.all([
      fetchPlannedActualGraph(
        props.projectDetails?.pipelineCode,
        props.projectDetails?.jobCode,
        value,
        moment(startDate).format(GC.dateFormatYMD),
        moment(endDate).format(GC.dateFormatYMD)
      ),
    ]);
    loaderContext.open(false);
  };

  const quarterlyData = (data: any) => {
    data.forEach((element) => {
      let key = element?.key;
      if (element?.key?.includes("-")) {
        const part = key.split("-");
        element["year"] = part[1];
        element["month"] = part[0];
      } else {
        const part = key.split("/");
        element["year"] = part[1];
        element["month"] = part[0];
      }
    });
    const groupedData = _.groupBy(data, "year");
    const sorted = _.sortBy(groupedData, "month");
    sorted.forEach((group) => {
      group.forEach((element) => {
        const month = parseInt(element["month"]);
        if (month <= 3) {
          //quarter 1
          element["key"] = "Q1-" + element["year"];
        } else if (month >= 4 && month < 6) {
          //quarter 2
          element["key"] = "Q4-" + element["year"];
        } else if (month > 6 && month <= 9) {
          //quarter 3
          element["key"] = "Q4-" + element["year"];
        } else if (month > 9 && month <= 12) {
          //quarter 4
          element["key"] = "Q4-" + element["year"];
        }
      });
    });

    const flatData = sorted.flat(1);

    const result = [];
    const allocatedData = flatData.reduce(function (res, value) {
      if (!res[value?.key]) {
        res[value?.key] = {
          key: value?.key,
          totalAllocationCost: 0,
          totalAllocationTime: 0,
          totalTimesheetCost: 0,
          totalTimesheetTime: 0,
        };
        result.push(res[value?.key]);
      }

      res[value.key].totalAllocationCost += value?.totalAllocationCost;
      res[value.key].totalAllocationTime += value?.totalAllocationTime;
      res[value.key].totalTimesheetCost += value?.totalTimesheetCost;
      res[value.key].totalTimesheetTime += value?.totalTimesheetTime;

      return res;
    }, {});
    const allocatedPlanned = Object.values(allocatedData);
    setTotalPlannedBudget(allocatedPlanned);
  };

  const fetchPlannedActualGraph = (
    pipelineCode: string,
    jobCode: string,
    timeOption: string,
    startDate: string,
    endDate: string
  ): Promise<any> => {
    return Promise.all([
      fetchPlannedAllocationGraph(
        pipelineCode,
        jobCode,
        timeOption,
        startDate,
        endDate
      ),
    ])
      .then((response) => {
        if (timeOption === "quarterly") {
          quarterlyData(response[0]);
        } else if (timeOption === "monthly") {
          const data = response[0];
          data.forEach((element) => {
            let key = element?.key;
            if (element?.key?.includes("-")) {
              const part = key.split("-");
              element["year"] = part[1];
              element["month"] = part[0];
            } else {
              const part = key.split("/");
              element["year"] = part[1];
              element["month"] = part[0];
            }
          });
          const groupedData = _.groupBy(data, "year");
          const sorted = _.sortBy(groupedData, "month");
          const flatData = sorted.flat(1);
          setTotalPlannedBudget(flatData);
        } else {
          setTotalPlannedBudget(response[0]);
        }
        return "";
      })
      .catch((ex) => {
        snackbarContext.displaySnackbar(
          "Error fetching data! Please re-try after some time. Please contact System Administrator if issue persists.",
          "error"
        );
        errorMessage("Budget", "fetchPlannedActualGraph", ex);
        return "";
      });
  };

  const fetchPlannedAllocationGraph = (
    pipelineCode: string,
    jobCode: string,
    timeOption: string,
    startDate: string,
    endDate: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      getResourceAllocationDayGroup(
        pipelineCode,
        jobCode,
        timeOption === "quarterly" ? "monthly" : timeOption,
        startDate,
        endDate
      )
        .then((planned) => {
          if (timeOption === "quarterly" || timeOption === "monthly") {
            const sorted = _.orderBy(planned.data, "key", "asc");
            resolve(sorted);
          } else {
            planned?.data?.forEach((element) => {
              element.key = new Date(element?.key);
            });
            const sorted = _.orderBy(planned.data, "key", "asc");
            planned?.data?.forEach((element) => {
              element.key = moment(element.key).format("DD-MM-YY");
            });
            resolve(sorted);
          }
        })
        .catch((ex) => {
          snackbarContext.displaySnackbar(
            "Error fetching data! Please re-try after some time. Please contact System Administrator if issue persists.",
            "error"
          );
          errorMessage("Budget", "fetchPlannedAllocationGraph", ex);
          reject(true);
        });
    });
  };

  const fetchBudgetDesignationWise = (
    pipelineCode: string,
    jobCode: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      getBudgetDesignationWise(pipelineCode, jobCode)
        .then((budget) => {
          // calculateOverAll(budget.data);
          setDesignationData(budget.data);
          resolve(true);
        })
        .catch((ex) => {
          snackbarContext.displaySnackbar(
            "Error fetching data! Please re-try after some time. Please contact System Administrator if issue persists.",
            "error"
          );
          errorMessage("Budget", "fetchBudgetDesignationWise", ex);
          resolve(true);
        });
    });
  };

  const fetchProjectBudget = async (pipelineCode: string, jobCode: string) => {
    if (
      props.projectDetails?.chargableType == ProjectChargeableType.Chargable
    ) {
      await getProjectBudget(pipelineCode, jobCode)
        .then((budget) => {
          const isBudget =
            budget?.data && budget?.data?.length > 0 ? true : false;
          setIsBudgetNotAllocated(!isBudget);
        })
        .catch((ex) => {
          snackbarContext.displaySnackbar(
            "Error fetching data! Please re-try after some time. Please contact System Administrator if issue persists.",
            "error"
          );
          errorMessage("Budget", "getProjectBudget", ex);
        });
    } else {
      //Non Chargeable data will come from wcgt Jobs
      await getWCGTJobByJobCode(pipelineCode, jobCode)
        .then((jobs) => {
          if (jobs && jobs?.jobBudgetValue && toggleValue === "cost") {
            setIsBudgetNotAllocated(false);
            setJobBudget(jobs?.jobBudgetValue);
          } else {
            setIsBudgetNotAllocated(true);
          }
        })
        .catch((ex) => {
          snackbarContext.displaySnackbar(
            "Error fetching data! Please re-try after some time. Please contact System Administrator if issue persists.",
            "error"
          );
          errorMessage("Budget", "getWCGTJobByJobCode", ex);
        });
    }
    return true;
  };

  const fetchBudgetOverallData = (
    jobCode: any,
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
          setTotalNoOfResource(
            overall?.data[0]?.totalResourcesCount
              ? overall?.data[0]?.totalResourcesCount
              : 0
          );
          setJobFee(overall?.data[0]?.jobFee ? overall?.data[0]?.jobFee : 0);
          let totalBudgetHrs = overall?.data[0]?.budgetedHrs
            ? overall?.data[0]?.budgetedHrs
            : 0;
          let totalBudgetCost = overall?.data[0]?.budgetedCost
            ? overall?.data[0]?.budgetedCost
            : 0;

          let totalOriginalBudgetCost = overall?.data[0]?.originalBudgetCost
            ? overall?.data[0]?.originalBudgetCost
            : 0;
          let totalOriginalBudgetHrs = overall?.data[0]?.originalBudgetHrs
            ? overall?.data[0]?.originalBudgetHrs
            : 0;
          let totalAllocatedhrs = overall?.data[0]?.totalAllocatedHours
            ? overall?.data[0]?.totalAllocatedHours
            : 0;
          let totalAllocatedCost = overall?.data[0]?.totalAllocatedCost
            ? overall?.data[0]?.totalAllocatedCost
            : 0;
          let totalTimesheetHrs = overall?.data[0]?.consumedHours
            ? overall?.data[0]?.consumedHours
            : 0;
          let totalTImesheetCost = overall?.data[0]?.consumnedCost
            ? overall?.data[0]?.consumnedCost
            : 0;

          setOverAllData({
            totalBudgetHrs: totalBudgetHrs,
            totalBudgetCost: totalBudgetCost,
            totalOriginalBudgetCost: totalOriginalBudgetCost,
            totalOriginalBudgetHrs: totalOriginalBudgetHrs,
            totalAllocatedhrs: totalAllocatedhrs,
            totalAllocatedCost: totalAllocatedCost,
            totalTimesheetHrs: totalTimesheetHrs,
            totalTImesheetCost: totalTImesheetCost,
          });

          resolve(true);
        })
        .catch((ex) => {
          snackbarContext?.displaySnackbar(
            "Error fetching data! Please re-try after some time. Please contact System Administrator if issue persists.",
            "error"
          );
          errorMessage("Budget", "fetchBudgetOverallData", ex);
          resolve(true);
        });
    });
  };

  const fetchResourceWiseGraphData = (
    pipelineCode: string,
    jobCode: string,
    start_date: string,
    end_date: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      resourceActualPlannedGraph(pipelineCode, jobCode, start_date, end_date)
        .then((resource) => {
          const sorted = _.sortBy(resource.data, "allocationmonthname");
          setResourceWiseGraphData(sorted);
          resolve(true);
        })
        .catch((ex) => {
          snackbarContext?.displaySnackbar(
            "Error fetching data! Please re-try after some time. Please contact System Administrator if issue persists.",
            "error"
          );
          errorMessage("Budget", "fetchResourceWiseGraphData", ex);
          resolve(true);
        });
    });
  };

  const selectedDataByFilter = (data: IBudgetFilterData) => {
    setAppliedFilters(data);
    const filteredResourceData = resourceWiseGraphData?.filter(function (el) {
      return (
        el?.identityUserResponse?.designation ===
          data[EBudgetFilterData.designation] ||
        el?.identityUserResponse?.competency ===
          data[EBudgetFilterData.competency] ||
        el?.identityUserResponse?.location ===
          data[EBudgetFilterData.location] ||
        el?.identityUserResponse?.grade === data[EBudgetFilterData.grade] ||
        el?.identityUserResponse?.businessUnit ===
          data[EBudgetFilterData.businessUnit]
      );
    });
    setResourceWiseFilterData(filteredResourceData);
  };

  const fetchConfigDefaultLimit = async () => {
    try {
      const fetchedConfigs: any =
        await GetExpertiesConfigurationByExpertiesNameAndConfigGroup(
          props?.projectDetails?.expertise,
          ConfigGroupEnum.BUDGET_CONSUMED_LIMIT
        );
      const configs: any = {};
      fetchedConfigs.data.forEach((item: any) => {
        configs[item.configurationGroup.configKey] = parseInt(
          item.attributeValue
        );
      });
      setBudgetLimitConfig(
        parseInt(configs[ConfigGroupEnum.BUDGET_CONSUMED_LIMIT])
      );
      return true;
    } catch (err) {
      snackbarContext.displaySnackbar("Error fetching Configurations", "error");
      return true;
    }
  };

  return (
    <>
      <div className="budget-container">
        <div className="budget-heading-overview">
          <span className="budget-toggle">
            <ToggleButtonGroup
              value={toggleValue}
              exclusive
              onChange={(e: any) => {
                setToggleValue(e.target?.value?.toString());
                createColDef(e.target?.value?.toString());
              }}
              aria-label="text alignment"
            >
              <ToggleButton value="cost" aria-label="left aligned">
                Cost(INR)
              </ToggleButton>
              <ToggleButton value="hours" aria-label="centered">
                Time(Hours)
              </ToggleButton>
            </ToggleButtonGroup>
          </span>
        </div>
        {initialPageLoaded && (
          <>
            <div className="budget-widget">
              <BudgetWidget
                overAllData={overAllData}
                toggleValue={toggleValue}
                totalNoOfResource={totalNoOfResource}
                budgetLimitConfig={budgetLimitConfig}
                jobFee={jobFee}
                isBudgetNotAllocated={isBudgetNotAllocated}
              />
            </div>
            <div className="budget-overview-container">
              <BudgetOverall
                overAllData={overAllData}
                toggleValue={toggleValue}
                totalNoOfResource={totalNoOfResource}
                budgetLimitConfig={budgetLimitConfig}
                jobFee={jobFee}
                isBudgetNotAllocated={isBudgetNotAllocated}
              />
              <BudgetDesignationTable
                coldef={coldef}
                designationData={designationData}
                toggleValue={toggleValue}
                isBudgetNotAllocated={isBudgetNotAllocated}
              />
            </div>
            <div className="graph-container">
              <Card
                className="graph"
                style={{
                  border: "1px solid lightgray",
                  borderRadius: "10px",
                }}
              >
                <CardContent>
                  <BudgetAllocatedActualGraph
                    toggleValue={toggleValue}
                    totalPlannedBudget={totalPlannedBudget}
                    setPlannedActualOption={setPlannedActualOption}
                    projectDetails={props?.projectDetails}
                  />
                </CardContent>
              </Card>
              <Card
                className="graph"
                style={{
                  border: "1px solid lightgray",
                  borderRadius: "10px",
                }}
              >
                <CardContent>
                  <BudgetResourceWiseGraph
                    toggleValue={toggleValue}
                    projectDetails={props?.projectDetails}
                    resourceWiseGraphData={resourceWiseGraphData}
                    dateRangeChange={dateRangeChange}
                    appliedFilters={appliedFilters}
                    selectedDataByFilter={selectedDataByFilter}
                    resourceWiseFilterData={resourceWiseFilterData}
                  />
                </CardContent>
              </Card>
            </div>
          </>
        )}
      </div>
    </>
  );
};

export default BudgetOverview;
