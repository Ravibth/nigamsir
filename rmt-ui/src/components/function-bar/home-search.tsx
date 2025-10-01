import React, { useContext, useEffect, useState } from "react";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { getAllProjectListByRequestorEmail } from "../../services/project-list-services/project-list-services";
import SearchBar from "../search/searchbar";
import { RESPONSE_STATUS } from "../../global/constant";
import SearchbarText from "../search/searchbartext";
import SearchText from "../search/searchbartext";
import { eachDayOfInterval } from "date-fns";
import { IProjectList } from "../../services/project-list-services/IProjectList";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";

const HomeSearch = (props?: any) => {
  const userDetails = React.useContext(UserDetailsContext);
  const [loader, setLoader] = useState<boolean>(false);
  const [projectLists, setProjectLists] = useState([]);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  useEffect(() => {
    //get data only when already does not available on client side
    if (projectLists !== null && projectLists.length > 0) {
      const pageLimitConst: number = Number(
        process.env.REACT_APP_PROJECTLISTING_PAGESIZE
          ? process.env.REACT_APP_PROJECTLISTING_PAGESIZE
          : 25
      ); //projectlisting page size limit
      const request: IProjectList = {
        userEmail: userDetails.username,
        limit: pageLimitConst,
        pagination: 1,
        filterQueryParameters: {},
      };
      Promise.all([getAllProjectListByRequestorEmail(request)]).then((resp) => {
        setLoader(false);
        loaderContext.open(false);
        if (resp.length > 0 && resp[0].status === RESPONSE_STATUS.Success) {
          const _projectLists = [];
          resp[0].data.forEach((element) => {
            _projectLists.push({
              label: element.projectName,
              pipelineCode: element.pipelineCode,
              jobCode: element.jobCode,
            });
          });
          setProjectLists(_projectLists);
        }
      });
    } else {
    }
  }, [userDetails]);

  const searchQueryHandle = (e: any) => {
    props.searchQueryHandle(e.currentTarget.value);
  };
  return (
    <>
      <SearchText
        onAutocompleteTextChange={(e) => searchQueryHandle(e)}
      ></SearchText>
    </>
  );
};

export default HomeSearch;
