import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Autocomplete,
  TextField,
  Button,
  IconButton,
  Typography,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import * as Constant from "./constant";
import React, { useContext, useEffect, useState } from "react";
import * as Utils from "./utils";
import { IAssignResourcesProps } from "./IAssignResourcesProps";
import DeleteIcon from "@mui/icons-material/Delete";
import "./assign-resources.css";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import ProjectSkills from "../project-skills/project-skills";
import { IProjectSkills } from "../project-skills/IProjectSkillsProps";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import AssignSkills from "../assign-skills/assign-skills";
import AssignResourcesForm from "./assign-resources-form/assign-resource-form";
// import { AnyGridOptions } from "ag-grid-community";

const AssignResources = React.memo((props: any) => {
  const isEmployee = React.useContext(UserDetailsContext).isEmployee;
  const {
    designations,
    allResourceDesignations,
    projectDetails,
    masterDetails,
    designationProps,
  } = props;
  const allDesignations = designationProps.allResourceDesignations;
  const [currentDesignations, setCurrentDesignations] = useState<any>([]);

  const [chooseDesignations, setChooseDestinations] = useState<any>([]);
  const { setProjectUpdateDemands } = useContext(ProjectUpdateDetailsContext);
  useEffect(() => {
    setChooseDestinations(
      Utils.GetAnother(allDesignations, currentDesignations)
    );
  }, [allResourceDesignations]);

  useEffect(() => {
    // console.log(props.designations);
    setCurrentDesignations(designations);
  }, []);

  useEffect(() => {
    // console.log(currentDesignations);
    setProjectUpdateDemands(currentDesignations);
  }, [currentDesignations]);

  useEffect(() => {
    const s = Utils.GetAnother(allDesignations, currentDesignations);
    setChooseDestinations(s);
  }, [currentDesignations, allDesignations]);

  useEffect(() => {
    setCurrentDesignations(designationProps.designations);
  }, [designationProps]);

  const handleAddResources = () => {
    // const updatedRemainingData = chooseDesignations;
    // const addedValue = updatedRemainingData[0];
    // updatedRemainingData.splice(0, 1);
    // setChooseDestinations((prevRemaining) => [...updatedRemainingData]);
    // setCurrentDesignations((prevSelected: any) => {
    //   const prevSelectedPref = [...prevSelected];
    //   let isPresent = false;
    //   let DataToAdd: any;
    //   const updatedSelectedArray = prevSelectedPref.filter((currPref) => {
    //     if (
    //       currPref.label.toString().toUpperCase() ===
    //       addedValue.label.toString().toUpperCase()
    //     ) {
    //       isPresent = true;
    //       DataToAdd = {
    //         ...currPref,
    //         isactive: true,
    //       };
    //     } else {
    //       return {
    //         ...currPref,
    //       };
    //     }
    //   });
    //   if (!isPresent) {
    //     updatedSelectedArray.push({
    //       id: 0,
    //       label: addedValue.label,
    //       noOfResources: 0,
    //       isactive: true,
    //     });
    //   } else {
    //     updatedSelectedArray.push(DataToAdd);
    //   }
    //   console.log("selected data for add ", updatedSelectedArray);
    //   return [...updatedSelectedArray];
    // });
    // const updateCurrentDestination = currentDesignations;
    // updateCurrentDestination.push({
    //   id: -1,
    //   label: "",
    //   noOfResources: "",
    //   isactive: true,
    // });
    // console.log(updateCurrentDestination);
    // const _prevDesignation: any = currentDesignations;
    setCurrentDesignations((prevDesignation: any) => {
      return [
        ...prevDesignation,
        {
          id: -1,
          label: "",
          noOfResources: "",
          projectDemandSkills: [],
          isactive: true,
        },
      ];
    });
  };

  const onHandleAnotherChange = (
    newValue: any,
    index: any,
    currentValue: any
  ) => {
    if (newValue) {
      // console.log(newValue, index, currentValue);
      let prevSelectedPref = currentDesignations;
      let DataToReplace: any;
      let isPresent = false;
      // console.log(prevSelectedPref);
      let finalUpdate: any[] = [];
      if (currentValue.id === -1) {
        let presentValueIdx = -1;
        prevSelectedPref.map((item: any, index: number) => {
          if (
            item?.label?.toString().toUpperCase() ===
            newValue.label.toString().toUpperCase()
          ) {
            isPresent = true;
            presentValueIdx = index;
            DataToReplace = {
              id: item.id,
              label: item?.label,
              noOfResources: "",
              projectDemandSkills: [],
              isactive: true,
            };
          }
        });
        if (isPresent) {
          prevSelectedPref[index] = DataToReplace;
          prevSelectedPref.splice(presentValueIdx, 1);
        } else {
          prevSelectedPref[index] = {
            id: 0,
            label: newValue.label,
            noOfResources: "",
            projectDemandSkills: [],
            isactive: true,
          };
        }
        // console.log(prevSelectedPref);
        setChooseDestinations(
          Utils.GetAnother(allDesignations, prevSelectedPref)
        );
        finalUpdate = prevSelectedPref;
      } else {
        const updatedSelectedArray = prevSelectedPref.filter(
          (currPref: any) => {
            if (
              currPref.label.toString().trim().toUpperCase() ===
              newValue.label.toString().trim().toUpperCase()
            ) {
              isPresent = true;
              DataToReplace = {
                id: currPref.id,
                label: currPref.label,
                noOfResources: "",
                projectDemandSkills: [],
                isactive: true,
              };
            } else {
              return {
                ...currPref,
              };
            }
          }
        );
        if (!isPresent) {
          DataToReplace = {
            id: 0,
            label: newValue.label,
            noOfResources: 0,
            projectDemandSkills: [],
            isactive: true,
          };
        }
        // console.log(updatedSelectedArray);

        const updatedSelectedArray2: any = updatedSelectedArray.map(
          (data: any) => {
            if (
              data.label.toString().trim().toUpperCase() ===
              currentValue.label.toString().trim().toUpperCase()
            ) {
              return {
                ...DataToReplace,
              };
            } else {
              return {
                ...data,
              };
            }
          }
        );

        updatedSelectedArray2.push({ ...currentValue, isactive: false });
        // console.log(updatedSelectedArray2);
        setChooseDestinations(
          Utils.GetAnother(allDesignations, updatedSelectedArray2)
        );
        // console.log("selected data for modify ", updatedSelectedArray2);
        finalUpdate = updatedSelectedArray2;
      }
      setCurrentDesignations(finalUpdate);
    }
  };

  const handleDeleteClick = (data: any, index: number) => {
    let prevSelectedPref = [...currentDesignations];
    // console.log(currentDesignations[index]);
    // console.log(data);
    if (data.id === -1) {
      let updated = prevSelectedPref.filter((item: any, idx: number) => {
        if (index === idx) {
        } else {
          return item;
        }
      });
      setCurrentDesignations(updated);
    } else {
      // prevSelectedPref.splice(index, 1);
      const updatedSelectedPref = prevSelectedPref.map((currPref: any) => {
        if (
          currPref.label.toString().toUpperCase() ===
          data.label.toString().toUpperCase()
        ) {
          return {
            ...currPref,
            noOfResources: "",
            projectDemandSkills: [],
            isactive: false,
          };
        } else {
          return {
            ...currPref,
          };
        }
      });
      setCurrentDesignations(updatedSelectedPref);
      // console.log("selected data for delete ", updatedSelectedPref);
      setChooseDestinations(
        Utils.GetAnother(allDesignations, updatedSelectedPref)
      );
    }
    // return [...updatedSelectedPref];
  };

  const onHandleResourceNumberChange = (event: any, data: any) => {
    const updatedDesignation = currentDesignations.map((desig: any) => {
      if (
        desig.label.toString().toUpperCase() ===
        data.label.toString().toUpperCase()
      ) {
        return {
          ...desig,
          noOfResources: event.target.value,
        };
      } else {
        return {
          ...desig,
        };
      }
    });
    setCurrentDesignations(updatedDesignation);
  };
  const handleSkillChange = (value: any, data: any) => {
    let index = currentDesignations.findIndex(
      (d: any) => d.label === data.label
    );
    let updatedDesignation = currentDesignations;
    const finalData = {
      ...data,
      projectDemandSkills: value,
    };
    updatedDesignation[index] = finalData;
    // handleCurrentDesignationChange(updatedDesignation);
    setCurrentDesignations(updatedDesignation);
    setProjectUpdateDemands(updatedDesignation);
    // console.log(value);
  };

  const ProjectSkillsProps: IProjectSkills = {
    skills: Utils.getProjectSkills(projectDetails),
  };

  const SkillsProps: any = {
    allSkills: Utils.getAllSkills(masterDetails),
  };
  return (
    <div style={{ marginLeft: "-8px" }}>
      <Typography className="resource-header">Resources</Typography>
      <TableContainer style={{ marginLeft: "-17px" }}>
        <Table aria-label="simple table">
          <TableHead className="table-row-header">
            <TableRow>
              <TableCell className="column-header">Designation</TableCell>
              <TableCell align="left" className="column-header">
                No. of Resources
              </TableCell>
              <TableCell align="left" className="column-header">
                Skills
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {currentDesignations
              ?.filter((item: any) => item.isactive)
              .map((data: any, index: number) => {
                // console.log(data);
                let currentValue = data;
                return (
                  // <AssignResourcesForm
                  //   data={data}
                  //   chooseDesignations={chooseDesignations}
                  //   index={index}
                  //   ProjectSkillsProps={ProjectSkillsProps}
                  //   SkillsProps={SkillsProps}
                  //   {...props}
                  //   currentSkills={Utils.getProjectSkills(
                  //     data.projectDemandSkills
                  //   )}
                  //   handleSkillChange={(value: any) => {
                  //     handleSkillChange(value, data);
                  //   }}
                  // />
                  <TableRow
                    className="table-row-body"
                    key={index}
                    sx={{
                      "&:last-child td, &:last-child th": { border: 0 },
                    }}
                  >
                    <TableCell>
                      <Autocomplete
                        sx={{
                          width: "70%",
                          "& .MuiAutocomplete-input": {
                            // fontFamily: "GT Walsheim Pro",
                          },
                        }}
                        // filterSelectedOptions
                        value={data}
                        options={chooseDesignations}
                        id="clear-on-escape"
                        clearOnEscape
                        renderInput={(params) => (
                          <TextField
                            style={{ color: "black" }}
                            {...params}
                            variant="standard"
                            placeholder="Select and Type"
                          />
                        )}
                        onChange={(event, newValue) => {
                          onHandleAnotherChange(newValue, index, currentValue);
                        }}
                      />
                    </TableCell>

                    <TableCell className="noOfResources">
                      <TextField
                        type="number"
                        value={data.noOfResources}
                        //defaultValue={data.noOfResources}
                        variant="standard"
                        placeholder="No of Resources"
                        inputProps={{
                          min: 0,
                        }}
                        onChange={(value: any) => {
                          onHandleResourceNumberChange(value, data);
                        }}
                      />
                    </TableCell>
                    <TableCell>
                      {!isEmployee && (
                        <AssignSkills
                          index={index}
                          {...ProjectSkillsProps}
                          {...SkillsProps}
                          {...props}
                          currentSkills={Utils.getProjectSkills(
                            data.projectDemandSkills
                          )}
                          handleSkillChange={(value: any) => {
                            handleSkillChange(value, data);
                          }}
                        ></AssignSkills>
                      )}
                    </TableCell>

                    <TableCell>
                      <IconButton
                        onClick={() => {
                          handleDeleteClick(data, index);
                        }}
                      >
                        <DeleteIcon className="delete-icon" />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                );
              })}
            {chooseDesignations?.length > 0 && (
              <TableRow>
                <Button
                  className="rmt-assignresource-button"
                  sx={Constant.AddButtonSxProps}
                  onClick={handleAddResources}
                  startIcon={<AddIcon sx={Constant.AddIconSxProps} />}
                >
                  Add
                </Button>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
});

export default AssignResources;
