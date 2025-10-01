import { Divider, Grid, Typography } from "@mui/material";
import LeaderProfile from "../profile/profile";
import {
  IEOOwnerLeadersProflieProps,
  IEQProfileProps,
} from "./IEQProfileProps";
import "./style.css";
import * as GC from "../../../global/constant";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";

const EQProfle = (props: IEOOwnerLeadersProflieProps) => {
  const { EOOwnerLeaders } = props;
  EOOwnerLeaders?.map((profile, index) => console.log(profile));
  return (
    <Grid className="enquiry-container" container sx={{ padding: "13px" }}>
      <Grid item className="enquiry-header" style={{ alignItems: "center" }}>
        <PersonOutlineOutlinedIcon style={{ marginRight: "3px" }} />

        <Typography
          display="inline"
          component="div"
          className="enquiryOwner"
          sx={{ whiteSpace: "nowrap", fontSize: "25px" }}
        >
          <span style={{ fontSize: "18px", display: "inline-block" }}>
            Enquiry Owner :
          </span>
          {/* <Divider sx={{ width: "150px" }} /> */}
        </Typography>
      </Grid>

      {EOOwnerLeaders.map((profile, index) => (
        <Grid key={index} item sx={{ paddingTop: "5px" }}>
          <LeaderProfile {...profile} />
        </Grid>
      ))}
    </Grid>
  );
};

export default EQProfle;
