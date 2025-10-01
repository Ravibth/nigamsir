import { useContext, useEffect, useState } from "react";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../../contexts/userDetailsContext";
import {
  GetAllMySkills,
  IAddUpdateMySkillsRequest,
  IGetAllMySkillsResponse,
} from "../../services/skills/userSkills.service";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../contexts/snackbarContext";
import MySkillsAgGrid from "./my-skills-grid/my-skills-grid";
import { Grid, Typography } from "@mui/material";
import ActionButton from "../actionButton/actionButton";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";
import {
  IMySkillGridContextProps,
  MySkillsGridContext,
} from "./my-skills-grid/mySkillsGridContext/mySkillsGridContext";
import AddUpdateMySkillModal from "./add-my-new-skill/add-update-skill-modal";
import { ESkillPopupType } from "./enums";
import { AddUpdateMySkillsUtils } from "./utils";
import { IMySkillsForm } from "./interfaces";

const MySkills = () => {
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  

  const [openDialogToAddUpdateSkill, setOpenDialogToAddUpdateSkill] =
    useState<ESkillPopupType | null>(null);
  const [gridRowsData, setGridRowsData] = useState<IGetAllMySkillsResponse[]>(
    []
  );
  const mySkillGridContext: IMySkillGridContextProps =
    useContext(MySkillsGridContext);

  const fetchUserSkills = (): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      GetAllMySkills(true)
        .then((resp) => {
          setGridRowsData(resp);
          resolve(true);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  const refreshMySkillsPage = async () => {
    loaderContext.open(true);
    Promise.all([fetchUserSkills()])
      .then((er) => {
        loaderContext.open(false);
      })
      .catch((err) => {
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Error fetching user skills",
          SnackbarSeverity.ERROR
        );
      });
  };

  const submitSkill = async (
    payload: IMySkillsForm,
    closeModalAfterSubmit: boolean
  ): Promise<boolean> => {
    try {
      if (openDialogToAddUpdateSkill === ESkillPopupType.NEW) {
        await addNewSkill(payload);
      } else if (openDialogToAddUpdateSkill === ESkillPopupType.UPDATE) {
        await updateMySkill(payload.proficiency, payload.comments || "");
      }
      if (closeModalAfterSubmit) {
        onCloseModal();
      } else {
        return true;
      }
    } catch (err) {
      //
    }
  };

  const addNewSkill = async (payload: IMySkillsForm): Promise<boolean> => {
    return new Promise<boolean>(async (resolve, reject) => {
      try {
        loaderContext.open(true);
        let selectedSkillName = "";
        let selectedSkillCode = "";
        if (typeof payload.skillName === "string") {
          const selectedSkill = gridRowsData.find(
            (row) =>
              typeof payload.skillName === "string" &&
              row.skillName === payload.skillName
          );
          if (selectedSkill) {
            selectedSkillName = selectedSkill.skillName;
            selectedSkillCode = selectedSkill.skillCode;
          }
        } else if (payload.skillName) {
          selectedSkillName = payload.skillName.skillName;
          selectedSkillCode = payload.skillName.skillCode;
        }

        var newSkillPayload: IAddUpdateMySkillsRequest = {
          skillName: selectedSkillName,
          proficiency: payload.proficiency,
          comments: payload.comments,
          skillCode: selectedSkillCode,
        };
        await AddUpdateMySkillsUtils([newSkillPayload]);
        await fetchUserSkills();
        loaderContext.open(false);
        mySkillGridContext.setCurrentEditingField(null);
        snackbarContext.displaySnackbar(
          "Skill added successfully",
          SnackbarSeverity.SUCCESS
        );
        resolve(true);
      } catch (err) {
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Error adding new skill",
          SnackbarSeverity.ERROR
        );
        reject(true);
      }
    });
  };

  const updateMySkill = async (
    proficiency: string,
    comments: string
  ): Promise<boolean> => {
    return new Promise<boolean>(async (resolve, reject) => {
      try {
        var updatedSkillPayload: IAddUpdateMySkillsRequest = {
          skillName: mySkillGridContext.currentEditingField.skillName,
          skillCode: mySkillGridContext.currentEditingField.skillCode,
          proficiency: proficiency,
          comments: comments,
        };
        loaderContext.open(true);
        await AddUpdateMySkillsUtils([updatedSkillPayload]);
        await fetchUserSkills();
        loaderContext.open(false);
        mySkillGridContext.setCurrentEditingField(null);
        snackbarContext.displaySnackbar(
          "Skill updated successfully",
          SnackbarSeverity.SUCCESS
        );
        resolve(true);
      } catch (err) {
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Error updating skill",
          SnackbarSeverity.ERROR
        );
        reject(true);
      }
    });
  };

  const onCloseModal = () => {
    setOpenDialogToAddUpdateSkill(null);
    mySkillGridContext.setCurrentEditingField(null);
  };

  useEffect(() => {
    if (userDetailsContext.username) {
      refreshMySkillsPage();
    }
  }, [userDetailsContext.username]);

  useEffect(() => {
    if (mySkillGridContext.currentEditingField) {
      setOpenDialogToAddUpdateSkill(ESkillPopupType.UPDATE);
    }
  }, [mySkillGridContext.currentEditingField]);

  return (
    <Typography component={"div"} sx={{ p: 2 }}>
      <Grid container spacing={2} sx={{ marginBottom: 2 }}>
        <Grid item xs={11} sm={11} md={10.5} lg={10.5} />
        <Grid item xs={1} sm={1} md={1.5} lg={1.5}>
          <ActionButton
            label={"Add new Skill"}
            onClick={function (e: any): void {
              setOpenDialogToAddUpdateSkill(ESkillPopupType.NEW);
            }}
            disabled={false}
            type={"button"}
          />
        </Grid>
      </Grid>
      <MySkillsAgGrid
        gridRowsData={gridRowsData}
        setGridRowsData={setGridRowsData}
        fetchUserSkills={fetchUserSkills}
        loaderContext={loaderContext}
        snackbarContext={snackbarContext}
      />
      <AddUpdateMySkillModal
        openDialogToAddUpdateSkill={openDialogToAddUpdateSkill}
        onCloseModal={onCloseModal}
        submitSkill={submitSkill}
        existingSkills={gridRowsData}
      />
    </Typography>
  );
};
export default MySkills;
