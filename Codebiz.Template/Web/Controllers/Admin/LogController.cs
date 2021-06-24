using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class LogController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly ILogService _logService;
        private readonly ILogEventTitleService _logEventTitleService;

        private readonly IUnitOfWork _unitOfWork;

        public LogController(
            IAppUserServices appUserServices,
            ILogService logService,
            ILogEventTitleService logEventTitleService,

            IUnitOfWork unitOfWork
            ) : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _logService = logService;
            _logEventTitleService = logEventTitleService;
            _unitOfWork = unitOfWork;
        }



        #region View
        // GET: Department
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAuditViewLogs)]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public JsonResult SearchAuditLogs(AuditLogsFilter auditLogsFilter)
        {
            var data = new AuditLogsFilter
            {
                ActivityId = auditLogsFilter.ActivityId,
                User = auditLogsFilter.User,
                Thread = auditLogsFilter.Thread,
                Level = auditLogsFilter.Level,
                Message = auditLogsFilter.Message,
                Exception = auditLogsFilter.Exception,
                LogEventTitleId = auditLogsFilter.LogEventTitleId,
                CreatedOnFrom = auditLogsFilter.CreatedOnFrom,
                CreatedOnTo = auditLogsFilter.CreatedOnTo,
                Page = auditLogsFilter.Page,
                PageSize = auditLogsFilter.PageSize,
                SortColumn = string.IsNullOrEmpty(auditLogsFilter.SortColumn) ? "Date" : auditLogsFilter.SortColumn.Replace(" ", string.Empty),
                SortDirection = string.IsNullOrEmpty(auditLogsFilter.SortOrder) ? "desc" : auditLogsFilter.SortOrder.Replace(" ", string.Empty)
            };

            var result = _logService.Search(data);

            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = data.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }


        #endregion
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAuditViewLogs)]
        public ActionResult View(int id)
        {
            var log = _logService.GetById(id);

            if (log == null)
                return HttpNotFound($"Log Id [{id}] not found.");

            if (Request.IsAjaxRequest())
            {
                return PartialView("_DetailsModal", log);
            }

            return View(log);
        }

        public JsonResult GetEventTitles()
        {
            var statuses = Enum.GetValues(typeof(LogEventTitles)).Cast<LogEventTitles>();

            return Json(new
            {
                data = statuses.Select(a => new
                {
                    Id = (int)a,
                    Description = a.GetEnumDescription()
                })
            },
         JsonRequestBehavior.AllowGet); ;
        }

        public JsonResult GetAuditLogById(int id)
        {
            var model = _logService.GetById(id);
            var value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

          //GET DETAILS OF LOG 
        [HttpPost]
        public JsonResult GetAuditLogDetails(int id)
        {
            var result = _logService.GetById(id);
            return Json(new
            {
                Id = result.Id,
                ActivityId = result.ActivityId,
                Date = result.Date,
                User = result.User,
                Thread = result.Thread,
                Level = result.Level,
                Logger = result.Logger,
                Message = result.Message,
                Exception = result.Exception,
                LogEventTitle = result.LogEventTitle !=null ? result.LogEventTitle.Description : "",
            }, JsonRequestBehavior.AllowGet);
        }
    }
}