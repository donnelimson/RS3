using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class HWIToExcel
    {
        [DisplayName("ACCOUNT NO.")]
        public string AccountNo { get; set; }
        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("CONSUMER TYPE")]
        public string ConsumerType { get; set; }
        [DisplayName("TRANSACTION TYPE")]
        public string TransactionType { get; set; }

        [DisplayName("STATUS")]
        public string Status { get; set; }

        [DisplayName("REMARKS")]
        public string Remarks { get; set; }

        [DisplayName("ACTION ON")]
        public DateTime? ActionOn { get; set; }
    }
}
