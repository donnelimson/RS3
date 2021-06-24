using Codebiz.Domain.Common.Model.DataModel.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class Log
    {
        public Log()
        {
            this.LogAttachments = new HashSet<LogAttachment>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(36)]
        public string ActivityId { get; set; }

        [MaxLength(100)]
        public string User { get; set; }

        [Required]
        [MaxLength(255)]
        public string Thread { get; set; }

        [Required]
        [MaxLength(50)]
        public string Level { get; set; }

        [Required]
        [MaxLength(255)]
        public string Logger { get; set; }

        [ForeignKey("LogEventTitle")]
        public int? LogEventTitleId { get; set; }
        public virtual LogEventTitle LogEventTitle { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Message { get; set; }

        [MaxLength(2000)]
        public string Exception { get; set; }
        public string Attachments { get; set; }
        public string FileUploadAttachments { get; set; }
        public string Changes { get; set; }

        public virtual ICollection<LogAttachment> LogAttachments{ get;set; }
    }
}
