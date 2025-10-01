import { Autocomplete, TextField } from "@mui/material";
import React from "react";
import { Controller } from "react-hook-form";

const ControllerAutocompleteFilteredOptionsTextfield = (props: any) => {
  const { defaultValue,resetKey } = props;

  return (
    <Controller
      name={props.name}
      control={props.control}
      rules={props.rules || {}} 
      render={({ field, fieldState }) => {
        const { error } = fieldState;

        return (
          <Autocomplete
            value={field.value}
            key={resetKey}
            disabled={props?.disabled}
            multiple={props.multiple}
            defaultValue={props.defaultValue || []}
            filterSelectedOptions
            options={props.options}
            sx={props.sx}
            getOptionLabel={props.getOptionLabel ? props.getOptionLabel : undefined}
            renderInput={(params) => {
              return (
                <TextField
                  {...params}
                  placeholder={props?.placeholder || ""}
                  variant={props.textfieldVariant ? props.textfieldVariant : "outlined"}
                  label={props?.label}
                  error={props.showError !== false && !!error} 
                  helperText={props.showError !== false && (error?.message || props.helperText)} 
                  InputLabelProps={ props?.FullWidth==true?{
                    shrink: true,
                    style: { width: '100%' }
                  }:{}}
                />
              );
            }}
            onChange={(_, data: any) => {
              field.onChange(data);
              props.onChange(data);
              return data;
            }}
          />
        );
      }}
    />
  );
};

export default ControllerAutocompleteFilteredOptionsTextfield;
