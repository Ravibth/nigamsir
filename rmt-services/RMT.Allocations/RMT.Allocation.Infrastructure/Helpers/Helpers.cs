using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Infrastructure.Helpers
{
    public class AllocationAndRequisitionStatusForWorkflow
    {
        public string AllocationStatus { get; set; }
        public string RequisitionStatus { get; set; }
        public bool IsAllocationDeleted { get; set; }
        public bool IsRequisitionDeleted { get; set; }
        public DateTime? AllocationConfirmedDate { get; set; }
    }
    public class Helpers
    {

        public static bool CheckToDeleteRequisition(string requisitionType)
        {
            //case of update allocation to be managed
            var sCase = requisitionType;
            switch (sCase)
            {
                case RequisitionTypeData.NamedAllocation:
                    return true;
                case RequisitionTypeData.SameTeamAllocation:
                    return true;
                case RequisitionTypeData.CreateRequisition:
                    return false;
                //rollForward remaining
                //updated remaining
                default:
                    break;
            }
            return false;
        }

        public static bool IsWorkflowTaskCreatedForSuperCoach(string workflowStatus)
        {
            var sCase = workflowStatus;
            switch (sCase)
            {
                case WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH:
                    return true;
                    
                case WorkflowStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION:
                    return true;
                    
                default:
                    break;
            }
            return false;
        }
        public static AllocationAndRequisitionStatusForWorkflow GetAllocationAndRequisitionStatusByWorkflowStatus(string workflowStatus, string requsitionType)
        {
            //update allocation check handling
            //reject 
            //success
            var sCase = workflowStatus;
            var response = new AllocationAndRequisitionStatusForWorkflow()
            {
                AllocationStatus = "",
                RequisitionStatus = "",
                IsAllocationDeleted = false,
                IsRequisitionDeleted = false,
                AllocationConfirmedDate = null
            };
            //string ALLOCATION_STATUS = "";
            //string REQUSITION_STATUS = "";
            //bool isAllocationDeleted = false;
            //bool isRequisitionDeleted = false;
            switch (sCase)
            {
                case WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER://DONE
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER;
                    response.RequisitionStatus = RequisitionStatuses.APPROVED;
                    response.AllocationConfirmedDate = DateTime.UtcNow;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH://DONE
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH;
                    response.RequisitionStatus = RequisitionStatuses.APPROVED;
                    response.AllocationConfirmedDate = DateTime.UtcNow;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER://DONE
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER://DONE
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER;
                    //response.IsRequisitionDeleted = CheckToDeleteRequisition(requsitionType);
                    response.RequisitionStatus = RequisitionStatuses.PENDING;
                    response.IsAllocationDeleted = true;
                    //workflow can end
                    //REQUSITION_STATUS = RequisitionStatuses.PENDING;//point-> what will happen in the case of update scenarios
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH:
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_SUPERCOACH:
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_SUPERCOACH;
                    response.RequisitionStatus = RequisitionStatuses.PENDING;
                    response.IsAllocationDeleted = true;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE://DONE
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE;
                    //REQUSITION_STATUS = RequisitionStatuses.PENDING_APPROVAL;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE://DONE
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE;
                    response.RequisitionStatus = RequisitionStatuses.APPROVED;
                    response.AllocationConfirmedDate = DateTime.UtcNow;
                    response.IsAllocationDeleted = false;
                    response.IsRequisitionDeleted = false;

                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR://DONE
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR;

                    //workflow can end
                    //REQUSITION_STATUS = unknown;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE:
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE;
                    //workflow can end
                    //REQUSITION_STATUS = unknown;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION:
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION;
                    response.IsRequisitionDeleted = CheckToDeleteRequisition(requsitionType);
                    response.RequisitionStatus = RequisitionStatuses.PENDING;
                    response.IsAllocationDeleted = true;
                    break;

                case WorkflowStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION:
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION;
                    //REQUSITION_STATUS = RequisitionStatuses.PENDING_APPROVAL;
                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION:
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION;
                    response.RequisitionStatus = RequisitionStatuses.APPROVED;
                    response.AllocationConfirmedDate = DateTime.UtcNow;
                    response.IsRequisitionDeleted = false;
                    response.IsAllocationDeleted = false;
                    break;
                case WorkflowStatus.Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION:
                    response.AllocationStatus = WorkflowStatus.Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION;
                    response.RequisitionStatus = RequisitionStatuses.PENDING;
                    response.IsRequisitionDeleted = CheckToDeleteRequisition(requsitionType);
                    response.RequisitionStatus = RequisitionStatuses.PENDING;
                    response.IsAllocationDeleted = true;
                    //REQUSITION_STATUS = RequisitionStatuses.PENDING;

                    break;
                case WorkflowStatus.EMPLOYEE_ALLOCATION_TERMINATION_BY_RR:
                    response.AllocationStatus = WorkflowStatus.EMPLOYEE_ALLOCATION_TERMINATION_BY_RR;
                    response.IsRequisitionDeleted = CheckToDeleteRequisition(requsitionType);
                    response.RequisitionStatus = RequisitionStatuses.PENDING;
                    response.IsAllocationDeleted = true;
                    //REQUSITION_STATUS = RequisitionStatuses.PENDING_APPROVAL;
                    break;
                default:
                    break;
            }
            return response;
        }
    }
}
