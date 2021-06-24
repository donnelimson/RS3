using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.Ordering
{
    public class OrderingLookUpViewModel
    {
        public int OrderingLookUpId { get; set; }
        public int OrderId { get; set; }
        public List<int> SupplierId { get; set; }
        public int SupplierIdForJson { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public int Indexer = 0;
        public string PurchaseOrderCode { get; set; }
        public List<Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem.OrderItem> Products { get; set; }

        public string OrderCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public string SupplierDescription { get; set; }
        public int PurchaseOrderId { get; set; }

        public List<int> MasterItemIds;
        public List<string> LongDescriptions;
        public List<double> UnitPrices;
        public List<int> Quantities;
        public IEnumerable<SelectListItem> SupplierCodeList { get; set; }
        public static int OrderIdForDb { get; set; }
        public static int SupplierIdForDb { get; set; }
        public static int PurchaseOrderIdForDb { get; set; }
    }
    public class OrdersFromAngular
    {
        public int SupplierId { get; set; }
        public int MasterItemId { get; set; }
        public string MasterItemDescription { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }

    }
}