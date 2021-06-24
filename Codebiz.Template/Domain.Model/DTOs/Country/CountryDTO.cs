using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class CountryDTO
    {
        public int CountryId { get; set; }

        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }

        [DisplayName("ACTION ON")]
        public DateTime? ActionOn { get; set; }
    }
}
