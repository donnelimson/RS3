using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs.CityTown;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System.Collections.Generic;

namespace Codebiz.Domain.Repository
{
    public interface ICityTownRepository : IRepositoryBase<CityTown>
    {
        CityTown GetById(int id);
        bool CheckNameIfExists(string name, int provinceId, int id = 0);
        IEnumerable<CityTown> GetAllCityTown();
        IEnumerable<CityTown> GetByProvinceId(int provinceId);
        bool CheckIfCityTownCodeExist(string cityTownCode, int cityTownId);
        GetDetatilsCityTownDTO GetDetails(int cityTownId);
        IEnumerable<CityTownIndexDTO> GetDataForExportingToExcel(CityTownFilter filter);
        IPagedList<CityTownIndexDTO> SearchCityTown(CityTownFilter filter);
    }
}
