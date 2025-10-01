import * as React from "react";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Marketplacetitle from "../marketplaceprojectcards/marketplacetitle/marketplacetitle";
import Marketprojectdetails from "../marketplaceprojectcards/marketprojectdetails/marketprojectdetails";
import Marketprofile from "../marketplaceprojectcards/marketplaceprofiles/marketprofiles";
import Marketplacedescription from "../marketplaceprojectcards/marketplacedescription/marketplacedescription";
import Marketdesignationskill from "../marketplaceprojectcards/marketplacedesignation/marketdesignationskill";

//Comment- This control is not used now
const Allocatedprojectcard = () => {
  return (
    <Card sx={{ minWidth: 275, marginLeft: "40px", marginTop: "20px" }}>
      <CardContent>
        <Marketplacetitle />
        <Marketplacedescription />
        <Marketprojectdetails />
        <Marketprofile />
        <Marketdesignationskill />
      </CardContent>
      {/* <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions> */}
    </Card>
  );
};

export default Allocatedprojectcard;
