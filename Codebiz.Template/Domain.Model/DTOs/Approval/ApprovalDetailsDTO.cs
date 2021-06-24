using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.Approval
{
    public class ApprovalDetailsDTO
    {
        public int ApprovalId { get; set; }
        public int MemberAccountId { get; set; }
        public int MemberId { get; set; }
        public string AccountNo { get; set; }
        public string MemberNo { get; set; }
        public string MemberName { get; set; }
        public string ConsumerType { get; set; }
        public string MembershipType { get; set; }
        public string MembershipTypeSubcategory { get; set; }
        public string EmailAddress { get; set; }
        public string BillingAddress { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Mailing { get; set; }
        public string TIN { get; set; }
        public bool IsTemporary { get; set; }   
        public string OwnershipType { get; set; }
        public int? ConsumerTypeId { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime? ActionDate { get; set; }
  
    }
}
