import * as React from "react";
import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import TextField from "@mui/material/TextField";
import SearchIcon from "@mui/icons-material/Search";
import * as constant from "./constant";
import ControllerAutocompleteFilteredOptionsTextfield from "../controllers/controller-autocomplete-filtered-options-textfield";

export default function SearchBar(props: any) {
  const { optionItems } = props;

  return (
    <Box sx={constant.BoxSxProps} id="AutoSearch">
      <FormControl variant="standard" sx={constant.FormControlSxProps}>
        <Box sx={constant.BoxSecondSxProps}>
          <ControllerAutocompleteFilteredOptionsTextfield
            name="items"
            placeholder={"Search"}
            multiple={true}
            control={props.control}
            filterSelectedOptions={true}
            sx={constant.AutocompleteSxProps}
            defaultValue={[]}
            options={optionItems ? optionItems : []}
            onChange={(e: any) => {
              props.onAutocompleteTextChange(e);
            }}
            textfieldVariant="standard"
          />
          <SearchIcon sx={constant.SearchIconSxProps} />
        </Box>
      </FormControl>
    </Box>
  );
}
