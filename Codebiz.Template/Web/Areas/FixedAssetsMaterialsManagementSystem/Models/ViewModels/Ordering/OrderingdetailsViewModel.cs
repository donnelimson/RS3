using Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.Ordering
{
    public class OrderingDetailsViewModel
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public static string GetOrderCode{ get; set; }
        public List<OrderItem> OrderLookUps { get; set; }
        public List<Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem.PurchaseOrder> PurchaseOrders { get; set; }
        public IPagedList<OrderHistory> OrderingHistories { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}