using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class ApprovalToExcel
    {
        [DisplayName("TRANSACTION TYPE")]
        public string TransactionType { get; set; }

        [DisplayName("TRANSACTION DATE")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("DETAILS")]
        public string Details { get; set; }

        [DisplayName("REMARKS")]
        public string Remarks { get; set; }

        [DisplayName("STATUS")]
        public string Status { get; set; }

        [DisplayName("ACTION ON")]
        public DateTime? ActionDate { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
