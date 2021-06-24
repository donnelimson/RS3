using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class RequestDisconnectionToExcel
    {
        [DisplayName("CONTROL NUMBER")]
        public string ControlNo { get; set; }

        [DisplayName("ACCOUNT NUMBER")]
        public string AccountNo { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("CONSUMER TYPE")]
        public string ConsumerType { get; set; }

        [DisplayName("TEMPORARY")]
        public bool IsTemporary { get; set; }

        [DisplayName("PERMANENT")]
        public bool Permanent { get; set; }

        [DisplayName("REMARKS")]
        public string Remarks { get; set; }

        [DisplayName("STATUS")]
        public string Status { get; set; }
    }
}
