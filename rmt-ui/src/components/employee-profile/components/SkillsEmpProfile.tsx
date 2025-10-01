import { Box, Button, Card, CardContent, Chip, Typography } from '@mui/material'
import React from 'react'
import { EmployeeProfile, skills } from '../interfaces/employeeProfile'
import {skillColorMap } from '../constants'
import { Link, useNavigate } from 'react-router-dom';
import { useEditButton } from '../hooks/useEditButton';

function SkillsEmpProfile({ mainProfileData, isEditable = true }: { mainProfileData: EmployeeProfile; isEditable?: boolean }) {
    const navigate = useNavigate();
    let data = mainProfileData.skillInformation;
    data.sort((a, b) => {
        const nameA = a.skillName.toUpperCase();
        const nameB = b.skillName.toUpperCase();
        if (nameA < nameB) return -1;
        if (nameA > nameB) return 1;
        return 0;
    });

    function handleEdit() {
        navigate('/myskill')
    }

    const { EditButtonComponent } = useEditButton({
        title: 'Skills',
        onSave: null,
        onCancel: null,
        onEdit: handleEdit,
        isSaveDisabled: true
    });

    return (
        <Card sx={{ mb: 2 }} className='box-radius'>
            <CardContent>
                {isEditable && <EditButtonComponent />}
                <Box mt={1} display="flex" gap={1} flexWrap="wrap">
                    {data ? (
                        data.map((skill, index) => {
                            let colour = skillColorMap[skill.proficiency]
                            return <Chip key={index} label={skill.skillName} sx={{ backgroundColor: colour, color: "white" }} variant="outlined" />
                        }
                        )
                    ) : (<Typography>
                        No approved skills available.
                    </Typography>)}
                </Box>
            </CardContent>
        </Card>
    )
}

export default SkillsEmpProfile