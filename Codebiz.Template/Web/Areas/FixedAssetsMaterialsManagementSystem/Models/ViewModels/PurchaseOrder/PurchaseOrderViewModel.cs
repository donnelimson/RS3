using Codebiz.Domain.Common.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.PurchaseOrder
{
    public class PurchaseOrderViewModel
    {

        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierDescription { get; set; }
        public int MasterItemId { get; set; }
        public string MasterItemDescription { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public double TotalCost { get; set; }

        public class PurchaseOrderIndexModel
        {
           public IPagedList<Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem.PurchaseOrder> PurchaseOrders { get; set; }
           public int Page { get; set; }
            public int PageSize { get; set; }
            public string PurchaseOrderCode { get; set; }
        }
    }
}