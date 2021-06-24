using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Model;
using Infrastructure.Repository.Common;
using Infrastructure.Utilities;
using Infrastructure.Utilities.QueryHelper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infrastructure.Services
{
    public interface IEmployeeService
    {
        void InsertOrUpdate(Employee entity, int appUserId);
        Employee GetById(int id);

        List<EmployeeDto> GetAllByOfficeAndPosition(int? officeId, string position);
        IPagedList<EmployeeDto> GetEmployeeLookup(LookUpFilter filter);
        IPagedList<EmployeeDto> GetAllEmployeesWithoutLicenceNo(LookUpFilter filter);
        EmployeeDto GetEmployeeByCode(string employeeNo, UrlHelper Url);
        EmployeeDto GetDetailsById(int id, UrlHelper Url);
        List<EmployeeDto> GetAllHeadOfficerByOfficeAndDepartmentId(int officeId, int departmentId, UrlHelper url);
        List<EmployeeDto> GetAllManagerByOffice(int officeId, UrlHelper url);

        EmployeeDto GetAreaManagerByOfficeAndDepartmentId(int officeId, int departmentId, UrlHelper url);
        EmployeeDto GetAreaHighestPositionByOfficeId(int officeId);
        EmployeeDto GetHeadOfficerDetailsById(int id, UrlHelper url);

        EmployeeDto GetManagerByDepartmentId(int? departmentId);

        #region Employee With License No

        IPagedList<EmployeeWithLicenseNoDTO> SearchEmployeeWithLicenseNo(EmployeeFilter filter);
        string ExportEmployeeWithLicenseNoDataToExcelFile(EmployeeFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);
        IPagedList<EmployeeWithLicenseNoDTO> GetEmployeeWithLicenseNoListLookup(LookUpFilter filter, UrlHelper url);
        bool CheckIfLicenseNoExist(string licenseNo, int employeeId);


        #endregion
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IContentFileService _contentFileService;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(
            IContentFileService contentFileService,
            IEmployeeRepository employeeRepository)
        {
            _contentFileService = contentFileService;
            _employeeRepository = employeeRepository;
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public void InsertOrUpdate(Employee entity, int appUserId)
        {
            if (entity.EmployeeId.Equals(0))
            {
                var now = DateTime.Now;

                entity.CreatedByAppUserId = appUserId;
                entity.CreatedOn = now;
            }
            else
            {
                entity.ModifiedByAppUserId = appUserId;
                entity.ModifiedOn = DateTime.Now;
            }
            _employeeRepository.InsertOrUpdate(entity);
        }

        //Get Highest Position List
        #region Get Highest Position List

        public List<EmployeeDto> GetAllHeadOfficerByOfficeAndDepartmentId(int officeId, int departmentId, UrlHelper url)
        {
            var datas = _employeeRepository.GetAllHeadOfficerByOfficeAndDepartmentId(officeId, departmentId);
            foreach (var data in datas)
            {
                var currentCroppedMemberPhoto = _contentFileService.GetCurrentCroppedEmployeePhotoById(data.EmployeeId);
                data.MemberPhotoThumbnailUrl = url.Action("CheckSource", "FileUpload", new
                {
                    area = "",
                    type = currentCroppedMemberPhoto.FileMimeType,
                    fileName = currentCroppedMemberPhoto.FileName,
                    folder = currentCroppedMemberPhoto.Folder
                });
            }

            return datas;
        }

        public List<EmployeeDto> GetAllManagerByOffice(int officeId, UrlHelper url)
        {
            var datas = _employeeRepository.GetAllManagerByOffice(officeId);
            foreach (var data in datas)
            {
                var currentCroppedMemberPhoto = _contentFileService.GetCurrentCroppedEmployeePhotoById(data.EmployeeId);
                data.MemberPhotoThumbnailUrl = url.Action("CheckSource", "FileUpload", new
                {
                    area = "",
                    type = currentCroppedMemberPhoto.FileMimeType,
                    fileName = currentCroppedMemberPhoto.FileName,
                    folder = currentCroppedMemberPhoto.Folder
                });
            }

            return datas;
        }

        #endregion

        //Get Highest Position Details
        #region Get Highest Position Details

        public EmployeeDto GetAreaHighestPositionByOfficeId(int officeId)
        {
            return _employeeRepository.GetAreaHighestPositionByOfficeId(officeId);
        }

        public EmployeeDto GetAreaManagerByOfficeAndDepartmentId(int officeId, int departmentId, UrlHelper url)
        {
            var data = _employeeRepository.GetAreaManagerByOfficeAndDepartmentId(officeId, departmentId);
            var currentCroppedMemberPhoto = data != null ? _contentFileService.GetCurrentCroppedEmployeePhotoById(data.EmployeeId) : null;

            if (data != null)
            {
                data.MemberPhotoThumbnailUrl = url.Action("CheckSource", "FileUpload", new
                {
                    area = "",
                    type = currentCroppedMemberPhoto.FileMimeType,
                    fileName = currentCroppedMemberPhoto.FileName,
                    folder = currentCroppedMemberPhoto.Folder
                });
            }
            return data;
        }

        public EmployeeDto GetHeadOfficerDetailsById(int id, UrlHelper url)
        {
            var dataDto = _employeeRepository.GetHeadOfficerDetailsById(id);
            var currentCroppedMemberPhoto = dataDto != null ? _contentFileService.GetCurrentCroppedEmployeePhotoById(dataDto.EmployeeId) : null;

            if (dataDto != null)
            {
                dataDto.MemberPhotoThumbnailUrl = url.Action("CheckSource", "FileUpload", new
                {
                    area = "",
                    type = currentCroppedMemberPhoto.FileMimeType,
                    fileName = currentCroppedMemberPhoto.FileName,
                    folder = currentCroppedMemberPhoto.Folder
                });
            }


            return dataDto;
        }

        #endregion

        public EmployeeDto GetManagerByDepartmentId(int? departmentId)
        {
            return _employeeRepository.GetManagerByDepartmentId(departmentId);
        }

        public EmployeeDto GetEmployeeByCode(string employeeNo, UrlHelper Url)
        {
            var data = _employeeRepository.GetAll.Where(a => a.EmployeeNo == employeeNo).FirstOrDefault();
            var currentCroppedAppUserPhoto = data != null ? _contentFileService.GetCurrentCroppedEmployeePhotoById(data.EmployeeId) : null;
            var dataDTO = new EmployeeDto();

            if (data != null)
            {
                dataDTO = new EmployeeDto
                {
                    EmployeeId = data.EmployeeId,
                    EmployeeNo = data.EmployeeNo,
                    FullName = data.LastName + ", " + data.FirstName + (data.MiddleName == null ? "" : " " + data.MiddleName),
                    PositionName = data.Position?.Name,
                    Office = data.Office?.Name,
                    DepartmentName = data.Department?.Name,
                    DivisionName = data.Division?.Name,
                    DivisionCategoryName = data.Position?.Divisions.FirstOrDefault(p => p.DivisionId == data.DivisionId && p.DivisionCategoryId != null) != null
                        ? data.Position?.Divisions.FirstOrDefault(p => p.DivisionId == data.DivisionId && p.DivisionCategoryId != null).DivisionCategory.Name
                        : "",
                    Email = data.Email,
                    BadgeNo = data.BadgeNo,
                    LicenseNo = data.LicenseNo,
                    MemberPhotoThumbnailUrl = Url.Action("CheckSource", "FileUpload", new
                    {
                        area = "",
                        type = currentCroppedAppUserPhoto.FileMimeType,
                        fileName = currentCroppedAppUserPhoto.FileName,
                        folder = currentCroppedAppUserPhoto.Folder
                    })
                };
            }

            return data == null ? null : dataDTO;
        }

        public List<EmployeeDto> GetAllByOfficeAndPosition(int? officeId, string position)
        {
            var data = _employeeRepository.GetAll.Where(p => p.IsActive);

            if (officeId != null)
            {
                data = data.Where(p => p.OfficeId == officeId);
            }

            if (!string.IsNullOrWhiteSpace(position))
            {
                data = data.Where(p => p.Position.Name.ToUpper() == position);
            }

            return data.Select(p => new EmployeeDto
            {
                EmployeeId = p.EmployeeId,
                EmployeeNo = p.EmployeeNo,
                FullName = p.LastName + ", " + p.FirstName + " " + p.MiddleName,
                PositionName = p.Position.Name,
                Office = p.Office.Name
            }).ToList();
        }

        public IPagedList<EmployeeDto> GetEmployeeLookup(LookUpFilter filter)
        {
            return _employeeRepository.GetAllEmployees(filter);
        }

        public IPagedList<EmployeeDto> GetAllEmployeesWithoutLicenceNo(LookUpFilter filter)
        {
            return _employeeRepository.GetAllEmployeesWithoutLicenceNo(filter);
        }

        #region Look ups

        public IPagedList<EmployeeWithLicenseNoDTO> GetEmployeeWithLicenseNoListLookup(LookUpFilter filter, UrlHelper url)
        {
            var data = _employeeRepository.GetAll.Where(x => x.LicenseNo != null);

            if (!string.IsNullOrWhiteSpace(filter.Searcher))
            {
                data = data.Where(x => (x.LastName + " " + x.FirstName + " " + x.MiddleName + " " + x.Suffix).Contains(filter.Searcher) || x.LicenseNo.Contains(filter.Searcher));
            }
            filter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new EmployeeWithLicenseNoDTO
            {
                EmployeeId = a.EmployeeId,
                ExpirationDate = a.LicenseExpiryDate,
                LicenseNo = a.LicenseNo,
                Name = a.LastName + " " + a.FirstName + " " + a.MiddleName + " " + a.Suffix,
                Restriction = a.LicenseRestriction
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }


        #endregion

        public EmployeeDto GetDetailsById(int id, UrlHelper Url)
        {
            var data = _employeeRepository.GetById(id);
            var currentCroppedAppUserPhoto = data != null ? _contentFileService.GetCurrentCroppedEmployeePhotoById(data.EmployeeId) : null;

            var dataDTO = new EmployeeDto
            {
                EmployeeId = data.EmployeeId,
                EmployeeNo = data.EmployeeNo,
                FullName = data.LastName + ", " + data.FirstName + (data.MiddleName == null ? "" : " " + data.MiddleName),
                PositionName = data.Position.Name,
                Office = data.Office?.Name,
                DepartmentName = data.Department?.Name,
                DivisionCategoryName = data.Division?.Name,
                Email = data.Email,
                BadgeNo = data.BadgeNo,
                ExpiryDate = data.LicenseExpiryDate,
                LicenseNo = data.LicenseNo,
                Restriction = data.LicenseRestriction,
                MemberPhotoThumbnailUrl = Url.Action("CheckSource", "FileUpload", new
                {
                    area = "",
                    type = currentCroppedAppUserPhoto.FileMimeType,
                    fileName = currentCroppedAppUserPhoto.FileName,
                    folder = currentCroppedAppUserPhoto.Folder
                })
            };

            return dataDTO;
        }

        #region Employee With License No

        public IPagedList<EmployeeWithLicenseNoDTO> SearchEmployeeWithLicenseNo(EmployeeFilter filter)
        {
            return _employeeRepository.SearchEmployeeWithLicenseNo(filter);
        }

        public string ExportEmployeeWithLicenseNoDataToExcelFile(EmployeeFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            var data = _employeeRepository.GetEmployeeWithLicenseNoDataForExportingToExcel(filter).ToList();
            var fileName = ExportToExcelFileHelper.GenerateExcelFile(
                ExportToExcelFileHelper.CreateObjectBy(typeof(EmployeeWithLicenseNoDTO)),
                data,
                "Employees With License No_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
                server,
                "Employees With License No List",
                currentAppUserId, currentOffice);

            return fileName;
        }

        public bool CheckIfLicenseNoExist(string licenseNo, int employeeId)
        {
            return _employeeRepository.CheckIfLicenseNoExist(licenseNo, employeeId);
        }

  

        #endregion
    }
}
