export interface IWorkflowTaskModelMaster {
  id?: string;
  assigned_to?: string;
  assigned_to_userName?: string;
  comment?: string;
  created_at?: Date;
  created_by?: string;
  description?: string;
  due_date?: Date | null;
  proxy_approval_by?: string;
  status?: string;
  title?: string;
  type?: string;
  updated_by?: string;
  workflow_id?: string;
  updated_at?: Date;
}

export interface IWorkflowModelMaster {
  id?: string;
  name?: string;
  module?: string;
  sub_module?: string;
  item_id?: string;
  outcome?: string;
  status?: string;
  created_by?: string;
  updated_by?: string;
  entity_type?: string;
  entity_meta_data?: any;
  is_active?: true;
  parent_id?: string;
  created_at?: Date;
  updated_at?: Date;
  task_list?: IWorkflowTaskModelMaster[];
}
