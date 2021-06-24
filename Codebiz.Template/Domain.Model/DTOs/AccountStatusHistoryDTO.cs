using System;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class AccountStatusHistoryDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        public int? CurrentStatusId { get; set; }
        public string CurrentStatus { get; set; }
        public int? PreviousStatusId { get; set; }
        public string PreviousStatus { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}
