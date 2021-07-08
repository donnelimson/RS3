using Codebiz.Domain.Common.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.RS3
{
    public class TicketIndexDTO
    {
        [DisplayName("TICKET NO.")]
        public string TicketNo { get; set; }
        [DisplayName("PRIORITY")]
        public int? Priority { get; set; }
        [DisplayName("TITLE")]
        public string Title { get; set; }
        [DisplayName("CONCERN WHOM")]
        public string ClientName { get; set; }
        [DisplayName("CREATED")]
        public string Created { get; set; }
        [DisplayName("STATUS")]
        public string Status { get; set; }
        [DisplayName("LAST UPDATED")]
        public string LastUpdated { get; set; }
        [DisplayName("ASSIGNED TO")]
        public string AssignedTo { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int Id { get; set; }

    }
    public class TicketAddOrUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Priority { get; set; }
        public int? ClientId { get; set; }
        public string Client { get; set; }
        public string ClientEmail { get; set; }
        public string ClientAddress { get; set; }

        public int? TechnicianId { get; set; }
        public List<TicketAttachmentDTO> Attachments { get; set; } = new List<TicketAttachmentDTO>();
        public string LogComment { get; set; }
        public bool IsReply { get; set; }

        public int? UserIdReplied { get; set; } // user replid
    }
    public class TicketAttachmentDTO : AttachmentViewModel
    {
        public int TicketAttachmentId { get; set; }
    }
    public class ViewTicketDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Priority { get; set; }
        public int? ClientId { get; set; }
        public string Client { get; set; }
        public string ClientEmail { get; set; }
        public string ClientAddress { get; set; }
        public int? TechnicianId { get; set; }
        public List<string> Logs { get; set; }
        public string Technician { get; set; }
        public string TechnicianEmail { get; set; }
        public bool IsResolved { get; set; }
        public bool CanBeTaken { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
    public class CommentAddDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool IsResolved { get; set; } = false;
    }
    public class CommentDTO
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class TicketCFLDTO
    {
        public int Id { get; set; }
        public string TicketNo { get; set; }
        public string Title { get; set; }
        public string ClientName { get; set; }
        public string TechnicianName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
    }
}
