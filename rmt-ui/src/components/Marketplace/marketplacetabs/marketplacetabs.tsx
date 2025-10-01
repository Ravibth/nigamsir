import React, { memo, useEffect, useRef, useState } from "react";
import { Container } from "@mui/material";
import Marketplaceprojectcard from "../marketplaceprojectcards/marketplaceprojectcard";
import Heading from "../Heading/heading";
import { GetAllProjectDetailsForMarketPlaceAPI } from "../../../services/marketPlace/get-all-project-for-marketplace";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";

import _ from "lodash";
import "./style.css";
import * as constant from "./constant";
import Loader from "../../loader/loader";
import { errorMessage } from "../../../services/log-services/log-service";
import moment from "moment";
import {
  IMarketPlaceDataObject,
  IMarketPlaceProjectDetailDTO,
} from "../interfaces/marketplaceinterfaces";

export interface IMarketPlaceFilter {
  pagination: number;
  limit: number;
  showLiked: boolean;
  buFiltervalue: string[];
  offeringsFiltervalue: string[];
  solutionsFiltervalue: string[];
  industryFiltervalue: string[];
  subIndustryFiltervalue: string[];
  locationFiltervalue: string[];
  isAllocatedFiltervalue: string[];
  startDateFiltervalue: any;
  endDateFiltervalue: any;
  sortColumn: string;
  sortOrder: string;
}

//state is not saving in this control so convertred to global variable
let myFilter: IMarketPlaceFilter = {
  pagination: 1,
  limit: 10,
  showLiked: null,
  buFiltervalue: [],
  offeringsFiltervalue: [],
  solutionsFiltervalue: [],
  industryFiltervalue: [],
  subIndustryFiltervalue: [],
  locationFiltervalue: [],
  isAllocatedFiltervalue: [],
  startDateFiltervalue: null,
  endDateFiltervalue: null,
  sortColumn: "", //Empty string means default sort by marketPlacePublishDate
  sortOrder: "desc",
};

//state is not saving in this control so convertred to global variable
let list_ended: boolean = false;
let isLoading: boolean = true;

function Marketplacetabs() {
  const setMyFilterMethod = (marketPlaceFilter: IMarketPlaceFilter) => {
    var _obj: IMarketPlaceFilter = {
      pagination: marketPlaceFilter.pagination,
      limit: marketPlaceFilter.limit,
      showLiked: marketPlaceFilter.showLiked,
      buFiltervalue: marketPlaceFilter.buFiltervalue,
      offeringsFiltervalue: marketPlaceFilter.offeringsFiltervalue,
      solutionsFiltervalue: marketPlaceFilter.solutionsFiltervalue,
      industryFiltervalue: marketPlaceFilter.industryFiltervalue,
      subIndustryFiltervalue: marketPlaceFilter.subIndustryFiltervalue,
      locationFiltervalue: marketPlaceFilter.locationFiltervalue,
      isAllocatedFiltervalue: marketPlaceFilter.isAllocatedFiltervalue,
      startDateFiltervalue: marketPlaceFilter.startDateFiltervalue,
      endDateFiltervalue: marketPlaceFilter.endDateFiltervalue,
      sortColumn: marketPlaceFilter.sortColumn,
      sortOrder: marketPlaceFilter.sortOrder,
    };
    myFilter = _obj;
    // console.log(JSON.stringify(_obj));
  };

  const marketplacecontainerRef = useRef(null);
  const userDetailsContext = React.useContext(UserDetailsContext);

  const [isInterestedInProject, setIsInterestedInProject] = useState(false);
  const [projectDetailForMarketPlace, setProjectDetailForMarketPlace] =
    useState<any[]>([]);
  const setIsLoading = (flag: boolean) => {
    isLoading = flag;
  };

  const setListEnded = (isFlag: boolean): void => {
    list_ended = isFlag;
  };

  const [selectedValueForSorting, setSelectedValueForSorting] =
    useState("Most recent");

  const debounceFetchSuggestion = (fn: any, delay: number) => {
    let timeout: any;
    return (...args: any) => {
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        fn(...args);
      }, delay);
    };
  };

  const handleScroll = () => {
    const element: any = marketplacecontainerRef.current;
    let scrollPosition = element.scrollHeight - element.scrollTop;
    if (
      scrollPosition <= element.clientHeight + 5 &&
      scrollPosition > element.clientHeight - 5 &&
      !isLoading
    ) {
      fetchnewDataListener();
    }
  };

  useEffect(() => {
    const element: any = marketplacecontainerRef.current;
    const debounceScroll = debounceFetchSuggestion(handleScroll, 300);

    if (element) {
      // Add a scroll event listener to the specific component
      element.addEventListener("scroll", debounceScroll);
      // Remove the event listener when the component is unmounted
      return () => {
        element.removeEventListener("scroll", debounceScroll);
      };
    }
  }, []);

  const fetchnewDataListener = async () => {
    if (isLoading || list_ended) return;

    let myNewFilters: IMarketPlaceFilter = {
      pagination: myFilter.pagination + 1,
      limit: myFilter.limit,
      showLiked: myFilter.showLiked,
      buFiltervalue: myFilter.buFiltervalue,
      offeringsFiltervalue: myFilter.offeringsFiltervalue,
      solutionsFiltervalue: myFilter.solutionsFiltervalue,
      industryFiltervalue: myFilter.industryFiltervalue,
      subIndustryFiltervalue: myFilter.subIndustryFiltervalue,
      locationFiltervalue: myFilter.locationFiltervalue,
      isAllocatedFiltervalue: myFilter.isAllocatedFiltervalue,
      startDateFiltervalue: myFilter.startDateFiltervalue,
      endDateFiltervalue: myFilter.endDateFiltervalue,
      sortColumn: myFilter.sortColumn,
      sortOrder: myFilter.sortOrder,
    } as IMarketPlaceFilter;

    setMyFilterMethod(myNewFilters);

    const resultData: IMarketPlaceDataObject =
      await GetAllProjectDetailsForMarketPlaceFromService(
        myNewFilters.pagination,
        myNewFilters.limit,
        myNewFilters.showLiked,
        myNewFilters.buFiltervalue,
        myNewFilters.offeringsFiltervalue,
        myNewFilters.solutionsFiltervalue,
        myNewFilters.industryFiltervalue,
        myNewFilters.subIndustryFiltervalue,
        myNewFilters.locationFiltervalue,
        myNewFilters.isAllocatedFiltervalue,
        myNewFilters.startDateFiltervalue,
        myNewFilters.endDateFiltervalue,
        myNewFilters.sortColumn,
        myNewFilters.sortOrder
      );

    if (resultData.marketplaceProjects.length > 0) {
      setProjectDetailForMarketPlace((prev: any[]) => [
        ...prev,
        ...resultData.marketplaceProjects,
      ]);
    } else {
      setListEnded(true);
    }

    setIsLoading(false);
  };

  useEffect(() => {
    GetAllProjectDetailsForMarketPlaceData(
      1,
      myFilter.limit,
      myFilter.showLiked,
      myFilter.buFiltervalue,
      myFilter.offeringsFiltervalue,
      myFilter.solutionsFiltervalue,
      myFilter.industryFiltervalue,
      myFilter.subIndustryFiltervalue,
      myFilter.locationFiltervalue,
      myFilter.isAllocatedFiltervalue,
      myFilter.startDateFiltervalue,
      myFilter.endDateFiltervalue,
      myFilter.sortColumn,
      myFilter.sortOrder
    );
  }, []);

  useEffect(() => {
    GetAllProjectDetailsForMarketPlaceData(
      1,
      myFilter.limit,
      myFilter.showLiked,
      myFilter.buFiltervalue,
      myFilter.offeringsFiltervalue,
      myFilter.solutionsFiltervalue,
      myFilter.industryFiltervalue,
      myFilter.subIndustryFiltervalue,
      myFilter.locationFiltervalue,
      myFilter.isAllocatedFiltervalue,
      myFilter.startDateFiltervalue,
      myFilter.endDateFiltervalue,
      myFilter.sortColumn,
      myFilter.sortOrder
    );
  }, [isInterestedInProject, selectedValueForSorting]);

  const GetAllProjectDetailsForMarketPlaceFromService = async (
    pagination: number,
    limit: number,
    showLiked: boolean,
    buFiltervalue: any[],
    offeringsFiltervalue: any[],
    solutionsFiltervalue: any[],
    industryFiltervalue: any[],
    subIndustryFiltervalue: any[],
    locationFiltervalue: any[],
    isAllocatedFiltervalue: any[],
    startDateFiltervalue: any[],
    endDateFiltervalue: any[],
    sortColumn: string,
    sortOrder: string
  ) => {
    var _sortVal = "";
    if (sortColumn != null) {
      //selectedValueForSorting
      if (sortColumn?.toLowerCase() == "Most recent".toLowerCase()) {
        _sortVal = "marketPlacePublishDate";
      } else if (
        sortColumn?.toLowerCase() == "Availability in MarketPlace".toLowerCase()
      ) {
        _sortVal = "marketPlaceExpirationDate";
      }
    }
    let currentDateValue = new Date();

    let startDateFiltervalue1 = startDateFiltervalue
      ? moment(startDateFiltervalue).local().endOf("day")
      : null;
    let endDateFiltervalue1 = endDateFiltervalue
      ? moment(endDateFiltervalue).local().endOf("day")
      : null;
    let currentDateValue1 = currentDateValue
      ? moment(currentDateValue).local().endOf("day")
      : null;

    var resultData: IMarketPlaceProjectDetailDTO[] =
      await GetAllProjectDetailsForMarketPlaceAPI(
        pagination,
        limit,
        currentDateValue1,
        showLiked,
        userDetailsContext.username,
        buFiltervalue,
        offeringsFiltervalue,
        solutionsFiltervalue,
        industryFiltervalue,
        subIndustryFiltervalue,
        locationFiltervalue,
        isAllocatedFiltervalue,
        startDateFiltervalue1,
        endDateFiltervalue1,
        _sortVal,
        sortOrder?.toLowerCase() == "desc" ? "desc" : "asc"
      );
    var listInterested = resultData.filter(
      (projectMP: IMarketPlaceProjectDetailDTO) =>
        projectMP.isInterested === true
    );
    return {
      marketplaceProjects: resultData,
      interestedProjects: listInterested,
    } as IMarketPlaceDataObject;
  };

  const GetAllProjectDetailsForMarketPlaceData = async (
    pagination: number,
    limit: number,
    showLiked: boolean,
    buFiltervalue: any[],
    offeringsFiltervalue: any[],
    solutionsFiltervalue: any[],
    industryFiltervalue: any[],
    subIndustryFiltervalue: any[],
    locationFiltervalue: any[],
    isAllocatedFiltervalue: any[],
    startDateFiltervalue: any[],
    endDateFiltervalue: any[],
    sortColumn: string,
    sortOrder: string
  ) => {
    let myNewFilters: IMarketPlaceFilter = {
      pagination: pagination,
      limit: limit,
      showLiked: showLiked,
      buFiltervalue: buFiltervalue,
      offeringsFiltervalue: offeringsFiltervalue,
      solutionsFiltervalue: solutionsFiltervalue,
      industryFiltervalue: industryFiltervalue,
      subIndustryFiltervalue: subIndustryFiltervalue,
      locationFiltervalue: locationFiltervalue,
      isAllocatedFiltervalue: isAllocatedFiltervalue,
      startDateFiltervalue: startDateFiltervalue,
      endDateFiltervalue: endDateFiltervalue,
      sortColumn: sortColumn,
      sortOrder: sortOrder,
    } as IMarketPlaceFilter;
    try {
      setMyFilterMethod(myNewFilters);
      setListEnded(false);

      var res: IMarketPlaceDataObject =
        await GetAllProjectDetailsForMarketPlaceFromService(
          pagination,
          limit,
          showLiked,
          buFiltervalue,
          offeringsFiltervalue,
          solutionsFiltervalue,
          industryFiltervalue,
          subIndustryFiltervalue,
          locationFiltervalue,
          isAllocatedFiltervalue,
          startDateFiltervalue,
          endDateFiltervalue,
          sortColumn,
          sortOrder
        );

      setProjectDetailForMarketPlace(res.marketplaceProjects);
      setIsLoading(false);
    } catch (err) {
      console.log(err);
      errorMessage(
        "Marketplacetabs",
        "GetAllProjectDetailsForMarketPlaceData",
        err
      );
    }
  };

  const likeButtonUpdateCallback = (
    pipelineCode: any,
    jobCode: any,
    isLike: any
  ) => {
    var _projectDetail = [];
    for (var i = 0; i < projectDetailForMarketPlace.length; i++) {
      if (
        projectDetailForMarketPlace[i].pipelineCode == pipelineCode &&
        projectDetailForMarketPlace[i].jobCode == jobCode
      ) {
        projectDetailForMarketPlace[i].isInterested = isLike;
      }
      _projectDetail.push(projectDetailForMarketPlace[i]);
    }
    setProjectDetailForMarketPlace(_projectDetail);
  };

  return (
    <div>
      <div className="main-container-marketplace">
        <Container
          maxWidth="xl"
          ref={marketplacecontainerRef}
          sx={constant.mainTab}
        >
          <>
            {!isLoading && (
              <div
                style={{
                  flexDirection: "column",
                }}
              >
                <Heading
                  selectedValueForSorting={selectedValueForSorting}
                  setSelectedValueForSorting={setSelectedValueForSorting}
                  GetAllProjectDetailsForMarketPlaceData={
                    GetAllProjectDetailsForMarketPlaceData
                  }
                  myFilter={myFilter}
                  setMyFilter={setMyFilterMethod}
                  isInterested={isInterestedInProject}
                  setIsInterested={setIsInterestedInProject}
                />

                {isLoading != true && projectDetailForMarketPlace.length > 0 ? (
                  projectDetailForMarketPlace.map(function (projectDetail) {
                    return (
                      <Marketplaceprojectcard
                        key={projectDetail.id}
                        projectDetail={projectDetail}
                        getProjectList={GetAllProjectDetailsForMarketPlaceData}
                        likeButtonUpdateCallback={likeButtonUpdateCallback}
                      />
                    );
                  })
                ) : (
                  <div style={{ textAlign: "center", width: "100%" }}>
                    <br></br>
                    <br></br>
                    No Records Found!
                  </div>
                )}
                <div style={{ display: "hidden" }}>{list_ended && "-"}</div>
              </div>
            )}
            {isLoading && (
              <div style={{ textAlign: "center", width: "100%" }}>
                <Loader small={false} />
              </div>
            )}
          </>
        </Container>
      </div>
    </div>
  );
}

export default memo(Marketplacetabs);
