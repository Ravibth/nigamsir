import { FormControl, FormControlLabel, Grid, Radio, RadioGroup, TextField, Typography } from '@mui/material'
import React from 'react'
import * as constant from "./constant";

const Efforts = () => {
  return (
    <div>
        <Grid container spacing={2} >
            <Grid item xs={3}>
              <Typography>Efforts(hr/day)</Typography>
              <TextField sx={constant.Textbox} id="outlined-basic" variant="outlined" size="small" />
            </Grid>

            <Grid item xs={3}>
              <Typography>Total Efforts(hr/day)</Typography>
              <TextField sx={constant.Textbox} id="outlined-basic" variant="outlined" size="small" />
            </Grid>
            <Grid item xs={6}>
              <Typography>Continuous Allocation</Typography>
              <FormControl>
                <RadioGroup row aria-labelledby="demo-row-radio-buttons-group-label" name="row-radio-buttons-group">
                  <FormControlLabel value="Yes" control={<Radio />} label="yes" />
                  <FormControlLabel value="no" control={<Radio />} label="no" />
                </RadioGroup>
              </FormControl>
            </Grid>
          </Grid>    </div>
  )
}

export default Efforts
