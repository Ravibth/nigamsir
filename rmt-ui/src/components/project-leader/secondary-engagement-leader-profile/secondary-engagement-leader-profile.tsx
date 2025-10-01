import { Divider, Grid, Typography } from "@mui/material";
import LeaderProfile from "../profile/profile";
import { IProfileProps } from "../profile/IProfileProps";
import { IELProflieProps } from "./IELProfileProps";
import "./style.css";
const SecELProfile = (props: IELProflieProps) => {
  const { engagementLeaders } = props;
  return (
    <>
      <Grid container>
        <Grid item xs={3} mt={1.5} mb={1}>
          <Typography
            display="inline"
            component="div"
            className="engagementLeader"  
          >
            Secondary Engagement Leader
            <Divider sx={{ width: "75%" }} />
          </Typography>
        </Grid>
        <Grid item xs={9} mt={1} mb={1}>
          <Grid container>
            {engagementLeaders?.map((profile: any, index: number) => {
              return (
                <Grid key={index} m={1} item xs={12} sm={12} lg={3}>
                  <LeaderProfile {...profile} />
                </Grid>
              );
            })}
          </Grid>
        </Grid>
      </Grid>
    </>
  );
};

export default SecELProfile;
