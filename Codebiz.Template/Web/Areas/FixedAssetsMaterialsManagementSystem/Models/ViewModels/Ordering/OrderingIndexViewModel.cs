using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.Ordering
{
    public class OrderingIndexViewModel
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalCost { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public IPagedList<Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem.ReplenishmentOrder> Orders { get; set; }
    }
    public class ReportDraftViewer
    {
        public static int OrderId;
        public static int SupplierId;
        public static string PurchaseOrderCode;
       
    }
}