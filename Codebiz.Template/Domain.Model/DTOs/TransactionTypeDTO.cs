namespace Codebiz.Domain.Common.Model.DTOs
{
    public class TransactionTypeFilterDTO
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public string Description { get; set; }
        public bool IsUsedInSupportingDocuments { get; set; }
        public bool HasSeparateRequestInformation { get; set; }
    }
}
