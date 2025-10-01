import * as service from "../../../services/project-list-services/project-list-services";
import moment from "moment";
import { useContext } from "react";
import { ProjectUpdateDetailsContext } from "../../../contexts/projectDetailsContext";
import {
  getUserByEmail,
  updateUserRoleList,
  // updateUserRoles,
} from "../../../services/role-permission-service/role-permission-service";
import { resolve } from "path";
import { IEmployeeDetails } from "../../project-details-layout/additionalElAgGrid";
import { IProjectRoles } from "../../../common/interfaces/IProjectRole";
import { RolesListMaster } from "../../../common/enums/ERoles";
import { getProjectRolesByEmailId } from "../../../../src/services/project-list-services/get-master-services";
import {
  RemoveUserRoleByEmail,
  RemoveUserRoleByEmailDto,
} from "../../../services/user/user.service";

export const getProjectTitleDetails = () => {
  return {
    variant: "h5",
    component: "span",
  };
};

export const UpdateProjectDetails = async (
  projectData: any,
  elData: any,
  dlData: any,
  skillData: any,
  projectDemands: any,
  projectDescription: any,
  projectEndDate: any,
  isEndDateChanged: boolean,
  AdditionalElAndAdditionalDelegateRole: any[],
  projectRoleAdditionalDBData: any[],
  IsConfidential: boolean
) => {
  let projectRoles: any[] = [];
  if (
    AdditionalElAndAdditionalDelegateRole &&
    AdditionalElAndAdditionalDelegateRole.length > 0
  ) {
    //projectRolesView change not required
    projectRoles = [...projectRoles, ...AdditionalElAndAdditionalDelegateRole];
  }
  // projectRoles = getELData(projectData, elData, projectRoles);
  //projectRolesView change not required
  const elUsers = getELData(projectData, elData, projectRoles);
  // projectRoles = getELData(projectData, dlData, projectRoles);
  //projectRolesView change not required
  const dlUsers = getELData(projectData, dlData, projectRoles);
  //projectRoles = { ...projectRoles, ...dlUsers };
  const skillsdata = getSkillData(projectData, skillData);
  const pDemands = getProjectDemandsData(projectData, projectDemands);
  const data = {
    id: projectData.id,
    modifiedBy: projectData.modifiedBy,
    // modifiedAt: moment().toDate(),
    projectRoles: projectRoles,
    projectSkills: skillsdata,
    projectDemands: pDemands,
    Description:
      projectDescription.length > 0
        ? projectDescription
        : projectData.description,
    projectEndDate: projectEndDate,
    isEndDateChanged: isEndDateChanged,
    pipelineCode: projectData.pipelineCode,
    jobCode: projectData.jobCode,
    IsConfidential: IsConfidential,
  };
  const response = await service.UpdateProjectDetails(data);

  // console.log("Project Updated Successfully", response);
  if (response.status === 200) {
    const payload = transformPayloadData(projectRoles);
    // console.log("Project Finalized Roles", payload);
    await updateUserRoleList(payload)
      .then((data) => {
        // console.log(data);
        // console.log("success");
        return true;
      })
      .catch((err) => {
        // console.log(err);
        return false;
      });
    await checkAndRemoveUserRolesFromIdentity(
      data,
      projectRoleAdditionalDBData,
      data.projectRoles,
      projectData
    );
    return true;
  } else {
    return false;
  }
};

const checkAndRemoveUserRolesFromIdentity = async (
  data: any,
  currentDBRoles: any[],
  newProjectRoles: any[],
  projectData: any
) => {
  const userRemovedList1 = data.projectRoles
    .filter((userItem) => !userItem.isActive)
    .map((userItem) => ({
      ...userItem,
      user: userItem?.delegateEmail ? userItem?.delegateEmail : userItem?.user,
      role: userItem?.delegateEmail
        ? RolesListMaster.AdditionalDelegate
        : userItem.role,
    }));

  const userRemoved = transformDataForAdditionalElAndDelegateRoles(
    currentDBRoles,
    newProjectRoles,
    projectData,
    true
  );
  const userRemovedList2 = (userRemoved?.updateData || []).filter(
    (userItem) => userItem.user && !userItem.isActive
  );

  const finalUsersThatAreRemoved = [...userRemovedList1, ...userRemovedList2];

  const emailsIds = Array.from(
    new Set([
      ...userRemovedList1.map((userItem) =>
        userItem?.delegateEmail ? userItem?.delegateEmail : userItem?.user
      ),
      ...userRemovedList2.map((userItem) => userItem.user),
    ])
  );
  if (emailsIds.length) {
    const usersProjectRoles = await getProjectRolesByEmailId(emailsIds);
    const finalUserWithRolesToRemove: RemoveUserRoleByEmailDto[] = [];

    finalUsersThatAreRemoved.forEach((userItem) => {
      const userRoles = usersProjectRoles.find(
        (roleItem) => roleItem.user.toLowerCase() == userItem.user.toLowerCase()
      );
      if (!userRoles) {
        finalUserWithRolesToRemove.push({
          email_id: userItem.user,
          roles: [userItem.role],
        });
      } else {
        if (!userRoles.role.includes(userItem.role)) {
          finalUserWithRolesToRemove.push({
            email_id: userItem.user,
            roles: [userItem.role],
          });
        }
      }
    });

    await RemoveUserRoleByEmail(finalUserWithRolesToRemove);
  }
};

const addUserRoleIdnetity = async (userRole: any) => {
  const _user = userRole.filter((a) => a.isActive);
};
const transformPayloadData = (projectRoles: any[]) => {
  const finalizedRole: any[] = [];
  projectRoles.map((role) => {
    if (role.user && role.user.length > 0) {
      const idx = finalizedRole.findIndex(
        (item) =>
          item.email_id.toLowerCase().trim() === role.user.toLowerCase().trim()
      );
      if (idx === -1) {
        finalizedRole.push({ email_id: role.user, roles: [role.role] });
      } else {
        const roleInfo = finalizedRole[idx];
        const userRolesInfo = roleInfo.roles;
        if (!userRolesInfo.includes(role.role)) {
          userRolesInfo.push(role.role);
        }
        finalizedRole[idx] = {
          ...roleInfo,
          roles: userRolesInfo,
        };
      }
    }
    if (role.delegateEmail && role.delegateEmail.length > 0) {
      const idx = finalizedRole.findIndex(
        (item) =>
          item.email_id.toLowerCase().trim() ===
          role.delegateEmail.toLowerCase().trim()
      );
      if (idx === -1) {
        finalizedRole.push({
          email_id: role.delegateEmail,
          roles: [RolesListMaster.AdditionalDelegate],
        });
      } else {
        const roleInfo = finalizedRole[idx];
        const userRolesInfo = roleInfo.roles;
        if (!userRolesInfo.includes(RolesListMaster.AdditionalDelegate)) {
          userRolesInfo.push(RolesListMaster.AdditionalDelegate);
        }
        finalizedRole[idx] = {
          ...roleInfo,
          roles: userRolesInfo,
        };
      }
    }
  });
  return finalizedRole;
};

const getELData = (project: any, elData: any, roleData: any) => {
  elData.map((data: any) => {
    roleData.push({
      id: data.id,
      projectId: project.id,
      user: data.internalName ? data.internalName : data.user,
      userName: data.label,
      delegateEmail: data.delegateEmail,
      delegateUserName: data.delegateUserName,
      role: data.role,
      isActive: data.isactive,
    });
  });
  return roleData;
};

const getSkillData = (project: any, skillData: any) => {
  const skills: any[] = [];
  skillData.map((data: any) => {
    skills.push({
      id: data.id,
      projectId: project.id,
      skillName: data.label,
      isActive: data.isactive,
    });
  });
  return skills;
};

const getProjectDemandsData = (project: any, projectDemands: any) => {
  const pDemands: any[] = [];

  projectDemands
    .filter((item: any) => (item.isactive && item.id === 0) || item.id !== 0)
    .map((data: any) => {
      const skills: any = [];
      if (data.projectDemandSkills) {
        data?.projectDemandSkills
          .filter(
            (item: any) => (item.isactive && item.id === 0) || item.id !== 0
          )
          .map((skillsData: any) => {
            skills.push({
              id: skillsData.id,
              projectDemandId: data.id,
              skillName: skillsData.label
                ? skillsData.label
                : skillsData.skillName,
              isActive: skillsData.isactive
                ? skillsData.isactive
                : skillsData.isActive,
            });
          });
      }
      pDemands.push({
        id: data.id,
        projectId: project.id,
        designation: data.label,
        noOfResources: data.noOfResources,
        isActive: data.isactive,
        projectDemandSkills: skills,
      });
    });
  return pDemands;
};

const getProjectDescription = (project: any, skillData: any) => {
  const skills: any[] = [];
  skillData.map((data: any) => {
    skills.push({
      id: data.id,
      projectId: project.id,
      skillName: data.label,
      isActive: data.isactive,
    });
  });
  return skills;
};
export interface ITransformDataForAdditionalElAndDelegateRoles {
  isError: boolean;
  error: string | null;
  updateData: any[] | null;
}

export const transformDataForAdditionalElAndDelegateRoles = (
  dbRolesData: IEmployeeDetails[],
  currentRolesData: IEmployeeDetails[],
  projectDetails: any,
  donotCheckElCondition: boolean = false
): ITransformDataForAdditionalElAndDelegateRoles => {
  console.log(dbRolesData, currentRolesData);
  const finalUpdateData: IProjectRoles[] = [];
  const result: ITransformDataForAdditionalElAndDelegateRoles = {
    isError: false,
    error: null,
    updateData: [],
  };

  const emptyAdditionalElRolesList = currentRolesData.filter((role) => {
    if (
      role.additionalElEmail === null ||
      !role.additionalElEmail ||
      role.additionalElEmail?.length === 0
    ) {
      return true;
    } else {
      return false;
    }
  });
  if (emptyAdditionalElRolesList.length > 0 && !donotCheckElCondition) {
    return {
      isError: true,
      error: "Additional EL can not be Empty",
      updateData: [],
    };
  }
  //ITERATE OVER CURRENT ROLES
  currentRolesData.map((currentRole, idx: number) => {
    const dbData = dbRolesData.find((data) => {
      if (
        data?.additionalElEmail?.toLowerCase().trim() ===
          currentRole?.additionalElEmail?.toLowerCase().trim() &&
        ((!data?.additionalDelegateEmail ||
          data?.additionalDelegateEmail?.length === 0) ===
          (!currentRole?.additionalDelegateEmail ||
            currentRole?.additionalDelegateEmail.length === 0) ||
          data?.additionalDelegateEmail?.toLowerCase().trim() ===
            currentRole?.additionalDelegateEmail?.toLowerCase().trim())
      ) {
        return true;
      }
    });
    if (dbData === undefined) {
      //new Added Record
      finalUpdateData.push({
        id: 0,
        projectId: projectDetails.id,
        roleOrder: idx,
        user: currentRole?.additionalElEmail,
        userName: currentRole?.additionalElName,
        role: RolesListMaster.AdditionalEl,
        delegateEmail: currentRole?.additionalDelegateEmail,
        delegateUserName: currentRole?.additionalDelegateName,
        isActive: true,
      } as IProjectRoles);
    } else {
      //update current record
      finalUpdateData.push({
        id: dbData.projectRoleId,
        projectId: projectDetails.id,
        roleOrder: idx,
        user: currentRole?.additionalElEmail,
        userName: currentRole?.additionalElName,
        role: RolesListMaster.AdditionalEl,
        delegateEmail: currentRole?.additionalDelegateEmail,
        delegateUserName: currentRole?.additionalDelegateName,
        isActive: true,
      } as IProjectRoles);
    }
    return { ...currentRole, roleOrder: idx };
  });
  //ITERATE OVER DB
  dbRolesData.map((dbRole) => {
    const currentData = currentRolesData.find((currentRole) => {
      if (
        currentRole?.additionalElEmail?.toLowerCase().trim() ===
          dbRole?.additionalElEmail?.toLowerCase().trim() &&
        ((!currentRole?.additionalDelegateEmail ||
          currentRole?.additionalDelegateEmail.length === 0) ===
          (!dbRole?.additionalDelegateEmail ||
            dbRole?.additionalDelegateEmail.length === 0) ||
          currentRole?.additionalDelegateEmail?.toLowerCase().trim() ===
            dbRole?.additionalDelegateEmail?.toLowerCase().trim())
      ) {
        return true;
      }
    });
    if (currentData === undefined) {
      finalUpdateData.push({
        id: dbRole.projectRoleId,
        projectId: projectDetails.id,
        user: dbRole?.additionalElEmail,
        userName: dbRole?.additionalElName,
        role: RolesListMaster.AdditionalEl,
        delegateUserName: dbRole?.additionalDelegateName,
        delegateEmail: dbRole?.additionalDelegateEmail,
        isActive: false,
      } as IProjectRoles);
    }
  });
  return {
    isError: false,
    error: null,
    updateData: finalUpdateData,
  };
};
