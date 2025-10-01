import React, { useContext, useEffect, createContext, useState } from 'react';
import { Grid, Typography } from '@mui/material';
import useProfileData from './hooks/useProfileData';
import { useProfileEdit } from './hooks/useProfileEdit';
import ProfileLeftPanel from './components/ProfileLeftPanel';
import AboutMeSection from './components/AboutMeSection';
import { SaveEmployeeProfile } from './services/employeeProfileService';
import { formatLanguages, formatQualifications, IndustryColumns } from './util';
import { UserDetailsContext } from '../../contexts/userDetailsContext';
import SkillsEmpProfile from './components/SkillsEmpProfile';
import EditableTableSection from './components/EditableTableSection'
import { useNavigate, useParams } from 'react-router-dom';
import WorkExperience from './components/WorkExperience';
import TrainingEducation from './components/TrainingEducation/TrainingEducation';
import { NameConstants } from './constants';
import IndustryExperience from './components/IndustryExperience';
import ProjectExperience from './components/ProjectExperience/ProjectExperience';
const ProfilePage = () => {
  const [isSelfProfileOpened, setIsSelfProfileOpened]=useState<boolean>(false);
  const { userEmailId } = useParams();
  const userContext = useContext(UserDetailsContext);
  const userEmail = userEmailId? userEmailId : userContext.username;

  const { mainProfileData, linkedInURL, loading, error, setMainProfileData, setLinkedInURL, aboutMe } = useProfileData(userEmail);
  const { editModes, tempData, setTempData, handleEdit, handleCancel, handleFieldChange, handleNestedFieldChange, setEditModes } = useProfileEdit({
    aboutMe: aboutMe,
    industry: [],
    project: [],
    linkedIn: '',
    yearsOfExp: 0,
    trainingEducation: []
  });

  useEffect(()=>{
    if(!userEmailId || userEmailId==userContext.username){
      setIsSelfProfileOpened(true);
    }else{
      setIsSelfProfileOpened(false);
    }
  },[userEmailId])

  useEffect(() => {
    if (mainProfileData) {
      setTempData({
        aboutMe: mainProfileData.about_me || aboutMe,
        industry: mainProfileData.employee_industry_expereience || [],
        project: mainProfileData.employee_project_experience || [],
        linkedIn: linkedInURL || '',
        yearsOfExp: mainProfileData.year_of_experience || 0,
        trainingEducation: mainProfileData.employee_qualification || [],
        present_work_location: mainProfileData.present_work_location || '',
        experience_outside_gt: mainProfileData.experience_outside_gt || [],
      });
    }
  }, [mainProfileData, linkedInURL, editModes]);

  useEffect(() => {
    if (tempData.qualification_update) {
      onSave('trainingEducation');
    }
  }, [tempData.qualification_update]);

  
  useEffect(() => {
    if (tempData.experience_outside_gt) {      
      onSave('experience_outside_gt');
    }
    console.log("Updated tempData:", tempData);
  }, [tempData.experience_outside_gt]);

  const onSave = async (section) => {
    console.log('saving profile')
    const success = await SaveEmployeeProfile(userEmail, tempData, mainProfileData, setMainProfileData, setLinkedInURL);
    if (success) {
      setEditModes(prev => ({ ...prev, [section]: false }));
    }
  };

  if (loading) return <Typography>Loading...</Typography>;
  if (error) return <Typography color="error">{error}</Typography>;
  if (!mainProfileData) return <Typography>No profile data found</Typography>;

  return (
    <Grid container spacing={2} p={2} sx={{backgroundColor:"lightgrey"}} >
      <ProfileLeftPanel
        profileData={mainProfileData}
        editModes={editModes}
        tempData={tempData}
        handleEdit={handleEdit}
        handleCancel={(section) => handleCancel(section, {
          ...tempData,
          [section]: mainProfileData[section] || (section === NameConstants.linkedIn ? linkedInURL : (section === NameConstants.yearsOfExp ? mainProfileData.year_of_experience : ''))
        })}
        handleSave={onSave}
        handleFieldChange={handleFieldChange}
        formatQualifications={formatQualifications}
        formatLanguages={formatLanguages}
        isEditable={isSelfProfileOpened}
      />

      {/* Right Panel */}
      <Grid item xs={12} md={9.5}>

        <AboutMeSection
          editMode={editModes.aboutMe}
          aboutMe={tempData.aboutMe}
          onEdit={() => handleEdit('aboutMe')}
          onCancel={() => handleCancel('aboutMe', {
            ...tempData,
            aboutMe: mainProfileData.about_me || aboutMe
          })}
          onSave={() => onSave('aboutMe')}
          onAboutMeChange={(value) => handleFieldChange('aboutMe', value)}
          isEditable={isSelfProfileOpened}
        />

        <SkillsEmpProfile mainProfileData={mainProfileData} isEditable={isSelfProfileOpened}/>

        {/* <IndustryExperience {...mainProfileData} /> */}

        {/* <WorkExperience {...mainProfileData} /> */}

        <ProjectExperience {...mainProfileData} setTempData={setTempData} onSave={() => onSave('experience_outside_gt')} />

        <TrainingEducation {...mainProfileData} tempData={tempData} setTempData={setTempData} onSave={onSave} isEditable={isSelfProfileOpened}/>

        {/* <TrainingEducation {...mainProfileData} tempData={tempData} setTempData={setTempData} onSave={onSave} /> */}
        <IndustryExperience mainProfileData={mainProfileData} isEditable={isSelfProfileOpened}/>
        
        <WorkExperience mainProfileData={mainProfileData} isEditable={isSelfProfileOpened}/>


      </Grid>
    </Grid>
  );
};

export default ProfilePage;