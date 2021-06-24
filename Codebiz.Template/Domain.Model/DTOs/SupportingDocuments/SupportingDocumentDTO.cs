using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class SupportingDocumentDTO
    {
        public int SupportingDocumentId { get; set; }

        [DisplayName("TRANSACTION TYPE")]
        public string TransactionType { get; set; }

        [DisplayName("TYPE")]
        public string Type { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime CreatedDate { get; set; }
    }
}
