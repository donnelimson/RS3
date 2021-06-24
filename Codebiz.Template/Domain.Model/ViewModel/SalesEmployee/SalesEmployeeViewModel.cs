using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel
{
    public class SalesEmployeeViewModel
    {
        public int SalesEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public int? CommissionGroupId { get; set; }
        public double Commission { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public EmployeeDetails EmployeeDetails { get; set; }
    }

    public class EmployeeDetails
    {
        public int AppUserId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string AreaOffice { get; set; }
    }
}
