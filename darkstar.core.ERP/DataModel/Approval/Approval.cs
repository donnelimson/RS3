using Codebiz.Domain.Common.Model;
using Codebiz.Domain.ERP.Model.Data.ERP;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.ERP.Model
{
    public class Approval : ModelBase
    {
        public Approval()
        {
            ApprovalApproverStages = new HashSet<ApprovalApproverStage>();
        }

        [Key]
        public int ApprovalId { get; set; }
        public string ApprovalNo { get; set; }

        //[ForeignKey("Account")]
        //public int? AccountId { get; set; }
        //public virtual Account Account { get; set; }

        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }

        public int TransactionId { get; set; }
        public string TransactionCode { get; set; }

        public bool IsProccessed { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ApprovalStatus")]
        public int? ApprovalStatusId { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }

        public virtual ICollection<ApprovalApproverStage> ApprovalApproverStages { get; set; }

        //[ForeignKey("Member")]
        //public int? MemberId { get; set; }
        //public virtual Member Member { get; set; }

        //[ForeignKey("Account")]
        //public int? AccountId { get; set; }
        //public virtual Account Account { get; set; }

        //[ForeignKey("AccountTransaction")]
        //public int? AccountTransactionId { get; set; }
        //public virtual AccountTransaction AccountTransaction { get; set; }

        //[ForeignKey("JobOrder")]
        //public int? JobOrderId { get; set; }
        //public virtual JobOrder JobOrder { get; set; }

        //[ForeignKey("VehicleRequest")]
        //public int? VehicleRequestId { get; set; }
        //public virtual VehicleRequest VehicleRequest { get; set; }

        //[ForeignKey("InventoryTransfer")]
        //public int? InventoryTransferId { get; set; }
        //public virtual InventoryTransfer InventoryTransfer { get; set; }
        //[ForeignKey("InventoryReceiving")]
        //public int? InventoryReceivingId { get; set; }
        //public virtual InventoryReceiving InventoryReceiving { get; set; }
        //[ForeignKey("BillOfMaterials")]
        //public int? BillOfMaterialsId { get; set; }
        //public virtual BillOfMaterials BillOfMaterials { get; set; }
        //[ForeignKey("MeterWithdrawal")]
        //public int? MeterWithdrawalId { get; set; }
        //public virtual MeterWithdrawal MeterWithdrawal { get; set; }
        //[ForeignKey("MaterialRequest")]
        //public int? MaterialRequestId { get; set; }
        //public virtual MaterialRequest MaterialRequest { get; set; }

        //[ForeignKey("StockRequisitionVoucher")]
        //public int? StockRequisitionVoucherId { get; set; }
        //public virtual StockRequisitionVoucher StockRequisitionVoucher { get; set; }

        //[ForeignKey("PurchaseRequisitionVoucher")]
        //public int? PurchaseRequisitionVoucherId { get; set; }
        //public virtual PurchaseRequisitionVoucher PurchaseRequisitionVoucher { get; set; }
    }
}
