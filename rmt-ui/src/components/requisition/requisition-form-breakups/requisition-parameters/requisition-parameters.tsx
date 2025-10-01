import {
  Control,
  FieldErrors,
  UseFormSetValue,
  FieldValues,
  UseFormGetValues,
  UseFormReset,
} from "react-hook-form";
import { IProjectMaster } from "../../../../common/interfaces/IProject";
import { sortCategories } from "../../../../global/utils";
import {
  EBaseRequisitionFormMainControlForm,
  IGlobalOptionsForParameters,
  IParametersSelectionConfigs,
  IRequisitionResourcesDetails,
  IRequisitionSkillOptions,
  IRequisitionSubIndustry,
  IResourceWiseParameterOptions,
  ParametersSelectionConfigs,
  RequisitionParametersPreferenceOrder,
  validateSkill,
  SkillsGroupByType,
} from "../../utils";
import { Grid, Slider, Tooltip, Typography } from "@mui/material";
import ControllerAutoCompleteChipsSimple from "../../../controllerInputs/controllerAutoCompleteChipsSimple";
import { IRequisitionParametersMaster } from "../../../../common/interfaces/IRequisition";
import React, { useEffect, useState } from "react";
import { ERequisitionParameters } from "../../../system-suggestions/requisition-form-system-suggestions/suggestions-grid-view/suggestions-grid-view";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import _ from "lodash";
import { ISkillsMaster } from "../../../../common/interfaces/ISkillsMaster";
import InfoIcon from "@mui/icons-material/Info";

interface IRequisitionParametersProps {
  isReadOnlyModeOn: boolean;
  control: Control<FieldValues, any>;
  getValues: UseFormGetValues<FieldValues>;
  setValue: UseFormSetValue<FieldValues>;
  errors: FieldErrors;
  isUpdateModeOn: boolean;
  projectInfo: IProjectMaster;
  getControlName: (type: string, errorType?: boolean) => string;
  trigger: any;
  field: any;
  fieldData: IRequisitionResourcesDetails;
  optionsMaster: IGlobalOptionsForParameters;
  index: number;
  getParameterControlError: (
    categoryItemToCheck: EBaseRequisitionFormMainControlForm
  ) => boolean;
  reset: UseFormReset<any>;
}
const RequisitionParameters = (props: IRequisitionParametersProps) => {
  const [resourceWiseParameterOptions, setResourceWiseParameterOptions] =
    useState<IResourceWiseParameterOptions>({
      skills: [],
      subIndustry: [],
    });

  const onValueChangeFunctions = (
    category: EBaseRequisitionFormMainControlForm
  ) => {
    switch (category) {
      case EBaseRequisitionFormMainControlForm.industry:
        props.setValue(
          props.getControlName(EBaseRequisitionFormMainControlForm.subIndustry),
          null
        );
        // props.reset(
        //   props.getControlName(EBaseRequisitionFormMainControlForm.subIndustry)
        // );
        break;
      case EBaseRequisitionFormMainControlForm.competency:
        getSkillsOptionBasedOnCompetency();
        props.setValue(
          props.getControlName(EBaseRequisitionFormMainControlForm.skills),
          []
        );
        break;
      default:
        break;
    }
  };

  const onSliderValueChanged = (event: any) => {
    const allParameterValues: IRequisitionParametersMaster[] =
      props.fieldData?.parameters;

    const finalParameterValues = allParameterValues.map((itemParam) => {
      if (itemParam.category === event.name) {
        return { ...itemParam, requisitionWeight: event.value };
      } else {
        return itemParam;
      }
    });
    props.setValue(props.getControlName("parameters"), finalParameterValues);
  };

  const GetOptionsForRenders = (category: string) => {
    switch (category) {
      case ERequisitionParameters.competency:
        return props.optionsMaster.competency;
      case ERequisitionParameters.Industry:
        return props.optionsMaster.industry;
      case ERequisitionParameters.Location:
        return props.optionsMaster.location;
      case ERequisitionParameters.Skills:
        return resourceWiseParameterOptions.skills;
      case ERequisitionParameters.Sub_Industry:
        return resourceWiseParameterOptions.subIndustry;
      default:
        return [];
    }
  };

  const getSkillsOptionBasedOnCompetency = () => {
    const designation = props.getValues(
      EBaseRequisitionFormMainControlForm.designation
    );
    const competency = props.getValues(
      props.getControlName(EBaseRequisitionFormMainControlForm.competency)
    );
    if (competency && designation) {
      const uniqueSkills: ISkillsMaster[] = _.orderBy(
        props.optionsMaster.skills,
        "skillCode"
      );
      const finalOptions: IRequisitionSkillOptions[] = uniqueSkills.map(
        (skillItem: ISkillsMaster) => {
          if (
            skillItem?.skill_Mapping?.find(
              (skillMap) =>
                skillMap?.competency?.toLowerCase() ===
                competency?.competency?.toLowerCase()
            ) &&
            skillItem?.skill_Mapping?.find((skillMap) =>
              skillMap?.designation.find(
                (mapDesignation) =>
                  mapDesignation.toLowerCase() ===
                  designation?.designation_name?.toLowerCase()
              )
            )
          ) {
            return { ...skillItem, type: SkillsGroupByType.Mandatory };
          } else {
            return { ...skillItem, type: SkillsGroupByType.NiceToHave };
          }
        }
      );

      const nameSorter = (record) => record?.skillName?.toLowerCase();
      const sortDirection = "asc";

      const mandatorySkills = finalOptions.filter(
        (m) => m.type === SkillsGroupByType.Mandatory
      );
      const sortedMandatorySkills = _.orderBy(
        mandatorySkills,
        [nameSorter],
        [sortDirection]
      );

      const additionalSkills = finalOptions.filter(
        (m) => m.type === SkillsGroupByType.NiceToHave
      );
      const sortedAdditionalSkills = _.orderBy(
        additionalSkills,
        [nameSorter],
        [sortDirection]
      );

      const finalSkillsSorted = [
        ...sortedMandatorySkills,
        ...sortedAdditionalSkills,
      ];
      setResourceWiseParameterOptions((prev) => ({
        ...prev,
        skills: finalSkillsSorted,
      }));
    } else {
      setResourceWiseParameterOptions((prev) => ({
        ...prev,
        skills: [],
      }));
      props.setValue(
        props.getControlName(EBaseRequisitionFormMainControlForm.skills),
        []
      );
    }
  };

  const getSubIndustryOptionsBasedOnIndustry = () => {
    const industry = props.getValues(
      props.getControlName(EBaseRequisitionFormMainControlForm.industry)
    );
    if (industry) {
      const allSubIndustries: IRequisitionSubIndustry[] =
        props.optionsMaster.industrySubIndustryMaster
          .filter(
            (ind) =>
              ind.industry_name.toLowerCase().trim() ===
              industry?.industry_name?.toLowerCase().trim()
          )
          .map((ind) => {
            return {
              subIndustry: ind.sub_industry_name,
              subIndustryId: ind.sub_industry_id,
            };
          });
      const uniqueSubIndustries = _.uniqBy(allSubIndustries, "subIndustry");
      setResourceWiseParameterOptions((prev) => ({
        ...prev,
        subIndustry: uniqueSubIndustries,
      }));
    } else {
      setResourceWiseParameterOptions((prev) => ({
        ...prev,
        subIndustry: [],
      }));
    }
  };

  useEffect(() => {
    getSkillsOptionBasedOnCompetency();
  }, [
    props.getValues(EBaseRequisitionFormMainControlForm.designation),
    props.getValues(
      props.getControlName(EBaseRequisitionFormMainControlForm.competency)
    ),
  ]);

  useEffect(() => {
    getSubIndustryOptionsBasedOnIndustry();
  }, [
    props.getValues(
      props.getControlName(EBaseRequisitionFormMainControlForm.industry)
    ),
  ]);

  const sliderRenderForParameter = (
    matchedCategoryParameter: IRequisitionParametersMaster,
    parameterConfig: IParametersSelectionConfigs
  ) => {
    const currentValue = props.getValues(
      props.getControlName(parameterConfig.controlName)
    );
    return (
      <Typography component={"div"} sx={{ marginTop: 1.6 }}>
        <Slider
          name={matchedCategoryParameter.category}
          defaultValue={matchedCategoryParameter.requisitionWeight}
          getAriaValueText={(valueText) => `${valueText}`}
          valueLabelDisplay="auto"
          value={matchedCategoryParameter.requisitionWeight}
          disabled={
            props.isReadOnlyModeOn ||
            !currentValue ||
            (parameterConfig.multipleSelectionAllowed &&
              currentValue &&
              currentValue.length === 0)
          }
          step={1}
          marks
          min={0}
          max={parameterConfig.maxSliderValue}
          onChange={(event: any) => {
            onSliderValueChanged(event.target);
          }}
        />
      </Typography>
    );
  };

  const validateParameter = (
    updatedValue: any,
    type: ERequisitionParameters
  ): boolean => {
    switch (type) {
      case ERequisitionParameters.Skills:
        return validateSkill(updatedValue);
      default:
        return true;
    }
  };

  const getControlToRenderForParameter = (
    matchedCategoryParameter: IRequisitionParametersMaster
  ) => {
    const parameterConfig: IParametersSelectionConfigs | null | undefined =
      ParametersSelectionConfigs.find(
        (config: IParametersSelectionConfigs) =>
          config.category.toLowerCase() ===
          matchedCategoryParameter.category.toLowerCase()
      );

    if (parameterConfig) {
      if (parameterConfig.multipleSelectionAllowed) {
        return (
          <>
            <Grid
              item
              xs={
                parameterConfig.xsSpace -
                (parameterConfig.infoIconMessage ? 0.2 : 0)
              }
              sx={{ mb: 3 }}
            >
              <ControllerAutoCompleteChipsSimple
                name={props.getControlName(parameterConfig.controlName)}
                control={props.control}
                defaultValue={parameterConfig.defaultValue}
                required={parameterConfig.isMandatory}
                multiple={true}
                onChange={(e: any) => {
                  onValueChangeFunctions(parameterConfig.controlName);
                }}
                options={GetOptionsForRenders(parameterConfig.category)}
                getOptionLabel={parameterConfig.getOptionLabel}
                isOptionEqualToValue={parameterConfig.isOptionEqualToValue}
                readOnly={
                  props.isReadOnlyModeOn || !parameterConfig.isChangeAllowed
                }
                disabled={
                  props.isReadOnlyModeOn || !parameterConfig.isChangeAllowed
                }
                filterSelectedOptions={true}
                label={parameterConfig.label}
                error={props.getParameterControlError(
                  parameterConfig.controlName
                )}
                validate={(e) => validateParameter(e, parameterConfig.category)}
                groupBy={parameterConfig.groupBy}
                sortBy={parameterConfig.sortBy}
                highlightDifferentValuesDifferently={
                  parameterConfig.highlightDifferentValuesDifferently
                }
                propertyToHighlightDifferently={
                  parameterConfig.propertyToHighlightDifferently
                }
                propertyValueToHighlightDifferently={
                  parameterConfig.propertyValueToHighlightDifferently
                }
              />
            </Grid>
            {parameterConfig.infoIconMessage && (
              <Grid item xs={0.2}>
                <Tooltip
                  placement="right"
                  title={parameterConfig.infoIconMessage}
                >
                  <InfoIcon className="infoIcon-skill" />
                </Tooltip>
              </Grid>
            )}
            <Grid item xs={2} sx={{ mb: 3 }}>
              {sliderRenderForParameter(
                matchedCategoryParameter,
                parameterConfig
              )}
            </Grid>
          </>
        );
      } else {
        return (
          <>
            <Grid
              item
              xs={
                parameterConfig.xsSpace -
                (parameterConfig.infoIconMessage ? 0.2 : 0)
              }
              sx={{ mb: 3 }}
            >
              <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                name={props.getControlName(parameterConfig.controlName)}
                control={props.control}
                required={parameterConfig.isMandatory}
                defaultValue={parameterConfig.defaultValue}
                freeSolo={
                  props.isReadOnlyModeOn || !parameterConfig.isChangeAllowed
                }
                isOptionEqualToValue={parameterConfig.isOptionEqualToValue}
                filterSelectedOptions={true}
                multiple={false}
                onChange={(e: any) => {
                  onValueChangeFunctions(parameterConfig.controlName);
                }}
                options={GetOptionsForRenders(parameterConfig.category)}
                getOptionLabel={parameterConfig.getOptionLabel}
                readOnly={
                  props.isReadOnlyModeOn || !parameterConfig.isChangeAllowed
                }
                disabled={
                  props.isReadOnlyModeOn || !parameterConfig.isChangeAllowed
                }
                label={parameterConfig.label}
                error={props.getParameterControlError(
                  parameterConfig.controlName
                )}
                validate={(e) => validateParameter(e, parameterConfig.category)}
              />
            </Grid>
            {parameterConfig.infoIconMessage && (
              <Grid item xs={0.2}>
                <Tooltip
                  placement="right"
                  title={parameterConfig.infoIconMessage}
                >
                  <InfoIcon className="infoIcon-skill" />
                </Tooltip>
              </Grid>
            )}
            <Grid item xs={2} sx={{ mb: 3 }}>
              {sliderRenderForParameter(
                matchedCategoryParameter,
                parameterConfig
              )}
            </Grid>
          </>
        );
      }
    } else {
      return <></>;
    }
  };

  return (
    <>
      {sortCategories(
        props.fieldData?.parameters?.map(
          (parameterItem) => parameterItem.category
        ),
        RequisitionParametersPreferenceOrder
      ).map((parameterItem) => {
        const matchedCategoryParameter: IRequisitionParametersMaster =
          props.fieldData?.parameters?.find(
            (itemParam) =>
              itemParam.category.toLowerCase() === parameterItem.toLowerCase()
          );

        return (
          <React.Fragment key={matchedCategoryParameter.category}>
            {matchedCategoryParameter &&
              getControlToRenderForParameter(matchedCategoryParameter)}
          </React.Fragment>
        );
      })}
    </>
  );
};
export default RequisitionParameters;
