using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Infrastructure.Constants
{
    //! TODO Check all routes and links again
    public static class NotificationTemplateLinksConstants
    {
        public const string Projects = "Projects";
        public const string ProjectListing = "ProjectListing";
        public const string ProjectDetails = "ProjectDetails";

        public const string ProjectCalendarView = "ProjectCalendarView";
        public const string ProjectBudgetDetails = "ProjectBudgetDetails";
        public const string ProjectActiveAllocation = "ProjectActiveAllocation";
        public const string ProjectActiveRequisition = "ProjectActiveRequisition";
        public const string ProjectMarketplaceInterests = "ProjectMarketplaceInterests";

        public const string Requisition = "Requisition";
        public const string CreateRequisition = "CreateRequisition";
        public const string ViewOrUpdateRequisition = "ViewOrUpdateRequisition";

        public const string MyProfile = "MyProfile";
        public const string Calendar = "Calendar";

        public const string Marketplace = "Marketplace";

        public const string RolePermission = "RolePermission";

        public const string Configurations = "Configurations";

        public const string MyApproval = "MyApproval";

        public const string BulkUpload = "BulkUpload";


        public static class RoutesPath
        {
            public const string ProjectListing = "/";
            public const string ProjectDetails = "/project-details/";
            public const string Calendar = "/calendar/";
            public const string CreateRequisition = "/create-requisition/";
            public const string ViewOrUpdateRequisition = "/update-requisition";
            public const string Marketplace = "/marketplace";
            public const string RolePermission = "/roles-permission";
            public const string Configurations = "/configurations";
            public const string MyApproval = "/myapproval";
            public const string BulkUpload = "/bulk-upload/";
            public const string Tab = "tab";

        }
    }
}
