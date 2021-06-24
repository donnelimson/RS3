using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel.DocumentNumbering
{
    public class DocumenberNumberingToExcel
    {
        [DisplayName("TRANSACTION TYPE")]
        public string TransactionType { get; set; }
        [DisplayName("DEFAULT SERIES")]
        public string DefaultSeries { get; set; }
        [DisplayName("FIRST NO.")]
        public int? FirstNo { get; set; }
        [DisplayName("NEXT NO.")]
        public int? NextNo { get; set; }
        [DisplayName("LAST NO.")]
        public int? LastNo { get; set; }
        [DisplayName("LOCKED")]
        public bool IsLocked { get; set; }
        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }
        [DisplayName("ACTION DATE")]
        public DateTime? ActionDate { get; set; }
    }
}
