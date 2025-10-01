import React from 'react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-material.css';
import { Box, Card, CardContent, IconButton, Tooltip, Typography } from '@mui/material';
import { InfoRounded } from '@mui/icons-material';
import { NameConstants } from '../../constants';
import '../../EmployeeProfile.css'
import { EmployeeProfile } from '../../interfaces/employeeProfile';
import ExperienceWithGT from './ExperienceWithGT';
import ExperienceOutsideGT from './ExperienceOutsideGT';

export interface ProjectExperienceProps extends EmployeeProfile {
  onSave: () => void;
  setTempData: React.Dispatch<React.SetStateAction<any>>;
}

const ProjectExperience = (props: ProjectExperienceProps) => {
 const { onSave,setTempData, ...mainProfileData } = props;
  return (
    <>
      <Box display="flex" alignItems={'center'} ml={2}>
        <Typography variant="h6">ProjectExperience</Typography>
      </Box>
      <Card className='box-radius'>
        <CardContent>
          <Box sx={{ display: 'flex', flexDirection: 'column', border: "1px", borderColor: "black"}}>
            <ExperienceWithGT {...mainProfileData}/>
            <ExperienceOutsideGT {...mainProfileData} setTempData={setTempData} onSave={onSave}/>
          </Box>
        </CardContent>
      </Card>
    </>
  );
};

export default ProjectExperience;