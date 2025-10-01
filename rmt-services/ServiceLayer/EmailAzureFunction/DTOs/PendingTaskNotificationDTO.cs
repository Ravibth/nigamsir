using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class PendingTaskNotificationDTO
    {
        public string Email { get; set; }
        public List<AllocationPendingTaskNotificationDTO> Allocation {  get; set; } 
        public List<SkillPendingTaskNotificationDTO> Skills { get; set; } 
    }
    public class AllocationPendingTaskNotificationDTO
    {
        
        public string Title { get; set; }   
        public DateOnly? ReceivedDate { get; set; }
        public string ReceivedFrom { get; set; }
        public DateOnly? DueDate { get; set; }
        public int? DaysLeft {  get; set; }
        public string Status { get; set; }
        public string JobCode { get; set; }

    }
    public class SkillPendingTaskNotificationDTO
    {
        public string Title { get; set; }
        public string TaskID { get; set; }
        public DateOnly? ReceivedDate { get; set; }
        public int Age { get; set; }
        public string Status { get; set; }
        public string EmployeeName { get; set; }
        public string SkillName { get; set; }

        public string SkillLevel { get; set; }

    }
}
