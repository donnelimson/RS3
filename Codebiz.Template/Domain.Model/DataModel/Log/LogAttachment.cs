using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel.Log
{
    public class LogAttachment
    {
        [Key]
        public int LogAttachmentId { get; set; }

        [ForeignKey("Log")]

        public int LogId { get; set; }
        public virtual Model.Log Log { get; set; }
        public string FileName { get; set; }

        public string FileUrl { get; set; }
    }
}
