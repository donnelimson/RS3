using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.MeterReadingRemarks
{
    public class MeterReadingRemarksDTO
    {
        public int MeterReadingRemarkId { get; set; }

        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("IS ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("ACTION BY")]
        public string CreatedBy { get; set; }

        [DisplayName("ACTION DATE")]
        public DateTime CreatedDate { get; set; }
    }
}
