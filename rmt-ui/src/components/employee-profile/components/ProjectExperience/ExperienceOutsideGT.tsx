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
import { EmployeeProfile, ProjectExperienceOutsideGT } from '../../interfaces/employeeProfile';
import { EmpProfileEditButtonSx, NameConstants } from '../../constants';
import { useEffect, useState } from 'react';
import AddProjectModal from './AddProjectModal';
import { IconButton } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import SaveIcon from "@mui/icons-material/Check";
import CancelIcon from "@mui/icons-material/Close";
import { InfoRounded } from '@mui/icons-material';
import { ProjectExperienceProps } from './ProjectExperience';
import { getIndustryMasterFromWCGT } from '../../../../services/wcgt-master-services/wcgt-master-services';
import { IIndustryMasterList } from '../../../../common/interfaces/IIndustryMaster';
import { IIndustryMappingPreferenceOptions, IIndustryOptionList } from '../../../manage/employee-preferences/industry/interface';
import _ from "lodash";

const ExperienceOutsideGT = (props: ProjectExperienceProps) => {

    const { onSave, setTempData, ...mainProfileData } = props;

    const [modalOpen, setModalOpen] = useState(false);
    const [rowData, setRowData] = useState<ProjectExperienceOutsideGT[]>(mainProfileData.experience_outside_gt);

    const [editRows, setEditRows] = useState<Record<string, boolean>>({});
    const [industryMaster, setIndustryMaster] = useState<IIndustryMasterList>([]);
    const [options, setOptions] = useState<IIndustryMappingPreferenceOptions>(null);

    const onEditClick = (rowId: string) => {
        setEditRows((prev) => ({ ...prev, [rowId]: true }));
    };

    const onCancelClick = (rowId: string) => {
        setEditRows((prev) => ({ ...prev, [rowId]: false }));
        // optional: reset row data if needed
    };

    const onSaveClick = (rowId: string, api: any) => {
        const rowNode = api.getRowNode(rowId);
        // console.log("Saved row:", rowNode.data);
        setEditRows((prev) => ({ ...prev, [rowId]: false }));
        setTempData(prev => ({
            ...prev,
            experience_outside_gt: prev.experience_outside_gt.map(item =>
                item.id == rowNode.data.id ? { ...item, ...rowNode.data } : item
            )
        }));
    };

    const getSubIndustryOptions = (industryName: string) => {
        if (!industryName || !industryMaster.length) return [];

        const subIndustries = industryMaster
            .filter(item => item.industry_name === industryName)
            .map(item => item.sub_industry_name)
            .filter((subIndustry): subIndustry is string => !!subIndustry);

        return _.uniq(subIndustries);
    };

    const columnDefs = [
        {
            headerName: "",
            field: "editAction",
            sortable: false,
            resizable: false,
            editable: false,
            suppressMenu: true,
            suppressMovable: true,
            width: 100,
            pinned: "left" as const,
            cellRendererFramework: (params: any) => {
                const rowId = params.node.id;
                const isEditing = editRows[rowId];

                if (isEditing) {
                    return (
                        <>
                            <IconButton
                                size="small"
                                onClick={() => onSaveClick(rowId, params.api)}
                            >
                                <SaveIcon fontSize="small" />
                            </IconButton>
                            <IconButton size="small" onClick={() => onCancelClick(rowId)}>
                                <CancelIcon fontSize="small" />
                            </IconButton>
                        </>
                    );
                }

                return (
                    <IconButton size="small" onClick={() => onEditClick(rowId)}>
                        <EditIcon fontSize="small" />
                    </IconButton>
                );
            },
        },
        {
            headerName: "Project Name",
            field: "project_name",
            flex: 1,
            editable: (params: any) => editRows[params.node.id] === true,
        },
        {
            headerName: "Client Name",
            field: "client_name",
            flex: 1,
            editable: (params: any) => editRows[params.node.id] === true,
        },
        {
            headerName: "Industry",
            field: "industry",
            flex: 1,
            editable: (params: any) => editRows[params.node.id] === true,
            cellEditor: "agSelectCellEditor",
            cellEditorParams: {
                values: options?.industryOptions ? options.industryOptions.map(opt => opt.industry_name) : [],
            },
            onCellValueChanged: (params: any) => {
                if (params.data) {
                    params.data.sub_industry = "";
                    params.api.refreshCells({
                        rowNodes: [params.node],
                        columns: ["sub_industry"],
                    });
                }
            },
        },
        {
            headerName: "Sub-Industry",
            field: "sub_industry",
            flex: 1,
            editable: (params: any) => editRows[params.node.id] === true,
            cellEditor: "agSelectCellEditor",
            cellEditorParams: (params: any) => {
                const industryName = params.data.industry;
                const subIndustryOptions = getSubIndustryOptions(industryName);
                return {
                    values: subIndustryOptions,
                };
            },
        },
        {
            headerName: "Project Location",
            field: "project_location",
            flex: 1,
            editable: (params: any) => editRows[params.node.id] === true,
        },
        {
            headerName: "Project Description",
            field: "project_description",
            flex: 1,
            width: 180,
            editable: (params: any) => editRows[params.node.id] === true,
            cellEditor: "agLargeTextCellEditor",
            cellEditorPopup: true,
            tooltipValueGetter: (params) => {            
            return params.value && params.value.length > 20 ? params.value : null;}
        },
        {
            headerName: "Task Assigned/Performed",
            field: "tasks_performed",
            flex: 2,
            editable: (params: any) => editRows[params.node.id] === true,
            cellEditor: "agLargeTextCellEditor",
            cellEditorPopup: true,
            tooltipValueGetter: (params) => {            
            return params.value && params.value.length > 20 ? params.value : null;}
        },
        {
            headerName: "From",
            field: "job_start_date",
            flex: 1,
            editable: (params: any) => editRows[params.node.id] === true,
            cellEditor: "agDateCellEditor",
        },
        {
            headerName: "To",
            field: "job_end_date",
            flex: 1,
            editable: (params: any) => editRows[params.node.id] === true,
            cellEditor: "agDateCellEditor",
        },
    ];

    const defaultColDef = {
        sortable: true,
        resizable: true,
        editable: true,
        filter: true,
    };

    const handleAdd = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleSaveProject = (project: ProjectExperienceOutsideGT) => {
        //set project.
        setTempData(prev => ({
            ...prev,
            experience_outside_gt: [...prev.experience_outside_gt, project]
        }))
        setRowData(prevData => [...prevData, project]);
    };



    const GetIndustryMaster = async () => {
        try {
            return await getIndustryMasterFromWCGT();
        } catch (e) {
            throw e;
        }
    };

    useEffect(() => {
        Promise.all([GetIndustryMaster()]).then((response) => {
            const industryMaster: IIndustryMasterList = response[0];
            setIndustryMaster(industryMaster);
            const defaultOptions = GetDefaultValues(industryMaster);
            setOptions(defaultOptions);
        });
    }, []);

    const GetDefaultValues = (industryMaster: IIndustryMasterList) => {
        const industryOption: IIndustryOptionList = industryMaster.map((e) => {
            return {
                industry_id: e.industry_id,
                industry_name: e.industry_name,
            };
        });
        const uniqIndustryOptions: IIndustryOptionList = _.uniqWith(
            industryOption,
            (a, b) => a.industry_id === b.industry_id
        ).filter((a) => a && a.industry_id && a.industry_name);
        const defaultOptions: IIndustryMappingPreferenceOptions = {
            industryOptions: uniqIndustryOptions,
            subIndustryOptions: [],
        };
        return defaultOptions;
    };

    return (
        <Card sx={{ mb: 2 }} className='box-radius'>
            <CardContent>
                <Box display="flex" justifyContent="space-between" alignItems={'center'} >
                    <Box display="flex" alignItems={'center'} >
                        <Typography variant="h6">Key Projects</Typography>
                        <Tooltip title={NameConstants.ExperienceOutsideGT}>
                            <IconButton><InfoRounded /></IconButton>
                        </Tooltip>
                    </Box>
                    <Button
                        onClick={handleAdd}
                        variant="outlined"
                        sx={EmpProfileEditButtonSx}
                    >
                        Add
                    </Button>
                </Box>

                <Box mt={1} className="ag-theme-alpine" style={{ height: 200, width: '100%' }}>
                    <AgGridReact
                        rowData={rowData}
                        columnDefs={columnDefs}
                        getRowStyle={() => ({ background: 'transparent' })}
                        defaultColDef={defaultColDef}
                        overlayNoRowsTemplate="<span style='padding: 10px; color: #666;'>Add your prior experience or key projects to complete your profile.</span>"
                    />
                </Box>

                <AddProjectModal
                    open={modalOpen}
                    onClose={handleCloseModal}
                    onSave={handleSaveProject}
                    industryOptions={options?.industryOptions ? options?.industryOptions : []}
                    industryMaster={industryMaster}
                />
            </CardContent>
        </Card>
    );
};

export default ExperienceOutsideGT;