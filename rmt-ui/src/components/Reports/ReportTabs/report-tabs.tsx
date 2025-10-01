// import { Box, Tab, Tabs } from "@mui/material";
// import React, { useContext, useEffect, useState } from "react";
// import ReportCustomTabPanel from "./report-custom-tab-panel";
// import { UserDetailsContext } from "../../../contexts/userDetailsContext";
// import { IBUTreeMappingListByMID } from "../../../common/interfaces/IBUTreeMappingListByMID";
// import { getBUTreeMappingListByMID } from "../../../services/configuration-services/configuration.service";
// import { RolesListMaster } from "../../../common/enums/ERoles";

const ReportTabs = () => {
  //   const [value, setValue] = useState(0);
  //   const [userLeaderRoles, setUserLeaderRoles] =
  //     useState<IBUTreeMappingListByMID | null>(null);
  //   const [isLeaderRole, setIsLeaderRole] = useState(false);
  //   const [isEmployeeOnlyRole, setIesEmployeeOnlyRole] = useState(false);
  //   const userDetailsContext = useContext(UserDetailsContext);
  //   const loadLeadersInformationFromBUTreeMapping = () => {
  //     return new Promise((resolve, reject) => {
  //       getBUTreeMappingListByMID(userDetailsContext.employee_id)
  //         .then((response) => {
  //           resolve(response.data);
  //         })
  //         .catch((err) => {
  //           reject(err);
  //         });
  //     });
  //   };
  //   const updateIsLeaderAndEmployyeeRole = (
  //     leaderRoles: IBUTreeMappingListByMID
  //   ) => {
  //     const isLeader =
  //       leaderRoles &&
  //       (Object.keys(leaderRoles?.bu).length > 0 ||
  //         Object.keys(leaderRoles?.offerings).length > 0 ||
  //         Object.keys(leaderRoles?.solutions).length > 0);
  //     const isEmp =
  //       userDetailsContext.role.includes(RolesListMaster.Employee) &&
  //       userDetailsContext.role.length === 1;
  //     setIsLeaderRole(isLeader);
  //     setIesEmployeeOnlyRole(isEmp);
  //     if (isLeader) {
  //       setValue(0);
  //     } else if (isEmp) {
  //       setValue(1);
  //     }
  //   };
  //   useEffect(() => {
  //     if (userDetailsContext && userDetailsContext.employee_id.length > 0) {
  //       loadLeadersInformationFromBUTreeMapping()
  //         .then((response) => {
  //           const result: IBUTreeMappingListByMID =
  //             response as IBUTreeMappingListByMID;
  //           setUserLeaderRoles(result);
  //           updateIsLeaderAndEmployyeeRole(result);
  //         })
  //         .catch((err) => {
  //           console.log(err);
  //         });
  //     }
  //   }, [userDetailsContext]);
  //   const handleChange = (event: React.SyntheticEvent, newValue: number) => {
  //     console.log("newValue", newValue);
  //     setValue(newValue);
  //   };
  //   return (
  //     <div>
  //       <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
  //         <Tabs
  //           value={value}
  //           onChange={handleChange}
  //           aria-label="basic tabs example"
  //         >
  //           <Tab
  //             sx={!isLeaderRole ? { display: "none" } : {}}
  //             label="Capacity Utilization Chart"
  //           />
  //           <Tab
  //             sx={isLeaderRole || isEmployeeOnlyRole ? {} : { display: "none" }}
  //             label="Scheduled Vs Variance Chart"
  //           />
  //         </Tabs>
  //       </Box>
  //       <ReportCustomTabPanel hidden={!isLeaderRole} value={value} index={0}>
  //         Item One
  //       </ReportCustomTabPanel>
  //       <ReportCustomTabPanel
  //         hidden={!isLeaderRole && !isEmployeeOnlyRole}
  //         value={value}
  //         index={1}
  //       >
  //         Item Two
  //       </ReportCustomTabPanel>
  //     </div>
  //   );
};

export default ReportTabs;
