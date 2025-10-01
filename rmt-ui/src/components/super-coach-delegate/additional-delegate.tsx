import {
    Autocomplete,
    Grid,
    TextField,
    Typography,
    debounce,
} from "@mui/material";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import React, { useContext, useEffect, useMemo, useState } from "react";
import { GetAllUsersExcludingEmployeesWithRolesInProject } from "../../services/project-list-services/project-list-services";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import { getEmailId } from "../../global/utils";
import { IEmployeeModel } from "../../common/interfaces/IEmployeeModel";
import { DelegateType } from "./constant";
import { getListIOfAllUsers } from "./util";


export interface DelegateViewData extends Partial<IEmployeeModel> {
    allocationDelegateName?: string;
    allocationDelegateEmail?: string;
    skillDelegateEmail?: string;
    skillDelegateName?: string;
    employee_mid?: string;
    employee_id?: string;
    email_id?: string;
    name?: string;
}

const AdditionalDelegate = (props) => {
    const {
        additionalDelegateGridValue,
        rowData,
        setRowData,
        rowIndex,
        gridUpdateTrigger,
        isDisabled,
        delegateType,
        params,
        currentSuperCoachMid 
    } = props;
    const [options, setOptions] = useState<readonly DelegateViewData[]>([]);
    const [value, setValue] = useState<DelegateViewData | null>(null);
    const [inputValue, setInputValue] = useState("");
    const { isEmployee } = useContext(UserDetailsContext);
    const SelectEventChangeHandler = (
        event: React.SyntheticEvent,
        newValue: DelegateViewData | null
    ) => {
        setValue(newValue);

        setRowData(prevData => {
        const newData = [...prevData];
        if (delegateType === DelegateType.Allocation) {
            newData[rowIndex] = {
                ...newData[rowIndex], // Preserve existing data
                allocationDelegateEmail: newValue?.email_id || "",
                allocationDelegateName: newValue?.name || "",
                allocation_delegate_mid:newValue?.employee_id|| "",
            };
        } else {
            newData[rowIndex] = {
                ...newData[rowIndex], // Preserve existing data
                skillDelegateEmail: newValue?.email_id || "",
                skillDelegateName: newValue?.name || "",
                skill_delegate_mid:newValue?.employee_id|| "",
            };
        }
        return newData;
        });
        
        gridUpdateTrigger();
    };  

    const DLChangeEvent = (event: React.SyntheticEvent, newValue: string) => {
        setInputValue(newValue);
    };

    const fetch = useMemo(() =>
        debounce(async (
            request: {
                input: string;
            },
            callback: (results?: readonly DelegateViewData[]) => void
        ) => {
            try {
                const result = await GetEmployeesList(
                    request.input,
                );
                callback(result);
            } catch (error) {
                console.error("Error fetching employees:", error);
                callback([]);
            }
        }, 400),
        []
    );

    const GetEmployeesList = async (
        inputEmail: string,
    ): Promise<DelegateViewData[]> => {
        try {            
            const employees = await getListIOfAllUsers();
            const seenEmails = new Set();            
            const allEmployees = employees.filter(employee => {                
                if (employee.email_id && !seenEmails.has(employee.email_id)) {                    
                    seenEmails.add(employee.email_id);
                    if(employee.employee_id === currentSuperCoachMid) return false;
                    return true;
                }
                return false;
            });

            // Filter employees based on input and exclusion list
            const filteredEmployees = allEmployees.filter(employee => {
                const matchesSearch = inputEmail
                    ? employee.name?.toLowerCase().includes(inputEmail.toLowerCase()) ||
                    employee.email_id?.toLowerCase().includes(inputEmail.toLowerCase())
                    : true;
                return matchesSearch;
            });

            // Map to the expected format
            return filteredEmployees.map(employee => ({
                employee_mid: employee.employee_mid,
                emailId: employee.email_id,
                name: employee.name,
                email_id: employee.email_id,
                ...employee
            }));

        } catch (error) {
            console.error("Error getting employee list:", error);
            return [];
        }
    };

    useEffect(() => {
        let active = true;

        if (inputValue === "" ||
            value?.allocationDelegateEmail?.toLowerCase().trim() === inputValue?.toLowerCase().trim()) {
            setOptions(value?.allocationDelegateName ? [value] : []);
            return;
        }

        if (inputValue) {
            fetch(
                {
                    input: inputValue,
                },
                (results) => {
                    if (active && results) {
                        setOptions(results);
                    }
                }
            );
        }

        return () => {
            active = false;
        };
    }, [inputValue, fetch]);


    useEffect(() => {
        if (additionalDelegateGridValue?.length > 0) {
        const dbData = additionalDelegateGridValue[0].data;
        const delegateViewData: DelegateViewData = {
            employee_mid: dbData.employee_mid || '',
            name: delegateType === DelegateType.Allocation 
                ? dbData.allocationDelegateName 
                : dbData.skillDelegateName,
            email_id: delegateType === DelegateType.Allocation 
                ? dbData.allocationDelegateEmail 
                : dbData.skillDelegateEmail,
        };
        setValue(delegateViewData);
        }
    }, [additionalDelegateGridValue, delegateType]);

    return (
        <div>
            {isEmployee ? (
                <TextField
                    value={
                        delegateType === DelegateType.Allocation
                            ? params?.data?.allocationDelegateName || ''
                            : params?.data?.skillDelegateName || ''
                    }
                    disabled
                    fullWidth
                    size="small"
                />
            ) : (
                <Autocomplete
                    autoComplete
                    size="small"
                    noOptionsText="No Match Found"
                    getOptionLabel={(option) =>
                        typeof option === 'string'
                            ? option
                            : option?.name || ''
                    }
                    getOptionKey={(option) =>
                        typeof option === 'string' ? option :
                            option?.employee_mid || option?.email_id || option?.name || ''
                    }
                    includeInputInList
                    filterOptions={(x) => x}
                    disabled={isDisabled || false}
                    options={options}
                    value={value || null}
                    onChange={(e, newValue) => SelectEventChangeHandler(e, newValue)}
                    onInputChange={(e, newValue) => DLChangeEvent(e, newValue)}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            fullWidth
                            placeholder="Search employee..."
                        />
                    )}
                    renderOption={(props, option: DelegateViewData) => (
                        <li {...props} key={option?.email_id || option?.name || ''}>
                            <Grid container alignItems="center">
                                <Grid item sx={{ width: "calc(100% - 44px)", wordWrap: "break-word" }}>
                                    <div className="search-employee-option">
                                        <span className="search-employee-name">
                                            {option?.name || ''} - {option?.email_id ? getEmailId(option.email_id) : ''}
                                        </span>
                                    </div>
                                </Grid>
                            </Grid>
                        </li>
                    )}
                />
            )}
        </div>
    );
};

export default AdditionalDelegate;