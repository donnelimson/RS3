using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Codebiz.Domain.ERP.Model.Enums.VEHICLES;
using Infrastructure.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers.User
{
    public class EmployeesController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(
            IAppUserServices appUserServices,
            IEmployeeService employeeService
            ) : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _employeeService = employeeService;
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }



        #region Get Employee Details

        public JsonResult GetEmployeeByCode(string code)
        {
            var result = _employeeService.GetEmployeeByCode(code, Url);
            return Json(new
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeDetails(int employeeId)
        {
            return Json(new
            {
                data = _employeeService.GetDetailsById(employeeId, Url),
            },
            JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpGet]
        public JsonResult GetAreaHighestPositionByOfficeId(int officeId)
        {
            return Json(new
            {
                data = _employeeService.GetAreaHighestPositionByOfficeId(officeId)
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetHeadOfficerDetailsById(int id)
        {
            return Json(new
            {
                data = _employeeService.GetHeadOfficerDetailsById(id, Url)
            }, JsonRequestBehavior.AllowGet);
        }

        #region Lookup

        public JsonResult GetEmployeeLookUp(LookUpFilter filter)
        {
            var result = _employeeService.GetEmployeeLookup(filter);
            return Json(new
            {
                data = result,
                totalRecordCount = filter.TotalRecordCount,
                filteredRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllEmployeesWithoutLicenceNo(LookUpFilter filter)
        {
            var result = _employeeService.GetAllEmployeesWithoutLicenceNo(filter);
            return Json(new
            {
                data = result,
                totalRecordCount = filter.TotalRecordCount,
                filteredRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRestrictionCode()
        {
            var statuses = Enum.GetValues(typeof(RestrictionCodeEnum)).Cast<RestrictionCodeEnum>();

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

        public JsonResult GetListLookUp(LookUpFilter filter)
        {
            var result = _employeeService.GetEmployeeWithLicenseNoListLookup(filter, Url);
            return Json(new
            {
                data = result,
                totalRecordCount = filter.TotalRecordCount,
                filteredRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}