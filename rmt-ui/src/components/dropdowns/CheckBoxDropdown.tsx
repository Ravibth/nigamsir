import React from "react";
import { Controller } from "react-hook-form";
import { Autocomplete, Checkbox, TextField } from "@mui/material";
const CheckBoxDropdown = (props) => {
  const {
    name,
    control,
    rules,
    options,
    label,
    getOptionLabel,
    isOptionEqualToValue,
    renderTags,
    errors,
    multiple = true,
    disableCloseOnSelect = true,
    sx ={}
  } = props;

  return (
    <Controller
      name={name}
      control={control}
      rules={rules}
      render={({ field }) => {
        const selectedOptions = options.filter((option) =>
          field.value?.includes(option.competencyId)
        );

        return (
          <Autocomplete
            multiple={multiple}
            options={options}
            getOptionLabel={getOptionLabel}
            isOptionEqualToValue={isOptionEqualToValue}
            disableCloseOnSelect={disableCloseOnSelect}
            value={selectedOptions}
            onChange={(_, newValue) => {
              const ids = newValue.map((item) => item.competencyId);
              field.onChange(ids);
            }}
            renderOption={(optionProps, option, { selected }) => (
              <li {...optionProps}>
                <Checkbox style={{ marginRight: 3 }} checked={selected} />
                {getOptionLabel(option)}
              </li>
            )}
            renderTags={renderTags}
            renderInput={(params) => (
              <TextField
                {...params}
                label={label}
                error={!!errors?.[name]}
                helperText={errors?.[name]?.message}
                InputLabelProps={{
                  shrink: true,
                  style: { width: '100%' }
                }}
              />
            )}
            sx={sx}
          />
        );
      }}
    />
  );
};

export default CheckBoxDropdown;
