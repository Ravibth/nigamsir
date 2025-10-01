import {
  Button,
  Drawer,
  List,
  Stack,
  ListItem,
  ListItemText,
  Autocomplete,
  TextField,
  Divider,
  Typography,
  IconButton,
} from "@mui/material";
import React, { useState } from "react";
import CloseIcon from "@mui/icons-material/Close";
import * as constant from "./constant";
import FilterAltOutlinedIcon from "@mui/icons-material/FilterAltOutlined";
import MainFilter from "../../../common/images/MainFilter.png";

const Approvalsfilter = () => {
  const submitFormHandler = (event: any) => {
    event.preventDefault();
    // console.log("Form Submitted");
  };

  const [openDrawerEmployee, setOpenDrawerEmployee] = useState(false);
  return (
    <React.Fragment>
      <Drawer
        open={openDrawerEmployee}
        onClose={() => setOpenDrawerEmployee(false)}
        sx={constant.DrawerSxProps}
      >
        <div className="stacks">
          <Stack
            direction="row"
            justifyContent="center"
            alignItems="center"
            spacing={2}
          >
            <List>
              <div>
                <form onSubmit={submitFormHandler}>
                  <ListItem>
                    <ListItemText primary="Filter By" />
                    <IconButton
                      onClick={() => {
                        setOpenDrawerEmployee(false);
                      }}
                    >
                      {" "}
                      <CloseIcon />
                    </IconButton>
                  </ListItem>
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      Designation
                    </Typography>
                  </ListItem>
                  <ListItem>
                    <Autocomplete
                      multiple
                      id="tags-standard"
                      options={constant.experties}
                      sx={constant.AutocompleteSxProps}
                      getOptionLabel={(option) => option.title}
                      renderInput={(params) => (
                        <TextField {...params} variant="standard" />
                      )}
                    />
                  </ListItem>
                  <Divider component="li" sx={constant.DividerSxProps} />
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      Period
                    </Typography>
                  </ListItem>
                  <ListItem>
                    <Autocomplete
                      multiple
                      id="tags-standard"
                      sx={constant.AutocompleteSxProps}
                      options={constant.sme}
                      getOptionLabel={(option) => option.title}
                      renderInput={(params) => (
                        <TextField {...params} variant="standard" />
                      )}
                    />
                  </ListItem>
                  <Divider component="li" sx={constant.DividerSxProps} />
                  <ListItem>
                    <Typography sx={constant.TypographySxProps}>
                      Job Code
                    </Typography>
                  </ListItem>
                  <ListItem>
                    <Autocomplete
                      multiple
                      id="tags-standard"
                      sx={constant.AutocompleteSxProps}
                      options={constant.clientName}
                      getOptionLabel={(option) => option.title}
                      renderInput={(params) => (
                        <TextField {...params} variant="standard" />
                      )}
                    />
                  </ListItem>

                  <ListItem>
                    <Button
                      sx={constant.CloseButtonSxProps}
                      variant="outlined"
                      onClick={() => {
                        setOpenDrawerEmployee(false);
                      }}
                    >
                      Close
                    </Button>
                    <Button
                      type="submit"
                      className="rmt-action-button"
                      variant="contained"
                      sx={constant.ApplyFilterButtonSxProps}
                    >
                      Apply
                    </Button>
                  </ListItem>
                </form>
              </div>
            </List>
          </Stack>
        </div>
      </Drawer>
      <Button
        onClick={() => setOpenDrawerEmployee(!openDrawerEmployee)}
        variant="outlined"
        sx={constant.filterIconButton}
      >
        {/* <FilterAltOutlinedIcon /> */}
        <img src={MainFilter} alt="upload" />
      </Button>
    </React.Fragment>
  );
};

export default Approvalsfilter;
