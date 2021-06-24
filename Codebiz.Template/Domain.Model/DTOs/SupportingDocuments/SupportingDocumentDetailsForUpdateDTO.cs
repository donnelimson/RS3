using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class SupportingDocumentDetailsForUpdateDTO
    {
        public int TransactionTypeId { get; set; }
        public int? TransactionSubTypeId { get; set; }
        public List<DocumentDetailsDTO> Documents { get; set; }
    }

    public class DocumentDetailsDTO
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public bool IsRequired { get; set; }
        public int? Group { get; set; }
        public bool IsEditing { get; set; } = false;
    }
}
