using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class SupportingDocumentsDetailsDTO
    {
        public SupportingDocumentsDetailsDTO()
        {
            Documents = new List<DocumentsForDetails>();
        }

        public string TransactionType { get; set; }
        public string TransactionSubType { get; set; }
        public List<DocumentsForDetails> Documents { get; set; }
    }

    public class DocumentsForDetails
    {
        public string DocumentName { get; set; }
        public bool IsRequired { get; set; }
        public int? Group { get; set; }
    }
}
