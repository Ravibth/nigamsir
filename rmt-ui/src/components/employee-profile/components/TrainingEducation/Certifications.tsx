import React from 'react';
import { AgGridReact } from 'ag-grid-react';
import { ColDef } from 'ag-grid-community';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-material.css';
import { Typography } from '@mui/material';
import { EmployeeProfile, EmployeeQualification } from '../../interfaces/employeeProfile';
import useTrainingEduData from '../../hooks/useTrainingEduData';
import { ITrainingProps } from '../../interfaces/ITrainingProps';
import AgGridTrainingAndEdu from './AgGridTrainingAndEdu';
import './style.css'

const Certifications: React.FC<ITrainingProps> = ({ tempData, setTempData, onSave,isEditable, ...mainProfileData }) => {

    const { data } = useTrainingEduData(mainProfileData, "Certification");

    const columnDefs: ColDef<EmployeeQualification>[] = [
        {
            headerName: '',
            field: 'is_published',
            width: 1,
            hide:!isEditable,
            headerComponent: (params: any) => {
                const allSelected = params.api.getDisplayedRowCount() > 0 &&
                    params.api.getModel().rowsToDisplay.every((row: any) => row.data.is_published);

                return (
                    <input
                        type="checkbox"
                        checked={allSelected}
                        onChange={(e) => {
                            const checked = e.target.checked;

                            const updatedRows: EmployeeQualification[] = [];
                            params.api.forEachNode((node: any) => {
                                const updated = { ...node.data, is_published: checked };
                                updatedRows.push(updated);
                            });

                            // Update AG Grid
                            params.api.applyTransaction({ update: updatedRows });

                            // Update your external state
                            setTempData(prev => ({
                                ...prev,
                                qualification_update: updatedRows.map(x => ({
                                    id: x.id,
                                    is_published: x.is_published
                                }))
                            }));
                        }}
                    />
                );
            },
            cellRenderer: (params: any) => {
                return (
                    <input
                        type="checkbox"
                        checked={params.value}
                        onChange={(e) => {
                            const updatedData = { ...params.data, is_published: e.target.checked };

                            // Update AG Grid row data
                            params.api.applyTransaction({ update: [updatedData] });

                            setTempData(prev => {
                                const newData = {
                                    ...prev,
                                    qualification_update: [{
                                        id: updatedData?.id,
                                        is_published: updatedData.is_published
                                    }]
                                };
                                return newData;
                            });
                        }}
                    />
                );
            },
            sortable: false,
            suppressMenu: true,
            pinned: 'left'
        },
        {
            headerName: 'Certification Name',
            field: 'qualification',
            sortable: true
        },
        {
            headerName: 'Institution Name',
            field: 'institute_name',
            sortable: true
        },
        {
            headerName: 'Location',
            field: 'institute_location_name',
            sortable: true
        },
        {
            headerName: 'Year of Passing',
            field: 'month_year_of_passing',
            sortable: true
        }
    ];

    return (
        <div className='qualification-box'>
            <Typography variant="h6" gutterBottom>
                Certfiication
            </Typography>

            <AgGridTrainingAndEdu data={data} columnDefs={columnDefs} />
        </div>
    )
}

export default Certifications