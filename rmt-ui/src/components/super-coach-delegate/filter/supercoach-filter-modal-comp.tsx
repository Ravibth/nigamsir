import * as React from "react";
import Backdrop from "@mui/material/Backdrop";
import Box from "@mui/material/Box";
import Modal from "@mui/material/Modal";
import Fade from "@mui/material/Fade";
import Button from "@mui/material/Button";
import FilterAltOutlinedIcon from "@mui/icons-material/FilterAltOutlined";
import { Container, Divider, List, ListItem, Typography } from "@mui/material";
import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";
import "./styles.css";
import * as constant from "./constant";
import MainFilter from "../../../common/images/MainFilter.png";


export default function SupercoachFilterModalComp() {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <React.Fragment>
      <Container maxWidth="lg">
        <Button
          onClick={handleOpen}
          variant={"outlined"}
          sx={constant.filterIconButton}
        >
          {/* <FilterAltOutlinedIcon /> */}
          <img src={MainFilter} alt="upload" />
        </Button>

        <Modal
          aria-labelledby="transition-modal-title"
          aria-describedby="transition-modal-description"
          open={open}
          onClose={handleClose}
          closeAfterTransition
          slots={{ backdrop: Backdrop }}
          slotProps={{
            backdrop: {
              timeout: 500,
            },
          }}
        >
          <Fade in={open}>
            <Box>
              <List>
                <ListItem>
                  <Typography>Experties</Typography>
                </ListItem>
                <ListItem>
                  <Autocomplete
                    multiple
                    id="tags-standard"
                    options={constant.TOP100FILMS}
                    getOptionLabel={(option) => option.title}
                    renderInput={(params) => (
                      <TextField {...params} variant="standard" />
                    )}
                  />
                </ListItem>
                <Divider component="li" sx={{ borderBottomWidth: 2 }} />
                <ListItem>
                  <Typography>SME</Typography>
                </ListItem>
                <ListItem>
                  <Autocomplete
                    multiple
                    id="tags-standard"
                    options={constant.TOP100FILMS}
                    getOptionLabel={(option) => option.title}
                    renderInput={(params) => (
                      <TextField {...params} variant="standard" />
                    )}
                  />
                </ListItem>
                <Divider component="li" sx={{ borderBottomWidth: 2 }} />
                <ListItem>
                  <Typography>Experties</Typography>
                </ListItem>
                <ListItem>
                  <Autocomplete
                    multiple
                    id="tags-standard"
                    options={constant.TOP100FILMS}
                    getOptionLabel={(option) => option.title}
                    renderInput={(params) => (
                      <TextField {...params} variant="standard" />
                    )}
                  />
                </ListItem>
                <Divider component="li" sx={{ borderBottomWidth: 2 }} />
                <ListItem>
                  <Typography>SME</Typography>
                </ListItem>
                <ListItem>
                  <Autocomplete
                    multiple
                    id="tags-standard"
                    options={constant.TOP100FILMS}
                    getOptionLabel={(option) => option.title}
                    renderInput={(params) => (
                      <TextField {...params} variant="standard" />
                    )}
                  />
                </ListItem>
                <Divider component="li" sx={{ borderBottomWidth: 2 }} />
                <ListItem>
                  <Typography>SME</Typography>
                </ListItem>
                <ListItem>
                  <Autocomplete
                    multiple
                    id="tags-standard"
                    options={constant.TOP100FILMS}
                    getOptionLabel={(option) => option.title}
                    renderInput={(params) => (
                      <TextField {...params} variant="standard" />
                    )}
                  />
                </ListItem>
              </List>

              <Button variant="outlined">Close</Button>
              <Button className="rmt-action-button" variant="outlined">
                Apply
              </Button>
            </Box>
          </Fade>
        </Modal>
      </Container>
    </React.Fragment>
  );
}
