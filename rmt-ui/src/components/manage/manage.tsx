/* eslint-disable @typescript-eslint/no-unused-vars */
import {
  Box,
  Button,
  Grid,
  IconButton,
  Modal,
  Tooltip,
  Typography,
} from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import {
  ConfigurationParameterConstants,
  EmployeePreferencesHeaderSxProps,
  PreferenceInfoIcon,
  SaveButtonSxProps,
} from "./constant";
import EmployeePreferenceLayout from "./employee-preferences/employee-preference-layout";
import { PREFERENCE_CATEGORY } from "./employee-preferences/constant";
import InfoIcon from "@mui/icons-material/Info";
import {
  // EmployeePreferenceStructuredData,
  checkUpdatePayloadValidity,
} from "./util";
import { SnackbarContext } from "../../contexts/snackbarContext";
import { updateEmployeePreference } from "../../services/employee-preference/employee-preference.service";
import "./style.css";
import { BtnNo, BtnYes } from "../../common/confirmation-dialog/constant";
import CloseIcon from "@mui/icons-material/Close";
import useBlockerCustom from "../../hooks/UnsavedChangesHook/useBlockerCustom";
import useBlockRefreshAndBack from "../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import { IBuMappingPreferenceList } from "./employee-preferences/area-of-experties/interface";
import { ILocationPreferenceList } from "./employee-preferences/location/interface";
import { IIndustryMappingPreferenceList } from "./employee-preferences/industry/interface";
import { getGonfigurationGroupByConfigGroupAndConfigType } from "../../services/configuration-services/configuration.service";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "40%",
  height: "auto",
  bgcolor: "background.paper",
  // padding: "15px",
  borderRadius: "15px",
};

const Manage = () => {
  const [value, setValue] = useState(1);
  const [updatedValue, setUpdatedValue] = useState([] as any[]);
  const snackbarContext: any = useContext(SnackbarContext);
  const [structuredPreferences, setStructuredPreferences] = useState([]);
  const [structuredMaster, setStructuredMaster] = useState([]);
  const [prefWithError, setPrefWithError] = useState([]);
  const [isSaveBtnDisabled, setIsSaveBtnDisabled] = useState(false);
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  const [buMappingRowData, setBuMappingRowData] =
    useState<IBuMappingPreferenceList>([]);
  const [locationRowData, setLocationRowData] =
    useState<ILocationPreferenceList>([]);
  const [industryRowData, setIndustryRowData] =
    useState<IIndustryMappingPreferenceList>([]);

  const [openModal, setOpenModal] = useState(false);
  const [maxNumberOfPreference, setMaxNumberOfPreference] = useState<number>();

  const ManageData = (EmployeePreferences: any) => {
    // const EmployeePreferencesStructured:  any =
    //   EmployeePreferenceStructuredData(EmployeePreferences);
    setStructuredPreferences(EmployeePreferences);
    setUpdatedValue(EmployeePreferences);
    setPrefWithError([]);
    // console.log(PreferenceMasterStructured);
    // setStructuredMaster(PreferenceMasterStructured);
  };
  const GetPreferenceConfiguration = async () => {
    const result: any = await getGonfigurationGroupByConfigGroupAndConfigType(
      ConfigurationParameterConstants.MAX_NUMBER_OF_PREFERENCES,
      ConfigurationParameterConstants.CONFIG_TYPE_GLOBAL
    );
    const configData = result.data;
    if (configData.length > 0 && configData.length === 1) {
      //console.log("data for config ", configData[0]);
      //console.log("data for config length ", configData[0].allValue);
      setMaxNumberOfPreference(Number(configData[0].allValue));
    }
  };
  useEffect(() => {
    GetPreferenceConfiguration();
  }, []);

  const UpdateEmployeePreference = (data: any) => {
    return new Promise((resolve, rejects) => {
      updateEmployeePreference(data)
        .then((resp) => {
          snackbarContext.displaySnackbar(
            "Your preferences have successfully been saved.",
            "success",
            6000
          );
          resolve(resp.data);
          // setIsSaveBtnDisabled(true);
        })
        .catch((err) => {
          snackbarContext.displaySnackbar("Error", "error", 6000);
          rejects(err);
        });
    });
  };
  const saveButtonhandler = () => {
    const finalRowData = [
      ...buMappingRowData,
      ...locationRowData,
      ...industryRowData,
    ].map((e, idx) => {
      return {
        ...e,
        preferenceOrder: idx,
      };
    });
    console.log(finalRowData);

    Promise.all([UpdateEmployeePreference(finalRowData)]).then((values) => {
      console.log(values[0]);
      ManageData(values[0]);
    });
    // }
  };

  const [isDirty, setIsDirty] = useState<boolean>(true);
  useBlockRefreshAndBack(isDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isDirty);

  return (
    <>
      {blocker.state === "blocked" && isDirty ? (
        <DialogBox
          showDialog={isDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <Modal
        open={openModal}
        onClose={() => {
          setOpenModal(false);
        }}
      >
        <Box sx={style}>
          <Grid
            item
            xs={12}
            sx={{ display: "flex", justifyContent: "flex-end" }}
          >
            <Tooltip title="Close">
              <IconButton
                onClick={() => {
                  setOpenModal(false);
                }}
              >
                <CloseIcon />
              </IconButton>
            </Tooltip>
          </Grid>
          <Typography className="preferences-modal-title-header">
            Confirm!
          </Typography>
          <Typography>
            <Grid container>
              <Grid item xs={12}>
                <span className="preferences-modal-content">
                  Are you sure you want to proceed?
                </span>
              </Grid>
              <Grid
                item
                xs={12}
                sm={12}
                sx={{
                  display: "flex",
                  justifyContent: "center",
                  marginBottom: "20px",
                }}
              >
                <Button
                  variant="outlined"
                  className="btn"
                  sx={BtnNo}
                  onClick={() => {
                    setOpenModal(false);
                  }}
                >
                  No
                </Button>
                <Button
                  variant="outlined"
                  className="btn"
                  sx={BtnYes}
                  onClick={() => {
                    saveButtonhandler();
                    setOpenModal(false);
                    setIsDirty(false);
                  }}
                >
                  Yes
                </Button>
              </Grid>
              <Grid item xs={12} className="preferences-modal-note">
                Note: Preference submission does not indicate actual allocation
              </Grid>
            </Grid>
          </Typography>
        </Box>
      </Modal>
      <div className="main-preference-container">
        <Typography component={"span"} sx={EmployeePreferencesHeaderSxProps}>
          My Preferences
        </Typography>
        <Grid sx={PreferenceInfoIcon}>
          <Tooltip
            title={`Maximum ${maxNumberOfPreference} preferences can be saved as defined in configuration by Admin`}
            placement="right"
          >
            <InfoIcon className="preference-info-icon" />
          </Tooltip>
        </Grid>
        <div className="btn-container-main">
          <Button
            className="rmt-manage-save-button"
            sx={SaveButtonSxProps}
            onClick={() => setOpenModal(true)}
            variant="contained"
            disabled={isSaveBtnDisabled}
          >
            Save
          </Button>
        </div>
      </div>

      <EmployeePreferenceLayout
        structuredPreferences={structuredPreferences}
        ManageData={ManageData}
        buMappingRowData={buMappingRowData}
        setBuMappingRowData={setBuMappingRowData}
        locationRowData={locationRowData}
        setLocationRowData={setLocationRowData}
        industryRowData={industryRowData}
        setIndustryRowData={setIndustryRowData}
        maxNumberOfPreference={maxNumberOfPreference}
      />
    </>
  );
};

export default Manage;
