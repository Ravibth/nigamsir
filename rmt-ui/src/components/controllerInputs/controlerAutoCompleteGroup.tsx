import { Autocomplete, TextField } from "@mui/material";
import { Controller } from "react-hook-form";
import "./controllers.css";

const ControllerAutoCompleteGroup = (props: any) => {
  return (
    <Controller
      name={props.name}
      control={props.control}
      defaultValue={props.defaultValue}
      rules={{ required: props.required, pattern: props.pattern }}
      render={({ field }) => {
        return (
          <Autocomplete
            sx={props.sx}
            multiple={props.multiple}
            freeSolo={props.freeSolo}
            value={
              typeof field?.value === "string"
                ? field?.value
                : Object.keys(props).includes("sortBy") &&
                  props?.sortBy(field?.value)
                ? props.sortBy(field?.value)
                : field?.value?.sort((a, b) => (a > b ? 1 : -1))
            }
            filterSelectedOptions={
              props.filterSelectedOptions ?? props.filterSelectedOptions
            }
            options={props.options}
            isOptionEqualToValue={(option, value) =>
              option?.Option === value?.Option
            }
            disabled={props.disabled}
            readOnly={props.readonly}
            getOptionDisabled={(option) => option.Disabled === true}
            getOptionLabel={(option) =>
              Object.keys(props).includes("getOptionLabel") &&
              props?.getOptionLabel(option)
                ? props.getOptionLabel(option)
                : option?.Option
            }
            className={
              props.required
                ? "input-field-group required_field chips-render"
                : "input-field-group chips-render"
            }
            groupBy={(option) => option.Type}
            // renderTags={(value, getTagProps) => {
            //   return value.map((option, index) => {
            //     return (
            //       <Chip
            //         variant="outlined"
            //         label={option?.Option}
            //         {...getTagProps({ index })}
            //         key={index}
            //       />
            //     );
            //   });
            // }}
            renderInput={(params) => (
              <TextField
                error={props.error}
                {...params}
                label={props.label ? props.label : "Skills"}
                type={props.type ? props.type : "text"}
                onBlur={field.onBlur}
                key={props.label}
                className={
                  props.required
                    ? "input-field-group required_field chips-render"
                    : "input-field-group chips-render"
                }
              />
            )}
            onChange={(_, data) => {
              const finalData =
                typeof data === "string"
                  ? data
                  : Object.keys(props).includes("sortBy") && props?.sortBy(data)
                  ? props.sortBy(data)
                  : data?.sort((a, b) => (a > b ? 1 : -1));
              field.onChange(finalData);
              props.onChange(finalData) ?? props.onChange(finalData);
              return finalData;
            }}
          />
        );
      }}
    />
  );
};
export default ControllerAutoCompleteGroup;
