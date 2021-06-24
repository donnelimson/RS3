using Codebiz.Domain.Common.Model;
using Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Controllers
{
    public class AccountsPayableController : FixedAssetsMaterialsManagementSystemControllerBaseController
    {
        private readonly IAppUserServices _appUserservice;
        private readonly IGoodsReceiptServices _goodsReceiptServices;
        private readonly IGoodsReceiptItemServices _goodsReceiptItemServices;
        private readonly IAccountsPayableServices _accountsPayableServices;
        private readonly IUnitOfWork _unitOfWork;
        public AccountsPayableController(
            IAppUserServices appUserServices,
            IGoodsReceiptServices goodsReceiptServices,
            IGoodsReceiptItemServices goodsReceiptItemServices,
            IAccountsPayableServices accountsPayableServices,
            IUnitOfWork unitOfWork
            

            ) : base(appUserServices) {
            _appUserservice = appUserServices;
            _goodsReceiptItemServices = goodsReceiptItemServices;
            _goodsReceiptServices = goodsReceiptServices;
            _accountsPayableServices = accountsPayableServices;
            _unitOfWork = unitOfWork;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public ActionResult Index(string accountsPayableCode="",int page=1,int pageSize=10)
        {
         
            return View();
        }
        public JsonResult Search(DateTime? dateFrom, DateTime? dateTo,int page,int pageSize,string sortColumn,string sortOrder, string accountsPayableCode="",string goodsReceiptCode="", string createdBy="",string status="")
        {
            var filter = new DataSearch
            {
                Page = page,
                PageSize = pageSize,
                SortColumn = string.IsNullOrEmpty(sortColumn) ? "SupplierId" : sortColumn.Replace(" ", string.Empty),
                SortDirection = sortOrder
            };
            var getAllData = _accountsPayableServices.Search(filter,accountsPayableCode, status, goodsReceiptCode, createdBy,dateFrom, dateTo);
            return Json(new
            {
                data=getAllData.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        public JsonResult ImportFromGoodsReceipt (int goodsReceiptId, int totalQuantity, double totalCost, string goodsReceiptCode)
        {
            bool alreadySent = false;
            int? goodsReceiptIdForDb = _accountsPayableServices.IsGoodsReceiptExists(goodsReceiptId);
            if (goodsReceiptIdForDb == null || goodsReceiptIdForDb == 0)
            {
                var createData = new AccountsPayable();
                createData.AccountsPayableCode = RandomString(10);
                createData.CreatedByAppUserId = CurrentUser.AppUserId;
                createData.GoodsReceiptId = goodsReceiptId;
                createData.SupplierId = _goodsReceiptItemServices.GetSupplierIdByGoodsReceiptId(goodsReceiptId);
                createData.CreatedOn = DateTime.Now;
                createData.TotalQuantity = totalQuantity;
                createData.TotalCost = totalCost;
                createData.Status = "OPEN";
                _accountsPayableServices.InsertOrUpdated(createData);
                _unitOfWork.SaveChanges();
                alreadySent = false;
            }
            else
            {
                alreadySent = true;
            }
            return Json(new
            {
                data = alreadySent
            }, JsonRequestBehavior.AllowGet);
        }
    }
}