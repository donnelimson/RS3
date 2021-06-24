using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Repository
{
    public interface IUserGroupRepository : IRepositoryBase<UserGroup>
    {
        List<int> GetAllUserGroupIdsByAppUserId(int id);
        List<string> GetAllPermissionsByUserGroupId(List<int> appUserUserGroupIds);
        List<string> GetAllPermissionsByPermissionGroupNames(List<string> permissionGroupNames, int id);
        IPagedList<UserGroupDTO> SearchUserGroup(UserGroupFilter filter);
        IEnumerable<UserGroupDTO> GetDataForExportingToExcel(UserGroupFilter filter);
    }
}
