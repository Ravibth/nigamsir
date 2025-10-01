import * as React from "react";
import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import TextField from "@mui/material/TextField";
import SearchIcon from "@mui/icons-material/Search";
import * as constant from "./constant";
import ControllerAutocompleteFilteredOptionsTextfield from "../controllers/controller-autocomplete-filtered-options-textfield";
import { useForm } from "react-hook-form";
import { Input, InputAdornment } from "@mui/material";
import "./style.css";

export default function SearchText(props: any) {
  const { optionItems } = props;
  const {
    handleSubmit,
    formState: { errors },
    control,
    reset,
    getValues,
    setValue,
    watch,
  } = useForm({
    mode: "onTouched",
  });

  return (
    <Box sx={constant.BoxSxProps} id="homeSearch">
      <FormControl variant="outlined" sx={constant.FormControlSxProps}>
        <Box sx={constant.BoxSecondSxProps} className={"search-bar-main"}>
          {/* <SearchIcon sx={constant.SearchIconSxProps} /> */}
          <TextField
            sx={constant.TextfieldSxProps}
            id="input-with-sx"
            variant="outlined"
            onChange={(e: any) => {
              props.onAutocompleteTextChange(e);
            }}
            placeholder={props.placeholder ? props.placeholder : "Search"}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <SearchIcon sx={constant.SearchIconSxProps} />
                </InputAdornment>
              ),
            }}
            size="small"
          />
        </Box>
      </FormControl>
    </Box>
  );
}
