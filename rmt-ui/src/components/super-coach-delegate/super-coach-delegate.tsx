import { useContext, useEffect, useState } from "react";
import TabularChart from "../Reports/Charts/Tabular-Chart/tabular-chart-data";
import AdditionalDelegate from "./additional-delegate";
import { getSuperCoachList, saveSuperCoachDelegate } from "./util";
import "./style.css";
import Grid from "@mui/material/Grid";
import SuperCoachFilter from "./filter/supercoach-filter-comp";
import { useForm } from "react-hook-form";
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Cancel';
import { DelegateType } from "./constant";
import SectionHeader from "../manage/section-header/section-header";
import { LoaderContext, LoaderContextProps } from "../../contexts/loaderContext";
import SplitPaneVerticalCollapsable from "./splitPane";
import { SnackbarContext } from "../../contexts/snackbarContext";

interface IRowTabularData {    
    sc_name: string;
    designation_name: string;
    business_unit: string;
    competency: string;
    location: string;
    skill: string;
    actions: string;
    email_id:string;
    allocationDelegateEmail: string | null;
    allocationDelegateName: string | null;
    allocation_delegate_mid:string;
    skillDelegateEmail: string | null;
    skillDelegateName: string | null;    
    skill_delegate_mid: string | null;
    supercoach_mid: string;        
    isEditing?: boolean; // Editing state
    originalData?: Partial<IRowTabularData>; // Store original data for reset
}

export interface ISelectedFilterOption {
    businessunit: string[] | null;
    competency: string[] | null;
    designation: string[] | null;
    grade: string[] | null;
    location: string[] | null;

}
const SuperCoachDelegate = () => {    
    const [isUserSystemAdmin] = useState(false);    
    const [rowTabularData, setRowTabularData] = useState<IRowTabularData[]>([]);
    const [isFilterApplied, setIsFilterApplied] = useState(false);    
    const loaderContext: LoaderContextProps = useContext(LoaderContext);
    const [filiterOptions, setFiliterOptions] =useState([]);
    const [isFilterShow, setIsFilterShow] = useState(true)
    const [submittedFilterData, setSubmittedFilterData] = useState<any>({});
    const snackbarContext: any = useContext(SnackbarContext);

     const {
        control,
        getValues,
        setValue,
        handleSubmit,
        watch,
        setError,
        formState: { errors, isDirty },
      } = useForm();
    
    
    const [colDef, setColDef] = useState<any[]>([
                {
            headerName: "Actions",
            headerTooltip: "Actions",
            field: "actions",
            width: 100,
            suppressMenu: true,
            suppressMovable: true,
            cellRenderer: function (params: any) {
                return (
                    <div className="actions-container">
                        {params.data.isEditing ? (
                            <>
                                <button
                                    className="icon-btn icon-btn--small icon-btn--primary"
                                    onClick={() => handleEditSave(params.rowIndex)}
                                >
                                    <SaveIcon fontSize="small" />
                                </button>
                                <button
                                    className="icon-btn icon-btn--small icon-btn--default"
                                    onClick={() => handleReset(params.rowIndex)}
                                >
                                    <CancelIcon fontSize="small" />
                                </button>
                            </>
                        ) : (
                            <button
                                className="icon-btn icon-btn--small icon-btn--default"
                                onClick={() => handleEditSave(params.rowIndex)}
                            >
                                <EditIcon fontSize="small" />
                            </button>
                        )}
                    </div>
                );
            }
        },
        {
            "headerName": "SuperCoach",
            "headerTooltip": "SuperCoach",
            "field": "sc_name",
            
            flex: 1,
            "filter": "agTextColumnFilter",
            "filterParams": {
                "suppressAndOrCondition": true
            },
            "sortable": true,
            "unSortIcon": true,
            "menuTabs": [
                "filterMenuTab"
            ],
            "tooltipField": "employee_name"
        },
        {
            "headerName": "Designation",
            "headerTooltip": "Designation",
            "field": "designation_name",
            "filter": "agTextColumnFilter",
            
            flex: 1,
            "filterParams": {
                "suppressAndOrCondition": true
            },
            "sortable": true,
            "unSortIcon": true,
            "menuTabs": [
                "filterMenuTab"
            ],
            "tooltipField": ""
        },
        {
            "headerName": "Business Unit",
            "headerTooltip": "Business Unit",
            "field": "business_unit",
            
            flex: 1,
            "filter": "agTextColumnFilter",
            "filterParams": {
                "suppressAndOrCondition": true
            },
            "sortable": true,
            "unSortIcon": true,
            "menuTabs": [
                "filterMenuTab"
            ],
            "tooltipField": ""
        },
        {
            "headerName": "Competency",
            "headerTooltip": "Competency",            
            flex: 1,
            "field": "competency",
            "filter": "agTextColumnFilter",
            "filterParams": {
                "suppressAndOrCondition": true
            },
            "sortable": true,
            "unSortIcon": true,
            "menuTabs": [
                "filterMenuTab"
            ],
            "tooltipField": ""
        },
        {
            "headerName": "Location",
            "headerTooltip": "Location",
            "field": "location",
            
            flex: 1,
            "filter": "agTextColumnFilter",
            "filterParams": {
                "suppressAndOrCondition": true
            },
            "sortable": true,
            "unSortIcon": true,
            "menuTabs": [
                "filterMenuTab"
            ],
            "tooltipField": ""
        },
        {
            headerName: "Allocation",
            headerTooltip: "Allocation Delegate",
            field: "allocationDelegateEmail",
            filter: "agTextColumnFilter",
            flex: 1,
            filterParams: { suppressAndOrCondition: true },
            cellRenderer: function (params: any) {
                if (params.data.isEditing) {
                    return (
                        <AdditionalDelegate
                            additionalDelegateGridValue={[params]}
                            rowData={rowTabularData}
                            setRowData={setRowTabularData}
                            rowIndex={params.rowIndex}
                            gridUpdateTrigger={gridUpdateTrigger}
                            isDisabled={checkAdditionalDelegateDisability(params)}
                            delegateType={DelegateType.Allocation}
                            currentSuperCoachMid={params.data.supercoach_mid}
                        />
                    );
                }
                return <div>{params.data.allocationDelegateName || params.value}</div>;
            },
        },
        {
            headerName: "Skill",
            headerTooltip: "Skill Delegate",
            field: "skillDelegateEmail",
            flex: 1,
            filter: "agTextColumnFilter",
            filterParams: { suppressAndOrCondition: true },
            cellRenderer: function (params: any) {
                if (params.data.isEditing) {
                    return (
                        <AdditionalDelegate
                            additionalDelegateGridValue={[params]}
                            rowData={rowTabularData}
                            setRowData={setRowTabularData}
                            rowIndex={params.rowIndex}
                            gridUpdateTrigger={gridUpdateTrigger}
                            isDisabled={checkAdditionalDelegateDisability(params)}
                            delegateType={DelegateType.Skill}
                            currentSuperCoachMid={params.data.supercoach_mid}
                        />
                    );
                }
                return <div>{params.data.skillDelegateName || params.value}</div>;
            },
        },
    ]);

    function gridUpdateTrigger() {
        setRowTabularData(prevData => [...prevData]); // Creates a new array reference
    }
    
    function checkAdditionalDelegateDisability(params: any) {
        return false;
    }
    
    // Function to handle reset
    const handleReset = (rowIndex: number) => {
        setRowTabularData(prevData => {
            const newData = [...prevData];
            const row = newData[rowIndex];
            
            if (row.originalData) {
                // Reset to original values
                newData[rowIndex] = {
                    ...row,
                    ...row.originalData,
                    isEditing: false,
                    originalData: undefined
                };
            }
            return newData;
        });
    };

    // Function to save delegate data
const saveDelegateData = async (rowData: IRowTabularData) => {        
    loaderContext.open(true);
    try {
        const payload = {
            supercoach_mid: rowData.supercoach_mid,
            supercoach_email: rowData.email_id,
            allocation_delegate_mid: rowData.allocation_delegate_mid || null,
            allocation_delegate_email: rowData.allocationDelegateEmail || null,
            allocation_delegate_name: rowData.allocationDelegateName || null,
            skill_delegate_mid: rowData.skill_delegate_mid || null,
            skill_delegate_email: rowData.skillDelegateEmail || null,
            skill_delegate_name: rowData.skillDelegateName || null
        };

        let response = await saveSuperCoachDelegate(payload);
        if (response.status === 201) {
            snackbarContext.displaySnackbar(
                "Super Coach Delegates Updated Successfully.",
                "success",
                6000
            );
            // Update the row's editing state
            setRowTabularData(prevData => 
                prevData.map(item => 
                    item.supercoach_mid === rowData.supercoach_mid 
                        ? { ...item, isEditing: false, originalData: undefined } 
                        : item
                )
            );
        }
        else
            snackbarContext.displaySnackbar("Error", "error", 6000);
        return false;
    } catch (error) {
        console.error("Error saving delegate data:", error);
        return false;
    } finally {            
        loaderContext.open(false);
    }
};

    // Function to handle edit/save action
const handleEditSave = (rowIndex: number) => {
    setRowTabularData(prevData => {
        const newData = [...prevData];
        const row = newData[rowIndex];

        if (row.isEditing) {
            // If currently editing, attempt to save
            saveDelegateData(row);
        } else {
            // If not editing, enable editing mode and store original data
            newData[rowIndex] = {
                ...row,
                isEditing: true,
                originalData: {
                    allocationDelegateEmail: row.allocationDelegateEmail,
                    allocationDelegateName: row.allocationDelegateName,
                    allocation_delegate_mid: row.allocation_delegate_mid,
                    skillDelegateEmail: row.skillDelegateEmail,
                    skillDelegateName: row.skillDelegateName,
                    skill_delegate_mid: row.skill_delegate_mid
                }
            };
        }
        return newData;
    });
};

    const fetchData = async (selectedOptios?: ISelectedFilterOption) => {
        try {
            const superCoachList = await getSuperCoachList(selectedOptios);
            const modifiedSuperCoach: IRowTabularData[] = superCoachList.filter(x=>x.supercoach_mid && x.name).map(coach => ({
                supercoach_mid: coach?.supercoach_mid,
                sc_name: coach?.name,
                designation_name: coach?.designation,
                business_unit: coach?.business_unit,
                competency: coach?.competency,
                location: coach?.location,
                skill: coach?.grade,
                actions: "Actions",
                email_id: coach?.email_id,
                allocationDelegateEmail: coach?.allocation_delegate_email,
                allocationDelegateName: coach?.allocation_delegate_name,
                allocation_delegate_mid: coach?.allocation_delegate_mid,
                skillDelegateEmail: coach?.skill_delegate_email,
                skillDelegateName: coach?.skill_delegate_name,
                skill_delegate_mid: coach?.skill_delegate_mid
            }));
            setRowTabularData(modifiedSuperCoach);
        } catch (error) {
            console.error("Error fetching and transforming super coach data:", error);
        }
    };

    useEffect(() => { 
       // fetchData();
    }, []);

    const selectedDataByFilter = (selectedOptios: ISelectedFilterOption)=>{
     
        fetchData(selectedOptios);
    }
   

    const handleResetClick = () =>{
        setRowTabularData([])        
    }
    
    return (
        <>
            <div className="container">
                <h2 className="user-management-heading">Super Coach Delegate</h2>
                <div className="splitter">
                    <SplitPaneVerticalCollapsable collapsed={!isFilterShow}>
                        <div className="simulationDiv">
                            <SuperCoachFilter
                                selectedDataByFilter={selectedDataByFilter}
                                handleResetClick={handleResetClick}
                                isShowFilter={isFilterShow}
                                showFilterOptions={() => setIsFilterShow(true)}
                                onCloseFliterOption={() => setIsFilterShow(false)}

                            />
                        </div>
                        <div>
                            {isUserSystemAdmin}
                            <TabularChart
                                rowData={rowTabularData}
                                isFilterApplied={isFilterApplied}
                                setisFilterApplied={setIsFilterApplied}
                                setRowData={setRowTabularData}
                                colDef={colDef}
                                height={"86vh"}
                                setColDef={setColDef}
                            />
                        </div>
                    </SplitPaneVerticalCollapsable>
                </div>
            </div>
        </>
    );
};

export default SuperCoachDelegate;