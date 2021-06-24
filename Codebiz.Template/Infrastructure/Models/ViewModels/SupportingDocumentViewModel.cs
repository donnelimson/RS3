using System.Collections.Generic;

namespace Infrastructure.Models.ViewModels
{
    public class SupportingDocumentViewModel
    {
        public int SupportingDocumentId { get; set; }
        public int TransactionTypeId { get; set; }
        public int? TransactionSubTypeId { get; set; }
        public string TransactionTypeSubType { get; set; }
        public List<DocumentViewModel> Documents { get; set; }
    }

    public class DocumentViewModel
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public bool IsRequired { get; set; }
        public int? Group { get; set; }
    }
}
