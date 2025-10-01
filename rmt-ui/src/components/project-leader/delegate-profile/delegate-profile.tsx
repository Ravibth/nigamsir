import { Divider, Grid, Typography } from "@mui/material";
import React from "react";
import LeaderProfile from "../profile/profile";
import { IProfileProps } from "../profile/IProfileProps";
import { IDelegateProfileProps } from "./IDelegateProfileProps";
import "./style.css";

const DelegateProfile = (props: IDelegateProfileProps) => {
  const { delegates } = props;

  return (
    <>
      <Grid container>
        <Grid item xs={3} mt={1.5} mb={1}>
          <Typography display="inline" component="div" className="delegateLeader">
            Delegates
          </Typography>
        </Grid>
        <Grid item xs={9} mt={1} mb={1}>
          <Grid container>
            {delegates?.map((profile: IProfileProps, index: number) => {
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

export default DelegateProfile;
