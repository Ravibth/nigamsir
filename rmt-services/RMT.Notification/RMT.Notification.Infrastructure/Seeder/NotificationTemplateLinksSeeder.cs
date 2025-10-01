using RMT.Notification.Domain.Entities;
using RMT.Notification.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Infrastructure.Seeder
{
    public static class NotificationTemplateLinksSeeder
    {
        public static List<NotificationTemplateLinks> NotificationTemplateLinksSeederData = new List<NotificationTemplateLinks>()
        {
            // 1.  Project Listing Page
            new()
            {
                Id = 1,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.ProjectListing,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.ProjectListing}"
            },
            // 2.  Project Details Page
            new()
            {
                Id = 2,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.ProjectDetails,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.ProjectDetails}<{NotificationTemplatePayloads.ProjectCode}>"
            },
            // 3.  Project Calendar View
            new()
            {
                Id = 3,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.ProjectCalendarView,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.ProjectDetails}<{NotificationTemplatePayloads.ProjectCode}>?{NotificationTemplateLinksConstants.RoutesPath.Tab}=1"
            },
            // 4.  Project Budget Details
            new()
            {
                Id = 4,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.ProjectBudgetDetails,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.ProjectDetails}<{NotificationTemplatePayloads.ProjectCode}>?{NotificationTemplateLinksConstants.RoutesPath.Tab}=2"
            },
            // 5.  Project Active Allocations
            new()
            {
                Id = 5,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.ProjectActiveAllocation,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.ProjectDetails}<{NotificationTemplatePayloads.ProjectCode}>?{NotificationTemplateLinksConstants.RoutesPath.Tab}=3"
            },
            // 6.  Project Active Requisition
            new()
            {
                Id = 6,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.ProjectActiveRequisition,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.ProjectDetails}<{NotificationTemplatePayloads.ProjectCode}>?{NotificationTemplateLinksConstants.RoutesPath.Tab}=4"
            },
            // 7.  Project Marketplace Interests
            new()
            {
                Id = 7,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.ProjectMarketplaceInterests,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.ProjectDetails}<{NotificationTemplatePayloads.ProjectCode}>?{NotificationTemplateLinksConstants.RoutesPath.Tab}=5"
            },
            // 8.  Project Create Requisition
            new()
            {
                Id = 8,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.CreateRequisition,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.CreateRequisition}<{NotificationTemplatePayloads.ProjectCode}>"
            },
            // 9.  Project View/Update Requisition
            new()
            {
                Id = 9,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = NotificationTemplateLinksConstants.ViewOrUpdateRequisition,
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.ViewOrUpdateRequisition}<{NotificationTemplatePayloads.ProjectCode}>/<{NotificationTemplatePayloads.RequisitionId}>"
            },
            // 10. RolePermission
            new()
            {
                Id = 10,
                Module = NotificationTemplateLinksConstants.RolePermission,
                SubModule = "",
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.RolePermission}"
            },
            // 11. Configurations
            new()
            {
                Id = 11,
                Module = NotificationTemplateLinksConstants.Configurations,
                SubModule = "",
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.Configurations}"
            },
            // 12. BulkUpload
            new()
            {
                Id = 12,
                Module = NotificationTemplateLinksConstants.Projects,
                SubModule = "",
                Link = $"{NotificationTemplateLinksConstants.RoutesPath.BulkUpload}<{NotificationTemplatePayloads.ProjectCode}>"
            },
        };
    }
}
