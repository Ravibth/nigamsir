import { Grid, Tooltip } from "@mui/material";
import { useForm } from "react-hook-form";
import ActionButton from "../../actionButton/actionButton";
import BackActionButton from "../../actionButton/backactionButton";
import { IBusinessUnitExpertiseOptions, IMySkillsForm } from "../interfaces";
import {
  EMySkillsForm,
  ESkillPopupType,
  ProficiencyLevelOptions,
  ProficiencyLevelOptionsKeyPair,
} from "../enums";
import ControllerTextField from "../../controllerInputs/controllerTextField";
import { HeadingStyleProps } from "../style";
import React, { useContext, useEffect, useState } from "react";
import {
  GetAllSkillsMaster,
  GetBusinessUnitsMaster,
  GetCompetencyMaster,
  GetDistinctBUOptionValues,
} from "../utils";
import { ISkillsMaster } from "../../../common/interfaces/ISkillsMaster";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../../contexts/snackbarContext";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../contexts/loaderContext";
import AddUpdateSkillFilter from "./add-update-skill-filter";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import {
  IMySkillGridContextProps,
  MySkillsGridContext,
} from "../my-skills-grid/mySkillsGridContext/mySkillsGridContext";
import {
  GetOptionsSorted,
  IAddUpdateSkillProps,
  MySkillFormDefaultValues,
  updateSkillsFilterOptionsUtils,
} from "./utils";
import { IGetAllMySkillsResponse } from "../../../services/skills/userSkills.service";
import { EUserSkillStatus } from "../my-skills-grid/utils";
import InfoIcon from "@mui/icons-material/Info";
import useBlockRefreshAndBack from "../../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import DialogBox from "../../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import useBlockerCustom from "../../../hooks/UnsavedChangesHook/useBlockerCustom";
import "./skill-style.css";
import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../../../contexts/userDetailsContext";
import { IBUTreeMapping } from "../../../common/interfaces/IBuTreeMapping";

const AddUpdateSkill = (props: IAddUpdateSkillProps) => {
  const {
    control,
    setValue,
    getValues,
    reset,
    trigger,
    formState: { errors, isDirty },
  } = useForm<IMySkillsForm>({
    mode: "onTouched",
    defaultValues: MySkillFormDefaultValues,
  });
  const [selectedCompetency, setSelectedCompetency] =
    useState<ICompetencyMaster>(null);
  const [selectedSkill, setSelectedSkill] = useState<ISkillsMaster>();
  const [selectedProficiency, setSelectedProficiency] = useState<string>("");

  const [selectedProficiencyDesc, setSelectedProficiencyDesc] =
    useState<string>("");
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const mySkillGridContext: IMySkillGridContextProps =
    useContext(MySkillsGridContext);
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);
  useBlockRefreshAndBack(isDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isDirty);

  const [skillsMaster, setSkillsMaster] = useState<ISkillsMaster[]>([]);
  const [skillsFilteredValues, setSkillsFilteredValues] = useState<
    ISkillsMaster[]
  >([]);
  const [buTreeMappings, setBUTreeMappings] = useState<IBUTreeMapping[]>([]);
  const [businessUnitExpertiseOptions, setBusinessUnitExpertiseOptions] =
    useState<IBusinessUnitExpertiseOptions>({
      businessUnitOptions: [],
      competencyOptions: [],
    });
  const submitSkill = async (closeModalAfterSubmit: boolean) => {
    trigger();
    const values = getValues();
    if (values.skillName && values.proficiency) {
      const submitted = await props.submitSkill(
        getValues(),
        closeModalAfterSubmit
      );
      if (submitted) {
        // setSelectedBU(getValues(EMySkillsForm.businessUnit));
        // setSelectedExpertise(getValues(EMySkillsForm.expertise));

        setSelectedCompetency(values.competency);
        setSelectedSkill(null);
        setSelectedProficiency("");
        setSelectedProficiencyDesc("");
        reset();
      }
    }
  };
  useEffect(() => {
    if (selectedCompetency) {
      setValue("competency", selectedCompetency);
    }
  }, [selectedCompetency]);
  // useEffect(() => {
  //   if (userDetailsContext.competency && userDetailsContext.competency !== "") {
  //     const skillInCompetencyOfUser = skillsFilteredValues.filter(
  //       (sk) =>
  //         sk.skill_Mapping.filter(
  //           (sm) => sm.competencyId === userDetailsContext.competencyId
  //         ).length > 0
  //     );
  //     const skillNotInCompetencyOfUser = skillsFilteredValues.filter(
  //       (sk) =>
  //         sk.skill_Mapping.filter(
  //           (sm) => sm.competencyId !== userDetailsContext.competencyId
  //         ).length > 0
  //     );
  //     console.log(
  //       GetOptionsSorted(skillInCompetencyOfUser, userDetailsContext)
  //     );
  //   }
  // }, [userDetailsContext, skillsFilteredValues]);
  const updateBusinessUnitOptions = (buTreeMapping: IBUTreeMapping[]) => {
    const buDistinctValues = GetDistinctBUOptionValues(buTreeMapping);
    setBusinessUnitExpertiseOptions((prev) => ({
      ...prev,
      businessUnitOptions: buDistinctValues,
    }));
  };

  const updateCompetencyOptions = (competencyMaster: ICompetencyMaster[]) => {
    // const buDistinctValues = GetDistinctBUOptionValues(buTreeMapping);
    setBusinessUnitExpertiseOptions((prev) => ({
      ...prev,
      competencyOptions: competencyMaster,
    }));
  };

  const autoPopulateForUpdateForm = () => {
    if (
      props.openDialogToAddUpdateSkill === ESkillPopupType.UPDATE &&
      mySkillGridContext.currentEditingField
    ) {
      // const tempSkillOption: ISkillsMaster = {
      //   skillName: mySkillGridContext.currentEditingField.skillName,
      //   skillCode: mySkillGridContext.currentEditingField.skillCode,
      // };
      const tempSkillOption: ISkillsMaster = {
        ...mySkillGridContext.currentEditingField,
      };
      setSelectedSkill(tempSkillOption);
      setSkillsFilteredValues([tempSkillOption]);
      setValue(EMySkillsForm.skillName, tempSkillOption);
      getSelectedProficiency(
        mySkillGridContext.currentEditingField.proficiency,
        tempSkillOption
      );
      setValue(
        EMySkillsForm.proficiency,
        mySkillGridContext.currentEditingField.proficiency
      );
    }
  };

  useEffect(() => {
    if (props.openDialogToAddUpdateSkill === ESkillPopupType.NEW) {
      loaderContext.open(true);
      Promise.all([
        GetAllSkillsMaster(),
        GetBusinessUnitsMaster(),
        GetCompetencyMaster(),
      ])
        .then((resp) => {
          const activeSkills: ISkillsMaster[] = resp[0].filter(
            (skill) => skill.isActive && skill.isEnable
          );
          setSkillsMaster(activeSkills);
          setSkillsFilteredValues(activeSkills);
          setBUTreeMappings(resp[1]);
          updateBusinessUnitOptions(resp[1]);
          updateCompetencyOptions(resp[2]);
          loaderContext.open(false);
        })
        .catch((err) => {
          loaderContext.open(false);
          snackbarContext.displaySnackbar(
            "Error Fetching Details",
            SnackbarSeverity.ERROR
          );
        });
    } else if (props.openDialogToAddUpdateSkill === ESkillPopupType.UPDATE) {
      autoPopulateForUpdateForm();
    }
  }, []);

  const updateSkillsFilterOptions = () => {
    const allValues = getValues();
    const tempSkillsFilteredValues = updateSkillsFilterOptionsUtils(
      skillsMaster,
      allValues
    );

    setSkillsFilteredValues(tempSkillsFilteredValues);
  };
  const getSelectedProficiency = (
    selectedProficiency: string,
    skill: ISkillsMaster
  ) => {
    if (selectedProficiency && skill) {
      const keyValue = ProficiencyLevelOptionsKeyPair.filter(
        (a) => a.key === selectedProficiency
      );
      setSelectedProficiencyDesc(keyValue && skill[keyValue[0].value]);
    } else {
      setSelectedProficiencyDesc("");
    }
  };
  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
      }}
    >
      {blocker.state === "blocked" && isDirty ? (
        <DialogBox
          showDialog={isDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <Grid container spacing={2}>
        <Grid item xs={12} sx={HeadingStyleProps}>
          {props.openDialogToAddUpdateSkill === ESkillPopupType.NEW ? (
            <>Add New Skill</>
          ) : (
            <> Update Skill</>
          )}
        </Grid>
        {props.openDialogToAddUpdateSkill === ESkillPopupType.NEW && (
          <AddUpdateSkillFilter
            control={control}
            selectedCompetency={selectedCompetency}
            businessUnitExpertiseOptions={businessUnitExpertiseOptions}
            updateSkillsFilterOptions={updateSkillsFilterOptions}
          />
        )}

        <Grid item xs={11.5} className="helper-text-main">
          <ControllerAutoCompleteTextFieldWithGetOptionsLabel
            control={control}
            name={EMySkillsForm.skillName}
            required={true}
            label={"Skill Name"}
            options={GetOptionsSorted(skillsFilteredValues, userDetailsContext)}
            getOptionLabel={(option: ISkillsMaster) =>
              `${option?.skillName} (${option?.skillCode})`
            }
            groupBy={(option: ISkillsMaster) => option.type}
            helperTextStyle={{
              paddingTop: 5,
              maxHeight: 190,
              overflowY: "auto",
            }}
            helperText={
              selectedSkill?.description ? selectedSkill?.description : ""
            }
            disabled={props?.openDialogToAddUpdateSkill !== ESkillPopupType.NEW}
            error={errors.skillName ? true : false}
            freeSolo={false}
            onChange={(e) => {
              setSelectedSkill(e);
              getSelectedProficiency(selectedProficiency, e);
            }}
            getOptionDisabled={(option: ISkillsMaster) => {
              if (
                props.existingSkills.find(
                  (skillItem: IGetAllMySkillsResponse) =>
                    skillItem.skillName === option.skillName &&
                    (skillItem.status === EUserSkillStatus.APPROVED ||
                      skillItem.status === EUserSkillStatus.PENDING ||
                      skillItem.status === EUserSkillStatus.PENDING_APPROVAL)
                )
              ) {
                return true;
              } else {
                return false;
              }
            }}
          />
        </Grid>

        <Grid item xs={11.5} className="helper-text-main">
          <ControllerAutoCompleteTextFieldWithGetOptionsLabel
            control={control}
            name={EMySkillsForm.proficiency}
            required={true}
            label={"Proficiency"}
            options={ProficiencyLevelOptions}
            error={errors.proficiency ? true : false}
            freeSolo={false}
            helperTextStyle={{
              paddingTop: 5,
              maxHeight: 190,
              overflowY: "auto",
            }}
            helperText={selectedProficiencyDesc ? selectedProficiencyDesc : ""}
            onChange={(e) => {
              setSelectedProficiency(e);
              getSelectedProficiency(e, selectedSkill);
              trigger();
            }}
          />
        </Grid>
        <Grid item xs={11.5}>
          <ControllerTextField
            control={control}
            name={EMySkillsForm.comments}
            required={false}
            multiline={true}
            label={"Comments"}
            maxRows={4}
            options={[]}
            error={errors.comments ? true : false}
            maxLength={255}
            onChange={(e) => {
              //
            }}
          />
        </Grid>
        <Grid item xs={0.5}>
          <span>
            <Tooltip
              placement="right"
              title={
                "Add comments to validate the skill proficiency level request submitted."
              }
            >
              <InfoIcon className="infoIconStyle" />
            </Tooltip>
          </span>
        </Grid>
        {props.openDialogToAddUpdateSkill === ESkillPopupType.NEW && (
          <React.Fragment>
            <Grid item lg={0.5} xs={0.5} sm={0.5} md={0.5} />
            <Grid item lg={3.5} xs={3.5} sm={3.5} md={3.5}>
              <BackActionButton
                label={"Cancel"}
                onClick={function (e: any): void {
                  props.closeModal();
                }}
              />
            </Grid>
            <Grid item lg={3.5} xs={3.5} sm={3.5} md={3.5}>
              <ActionButton
                label={"Save"}
                onClick={function (e: any): void {
                  submitSkill(true);
                }}
                disabled={false}
                type={"submit"}
              />
            </Grid>
            <Grid item lg={4} xs={4} sm={4} md={4}>
              <ActionButton
                label={"Add more skill"}
                onClick={function (e: any): void {
                  submitSkill(false);
                }}
                disabled={false}
                type={"submit"}
              />
            </Grid>
          </React.Fragment>
        )}
        {props.openDialogToAddUpdateSkill === ESkillPopupType.UPDATE && (
          <React.Fragment>
            <Grid item xs={3.5} />
            <Grid item xs={4}>
              <BackActionButton
                label={"Cancel"}
                onClick={function (e: any): void {
                  props.closeModal();
                }}
              />
            </Grid>
            <Grid item xs={4}>
              <ActionButton
                label={"Update"}
                onClick={function (e: any): void {
                  submitSkill(true);
                }}
                disabled={
                  !getValues(EMySkillsForm.proficiency) ||
                  mySkillGridContext?.currentEditingField?.proficiency ===
                    getValues(EMySkillsForm.proficiency)
                }
                type={"submit"}
              />
            </Grid>
          </React.Fragment>
        )}
      </Grid>
    </form>
  );
};
export default AddUpdateSkill;
