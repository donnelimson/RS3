using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class AuditLogsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ActivityId { get; set; }
        public string User { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string LogEventTitle { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Attachments { get; set; }

        public string FileUploadAttachments { get; set; }
        public string Changes { get; set; }


    }
}
