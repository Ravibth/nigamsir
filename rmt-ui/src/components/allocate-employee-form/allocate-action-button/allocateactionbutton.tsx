import { Grid } from "@mui/material";
import ActionButton from "../../actionButton/actionButton";
import BackActionButton from "../../actionButton/backactionButton";

export const AllocateActionButton = (props: any) => {
  return (
    <Grid
      container
      spacing={2}
    >
      <Grid
        item
        xs={3}
      ></Grid>
      <Grid
        item
        xs={2.5}
        sm={2.5}
        md={2.5}
        lg={2}
        className="confrim-btn-main"
      >
        <BackActionButton
          label={"Cancel"}
          onClick={(e: any) => {
            props.closeModel();
          }}
        />
      </Grid>
      <Grid
        item
        xs={2.5}
        sm={2.5}
        md={2.5}
        lg={2}
        className="confrim-btn-main"
      >
        <ActionButton
          label={"Allocate"}
          type="submit"
          disabled={props?.isEnableSubmitBtn}
          onClick={(e: any) => {
            props.onSubmitBtnClick(e);
          }}
        />
      </Grid>
    </Grid>
  );
};
