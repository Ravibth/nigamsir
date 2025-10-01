using RMT.Configuration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Configuration.Domain.Seeder
{
    public static class ConfigurationMasterSeeder
    {
        public static List<ConfigurationMaster> ConfigurationMasterSeederData = new()
        {
            //1.Create Resource Requisition 
            new()
            {
                Id = new Guid("f9d28250-c3fd-4309-84d4-6fb3204a7283"),
                ConfigGroup = ConfigurationGroupMasterEnum.REQUISITION_FORM_DB_GROUP,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.REQUISITION_FORM,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.REQUISITION_FORM_PARAMETERS,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.Location,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.Location,
                        Description = ConfigMasterKeyDisplayLabel.Location,
                        ValidationRegEx = ValidationRegex.ZeroToTen
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Same_client,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.Same_client,
                        Description = ConfigMasterKeyDisplayLabel.Same_client,
                        ValidationRegEx = ValidationRegex.ZeroToTen
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Competency,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.Competency,
                        Description = ConfigMasterKeyDisplayLabel.Competency,
                        ValidationRegEx = ValidationRegex.ZeroToTen
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Offerings,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.Offerings,
                        Description = ConfigMasterKeyDisplayLabel.Offerings,
                        ValidationRegEx = ValidationRegex.ZeroToNine
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Solutions,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.Solutions,
                        Description = ConfigMasterKeyDisplayLabel.Solutions,
                        ValidationRegEx = ValidationRegex.ZeroToNine
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Industry,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.Industry,
                        Description = ConfigMasterKeyDisplayLabel.Industry,
                        ValidationRegEx = ValidationRegex.ZeroToTen
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Sub_Industry,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.Sub_Industry,
                        Description = ConfigMasterKeyDisplayLabel.Sub_Industry,
                        ValidationRegEx = ValidationRegex.ZeroToTen
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Skills,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.Skills,
                        Description = ConfigMasterKeyDisplayLabel.Skills,
                        ValidationRegEx = ValidationRegex.ZeroToNine
                    }

                })),
            },
            //2.System Suggestions for a Resource Requisition :  Employee suggestions whose match% is equal or more than this range shall only be provided.
            new()
            {
                Id = new Guid("6e2f6490-d03a-4ade-854e-fb7e1ad43cc5"),
                ConfigGroup = ConfigurationGroupMasterEnum.SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH_DB_GROUP,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.MATCH_RANGE_FOR_SYSTEM_SUGGESTIONS_FOR_A_REQUISITION,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH_DB_GROUP,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigMasterKeyDisplayLabel.System_Suggestion_For_Requisition_Percentage_Match,
                        Description = ConfigMasterKeyDisplayLabel.System_Suggestion_For_Requisition_Percentage_Match,
                        ValidationRegEx = ValidationRegex.OneToHundred
                    }
                }))
            },
            //3.Reviewer Configuration for number of days for reviewer to take action over the workflow WIP
            new()
            {
                Id = new Guid("bbf7f6e5-bd62-4e2c-9e82-c5e2313c23ba"),
                ConfigGroup = ConfigurationGroupMasterEnum.RESOURCE_ALLOCATION_DB_GROUP,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.RESOURCE_ALLOCATION_GROUP,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.RESOURCE_ALLOCATION_REVIEW,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.RESOURCE_ALLOCATION_DB_GROUP,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.RESOURCE_ALLOCATION_GROUP,
                        Description = ConfigurationGroupMasterDisplayEnum.RESOURCE_ALLOCATION_GROUP,
                        ValidationRegEx = ValidationRegex.MinusOneOrOneToFour
                    }
                }))
            },
            //4.Threshold number of days within which the employee can accept/ reject their allocation in a project. (User can enter number of days b/w 1-4) 
            new()
            {
                Id = new Guid("4c0b7855-ce34-4fe1-87bc-8b1b5fa3c30d"),
                ConfigGroup = ConfigurationGroupMasterEnum.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION,
                        Description = ConfigurationGroupMasterDisplayEnum.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION,
                        ValidationRegEx = ValidationRegex.MinusOneOrOneToFour
                    }
                }))
            },
            //5."Employee Allocation Rejection review &  Number of days for User  to take action on employee rejection of allocation.(Value of -1 indicates process is disabled. In order to enable the process,  user can enter number of days b/w  1-4)  )"
            new()
            {
                Id = new Guid("d4b5458e-87e7-4dda-a54d-be427dc4d8d0"),
                ConfigGroup = ConfigurationGroupMasterEnum.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR,
                        Description = ConfigurationGroupMasterDisplayEnum.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR,
                        ValidationRegEx = ValidationRegex.MinusOneOrOneToFour
                    }
                }))
            },
            //6.Maximum number of parameters that can be selected by Employee in Preference screen (Dropdown : 1-5)
            new()
            {
                Id = new Guid("bd59fd6d-fc57-4002-91c9-1f13f87a2cd1"),
                ConfigGroup = ConfigurationGroupMasterEnum.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = false,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Global,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN,
                        Description = ConfigurationGroupMasterDisplayEnum.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN,
                        ValidationRegEx = ValidationRegex.OneToFive
                    }
                }))
            },
            //7."Amber condition for Project Budget if Allocation cost reaches X% of Budgeted Cost. (X to be between 1-100)"
            new()
            {
                Id = new Guid("2f185e77-16d0-4151-ad38-0f7ddd93a22f"),
                ConfigGroup = ConfigurationGroupMasterEnum.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION,
                        Description = ConfigurationGroupMasterDisplayEnum.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION,
                        ValidationRegEx = ValidationRegex.OneToHundred
                    }
                }))
            },
            //8."Alert condition for Allocation cost reaches X% of Budgeted Cost. (X to be between 1-100)"
            new()
            {
                Id = new Guid("0748bb53-c8bb-4c03-8d9c-ba9318758aea"),
                ConfigGroup = ConfigurationGroupMasterEnum.ALERT_CONDITION_FOR_ALLOCATION_COST_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.ALERT_CONDITION_FOR_ALLOCATION_COST,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.ALERT_CONDITION_FOR_ALLOCATION_COST,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.ALERT_CONDITION_FOR_ALLOCATION_COST_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.ALERT_CONDITION_FOR_ALLOCATION_COST,
                        Description = ConfigurationGroupMasterDisplayEnum.ALERT_CONDITION_FOR_ALLOCATION_COST,
                        ValidationRegEx = ValidationRegex.OneToHundred
                    }
                }))
            },
            //9."Alert if Timesheet Hours>= X% of Allocation Hours(X to be between 1-100)"
            new()
            {
                Id = new Guid("9999ce5f-c6aa-404e-aeda-1334a69641d9"),
                ConfigGroup = ConfigurationGroupMasterEnum.ALERT_CONDITION_FOR_TIMESHEET_HOURS_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.ALERT_CONDITION_FOR_TIMESHEET_HOURS,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.ALERT_CONDITION_FOR_TIMESHEET_HOURS,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.ALERT_CONDITION_FOR_TIMESHEET_HOURS_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.ALERT_CONDITION_FOR_TIMESHEET_HOURS,
                        Description = ConfigurationGroupMasterDisplayEnum.ALERT_CONDITION_FOR_TIMESHEET_HOURS,
                        ValidationRegEx = ValidationRegex.OneToHundred
                    }
                }))
            },
            //10."The Additional EL can View below items basis config settings Requisitions,Allocations View interests in Marketplace,Employee Calendar with details of any allocations/ requisitions made by the Resource Requestor (EL & EO) & Delegate, Other Additional EL's & Other Additional Delegate"
            new()
            {
                Id = new Guid("40b4d939-d7e4-4376-9c05-2023dacbec67"),
                ConfigGroup = ConfigurationGroupMasterEnum.PERMISSION_FOR_ADDITIONAL_EL_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.PERMISSION_FOR_ADDITIONAL_EL,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.ALERT_CONDITION_FOR_ADDITIONAL_EL,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = false,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Global,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.PERMISSION_FOR_ADDITIONAL_EL_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.PERMISSION_FOR_ADDITIONAL_EL,
                        Description = ConfigurationGroupMasterDisplayEnum.PERMISSION_FOR_ADDITIONAL_EL,
                        ValidationRegEx = ValidationRegex.MinusOneOrOne
                    }
                }))
            },
            //11."The Additional Delegate can View below items basis config settings Requisitions, Allocations View interests in Marketplace, Employee Calendar with details of any allocations/ requisitions made by the Resource Requestor (EL & EO) & Delegate, Other Additional EL's & Other Additional Delegate"
            new()
            {
                Id = new Guid("0672de0b-6dee-4d11-a608-df9cbc75fd3c"),
                ConfigGroup = ConfigurationGroupMasterEnum.PERMISSION_FOR_ADDITIONAL_DELEGATE_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.PERMISSION_FOR_ADDITIONAL_DELEGATE,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.ALERT_CONDITION_FOR_ADDITIONAL_DELEGATE,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = false,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Global,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.PERMISSION_FOR_ADDITIONAL_DELEGATE_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.PERMISSION_FOR_ADDITIONAL_DELEGATE,
                        Description = ConfigurationGroupMasterDisplayEnum.PERMISSION_FOR_ADDITIONAL_DELEGATE,
                        ValidationRegEx = ValidationRegex.MinusOneOrOne
                    }
                }))
            },
            //12.Number for days to action Skill Assessment Requests. (Value range : 1-5)
            new()
            {
                Id = new Guid("e8534542-87df-48fe-8b75-ac0afc8244d9"),
                ConfigGroup = ConfigurationGroupMasterEnum.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE_GROUP_BY,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = false,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Global,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE_GROUP_BY,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE,
                        Description = ConfigurationGroupMasterDisplayEnum.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE,
                        ValidationRegEx = ValidationRegex.OneToFive
                    }
                }))
            },
            //13.Reviewer Configuration for number of days for reviewer to take action over the workflow WIP
            new()
            {
                Id = new Guid("69ed0225-64db-445a-bfef-bd739835eb87"),
                ConfigGroup = ConfigurationGroupMasterEnum.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                ConfigGroupDisplay = ConfigurationGroupMasterDisplayEnum.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                ConfigNote = ConfigurationGroupMasterConfigNoteEnum.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                GlobalDefaultDisplay = true,
                SelectorWiseDisplay = true,
                Description = "",
                SelectorConfigType = ConfigurationGroupMasterSelectorConfigType.Offerings,
                schema = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMasterSchema>()
                {
                    new()
                    {
                        Key = ConfigurationGroupMasterEnum.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                        ControlType = ConfigurationMasterControlType.Integer,
                        KeyDisplay = ConfigurationGroupMasterDisplayEnum.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                        Description = ConfigurationGroupMasterDisplayEnum.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                        ValidationRegEx = ValidationRegex.MinusOneOrOneToFour
                    }
                }))
            },
        };

        public static List<ConfigurationMainBreakup> ConfigurationMainBreakupsSeederData = new()
        {
            new()
            {
                Id =  new Guid("31db74b1-6a80-4164-ae02-d3863aab976f"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.REQUISITION_FORM_DB_GROUP).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.Location,
                        DisplayKey = ConfigMasterKeyDisplayLabel.Location,
                        Value = "8"
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Same_client,
                        DisplayKey = ConfigMasterKeyDisplayLabel.Same_client,
                        Value = "8"
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Competency,
                        DisplayKey = ConfigMasterKeyDisplayLabel.Competency,
                        Value = "8"
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Offerings,
                        DisplayKey = ConfigMasterKeyDisplayLabel.Offerings,
                        Value = "8"
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Solutions,
                        DisplayKey = ConfigMasterKeyDisplayLabel.Solutions,
                        Value = "8"
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Industry,
                        DisplayKey = ConfigMasterKeyDisplayLabel.Industry,
                        Value = "8"
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Sub_Industry,
                        DisplayKey = ConfigMasterKeyDisplayLabel.Sub_Industry,
                        Value = "8"
                    },
                    new()
                    {
                        Key = ConfigurationParametersEnum.Skills,
                        DisplayKey = ConfigMasterKeyDisplayLabel.Skills,
                        Value = "8"
                    },
                }))
            },
            new()
            {
                Id =  new Guid("b3d0f14d-fb5e-44e9-9abf-1617c2d6a565"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH_DB_GROUP).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.System_Suggestion_For_Requisition_Percentage_Match,
                        DisplayKey = ConfigMasterKeyDisplayLabel.System_Suggestion_For_Requisition_Percentage_Match,
                        Value = "60"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("868dbe58-2086-48b9-bb6b-ec70d01178a4"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.RESOURCE_ALLOCATION_DB_GROUP).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.RESOURCE_ALLOCATION_DB_GROUP,
                        DisplayKey = ConfigMasterKeyDisplayLabel.RESOURCE_ALLOCATION_GROUP,
                        Value = "1"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("c2dc3ca6-80a2-4123-9910-e2cb1aeec180"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                        DisplayKey = ConfigMasterKeyDisplayLabel.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                        Value = "1"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("38770e66-cc61-4ddb-8f39-a2c5bfbe2000"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION,
                        Value = "1"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("5bca1b3f-29a1-4b98-8f66-ebfa5c668df5"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR,
                        Value = "1"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("d0989935-537b-4499-ab34-4ae4f02adfa5"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN,
                        Value = "5"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("303ac0a5-b366-4258-8bfb-ea342e38d2ba"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.AMBER_CONDITION_FOR_BUDGET_CONSUMPTION,
                        Value = "80"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("850f9b34-307a-4c28-a737-882d505afd36"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.ALERT_CONDITION_FOR_ALLOCATION_COST_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.ALERT_CONDITION_FOR_ALLOCATION_COST_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.ALERT_CONDITION_FOR_ALLOCATION_COST,
                        Value = "90"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("e3238e31-8ac2-4ead-aef4-5c39ff07d7b6"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.ALERT_CONDITION_FOR_TIMESHEET_HOURS_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.ALERT_CONDITION_FOR_TIMESHEET_HOURS_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.ALERT_CONDITION_FOR_TIMESHEET_HOURS,
                        Value = "90"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("7713e0f5-7259-4610-bf94-97db59b49fa8"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.PERMISSION_FOR_ADDITIONAL_EL_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.PERMISSION_FOR_ADDITIONAL_EL_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.PERMISSION_FOR_ADDITIONAL_EL,
                        Value = "-1"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("b5357e78-ca69-4dc2-a69a-cbaf2952cb47"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.PERMISSION_FOR_ADDITIONAL_DELEGATE_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.PERMISSION_FOR_ADDITIONAL_DELEGATE_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.PERMISSION_FOR_ADDITIONAL_DELEGATE,
                        Value = "-1"
                    }
                }))
            },
            new()
            {
                Id =  new Guid("2137d048-0e08-46d9-b86b-05c15f11613f"),
                ConfigurationMasterId = ConfigurationMasterSeederData.FirstOrDefault(m => m.ConfigGroup == ConfigurationGroupMasterEnum.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE_GROUP_BY).Id,
                KeySelector = ConfigMasterKeyDisplayLabel.Default_Key_Selector,
                CreatedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                ModifiedAt = new DateTime(2024, 11, 9, 18, 33, 58, 292, DateTimeKind.Utc).AddTicks(6320),
                CreatedBy = "system",
                ModifiedBy = "system",
                MetaValue = JsonDocument.Parse(JsonSerializer.Serialize(new List<ConfigurationMainBreakupMetaValue>()
                {
                    new()
                    {
                        Key = ConfigurationParametersEnum.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE_GROUP_BY,
                        DisplayKey = ConfigMasterKeyDisplayLabel.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE,
                        Value = "3"
                    }
                }))
            }

        };
    }
}
