using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class EmployeeWithLicenseNoDTO
    {
        public int EmployeeId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Office { get; set; }

        [DisplayName("EMPLOYEE NO.")]
        public string EmployeeNo { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("LICENSE NUMBER")]
        public string LicenseNo { get; set; }

        [DisplayName("EXPIRATION DATE")]
        public string ExpiDate { get; set; }

        [DisplayName("RESTRICTION/S")]
        public string Restriction { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }

        [DisplayName("ACTION ON")]
        public DateTime? ActionOn { get; set; }
    }
}
