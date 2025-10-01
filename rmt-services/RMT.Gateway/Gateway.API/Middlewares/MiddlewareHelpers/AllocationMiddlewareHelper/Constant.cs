namespace Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper
{
    public static class Constant
    {
        public static class RequisitionTypeData
        {
            public const string NamedAllocation = "Named Allocation";
            public const string SameTeamAllocation = "Same Team Allocation";
            public const string CreateRequisition = "Create Requisition";
            public const string RollForwardAllocation = "Roll Forward Allocation";
            public const string BulkAllocation = "Bulk Allocation";
            public const string BulkRequisition = "Bulk Requisition";
            public const int DefaultRequisitionTypeId = 3;
        }
    }
}
