import React, { useEffect, useState } from "react";
import { Checkbox, Divider, Grid, Tooltip, Typography } from "@mui/material";
import * as constant from "./constant";
import Sortprojects from "../sortprojects/sortprojects";
import Filtermarketplaceproject from "../filtermarketplaceproject/filtermarketplaceproject";
import {
  GetNotificationSubscription,
  UpdateNotificationSubscription,
} from "../../../services/marketPlace/empProjectInterest";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import {
  SUBSCRIPTION_MODULES,
  SUBSCRIPTION_ROLES,
} from "../../../global/constant";
import "./style.css";
import MySwitch from "../marketplaceswitch/switch";
import InfoIcon from "@mui/icons-material/Info";
import FilterAltIcon from "@mui/icons-material/FilterAlt";
import { VerticalCenterAlignSxProps } from "../../common-allocation/user-info-timeline-group/style";

const label = { inputProps: { "aria-label": "Checkbox demo" } };
const Heading = (props: any) => {
  const [filter, setFilter] = useState({
    buFiltervalue: [],
    offeringsFiltervalue: [],
    solutionsFiltervalue: [],
    industryFiltervalue: [],
    subIndustryFiltervalue: [],
    locationFiltervalue: [],
    isAllocatedFiltervalue: [],
    startDateFiltervalue: null,
    endDateFiltervalue: null,
    sortColumn: null,
    sortOrder: null,
  });
  const [isSubscribed, setIsSubscribed] = useState(false);
  const userContext = React.useContext(UserDetailsContext);
  const { isInterested, setIsInterested } = props;

  useEffect(() => {
    getNotificationSubscriptionData();
  }, []);

  const getNotificationSubscriptionData = async () => {
    try {
      var val = await GetNotificationSubscription(
        userContext.username,
        SUBSCRIPTION_MODULES.MARKETPLACE,
        SUBSCRIPTION_ROLES.MARKETPLACE_LISTING
      );
      //set data to state
      if (val && val.data && val.data.length) {
        var flag = val.data[0].is_active;
        setIsSubscribed(flag);
      }
    } catch (err) {}
  };

  const handleChange = async (e) => {
    var flag = e.target.checked === true;
    var payload = {
      module: SUBSCRIPTION_MODULES.MARKETPLACE,
      subscription_role: SUBSCRIPTION_ROLES.MARKETPLACE_LISTING,
      user_emailid: userContext.username,
      user_name: userContext.name,
      is_active: flag,
      createdDate: new Date(),
      modifiedDate: new Date(),
      createdBy: userContext.username,
      modifiedBy: userContext.username,
    };
    await UpdateNotificationSubscription(payload);
    setIsSubscribed(flag);
  };
  const [isFilterApplied, setIsFilterApplied] = useState<boolean>(false);

  const checkIfIsFilterApplied = () => {
    if (filter && Object.values(filter).find((v: any) => v && v.length > 0)) {
      setIsFilterApplied(true);
      return true;
    } else {
      setIsFilterApplied(false);
      return false;
    }
  };

  useEffect(() => {
    checkIfIsFilterApplied();
  }, [filter]);

  return (
    <div>
      <Grid
        container
        spacing={2}
        sx={{
          ...VerticalCenterAlignSxProps,
          paddingTop: "10px",
          paddingBottom: "20px",
        }}
      >
        <Grid item xs={12} sx={VerticalCenterAlignSxProps}>
          <Typography sx={constant.typographyHeading}>
            Recommended Projects For You
          </Typography>
        </Grid>

        <Grid item sx={VerticalCenterAlignSxProps}>
          <Filtermarketplaceproject
            GetAllProjectDetailsForMarketPlaceData={
              props.GetAllProjectDetailsForMarketPlaceData
            }
            filter={filter}
            setFilter={setFilter}
            myFilter={props.myFilter}
            setMyFilter={props.setMyFilter}
          />
        </Grid>
        <Grid item sx={VerticalCenterAlignSxProps}>
          {isFilterApplied && (
            <Tooltip title="Filters applied">
              <FilterAltIcon fontSize="large" />
            </Tooltip>
          )}
        </Grid>

        <Grid item sx={VerticalCenterAlignSxProps}>
          <Sortprojects
            GetAllProjectDetailsForMarketPlaceData={
              props.GetAllProjectDetailsForMarketPlaceData
            }
            filter={filter}
            setFilter={setFilter}
            myFilter={props.myFilter}
            setMyFilter={props.setMyFilter}
            selectedValueForSorting={props.selectedValueForSorting}
            setSelectedValueForSorting={props.setSelectedValueForSorting}
          />
        </Grid>
        <Grid item sx={VerticalCenterAlignSxProps}>
          <MySwitch
            GetAllProjectDetailsForMarketPlaceData={
              props.GetAllProjectDetailsForMarketPlaceData
            }
            myFilter={props.myFilter}
            isInterested={isInterested}
            setIsInterested={setIsInterested}
          />
        </Grid>
        <Grid item sx={VerticalCenterAlignSxProps}>
          {
            <div className="RequisitionClosure subscribe-container">
              <Checkbox
                {...label}
                checked={isSubscribed}
                onChange={(e) => {
                  handleChange(e);
                }}
              />
              <span>{"Subscribe to Marketplace Listings"}</span>
              <Tooltip
                sx={{ marginLeft: "5px", marginTop: "-5px" }}
                className={"tool-requisition"}
                title={
                  "Activate daily email alerts for new listings in the Marketplace"
                }
                placement="top"
              >
                <InfoIcon />
              </Tooltip>
            </div>
          }
        </Grid>
      </Grid>
      <Divider sx={{ backgroundColor: "rgba(0, 0, 0, 0.05)", width: "100%" }} />
    </div>
  );
};

export default Heading;
