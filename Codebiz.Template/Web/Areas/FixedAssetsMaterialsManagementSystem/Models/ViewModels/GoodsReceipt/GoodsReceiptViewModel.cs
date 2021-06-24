using Codebiz.Domain.Common.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.GoodsReceipt
{
    public class GoodsReceiptViewModel
    {
        public class GoodsReceiptIndexModel
        {
            public IPagedList<Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem.GoodsReceipt> GoodsReceipts { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public string GoodsReceiptCode { get; set; }
            public string AppUserFullName { get; set; }
            public DateTime DateCreated { get; set; }
        }
        public class GoodsReceiptCreateModel
        {
            public int GoodsReceiptId { get; set; }
            public string GoodsReceiptCode { get; set; }
            public int PurchaseOrderCode { get;set; }
            public int TotalQuantity { get; set; }
            public double TotalCost { get; set; }
            public string Status { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime ModifiedOn { get; set; }
            public int CreatedByUserAppId { get; set; }
            public int ModifedByUserAppId { get; set; }
        }
        public class GoodsReceiptSavedOrders
        {
            public int GoodsReceiptId { get; set; }
            public int MasterItemId { get; set; }
            public int Quantity { get; set; }
            public double Cost { get; set; }
            public double TotalCost {get;set;}
            public int SupplierId { get; set; }
        }
        public class GoodsReceiptEditModel
        {
            public static int GoodsReceiptIdForDb { get; set; }
            public int GoodsReceiptId { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }
            public List<string> MasterItemDescription { get; set; }
            public DateTime CreatedOn { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedOn { get; set; }
            public string PurchaseOrderCode { get; set; }
            public List<Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem.GoodsReceiptItem> Products { get; set; }
            public int SupplierIdForJson { get; set; }
            public string SupplierDescription { get; set; }
            public IEnumerable<SelectListItem> SupplierCodeList { get; set; }
        }
        public class GoodsReceiptDatatoReceiveFromJson
        {
            public int GoodsReceiptId { get; set; }
            public int MasterItemId { get; set; }
            public string MasterItemDescription { get; set; }
            public int Quantity { get; set; }
            public double Cost { get; set; }
            public double TotalCost { get; set; }
            public int SupplierId { get; set; }
        }
        public class GoodsReceiptManualCreateModel
        {
            public static string GoodsReceiptCodeForDb { get; set; }
            public static int GoodsReceiptIdForDb { get; set; }
            public SelectList SupplierList { get; set; }
            public int SupplierId { get; set; }
            public int GoodsReceiptId { get; set; }
            public string GoodsReceiptCode { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string Status { get; set; }
        }
    }
    
}