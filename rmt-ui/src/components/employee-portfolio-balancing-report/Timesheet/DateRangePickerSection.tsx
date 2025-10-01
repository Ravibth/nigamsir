import { Box, Grid } from "@mui/material";
import ControllerCalendar from "../../controllerInputs/controlerCalendar";
import { EDateControl } from "../Type/type";

export const DateRangePickerSection = ({
  control,
  errors,
  setStartDate,
  setEndDate,
  startDate,
  onStartDateChange
}: {
  control: any;
  errors:any;
  setStartDate: (date: Date | null) => void;
  setEndDate: (date: Date | null) => void;
  startDate: Date | null;
  onStartDateChange: (date: Date | null) => void;
}) => (
  <>
    <Box display="flex" ml={1} mt={3.5}>
      <Box sx={{ transform: "scale(0.8)", transformOrigin: "top left", mr: -3 }}>
        <Grid item minWidth={160}>
          <ControllerCalendar
            name={EDateControl.start_date}
            control={control}
            defaultValue={null}
            label="Start Date*"
            onChange={(date: any) => {
              setStartDate(date);          // Update state
              onStartDateChange(date);     // Call method with selected date
            }}
            error={!!errors[EDateControl.start_date]}
            helperText={errors[EDateControl.start_date]?.message}
            rules={{ required: "Start Date is required" }}            
          />
        </Grid>
      </Box>

      <Box sx={{ transform: "scale(0.8)", transformOrigin: "top left" }}>
        <Grid item xs={12} sm="auto" minWidth={140}>
          <ControllerCalendar
            name={EDateControl.end_date}
            control={control}
            defaultValue={null}
            label="End Date*"
            onChange={(date: any) => setEndDate(date)}
            minDate={startDate}
            rules={{ required: "End Date is required" }}
            error={!!errors[EDateControl.end_date]}
            helperText={errors[EDateControl.end_date]?.message}
            maxDate={
              startDate
                ? new Date(
                    new Date(startDate).setMonth(
                      new Date(startDate).getMonth() + 3
                    )
                  )
                : undefined
            }
          />
        </Grid>
      </Box>
    </Box>
  </>
);
