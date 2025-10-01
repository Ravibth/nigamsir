import { useState } from 'react';
import {
  Box,
  Button,
  IconButton,
  Typography,
  SxProps,
  Theme,
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Cancel';
import { EmpProfileEditButtonSx } from '../constants';

interface UseEditButtonProps {
  title: string;
  onSave: () => void;
  onCancel: () => void;
  onEdit: () => void;
  isSaveDisabled?: boolean;
  editButtonSx?: SxProps<Theme>;
}

export const useEditButton = ({
  title,
  onSave,
  onCancel,
  onEdit,
  isSaveDisabled = false,
  editButtonSx = {},
}: UseEditButtonProps) => {
  const [editMode, setEditMode] = useState(false);

  const handleEdit = () => {
    setEditMode(true);
    onEdit();
  };

  const handleSave = () => {
    setEditMode(false);
    onSave();
  };

  const handleCancel = () => {
    setEditMode(false);
    onCancel();
  };

  const EditButtonComponent = () => (
    <Box display="flex" justifyContent="space-between" alignItems="center">
      <Typography variant="h6">{title}</Typography>
      {editMode ? (
        <Box>
          <IconButton onClick={handleSave} disabled={isSaveDisabled}>
            <SaveIcon />
          </IconButton>
          <IconButton onClick={handleCancel}>
            <CancelIcon />
          </IconButton>
        </Box>
      ) : (
        <Button
          onClick={handleEdit}
          variant="outlined"
          startIcon={<EditIcon />}
          sx={EmpProfileEditButtonSx}
        >
          Edit
        </Button>
      )}
    </Box>
  );

  return { EditButtonComponent, editMode, setEditMode };
};