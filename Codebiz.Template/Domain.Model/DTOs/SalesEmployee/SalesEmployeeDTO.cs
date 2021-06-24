using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class SalesEmployeeDTO
    {
        public int SalesEmployeeId { get; set; }

        public int EmployeeId { get; set; }

        [DisplayName("NAME")]
        public string SalesEmployee { get; set; }

        [DisplayName("POSITION")]
        public string Position { get; set; }

        [DisplayName("AREA OFFICE")]
        public string AreaOffice { get; set; }

        [DisplayName("COMMISSION GROUP")]
        public string CommissionGroup { get; set; }

        [DisplayName("COMMISSION")]
        public double Commission { get; set; }

        [DisplayName("REMARKS")]
        public string Remarks { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
