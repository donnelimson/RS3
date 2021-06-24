using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.Ordering
{
    public class MegaQuery
    {

        public static string QueryForPurchaseOrder(int orderId,int suppierId)
        {
           return "SELECT ROW_NUMBER() OVER(ORDER By OI.ItemDescription) As LineNum, OI.SupplierId, OI.ItemDescription, MI.ItemCode, CAST(FORMAT(OI.Cost,'N') AS varchar(20)) as " +
            "UnitCost, OI.Quantity, CAST(FORMAT(OI.TotalCost,'N') as varchar(20)) as TotalUnitCost, RO.OrderCode, RO.ModifiedOn as " +
            "BusinessDate, CONCAT(AU.FirstName,' ', AU.LastName) as CompletedBy, CONCAT(S.SupplierCode,'-',S.SupplierDescription) as SupplierField, RO.OrderCode from OrderItem OI INNER JOIN MasterItem MI on OI.MasterItemId=MI.MasterItemId INNER JOIN " +
            "ReplenishmentOrder RO on RO.OrderId= OI.OrderId INNER JOIN AppUser AU on RO.ModifiedByAppUserId= AU.AppUserId INNER JOIN Supplier S on S.SupplierId= OI.SupplierId where " +
            "OI.OrderId="+ orderId + " and OI.SupplierId= "+ suppierId + " GROUP BY OI.ItemDescription, OI.SupplierId,OI.Quantity, OI.TotalCost, MI.ItemCode, OI.Cost, RO.OrderCode, RO.ModifiedOn, AU.FirstName, RO.TotalQuantity, RO.TotalCost, " +
            "Au.LastName, S.SupplierCode, S.SupplierDescription";
    }
       public static string QueryForGoodsReceipt (int goodsReceiptId)
        {
            return "SELECT ROW_NUMBER() OVER(ORDER By GR.ItemDescription) as LineNum, MI.ItemCode as MasterItemCode,MI.LongDescription as MasterItemDescription, CAST(FORMAT(GR.Cost,'N') as varchar(20)) as UnitCost, GR.Quantity as UnitQuantity, CAST(FORMAT(GR.TotalCost,'N') as varchar(20)) as UnitTotalCost, CONCAT(SU.SupplierCode,'-',SU.SupplierDescription) as Supplier "+
                "from GoodsReceiptItem "+
                "GR INNER JOIN MasterItem MI on GR.MasterItemId=MI.MasterItemId INNER JOIN Supplier SU " +
            "on SU.SupplierId = GR.SupplierId Where GoodsReceiptId = " + goodsReceiptId + "";
        }

    }
}