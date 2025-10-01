import React from 'react';
import {
  Box,
  Card,
  CardContent,
} from '@mui/material';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { useEditButton } from '../hooks/useEditButton';

const EditableTableSection = ({
  title,
  editMode,
  data,
  columns,
  onEdit,
  onCancel,
  onSave,
  onFieldChange,
  height
}) => {

  const columnDefs = columns.map(column => ({
    headerName: column.header,
    field: column.field,
    flex: 1,
    editable: editMode,
    cellRenderer: editMode ? undefined : (params) => {
      return `${params.value}${column.append || ''}`;
    },
    suppressSizeToFit: true,
  }));

  const defaultColDef = {
    sortable: true,
    resizable: true,
  };

  const onCellValueChanged = (event) => {
    onFieldChange(event.rowIndex, event.colDef.field, event.newValue);
  };
  
  const { EditButtonComponent } = useEditButton({ title: title, onSave: onSave, onCancel: onCancel, onEdit: onEdit, isSaveDisabled: true });

  return (
    <Card sx={{ mb: 2 }} className='box-radius'>
      <CardContent>
        <EditButtonComponent />
        <Box mt={1} className="ag-theme-alpine" style={{ height: height, width: '100%' }}>
          <AgGridReact
            rowData={data}
            columnDefs={columnDefs}
            getRowStyle={() => ({ background: 'transparent' })}
            defaultColDef={defaultColDef}
            onCellValueChanged={onCellValueChanged}
            suppressCellFocus={!editMode}
          />
        </Box>
      </CardContent>
    </Card>
  );
};

export default EditableTableSection;