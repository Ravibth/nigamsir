namespace RMT.Scheduler.DTOs
{
    public class Workflow
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? module { get; set; }
        public string? sub_module { get; set; }
        public string? item_id { get; set; }
        public string? outcome { get; set; }
        public string? status { get; set; }
        public string? created_by { get; set; }
        public string? created_at { get; set; }
        public string? updated_by { get; set; }
        public string? updated_at { get; set; }
        public bool? is_active { get; set; }
        public string? parent_id { get; set; }
    }
    public class WorkflowTaskRequest
    {
        public string? id { get; set; }
        public string? assigned_to { get; set; }
        public string? comment { get; set; }
        public string? created_at { get; set; }
        public string? created_by { get; set; }
        public string? description { get; set; }
        public string? due_date { get; set; }
        public string? proxy_approval_by { get; set; }
        public string? status { get; set; }
        public string? type { get; set; }
        public string? title { get; set; }
        public string? updated_at { get; set; }
        public string? updated_by { get; set; }
        public string? MyProperty { get; set; }
        public Workflow? workflow { get; set; }
        public string? workflow_id { get; set; }

    }
}


//@BelongsTo(() => WorkflowModel)
//  workflow: WorkflowModel;

//@ForeignKey(() => WorkflowModel)
//  @Column({ type: DataTypes.STRING, allowNull: false })
//  workflow_id: string;