import React, { useContext } from 'react';
import {
    Box,
    Button,
    Card,
    CardContent,
    IconButton,
    Tooltip,
    Typography,
} from '@mui/material';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import { EmployeeProfile } from '../../interfaces/employeeProfile';
import { UserDetailsContext } from '../../../../contexts/userDetailsContext';
import { RolesListMaster } from '../../../../common/enums/ERoles';
import { NameConstants } from '../../constants';
import { InfoRounded } from '@mui/icons-material';

const ExperienceWithGT = (mainProfileData: EmployeeProfile) => {

    const userContext = useContext(UserDetailsContext);

    const columnDefs = [
        { headerName: 'Job Name', field: 'job_name', width: 180 },
        { headerName: 'Job Start Date', field: 'job_start_date', width: 180 },
        { headerName: 'Job End Date', field: 'job_end_date', width: 180 },
        { headerName: 'Primary EL', field: 'primary_el', width: 180 },
        { headerName: 'CSP', field: 'csp', width: 180 },
        { headerName: 'Project Type', field: 'project_type', width: 180 },
        { headerName: 'Project Description', field: 'project_description', width: 180,         
            tooltipValueGetter: (params) => {            
            return params.value && params.value.length > 20 ? params.value : null;}
        },
        { 
            headerName: 'Task Description', field: 'task_description', width: 180,
            tooltipValueGetter: (params) => {            
            return params.value && params.value.length > 20 ? params.value : null;}
         },
        { headerName: 'Skills Utilized', field: 'skills_utilized', width: 180 },
        { headerName: 'Client Group', field: 'client_group', width: 180 },
        { headerName: 'Client Name', field: 'client_name', width: 180 },
        { headerName: 'Business Unit', field: 'business_unit', width: 180 },
        { headerName: 'Offering', field: 'offering', width: 180 },
        { headerName: 'Solution', field: 'solution', width: 180 },
        { headerName: 'Industry', field: 'industry', width: 180 },
        { headerName: 'Sub Industry', field: 'sub_industry', width: 180 },
        { headerName: 'Actual Hours', field: 'actual_hours', width: 180 },
    ];

    const defaultColDef = {
        sortable: true,
        resizable: true
    };

    const rowData = mainProfileData.employee_project_experience || [];

    //No internal project experience found yet.

    return (
        <Card sx={{ mb: 2 }} className='box-radius'>
            <CardContent >
                <Box display="flex" alignItems={'center'} >
                    <Typography variant="h6">Experience with GT</Typography>
                    <Tooltip title={NameConstants.ExperienceWithGT}>
                        <IconButton><InfoRounded /></IconButton>
                    </Tooltip>
                </Box>

                <Box mt={1} className="ag-theme-alpine" style={{ height: 200, width: '100%' }}>
                    <AgGridReact
                        rowData={rowData}
                        columnDefs={columnDefs}
                        getRowStyle={() => ({ background: 'transparent' })}
                        defaultColDef={defaultColDef}
                        overlayNoRowsTemplate="<span style='padding: 10px; color: #666;'>No internal project experience found yet.</span>"
                    />
                </Box>
            </CardContent>
        </Card>
    );
};

export default ExperienceWithGT;