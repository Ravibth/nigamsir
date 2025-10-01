import React from 'react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-material.css';
import { Box, Card, CardContent, IconButton, Tooltip, Typography } from '@mui/material';
import Education from './Education';
import ProfessionalQualifications from './Professional';
import { EmployeeProfile } from '../../interfaces/employeeProfile';
import Certifications from './Certifications';
import { InfoRounded } from '@mui/icons-material';
import { NameConstants } from '../../constants';
import '../../EmployeeProfile.css'

interface EducationProps extends EmployeeProfile {
  tempData: any;
  isEditable:boolean;
  setTempData: React.Dispatch<React.SetStateAction<any>>;
  onSave: (section: string) => void;
}

const TrainingEducation = (props: EducationProps) => {

  const { tempData, setTempData, onSave, isEditable, ...mainProfileData } = props;

  return (
    <>
      <Box display="flex" alignItems={'center'} ml={2}>
        <Typography variant="h6">Training & Education</Typography>
        <Tooltip title={NameConstants.TrainingInfo}>
          <IconButton><InfoRounded /></IconButton>
        </Tooltip>
      </Box>
      <Card className='box-radius'>
        <CardContent>
          <Box sx={{ display: 'flex', flexDirection: 'column', border: "1px", borderColor: "black"}}>
            <Education
              {...mainProfileData}
              tempData={tempData}
              setTempData={setTempData}
              onSave={onSave}
              isEditable={isEditable}
            />
            <ProfessionalQualifications
              {...mainProfileData}
              tempData={tempData}
              setTempData={setTempData}
              onSave={onSave}
              isEditable={isEditable}
            />
            {/* <Certifications
              {...mainProfileData}
              tempData={tempData}
              setTempData={setTempData}
              onSave={onSave}
              isEditable={isEditable}
            /> */}
          </Box>
        </CardContent>
      </Card>
    </>
  );
};

export default TrainingEducation;