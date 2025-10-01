import { useEffect, useState } from "react";
import { Stack } from "@mui/material";
import Marketsubtitle from "../marketplacetitle/marketplacesubtitle/marketsubtitle";
import { ProjectChargeableType } from "../../../project-types/constant";
import moment from "moment";

const Marketprofile = (props: any) => {
  const [isPipeLine, setIsPipeLine] = useState(null);

  const marketplaceCardDateFormat = "DD-MM-YYYY";

  const pipeLinedata = [
    {
      label: "Published Date : ",
      value: moment(props.projectdetails.marketPlacePublishDate).format(
        marketplaceCardDateFormat
      ),
    },
    {
      label: "Expiration Date : ",
      value: moment(props.projectdetails.marketPlaceExpirationDate).format(
        marketplaceCardDateFormat
      ),
    },
    {
      label: "Proposed Client service partner : ",
      value: props.projectdetails.proposedCsp,
    },
    {
      label: "Proposed Engagement Leader : ",
      value: props.projectdetails.elForPipeLine,
    },
  ];
  const projectData = [
    {
      label: "Published Date : ",
      value: moment(props.projectdetails.marketPlacePublishDate).format(
        marketplaceCardDateFormat
      ),
    },
    {
      label: "Expiration Date : ",
      value: moment(props.projectdetails.marketPlaceExpirationDate).format(
        marketplaceCardDateFormat
      ),
    },
  ];

  //show jobmanager and smeg leader only for non chnareagble jobs
  if (
    props.projectdetails.chargableType?.toLowerCase() ==
    ProjectChargeableType.NonChargable.toLowerCase()
  ) {
    projectData.push(
      {
        label: "SMEG Leader : ",
        value: props.projectdetails.csp,
      },
      {
        label: "Job Manager : ",
        value: props.projectdetails.jobManager,
      }
    );
  } else if (
    props.projectdetails.chargableType?.toLowerCase() ==
    ProjectChargeableType.Chargable.toLowerCase()
  ) {
    projectData.push(
      { label: "CSP : ", value: props.projectdetails.csp },
      {
        label: "Engagement Leader : ",
        value: props.projectdetails.elForJob,
      }
    );
  }

  useEffect(() => {
    setIsPipeLine(props.projectdetails.ispipeLine);
  }, [props.projectdetails.ispipeLine]);

  return (
    <div style={{ paddingTop: "20px" }}>
      <Stack direction="row" spacing={6}>
        {isPipeLine != null && (
          <>
            {props.projectdetails.ispipeLine && (
              <Marketsubtitle
                projectDetail={props.projectDetail}
                data={pipeLinedata}
              />
            )}

            {!props.projectdetails.ispipeLine && (
              <Marketsubtitle
                projectDetail={props.projectDetail}
                data={projectData}
              />
            )}
          </>
        )}
      </Stack>
    </div>
  );
};

export default Marketprofile;
