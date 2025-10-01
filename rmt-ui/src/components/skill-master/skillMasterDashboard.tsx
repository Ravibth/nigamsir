import React, { useState } from "react";
import SkillMaster from "./skillMaster";
import AddNewSkill from "./addNewSkill";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import useBlockRefreshAndBack from "../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import useBlockerCustom from "../../hooks/UnsavedChangesHook/useBlockerCustom";
import { RolesListMaster } from "../../common/enums/ERoles";

const SkillMasterDashboard = (props: any) => {
  const [isGrid, setIsGrid] = useState<boolean>(true);
  const [skillName, setSkillName] = useState<string>("");
  const [isReadOnlyModeOn, setIsReadOnlyModeOn] = useState<boolean>(true);
  const [isEditMode, setIsEditMode] = useState<boolean>(true);

  // Route Block
  const [isDirty, setIsDirty] = useState<boolean>(false);
  useBlockRefreshAndBack(isDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isDirty);
  //----- Route Block -------//

  const userContext = React.useContext(UserDetailsContext);
  const isAdmin =
    userContext.role?.filter(
      (role) =>
        role?.toLowerCase() === RolesListMaster.Admin.toLowerCase() ||
        role?.toLowerCase() === RolesListMaster.CEOCOO.toLowerCase() ||
        role?.toLowerCase() === RolesListMaster.SystemAdmin.toLowerCase()
    )?.length > 0;

  const navigateGrid = (
    isGrid: boolean,
    skill: string,
    isReadOnly,
    isEditMode: boolean
  ) => {
    setSkillName(skill);
    setIsGrid(isGrid);
    if (!isGrid) {
      setIsReadOnlyModeOn(isReadOnly);
      setIsEditMode(isEditMode);
    }
  };
  const updateEditMode = (isEdit) => {
    console.log(isEdit);
    setIsEditMode(isEdit);
    setIsReadOnlyModeOn(isEdit ? false : true);
  };

  return (
    <>
      {isAdmin ? (
        <>
          {blocker.state === "blocked" && isDirty ? (
            <DialogBox
              showDialog={isDirty}
              cancelNavigation={handleCancel}
              confirmNavigation={handleConfirm}
            />
          ) : null}

          {isGrid ? (
            <SkillMaster
              navigateGrid={navigateGrid}
              userName={userContext.username}
              isAdmin={isAdmin}
            />
          ) : (
            <AddNewSkill
              navigateGrid={navigateGrid}
              skillName={skillName}
              isReadOnlyModeOn={isReadOnlyModeOn}
              isEditMode={isEditMode}
              userName={userContext.username}
              updateEditMode={updateEditMode}
            />
          )}
        </>
      ) : (
        <div className="center">
          {" "}
          You do not have permission to view this page!{" "}
        </div>
      )}
    </>
  );
};
export default SkillMasterDashboard;
