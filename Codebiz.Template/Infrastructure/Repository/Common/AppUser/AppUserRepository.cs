using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DTOs.RS3;
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

namespace Infrastructure.Repository
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppCommonContext context) : base(context)
        {
            
        }

        public override void InsertOrUpdate(AppUser entity)
        {
            if (entity.AppUserId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
        public AppUser GetAppUserByActivationUrlParam(string activationUrlParam)
        {
            return GetAll.Where(x => x.ActivationUrlParam == activationUrlParam).FirstOrDefault();
        }
        public AppUser GetById(int id)
        {
            return GetAll.FirstOrDefault(a => a.AppUserId == id);
        }

        public AppUser GetByUserName(string userName)
        {
            return GetAll.FirstOrDefault(a => a.Username == userName);
        }

        public AppUser GetActiveById(int id)
        {
            return GetAll.FirstOrDefault(a => a.AppUserId == id && a.IsActive);
        }

        public bool IsUsernameExists(string username, int appUserId)
        {
            return GetAll.Any(a => a.Username == username && a.AppUserId != appUserId);
        }
        public bool IsUsernameExists(string username)
        {
            return GetAll.Any(a => a.Username == username);
        }

        public bool IsEmailExists(string email, int appUserId)
        {
            return GetAll.Any(a => a.Employee.Email == email && a.AppUserId != appUserId);
        }
        public bool IsEmailExists(string email)
        {
            return GetAll.Any(a => a.Email == email);
        }

        public IPagedList<AppUserDTO> SearchAppUser(AppUserFilter filter)
        {
            var data = GetData(filter);

            filter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new AppUserDTO
            {
                AppUserId = a.AppUserId,
                Name =a.LastName + ", " + a.FirstName + (a.MiddleName == null ? "" : " " + a.MiddleName) + (a.Suffix == null ? "" : " " + a.Suffix),
                Username = a.Username,
                Email = a.Email,
                Position = a.Employee != null ? a.Employee.Position.Name : "",
                Office = a.Employee != null ? a.Employee.Office.Name : "",
                Department = a.Employee != null ? a.Employee.Department.Name : "",
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.FirstName + ", " + a.CreatedByAppUser.LastName,
                CreatedDate = a.CreatedOn,
                Role = a.Role.Description,
                //IsManager = a.Employee.PositionId == null ? false : a.Employee.Position.IsManager
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        public IEnumerable<AppUserToExcel> GetDataForExportingToExcel(AppUserFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.Select(a => new AppUserToExcel
            {
                Name = a.Employee != null
                    ? a.Employee.LastName + ", " + a.Employee.FirstName + (a.Employee.MiddleName == null ? "" : " " + a.Employee.MiddleName) + (a.Employee.Suffix == null ? "" : " " + a.Employee.Suffix)
                    : a.Username,
                Username = a.Username,
                Email = a.Employee != null ? a.Employee.Email : "",
                Position = a.Employee != null ? a.Employee.Position.Name : "",
                Office = a.Employee != null ? a.Employee.Office.Name : "",
                Department = a.Employee != null ? a.Employee.Department.Name : "",
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        public IPagedList<AppUserSearchDTO> SearchAppUserForLookup(LookUpFilter filter, bool isDriver)
        {
            var employeesWithLicense = this.GetAll
                .Where(a => a.EmployeeId != null)
                .Where(a => a.Employee.LicenseNo != null && DbFunctions.TruncateTime(a.Employee.LicenseExpiryDate) >= DbFunctions.TruncateTime(DateTime.Now))
                .Select(d => d.EmployeeId).ToList();

            var data = isDriver
                ? this.GetAll.Where(x => x.IsActive && x.EmployeeId != null && !employeesWithLicense.Contains(x.AppUserId))
                : this.GetAll.Where(x => x.IsActive && x.EmployeeId != null);

            if (!string.IsNullOrEmpty(filter.Searcher))
            {
                data = data.Where(x => x.Employee.Position.Name.Contains(filter.Searcher) || 
                                       x.Employee.Office.Name.Contains(filter.Searcher) || 
                                       (x.Employee.LastName + ", " + x.Employee.FirstName + " " + x.Employee.MiddleName).Contains(filter.Searcher) || 
                                       (x.Employee.FirstName + " " + x.Employee.LastName).Contains(filter.Searcher));
            }

            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new AppUserSearchDTO
            {
                Id = a.AppUserId,
                FullName = a.Employee.LastName + ", " + a.Employee.FirstName + " " + a.Employee.MiddleName,
                Position = a.Employee.Position.Name,
                Office = a.Employee.Office.Name,
                Department = a.Employee.Department.Name,
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        private IQueryable<AppUser> GetData(AppUserFilter filter)
        {
            var data = GetAll;

            if (!string.IsNullOrWhiteSpace(filter.Username))
            {
                filter.Username = filter.Username.Trim();

                data = data.Where(p => p.Employee != null).Where(a => a.Username.Contains(filter.Username));
            }

            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                filter.CreatedBy = filter.CreatedBy.Trim();
                data = data.Where(a => (a.CreatedByAppUser.Employee.FirstName + " "
                + a.CreatedByAppUser.Employee.LastName)
                .Trim().Contains(filter.CreatedBy));
            }

            if (filter.CreatedOnFrom != null && filter.CreatedOnTo != null)
            {
                data = data.Where(a => DbFunctions.TruncateTime(a.CreatedOn) >= filter.CreatedOnFrom && DbFunctions.TruncateTime(a.CreatedOn) <= filter.CreatedOnTo);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filter.Name = filter.Name.Trim();

                data = data.Where(p => p.Employee != null)
                    .Where(a => (a.Employee.LastName + ", " +
                                a.Employee.FirstName +
                                (a.Employee.MiddleName != null ? " " + a.Employee.MiddleName : "") + 
                                (a.Employee.Suffix != null ? " " + a.Employee.Suffix : "")).Trim().Contains(filter.Name));
            }


            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                filter.Email = filter.Email.Trim();

                data = data.Where(p => p.Employee != null).Where(a => a.Employee.Email.Contains(filter.Email));
            }

            if (!string.IsNullOrWhiteSpace(filter.Office))
            {
                filter.Office = filter.Office.Trim();

                data = data.Where(p => p.Employee != null).Where(a => a.Employee.Office.Name.Contains(filter.Office));
            }

            if (!string.IsNullOrWhiteSpace(filter.Department))
            {
                filter.Department = filter.Department.Trim();

                data = data.Where(p => p.Employee != null).Where(a => a.Employee.Department.Name.Contains(filter.Department));
            }

            if (!string.IsNullOrWhiteSpace(filter.Position))
            {
                filter.Position = filter.Position.Trim();

                data = data.Where(p => p.Employee != null).Where(a => a.Employee.Position.Name.Contains(filter.Position));
            }

            return data;
        }

        public List<AppUser> GetAllUserByDepartmentAndDivision(int appUSerId, int? officeId, int? departmentId, int? divisionId, int? divisionCategoryId)
        {
            var result = GetAll
                 .Where(x => x.Employee.OfficeId == officeId &&
                             x.AppUserId != appUSerId &&
                             x.IsActive);

            if (departmentId != null)
            {
                result = result.Where(x => x.Employee.DepartmentId == departmentId);
            }

            if (divisionId != null)
            {
                result = result.Where(x => x.Employee.DivisionId == divisionId);
            }

            if (divisionCategoryId != null)
            {
                result = result.Where(x => x.Employee.Position.Divisions
                    .Any(a => a.DepartmentId == departmentId &&
                              a.DivisionId == divisionId &&
                              a.DivisionCategoryId == divisionCategoryId));
            }

            return result.ToList();
        }
        public IPagedList<AppuserDTOForCFL> GetAllAppuserForCFL(LookUpFilter filter, int? roleId)
        {
            var data = GetAll.Where(x => x.IsActive);
            if (!string.IsNullOrEmpty(filter.Searcher))
            {
                filter.Searcher = filter.Searcher.Trim();

                data = data.Where(a => (a.LastName + ", " +
                                a.FirstName +
                                (a.MiddleName != null ? " " + a.MiddleName : "") +
                                (a.Suffix != null ? " " + a.Suffix : "")).Trim().Contains(filter.Searcher) || a.Email.Contains(filter.Searcher));
            }
            if (roleId != null)
            {
                data = data.Where(x => x.RoleId == roleId);
            }
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new AppuserDTOForCFL
            {
                Email = a.Email,
                Id =a.AppUserId,
                Name = a.LastName + ", " +
                                a.FirstName +
                                (a.MiddleName != null ? " " + a.MiddleName : "") +
                                (a.Suffix != null ? " " + a.Suffix : "")
            });
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        public List<string> GetEmailsOfAdministrators()
        {
            return GetAll.Where(x => x.RoleId == 1).Select(a => a.Email).ToList();
        }
    }
}
