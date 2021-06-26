using Codebiz.Domain.Common.Model.Filter.RS3;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.Common;
using System;
using System.Collections.Generic;
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
            return View();
        }
        #region JSON
        public JsonResult Search(TicketFilter filter)
        {
            return Json(new { result = _ticketService.GetAllOpenTickets(filter), totalRecordCount = filter.FilteredRecordCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}