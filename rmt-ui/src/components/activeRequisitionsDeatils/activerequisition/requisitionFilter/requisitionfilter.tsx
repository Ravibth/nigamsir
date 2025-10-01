import { Box, Button } from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import * as constant from "./constant";
import FilterAltOutlinedIcon from "@mui/icons-material/FilterAltOutlined";
import RequisitionfilterDrawer from "./requisitionfilterdrawer";
import { IRequisitionFilterData } from "../../IRequisitionFilterData";
import {
  getBU_Exp_SME_RUFromWcgt,
  getDesignationFromWcgt,
} from "../../../../services/wcgt-master-services/wcgt-master-services";
import { SnackbarContext } from "../../../../contexts/snackbarContext";
import { LoaderContext } from "../../../../contexts/loaderContext";
import MainFilter from "../../../common/images/MainFilter.png";

const Requisitionfilter = (props: any) => {
  const snackbarContext: any = useContext(SnackbarContext);
  const loaderContext: any = useContext(LoaderContext);
  const [designationData, setDesignationData] = useState([]);
  const [designationList, setDesignationList] = useState<string[]>([]);

  const [buTreeMaster, setBuTreeMaster] = useState([]);
  const [openDrawer, setOpenDrawer] = useState(false);
  const [filterData, setfilterData] = useState<IRequisitionFilterData>();
  const [filterDefaultValue, setFilterDefaultValue] = useState<any>({
    designation: [],
    experties: [],
    sme: [],
    businessUnit: [],
    revenueUnit: [],
  });

  useEffect(() => {
    setFilterDefaultValue(props.submittedFilterData);
  }, [props.submittedFilterData]);

  // Get All Designation
  // const getAllDesignation = () => {
  //   return new Promise((resolve, reject) => {
  //     getDesignationFromWcgt().then((resp) => {
  //       console.log(resp.data);
  //       resolve(resp.data);
  //     });
  //   }).catch((err) => {
  //     snackbarContext.displaySnackbar("Error Fetching Users", "error", 6000);
  //   });
  // };

  // const getBUListTreeMaster = () => {
  //   return new Promise((resolve, reject) => {
  //     getBU_Exp_SME_RUFromWcgt().then((resp) => {
  //       console.log(resp.data);
  //       resolve(resp.data);
  //     });
  //   }).catch((err) => {
  //     snackbarContext.displaySnackbar("Error Fetching Users", "error", 6000);
  //   });
  // };

  // const onLoadPageApiCalls = () => {
  //   loaderContext.open(true);
  //   Promise.all([getAllDesignation(), getBUListTreeMaster()])
  //     .then((values: any) => {
  //       setDesignationData(values[0]);
  //       setBuTreeMaster(values[1]);
  //       loaderContext.open(false);
  //     })
  //     .catch(() => {
  //       snackbarContext.displaySnackbar("Error in fetching Data", "error");
  //       loaderContext.open(false);
  //     });
  // };

  // useEffect(() => {
  //   onLoadPageApiCalls();
  //   // designationDataList();
  // }, []);

  useEffect(() => {
    designationDataList();
  }, [designationData]);

  const designationDataList = () => {
    const design = designationData.map((item: any) => item.designation_name);
    setDesignationList(design);
  };

  return (
    <>
      <Button
        variant="outlined"
        sx={constant.filterIconButton}
        onClick={() => {
          setOpenDrawer(true);
        }}
      >
        {/* <FilterAltOutlinedIcon /> */}
        <img src={MainFilter} alt="upload" />
      </Button>
      {openDrawer ? (
        <Box mt={2} ml={2} mr={2} mb={2}>
          <RequisitionfilterDrawer
            defaultValue={filterDefaultValue}
            filterData={filterData}
            openDrawer={openDrawer}
            setOpenDrawer={setOpenDrawer}
            handleResetClick={props.handleResetClick}
            handleStartDateChange={props.handleStartDateChange}
            handleEndDateChange={props.handleEndDateChange}
            selectedDataByFilter={props.selectedDataByFilter}
            designationList={designationList}
          />
        </Box>
      ) : (
        <></>
      )}
    </>
  );
};

export default Requisitionfilter;
