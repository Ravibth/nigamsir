import { useContext, useEffect, useState } from "react";
import {
  DeleteRequisitionById,
  getAllRequisitionByProjectCode,
} from "../../services/requisition/requisition";
import { LoaderContext } from "../../contexts/loaderContext";
import { SnackbarContext } from "../../contexts/snackbarContext";
import SystemSuggestionLayout, {
  ViewTypeOfSuggestions,
} from "../system-suggestions/system-suggestions-layout";
import RequisitionGrid from "./activerequisition/activerequisitiongrid/requisitiongrid";
import { IRequisitionFilterData } from "./IRequisitionFilterData";
import { filteredRequisitionList, getFilterDataForRequisition } from "./utils";
import { IRequisitionMaster } from "../../common/interfaces/IRequisition";

const ActiveRequisitionsDeatils = (props: any) => {
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);
  const [requisitionSelected, setRequisitionSelected] =
    useState<IRequisitionMaster | null>(null);
  const [requisitionsList, setRequisitionsList] = useState<
    Array<IRequisitionMaster>
  >([]);
  const [requisitionsSelected1, setRequisitionsSelected1] = useState([]);
  const [filterData, setFilterData] = useState<IRequisitionFilterData>();
  const [startDate, setStartDate] = useState<string>();
  const [endDate, setEndDate] = useState<string>();
  const [submittedFilterData, setSubmittedFilterData] = useState<any>({});

  const fetchAllRequisitions = (
    pipelineCode: string,
    jobCode: string
  ): Promise<any> => {
    return new Promise((resolve, reject) => {
      getAllRequisitionByProjectCode(pipelineCode, jobCode, true)
        .then((requisitions) => {
          setRequisitionsList([]);
          const activeRequisition = requisitions.data;
          if (activeRequisition && activeRequisition.length > 0) {
            const filterRequisitionData =
              getFilterDataForRequisition(activeRequisition);
          }
          setRequisitionsList(activeRequisition);
          setRequisitionsSelected1(activeRequisition);
          resolve(requisitions.data);
        })
        .catch(() => {
          reject("");
        });
    });
  };

  const deleteRequisition = (requisitionId: string) => {
    return new Promise((resolve, reject) => {
      DeleteRequisitionById(requisitionId)
        .then((resp) => {
          Promise.all([
            fetchAllRequisitions(
              props.projectDetails.pipelineCode,
              props.projectDetails.jobCode
            ),
          ])
            .then(() => {
              loaderContext.open(false);
            })
            .catch(() => {
              snackbarContext.displaySnackbar(
                "Error fetching requisitions",
                "error"
              );
              loaderContext.open(false);
            });
          resolve("");
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  const resetActiveRequisitions = () => {
    setRequisitionSelected(null);
    loaderContext.open(true);
    Promise.all([
      fetchAllRequisitions(
        props.projectDetails.pipelineCode,
        props.projectDetails.jobCode
      ),
    ])
      .then(() => {
        loaderContext.open(false);
      })
      .catch(() => {
        snackbarContext.displaySnackbar("Error fetching requisitions", "error");
        loaderContext.open(false);
      });
  };

  useEffect(() => {
    resetActiveRequisitions();
  }, [props?.projectDetails.pipelineCode]);

  const selectedDataByFilter = (filterData: IRequisitionFilterData) => {
    setSubmittedFilterData((prevData: any) => {
      return {
        ...filterData,
      };
    });
    filterData.startDate = startDate as string;
    filterData.endDate = endDate as string;
    const filteredDataList = filteredRequisitionList(
      requisitionsSelected1,
      filterData
    );
    setRequisitionsList(filteredDataList);
  };

  const handleResetClick = (data: IRequisitionFilterData) => {
    setFilterData(data);
  };

  const handleStartDateChange = (date: string) => {
    setStartDate(date);
  };
  const handleEndDateChange = (date: string) => {
    setEndDate(date);
  };

  return (
    <>
      {requisitionSelected ? (
        <SystemSuggestionLayout
          requisitionId={requisitionSelected.id}
          requisitionDetails={requisitionSelected}
          // setRequisitionSelected={setRequisitionSelected}
          resetActiveRequisitions={resetActiveRequisitions}
          projectInfo={props.projectDetails}
          showBackActionButton={true}
          baseViewType={ViewTypeOfSuggestions.CardsView}
          showSuggestionsViewToggle={false}
          openCommonAllocationInPopup={false}
          useCustomFilter={true}
          useGridFilter={false}
          useLazyLoading={true}
        />
      ) : (
        <div>
          <RequisitionGrid
            submittedFilterData={submittedFilterData}
            requisitionsList={requisitionsList}
            setRequisitionSelected={setRequisitionSelected}
            deleteRequisition={deleteRequisition}
            setRequisitionsList={setRequisitionsList}
            handleResetClick={handleResetClick}
            handleStartDateChange={handleStartDateChange}
            handleEndDateChange={handleEndDateChange}
            selectedDataByFilter={selectedDataByFilter}
            // pipelineCode={props.pipelineCode}
            // jobCode={props.jobCode}
            projectDetails={props.projectDetails}
          />
          {/* {requisitionsList.map((requisition: IRequisitonDetails) => (
            <Grid item xs={12} key={requisition.requisionId}>
              <span onClick={() => setRequisitionSelected(requisition)}>
                {requisition.requisionId}
              </span>
            </Grid>
          ))} */}
        </div>
      )}
    </>
  );
};
export default ActiveRequisitionsDeatils;
