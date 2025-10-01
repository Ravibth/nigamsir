import React from "react";
import { Routes, Route } from "react-router-dom";
import Layout from "../pages/layout";
import ProjectListingPage from "../pages/project-list";
import ProjectDetailPage from "../pages/project-details";
import CalendarPage from "../pages/calender";
import CreateRequisition from "../pages/requisition";
import RolePermission from "../pages/role-permission";
import ManagePage from "../pages/manage";
// import Configurations from "../pages/configurations";
import Marketplace from "../components/Marketplace/marketplace";
import Approvals from "../components/approvals/approvals";
import UpdateRequisitionForm from "../pages/update-requisition";
import MyApproval from "../components/workflow/my-approval";
import BulkUpload from "../pages/bulk-upload";
import UnAuthorizedView from "../pages/UnAuthorizedView";
import Report from "../pages/report";
import AddNewSkill from "../components/skill-master/addNewSkill";
import SkillMasterDashboard from "../components/skill-master/skillMasterDashboard";
import SearchMain from "../components/skills/search-main";
import MySkillsMainRoute from "../pages/mySkillsMainRoute";
import SkillReviewPage from "../pages/skill-review-page";
import Preferences from "../pages/preferences";
import MyCalendar from "../pages/mycalendar";
import ConfigurationMaster from "../components/configuration-master/configuration-master";
import EmployeePortfolioBalancing from "../pages/employee-portfolio-balancing";
import SuperCoachDelegate from "../components/super-coach-delegate/super-coach-delegate";
import EmployeeProfile from "../components/employee-profile/profile";

const RoutesConfig = () => (
  <Routes>
    <Route path="/" element={<Layout />}>
      <Route
        path="/projects"
        element={<ProjectListingPage isEmployee={false} />}
      />
      <Route path="/" element={<ProjectListingPage isEmployee={false} />} />
      <Route
        path="/project-details/:pipelineCode/:jobCode"
        element={<ProjectDetailPage isEmployee={false} />}
      />
      <Route
        path="/project-details/:pipelineCode/:jobCode/:guid"
        element={<ProjectDetailPage isEmployee={false} />}
      />
      <Route
        path="/employee"
        element={<ProjectListingPage isEmployee={true} />}
      />
      <Route
        path="/calendar/:pipelineCode/:jobCode"
        element={<CalendarPage />}
      />
      {/* <Route
        path="/calendar/:id"
        element={<CalendarPage />}
      /> */}
      <Route
        path="/create-requisition/:pipelineCode/:jobCode"
        element={<CreateRequisition />}
      />
      <Route
        path="/update-requisition/:pipelineCode/:jobCode/:requisitionId"
        element={<UpdateRequisitionForm />}
      />
      <Route path="/marketplace" element={<Marketplace />} />
      <Route path="/manage" element={<ManagePage />} />
      <Route path="/my-preference" element={<Preferences />} />
      <Route path="/my-calender" element={<MyCalendar />} />
      <Route path="/reports" element={<Report />} />
      <Route
        path="/portfolio-balancing"
        element={<EmployeePortfolioBalancing />}
      />
      <Route path="/employee-profile/:userEmailId?" element={<EmployeeProfile />} />
      {/* <Route path="/reports/dashboard" element={<ReportDashboard />} /> */}
      <Route path="/skillmaster" element={<SkillMasterDashboard />} />
      <Route path="/addNewSkill" element={<AddNewSkill />} />
      <Route path="/roles-permission" element={<RolePermission />} />
      <Route path="/configurations" element={<ConfigurationMaster />} />
      <Route path="/super-coach-delegate" element={<SuperCoachDelegate />} />
      <Route path="/alerts" element={<Approvals />} />
      <Route path="/myapproval" element={<MyApproval />} />
      <Route
        path="/bulk-upload/:pipelineCode/:jobCode"
        element={<BulkUpload />}
      />
      <Route path="/unauthorized" element={<UnAuthorizedView />} />
      <Route path="/myskill" element={<MySkillsMainRoute />} />
      <Route path="/searchskill" element={<SearchMain />} />
      <Route path="/skill-review" element={<SkillReviewPage />} />
      <Route
        path="/skill-review/:pipelineCode/:jobCode/:guid"
        element={<SkillReviewPage />}
      />
    </Route>
  </Routes>
);

export default RoutesConfig;
