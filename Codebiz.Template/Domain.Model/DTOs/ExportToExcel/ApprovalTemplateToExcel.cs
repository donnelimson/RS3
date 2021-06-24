
using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class ApprovalTemplateToExcel
    {
        [DisplayName("NAME")]
        public string Name { get; set; }
        [DisplayName("ACTIVE")]
        public string Status { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("CREATED DATE")]
        public DateTime CreatedDate { get; set; }

    }
}
