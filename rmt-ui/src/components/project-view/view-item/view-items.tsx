import { Badge, Box, Tooltip } from "@mui/material";
import * as constant from "./constant";
import moment from "moment";
import { ProjectCategories } from "../../../global/constant";
import InfoIcon from "@mui/icons-material/Info";

const ViewItems = (props: any) => {
  const { title, value } = props;

  // const getProjectBudgetInfoIcon = (value) => {
  //   let finalLabel = "";
  //   let finalColor = "";
  //   switch (value) {
  //     case ProjectCategories.InBudget:
  //       finalLabel = ProjectCategories.InBudget;
  //       finalColor = "success";
  //       break;
  //     case ProjectCategories.WithinBudget:
  //       finalLabel = ProjectCategories.InBudget;
  //       finalColor = "success";
  //       break;
  //     case ProjectCategories.OutBudget:
  //       finalLabel = ProjectCategories.OverBudget;
  //       finalColor = "error";
  //       break;
  //     case ProjectCategories.OverBudget:
  //       finalLabel = ProjectCategories.OverBudget;
  //       finalColor = "error";
  //       break;
  //     case ProjectCategories.BudgetExceeded:
  //       finalLabel = ProjectCategories.OverBudget;
  //       finalColor = "error";
  //       break;
  //     case ProjectCategories.ReachingThresholdBudget:
  //       finalLabel = ProjectCategories.ReachingThresholdBudget;
  //       finalColor = "warning";
  //       break;
  //     default:
  //       finalLabel = "";
  //       finalColor = "";
  //   }
  //   if (finalLabel && finalColor) {
  //     return (
  //       <Tooltip placement="right" title={finalLabel}>
  //         <InfoIcon color={finalColor as any} />
  //       </Tooltip>
  //     );
  //   } else {
  //     return <></>;
  //   }
  // };

  return (
    <Box>
      <Box
        pl={1}
        mt={0.5}
        mb={0.5}
        pt={0.5}
        pb={0.5}
        height={"30px"}
        sx={constant.title}
      >
        {title}
      </Box>
      <Box pl={1} mt={0.5} mb={0.5} pt={0.5} pb={0.5} sx={constant.data}>
        {title === "Budget" && value && (
          <>
            {value.toLowerCase() === ProjectCategories.InBudget.toLowerCase() ||
            value.toLowerCase() ===
              ProjectCategories.WithinBudget.toLowerCase() ? (
              <Badge color="success" variant="dot" sx={{ mr: 1 }}></Badge>
            ) : value.toLowerCase() ===
                ProjectCategories.OutBudget.toLowerCase() ||
              value.toLowerCase() ===
                ProjectCategories.OverBudget.toLowerCase() ||
              value.toLowerCase() ===
                ProjectCategories.BudgetExceeded.toLowerCase() ? (
              <Badge color="error" variant="dot" sx={{ mr: 1 }}></Badge>
            ) : value.toLowerCase() ===
              ProjectCategories.ReachingThresholdBudget.toLowerCase() ? (
              <Badge color="warning" variant="dot" sx={{ mr: 1 }}></Badge>
            ) : (
              <Badge color="default" variant="dot" sx={{ mr: 1 }}></Badge>
            )}
          </>
        )}
        {title === constant.viewItemTitle.START_DATE ||
        title === constant.viewItemTitle.END_DATE
          ? moment(value).format("DD-MM-YYYY")
          : value}
        {/* {getProjectBudgetInfoIcon(value)} */}
      </Box>
    </Box>
  );
};

export default ViewItems;
