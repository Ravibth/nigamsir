using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SkillReviewCommentDTO
    {
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string comment { get; set; }
    }
}
