using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.Barangay;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DTOs.Route;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System.Collections.Generic;

namespace Codebiz.Domain.Repository
{
    public interface IBarangayRepository : IRepositoryBase<Barangay>
    {
        Barangay GetById(int id);
        IEnumerable<Barangay> GetAllBarangay();
        IEnumerable<Barangay> GetAllByCityTownId(int CityTownId);
        bool CheckIfBarangayCodeExist(string barangayCode, int barangayId, int cityTownId);
        bool CheckIfBarangayNameExist(string barangayName, int barangayId, int cityTownId);
        GetDetailsBarangayDTO GetDetails(int barangayId);
        IPagedList<BarangayIndexDTO> SearchBarangay(BarangayFilter filter);
        IEnumerable<BarangayToExcel> GetDataForExportingToExcel(BarangayFilter filter);
    }
}
