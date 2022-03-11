using Codebiz.Domain.Common.Model;
using Codebiz.Domain.ERP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.DataModel
{
    public class SaleTransaction:ModelBase
    {
        [Key]
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public decimal? TotalCost { get; set; } = 0;
        public decimal? TotalDiscount { get; set; } = 0;
        public decimal? TotalVat { get; set; } = 0;
        public string PriceType { get; set; } //(B)rand, (P)ricelist, (B)ase (P)rice
        public string ModeOfPayment { get; set; } //(C)ash, (T)ransfer
        public decimal? TotalCashTender { get; set; } = 0;
        public decimal? TotalTransferTender { get; set; } = 0;
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerEmail { get; set; }
        public string Ref1 { get; set; } //remarks
        public string Ref2 { get; set; } //reference no for payment
        public string Ref3 { get; set; } //internal remarks
    }
    public class SaleTransactionDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("SaleTransaction")]
        public int? SaleTransactionId { get; set; }
        public SaleTransaction SaleTransaction { get; set; }
        [ForeignKey("ItemMaster")]
        public int? ItemMasterId { get; set; }
        public ItemMaster ItemMaster { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public decimal? ItemCost { get; set; } = 0;
        public decimal? Discount { get; set; } = 0;
        public decimal? Vat { get; set; } = 0;
        public string BrandName { get; set; }
        public string PriceListName { get; set; }
    }
    
}
