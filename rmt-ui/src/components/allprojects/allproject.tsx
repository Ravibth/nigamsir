import Stack from "@mui/material/Stack";
import { Chip, Typography } from "@mui/material";
import * as constant from "./constant";

const Allproject = (props:any) => {
  return (
    <Stack direction="row" spacing={2} sx={constant.StackSxProps}>
      <Stack>
        <Typography sx={constant.TypographySxProps}>All Projects</Typography>
      </Stack>
      <Chip sx={constant.AvatarSxProps} label={props.projectCount} />
    </Stack>
  );
};

export default Allproject;
