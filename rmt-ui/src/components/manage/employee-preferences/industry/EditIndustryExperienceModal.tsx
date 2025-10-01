import React, { useEffect, useState } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
} from "@mui/material";
import { useForm } from "react-hook-form";
import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";
import ControllerNumberTextField from "../../../controllerInputs/controllerNumbeTextfield";
import ControllerTextField from "../../../controllerInputs/controllerTextField";
import _ from "lodash";
import {
  IIndustryMappingPreference,
  IIndustryMappingPreferenceOptions,
  IIndustryOption,
  ISubIndustryOption,
} from "./interface";
import { IIndustryMasterList } from "../../../../common/interfaces/IIndustryMaster";
import { BtnCancelSx, BtnSaveSx } from "./constant";

interface EditIndustryExperienceModalProps {
  open: boolean;
  onClose: () => void;
  onSave: (updated: IIndustryMappingPreference) => void;
  editingRow: IIndustryMappingPreference | null;
  options: IIndustryMappingPreferenceOptions;
  industryMaster: IIndustryMasterList;
}

const EditIndustryExperienceModal: React.FC<
  EditIndustryExperienceModalProps
> = ({ open, onClose, editingRow, options, onSave, industryMaster }) => {
  const { control, handleSubmit, reset, setValue } = useForm({
    defaultValues: {
      industry: null,
      subIndustry: null,
      year_of_experience: 0,
      description: "",
    },
  });

  const [modalOptions, setModalOptions] =
    useState<IIndustryMappingPreferenceOptions>(options);

  useEffect(() => {
    if (editingRow) {
      reset({
        industry: editingRow.industry ?? null,
        subIndustry: editingRow.subIndustry ?? null,
        year_of_experience: editingRow.industry?.year_of_experience ?? 0,
        description: editingRow.industry?.description ?? "",
      });
      if (editingRow.industry) {
      const selectedIndustries = industryMaster.filter(
        (item) => item.industry_id === editingRow.industry!.industry_id
      );

      const subIndustryOptions: ISubIndustryOption[] = selectedIndustries.map(
        (item) => ({
          sub_industry_id: item.sub_industry_id,
          sub_industry_name: item.sub_industry_name,
        })
      );

      const uniqSubIndustryOptions: ISubIndustryOption[] = _.uniqWith(
        subIndustryOptions,
        (a, b) => a.sub_industry_id === b.sub_industry_id
      ).filter((a) => a && a.sub_industry_id && a.sub_industry_name);

      setModalOptions({
        industryOptions: options.industryOptions,
        subIndustryOptions: uniqSubIndustryOptions,
      });
    } else {
      setModalOptions({
        industryOptions: options.industryOptions,
        subIndustryOptions: [],
      });
    }
    }
  }, [editingRow, reset, options, open]);

  
  const onIndustryChange = (selectedIndustry: IIndustryOption | null) => {
    if (selectedIndustry === null) {
      reset({
        industry: null,
        subIndustry: null,
        year_of_experience: null,
        description: "",
      });
      setModalOptions((prev) => ({
        industryOptions: prev.industryOptions,
        subIndustryOptions: [],
      }));
      return;
    }

    const selectedIndustries = industryMaster.filter(
      (item) => selectedIndustry.industry_id === item.industry_id
    );

    const subIndustryOptions: ISubIndustryOption[] = selectedIndustries.map(
      (item) => ({
        sub_industry_id: item.sub_industry_id,
        sub_industry_name: item.sub_industry_name,
      })
    );

    const uniqSubIndustryOptions: ISubIndustryOption[] = _.uniqWith(
      subIndustryOptions,
      (a, b) => a.sub_industry_id === b.sub_industry_id
    ).filter((a) => a && a.sub_industry_id && a.sub_industry_name);

    setModalOptions((prev) => ({
      industryOptions: prev.industryOptions,
      subIndustryOptions: uniqSubIndustryOptions,
    }));

    setValue("subIndustry", null);
  };

  const onSubmit = (data: any) => {
    const updatedRow: IIndustryMappingPreference = {
      ...editingRow!,
      industry: {
        ...data.industry,
        year_of_experience: data.year_of_experience,
        description: data.description,
      },
      subIndustry: data.subIndustry,
    };

    onSave(updatedRow);
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Edit Industry Preference</DialogTitle>
      <DialogContent>
        {editingRow && (
          <form id="editForm" onSubmit={handleSubmit(onSubmit)}>
            {/* Industry */}
            <div style={{ margin: "10px 0" }}>
              <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                control={control}
                name="industry"
                required
                multiple={false}
                label="Industry"
                getOptionLabel={(option: IIndustryOption) =>
                  option.industry_name || ""
                }
                options={modalOptions?.industryOptions || []}
                onChange={onIndustryChange}
              />
            </div>

            {/* Sub-Industry */}
            <div style={{ margin: "10px 0" }}>
              <ControllerAutoCompleteTextFieldWithGetOptionsLabel
                control={control}
                name="subIndustry"
                required={false}
                multiple={false}
                label="Sub-Industry"
                getOptionLabel={(option: ISubIndustryOption) =>
                  option.sub_industry_name || ""
                }
                options={modalOptions?.subIndustryOptions || []}
              />
            </div>

            {/* Years of Experience */}
            <div style={{ margin: "10px 0" }}>
              <ControllerNumberTextField
                name="year_of_experience"
                control={control}
                required
                label="Years Of Experience"
                onChange={(e: any) => {}}
              />
            </div>

            {/* Description */}
            <div style={{ margin: "10px 0" }}>
              <ControllerTextField
                control={control}
                name="description"
                required={false}
                multiline
                label="Details"
                onChange={(e) => {}}
              />
            </div>
          </form>
        )}
      </DialogContent>
      <DialogActions>
        <Button
          variant="outlined"
          className="btn"
          sx={BtnCancelSx}
          onClick={onClose}
        >
          Cancel
        </Button>
        <Button
          type="submit" form="editForm" variant="contained"
          sx={BtnSaveSx}
        >
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default EditIndustryExperienceModal;
