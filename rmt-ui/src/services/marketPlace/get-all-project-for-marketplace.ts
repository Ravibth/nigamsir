import axios from "axios";
import { IGetAllProjectDetailsForMarketPlace } from "../../components/Marketplace/interfaces/marketplaceinterfaces";

const baseurl = process.env.REACT_APP_MARKETPLACE;

export const GetAllMarketPlaceFilters = async () => {
  try {
    return await axios
      .get(baseurl + `MarketPlace/GetAllMarketPlaceFilters`)
      .then((resp: any) => {
        return resp.data;
      });
  } catch (err) {
    throw err;
  }
};

export const GetAllProjectDetailsForMarketPlaceAPI = async (
  pagination: number,
  limit: number,
  currentDateValue: any,
  showLiked: boolean,
  emailId: string,
  buFiltervalue: string[],
  offeringsFiltervalue: string[],
  solutionsFiltervalue: string[],
  industryFiltervalue: string[],
  subIndustryFiltervalue: string[],
  locationFiltervalue: string[],
  isAllocatedFiltervalue: string[],
  startDateFiltervalue: any,
  endDateFiltervalue: any,
  selectedValueForSorting: string,
  orderBy: string
) => {
  var flag =
    isAllocatedFiltervalue.length == 1 && isAllocatedFiltervalue[0] == "Yes"
      ? true
      : isAllocatedFiltervalue.length == 1 && isAllocatedFiltervalue[0] == "No"
      ? false
      : null;

  var payload: IGetAllProjectDetailsForMarketPlace = {
    limit: limit,
    pagination: pagination,
    currentDateValue: currentDateValue,
    showLiked: showLiked,
    emailId: emailId,
    buFiltervalue: buFiltervalue,
    offeringsFiltervalue: offeringsFiltervalue,
    solutionsFiltervalue: solutionsFiltervalue,
    industryFiltervalue: industryFiltervalue,
    subIndustryFiltervalue: subIndustryFiltervalue,
    locationFiltervalue: locationFiltervalue,
    isAllocatedToProject: flag,
    startDateFiltervalue: startDateFiltervalue,
    endDateFiltervalue: endDateFiltervalue,
    selectedValueForSorting: selectedValueForSorting,
    orderBy: orderBy,
  };

  try {
    return await axios
      .post(baseurl + `MarketPlace/GetAllProjectDetailsForMarketPlace`, payload)
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};

export const GetAllRequisitionByProjectCodeFromMP = async (
  mpRecordId: any,
  username: any,
  ScoreCalculationForRequisitionIdsAllowed: any
) => {
  try {
    const limit = 100;
    const pagination = 1;
    return await axios
      .get(
        `${baseurl}${`MarketPlace/GetAllRequisitionByProjectCode?id=`}${mpRecordId}&limit=${limit}&pagination=${pagination}&currentEmp=${username}&ScoreCalculationForRequisitionIdsAllowed=${ScoreCalculationForRequisitionIdsAllowed}`
      )
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
