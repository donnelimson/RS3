using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel
{
    public class Ticket:ModelBase
    {
        public Ticket()
        {
            this.Attachments = new HashSet<TicketAttachment>();
            this.Logs = new HashSet<TicketLog>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(14)]
        public string TicketNo { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int? Priority { get; set; } //1,2,3,4 so forth
        public bool IsParent { get; set; }
        public bool IsChild { get; set; }
        [ForeignKey("AppUserClient")]
        public int? ClientId { get; set; }
        public virtual AppUser AppUserClient { get; set; }
        [ForeignKey("AppUserTechnician")]
        public int? TechnicianId { get; set; }
        public virtual AppUser AppUserTechnician { get; set; }
        [MaxLength(1)]
        public string TicketStatus { get; set; } // [R]esolved, [O]pen, [P]ark
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string GuessClientName { get; set; }
        [MaxLength(100)]
        public string GuessClientEmail { get; set; }
        [MaxLength(200)]
        public string GuessClientAddress { get; set; }

        public virtual ICollection<TicketAttachment> Attachments { get; set; }
        public virtual ICollection<TicketLog> Logs { get; set; }
    }
    public class Subticket:ModelBase
    {
        [Key]
        public int SubticketId { get; set; }
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
    public class TicketComment : ModelBase
    {
        [Key]
        public int TicketCommentId { get; set; }
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        [MaxLength(500)]
        public string Comment { get; set; }
    }
    public class TicketAttachment:ModelBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ContentFile")]
        public int ContentFileId { get; set; }
        public virtual ContentFile ContentFile { get; set; }
        [ForeignKey("Ticket")]
        public int? TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
    public class TicketLog:ModelBase
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public string Message { get; set; }
    }
}
