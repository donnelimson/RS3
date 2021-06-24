using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System.Collections.Generic;

namespace Codebiz.Domain.Repository
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
        AppUser GetById(int id);
        AppUser GetByUserName(string userName);
        AppUser GetActiveById(int id);
        AppUser GetDepartmentManagerById(int? departmentId);

        bool IsUsernameExists(string username, int appUserId);
        bool IsEmailExists(string email, int appUserId);

        IPagedList<AppUserDTO> SearchAppUser(AppUserFilter filter);
        IEnumerable<AppUserToExcel> GetDataForExportingToExcel(AppUserFilter filter);
        IPagedList<AppUserSearchDTO> SearchAppUserForLookup(LookUpFilter filter, bool isDriver);

        List<AppUser> GetAllUserByDepartmentAndDivision(int appUSerId, int? officeId, int? departmentId, int? divisionId, int? divisionCategoryId);

    }
}
