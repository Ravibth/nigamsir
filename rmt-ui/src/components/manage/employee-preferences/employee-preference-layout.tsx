import React, { useContext, useEffect, useState } from "react";
import { SnackbarContext } from "../../../contexts/snackbarContext";
import { getEmployeePreferenceByEmail } from "../../../services/employee-preference/employee-preference.service";
import { LoaderContext } from "../../../contexts/loaderContext";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../../../contexts/userDetailsContext";
import BuMappingPreference from "./area-of-experties/bu-mapping-preference";
import LocationPreference from "./location/location-preference-3";
import IndustryPreference from "./industry/industry-preference";

const EmployeePreferenceLayout = (props: any) => {
  const {
    structuredPreferences,
    ManageData,
    buMappingRowData,
    setBuMappingRowData,
    locationRowData,
    setLocationRowData,
    industryRowData,
    setIndustryRowData,
    maxNumberOfPreference,
  } = props;
  const snackbarContext: any = useContext(SnackbarContext);
  const user: IUserDetailsContext = useContext(UserDetailsContext);
  const loaderContext: any = useContext(LoaderContext);

  const GetEmployeePreferenceData = (employeeEmail: string): Promise<any> => {
    return new Promise((resolve, reject) => {
      getEmployeePreferenceByEmail(employeeEmail)
        .then((resp) => {
          resolve(resp.data);
        })
        .catch((ex) => {
          snackbarContext.displaySnackbar(
            "Error Fetching Employee Preference",
            "error",
            6000
          );
          reject(ex);
        });
    });
  };
  const InitialDataRender = () => {
    Promise.all([GetEmployeePreferenceData(user.username)])
      .then((values) => {
        const EmployeePreferences: any = values[0];
        ManageData(EmployeePreferences);
        loaderContext.open(false);
      })
      .catch((error) => {
        loaderContext.open(false);
        console.log(error);
      });
  };
  useEffect(() => {
    loaderContext.open(true);
    InitialDataRender();
    return () => {
      ManageData([]);
    };
  }, [user]);
  return (
    <>
      <LocationPreference
        rowData={locationRowData}
        setRowData={setLocationRowData}
        employeePreference={structuredPreferences}
        maxNumberOfPreference={maxNumberOfPreference}
      />
      <BuMappingPreference
        rowData={buMappingRowData}
        setRowData={setBuMappingRowData}
        employeePreference={structuredPreferences}
        maxNumberOfPreference={maxNumberOfPreference}
      />
      <IndustryPreference
        rowData={industryRowData}
        setRowData={setIndustryRowData}
        employeePreference={structuredPreferences}
        maxNumberOfPreference={maxNumberOfPreference}
      />
    </>
  );
};

export default EmployeePreferenceLayout;
