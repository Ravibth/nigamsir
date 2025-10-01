import { IBuOfferingsMaster } from "./IBuOfferingsMaster";
import { IClient } from "./IClient";
import { ICompetencyMaster } from "./ICompetencyMaster";
import { IDesignations } from "./IDesignations";
import { IEmployeeModel, ILocation } from "./IEmployeeModel";
import { ISuperCoach } from "./ISuperCoach";

export interface ISupercochFiltersOptions {
    businessunit: IBuOfferingsMaster[],
    competency: ICompetencyMaster[],
    designations: IDesignations[],
    grade: IDesignations[],
    location: ILocation[],
    employees: ISuperCoach[];
}

