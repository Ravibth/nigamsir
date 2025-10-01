import { Box, Tab, Tabs, Button } from "@mui/material";
import CustomTabPanel from "./tab-panel/custom-tab-panel";
import { useEffect, useState, useContext } from "react";
import DescriptionLayout from "../project-details-layout/projectdescriptionlayout";
import CalendarView from "../calendar-view/calendar-view";
import * as Constant from "./constant";
import RequestorCalendarView from "./requestor-calendar-view/requestor-calendar-view";
import "./style.css";
import { TabLabelSXProps, TabsTitleEnum } from "./constant";
import ActiveRequisitionsDeatils from "../activeRequisitionsDeatils/activeRequisitionsDeatils";
import Activeallocation from "../activeallocation/activeallocation";
import ActionButton from "../actionButton/actionButton";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";
import MarketplaceInterests from "../Marketplace/MarketplaceInterests/MarketplaceInterests";
import BudgetOverview from "../budget/budetOverview";
import React from "react";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { MODULE_NAME_ENUM } from "../../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../../common/access-control-guard/access-control";
import {
  IsPermissionExistForProject,
  routeValueEncode,
} from "../../global/utils";
import {
  SnackbarContext,
  SnackbarContextProps,
} from "../../contexts/snackbarContext";
import { isShowHideProjectDetailsTab } from "./util";
import { getBUTreeMappingListByMID } from "../../services/configuration-services/configuration.service";
import { getAllCompetencyByMID } from "../../services/wcgt-master-services/wcgt-master-services";
import { IsLeader } from "../Reports/util";

const RequestorView = (props: any) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const tab = searchParams.get("tab");
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);

  const [showBudgetTab, setShowBudgetTab] = useState(false);
  const [value, setValue] = useState(
    tab != undefined && tab != "0" ? parseInt(tab) : 0
  );
  const navigate = useNavigate();
  const {
    projectDetails,
    handleTabChange,
    setDelegateDBData,
    delegateDBData,
    additionalElDBData, //WIP
    setadditionalElDBData, //WIP
    refreshHeader,
  } = props;
  const userContext = React.useContext(UserDetailsContext);
  const handleChange = (event: any, newValue: number) => {
    snackbarContext.closeSnackbar();
    var tabValue;
    switch (event.target.textContent) {
      case "Detail View":
        // setValue(TabsTitleEnum.DetailView);
        refreshHeader();
        navigate(
          `/project-details/${routeValueEncode(
            props.projectDetails.pipelineCode
          )}/${routeValueEncode(props.projectDetails.jobCode)}?tab=${
            TabsTitleEnum.DetailView
          }`
        );
        tabValue = TabsTitleEnum.DetailView;
        break;
      case "Calendar View":
        // setValue(TabsTitleEnum.CalenderView);
        navigate(
          `/project-details/${routeValueEncode(
            props.projectDetails.pipelineCode
          )}/${routeValueEncode(props.projectDetails.jobCode)}?tab=${
            TabsTitleEnum.CalenderView
          }`
        );
        tabValue = TabsTitleEnum.CalenderView;
        break;
      case "Marketplace Interests":
        // setValue(TabsTitleEnum.MarketplaceInterests);
        navigate(
          `/project-details/${routeValueEncode(
            props.projectDetails.pipelineCode
          )}/${routeValueEncode(props.projectDetails.jobCode)}?tab=${
            TabsTitleEnum.MarketplaceInterests
          }`
        );
        tabValue = TabsTitleEnum.MarketplaceInterests;
        break;
      case "Budget Details":
        // setValue(TabsTitleEnum.BudgetDetails);
        navigate(
          `/project-details/${routeValueEncode(
            props.projectDetails.pipelineCode
          )}/${routeValueEncode(props.projectDetails.jobCode)}?tab=${
            TabsTitleEnum.BudgetDetails
          }`
        );
        tabValue = TabsTitleEnum.BudgetDetails;
        break;
      case "Allocations":
        // setValue(TabsTitleEnum.Allocations);
        navigate(
          `/project-details/${routeValueEncode(
            props.projectDetails.pipelineCode
          )}/${routeValueEncode(props.projectDetails.jobCode)}?tab=${
            TabsTitleEnum.Allocations
          }`
        );
        tabValue = TabsTitleEnum.Allocations;
        break;
      case "Requisitions":
        // setValue(TabsTitleEnum.Requisitions);
        navigate(
          `/project-details/${routeValueEncode(
            props.projectDetails.pipelineCode
          )}/${routeValueEncode(props.projectDetails.jobCode)}?tab=${
            TabsTitleEnum.Requisitions
          }`
        );
        tabValue = TabsTitleEnum.Requisitions;
        break;
    }
    // setValue(newValue);
    // setSearchParams((params) => {
    //   params.delete("tab");
    //   return params;
    // });
    handleTabChange(tabValue);
  };
  useEffect(() => {
    if ((tab == undefined || tab == null) && props.navigationState?.tab) {
      setValue(props.navigationState?.tab);
      handleTabChange(props.navigationState?.tab);
    } else if (tab) {
      //todo: will check later
      const _tabid = tab ? parseInt(tab) : 0;
      setValue(_tabid);
      handleTabChange(_tabid);
    }
  }, [tab]);

  useEffect(() => {
    updateBudgetTabVisibility();
  }, [projectDetails, userContext]);

  const updateBudgetTabVisibility = async () => {
    //check applicable only for leader
    let flag = false;
    let isUserLeader = IsLeader(userContext);
    if (isUserLeader) {
      let resBuTree = await getBUTreeMappingListByMID(userContext.employee_id);
      let resComp = await getAllCompetencyByMID(userContext.employee_id);
      const currentUserBUs = Object.values(resBuTree?.data?.bu);
      const currentUserOfferings = Object.values(resBuTree?.data?.offerings);
      const currentUserSolutions = Object.values(resBuTree?.data?.solutions);

      const isMatchCompetency =
        resComp.filter(
          (c) =>
            props.projectDetails.projectCompetencies.filter(
              (pc) => pc.competency == c.competency
            )?.length > 0
        )?.length > 0;

      const isMatchBu =
        currentUserBUs.filter(
          (b: string) =>
            b?.toLowerCase().trim() ===
            props.projectDetails?.bu?.toLowerCase().trim()
        )?.length > 0;
      const isMatchOffering =
        currentUserOfferings.filter(
          (b: string) =>
            b?.toLowerCase().trim() ===
            props.projectDetails?.offerings?.toLowerCase().trim()
        )?.length > 0;
      const isMatchSolution =
        currentUserSolutions.filter(
          (b: string) =>
            b?.toLowerCase().trim() ===
            props.projectDetails?.solutions?.toLowerCase().trim()
        )?.length > 0;

      if (
        isMatchBu ||
        isMatchOffering ||
        isMatchSolution ||
        isMatchCompetency
      ) {
        flag = true;
      }
    } else {
      flag = true;
    }
    setShowBudgetTab(flag);
  };

  return (
    <Box className="tab-content-container">
      <Box
        className={"tab-container"}
        sx={{ borderBottom: 1, borderColor: "divider" }}
      >
        <Tabs
          value={value} 
          onChange={handleChange}
          aria-label="basic tabs example"
        >
          <Tab
            sx={isShowHideProjectDetailsTab(
              userContext,
              MODULE_NAME_ENUM.Project_Details
            )}
            label="Detail View"
          />

          <Tab
            sx={isShowHideProjectDetailsTab(
              userContext,
              MODULE_NAME_ENUM.Project_Calender
            )}
            label="Calendar View"
          />
          <Tab
            sx={isShowHideProjectDetailsTab(
              userContext,
              MODULE_NAME_ENUM.Project_Budget,
              PERMISSION_TYPE.Read,
              projectDetails
            )}
            label="Budget Details"
            style={projectDetails?.jobCode == null ? { display: "none" } : {}}
          />

          <Tab
            sx={isShowHideProjectDetailsTab(
              userContext,
              MODULE_NAME_ENUM.Allocation
            )}
            label="Allocations"
          />
          <Tab
            sx={isShowHideProjectDetailsTab(
              userContext,
              MODULE_NAME_ENUM.Requisition
            )}
            label="Requisitions"
          />
          <Tab
            sx={isShowHideProjectDetailsTab(
              userContext,
              MODULE_NAME_ENUM.Marketplace_Interests
            )}
            label="Marketplace Interests"
          />
        </Tabs>
      </Box>
      <CustomTabPanel
        hidden={
          !IsPermissionExistForProject(
            userContext.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Project_Details,
            PERMISSION_TYPE.Read,
            userContext.role
          )
        }
        value={value}
        index={0}
        className="custom_tab_padding"
      >
        <DescriptionLayout
          projectDetails={projectDetails}
          handleElChange={props.handleElChange}
          assignDelegateHandler={props.assignDelegateHandler}
          handleCurrentDesignationChange={props.handleCurrentDesignationChange}
          handleSkillChange={props.handleSkillChange}
          setDelegateDBData={setDelegateDBData}
          delegateDBData={delegateDBData}
          setadditionalElDBData={setadditionalElDBData}
          additionalElDBData={additionalElDBData}
        />
      </CustomTabPanel>
      <CustomTabPanel
        hidden={
          !IsPermissionExistForProject(
            userContext.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Project_Calender,
            PERMISSION_TYPE.Read,
            userContext.role
          )
        }
        value={value}
        index={1}
        className="custom_tab_padding"
      >
        <RequestorCalendarView projectDetails={projectDetails} />
      </CustomTabPanel>
      <CustomTabPanel
        hidden={
          !IsPermissionExistForProject(
            userContext.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Project_Budget,
            PERMISSION_TYPE.Read,
            userContext.role,
            userContext.buTreeMappingListByMID,
            projectDetails
          )
        }
        value={value}
        index={2}
        className="custom_tab_padding"
      >
        <BudgetOverview
          projectDetails={projectDetails}
          {...props}
        ></BudgetOverview>
      </CustomTabPanel>
      <CustomTabPanel
        hidden={
          !IsPermissionExistForProject(
            userContext.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Allocation,
            PERMISSION_TYPE.Read,
            userContext.role
          )
        }
        value={value}
        index={3}
        className="active_grid"
      >
        <Activeallocation projectDetails={projectDetails} {...props} />
      </CustomTabPanel>
      <CustomTabPanel value={value} index={4} className="active_grid">
        {IsPermissionExistForProject(
          userContext.projectPermissionData?.permissions,
          MODULE_NAME_ENUM.Requisition,
          PERMISSION_TYPE.Read,
          userContext.role
        ) && (
          <ActiveRequisitionsDeatils
            pipelineCode={projectDetails.pipelineCode}
            jobCode={projectDetails.jobCode}
            projectDetails={projectDetails}
          />
        )}
      </CustomTabPanel>
      <CustomTabPanel value={value} index={5} className="active_grid">
        {IsPermissionExistForProject(
          userContext.projectPermissionData?.permissions,
          MODULE_NAME_ENUM.Marketplace_Interests,
          PERMISSION_TYPE.Read,
          userContext.role
        ) && <MarketplaceInterests projectDetails={projectDetails} />}
      </CustomTabPanel>
    </Box>
  );
};

export default RequestorView;
