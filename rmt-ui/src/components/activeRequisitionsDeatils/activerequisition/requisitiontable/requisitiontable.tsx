import * as React from "react";
import { useState } from "react";
import DeleteRequisitionModal from "../active-requisition-modal/delete-requisition-modal/delete-requisition-modal";
import ViewMoreRequisitionDetail from "../active-requisition-modal/view-more-requisition-detail-modal/view-more-requisition-detail";
import RequisitionAggrid from "./requisitionAggrid";
import { useNavigate } from "react-router-dom";
import { AgGridReact } from "ag-grid-react";
import { routeValueEncode } from "../../../../global/utils";

export default function Requisitiontable(props: any) {
  const [selectedRequisitionId, setSelectedRequisitionId] = useState("");
  const [open, setOpen] = useState(false);
  const [isViewMorePopOpen, setIsViewMorePopOpen] = useState(false);
  const navigate = useNavigate();
  const gridComponentRef = React.useRef<AgGridReact | null>(null);
  const handleOpen = (e: string) => {
    setOpen(true);
    setSelectedRequisitionId(e);
  };
  const handleCloseModal = () => setOpen(false);
  const handleCloseViewDetailModal = () => setIsViewMorePopOpen(false);
  const [selectedRowData, setSelectedRowData] = useState(null);

  const categoryValue = (row: any, item: any) => {
    let value = "";
    if (item?.category.toLowerCase() === "skill") {
      value = row.requisitionSkill
        .map((skill: any) => skill?.skillName)
        .join(", ");
    } else if (item?.category.toLowerCase() === "location") {
      value = row?.requisitionLocation
        .map((location: any) => location?.location)
        .join(", ");
    } else if (item?.category.toLowerCase() === "same client exp.") {
      value = "Yes";
    } else {
      value = row[item.category.toLowerCase()];
    }
    return (
      <div>{`${item.category} (${item?.requisitonWeight}) :  ${
        value || ""
      }`}</div>
    );
  };

  const openViewMoreDetailsModal = (rowData: any) => {
    setSelectedRowData(rowData);
    setIsViewMorePopOpen(true);
  };

  const handleNavigationToUpdateRequisitionForm = (
    event: any,
    requisition: any,
    pipelineCode: string,
    jobCode: string
  ) => {
    event.preventDefault();

    navigate(
      `/update-requisition/${routeValueEncode(
        requisition.pipelineCode
      )}/${routeValueEncode(requisition.jobCode)}/${requisition.id}`
    );
  };

  return (
    <>
      <DeleteRequisitionModal
        open={open}
        selectedRequisitionId={selectedRequisitionId}
        handleOpen={handleOpen}
        handleCloseModal={handleCloseModal}
        deleteRequisition={props.deleteRequisition}
        setRequisitionsList={props.setRequisitionsList}
        requisitionList={props.requisitionsList}
      />
      <ViewMoreRequisitionDetail
        isViewMorePopOpen={isViewMorePopOpen}
        handleCloseViewDetailModal={handleCloseViewDetailModal}
        requisitionsList={props.requisitionsList}
        categoryValue={categoryValue}
        selectedRowData={selectedRowData}
      />

      <>
        <RequisitionAggrid
          requisitionsList={props.requisitionsList}
          categoryValue={categoryValue}
          handleOpen={handleOpen}
          gridComponentRef={gridComponentRef}
          setRequisitionSelected={props.setRequisitionSelected}
          openViewMoreDetailsModal={openViewMoreDetailsModal}
          handleNavigationToUpdateRequisitionForm={
            handleNavigationToUpdateRequisitionForm
          }
          projectDetails={props.projectDetails}
        />
      </>
    </>
  );
}
