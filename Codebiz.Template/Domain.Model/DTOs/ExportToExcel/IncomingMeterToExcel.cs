using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class IncomingMeterToExcel
    {
        [DisplayName("JOB ORDER NO.")]
        public string JobOrderNo { get; set; }
        [DisplayName("ACCOUNT NO.")]
        public string AccountNo { get; set; }
        [DisplayName("BRAND")]
        public string Brand { get; set; }
        [DisplayName("SERIAL NO.")]
        public string SerialNo { get; set; }
        [DisplayName("STATUS")]
        public string Status { get; set; }
        [DisplayName("WITHDRAWN")]
        public bool IsWithdrawn { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("TESTED BY")]
        public string TestedBy { get; set; }
        [DisplayName("TESTED DATE")]
        public DateTime? TestedDate { get; set; }
    }
}
