using Domain.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using Infrastructure.Services;
using Infrastructure;
using Codebiz.Domain.Common.Model;
using Web.Areas.Inventory.Models.ViewModels.MasterItems;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using System.Net.Mail;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.ERP.Model.DTO;
using Infrastructure.Models;
using Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.Ordering;
using Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem;
using Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.Ordering;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Controllers
{
    public class OrderingController : FixedAssetsMaterialsManagementSystemControllerBaseController
    {
        // GET: Ordering
        
        public string fileNameExtension = "PDF";
        public byte[] renderedByte = null;
        MailMessage message;
        SmtpClient smtpServer;
        private readonly IAppUserServices _appUserservice;
        private readonly IMasterItemServices _masterItemServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderServices _orderingService;
        private readonly IOrderItemServices _orderingLookUpService;
        private readonly ISupplierService _supplierService;
        private readonly IMasterItemSupplierService _masterItemSupplierService;
        private readonly IPurchaseOrderServices _purchaseOrderService;
        private readonly IOrderHistoryService _orderingHistoryService;
        private readonly IGoodsReceiptServices _goodsReceiptService;
        private readonly IConfigSettingService _configSettingsService;
        public OrderingController(IAppUserServices appUserService,
        IUnitOfWork unitOfWork,
        IMasterItemServices masterItemService,
        IOrderServices orderingService,
        IOrderItemServices orderingLookUpService,
        ISupplierService supplierService,
        IMasterItemSupplierService masterItemSupplierService,
        IPurchaseOrderServices purchaseOrderService,
        IOrderHistoryService orderingHistoryService,
        IGoodsReceiptServices goodsReceiptService,
        IConfigSettingService configSettingsService
        ) : base(appUserService) {
            _appUserservice = appUserService;
            _unitOfWork = unitOfWork;
            _masterItemServices = masterItemService;
            _orderingLookUpService = orderingLookUpService;
            _orderingService = orderingService;
            _supplierService = supplierService;
            _masterItemSupplierService = masterItemSupplierService;
            _purchaseOrderService = purchaseOrderService;
            _orderingHistoryService = orderingHistoryService;
            _goodsReceiptService = goodsReceiptService;
            _configSettingsService = configSettingsService;
        }
        public ActionResult Index(string orderCode="",int page = 1, int pageSize = 10)
        {
            return View();
        }
        public JsonResult Search(DateTime? dateFrom, DateTime? dateTo,int page,int pageSize, string sortOrder,string sortColumn,string orderCode="",string status = "",string createdBy="")
        {
            var filter = new DataSearch
            {
                Page = page,
                PageSize = pageSize,
                SortColumn = string.IsNullOrEmpty(sortColumn) ? "OrderId" : sortColumn.Replace(" ", string.Empty),
                SortDirection = sortOrder
            };

            var getData = _orderingService.Search(filter,orderCode, status,createdBy,dateFrom,dateTo);
            return Json(new
            {
                data = getData.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            },JsonRequestBehavior.AllowGet);
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public ActionResult Create()
        {

            var ordering = new ReplenishmentOrder();
            ordering.OrderCode = RandomString(10);
            ordering.CreatedByAppUserId =CurrentUser.AppUserId ;
            ordering.CreatedOn = DateTime.Now;
            ordering.ModifiedByAppUserId = CurrentUser.AppUserId;
            ordering.ModifiedOn = DateTime.Now;
            ordering.TotalQuantity = 0;
            ordering.TotalCost = 0;
            ordering.Status = "CREATED";
            _orderingService.InsertOrUpdate(ordering);
          
            CreateSuccessMessage("Order successfully generated");
            string Status = "Created";
            InsertOrderHistory(_orderingService.GetOrderIdByOrderCode(ordering.OrderCode),Status);
            _unitOfWork.SaveChanges();
            return RedirectToAction("EditDraft",new {OrderId=ordering.OrderId});
        }
        public ActionResult ViewGoodsReceipt(int purchaseOrderId)
        {
            var goodsReceiptCode = _goodsReceiptService.GetGoodsReceiptCode(purchaseOrderId);
            return Redirect($"~/SystemReport/GoodsReceipt.aspx?goodsReceiptCode="+ goodsReceiptCode);
        }
        private void InsertOrderHistory(int OrderId,string Status)
        {
            
            var orderingHistory = new OrderHistory();
            orderingHistory.OrderId = OrderId;
            orderingHistory.CreatedByAppUserId = CurrentUser.AppUserId;
            orderingHistory.CreatedOn = DateTime.Now;
            orderingHistory.ActionMade = Status+" the order";
            _orderingHistoryService.InsertOrUpdate(orderingHistory);
            _unitOfWork.SaveChanges();

        }
        public ActionResult Draft(int orderId,string orderCode,int page=1,int pageSize=7)
        {
            var result = _orderingService.GetByOrderCode(orderCode);
            var getDataFromOrderingLookup = _orderingLookUpService.GetOrderingLookUpById(orderId);
            var getDataFromPurchaseOrder = _purchaseOrderService.GetAllById(orderId);
            var getDataFromOrderingHistory = _orderingHistoryService.GetAllByOrderId(orderId);
            var viewModel = new OrderingDetailsViewModel
            {
                OrderId = result.OrderId,
                OrderCode = result.OrderCode,
                Status = result.Status,
                CreatedBy = result.CreatedByAppUser.FullName,
                UpdatedBy = result.ModifiedByAppUser.FullName,
                TotalCost = result.TotalCost,
                TotalQuantity = result.TotalQuantity,
                CreatedOn = result.CreatedOn,
                UpdatedOn = result.ModifiedOn,
                OrderLookUps = getDataFromOrderingLookup,
                PurchaseOrders=getDataFromPurchaseOrder,
                OrderingHistories=getDataFromOrderingHistory.ToPagedList(page,pageSize),
                Page=page,
                PageSize=pageSize
            };
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult EditDraft(int OrderId)
        {           
            var getDataFromOrdering = _orderingService.GetById(OrderId);
            if (getDataFromOrdering.Status == "COMPLETED")
            {
                CreateErrorMessage("Order is already completed");
                return RedirectToAction("Index");
            }
            var getItemsFromLookUp = _orderingLookUpService.GetOrderingLookUpById(OrderId);
            OrderingLookUpViewModel.OrderIdForDb = getDataFromOrdering.OrderId;
            var getSupplierCodes = _supplierService.GetAllActiveSuppliers();
            OrderingLookUpViewModel.OrderIdForDb = getDataFromOrdering.OrderId;
            var viewModel = new OrderingLookUpViewModel
            {
                OrderId = getDataFromOrdering.OrderId,                
                OrderCode = getDataFromOrdering.OrderCode,
                Status = getDataFromOrdering.Status,
                CreatedBy = getDataFromOrdering.CreatedByAppUser.FullName,
                CreatedOn = getDataFromOrdering.CreatedOn,
                UpdatedBy = getDataFromOrdering.ModifiedByAppUser.FullName,
                UpdatedOn = getDataFromOrdering.ModifiedOn,
                TotalCost = getDataFromOrdering.TotalCost,
                TotalQuantity = getDataFromOrdering.TotalQuantity,
                Products = getItemsFromLookUp,
                SupplierCodeList = new SelectList(getSupplierCodes, "SupplierId", "SupplierDescription")
            };
            return View(viewModel);
        }
        #region JSON
        public JsonResult GetOrderIdForShortCut()
        {
            var orderId = OrderingLookUpViewModel.OrderIdForDb;
            return Json(new
            {
                data = orderId
            }, JsonRequestBehavior.AllowGet);
        }
 
        public JsonResult GetSuppliers()
        {
            var getSuppliers = _supplierService.GetAllActiveSuppliers();

            return Json(new
            {
                data = getSuppliers.Select(a => new
                {
                    Id=a.SupplierId,
                    Description = a.SupplierDescription
                })

            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSavedOrders()
        {
            var getOrders = _orderingLookUpService.GetOrderingLookUpById(OrderingLookUpViewModel.OrderIdForDb);
            return Json(new
            {
                data = getOrders.Select(a => new
                {
                    a.SupplierId,
                    a.MasterItemId,
                    MasterItemDescription=a.ItemDescription,
                    a.Cost,
                    a.Quantity,
                    a.TotalCost,
                    a.OrderId
                })
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllMasterItem()
        {
            var getAllItems = _masterItemSupplierService.GetAllItems();
            return Json(new
            {
                data=getAllItems
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemList(int SupplierId)
        {
            var getItems = _masterItemSupplierService.GetMasterItemSuppliersId(SupplierId);
            var Items = new List<MasterItemOrderingDTO>();
            foreach (var item in getItems)
            {
                Items.Add(new MasterItemOrderingDTO
                {
                    SupplierId = item.SupplierId,
                    Id = item.MasterItem.MasterItemId,
                    ItemCode = item.MasterItem.ItemCode,
                    Description = item.MasterItem.LongDescription,
                    Cost = item.MasterItem.UnitPrice,
                    SupplierDescription = item.Supplier.SupplierDescription,

                    BrandId = _masterItemServices.GetById(item.MasterItem.MasterItemId).Brand.BrandId,
                    BrandName = _masterItemServices.GetById(item.MasterItem.MasterItemId).Brand.BrandName

                });
            }
            return Json(new
            {
                data = Items
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FilterBySupplier (int? supplierId)
        {
            var getAllItems = _masterItemSupplierService.FilterSuppliers(supplierId);
            return Json(new
            {
                data = getAllItems
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchProduct(string longDescription,int? supplierId)
        {
            var getItemsFromSearch = _masterItemSupplierService.SearchProductModal(longDescription,supplierId);
            var Items = new List<MasterItemOrderingDTO>();
            foreach (var item in getItemsFromSearch)
            {
                Items.Add(new MasterItemOrderingDTO
                {
                    SupplierId = item.SupplierId,
                    Id = item.MasterItem.MasterItemId,
                    ItemCode = item.MasterItem.ItemCode,
                    Description = item.MasterItem.LongDescription,
                    Cost = item.MasterItem.UnitPrice,
                    SupplierDescription = item.Supplier.SupplierDescription,
                    BrandId = item.MasterItem.Brand.BrandId,
                    BrandName=item.MasterItem.Brand.BrandName
                });
            }
            return Json(new
            {
                data = Items
            }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetItemCost(int MasterItemId)
        {           
            var cost = _masterItemServices.GetAllById(MasterItemId).Select(a => a.UnitPrice).FirstOrDefault();
            return Json(new
            {
                data = cost
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMaxOrderId()
        {
            var orderId = _orderingService.GetMaxOrderId();
            return Json(new
            {
                data=orderId
            }, JsonRequestBehavior.AllowGet);
        }
       
        #endregion
        public ActionResult SaveOrder(List<OrdersFromAngular> data, int OrderId)
        {
        
                var insertIntoOrderingLookUp = new OrderItem();
                var getItemsToClear = _orderingLookUpService.GetOrderingLookUpById(OrderId);
                double TotalCost = 0;
                int TotalQuantity = 0;
            try
            {
                foreach (var item in getItemsToClear)
                {
                    _orderingLookUpService.Delete(item);
                }
                _unitOfWork.SaveChanges();
                for (int i = 0; i <= data.Count - 1; i++)
                {
                    insertIntoOrderingLookUp.OrderId = OrderId;
                    insertIntoOrderingLookUp.SupplierId = data[i].SupplierId;
                    insertIntoOrderingLookUp.MasterItemId = data[i].MasterItemId;
                    insertIntoOrderingLookUp.ItemDescription = data[i].MasterItemDescription;
                    if (data[i].Quantity < 1)
                    {
                        CreateErrorMessage("An error occured.");
                    }
                    else
                    {
                        insertIntoOrderingLookUp.Quantity = data[i].Quantity;
                    }
                
                    insertIntoOrderingLookUp.Cost = data[i].Cost;
                    insertIntoOrderingLookUp.TotalCost = data[i].Cost * data[i].Quantity;
                    TotalCost += data[i].Cost * data[i].Quantity;
                    TotalQuantity += data[i].Quantity;
                    _orderingLookUpService.InsertOrUpdate(insertIntoOrderingLookUp);
                    _unitOfWork.SaveChanges();
                }
         
            }
            catch
            {
                throw;
            }

                var editOrderData = _orderingService.GetAllById(OrderId).FirstOrDefault();
                editOrderData.TotalCost = TotalCost;
                editOrderData.TotalQuantity = TotalQuantity;
                editOrderData.CreatedByAppUserId =CurrentUser.AppUserId;
                editOrderData.ModifiedOn = DateTime.Now;
                _orderingService.InsertOrUpdate(editOrderData);
                _unitOfWork.SaveChanges();
                string Status = "Updated";
                InsertOrderHistory(OrderId, Status);
          
                CreateSuccessMessage("Order successfully saved");
                return RedirectToAction("Index");    
        }
        public ActionResult CancelOrder(List<OrdersFromAngular> data,int OrderId)
        {

            var insertIntoOrderingLookUp = new OrderItem();
            var getItemsToClear = _orderingLookUpService.GetOrderingLookUpById(OrderId);
            double TotalCost = 0;
            int TotalQuantity = 0;
            try { 
            foreach (var item in getItemsToClear)
            {
                _orderingLookUpService.Delete(item);
            }
   
                for (int i = 0; i <= data.Count - 1; i++)
                {
                    insertIntoOrderingLookUp.OrderId =OrderId;
                    insertIntoOrderingLookUp.SupplierId = data[i].SupplierId;
                    insertIntoOrderingLookUp.MasterItemId = data[i].MasterItemId;
                    insertIntoOrderingLookUp.ItemDescription = data[i].MasterItemDescription;
                    insertIntoOrderingLookUp.Quantity = data[i].Quantity;
                    insertIntoOrderingLookUp.Cost = data[i].Cost;
                    insertIntoOrderingLookUp.TotalCost = data[i].Cost * data[i].Quantity;
                    TotalCost += data[i].Cost * data[i].Quantity;
                    TotalQuantity += data[i].Quantity;
                    _orderingLookUpService.InsertOrUpdate(insertIntoOrderingLookUp);
                }
            }
            catch
            {
                throw;
            }
            var editOrderData = _orderingService.GetAllById(OrderId).FirstOrDefault();
            editOrderData.TotalCost = TotalCost;
            editOrderData.TotalQuantity = TotalQuantity;
            editOrderData.ModifiedByAppUserId = CurrentUser.AppUserId;
            editOrderData.Status = "CANCELLED";
            editOrderData.ModifiedOn = DateTime.Now;
            _orderingService.InsertOrUpdate(editOrderData);
            _unitOfWork.SaveChanges();
            string Status = "Cancelled";
            InsertOrderHistory(OrderId, Status);
            CreateSuccessMessage("Order successfully cancelled");
            return RedirectToAction("Index");
        }
        public ActionResult SuccessfulSave()
        {
            CreateSuccessMessage("Order saved successfully");
            return RedirectToAction("Index");
        }
        public ActionResult SuccessfulComplete()
        {
            CreateSuccessMessage("Order completed successfully");
            return RedirectToAction("Index");
        }
        private void GeneratePDF(int OrderId, int SupplierId, string PurchaseOrderCode)
        {
            PurchaseOrderDataset purchaseDataset = new PurchaseOrderDataset();
            string path = System.Configuration.ConfigurationManager.ConnectionStrings["CodebizERP"].ConnectionString;
            SqlConnection connection = new SqlConnection(path);
            SqlDataReader reader;
            SqlCommand command = new SqlCommand();
            string[] stream;
            string mimeType;
            Warning[] warnings;
            string encoding;
            fileNameExtension = "PDF";
            renderedByte = null;
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(MegaQuery.QueryForPurchaseOrder(OrderId,SupplierId), connection);
            adapter.Fill(purchaseDataset, purchaseDataset.Tables[0].TableName);
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT SUM(Quantity), CAST(FORMAT(SUM(TotalCost),'N') as varchar (20)) from OrderItem where OrderId="+OrderId+" and SupplierId="+SupplierId+"";
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                foreach (DataRow dr in purchaseDataset.Tables[0].Rows)
                {
                    dr["PrintDate"] = DateTime.Now;
                    dr["PurchaseOrdercode"] = PurchaseOrderCode;
                    dr["TotalQuantity"] = reader.GetValue(0);
                    dr["TotalCost"] = reader.GetValue(1);
                }
            }
            connection.Close();
            reader.Close();
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/Ordering/Reports/PurchaseOrder.rdlc");
            ReportDataSource rds = new ReportDataSource("PurchaseOrderDataset", purchaseDataset.Tables[0]);
            rds.Name = "PurchaseOrderDataset";
            localreport.DataSources.Clear();
            localreport.DataSources.Add(rds);
            fileNameExtension = "PDF";
            renderedByte = localreport.Render("PDF", "", out mimeType, out encoding, out fileNameExtension, out stream, out warnings);
            Response.ContentType = "application/pdf";
        }
    
        public ActionResult CompleteOrder(int orderId)
        {
            try
            {
                // If the directory doesn't exist, create it.
                if (!Directory.Exists(_configSettingsService.GetValueById((int)ConfigurationSettings.PurchaseOrderPdfFolder)))
                {
                    Directory.CreateDirectory(_configSettingsService.GetValueById((int)ConfigurationSettings.PurchaseOrderPdfFolder));
                }
            }
            catch (Exception)
            {
                throw;
            }
            var getDistinctSuppliers = _orderingLookUpService.GetOrderingLookUpById(orderId).Select(x => x.SupplierId).Distinct().ToList();
          
            for (int i = 0; i <= getDistinctSuppliers.Count - 1; i++)
            {
                var createData = new PurchaseOrder();
                string PurchaseOrderCodeToDb = RandomString(10);
                createData.PurchaseOrderCode = PurchaseOrderCodeToDb;
                createData.SupplierId = getDistinctSuppliers[i];
                createData.OrderId = orderId;
                createData.Status = "CREATED";
                createData.Quantity = _orderingLookUpService.GetSumOfQuantity(orderId, getDistinctSuppliers[i]);
                createData.TotalCost = _orderingLookUpService.GetSumOfTotalCost(orderId, getDistinctSuppliers[i]);                
                _purchaseOrderService.InsertOrUpdate(createData);
                _unitOfWork.SaveChanges();              
                GeneratePDF(orderId, getDistinctSuppliers[i], PurchaseOrderCodeToDb);
                FileStream fs = new FileStream(_configSettingsService.GetValueById((int)ConfigurationSettings.PurchaseOrderPdfFolder) + PurchaseOrderCodeToDb + "." + fileNameExtension + "", FileMode.OpenOrCreate);           
                byte[] data = new byte[fs.Length];
                fs.Write(renderedByte, 0, renderedByte.Length);
                fs.Close();
                int SupplierIdForEmail = getDistinctSuppliers[i];
                var toEmailAddress = _supplierService.GetEmail(SupplierIdForEmail);
                string EmailRecipient = toEmailAddress;
                message = new MailMessage();
                smtpServer = new SmtpClient(_configSettingsService.GetValueById((int)ConfigurationSettings.SmtpHost));
                message.From = new MailAddress(_configSettingsService.GetValueById((int)ConfigurationSettings.SmtpUsername));
                message.To.Add(EmailRecipient);
                message.Subject = "Purchase Order";
                message.Body = "Here's the attached pdf of Purchase Order:" + PurchaseOrderCodeToDb;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(_configSettingsService.GetValueById((int)ConfigurationSettings.PurchaseOrderPdfFolder) + PurchaseOrderCodeToDb + "." + fileNameExtension);
                attachment.Name = PurchaseOrderCodeToDb + "." + fileNameExtension;
                message.Attachments.Add(attachment);
                smtpServer.Port = Convert.ToInt32(_configSettingsService.GetValueById((int)ConfigurationSettings.SmtpPort));
                smtpServer.Credentials = new System.Net.NetworkCredential(_configSettingsService.GetValueById((int)ConfigurationSettings.SmtpUsername), _configSettingsService.GetValueById((int)ConfigurationSettings.SmtpPassword));
                smtpServer.EnableSsl = true;
                smtpServer.Send(message);
            }
            var dataSetStatus = _orderingService.GetById(orderId);
            dataSetStatus.Status = "COMPLETED";
            _orderingService.InsertOrUpdate(dataSetStatus);
            _unitOfWork.SaveChanges();
            string Status = "Completed";
            InsertOrderHistory(orderId, Status);
            CreateSuccessMessage("Order successfully completed");
            return RedirectToAction("Index");
        }
        public void DownloadPurchaseOrder(int OrderId, int SupplierId, string PurchaseOrderCode)
        {
           // GeneratePDF(OrderId, SupplierId, PurchaseOrderCode);
           // ReportDraftViewer.OrderId = OrderId;
           // ReportDraftViewer.SupplierId = SupplierId;
           // ReportDraftViewer.PurchaseOrderCode = PurchaseOrderCode;

           //return PartialView("PurchaseOrderViewerModal");
        }
     
    }
}