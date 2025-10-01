import React from 'react';
import { Box, Typography, TextField, Button, IconButton, Card, CardContent } from '@mui/material';
import {useEditButton} from '../hooks/useEditButton'
import { NameConstants } from '../constants';

const AboutMeSection = ({ editMode, aboutMe, onEdit, onCancel, onSave, onAboutMeChange, isEditable }) => {
  const isSaveDisabled = aboutMe.length < 100;

  const { EditButtonComponent } = useEditButton({ title: 'About Me', onSave: onSave, onCancel: onCancel, onEdit: onEdit, isSaveDisabled });
  
  return (
    <Card sx={{ mb: 2 }} className='box-radius'>
      <CardContent>
        {isEditable && <EditButtonComponent />}
        {editMode ? (
          <Box>
            <TextField
              multiline
              fullWidth
              minRows={3}
              value={aboutMe}
              onChange={(e) => onAboutMeChange(e.target.value)}
              sx={{ mt: 1 }}
              error={aboutMe.length < 100 && aboutMe.length > 0}
              helperText={
                aboutMe.length < 100 && aboutMe.length > 0 
                  ? NameConstants.AboutMeMinTextLimit
                  : ''
              }
            />
            <Typography variant="caption" color="textSecondary">
              {aboutMe.length}/100 minimum characters
            </Typography>
          </Box>
        ) : (
          <Typography mt={1} whiteSpace="pre-line">
            {aboutMe}
          </Typography>
        )}
      </CardContent>
    </Card>
  );
};

export default AboutMeSection;