using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.DTO
{
    public class GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto
    {
        public string email_id { get; set; }
        public string uemail_id { get; set; }
        public string employee_mid { get; set; }
        public DateOnly last_available_day { get; set; }
    }
}
