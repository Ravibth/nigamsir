import { Button } from "@mui/material";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import * as GlobalConstant from "../../global/constant";
// import "../backbutton/style.css";

const BackUploadButton = () => {
  return (
    <div>
      <Button
        className="backuploadbtn"
        sx={{ color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}
        startIcon={
          <ArrowBackIosNewIcon
            className="backuploadarrow"
            sx={{
              color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
            }}
          />
        }
        onClick={() => window.location.reload()}
      >
        Back
      </Button>
    </div>
  );
};

export default BackUploadButton;
