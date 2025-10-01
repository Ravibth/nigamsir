import { useContext, useEffect, useState } from "react";
import { IUserDetailsContext, UserDetailsContext } from "../../contexts/userDetailsContext";
import ControllerAutoCompleteChipsSimple from "../controllerInputs/controllerAutoCompleteChipsSimple";
import { useForm } from "react-hook-form";
import _ from "lodash";
import { IRoleOption, IRoleOptions, IRoleSearchProps } from "./interface";
import { AutocompleteSxProps } from "./constant";
import { Autocomplete, Box, TextField } from "@mui/material";
import { GetUserRoleCompeteInformation } from "./util";
import { RolesListMaster } from "../../common/enums/ERoles";
import { GetUserRoleOptions } from "../../global/utils";

const RoleSearch = (props: IRoleSearchProps) => {
  const userContext = useContext(UserDetailsContext);
  const [options, setOptions] = useState<IRoleOptions>([]);
  const {
    formState: { errors },
    control,
  } = useForm({
    mode: "onTouched",
  });
  useEffect(() => {
    // console.log(userContext);
    const options =  GetUserRoleOptions(userContext);
    setOptions(options);
  }, [userContext]);
  const roleChangeHandler = (selectedOptions: IRoleOptions) => {
    // console.log(selectedOptions);
    const modifiedSelectedOptions = selectedOptions.map((opt) => opt.roleName);
    props.setSearchRoles(modifiedSelectedOptions);
  };
  return (
    <>
      <Box component={"div"} mt={2}>
        
        <Autocomplete
          multiple
          id="tags-outlined"
          options={options ? options : []}
          getOptionLabel={(option: IRoleOption) => option.roleDisplayName}
          filterSelectedOptions
          sx={AutocompleteSxProps}
          size="small"
          renderInput={(params) => (
            <TextField {...params} label="Role" placeholder="Role" />
          )}
          onChange={(e, value) => {
            roleChangeHandler(value);
          }}
        />
      </Box>
    </>
  );
};

export default RoleSearch;
