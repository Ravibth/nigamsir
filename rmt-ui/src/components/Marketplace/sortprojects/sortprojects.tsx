import { Autocomplete, Grid, TextField, Typography } from "@mui/material";
import * as constant from "./constant";
import KeyboardArrowUpOutlinedIcon from "@mui/icons-material/KeyboardArrowUpOutlined";
import KeyboardArrowDownOutlinedIcon from "@mui/icons-material/KeyboardArrowDownOutlined";

const Sortprojects = (props: any) => {
  const handleSelectionChange = (e: any, value: any) => {
    console.log("handleSelectionChange");
    props.setSelectedValueForSorting(value);
    props.GetAllProjectDetailsForMarketPlaceData(
      1, // reset the pagintaion for filtering
      props.myFilter.limit,
      props.filter.showLiked,
      props.filter.buFiltervalue,
      props.filter.offeringsFiltervalue,
      props.filter.solutionsFiltervalue,
      props.filter.industryFiltervalue,
      props.filter.subIndustryFiltervalue,
      props.filter.locationFiltervalue,
      props.filter.isAllocatedFiltervalue,
      props.filter.startDateFiltervalue,
      props.filter.endDateFiltervalue,
      value,
      props.myFilter.sortOrder
    );
  };

  const flatProps = {
    options: sortingOption.map((option) => option.title),
  };
  const sortHandlerUp = () => {
    //console.log("sortHandlerUp");
    props.GetAllProjectDetailsForMarketPlaceData(
      1, // reset the pagintaion for filtering
      props.myFilter.limit,
      props.filter.showLiked,
      props.filter.buFiltervalue,
      props.filter.offeringsFiltervalue,
      props.filter.solutionsFiltervalue,
      props.filter.industryFiltervalue,
      props.filter.subIndustryFiltervalue,
      props.filter.locationFiltervalue,
      props.filter.isAllocatedFiltervalue,
      props.filter.startDateFiltervalue,
      props.filter.endDateFiltervalue,
      props.myFilter.sortColumn,
      "desc"
    );
  };
  const sortHandlerDown = () => {
    //console.log("sortHandlerDown");
    props.GetAllProjectDetailsForMarketPlaceData(
      1, // reset the pagintaion for filtering
      props.myFilter.limit,
      props.filter.showLiked,
      props.filter.buFiltervalue,
      props.filter.offeringsFiltervalue,
      props.filter.solutionsFiltervalue,
      props.filter.industryFiltervalue,
      props.filter.subIndustryFiltervalue,
      props.filter.locationFiltervalue,
      props.filter.isAllocatedFiltervalue,
      props.filter.startDateFiltervalue,
      props.filter.endDateFiltervalue,
      props.myFilter.sortColumn,
      "asc"
    );
  };

  return (
    <div>
      <Grid container xs={12}>
        <Grid className="gridOne" container alignItems="center">
          <Grid item xs={0.9}>
            <Grid container alignItems="center">
              <KeyboardArrowUpOutlinedIcon
                className="sortIcon-btn"
                onClick={sortHandlerUp}
              />
              <KeyboardArrowDownOutlinedIcon
                className="sortIcon-btn"
                onClick={sortHandlerDown}
              />
            </Grid>
          </Grid>
          <Grid>
            <Typography sx={{ ...constant.label }}>Sort By:</Typography>
          </Grid>
          <Grid className="gridOne select-control-market" item>
            <Autocomplete
              {...flatProps}
              value={props.selectedValueForSorting}
              onChange={handleSelectionChange}
              id="flat-demo"
              size="small"
              disableClearable
              renderInput={(params) => (
                <TextField
                  style={{ width: "200px" }}
                  {...params}
                  label=""
                  // variant="standard"
                />
              )}
            />
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};
const sortingOption = [
  { title: "Most recent" },
  { title: "Availability in MarketPlace" },
];

export default Sortprojects;
