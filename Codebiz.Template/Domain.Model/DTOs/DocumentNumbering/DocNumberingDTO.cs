using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.DocumentNumbering
{
    public class DocNumberingIndexDTO
    {
        public int DocumentNumberingId { get; set; }
        [DisplayName("TRANSACTION TYPE")]
        public string TransactionType { get; set; }
        [DisplayName("DEFAULT SERIES")]
        public string DefaultSeries { get; set; }
        [DisplayName("FIRST NO.")]
        public long? FirstNo { get; set; }
        [DisplayName("NEXT No.")]
        public long? NextNo { get; set; }
        [DisplayName("LAST NO.")]
        public long? LastNo { get; set; }
        [DisplayName("IS LOCKED")]
        public bool? IsLocked { get; set; }
        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }
        [DisplayName("ACTION DATE")]
        public DateTime? ActionDate { get; set; }
    }
}
