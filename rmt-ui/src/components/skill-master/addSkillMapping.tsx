/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from "react";
import "./skill.css";
import { useForm } from "react-hook-form";
import CloseIcon from "@mui/icons-material/Close";
import { Box, Grid, IconButton, Modal, Tooltip } from "@mui/material";
import _ from "lodash";
import ControllerAutoCompleteChipsSimple from "../controllerInputs/controllerAutoCompleteChipsSimple";
import { getAllCompetency } from "../../services/wcgt-master-services/wcgt-master-services";
import { ICompetencyMaster } from "../../common/interfaces/ICompetencyMaster";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import { IskillMapping } from "./Interface/IskillMapping";
import ActionButton from "../actionButton/actionButton";
import { SnackbarContextProps } from "../../contexts/snackbarContext";
import { GetDesignationList, RequisitionButtons } from "./utils";

export enum EAddNewSkillMappingForm {
  Competency = "Competency",
  Designation = "Designation",
}
export interface IAddNewSkillMappingForm {
  [EAddNewSkillMappingForm.Competency]: ICompetencyMaster;
  [EAddNewSkillMappingForm.Designation]: string[];
}

interface IAddSkillMappingProps {
  open: boolean;
  handleModelClose: () => void;
  handleMappingData: (data: IskillMapping) => void;
  snackbarContext: SnackbarContextProps;
  skillMappingToEdit: IAddNewSkillMappingForm | null;
}

const AddSkillMapping = (props: IAddSkillMappingProps) => {
  const {
    control,
    handleSubmit,
    setValue,
    formState: { errors, isDirty },
  } = useForm<IAddNewSkillMappingForm>({
    mode: "onTouched",
  });

  const style = {
    position: "absolute" as "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    width: "40%",
    borderRadius: "15px",
    bgcolor: "background.paper",
  };
  const { handleModelClose, open } = props;
  const [competencyOption, setCompetencyOption] = useState<ICompetencyMaster[]>(
    []
  );

  const [designationOption, setDesignationOption] = useState<Array<string>>([]);
  const loadCompetencyMasterData = async () => {
    try {
      const data: ICompetencyMaster[] = await getAllCompetency();
      return data.filter((c: ICompetencyMaster) => c.isActive);
    } catch (error) {}
  };

  useEffect(() => {
    getAllDesignationList();
  }, []);

  useEffect(() => {
    loadCompetencyMasterData()
      .then((resp) => {
        setCompetencyOption(resp);
      })
      .catch((err) => {});
  }, []);

  useEffect(() => {
    if (props.skillMappingToEdit) {
      setValue(
        EAddNewSkillMappingForm.Competency,
        props.skillMappingToEdit[EAddNewSkillMappingForm.Competency]
      );
      setValue(
        EAddNewSkillMappingForm.Designation,
        props.skillMappingToEdit[EAddNewSkillMappingForm.Designation]
      );
    }
  }, [props.skillMappingToEdit]);

  const getAllDesignationList = () => {
    GetDesignationList()
      .then((options: any) => {
        if (options.length) {
          const designationList = options?.map(
            (designation) => designation?.label
          );
          setDesignationOption(designationList);
        }
      })
      .catch(() => {
        props.snackbarContext.displaySnackbar(
          "Error fetching Details",
          "error"
        );
      });
  };
  const handleFormSubmit = async (e: IAddNewSkillMappingForm) => {
    if (e[EAddNewSkillMappingForm.Designation].length > 0) {
      const competency =
        e[EAddNewSkillMappingForm.Competency] &&
        Array.isArray(e[EAddNewSkillMappingForm.Competency])
          ? e[EAddNewSkillMappingForm.Competency]
          : e[EAddNewSkillMappingForm.Competency]
          ? [e[EAddNewSkillMappingForm.Competency]]
          : [];
      competency.forEach((element: ICompetencyMaster) => {
        const data: IskillMapping = {
          competency: element,
          designation: e[EAddNewSkillMappingForm.Designation],
        };
        props?.handleMappingData(data);
      });
    }
  };

  return (
    <Modal open={open} onClose={handleModelClose}>
      <Box sx={style}>
        <form onSubmit={handleSubmit(handleFormSubmit)}>
          <Grid container spacing={2} sx={{ p: 4 }}>
            <Grid item xs={8} className="skill-mapping-modal">
              Add Mapping
            </Grid>
            <Grid item xs={4} sx={RequisitionButtons}>
              <Tooltip title="Close">
                <IconButton onClick={handleModelClose}>
                  <CloseIcon />
                </IconButton>
              </Tooltip>
            </Grid>

            <Grid item xs={11.8}>
              <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                name={EAddNewSkillMappingForm.Competency}
                control={control}
                defaultValue={null}
                options={competencyOption}
                getOptionLabel={(option: ICompetencyMaster) => {
                  return option?.competency || "";
                }}
                multiple={false}
                filterSelectedOptions={true}
                error={errors[EAddNewSkillMappingForm.Competency]}
                required={true}
                label={"Competency"}
                onChange={() => {}}
              />
            </Grid>
            <Grid item xs={11.8} className="skill_addmapping_designation">
              <ControllerAutoCompleteChipsSimple
                name={EAddNewSkillMappingForm.Designation}
                multiple={true}
                control={control}
                defaultValue={[]}
                required={true}               
                options={designationOption ? designationOption : []}
                onChange={() => {}}
                error={errors[EAddNewSkillMappingForm.Designation]}
                label={"Designation"}
                textfieldVariant="standard"
              />
            </Grid>
            <Grid item xs={10} />
            <Grid
              item
              xs={1.8}
              sx={RequisitionButtons}
              className="skill-mapping-save"
            >
              <ActionButton
                label={"Save"}
                onClick={() => {}}
                disabled={false}
                type={"submit"}
              />
            </Grid>
          </Grid>
        </form>
      </Box>
    </Modal>
  );
};

export default AddSkillMapping;
