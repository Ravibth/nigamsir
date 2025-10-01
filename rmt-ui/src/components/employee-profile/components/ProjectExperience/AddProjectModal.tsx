import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    TextField,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Grid,
    Box,
} from '@mui/material';

import { useState, useEffect } from 'react';
import { ProjectExperienceOutsideGT } from '../../interfaces/employeeProfile';
import { IIndustryOptionList, ISubIndustryOptionList } from '../../../manage/employee-preferences/industry/interface';
import { IIndustryMasterList } from '../../../../common/interfaces/IIndustryMaster';
import _ from "lodash";

export interface AddProjectModalProps {
    open: boolean;
    onClose: () => void;
    onSave: (project: ProjectExperienceOutsideGT) => void;
    industryOptions: IIndustryOptionList;
    industryMaster: IIndustryMasterList;
}

const AddProjectModal = ({
    open,
    onClose,
    onSave,
    industryOptions,
    industryMaster,
}: AddProjectModalProps) => {
    const [newProject, setNewProject] = useState<ProjectExperienceOutsideGT>({
        project_name: '',
        client_name: '',
        industry: '',
        sub_industry: '',
        project_location: '',
        project_description: '',
        tasks_performed: '',
        job_start_date: '',
        job_end_date: ''
    });

    const [subIndustryOptions, setSubIndustryOptions] = useState<string[]>([]);

    // Reset form when modal opens/closes
    useEffect(() => {
        if (open) {
            setNewProject({
                project_name: '',
                client_name: '',
                industry: '',
                sub_industry: '',
                project_location: '',
                project_description: '',
                tasks_performed: '',
                job_start_date: '',
                job_end_date: ''
            });
        }
    }, [open]);

    // Update sub-industry options when industry changes
    useEffect(() => {
        if (newProject.industry && industryMaster.length > 0) {
            const filteredSubIndustries = industryMaster
                .filter(item => item.industry_name === newProject.industry)
                .map(item => item.sub_industry_name)
                .filter((subIndustry): subIndustry is string => !!subIndustry);

            const uniqueSubIndustries = _.uniq(filteredSubIndustries);
            setSubIndustryOptions(uniqueSubIndustries);

            // Clear sub-industry if it's no longer valid for the selected industry
            if (newProject.sub_industry && !uniqueSubIndustries.includes(newProject.sub_industry)) {
                setNewProject(prev => ({ ...prev, sub_industry: '' }));
            }
        } else {
            setSubIndustryOptions([]);
        }
    }, [newProject.industry, industryMaster]);

    const handleInputChange = (field: keyof ProjectExperienceOutsideGT, value: string) => {
        setNewProject(prev => ({
            ...prev,
            [field]: value
        }));
    };

    const handleIndustryChange = (value: string) => {
        setNewProject(prev => ({
            ...prev,
            industry: value,
            sub_industry: '' // Clear sub-industry when industry changes
        }));
    };

    const handleSave = () => {
        onSave(newProject);
        onClose();
    };

    const isFormValid = () => {
        return newProject.project_name.trim() !== '' &&
            newProject.client_name.trim() !== '' &&
            newProject.industry.trim() !== '' &&
            newProject.job_start_date.trim() !== '' &&
            newProject.job_end_date.trim() !== '';
    };

    return (
        <Dialog open={open} onClose={onClose} maxWidth="md" fullWidth>
            <DialogTitle>
                <Box display="flex" justifyContent="space-between" alignItems="center">
                    Add New Project Experience
                </Box>
            </DialogTitle>

            <DialogContent>
                <Grid container spacing={2} sx={{ mt: 1 }}>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Project Name *"
                            value={newProject.project_name}
                            onChange={(e) => handleInputChange('project_name', e.target.value)}
                            margin="normal"
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Client Name *"
                            value={newProject.client_name}
                            onChange={(e) => handleInputChange('client_name', e.target.value)}
                            margin="normal"
                        />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                        <FormControl fullWidth margin="normal">
                            <InputLabel>Industry *</InputLabel>
                            <Select
                                value={newProject.industry}
                                label="Industry *"
                                onChange={(e) => handleIndustryChange(e.target.value)}
                            >
                                <MenuItem value="">
                                    <em>Select Industry</em>
                                </MenuItem>
                                {industryOptions.map((industry) => (
                                    <MenuItem key={industry.industry_id} value={industry.industry_name}>
                                        {industry.industry_name}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>

                    <Grid item xs={12} sm={6}>
                        <FormControl fullWidth margin="normal">
                            <InputLabel>Sub-Industry</InputLabel>
                            <Select
                                value={newProject.sub_industry}
                                label="Sub-Industry"
                                onChange={(e) => handleInputChange('sub_industry', e.target.value)}
                                disabled={!newProject.industry}
                            >
                                <MenuItem value="">
                                    <em>Select Sub-Industry</em>
                                </MenuItem>
                                {subIndustryOptions.map((subIndustry) => (
                                    <MenuItem key={subIndustry} value={subIndustry}>
                                        {subIndustry}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>

                    <Grid item xs={12}>
                        <TextField
                            fullWidth
                            label="Project Location"
                            value={newProject.project_location}
                            onChange={(e) => handleInputChange('project_location', e.target.value)}
                            margin="normal"
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <TextField
                            fullWidth
                            multiline
                            rows={2}
                            label="Project Description"
                            value={newProject.project_description}
                            onChange={(e) => handleInputChange('project_description', e.target.value)}
                            margin="normal"
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <TextField
                            fullWidth
                            multiline
                            rows={4}
                            label="Task Assigned/Performed"
                            value={newProject.tasks_performed}
                            onChange={(e) => handleInputChange('tasks_performed', e.target.value)}
                            placeholder="Enter tasks, one per line"
                            margin="normal"
                            helperText="You can use bullet points or separate tasks with new lines"
                        />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="From Date *"
                            type="date"
                            InputLabelProps={{ shrink: true }}
                            value={newProject.job_start_date}
                            onChange={(e) => handleInputChange('job_start_date', e.target.value)}
                            margin="normal"
                        />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="To Date *"
                            type="date"
                            InputLabelProps={{ shrink: true }}
                            value={newProject.job_end_date}
                            onChange={(e) => handleInputChange('job_end_date', e.target.value)}
                            margin="normal"
                        />
                    </Grid>
                </Grid>
            </DialogContent>

            <DialogActions>
                <Button onClick={onClose} color="secondary">
                    Cancel
                </Button>
                <Button
                    onClick={handleSave}
                    variant="contained"
                    color="primary"
                    disabled={!isFormValid()}
                >
                    Add Project
                </Button>
            </DialogActions>
        </Dialog>
    );
};

export default AddProjectModal;