import { InfoRounded } from '@mui/icons-material'
import { Box, Card, CardContent, Divider, Grid, IconButton, Tooltip, Typography } from '@mui/material'
import React from 'react'
import { EmployeeProfile } from '../interfaces/employeeProfile'
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { NameConstants } from '../constants';

function WorkExperience({ mainProfileData, isEditable = true }: { mainProfileData: EmployeeProfile; isEditable?: boolean }) {

  const columnDefs = [
    { field: 'name_of_employer', headerName: 'Name of the Employer', flex: 4 },
    {
      field: 'from',
      headerName: 'From',
      valueFormatter: (params: any) => new Date(params.value).toLocaleDateString(),
      flex: 2
    },
    {
      field: 'to',
      headerName: 'To',
      valueFormatter: (params: any) => new Date(params.value).toLocaleDateString(),
      flex: 2
    },
    { field: 'last_designation_held', headerName: 'Last designation held', flex: 4 }
  ];

  const rowData = mainProfileData.employee_Work_experience || [];

  return (
    <>
      <Box display="flex" alignItems={'center'} ml={2} >
        <Typography variant="h6">Work Experience</Typography>
        <Tooltip title={NameConstants.WorkExpInfo}>
          <IconButton><InfoRounded /></IconButton>
        </Tooltip>
      </Box>
      <Card sx={{ mb: 2 }} className='box-radius'>
        <CardContent>
          <Box mt={1} className="ag-theme-alpine" style={{ height: 'auto', width: '100%' }}>
            <AgGridReact
              columnDefs={columnDefs}
              rowData={rowData}
              domLayout='autoHeight'
              headerHeight={40}
              rowHeight={40}
              getRowStyle={() => ({ background: 'transparent' })}
              suppressCellFocus={true}
              defaultColDef={{
                sortable: true,
                resizable: true,
              }}
            />
          </Box>
        </CardContent>
      </Card>
    </>
  );
}

export default WorkExperience