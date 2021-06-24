using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Enums.FAMMS.Inventory.ItemMasterData;
using Codebiz.Domain.Common.Model.Filter.FAMMS.Inventory.ItemMasterData;
using Codebiz.Domain.ERP.Context.SeedData;
using Codebiz.Domain.ERP.Model.Enums.FAMMS;
using Infrastructure.Services;
using Infrastructure.Services.Common.Inventory;
using Infrastructure.Services.Common.Inventory.ShippingType;
using Infrastructure.Services.FAMMS.Inventory;
using Infrastructure.Services.FAMMS.Inventory.ItemGroup;
using Infrastructure.Services.FAMMS.Inventory.ItemMasterData;
using Infrastructure.Services.FAMMS.Inventory.ItemType;
using Infrastructure.Services.FAMMS.Inventory.PriceList;
using Infrastructure.Services.FAMMS.Inventory.Status;
using System;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Controllers
{
    public class ItemMasterDataController : FixedAssetsMaterialsManagementSystemControllerBaseController
    {
        private readonly IItemMasterDataService _itemMasterDataService;
        private readonly IAppUserServices _appUserServices;
        private readonly IItemTypeService _itemTypeService;
        private readonly IPriceListService _priceListService;
        private readonly IItemGroupService _itemGroupService;
        private readonly IItemMasterDataStatusService _itemMasterDataStatusService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IShippingTypeService _shippingTypeService;

        public ItemMasterDataController(
            IAppUserServices appUserServices,
            IItemTypeService itemTypeService,
            IPriceListService priceListService,
            IItemGroupService itemGroupService,
            IItemMasterDataStatusService itemMasterDataStatusService,
            IManufacturerService manufacturerService,
            IShippingTypeService shippingTypeService,
            IItemMasterDataService itemMasterDataService) 
            : base(appUserServices)
        {
            _itemMasterDataService = itemMasterDataService;
            _appUserServices = appUserServices;
            _itemTypeService = itemTypeService;
            _priceListService = priceListService;
            _itemGroupService = itemGroupService;
            _itemMasterDataStatusService = itemMasterDataStatusService;
            _manufacturerService = manufacturerService;
            _shippingTypeService = shippingTypeService;
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewItemMasterData)]
        public ActionResult Index()
        {
            return View();
        }
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddItemMasterData)]
        public ActionResult Form()
        {
            return View("AddForm");
        }

        #region GetData

        [HttpGet]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewItemMasterData)]
        public JsonResult ItemMasterDataIndex(ItemMasterDataFilter filter)
        {
            var result = _itemMasterDataService.Search(filter);
            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportItemMasterData)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(ItemMasterDataFilter filter)
        {
            var currentOffice = _appUserServices.GetById(CurrentUser.AppUserId)?.CurrentOffice;
            var exportResult = _itemMasterDataService.ExportDataToExcelFile(filter, Server, CurrentUser.AppUserId, currentOffice);
            return Json(new
            {
                data = new
                {
                    FileName = exportResult
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region LookUp
        //Item Type
        [HttpGet]
        public JsonResult GetAllItemType()
        {
            var result = _itemTypeService.GetAllItemType();

            return Json(new
            {
                data = result.Select(a => new
                {
                    Id = a.ItemTypeId,
                    a.Description
                })
            }, JsonRequestBehavior.AllowGet);
        }
        //Price List
        [HttpGet]
        public JsonResult GetAllPriceList()
        {
            var result = _priceListService.GetAllPriceList();

            return Json(new
            {
                data = result.Select(a => new
                {
                    Id = a.PriceListId,
                    a.Description
                })
            }, JsonRequestBehavior.AllowGet);
        }
        //Item Group
        [HttpGet]
        public JsonResult GetAllItemGroup()
        {
            var result = _itemGroupService.GetAllItemGroup();

            return Json(new
            {
                data = result.Select(a => new
                {
                    Id = a.ItemGroupId,
                    a.Description
                })
            }, JsonRequestBehavior.AllowGet);
        }
        //Manufacturer
        [HttpGet]
        public JsonResult GetAllManufacturer()
        {
            var result = _manufacturerService.GetAllManufacturer();

            return Json(new
            {
                data = result.Select(a => new
                {
                    Id = a.ManufacturerId,
                    a.Name
                })
            }, JsonRequestBehavior.AllowGet);
        }
        //Shipping Type
        [HttpGet]
        public JsonResult GetAllShippingType()
        {
            var result = _shippingTypeService.GetAllShippingType();

            return Json(new
            {
                data = result.Select(a => new
                {
                    Id = a.ShippingTypeId,
                    a.Name
                })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Enum Base JSON
        [HttpGet]
        public JsonResult GetAllStatus()
        {
            var result = _itemMasterDataStatusService.GetAllStatus();

            return Json(new
            {
                data = result.Select(a => new
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                })
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllTaxTypes()
        {
            var result = Enum.GetValues(typeof(TaxTypeEnums)).Cast <TaxTypeEnums>();

            return Json(new
            {
                data = result.Select(a => new
                {
                    Id=(int)a,
                    Name = a.GetEnumDescription(),
                })
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllVolumes()
        {
            var result = Enum.GetValues(typeof(VolumeEnums)).Cast<VolumeEnums>().ToList();

            return Json(new
            {
                data = result.Select(a => new
                {
                    Id = (int)a,
                    Name = a.GetEnumDescription()
                })
            }, JsonRequestBehavior.AllowGet) ;
        }
        [HttpGet]
        public JsonResult GetManageItemsBy()
        {
            var manageItems = Enum.GetValues(typeof(ManageItemByEnum)).Cast<ManageItemByEnum>();

            return Json(new
            {
                data = manageItems.Select(a => new
                {
                    Id = (int)a,
                    Description = a.GetEnumDescription()
                })
            },
         JsonRequestBehavior.AllowGet); ;
        }
        [HttpGet]
        public JsonResult GetIssuedMethod()
        {
            var issuedMethods = Enum.GetValues(typeof(IssuedMethodEnum)).Cast<IssuedMethodEnum>();

            return Json(new
            {
                data = issuedMethods.Select(a => new
                {
                    Id = (int)a,
                    Description = a.GetEnumDescription()
                })
            },
         JsonRequestBehavior.AllowGet); ;
        }
        #endregion

    }
}