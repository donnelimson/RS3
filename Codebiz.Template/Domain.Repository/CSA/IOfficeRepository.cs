using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.Office;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System.Collections.Generic;

namespace Codebiz.Domain.Repository
{
    public interface IOfficeRepository : IRepositoryBase<Office>
    {
        Office GetById(int id);
        IEnumerable<Office> GetAllOffices();
        Office GetallById(int? officeId);
        Office GetByCode(string code);
        bool IsCodeExists(string code, int id);
        bool IsNameExists(string name, int id);
        IPagedList<OfficeDTO> SearchOffice(OfficeFilter officeFilter);
        string GetOfficeNameById(int officeId);
        IEnumerable<OfficeDTO> GetDataForExportingToExcel(OfficeFilter officeFilter);
        IEnumerable<DepartmentLookUpDTO> GetDepartmentsByOfficeId(int? officeId);
        void DeleteDepartment(OfficeDepartment entity);
        bool CheckAddressIfExists(OfficeAddressFilter filter);
        bool CheckOfficeIfInUse(int id);
        IEnumerable<DepartmentLookUpDTO> GetDepartmentsByOfficeAndCurrentDepartmentId(int? officeId, int? departmentId, int? currentOfficeId);
    }
}
