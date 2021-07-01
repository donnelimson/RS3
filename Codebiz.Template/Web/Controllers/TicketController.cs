using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.RS3;
using Codebiz.Domain.Common.Model.Filter.RS3;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.Common;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class TicketController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITicketService _ticketService;
        public TicketController(IAppUserServices appUserServices,
            IUnitOfWork unitOfWork,
             ITicketService ticketService) : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _unitOfWork = unitOfWork;
            _ticketService = ticketService;
        }
        // GET: Ticket
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewTicketList)]

        public ActionResult Index()
        {
            return View();
        }
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanCreateTicket)]

        public ActionResult Create()
        {
            return View("ViewTicket");
        }
        public ActionResult ViewTicket(int? id)
        {
            return View("ViewTicket",id);
        }
        #region JSON
        public JsonResult Search(TicketFilter filter)
        {
            return Json(new { result = _ticketService.GetAllOpenTickets(filter), totalRecordCount = filter.FilteredRecordCount }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddOrUpdate(TicketAddOrUpdateDTO viewModel)
        {
            AjaxResult ajaxResult = new AjaxResult();
            var action = viewModel.Id == 0 ? "create" : "update";
            ajaxResult.LogEventTitle = viewModel.Id.Equals(0) ?
                LogEventTitles.TicketCreated : LogEventTitles.TicketUpdated;
            try
            {
                if (viewModel.Id == 0)
                {
                    viewModel.LogComment = "Ticket is created by " + CurrentUser.Username;
                }
                else if(viewModel.Id != 0 && !viewModel.IsReply)
                {
                    viewModel.LogComment = "Ticket is updated by " + CurrentUser.Username;
                }
                else
                {
                    viewModel.LogComment = "User " + CurrentUser.Username +" commented on this ticket";
                }
                _ticketService.AddOrUpdate(viewModel, CurrentUser.AppUserId);
                _unitOfWork.SaveChanges();
                ajaxResult.Message = "Ticket has been successfully " + action + "d" + "!";
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]." + JsonConvert.SerializeObject(viewModel), ajaxResult.LogEventTitle, "", "", JsonConvert.SerializeObject(viewModel));
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {action} ticket! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), ajaxResult.LogEventTitle);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {action} ticket! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message), ajaxResult.LogEventTitle);
            }
            return Json(new
            {
                ajaxResult.Success,
                ajaxResult.Message
            }, JsonRequestBehavior.AllowGet);
        }
        public  JsonResult GetTicketDetailsById(int id)
        {
            return Json(new { result = _ticketService.GetTicketDetailsById(id, Url) }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}