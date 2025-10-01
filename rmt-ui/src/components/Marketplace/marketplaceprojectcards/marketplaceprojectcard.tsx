import * as React from "react";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Marketplacetitle from "./marketplacetitle/marketplacetitle";
import Marketplacedescription from "./marketplacedescription/marketplacedescription";
import Marketprofile from "./marketplaceprofiles/marketprofiles";
import Marketprojectdetails from "./marketprojectdetails/marketprojectdetails";
import Marketdesignationskill from "./marketplacedesignation/marketdesignationskill";
import { Grid } from "@mui/material";
import ActionButton from "../../actionButton/actionButton";
import { useState } from "react";
import { GetAllRequisitionByProjectCode } from "../../../services/allocation/getAllRequisitionByProjectCode";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import { GetAllRequisitionByProjectCodeFromMP } from "../../../services/marketPlace/get-all-project-for-marketplace";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";
import * as constant from "./marketprojectdetails/constant";
import { CardMainContainer } from "../sortprojects/constant";
import Loader from "../../loader/loader";
export default function Marketplaceprojectcard(props: any) {
  const [detailCardView, setdetailCardView] = useState(0);
  const [detailCardViewInfo, setdetailCardViewInfo] = useState<any[]>([]); //create interface for datatype
  const [isLoader, setIsLoader] = useState(null);

  const username = React.useContext(UserDetailsContext)?.username;

  const GetAllRequisitionByProjectCodeInfo = async (
    pipelineCode: any,
    jobCode: any
  ) => {
    //const maskedValue = "xxxxxxxx";
    //if (pipelineCode == maskedValue || jobCode == maskedValue) {
    setIsLoader(true);
    //console.log("props.projectDetail", props.projectDetail);
    //create interface for datatype
    const RequisitionData: any = await GetAllRequisitionByProjectCodeFromMP(
      props.projectDetail.id,
      username,
      true
    );
    setdetailCardView(1);
    setdetailCardViewInfo(RequisitionData);
    setIsLoader(false);
    // } else {
    //   const RequisitionData: any = await GetAllRequisitionByProjectCode(
    //     pipelineCode,
    //     jobCode,
    //     username,
    //     true
    //   );
    //   setdetailCardViewInfo(RequisitionData);
    // }
  };

  return (
    <Card sx={constant.mainCard}>
      <CardContent>
        <Marketplacetitle
          detailCardViewInfo={detailCardViewInfo}
          projectDetail={props.projectDetail}
          GetAllRequisitionByProjectCodeInfo={
            GetAllRequisitionByProjectCodeInfo
          }
          getProjectList={props.getProjectList}
          // fetchDetailsConfig = {props.fetchDetailsConfig}
          likeButtonUpdateCallback={props.likeButtonUpdateCallback}
        />
        <Marketprojectdetails projectdetails={props.projectDetail} />
        <Marketplacedescription
          projectdescription={props.projectDetail.description}
        />

        <Marketprofile projectdetails={props.projectDetail} />

        {detailCardView === 1 && isLoader != true && (
          <Marketdesignationskill detailCardViewInfo={detailCardViewInfo} />
        )}

        <Grid
          container
          justifyContent="flex-end"
          alignItems="center"
          sx={CardMainContainer}
        >
          <Grid item xs={1}>
            <>
              {isLoader != true && (
                <>
                  <ActionButton
                    label={detailCardView === 0 ? "View more" : "View less"}
                    type="submit"
                    disabled={false}
                    onClick={async (e: any) => {
                      if (detailCardView === 1) {
                        setdetailCardView(0);
                      } else {
                        if (detailCardViewInfo.length === 0) {
                          //setdetailCardView(null);
                          GetAllRequisitionByProjectCodeInfo(
                            props.projectDetail.pipelineCode,
                            props.projectDetail.jobCode
                          );
                        }
                        setdetailCardView(1);
                      }
                    }}
                  />
                </>
              )}
              {(isLoader || !(detailCardView != null)) && (
                <>
                  <Loader small></Loader>
                </>
              )}
            </>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
}
