import { Switch, FormControlLabel } from "@mui/material";
import { Control, Controller } from "react-hook-form";

export interface IControllerSwitch {
  label: string;
  name: string;
  control: Control<any>;
  onChange: (value: any) => void;
  defaultValue: boolean;
  disabled?: boolean;
  color?: "primary" | "secondary" | "default";
}

const ControllerSwitch = (props: IControllerSwitch) => {
  return (
    <Controller
      control={props.control}
      name={props.name}
      defaultValue={props.defaultValue}
      render={({ field }) => (
        <FormControlLabel
          control={
            <Switch
              checked={Boolean(field.value)}
              {...field}
              disabled={props.disabled}
              color={props.color || "primary"}
              onChange={(e) => {
                field.onChange(e.target.checked);
                props.onChange(e.target.checked);
              }}
            />
          }
          label={props.label}
        />
      )}
    />
  );
};

export default ControllerSwitch;