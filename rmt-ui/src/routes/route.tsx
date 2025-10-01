import { createBrowserRouter } from "react-router-dom";
// import Layout from "../pages/layout";
// import ProjectListingPage from "../pages/project-list";
// import ProjectDetailPage from "../pages/project-details";
// import CalendarPage from "../pages/calender";
// import CreateRequisition from "../pages/requisition";
// import Employees from "../pages/employees";
// import RolePermission from "../pages/role-permission";
// import ManagePage from "../pages/manage";
// import Configurations from "../pages/configurations";
// import Marketplace from "../components/Marketplace/marketplace";
// import Approvals from "../components/approvals/approvals";
// import UpdateRequisitionForm from "../pages/update-requisition";
// import MyApproval from "../components/workflow/my-approval";
// import BulkUpload from "../pages/bulk-upload";
// import UnAuthorizedView from "../pages/UnAuthorizedView";
// import RoutesConfig from "./routeConfig";
import MainHome from "../components/home/main-home";

const router = createBrowserRouter([
  { path: "*", Component: MainHome },
  // todo:: Use routeConfig instead of this
  // {
  //   path: "/",
  //   element: <Layout />,
  //   children: [
  //     { path: "/", element: <ProjectListingPage isEmployee={false} /> },
  //     {
  //       //! id is Project Code
  //       path: "/project-details/:id",
  //       element: <ProjectDetailPage isEmployee={false} />,
  //     },
  //     { path: "/employee", element: <ProjectListingPage isEmployee={true} /> },
  //     // {
  //     //   path: "/employee-project-details/:id",
  //     //   element: <ProjectDetailPage isEmployee={true} />,
  //     // },
  //     //! TODO Check For what is Id
  //     { path: "/calendar/:id", element: <CalendarPage /> },
  //     {
  //       path: "/create-requisition/:projectId",
  //       element: <CreateRequisition />,
  //     },
  //     {
  //       path: "/update-requisition/:projectId/:requisitionId",
  //       element: <UpdateRequisitionForm />,
  //     },
  //     { path: "/employees", element: <Employees /> },
  //     { path: "/marketplace", element: <Marketplace /> },
  //     { path: "/manage", element: <ManagePage /> },
  //     { path: "/reports", element: <Employees /> },
  //     { path: "/roles-permission", element: <RolePermission /> },
  //     { path: "/configurations", element: <Configurations /> },
  //     { path: "/alerts", element: <Approvals /> },
  //     { path: "/myapproval", element: <MyApproval /> },
  //     {
  //       path: "/bulk-upload/:projectId",
  //       element: <BulkUpload />,
  //     },
  //   ],
  // },
]);

export default router;
