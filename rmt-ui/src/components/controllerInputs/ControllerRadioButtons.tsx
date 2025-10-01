import { FormControlLabel, FormLabel, Radio, RadioGroup } from "@mui/material";
import { Control, Controller } from "react-hook-form";

export interface IControllerRadioButtons {
  label: string;
  name: string;
  control: Control<any>;
  row: boolean;
  onChange: (value: any) => void;
  radioButtonsLabel: string[];
  defaultValueChecked: string | boolean;
}

const ControllerRadioButtons = (props: IControllerRadioButtons) => {
  return (
    <>
      <FormLabel component={"legend"}>{props.label}</FormLabel>
      <Controller
        name={props.name}
        control={props.control}
        defaultValue={props.defaultValueChecked}
        render={({ field }) => {
          return (
            <RadioGroup
              row={props.row}
              aria-label={props.name}
              {...field}
              onChange={(e) => {
                props.onChange(e);
              }}
            >
              {props.radioButtonsLabel.map((radioButton: string) => (
                <FormControlLabel
                  value={radioButton}
                  label={radioButton}
                  control={<Radio />}
                  checked={field.value === radioButton ? true : false}
                />
              ))}
            </RadioGroup>
          );
        }}
      />
    </>
  );
};
export default ControllerRadioButtons;
