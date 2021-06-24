using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        Department GetById(int id);
        IEnumerable<Department> GetAllDepartments();
        Department GetallById(int? departmentid);
        bool IsDepartmentCodeExist(string departmentCode, int id);
        bool IsDepartmentNameExist(string departmentName, int id);
        bool CheckDepartmentIfInUse(int id);
        IPagedList<DepartmentDTO> SearchDepartment(DepartmentFilter departmentFilter);
        IEnumerable<DepartmentsToExcel> GetDataForExportingToExcel(DepartmentFilter filter);
        IEnumerable<PositionLookUpDTO> GetPositionsByDepartmentId(int? departmentId);
        void DeleteDetail(DepartmentDetail entity);
        EmployeeDto GetAppUserWithHighestPositionByDepartmentId(int officeId, int departmentId);
    }
}
