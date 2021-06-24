using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class VendorGroupIndexDTO
    {
        public int VendorGroupId { get; set; }
        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }
        [DisplayName("ACTIVE")]

        public bool IsActive { get; set; }
        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }
        [DisplayName("ACTION DATE")]
        public DateTime ActionOn { get; set; }
    }
    public class VendorGroupDetailsDTO
    {
        public int VendorGroupId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
