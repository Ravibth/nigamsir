import { Grid } from "@mui/material";
import { IELProflieProps } from "./secondary-engagement-leader-profile/IELProfileProps";
import { IDelegateProfileProps } from "./delegate-profile/IDelegateProfileProps";
import { IEQProfileProps } from "./enquiry-owner-profile/IEQProfileProps";
import { useState } from "react";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import React from "react";


const ProjectLeadingMembers = (props: any) => {
  // const [isEmployee, setIsEmployee] = useState(false);
  const isEmployee = React.useContext(UserDetailsContext).isEmployee;
  const ELProps: IELProflieProps = {
    engagementLeaders: props?.EngagementLeadersList,
  };
  const DelegateProps: IDelegateProfileProps = {
    delegates: props?.DelegateList,
  };
  const EQProps: IEQProfileProps = {
    eqProfile: props?.eqProfile,
  };
  return (
    <div>
      <Grid container>
        <Grid
          item
          xs={12}
        >
        
        </Grid>
        <Grid
          item
          xs={12}
        >
          {/* <ELProfle  /> */}
        </Grid>

      </Grid>
    </div>
  );
};

export default ProjectLeadingMembers;
