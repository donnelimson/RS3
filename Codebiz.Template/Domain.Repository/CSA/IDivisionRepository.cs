using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.Filter;
using System.Collections.Generic;

namespace Codebiz.Domain.Repository
{
    public interface IDivisionRepository : IRepositoryBase<Division>
    {
        Division GetById(int id);
        IEnumerable<Division> GetAllDivisions();
        T SearchDivisionsOrExportToExcel<T>(DivisionsFilter filter);

        IEnumerable<Division> GetAllByDepartment(int departmentId, int officeId);
        IEnumerable<Division> GetAllByDepartment(int departmentId);
        bool IsDivisionNameExist(string divisionName, int divisionId);
        bool IsDivisionCodeExist(string divisionCode, int divisionId);
        void DeleteCategory(DivisionCategory item);
        Division GetByIdForStatus(int id);

        List<Division> GetDivisionByPositionId(int? id, int? depararmentId, int? officeId);
    }
}
