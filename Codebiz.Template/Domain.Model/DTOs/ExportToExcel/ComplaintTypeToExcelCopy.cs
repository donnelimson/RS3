using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class ComplaintTypeToExcelCopy
    {
        [DisplayName("COMPLAINT TYPE")]
        public string ComplaintType { get; set; }
        [DisplayName("COMPLAINT SUB TYPES")]
        public string ComplaintSubTypes { get; set; }
        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
