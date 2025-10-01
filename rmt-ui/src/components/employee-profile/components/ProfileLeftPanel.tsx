import React from 'react';
import {
  Typography,
  Box,
  Link,
  Card,
  CardContent,
  Divider,
  IconButton,
  TextField,
  Button,
  Grid,
  Tooltip
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Cancel';
import {
  WorkOutlineRounded,
  OutlinedFlag,
  BusinessOutlined,
  SchoolOutlined,
  SupervisorAccountOutlined,
  PeopleAltOutlined,
  LocationOnOutlined,
  TimelapseOutlined,
  LinkedIn,
  MenuBook,
  AutoStories,
  InfoRounded
} from '@mui/icons-material';
import { GT_DESIGN_PARAMETERS } from '../../../global/constant';
import { formatLink } from '../util';
import { NameConstants } from '../constants';

const ProfileLeftPanel = ({
  profileData,
  editModes,
  tempData,
  handleEdit,
  handleCancel,
  handleSave,
  handleFieldChange,
  formatQualifications,
  formatLanguages,
  isEditable
}) => {
  return (
    <Grid item xs={12} md={2.5}>
      <Card sx={{ height: "100%" }}>
        <CardContent>
          <Grid container spacing={1} direction="column" alignItems="center" marginLeft="1px">
            <Typography variant="h6">{profileData.employee_name}</Typography>
            <Link href="#" underline="hover" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}>
              Grant Thornton Bharat LLP
            </Link>
            <Divider sx={{ my: 1 }} />
            <Typography align="left" width="100%"> Basic Details </Typography>
            <Divider sx={{ my: 0.5 }} />
            <Box width="100%">
              <Typography sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <WorkOutlineRounded sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                <Box component="span" sx={{ fontWeight: 'bold' }}>Employee Type:</Box>
                <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}>
                  {profileData.employee_type || 'N/A'}
                </Box>
              </Typography>

              <Typography sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <WorkOutlineRounded sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                <Box component="span" sx={{ fontWeight: 'bold' }}>Designation:</Box>
                <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}>
                  {profileData.designation}
                </Box>
              </Typography>

              <Typography sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <WorkOutlineRounded sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                <Box component="span" sx={{ fontWeight: 'bold' }}>Business Unit:</Box>
                <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}>
                  {profileData.business_unit}
                </Box>
              </Typography>

              <Typography sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <OutlinedFlag sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                <Box component="span" sx={{ fontWeight: 'bold' }}>Competency:</Box>
                <Tooltip title={profileData.competency}>
                  <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple, overflow: 'hidden', textOverflow: "ellipsis", whiteSpace: 'nowrap' }}>
                    {profileData.competency}
                  </Box>
                </Tooltip>

              </Typography>

              <Typography sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <OutlinedFlag sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                <Box component="span" sx={{ fontWeight: 'bold' }}>Super Coach:</Box>
                <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}>
                  {profileData.supercoach_name || 'N/A'}
                </Box>
              </Typography>

              <Typography sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <OutlinedFlag sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                <Box component="span" sx={{ fontWeight: 'bold' }}>Co-Super Coach:</Box>
                <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple }}>
                  {profileData.co_supercoach_name || 'N/A'}
                </Box>
              </Typography>

              <Typography sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <LocationOnOutlined sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                <Box component="span" sx={{ fontWeight: 'bold' }}></Box>
                <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple, marginLeft: "-7px" }}>
                  {profileData.location || 'N/A'}
                </Box>
              </Typography>

              {/* Present Work Location */}
              <Typography sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', gap: 1, mb: 1 }}>
                <LocationOnOutlined sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                {editModes.present_work_location ? (
                  <TextField
                    value={tempData.present_work_location}
                    onChange={(e) => handleFieldChange('present_work_location', e.target.value)}
                    sx={{ mt: 1 }}
                  />
                ) : (
                  <>
                    <Box sx={{ display: "flex", alignItems: "flex-start", justifyContent: "flex-start", width: "100%", }} >
                      <Box component="span" sx={{ fontWeight: "bold", textAlign: "left", mr: 0.5 }} >
                        Present Work Location
                      </Box>
                      <Tooltip title={NameConstants.PresentWorkLocation}>
                        <IconButton size="small" sx={{ p: 0, mt: "-2px" }}>
                          <InfoRounded fontSize="small" />
                        </IconButton>
                      </Tooltip>
                    </Box>

                    <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple, width: "50%" }} >
                      {profileData.present_work_location
                        ? profileData.present_work_location
                        : "N/A"}
                    </Box>
                  </>

                )}

                {editModes.present_work_location ? (
                  <Box>
                    <IconButton onClick={() => handleSave('present_work_location')}>
                      <SaveIcon />
                    </IconButton>
                    <IconButton onClick={() => handleCancel('present_work_location')}>
                      <CancelIcon />
                    </IconButton>
                  </Box>
                ) : (
                  <Button onClick={() => handleEdit('present_work_location')} startIcon={<EditIcon />} />
                )}
              </Typography>

              {/* YEARS OF EXPERIENCE */}
              <Typography sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', gap: 1, mb: 1 }}>
                <OutlinedFlag sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                {editModes.yearsOfExp ? (
                  <TextField
                    value={tempData.yearsOfExp}
                    onChange={(e) => {
                      // Validate the input to allow only two decimal places
                      const value = e.target.value;
                      if (/^\d*\.?\d{0,2}$/.test(value) || value === '') {
                        handleFieldChange('yearsOfExp', value);
                      }
                    }}
                    onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>) => {
                      const target = e.target as HTMLInputElement;
                      // Allow: digits (0-9), '.', Backspace (8), Tab (9), Enter (13), Arrow keys (37-40), Delete (46)
                      if (
                        (e.key >= '0' && e.key <= '9') ||
                        (e.key === '.' && !target.value.includes('.')) ||
                        [8, 9, 13, 37, 38, 39, 40, 46].includes(e.keyCode)
                      ) {
                        // Additional check for existing decimal places
                        if (e.key >= '0' && e.key <= '9') {
                          const decimalParts = target.value.split('.');
                          if (decimalParts.length > 1 && decimalParts[1].length >= 2) {
                            e.preventDefault();
                            return;
                          }
                        }
                        return; // Allow the key
                      }
                      e.preventDefault(); // Block other keys
                    }}
                    sx={{ mt: 1 }}
                    inputProps={{
                      inputMode: 'decimal',
                      pattern: '\\d*\\.?\\d{0,2}',  // Regex for up to 2 decimal places
                    }}
                  />
                ) : (
                  <>
                    <div style={{ display: 'block', textAlign: 'left', width: '100%' }} >
                      <Box component="span" sx={{ fontWeight: 'bold', textAlign: 'left' }}>Years of Experience</Box>
                    </div>
                    <Box component="span" sx={{ color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple, width: '50%' }}>
                      {profileData.year_of_experience ? `${profileData.year_of_experience} Years` : 'N/A'}
                    </Box>
                  </>
                )}

                {editModes.yearsOfExp ? (
                  <Box>
                    <IconButton onClick={() => handleSave('yearsOfExp')}>
                      <SaveIcon />
                    </IconButton>
                    <IconButton onClick={() => handleCancel('yearsOfExp')}>
                      <CancelIcon />
                    </IconButton>
                  </Box>
                ) : (
                  isEditable && <Button onClick={() => handleEdit('yearsOfExp')} startIcon={<EditIcon />} />
                )}
              </Typography>

              {/* LINKEDIN SECTION */}
              <Typography sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', gap: 1 }}>
                <LinkedIn sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                <Tooltip title={tempData.linkedIn}>

                  {editModes.linkedIn ? (
                    <TextField
                      value={tempData.linkedIn}
                      onChange={(e) => handleFieldChange('linkedIn', e.target.value)}
                      sx={{ mt: 1 }}
                    />
                  ) : (
                    <Link
                      sx={{ fontWeight: "bold", overflow: 'hidden', textOverflow: "ellipsis", whiteSpace: 'nowrap', display: 'block', textAlign: 'left', width: '100%' }}
                      href={formatLink(tempData.linkedIn)}
                      underline="hover"
                      target="_blank"
                    >
                      {tempData.linkedIn || 'N/A'}
                    </Link>
                  )}
                </Tooltip>

                {editModes.linkedIn ? (
                  <Box>
                    <IconButton onClick={() => handleSave('linkedIn')}>
                      <SaveIcon />
                    </IconButton>
                    <IconButton onClick={() => handleCancel('linkedIn')}>
                      <CancelIcon />
                    </IconButton>
                  </Box>
                ) : (
                  isEditable && <Button onClick={() => handleEdit('linkedIn')} startIcon={<EditIcon />} />
                )}
              </Typography>
            </Box>
            <Divider sx={{ my: 2 }} />

            {/* Education Qualification */}
            <Box width="100%" sx={{ mb: 2, position: 'relative' }}>
              <Typography variant="subtitle1" sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                Education Qualification
              </Typography>
              <Typography sx={{
                display: 'flex',
                alignItems: 'center',
                gap: 1,
                pl: 1,
                color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
                fontWeight: 'bold',
                position: 'relative',
                paddingLeft: '32px'
              }}>
                <MenuBook sx={{
                  color: GT_DESIGN_PARAMETERS.GTTealColor,
                  position: 'absolute',
                  left: 0,
                  top: 0
                }} />
                {formatQualifications(profileData, 'Education') || 'N/A'}
              </Typography>
            </Box>

            {/* Professional Qualification */}
            <Box width="100%" sx={{ mb: 2, position: 'relative' }}>
              <Typography variant="subtitle1" sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                Professional Qualification
              </Typography>
              <Typography sx={{
                display: 'flex',
                alignItems: 'center',
                gap: 1,
                pl: 1,
                color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
                fontWeight: 'bold',
                position: 'relative',
                paddingLeft: '32px'
              }}>
                <MenuBook sx={{
                  color: GT_DESIGN_PARAMETERS.GTTealColor,
                  position: 'absolute',
                  left: 0,
                  top: 0
                }} />
                {formatQualifications(profileData, 'Professional') || 'N/A'}
              </Typography>
            </Box>

            {/* Certifications */}
            {/* <Box width="100%" sx={{ mb: 2, position: 'relative' }}>
              <Typography variant="subtitle1" sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                Certifications
              </Typography>
              <Typography sx={{
                display: 'flex',
                alignItems: 'center',
                gap: 1,
                pl: 1,
                color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
                fontWeight: 'bold',
                position: 'relative',
                paddingLeft: '32px'
              }}>
                <MenuBook sx={{
                  color: GT_DESIGN_PARAMETERS.GTTealColor,
                  position: 'absolute',
                  left: 0,
                  top: 0
                }} />
                {formatQualifications(profileData, 'Certification') || 'N/A'}
              </Typography>
            </Box> */}
            {/* Languages */}
            <Box width="100%" sx={{ mb: 2, position: 'relative' }}>
              <Typography variant="subtitle1" sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                Languages
              </Typography>
              <Typography sx={{
                display: 'flex',
                alignItems: 'center',
                gap: 1,
                pl: 1,
                color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
                fontWeight: 'bold',
                position: 'relative',
                paddingLeft: '32px'
              }}>
                <AutoStories sx={{
                  color: GT_DESIGN_PARAMETERS.GTTealColor,
                  position: 'absolute',
                  left: 0,
                  top: 0
                }} />
                {formatLanguages(profileData, 'Certification') || 'N/A'}
              </Typography>
              {/* <Typography sx={{ display: 'flex', alignItems: 'center', gap: 1, pl: 1, color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple, fontWeight: 'bold' }}>
                <AutoStories sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
                {formatLanguages(profileData) || 'N/A'}
              </Typography> */}
            </Box>
          </Grid>
        </CardContent>
      </Card>
    </Grid>
  );
};

export default ProfileLeftPanel;