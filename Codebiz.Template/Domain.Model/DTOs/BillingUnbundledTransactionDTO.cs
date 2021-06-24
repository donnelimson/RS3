using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class BillingUnbundledTransactionDTO
    {
        public int BillingUnbundledTransactionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }  
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
    public class BillingUnbundledTransactionLookUpDTO
    {
        public int Id { get; set; }
        public int BillingUnbundledTransactionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }
        public bool? Existing { get; set; }
    }
    public class BillingUnbundledTransactionIdAndNameDTO
    {
        public int? BillingUnbundledTransactionId { get; set; }
        public string CodeAndName { get; set; }
    }
    public class BillingUnbundledTransactionIdAndDisplayedNameDTO: BillingUnbundledTransactionIdAndNameDTO
    {
        public string DisplayedName { get; set; }
    }
    public class BillingUnbundledTransactionDetailsIdAndDisplayedNameDTO: BillingUnbundledTransactionIdAndDisplayedNameDTO
    {
        public int? BillingUnbundledTransactionDetailId { get; set; }
        public int? BillingUnbundledTransactionForVatId { get; set; }
        public int? BillingUnbundledTransactionForDiscountId { get; set; }
    }
    public class BillingUnbundledTransactionForBPDTO
    {
        public int BillingUnbundledTransactionId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string DebitCode { get; set; }
        public string DebitName { get; set; }
        public string CreditCode { get; set; }
        public string CreditName { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
    public class ValidateBillingTransactionDTO
    {
        public int BillingTransactionId { get; set; }
        public bool HasTransformer { get; set; }
        public int AccountId { get; set; }
        public string MeterReadingRemarks { get; set; }
        public decimal? PreviousReading { get; set; }
        public decimal? CurrentReading { get; set; }
        public decimal? Energy { get; set; }
        public decimal? Demand { get; set; }
        public string Remarks { get; set; }
        public bool IsPosted { get; set; }
        public ValidateBillingTransactionAccountDetails AccountDetails { get; set; }
    }
    public class ValidateBillingTransactionAccountDetails
    {
        public string MemberPhotoThumbnailUrl { get; set; }
        public string MemberName { get; set; }
        public string MemberNo { get; set; }
        public string AccountNo { get; set; }
        public string MeterSerialNo { get; set; }
        public string ConsumerType { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Route { get; set; }
        public string PoleNo { get; set; }
        public bool ForAccountDetails { get; set; } = true;
        public bool NotaMember { get; set; } = false;
        public bool IsICERA { get; set; }
        public bool IsGRAM { get; set; }
    }


}
