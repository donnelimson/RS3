using System;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class ApplicationPhaseDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string PreviousPhase { get; set; }
        public string NewPhase { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string EndorseBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
    }
    public class MemberPhaseDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string PreviousPhase { get; set; }
        public string NewPhase { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string EndorseBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
    }

    public class VehiclePhaseDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string PreviousPhase { get; set; }
        public string NewPhase { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string EndorseBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
    }
}
