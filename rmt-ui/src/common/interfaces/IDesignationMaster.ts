export interface IDesignationMaster {
  createdat?: Date;
  modifiedat?: Date;
  createdby?: string;
  modifiedby?: string;
  designation_id?: string;
  designation_name?: string;
  grade?: string | null;
  description?: string;
  isactive?: boolean;
}
