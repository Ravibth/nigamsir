import * as React from "react";
import CircularProgress, { CircularProgressProps } from "@mui/material/CircularProgress";
import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import { Grid } from "@mui/material";

function CircularProgressWithLabel(props: CircularProgressProps & { value: number }) {
  return (
    <Grid>
      <Grid>
        <Box sx={{ position: "relative", display: "inline-flex" }}>
          <CircularProgress
            variant="determinate"
            {...props}
            sx={{
              color: "green", // Change the color of the progress bar to green
            }}
          />
          <Box
            sx={{
              top: 0,
              left: 0,
              bottom: 0,
              right: 0,
              position: "absolute",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <Typography variant="caption" component="div" color="text.secondary">{`${Math.round(
              props.value
            )}%`}</Typography>
          </Box>
        </Box>
      </Grid>
      <Grid item sx={{ marginLeft: "-8px", marginTop: "-8px" }}>
        <Typography sx={{backgroundColor:"green",width:"60px",color:"white",borderRadius:"20px",textAlign:"center",fontSize:"12px"}}>match</Typography>
      </Grid>
    </Grid>
  );
}

export default function Progressbar() {
  const [progress, setProgress] = React.useState(10);

  React.useEffect(() => {
    const timer = setInterval(() => {
      setProgress((prevProgress) => (prevProgress >= 100 ? 0 : prevProgress + 10));
    }, 800);
    return () => {
      clearInterval(timer);
    };
  }, []);

  return <CircularProgressWithLabel value={5} />;
}
