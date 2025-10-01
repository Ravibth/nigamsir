import { Checkbox, FormControlLabel } from "@mui/material";
import { Control, Controller } from "react-hook-form";

export interface IControllerCheckbox {
  label: string;
  name: string;
  control: Control<any>;
  onChange: (value: any) => void;
  defaultValue: boolean;
  disabled?: boolean;
}
const ControllerCheckbox = (props: IControllerCheckbox) => {
  return (
    <>
      <Controller
        control={props.control}
        name={props.name}
        defaultValue={props.defaultValue}
        render={({ field }) => (
          <FormControlLabel
            control={
              <Checkbox
                checked={field.value}
                {...field}
                disabled={props.disabled}
                onChange={(e) => {
                  field.onChange(e);
                  props.onChange(e);
                }}
              />
            }
            label={props.label}
          />
        )}
      />
    </>
  );
};
export default ControllerCheckbox;
