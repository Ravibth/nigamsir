import React from 'react';
import {
    Box,
    Button,
    Card,
    CardContent,
    Tooltip,
    Typography,
} from '@mui/material';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import EditIcon from '@mui/icons-material/Edit';
import { useNavigate } from 'react-router-dom';
import { EmpProfileEditButtonSx } from '../constants';
import { EmployeeProfile } from '../interfaces/employeeProfile';

const IndustryExperience = ({ mainProfileData, isEditable = true }: { mainProfileData: EmployeeProfile; isEditable?: boolean }) => {

    const columnDefs = [
        { header: 'Industry', field: 'industry_name', flex: 1 },
        { header: 'Sub Industry', field: 'sub_industry_name', flex: 1 },
        { header: 'Years of Experience', field: 'year_of_experience', flex: 1 },
        {
            header: 'Details',
            field: 'description',
            flex: 1,
            cellRenderer: (params: any) => {
                const text = params.value || "";
                const maxLength = 20; // limit text length
                const truncated =
                    text.length > maxLength ? text.substring(0, maxLength) + "..." : text;

                return (
                    <Tooltip title={text} arrow placement="top">
                        <span>{truncated}</span>
                    </Tooltip>
                );
            },
        },
    ];

    const defaultColDef = {
        sortable: true,
        resizable: true,
    };

    const navigate = useNavigate();

    function handleEdit() {
        navigate('/my-preference');
    }

    const rowData = mainProfileData.employee_industry_expereience || [];

    return (
        <Card sx={{ mb: 2 }} className='box-radius'>
            <CardContent >
                <Box display="flex" justifyContent="space-between" alignItems={'center'} >
                    <Typography variant="h6">Industry Experience</Typography>
                    {isEditable &&<Button
                        onClick={handleEdit}
                        variant="outlined"
                        startIcon={<EditIcon />}
                        sx={EmpProfileEditButtonSx}
                    >
                        Edit
                    </Button>}
                </Box>

                <Box mt={1} className="ag-theme-alpine" style={{ height: 200, width: '100%' }}>
                    <AgGridReact
                        rowData={rowData}
                        columnDefs={columnDefs}
                        getRowStyle={() => ({ background: 'transparent' })}
                        defaultColDef={defaultColDef}
                    />
                </Box>
            </CardContent>
        </Card>
    );
};

export default IndustryExperience;