using Codebiz.Domain.Common.Model;
using PagedList;
using System.Collections.Generic;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;

namespace Codebiz.Domain.Repository
{
    public interface IProvinceRepository : IRepositoryBase<Province>
    {
        Province GetById(int id);
        bool CheckAbbreviationIfExists(string name, int? regionId, int id);
        IEnumerable<Province> GetAllProvince();
        IPagedList<ProvinceDTO> SearchProvinces(ProvinceFilter provinceFilter);
        IEnumerable<Province> GetAllProvinceByRegionId(int regionId);
        IEnumerable<ProvinceToExcel> GetDataForExportingToExcel(ProvinceFilter filter);
        int GetTarlacProvinceId();
        bool CheckNameIfExists(string name, int? regionId, int id);
    }
}
