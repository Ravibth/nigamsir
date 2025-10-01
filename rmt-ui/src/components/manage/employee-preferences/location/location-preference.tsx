import React, { useEffect, useState } from "react";
import * as util from "./util";
import "./style.css";
import DeleteOutlineIcon from "@mui/icons-material/DeleteOutline";
import InfoIcon from "@mui/icons-material/Info";
import {
  Autocomplete,
  Avatar,
  Button,
  Grid,
  IconButton,
  InputAdornment,
  TextField,
  Tooltip,
} from "@mui/material";
import { useForm, Controller } from "react-hook-form";
import { PREFERENCE_CATEGORY } from "../constant";

const LocationPreferance = (props: any) => {
  const {
    selectedLocations,
    allLocations,
    handleChangeForUpdate,
    category,
    PreferenceTitle,
    EmployeeEmail,
    prefWithError,
  } = props;
  const [selected, setSelected] = useState(selectedLocations);
  const [all, setAll] = useState(allLocations);
  const [remaining, setRemaining] = useState([] as any[]);

  useEffect(() => {
    const selectVal = selectedLocations
      .filter(
        (data: any) =>
          data.category.toString().toUpperCase() ===
          category.toString().toUpperCase()
      )
      .sort((a: any, b: any) => a.preferenceOrder - b.preferenceOrder);
    // console.log(selectVal);
    const allVal = allLocations.filter(
      (data: any) =>
        data.category.toString().toUpperCase() ===
        category.toString().toUpperCase()
    );
    setSelected(selectVal);
    setAll(allVal);
    const remainingLocations = util.GetRemainingLocations(allVal, selectVal);
    setRemaining(remainingLocations);
  }, [selectedLocations, allLocations]);

  const handleDeleteClick = (data: any, index: number) => {
    let finalUpdated: any;
    let prevSelectedPref = selected;
    // let count = 0;
    // let index = -1;
    // prevSelectedPref.map((item: any, idx: number) => {
    //   if (item.isActive && count === activeIndex) {
    //     index = idx;
    //   }
    //   count++;
    // });
    //console.log(data, index);
    if (data.id === -1) {
      console.log(data, index);
      finalUpdated = prevSelectedPref.filter((item: any, idx: number) => {
        if (index === idx) {
          return false;
        } else {
          return true;
        }
      });
      // console.log(prevSelectedPref);
      //console.log("...delete", finalUpdated);
      setSelected(finalUpdated);
    } else {
      // prevSelectedPref.splice(index, 1);
      const updatedSelectedPref = prevSelectedPref.map((currPref: any) => {
        if (
          currPref.label.toString().toUpperCase() ===
          data.label.toString().toUpperCase()
        ) {
          return {
            ...currPref,
            isActive: false,
          };
        } else {
          return {
            ...currPref,
          };
        }
      });
      //console.log("...selected data for delete ", updatedSelectedPref);
      setRemaining(util.GetRemainingLocations(all, updatedSelectedPref));
      finalUpdated = updatedSelectedPref;
      setSelected(finalUpdated);
    }
    handleChangeForUpdate(finalUpdated);
  };

  const handleAddResources = () => {
    const currentSelected = selected;
    currentSelected.push({
      id: -1,
      label: "",
      employeeEmail: EmployeeEmail,
      category: category,
      preferedValue: -1,
      isActive: true,
      preferenceOrder: 0,
    });
    //console.log("...add", currentSelected);
    setSelected(currentSelected);
    // let finalUpdated: any;
    // const updatedRemainingData = remaining;
    // const addedValue = updatedRemainingData[0];
    // updatedRemainingData.splice(0, 1);
    // setRemaining((prevRemaining) => [...updatedRemainingData]);
    // const prevSelectedPref = selected;
    // let isPresent = false;
    // let DataToAdd: any;
    // const updatedSelectedArray = prevSelectedPref.filter((currPref: any) => {
    //   if (
    //     currPref.label.toString().toUpperCase() ===
    //     addedValue.label.toString().toUpperCase()
    //   ) {
    //     isPresent = true;
    //     DataToAdd = {
    //       ...currPref,
    //       isActive: true,
    //     };
    //   } else {
    //     return {
    //       ...currPref,
    //     };
    //   }
    // });
    // if (!isPresent) {
    //   updatedSelectedArray.push({
    //     id: 0,
    //     label: addedValue.label,
    //     category: category.toUpperCase(),
    //     employeeEmail: EmployeeEmail,
    //     preferedValue: addedValue.id,
    //     isActive: true,
    //   });
    // } else {
    //   updatedSelectedArray.push(DataToAdd);
    // }
    // finalUpdated = updatedSelectedArray;
    // console.log("selected data for add ", updatedSelectedArray);
    // setSelected(finalUpdated);
    handleChangeForUpdate(currentSelected);
    // console.log(selected);
  };

  const onHandleAnotherChange = (
    newValue: any,
    index: any,
    currentValue: any
  ) => {
    //console.log("....hi0...");

    if (newValue) {
      let finalUpdated: any;
      let prevSelectedPref = selected;
      let DataToReplace: any;
      let isPresent = false;
      //console.log("....hi1...");
      // let count = 0;
      // let index = -1;
      // prevSelectedPref.map((item: any, idx: number) => {
      //   console.log(item);
      //   if (item.isActive && count === activeIndex) {
      //     index = idx;
      //   }
      //   count++;
      // });
      //console.log(newValue, index, currentValue);
      if (currentValue.id === -1) {
        let indexToRemove = -1;
        //console.log("...", prevSelectedPref);
        // console.log(newValue, activeIndex, index, currentValue);
        prevSelectedPref.map((item: any, idx: number) => {
          if (
            item.label.toString().trim().toUpperCase() ===
            newValue.label.toString().trim().toUpperCase()
          ) {
            isPresent = true;
            indexToRemove = idx;
            DataToReplace = {
              ...item,
              isActive: true,
            };
          }
        });
        if (!isPresent) {
          DataToReplace = {
            id: 0,
            label: newValue.label,
            employeeEmail: EmployeeEmail,
            category: category,
            preferedValue: newValue.id,
            isActive: true,
            preferenceOrder: 0,
          };
        }
        let updatedSelectedArray2 = prevSelectedPref;
        updatedSelectedArray2[index] = DataToReplace;
        console.log(updatedSelectedArray2);
        if (isPresent) {
          finalUpdated = updatedSelectedArray2.filter(
            (item: any, idx: number) => {
              if (idx === indexToRemove) {
                return false;
              } else {
                return true;
              }
            }
          );
        } else {
          finalUpdated = updatedSelectedArray2;
        }
        console.log(finalUpdated);
        finalUpdated.push({ ...currentValue, isActive: false });
        setRemaining(util.GetRemainingLocations(all, finalUpdated));
        setSelected(finalUpdated);
      } else {
        let indexToRemove = -1;
        prevSelectedPref.map((currPref: any, idx: number) => {
          if (
            currPref.label.toString().toUpperCase() ===
            newValue.label.toString().toUpperCase()
          ) {
            isPresent = true;
            DataToReplace = {
              ...currPref,
              isActive: true,
            };
            indexToRemove = idx;
          }
        });
        if (!isPresent) {
          DataToReplace = {
            id: 0,
            label: newValue.label,
            employeeEmail: EmployeeEmail,
            preferedValue: newValue.id,
            category: category.toUpperCase(),
            isActive: true,
          };
        }
        let updatedSelectedArray = prevSelectedPref;
        updatedSelectedArray[index] = DataToReplace;
        let updatedSelectedArray2 = updatedSelectedArray;
        if (isPresent) {
          updatedSelectedArray2 = updatedSelectedArray.filter(
            (item: any, idx: number) => {
              if (idx === indexToRemove) {
                return false;
              } else {
                return true;
              }
            }
          );
        }
        updatedSelectedArray2.push({ ...currentValue, isActive: false });
        // if (currentValue.id == -1) {
        // }
        updatedSelectedArray2.push({ ...currentValue, isActive: false });
        setRemaining(util.GetRemainingLocations(all, updatedSelectedArray2));
        //console.log("selected data for modify ", updatedSelectedArray2);
        finalUpdated = updatedSelectedArray2;
        setSelected(finalUpdated);
        // const updatedSelectedArray = prevSelectedPref.filter(
        //   (currPref: any) => {
        //     if (
        //       currPref.label.toString().toUpperCase() ===
        //       newValue.label.toString().toUpperCase()
        //     ) {
        //       isPresent = true;
        //       DataToReplace = {
        //         ...currPref,
        //         isActive: true,
        //       };
        //     } else {
        //       return {
        //         ...currPref,
        //       };
        //     }
        //   }
        // );
        // if (!isPresent) {
        //   DataToReplace = {
        //     id: 0,
        //     label: newValue.label,
        //     employeeEmail: EmployeeEmail,
        //     preferedValue: newValue.id,
        //     category: category.toUpperCase(),
        //     isActive: true,
        //   };
        // }
        // const updatedSelectedArray2 = updatedSelectedArray.map((data: any) => {
        //   if (
        //     data.label.toString().toUpperCase() ===
        //     currentValue.label.toString().toUpperCase()
        //   ) {
        //     return {
        //       ...DataToReplace,
        //     };
        //   } else {
        //     return {
        //       ...data,
        //     };
        //   }
        // });
        // updatedSelectedArray2.push({ ...currentValue, isActive: false });
        // setRemaining(util.GetRemainingLocations(all, updatedSelectedArray2));
        // console.log("selected data for modify ", updatedSelectedArray2);
        // finalUpdated = updatedSelectedArray2;
        // setSelected(finalUpdated);
      }
      handleChangeForUpdate(finalUpdated);
    }
  };
  return (
    <div className="location-container">
      <div className="preference-title">
        <Grid container justifyContent={"space-between"}>
          <Grid item>
            {PreferenceTitle + ` `}
            <span
              style={{
                color: prefWithError.includes(category) ? "red" : "red",
              }}
            >
              *
            </span>
            {/* {util.capitalizeFirstLetter(category.toString().toLowerCase())} */}
          </Grid>
          <Grid>
            {/* <Tooltip
              title={`Atleast one Value must be selected for ${PreferenceTitle}`}
              placement="left"
            >
              <InfoIcon className="Information-Icon" />
            </Tooltip> */}
          </Grid>
        </Grid>
      </div>
      <div>
        <Grid container>
          {selected
            // .filter((data: any) => {
            //   if(data.isActive){

            //   }
            // })
            .map((data: any, index: number) => {
              const currentValue = data;
              if (data.isActive) {
                return (
                  <React.Fragment key={index}>
                    <Grid item xs={9}>
                      <Autocomplete
                        sx={{ width: "100%", textTransform: "capitalize" }}
                        filterSelectedOptions
                        value={data}
                        options={remaining}
                        id="clear-on-escape"
                        clearOnEscape
                        renderInput={(params) => (
                          <div style={{ position: "relative" }}>
                            {/* {category.toUpperCase().trim() ===
                              PREFERENCE_CATEGORY.ENGAGEMENT_LEADER.toString()
                                .trim()
                                .toUpperCase() &&
                              params.inputProps.value && (
                                <span
                                  style={{
                                    position: "absolute",
                                    top: "15%",
                                    // transform: "translateY(50%)",
                                    marginLeft: "5px",
                                  }}
                                >
                                  <Avatar
                                    sizes="small"
                                    sx={{ height: "23px", width: "23px" }}
                                  >
                                    {data.label.charAt(0)}
                                  </Avatar>
                                </span>
                              )
                              } */}

                            <TextField
                              {...params}
                              placeholder={"Type And Select"}
                              // InputProps={{
                              //   startAdornment: params.inputProps.value && (
                              //     <InputAdornment position="start">
                              //       <Avatar>Val</Avatar>
                              //     </InputAdornment>
                              //   ),
                              // }}
                              inputProps={{
                                ...params.inputProps,
                                // style: {
                                //   paddingLeft:
                                //     category.toUpperCase().trim() ===
                                //     PREFERENCE_CATEGORY.ENGAGEMENT_LEADER.toString()
                                //       ? "35px"
                                //       : "0px",
                                // },
                              }}
                              variant="standard"
                            />
                          </div>
                        )}
                        onChange={(event, newValue) => {
                          onHandleAnotherChange(newValue, index, currentValue);
                        }}
                      />
                    </Grid>
                    <Grid item xs={3} className="delete-container-main">
                      <IconButton
                        onClick={() => {
                          handleDeleteClick(data, index);
                        }}
                        disabled={
                          selected.filter((data: any) => data.isActive)
                            .length === 1
                        }
                        style={{
                          color:
                            selected.filter((data: any) => data.isActive)
                              .length === 1
                              ? "grey"
                              : "4f2d7f",
                        }}
                      >
                        <DeleteOutlineIcon
                          style={{
                            color:
                              selected.filter((data: any) => data.isActive)
                                .length === 1
                                ? "grey"
                                : "4f2d7f",
                          }}
                          // className="delete-icon"
                        />
                      </IconButton>
                    </Grid>
                  </React.Fragment>
                );
              }
            })}
        </Grid>
      </div>
      {/* {selected.filter((data: any) => data.isActive).length < all.length &&
        selected.filter((data: any) => data.isActive).length < 5 && ( */}
      <div className="add-button-container">
        <Button
          className="add-button rmt-preference-button"
          onClick={handleAddResources}
        >
          Add {PreferenceTitle}
        </Button>
      </div>
      {/* )} */}
    </div>
  );
};

export default LocationPreferance;
