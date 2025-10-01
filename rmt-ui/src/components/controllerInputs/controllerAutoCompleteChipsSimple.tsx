import { Autocomplete, Chip, TextField } from "@mui/material";
import { Controller } from "react-hook-form";
import "./controllers.css";

const ControllerAutoCompleteChipsSimple = (props: any) => {
  return (
    <Controller
      name={props.name}
      control={props.control}
      defaultValue={props.defaultValue}
      rules={{
        required: props.required,
        pattern: props.pattern,
        validate: !props.disabled && props.validate,
      }}
      render={({ field }) => {
        return (
          <Autocomplete
            sx={props.sx}
            multiple={props.multiple}
            freeSolo={props.freeSolo}
            size={props.size ?? props.size}
            value={
              props.value
                ? props.value
                : typeof field?.value === "string"
                ? field?.value
                : Object.keys(props).includes("sortBy") &&
                  props?.sortBy(field?.value)
                ? props.sortBy(field?.value)
                : field?.value?.sort((a, b) => (a > b ? 1 : -1))
            }
            filterSelectedOptions={
              props.filterSelectedOptions ?? props.filterSelectedOptions
            }
            isOptionEqualToValue={(option, value) => {
              if (
                Object.keys(props).includes("isOptionEqualToValue") &&
                typeof props.isOptionEqualToValue === "function"
              ) {
                return props.isOptionEqualToValue(option, value);
              } else {
                return option === value;
              }
            }}
            options={props.options}
            disabled={props.disabled}
            readOnly={props.readonly}
            getOptionLabel={(option) => {
              return Object.keys(props).includes("getOptionLabel") &&
                typeof props.getOptionLabel === "function"
                ? props.getOptionLabel(option)
                : option || "";
            }}
            className={
              props.required
                ? "input-field-group required_field chips-render"
                : "input-field-group chips-render"
            }
            groupBy={(option) => {
              return Object.keys(props).includes("groupBy") &&
                typeof props.groupBy === "function"
                ? props.groupBy(option)
                : null;
            }}
            renderTags={(value, getTagProps) => {
              return value?.map((option, index) => {
                return (
                  <Chip
                    variant="outlined"
                    label={
                      Object.keys(props).includes("getOptionLabel") &&
                      typeof props.getOptionLabel === "function"
                        ? props.getOptionLabel(option)
                        : option || ""
                    }
                    {...getTagProps({ index })}
                    key={index}
                    className={
                      props?.highlightDifferentValuesDifferently &&
                      Object.keys(props).includes(
                        "propertyToHighlightDifferently"
                      ) &&
                      Object.keys(props).includes(
                        "propertyValueToHighlightDifferently"
                      ) &&
                      Object.keys(option).includes(
                        props?.propertyToHighlightDifferently
                      ) &&
                      option[props?.propertyToHighlightDifferently]
                        ?.toLowerCase()
                        ?.trim() ===
                        props?.propertyValueToHighlightDifferently
                          ?.toLowerCase()
                          ?.trim()
                        ? "chips-highlight"
                        : ""
                    }
                  />
                );
              });
            }}
            renderInput={(params) => (
              <TextField
                error={props.error}
                {...params}
                label={props.label}
                type={props.type ? props.type : "text"}
                onBlur={field?.onBlur}
                className={
                  props.required
                    ? "input-field-group required_field chips-render"
                    : "input-field-group chips-render"
                }
              />
            )}
            renderOption={(propsRenderOption, option) => (
              <li {...propsRenderOption} key={option?.id}>
                {Object.keys(props).includes("getOptionLabel") &&
                typeof props.getOptionLabel === "function"
                  ? props.getOptionLabel(option)
                  : option || ""}
              </li>
            )}
            onChange={(_, data) => {
              field?.onChange(data);
              props?.onChange?.(data);
              return data;
            }}
          />
        );
      }}
    />
  );
};
export default ControllerAutoCompleteChipsSimple;
