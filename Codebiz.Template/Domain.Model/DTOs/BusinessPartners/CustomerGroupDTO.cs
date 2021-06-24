using Codebiz.Domain.Common.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class CustomerGroupDTO
    {
        public int CustomerGroupId { get; set; }

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

}
