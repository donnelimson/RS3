using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class ApprovalStageToExcel
    {
        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("DESCRIPTION")]
        public string Description { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("REQUIRED APPROVALS")]
        public int RequiredApprovals { get; set; }

        [DisplayName("REQUIRED REJECTIONS")]
        public int RequiredRejections { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
