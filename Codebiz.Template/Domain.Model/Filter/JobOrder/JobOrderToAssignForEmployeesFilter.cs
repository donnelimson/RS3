using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class JobOrderToAssignForEmployeesFilter : FilterBase
    {
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string ActionBy { get; set; }
        public string JobOrderNo { get; set; }

        public DateTime? ActionOnFrom { get; set; }
        public DateTime? ActionOnTo { get; set; }

        public string Type { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }

        public int CurrentUserId { get; set; }
        public int? CurrentEmployeeId { get; set; }
        public int? CurrentUserOfficeId { get; set; }
        public int? CurrentUserDivisionId { get; set; }
        public int? CurrentUserDepartmentId { get; set; }
        public int? CurrentUserDivisionCategoryId { get; set; }
    }
}
