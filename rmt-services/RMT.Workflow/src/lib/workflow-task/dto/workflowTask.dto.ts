export class CreateWorkflowTaskDto {
  assigned_to?: string;
  assigned_to_json?: any;
  comment?: string;

  description?: string;

  created_by: string;
  updated_by: string;

  proxy_approval_by?: string;

  status: string;

  title?: string;

  type?: string; // user or role

  workflow_id?: string;
  //-----> NEW ADDED <------
  due_date?: Date;
}
