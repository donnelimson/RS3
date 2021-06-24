using Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.Ordering;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Reporting.WebForms;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.GoodsReceipt;
using Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.Ordering;
using Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.PurchaseOrder;


namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Controllers
{
    public class GoodsReceiptController : FixedAssetsMaterialsManagementSystemControllerBaseController
    {
        string fileNameExtension;
        byte[] renderedByte;
        private readonly IAppUserServices _appUserservice;
        private readonly IMasterItemServices _masterItemServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderServices _orderingService;
        private readonly IOrderItemServices _orderingLookUpService;
        private readonly IGoodsReceiptServices _goodsReceiptService;
        private readonly IGoodsReceiptItemServices _goodsReceiptItemService;
        private readonly ISupplierService _supplierService;
        private readonly IPurchaseOrderServices _purchaseOrderService;
        private readonly IConfigSettingService _configSettingsService;
        public GoodsReceiptController(IAppUserServices appUserService,
        IUnitOfWork unitOfWork,
        IMasterItemServices masterItemService,
        IOrderServices orderingService,
        IOrderItemServices orderingLookUpService,
        ISupplierService supplierService,
        IMasterItemSupplierService masterItemSupplierService,
        IPurchaseOrderServices purchaseOrderService,
        IOrderHistoryService orderingHistoryService,
        IGoodsReceiptServices goodsReceiptService,
        IGoodsReceiptItemServices goodsReceiptItemService,
        IConfigSettingService configSettingsService
    
        ) : base(appUserService)
        {
            _appUserservice = appUserService;
            _unitOfWork = unitOfWork;
            _masterItemServices = masterItemService;
            _orderingLookUpService = orderingLookUpService;
            _orderingService = orderingService;
            _goodsReceiptService = goodsReceiptService;
            _goodsReceiptItemService = goodsReceiptItemService;
            _supplierService = supplierService;
            _purchaseOrderService = purchaseOrderService;
            _configSettingsService = configSettingsService;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public ActionResult Index()
        {
            return View();
        }

       public JsonResult Search(DateTime? dateFrom, DateTime? dateTo,int page,int pageSize, string sortColumn,string sortOrder,string goodsReceiptCode="",string status="", string createdBy="")
        {
            var filter = new DataSearch
            {
                Page = page,
                PageSize = pageSize,
                SortColumn = string.IsNullOrEmpty(sortColumn) ? "GoodsReceiptId" : sortColumn.Replace(" ", string.Empty),
                SortDirection = sortOrder
            };

            var getSearchedData = _goodsReceiptService.Search(filter,goodsReceiptCode, status, createdBy,dateFrom,dateTo);
            return Json(new
            {
                data = getSearchedData.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SaveGoodsReceipt(List<GoodsReceiptViewModel.GoodsReceiptDatatoReceiveFromJson> items,int? purchaseOrderId,int totalQuantity, double totalCost)
        {
            if (purchaseOrderId == 0)
            {
                purchaseOrderId = null;
            }
            var createData = new GoodsReceipt();
            createData.GoodsReceiptCode = RandomString(10);
            createData.PurchaseOrderId = purchaseOrderId;
            createData.TotalQuantity = totalQuantity;
            createData.TotalCost = totalCost;
            createData.CreatedByAppUserId = CurrentUser.AppUserId;
            createData.CreatedOn = DateTime.Now;
            createData.ModifiedOn = DateTime.Now;
            createData.ModifiedByAppUserId = CurrentUser.AppUserId;
            createData.Status = "CREATED";
            _goodsReceiptService.InsertOrUpdate(createData);
            var saveData = new GoodsReceiptItem();

            foreach (var item in items)
            {
                saveData.GoodsReceiptId = _goodsReceiptService.GetGoodsReceiptIdByCode(createData.GoodsReceiptCode);
                saveData.MasterItemId = item.MasterItemId;
                saveData.Quantity = item.Quantity;
                saveData.Cost = item.Cost;
                saveData.TotalCost = item.TotalCost;
                saveData.SupplierId = item.SupplierId;
                saveData.ItemDescription = item.MasterItemDescription;
                _goodsReceiptItemService.InsertOrUpdate(saveData);
                _unitOfWork.SaveChanges();
            }
            _unitOfWork.SaveChanges();
        }
        public ActionResult SuccessSaveGoodsReceipt()
        {
            CreateSuccessMessage("Goods Receipt successfully saved.");
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int goodsReceiptId, string purchaseOrderCode, int? purchaseOrderId)
        {
            var getDataFromGoodsReceipt = _goodsReceiptService.GetAllById(goodsReceiptId);
            var getItemsFromGoodsReceiptItem = _goodsReceiptItemService.GetAllByGoodsReceiptId(goodsReceiptId);
            var getSupplierCodes = _supplierService.GetAllActiveSuppliers();
            int supplierId;
          
            if (purchaseOrderId == null)
            {
                 supplierId = _goodsReceiptItemService.GetSupplierIdByGoodsReceiptId(goodsReceiptId);
            }
            else
            {
                 supplierId = _goodsReceiptItemService.GetSupplierId(purchaseOrderId);
            }
       
            List<string> ItemDescriptions = _goodsReceiptItemService.GetMasterItemDescriptionById(goodsReceiptId);
            if (purchaseOrderCode == null)
            {
                purchaseOrderCode = "N/A";
            }
            GoodsReceiptViewModel.GoodsReceiptEditModel.GoodsReceiptIdForDb = goodsReceiptId;
       
            var viewModel = new GoodsReceiptViewModel.GoodsReceiptEditModel
            {           
                GoodsReceiptId = goodsReceiptId,
                PurchaseOrderCode = purchaseOrderCode,
                Status = getDataFromGoodsReceipt.Status,
                CreatedBy = getDataFromGoodsReceipt.CreatedByAppUser.FullName,
                CreatedOn = getDataFromGoodsReceipt.CreatedOn,
                UpdatedBy = getDataFromGoodsReceipt.ModifiedByAppUser.FullName,
                UpdatedOn = getDataFromGoodsReceipt.ModifiedOn,
                Products = getItemsFromGoodsReceiptItem,
                MasterItemDescription = ItemDescriptions,
                SupplierIdForJson = supplierId,
                SupplierDescription = _supplierService.GetSupplierDescription(supplierId),
                SupplierCodeList = new SelectList(getSupplierCodes, "SupplierId", "SupplierDescription")
            };
            return View(viewModel);
        }
     
        public ActionResult UpdateGoodsReceipt(List<GoodsReceiptViewModel.GoodsReceiptDatatoReceiveFromJson> items, int goodsReceiptId, int totalQuantity, double totalCost)
        {
            var getDataToUpdated = _goodsReceiptService.GetAllById(goodsReceiptId);
            getDataToUpdated.TotalCost = totalCost;
            getDataToUpdated.TotalQuantity = totalQuantity;
            getDataToUpdated.ModifiedOn = DateTime.Now;
            getDataToUpdated.ModifiedByAppUserId = CurrentUser.AppUserId;
            _goodsReceiptService.InsertOrUpdate(getDataToUpdated);

            var getItemsToClear = _goodsReceiptItemService.GetAllByGoodsReceiptId(_goodsReceiptService.GetGoodsReceiptIdByCode(getDataToUpdated.GoodsReceiptCode));
            foreach (var item in getItemsToClear)
            {
                _goodsReceiptItemService.Delete(item);
            }
            var updateGoodsReceiptItem = new GoodsReceiptItem();
            foreach (var item in items)
            {
                updateGoodsReceiptItem.GoodsReceiptId = goodsReceiptId;
                updateGoodsReceiptItem.MasterItemId = item.MasterItemId;
                updateGoodsReceiptItem.Quantity = item.Quantity;
                updateGoodsReceiptItem.Cost = item.Cost;
                updateGoodsReceiptItem.TotalCost = item.TotalCost;
                updateGoodsReceiptItem.SupplierId = item.SupplierId;
                updateGoodsReceiptItem.ItemDescription = item.MasterItemDescription;
                _goodsReceiptItemService.InsertOrUpdate(updateGoodsReceiptItem);
                _unitOfWork.SaveChanges();
            }
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ExportToGoodsReceipt(int orderId, string purchaseOrderCode, int supplierId, int purchaseOrderId)
        {
            var getDataFromOrdering = _orderingService.GetById(orderId); ;
            var getItemsFromLookUp = _orderingLookUpService.GetOrderingLookUpById(orderId);
            OrderingLookUpViewModel.OrderIdForDb = getDataFromOrdering.OrderId;
            OrderingLookUpViewModel.SupplierIdForDb = supplierId;
            OrderingLookUpViewModel.PurchaseOrderIdForDb = purchaseOrderId;
            var getSupplierCodes = _supplierService.GetAllActiveSuppliers();

            int? purchaseOrderIdForDb = _goodsReceiptService.IsPurchaseOrderExist(purchaseOrderId);
            var purchaseOrderData = _purchaseOrderService.GetPurchaseOrderId(purchaseOrderId);
            purchaseOrderData.Status = "FORWARDED";
            _purchaseOrderService.InsertOrUpdate(purchaseOrderData);
            _unitOfWork.SaveChanges();


            if (purchaseOrderIdForDb == null || purchaseOrderIdForDb == 0)
            {
                var viewModel = new OrderingLookUpViewModel
                {
                    OrderId = getDataFromOrdering.OrderId,
                    PurchaseOrderCode = purchaseOrderCode,
                    Status = getDataFromOrdering.Status,
                    CreatedBy = getDataFromOrdering.CreatedByAppUser.FullName,
                    CreatedOn = getDataFromOrdering.CreatedOn,
                    UpdatedBy = getDataFromOrdering.ModifiedByAppUser.FullName,
                    UpdatedOn = getDataFromOrdering.ModifiedOn,
                    TotalCost = getDataFromOrdering.TotalCost,
                    TotalQuantity = getDataFromOrdering.TotalQuantity,
                    Products = getItemsFromLookUp,
                    SupplierIdForJson = supplierId,
                    SupplierDescription = _supplierService.GetSupplierDescription(supplierId),
                    PurchaseOrderId = purchaseOrderId,
                    SupplierCodeList = new SelectList(getSupplierCodes, "SupplierId", "SupplierDescription")
                };
                CreateSuccessMessage("Copied to Goods Receipt Template");
                return View("~/Views/Ordering/GoodsReceipt.cshtml", viewModel);
            }
            else
            {
                CreateErrorMessage("P.O. already exists in G.R.");
                return RedirectToAction("Index","PurchaseOrder");
            }
        }
        [HttpGet]
        public ActionResult Create()
        {

            var showAllSuppliers = _supplierService.GetAllActiveSuppliers().Select(x => new KeyValuePair<int, string>(x.SupplierId,x.SupplierDescription)).ToList();
            var suppliersList = new SelectList((IEnumerable)showAllSuppliers, "Key", "Value");
            GoodsReceiptViewModel.GoodsReceiptManualCreateModel.GoodsReceiptIdForDb = _goodsReceiptService.GetMaxGoodsReceiptId() + 1;
            var showData = new GoodsReceiptViewModel.GoodsReceiptManualCreateModel
            {
                CreatedOn = DateTime.Now,
                CreatedBy = CurrentUser.FullName,
                Status = "CREATED",
                SupplierList = suppliersList,
                GoodsReceiptId = _goodsReceiptService.GetMaxGoodsReceiptId()+1
            };

            return View(showData);
        }
        public JsonResult PurchaseOrderIdForShortcut()
        {
            var purchaseOrderId = OrderingLookUpViewModel.PurchaseOrderIdForDb;
            return Json(new
            {
                data = purchaseOrderId
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGoodsReceiptIdForShortcutManual()
        {
            var goodsReceiptId = GoodsReceiptViewModel.GoodsReceiptManualCreateModel.GoodsReceiptIdForDb;
            return Json(new
            {

                data = goodsReceiptId
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGoodsReceiptIdForShortcutEdit()
        {
            var goodsReceiptId = GoodsReceiptViewModel.GoodsReceiptEditModel.GoodsReceiptIdForDb;
            return Json(new
            {

                data = goodsReceiptId
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSavedGoodsReceipt()
        {
            var getData = _goodsReceiptItemService.GetAllByGoodsReceiptId(GoodsReceiptViewModel.GoodsReceiptEditModel.GoodsReceiptIdForDb);
            return Json(new
            {
                data = getData.Select(a => new
                {
                  a.GoodsReceiptId,
                  a.MasterItemId,
                  MasterItemDescription=a.ItemDescription,
                  SupplierDescription=a.Supplier.SupplierDescription,
                  a.Quantity,
                  a.Cost,
                  a.TotalCost,
                  a.SupplierId,
                })
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllSuppliers()
        {
            var getSuppliers = _supplierService.GetAllActiveSuppliers();
            return Json(new
            {
                data = getSuppliers
            }, JsonRequestBehavior.AllowGet);
        }
     

        public JsonResult ExportGoodsReceipt()
        {
            var getAllItemsFromLookUp = _orderingLookUpService.GetOrdersBySupplierIdAndOrderId(OrderingLookUpViewModel.OrderIdForDb, OrderingLookUpViewModel.SupplierIdForDb);
            List<PurchaseOrderViewModel> itemsToExport = new List<PurchaseOrderViewModel>();
            foreach (var item in getAllItemsFromLookUp)
            {
                itemsToExport.Add(new PurchaseOrderViewModel
                {
                    OrderId = item.OrderId,
                    SupplierId = item.SupplierId,
                    SupplierDescription = item.Supplier.SupplierDescription,
                    MasterItemId = item.MasterItemId,
                    MasterItemDescription = item.ItemDescription,
                    Quantity = item.Quantity,
                    Cost = item.Cost,
                    TotalCost = item.TotalCost
                });
            }
            return Json(new
            {
                data = itemsToExport
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGoodsReceiptItemForForward(int goodsReceiptId)
        {
            var getData = _goodsReceiptItemService.GetGoodsReceiptItemForForward(goodsReceiptId);
            return Json(new
            {
                data = getData
            }, JsonRequestBehavior.AllowGet);
        }
  
        public ActionResult ViewPDF(int goodsReceiptId,string goodsReceiptCode)
        {
            return Redirect($"~/SystemReport/GoodsReceipt.aspx?goodsReceiptId="+goodsReceiptId+"&"+"goodsReceiptCode="+goodsReceiptCode);
        }
        public ActionResult CompleteGoodsReceipt(int goodsReceiptId,int supplierId)
        {
            var updateData = _goodsReceiptService.GetAllById(goodsReceiptId);
            updateData.Status = "APPROVED";
            updateData.TotalQuantity = _goodsReceiptItemService.GetSumOfQuantityGoodsReceiptItem(goodsReceiptId);
            updateData.TotalCost = _goodsReceiptItemService.GetSumOfTotalCostGoodsReceiptItem(goodsReceiptId);
            updateData.ModifiedOn = DateTime.Now;
            updateData.ModifiedByAppUserId = CurrentUser.AppUserId;
            _goodsReceiptService.InsertOrUpdate(updateData);
            if (updateData.PurchaseOrderId == null)
            {

            }
            else
            {
                var updatePurchaseOrder = _purchaseOrderService.GetPurchaseOrderId(updateData.PurchaseOrderId);
                updatePurchaseOrder.Status = "APPROVED";
                _purchaseOrderService.InsertOrUpdate(updatePurchaseOrder);
            }
 
            _unitOfWork.SaveChanges();
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(_configSettingsService.GetValueById((int)ConfigurationSettings.GoodsReceiptPdfFolder)))
                {
                    Directory.CreateDirectory(_configSettingsService.GetValueById((int)ConfigurationSettings.GoodsReceiptPdfFolder));
                }
            }
            catch (Exception)
            {
                throw;
            }
            GeneratePDF(goodsReceiptId, updateData.GoodsReceiptCode);
            FileStream fs = new FileStream(_configSettingsService.GetValueById((int)ConfigurationSettings.GoodsReceiptPdfFolder) + updateData.GoodsReceiptCode + "." + fileNameExtension + "", FileMode.OpenOrCreate);
            byte[] data = new byte[fs.Length];
            fs.Write(renderedByte, 0, renderedByte.Length);
            fs.Close();
            return RedirectToAction("Index");
        }
        private void GeneratePDF(int goodsReceiptId, string goodsReceiptCode)
        {
            GoodsReceiptDataset goodsReceiptDataset = new GoodsReceiptDataset();
            string[] stream;
            string mimeType;
            Warning[] warnings;
            string encoding;
            renderedByte = null;
            string path = System.Configuration.ConfigurationManager.ConnectionStrings["CodebizERP"].ConnectionString;
            SqlConnection connection = new SqlConnection(path);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter;
            SqlDataReader reader;
            adapter = new SqlDataAdapter(MegaQuery.QueryForGoodsReceipt(goodsReceiptId), connection);
            adapter.Fill(goodsReceiptDataset, goodsReceiptDataset.Tables[0].TableName);
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT PO.PurchaseOrderCode from PurchaseOrder PO INNER JOIN GoodsReceipt GR on PO.PurchaseOrderId=GR.PurchaseOrderId where GoodsReceiptId="+goodsReceiptId+"";
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                foreach(DataRow dr in goodsReceiptDataset.Tables[0].Rows)
                {             
                  dr["PurchaseOrderCode"] = reader.GetValue(0);                   
                }
            }
            else
            {
                foreach (DataRow dr in goodsReceiptDataset.Tables[0].Rows)
                {
                    dr["PurchaseOrderCode"] = "N/A (Direct G.R.)";
                }
            }
            connection.Close();
            reader.Close();
            connection.Open();
            command.CommandText = "SELECT GoodsReceiptCode,TotalQuantity, CAST(FORMAT(TotalCost,'N') as varchar(20)) as TotalCost from GoodsReceipt where GoodsReceiptId="+goodsReceiptId+"";
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                foreach (DataRow dr in goodsReceiptDataset.Tables[0].Rows)
                {
                    dr["PrintDate"] = DateTime.Now.ToString();
                    dr["TotalQuantity"] = reader.GetValue(1);
                    dr["TotalCost"] = reader.GetValue(2);
                    dr["GoodsReceiptCode"] = reader.GetValue(0);

                }
            }
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/GoodsReceipt/Reports/GoodsReceiptReport.rdlc");
            ReportDataSource rds = new ReportDataSource("GoodsReceiptDataset", goodsReceiptDataset.Tables[0]);
            rds.Name = "GoodsReceiptDataset";
            localreport.DataSources.Clear();
            localreport.DataSources.Add(rds);
            fileNameExtension = "PDF";
            renderedByte = localreport.Render("PDF", "", out mimeType, out encoding, out fileNameExtension, out stream, out warnings);
            Response.ContentType = "application/pdf";
        }

    }
}