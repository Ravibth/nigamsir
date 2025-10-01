import { Badge, IconButton } from "@mui/material";
import AddTaskIcon from "@mui/icons-material/AddTask";
import { useNavigate } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import { getEmployeeTaskCountByApiService } from "../Util";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";

export default function MyTaskBar() {
  const navigate = useNavigate();
  const navigateToMyApproval = () => {
    navigate("/myapproval");
  };
  const userDetailsContext = useContext(UserDetailsContext);

  const [taskCount, setTaskCount] = useState<number>(0);

  const updateTaskCount = (count: number) => {
    setTaskCount(count);
  };

  const getEmployeeTaskCountByApi = () => {
    const payload = {
      assigned_to: userDetailsContext.username,
      taskstatus: "pending",
      outcome: "inprogress",
    };

    return new Promise<boolean>((resolve, reject) => {
      getEmployeeTaskCountByApiService(payload)
        .then((resp) => {
          updateTaskCount(resp);
          resolve(true);
        })
        .catch((err) => {
          resolve(true);
        });
    });
  };

  useEffect(() => {
    getEmployeeTaskCountByApi();
    const intervalValue: number = Number(
      process.env.REACT_APP_PUSH_NOTIFICATION_TIMEINTERVAL_VALUE
    );
    const getByTimeInterval =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "true";
    if (getByTimeInterval) {
      setInterval(() => {
        getEmployeeTaskCountByApi();
      }, intervalValue);
    }
  }, []);

  return (
    <>
      <IconButton size="large" color="inherit">
        <Badge
          badgeContent={taskCount}
          color="secondary"
          invisible={taskCount > 0 ? false : true}
        >
          <AddTaskIcon onClick={() => navigateToMyApproval()} />
        </Badge>
      </IconButton>
    </>
  );
}
