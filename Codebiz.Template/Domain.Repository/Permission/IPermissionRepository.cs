using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Repository
{
    public interface IPermissionRepository : IRepositoryBase<Permission>
    {
        IEnumerable<Permission> GetAllPermission();
        IEnumerable<PermissionToExcel> GetDataForExportingToExcel(PermissionFilter filter);
    }
}
