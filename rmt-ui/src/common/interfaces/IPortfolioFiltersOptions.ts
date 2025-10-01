import { IClient } from "./IClient";
import { IDesignations } from "./IDesignations";
import { IEmployeeModel, ILocation } from "./IEmployeeModel";

export interface IPortfolioFiltersOptions {
    supercoaches: IEmployeeModel[],
    cosupercoaches: IEmployeeModel[],
    employees: IEmployeeModel[],
    designations: IDesignations[],
    clients: IClient[],
    locations: ILocation[],
} 