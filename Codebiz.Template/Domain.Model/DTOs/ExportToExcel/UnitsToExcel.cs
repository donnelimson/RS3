using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class UnitsToExcel
    {
        [DisplayName("UNIT")]
        public string Unit { get; set; }

        [DisplayName("AREA OFFICE")]
        public string AreaOffice { get; set; }

        [DisplayName("METER READER")]
        public string MeterReader { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime CreatedDate { get; set; }
    }
}
