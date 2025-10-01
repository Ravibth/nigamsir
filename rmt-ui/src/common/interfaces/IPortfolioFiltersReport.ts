import { IClient } from "./IClient";
import { IDesignations } from "./IDesignations";
import { IEmployeeModel } from "./IEmployeeModel";

export interface IPortfolioFiltersReport {
    employee_mid: string;
    name: string;
    designation: string;
    grade: string;
    supercoach?: string;
    supercoach_mid?: string;
    cosupercoach?: string;
    cosupercoach_mid?: string;
    officelocation: string;
    availablevsallocated: string;
    allocatedhours: number;
    netavailablehours: number;
    clientgroup: string;
    client: string;
    jobcode?: string;
    jobname?: string;
    workingDate: string; // use Date if you parse it as Date object
    week: string;
    month: string;
    WeeklyHours: Record<string, number>;
    Period?: string;        // "W1", "M4", etc.
    PeriodType?: string;    // "Weekly" or "Monthly"
    PeriodStart?: string;   // use Date if parsed (DateOnly in C# is typically serialized as string)
    PeriodEnd?: string;
  }
  
