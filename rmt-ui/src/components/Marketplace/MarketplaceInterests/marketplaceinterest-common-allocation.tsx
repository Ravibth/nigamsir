import { Typography } from "@mui/material";
import BackDropModal from "../../../common/back-drop-modal/backDropModal";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { AllocateEmployeesState } from "../../../contexts/allocateEmployeesContext";
import CommonAllocationWrapper from "../../common-allocation/commonAllocationWrapper";
import { EAllocationType } from "../../common-allocation/enum";

interface IMarketplaceInterestCommonAllocationProps {
  requisitionId: string;
  emailId: string;
  projectInfo: IProjectMaster;
  back: () => void;
}

const MarketplaceInterestCommonAllocation = (
  props: IMarketplaceInterestCommonAllocationProps
) => {
  const closePopup = () => {
    props.back();
  };

  return (
    <BackDropModal
      open={true}
      onclose={closePopup}
      restrictOnClose={true}
      style={{
        width: "95%",
        height: "90vh",
        borderRadius: "15px",
      }}
    >
      <Typography component="div" sx={{ marginTop: "20px" }}>
        <AllocateEmployeesState>
          <CommonAllocationWrapper
            back={function (): {} {
              closePopup();
              return;
            }}
            projectInfo={props.projectInfo}
            baseType={EAllocationType.NAME_ALLOCATION}
            baseUserEmailToSelect={[props.emailId]}
          />
        </AllocateEmployeesState>
      </Typography>
    </BackDropModal>
  );
};

export default MarketplaceInterestCommonAllocation;
