import { Grid } from "@mui/material";
import ActionButton from "../actionButton/actionButton";
import ControllerTextField from "../controllerInputs/controllerTextField";
import { useForm } from "react-hook-form";
import { BUTTON_BACKGROUND_COLOR } from "../../global/constant";

export default function MyApprovalForm(props: any) {
  const {
    control,
    formState: { errors },
    handleSubmit,
    watch,
    getValues,
    setValue,
  } = useForm();
  return (
    <>
      <Grid container>
        <Grid item xs={12}>
          <ControllerTextField
            name="description"
            control={control}
            defaultValue={""}
            required={true}
            label={"Description"}
            error={errors.description}
            multiline={"multiline"}
            fullWidth={"fullWidth"}
            onChange={(e: any) => {}}
          />
        </Grid>
        <Grid item xs={10}></Grid>
        <Grid item xs={2} sm={2} md={2} className="confrim-btn-main">
          <ActionButton
            label={"Approve"}
            type="submit"
            disabled={false}
            backgroundColor={BUTTON_BACKGROUND_COLOR.approved}
            onClick={(e: any) => {
              // handleSubmit(onAllocationClick);
            }}
          />{" "}
          &nbsp;&nbsp;&nbsp;
          <ActionButton
            label={"Reject"}
            type="submit"
            disabled={false}
            backgroundColor={BUTTON_BACKGROUND_COLOR.rejected}
            onClick={(e: any) => {
              // handleSubmit(onAllocationClick);
            }}
          />
        </Grid>
      </Grid>
    </>
  );
}
