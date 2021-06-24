using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using Domain.Context;
using Infrastructure.Utilities.QueryHelper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repository.Common
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Employee GetById(int id);
        IPagedList<EmployeeDto> GetAllEmployees(LookUpFilter filter);
        IPagedList<EmployeeDto> GetAllEmployeesWithoutLicenceNo(LookUpFilter filter);
        List<EmployeeDto> GetAllHeadOfficerByOfficeAndDepartmentId(int officeId, int departmentId);
        List<EmployeeDto> GetAllManagerByOffice(int officeId);

        EmployeeDto GetAreaManagerByOfficeAndDepartmentId(int officeId, int departmentId);
        EmployeeDto GetAreaHighestPositionByOfficeId(int officeId);
        EmployeeDto GetHeadOfficerDetailsById(int id);

        EmployeeDto GetManagerByDepartmentId(int? departmentId);

        IPagedList<EmployeeWithLicenseNoDTO> SearchEmployeeWithLicenseNo(EmployeeFilter filter);
        IEnumerable<EmployeeWithLicenseNoDTO> GetEmployeeWithLicenseNoDataForExportingToExcel(EmployeeFilter filter);
        bool CheckIfLicenseNoExist(string licenseNo, int employeeId);
       
    }
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppCommonContext context) : base(context)
        {
        }
        public override void InsertOrUpdate(Employee entity)
        {
            if (entity.EmployeeId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public Employee GetById(int id)
        {
            return GetAll.Where(a => a.EmployeeId == id).FirstOrDefault();
        }

        public IPagedList<EmployeeDto> GetAllEmployees(LookUpFilter filter)
        {
            var data = this.GetAll.Where(a => a.IsActive && a.EmployeeNo != null);

            if (!string.IsNullOrWhiteSpace(filter.Searcher))
            {
                data = data.Where(x => (x.LastName + ", " + x.FirstName + " " + x.MiddleName).Contains(filter.Searcher) ||
                x.Position.Name.Contains(filter.Searcher) ||
                x.EmployeeNo.Contains(filter.Searcher));
            }

            if (filter.DepartmentId != null)
            {
                data = data.Where(x => x.DepartmentId == filter.DepartmentId);
            }

            if (filter.DivisionId != null)
            {
                data = data.Where(x => x.DivisionId == filter.DivisionId);
            }

            if (filter.DivisionCategoryId != null)
            {
                data = data.Where(x => x.Position.Divisions
                    .Any(a => a.DepartmentId == filter.DepartmentId &&
                              a.DivisionId == filter.DivisionId &&
                              a.DivisionCategoryId == filter.DivisionCategoryId));
            }

            filter.FilteredRecordCount = data.Count();
            filter.TotalRecordCount = data.Count();

            var dataDTO = data.Select(a => new EmployeeDto
            {
                Id = a.EmployeeId,
                EmployeeId = a.EmployeeId,
                EmployeeNo = a.EmployeeNo,
                FullName = a.LastName + ", " + a.FirstName + (a.MiddleName == null ? "" : " " + a.MiddleName),
                PositionName = a.Position != null ? a.Position.Name : null,
                Office = a.Office != null ? a.Office.Name : null
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        public IPagedList<EmployeeDto> GetAllEmployeesWithoutLicenceNo(LookUpFilter filter)
        {
            var data = this.GetAll.Where(a => a.IsActive && a.EmployeeNo != null && a.LicenseNo == null);

            if (!string.IsNullOrWhiteSpace(filter.Searcher))
            {
                data = data.Where(x => (x.LastName + ", " + x.FirstName + " " + x.MiddleName).Contains(filter.Searcher) ||
                x.Position.Name.Contains(filter.Searcher) ||
                x.EmployeeNo.Contains(filter.Searcher));
            }

            if (filter.DepartmentId != null)
            {
                data = data.Where(x => x.DepartmentId == filter.DepartmentId);
            }

            if (filter.DivisionId != null)
            {
                data = data.Where(x => x.DivisionId == filter.DivisionId);
            }

            if (filter.DivisionCategoryId != null)
            {
                data = data.Where(x => x.Position.Divisions
                    .Any(a => a.DepartmentId == filter.DepartmentId &&
                              a.DivisionId == filter.DivisionId &&
                              a.DivisionCategoryId == filter.DivisionCategoryId));
            }

            filter.FilteredRecordCount = data.Count();
            filter.TotalRecordCount = data.Count();

            var dataDTO = data.Select(a => new EmployeeDto
            {
                Id = a.EmployeeId,
                EmployeeId = a.EmployeeId,
                EmployeeNo = a.EmployeeNo,
                FullName = a.LastName + ", " + a.FirstName + (a.MiddleName == null ? "" : " " + a.MiddleName),
                PositionName = a.Position.Name
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        #region Get Highest Position List

        public List<EmployeeDto> GetAllHeadOfficerByOfficeAndDepartmentId(int officeId, int departmentId)
        {
            var data = GetAll.Where(a => a.OfficeId == officeId
                   && a.IsActive
                   && a.Position.IsHeadOfficer
                   && a.Department.Details.Any(p => p.PositionId == a.PositionId && p.DivisionId != null && p.DivisionCategoryId != null));

            if (departmentId > 0)
            {
                data = data.Where(a => a.DepartmentId == departmentId);
            }

            return data
                .Select(a => new EmployeeDto
                {
                    Id = a.EmployeeId,
                    EmployeeId = a.EmployeeId,
                    EmployeeNo = a.EmployeeNo,
                    FullName = (a.FirstName + " " + (a.MiddleName != null ? a.MiddleName.Substring(0, 1) + "." : "") + a.LastName).ToUpper(),
                    PositionName = a.Position.Name,
                    PositionId = a.PositionId,
                    Office = a.Office.Name,
                    DepartmentName = a.Department == null ? "" : a.Department.Name,
                    DepartmentId = a.DepartmentId,
                    DivisionId = a.DivisionId,
                    DivisionName = a.Division == null ? "" : a.Division.Name,
                    DivisionCategoryId = a.Position.Divisions.FirstOrDefault(p => p.DivisionId == a.DivisionId && p.DivisionCategoryId != null) != null
                        ? a.Position.Divisions.FirstOrDefault(p => p.DivisionId == a.DivisionId && p.DivisionCategoryId != null).DivisionCategoryId
                        : (int?)null,
                    DivisionCategoryName = a.Position.Divisions.FirstOrDefault(p => p.DivisionId == a.DivisionId && p.DivisionCategoryId != null) != null
                        ? a.Position.Divisions.FirstOrDefault(p => p.DivisionId == a.DivisionId && p.DivisionCategoryId != null).DivisionCategory.Name
                        : ""
                }).ToList();
        }

        public List<EmployeeDto> GetAllManagerByOffice(int officeId)
        {
            var data = GetAll.Where(a => a.IsActive
                   && a.Position.IsManager
                   && a.Department.Details.Any(p => p.DivisionId != null && p.DivisionCategoryId != null));

            if (officeId > 0)
            {
                data = data.Where(a => a.OfficeId == officeId);
            }

            return data
                .Select(a => new EmployeeDto
                {
                    Id = a.EmployeeId,
                    EmployeeId = a.EmployeeId,
                    EmployeeNo = a.EmployeeNo,
                    FullName = (a.FirstName + " " + (a.MiddleName != null ? a.MiddleName.Substring(0, 1) + "." : "") + a.LastName).ToUpper(),
                    PositionName = a.Position.Name,
                    PositionId = a.PositionId,
                    Office = a.Office.Name,
                    DepartmentName = a.Department == null ? "" : a.Department.Name,
                    DepartmentId = a.DepartmentId,
                    DivisionId = a.DivisionId,
                    DivisionName = a.Division == null ? "" : a.Division.Name,
                    IsMainOffice = a.Office.IsMainOffice
                }).ToList();
        }

        #endregion

        #region Get Highest Position Details

        public EmployeeDto GetAreaManagerByOfficeAndDepartmentId(int officeId, int departmentId)
        {
            var data = GetAll.Where(a => a.OfficeId == officeId
                   && a.IsActive
                   && a.Position.IsManager);

            if (departmentId > 0)
            {
                data = data.Where(a => a.DepartmentId == departmentId);
            }

            return data
                .Select(a => new EmployeeDto
                {
                    Id = a.EmployeeId,
                    EmployeeId = a.EmployeeId,
                    FullName = (a.FirstName + " " + (a.MiddleName != null ? a.MiddleName.Substring(0, 1) + "." : "") + a.LastName).ToUpper(),
                    PositionName = a.Position.Name,
                    PositionId = a.PositionId,
                    Office = a.Office.Name,
                    DivisionId = a.DivisionId,
                    DivisionName = a.Division == null ? "" : a.Division.Name,
                    IsMainOffice = a.Office.IsMainOffice

                }).FirstOrDefault();
        }

        public EmployeeDto GetAreaHighestPositionByOfficeId(int officeId)
        {
            return GetAll.Where(a => a.OfficeId == officeId && a.PositionId == 5 && a.IsActive)
                .Select(a => new EmployeeDto
                {
                    Id = a.EmployeeId,
                    EmployeeId = a.EmployeeId,
                    FullName = (a.FirstName + " " + (a.MiddleName != null ? a.MiddleName.Substring(0, 1) + "." : "") + a.LastName).ToUpper(),
                    PositionName = a.Position.Name,
                    PositionId = a.PositionId
                }).FirstOrDefault();
        }

        public EmployeeDto GetHeadOfficerDetailsById(int id)
        {
            var employee = GetAll.FirstOrDefault(p => p.EmployeeId == id);

            return employee == null ? null :
                new EmployeeDto
                {
                    Id = employee.EmployeeId,
                    EmployeeId = employee.EmployeeId,
                    FullName = (employee.FirstName + " " + employee.LastName).ToUpper(),
                    PositionName = employee.Position.Name,
                    PositionId = employee.PositionId,
                    Office = employee.Office != null ? employee.Office.Name + "(" + employee.Department.Name + ")" : "",
                };
        }

        #endregion

        public EmployeeDto GetManagerByDepartmentId(int? departmentId)
        {
            return GetAll.Where(a => a.Position.IsManager && a.DepartmentId == departmentId && a.IsActive)
                .Select(a => new EmployeeDto
                {
                    EmployeeId = a.EmployeeId,
                    PositionId = a.PositionId,
                    FullName = (a.FirstName + " " + (a.MiddleName != null ? a.MiddleName.Substring(0, 1) + "." : "") + a.LastName).ToUpper(),
                    PositionName = a.Position.Name

                }).FirstOrDefault();
        }

        #region Employees With License No

        private IQueryable<Employee> GetEmployeeWithLicenseNoData(EmployeeFilter filter)
        {
            var data = GetAll.Where(a => a.LicenseNo != null);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filter.Name = filter.Name.Trim();
                data = data.Where(a => (a.LastName + ", " + a.FirstName + " " + a.MiddleName + " " + a.Suffix)
                  .Trim().Contains(filter.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.EmployeeNo))
            {
                filter.Name = filter.EmployeeNo.Trim();
                data = data.Where(a => (a.EmployeeNo).Trim().Contains(filter.EmployeeNo));
            }

            if (!string.IsNullOrWhiteSpace(filter.LicenseNo))
            {
                filter.LicenseNo = filter.LicenseNo.Trim();
                data = data.Where(a => a.LicenseNo.Contains(filter.LicenseNo));
            }

            if (filter.ExpDate != null)
            {
                data = data.Where(a => DbFunctions.TruncateTime(a.LicenseExpiryDate) == filter.ExpDate);
            }

            return data;
        }

        public IEnumerable<EmployeeWithLicenseNoDTO> GetEmployeeWithLicenseNoDataForExportingToExcel(EmployeeFilter filter)
        {
            var data = GetEmployeeWithLicenseNoData(filter);

            var dataDTO = data.ToList().Select(a => new EmployeeWithLicenseNoDTO
            {
                Name = a.LastName + ", " + a.FirstName + " " + a.MiddleName + " " + a.Suffix,
                EmployeeNo = a.EmployeeNo,
                LicenseNo = a.LicenseNo,
                ExpiDate = a.LicenseExpiryDate == null ? "" : a.LicenseExpiryDate.Value.ToString("MM/dd/yyyy"),
                ExpirationDate = a.LicenseExpiryDate,
                Restriction = a.LicenseRestriction,
                IsActive = a.IsActive,
                ActionBy = a.ModifiedByAppUser != null ?
                a.ModifiedByAppUser.Employee.LastName + " " + a.ModifiedByAppUser.Employee.FirstName :
                a.CreatedByAppUser.Employee.LastName + " " + a.CreatedByAppUser.Employee.FirstName,
                ActionOn = a.ModifiedOn != null ? a.ModifiedOn : a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO.AsQueryable(), filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        public IQueryable<Employee> GetallByEmployeeId(int employeeId)
        {
            return GetAll.Where(x => x.EmployeeId == employeeId);
        }

        public IPagedList<EmployeeWithLicenseNoDTO> SearchEmployeeWithLicenseNo(EmployeeFilter filter)
        {
            var data = GetEmployeeWithLicenseNoData(filter);

            var dateCanRenew = DateTime.Now.AddMonths(-1);
            var date = DateTime.Now.AddYears(1);

            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new EmployeeWithLicenseNoDTO
            {
                EmployeeId = a.EmployeeId,
                Name = a.LastName + ", " + a.FirstName + " " + a.MiddleName + " " + a.Suffix,
                EmployeeNo = a.EmployeeNo,
                LicenseNo = a.LicenseNo,
                ExpirationDate = a.LicenseExpiryDate,
                Restriction = a.LicenseRestriction,
                IsActive = a.IsActive,
                ActionBy = a.ModifiedByAppUser != null ?
                            a.ModifiedByAppUser.Employee.LastName + " " + a.ModifiedByAppUser.Employee.FirstName :
                            a.CreatedByAppUser.Employee.LastName + " " + a.CreatedByAppUser.Employee.FirstName,
                ActionOn = a.ModifiedOn != null ? a.ModifiedOn : a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        public bool CheckIfLicenseNoExist(string licenseNo, int employeeId)
        {
            return GetAll.Where(a => a.LicenseNo != null).Any(x => x.LicenseNo == licenseNo && x.EmployeeId != employeeId);
        }

        #endregion
    }
}
