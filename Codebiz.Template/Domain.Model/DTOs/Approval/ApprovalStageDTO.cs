using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.DTO;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class ApprovalStageDTO
    {
        public int ApprovalStageId { get; set; }
        [DisplayName("NAME")]
        public string Name { get; set; }
        [DisplayName("DESCRIPTION")]
        public string Description { get; set; }
    
        [DisplayName("REQUIRED APPROVALS")]
        public int RequiredApprovals { get; set; }
        [DisplayName("REQUIRED REJECTIONS")]
        public int RequiredRejections { get; set; }
        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("CREATED DATE")]
        public DateTime CreatedDate { get; set; }
        public bool CanDelete { get; set; }
    }

    public class ApprovalStageLookUpDTO
    {
        public int Id { get; set; }
        public int ApprovalStageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovals { get; set; }
    
    }
    public class AllAprovalStageLookUpDTO: ApprovalStageLookUpDTO
    {
    }
    public class ApprovalStageDetailsDTO
    {
        public int ApprovalStageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovals { get; set; }
        public int RequiredRejections { get; set; }
        public int LabelId { get; set; }
        public string Label { get; set; }
        public ICollection<SearchAppUserDTO> Approvers { get; set; }
    }

}
