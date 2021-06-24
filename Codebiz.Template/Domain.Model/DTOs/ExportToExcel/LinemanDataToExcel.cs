using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class LinemanDataToExcel
    {
        [DisplayName("ACCOUNT NO")]
        public string AccountNo { get; set; }

        [DisplayName("NAME")]
        public string FullName { get; set; }

        [DisplayName("CONSUMER TYPE")]
        public string ConsumerType { get; set; }

        [DisplayName("REMARKS")]
        public string Remarks { get; set; }

        [DisplayName("ISSUED BY")]
        public string IssuedBy { get; set; }

        [DisplayName("ISSUED DATE")]
        public DateTime? ConnectionOrderIssuedOn { get; set; }
    }
}
