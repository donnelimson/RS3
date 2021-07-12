using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.RS3;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.Filter.RS3;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.Common;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            if (CurrentUser.RoleId == 4) //client
            {
                return View("ViewTicket");
            }
            return View();
        }
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanCreateTicket)]

        public ActionResult Create()
        {
       
            return View("ViewTicket");
        }
        public ActionResult ViewTicket(int? id)
        {
            var isClient = CurrentUser.RoleId == 4;
            if (isClient)
            {
                var canview = _ticketService.CheckIfClientOwnTicket(id.Value, CurrentUser.AppUserId);
                if (!canview)
                {
                    return RedirectToAction("Create");
                }
            }
            return View("ViewTicket",id);
        }
        #region JSON
        public JsonResult Search(TicketFilter filter)
        {
            filter.TechnicianId = filter.MyTicketsOnly ? CurrentUser.AppUserId : (int?)null;
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
                var ticket = _ticketService.AddOrUpdate(viewModel, CurrentUser.AppUserId, CurrentUser.RoleId==4);
                _unitOfWork.SaveChanges();
                if (!string.IsNullOrEmpty(viewModel.ClientEmail) && CurrentUser.RoleId!=4)
                {
                    SendEmail(viewModel.ClientEmail, viewModel.Id==0? "The ticket has been created with ticket no: " + ticket.TicketNo:
                        "Ticket: "+ ticket.TicketNo + " has been updated", ticket.Title);
                }
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
        public JsonResult TakeTicket(int id)
        {
            AjaxResult ajaxResult = new AjaxResult();
            try
            {
                _ticketService.TakeTicket(id, CurrentUser.AppUserId, CurrentUser.Username);
                _unitOfWork.SaveChanges();
                ajaxResult.Message = "Ticket has been successfully taken this ticket!";
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}].");
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to take ticket! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), LogEventTitles.TicketTaken);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to take ticket! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message), LogEventTitles.TicketTaken);
            }
            return Json(new
            {
                ajaxResult.Success,
                ajaxResult.Message
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubmitComment(CommentAddDTO model)
        {
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult.LogEventTitle = model.IsResolved ? LogEventTitles.TicketCommentedAndResolved : LogEventTitles.TicketCommented;
            try
            {
                _ticketService.SubmitComment(model, CurrentUser.AppUserId, CurrentUser.Username);
                _unitOfWork.SaveChanges();
                if (!string.IsNullOrEmpty(model.Email) && !model.IsInternal)
                {
                    SendEmail(model.Email,model.Comment,model.Title);
                }
              
                ajaxResult.Message = "You have successfully submitted a comment!";
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}].");
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to comment on ticket! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), ajaxResult.LogEventTitle);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to comment on ticket! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message), ajaxResult.LogEventTitle);
            }
            return Json(new
            {
                ajaxResult.Success,
                ajaxResult.Message
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ResolveOrReopenTicket(int id)
        {
            AjaxResult ajaxResult = new AjaxResult();
            ajaxResult.LogEventTitle = LogEventTitles.TicketResolved; //by default
            var action = "resolve";
            try
            {
                var isResolved = _ticketService.ResolveOrReopenTicket(id, CurrentUser.AppUserId, CurrentUser.Username);
                ajaxResult.LogEventTitle = isResolved ? LogEventTitles.TicketResolved : LogEventTitles.TicketReopened;
                action = isResolved ? "resolve" : "reopen";
                _unitOfWork.SaveChanges();
                ajaxResult.Message = "You have successfully "+action+" a ticket!";
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}].");
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to  " + action + " ticket! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), ajaxResult.LogEventTitle);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to " + action + "  ticket! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
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
            var isClient = CurrentUser.RoleId == 4;
            var data = _ticketService.GetTicketDetailsById(id, Url);
            data.CanBeTaken = data.TechnicianId != CurrentUser.AppUserId;
            data.IsClient = isClient;//client
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMyTickets(LookUpFilter filter)
        {
            return Json(new { result = _ticketService.GetMyTickets(filter, CurrentUser.AppUserId) });
        }
        public JsonResult CheckIfClient()
        {
            return Json(new { result = CurrentUser.RoleId == 4 },JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region EmailSender
        private static void SendEmail(string toEmail,string mail, string subject)
        {
            try
            {
                //_displayName = _configSettingService.GetStringValueById((int)ConfigurationSettings.SmtpDisplayName);
                //_emailClientUsername = _configSettingService.GetStringValueById((int)ConfigurationSettings.SmtpUsername);
                //_emailClientPassword = _configSettingService.GetStringValueById((int)ConfigurationSettings.SmtpPassword);
                //_smtpHost = _configSettingService.GetStringValueById((int)ConfigurationSettings.SmtpHost);
                //_smtpPort = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.SmtpPort);
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ConfigurationManager.AppSettings["SmtpUsername"].ToString());
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = mail;
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"].ToString(); //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUsername"].ToString(), ConfigurationManager.AppSettings["SmtpPassword"].ToString());
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex) { 
            }
        }
        #endregion
    }
}