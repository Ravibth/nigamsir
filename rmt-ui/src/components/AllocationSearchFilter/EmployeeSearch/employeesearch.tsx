import { useState, useEffect, useMemo } from "react";
import TextField from "@mui/material/TextField";
import SearchIcon from "@mui/icons-material/Search";
import * as constant from "./constant";
import { debounce } from "@mui/material/utils";
import Typography from "@mui/material/Typography";
import Autocomplete from "@mui/material/Autocomplete";
import Grid from "@mui/material/Grid";
import { GetResourceNameService } from "../../../services/allocation/get-resource-name.service";
import "./employeeSearch.css";
import { ESearchTypeOptions } from "../../common-allocation/name-search/name-search";
import ConfirmationDialog from "../../../common/confirmation-dialog/confirmation-dialog";
import { getEmailId } from "../../../global/utils";

interface IEmployeeDetails {
  name: string;
  skills: string[];
  emailId: string;
  designation: string;
  BusinessUnit: string;
  // expertise: string;
  // smeg: string;
  // competency: string;
}

interface IEmployeeSearchProps {
  employeeSelected: (user: any) => void;
  showDifferentTypeSelections: boolean;
  valueForDifferentTypeSelections?: string;
  optionsForDifferentTypeSelections?: string[];
  optionsForSameTypeSearch?: string[];
  onChangeForDifferentTypeSelections?: (e) => void;
  onChangeForSameTeamJobCode?: (e) => void;
  selectedJobCode?: string;
}

export default function Employeesearch(props: IEmployeeSearchProps) {
  const [options, setOptions] = useState<readonly IEmployeeDetails[]>([]);
  const [value, setValue] = useState<IEmployeeDetails | null | any>(null);
  const [inputValue, setInputValue] = useState("");
  const [openConfirmation, setOpenConfirmation] = useState<string>("");

  const fetch = useMemo(
    () =>
      debounce(
        async (
          request: { input: string },
          callback: (results?: readonly IEmployeeDetails[]) => void
        ) => {
          const result: any = await GetResourceNameService([request.input]);
          if (result.data) {
            showOptions(result.data);
          }
        },
        400
      ),
    []
  );

  const showOptions = (results?: readonly IEmployeeDetails[]) => {
    if (results) {
      let newOptions: readonly IEmployeeDetails[] = [];
      if (value) {
        newOptions = [value];
      }

      if (results) {
        newOptions = [...newOptions, ...results];
      }
      setOptions(newOptions);
    }
  };

  useEffect(() => {
    let active = true;
    if (inputValue === "") {
      setOptions(value ? [value] : []);
      return undefined;
    }

    fetch({ input: inputValue }, (results?: readonly IEmployeeDetails[]) => {
      if (active) {
        let newOptions: readonly IEmployeeDetails[] = [];
        if (value) {
          newOptions = [value];
        }
        if (results) {
          newOptions = [...newOptions, ...results];
        }
        setOptions(newOptions);
      }
    });

    return () => {
      active = false;
    };
  }, [value, inputValue, fetch]);

  const findSelectedUser = (selectedUser: string) => {
    const user = options.find((option) => {
      return option.name === selectedUser;
    });
    props.employeeSelected(user);
    if (
      user &&
      props.valueForDifferentTypeSelections === ESearchTypeOptions.Name
    ) {
      setTimeout(() => {
        setValue("");
      }, 100);
    }
    return user?.emailId;
  };

  return (
    <>
      <ConfirmationDialog
        handleYesClick={(e) => {
          setValue(openConfirmation);
          props.onChangeForSameTeamJobCode(openConfirmation);
          setOpenConfirmation("");
        }}
        title="Change Job Code ?"
        content="Are you sure you want to change the job code?"
        noBtnLabel="No"
        yesBtnLabel="Yes"
        open={openConfirmation ? true : false}
        onConfirmationPopClose={(e) => {
          setOpenConfirmation("");
        }}
      />
      <Typography component={"div"} sx={{ display: "flex" }}>
        <span className="name-container">
          {props.showDifferentTypeSelections && (
            <Autocomplete
              value={props.valueForDifferentTypeSelections}
              options={props.optionsForDifferentTypeSelections}
              freeSolo={false}
              className={"input-field-group "}
              style={{ backgroundColor: "lightgrey" }}
              renderInput={(params) => (
                <TextField
                  // label={"Search type"}
                  className={"input-field-group  "}
                  {...params}
                  style={{ width: "160px" }}
                  fullWidth
                />
              )}
              onChange={(_, data) => {
                if (
                  props.showDifferentTypeSelections &&
                  props.valueForDifferentTypeSelections ===
                    ESearchTypeOptions.Name
                ) {
                  setValue(props?.selectedJobCode ? props.selectedJobCode : "");
                } else {
                  setValue(null);
                }
                props.onChangeForDifferentTypeSelections(data);
              }}
              sx={constant.AutoCompleteControlSxProps}
            />
          )}
        </span>
        <span
          className={
            props.showDifferentTypeSelections
              ? "search-container-multiple"
              : "search-container-single"
          }
        >
          <Autocomplete
            id="searchEmployee"
            sx={{ width: "100%", borderRadius: 0 }}
            getOptionLabel={(option) =>
              typeof option === "string" ? option : option.name
            }
            filterOptions={(x) => x}
            options={
              props.showDifferentTypeSelections &&
              props.valueForDifferentTypeSelections ===
                ESearchTypeOptions.SameTeam
                ? props.optionsForSameTypeSearch
                : options
            }
            autoComplete
            includeInputInList
            filterSelectedOptions
            value={value}
            noOptionsText="No Match Found."
            onChange={(event: any, newValue: IEmployeeDetails | null | any) => {
              if (
                props.showDifferentTypeSelections &&
                props.valueForDifferentTypeSelections ===
                  ESearchTypeOptions.SameTeam
              ) {
                setOpenConfirmation(newValue);
              } else {
                setOptions(newValue ? [newValue, ...options] : options);
                setValue(newValue);
              }
            }}
            onInputChange={(event, newInputValue) => {
              if (newInputValue.length > 2) {
                setInputValue(newInputValue);
                findSelectedUser(newInputValue);
              }
            }}
            renderInput={(params) => (
              <TextField
                {...params}
                placeholder={
                  props.showDifferentTypeSelections &&
                  props.valueForDifferentTypeSelections ===
                    ESearchTypeOptions.SameTeam
                    ? "Select Job Code"
                    : "Search Employee"
                }
                // label="Search Employee"
                InputProps={{
                  ...params.InputProps,
                  startAdornment: (
                    <>
                      {props.valueForDifferentTypeSelections ===
                        ESearchTypeOptions.Name && (
                        <SearchIcon sx={constant.SearchIconSxProps} />
                      )}

                      {params.InputProps.startAdornment}
                    </>
                  ),
                }}
              />
            )}
            renderOption={(params, option) => {
              return (
                <>
                  {props.showDifferentTypeSelections &&
                  props.valueForDifferentTypeSelections ===
                    ESearchTypeOptions.SameTeam ? (
                    <li {...params} key={option}>
                      {option}
                    </li>
                  ) : (
                    <li {...params} key={option.emailId}>
                      <Grid container alignItems="center">
                        <Grid
                          item
                          sx={{
                            width: "calc(100% - 44px)",
                            wordWrap: "break-word",
                          }}
                        >
                          <div className="search-employee-option">
                            {`${option.name} -  ${getEmailId(option.emailId)}`}
                          </div>

                          <Typography variant="body2" color="text.secondary">
                            {option.designation},{" "}
                            {option.BusinessUnit
                              ? option.BusinessUnit + " -> "
                              : ""}
                            {option.competency ? option.competency : ""}
                          </Typography>
                          <Typography variant="body2" color="text.secondary">
                            {option.skills?.toString()}
                          </Typography>
                        </Grid>
                      </Grid>
                    </li>
                  )}
                </>
              );
            }}
          />
        </span>
      </Typography>
    </>
  );
}
