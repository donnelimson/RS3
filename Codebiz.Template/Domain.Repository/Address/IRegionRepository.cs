using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System.Collections.Generic;

namespace Codebiz.Domain.Repository
{
    public interface IRegionRepository : IRepositoryBase<Region>
    {
        IEnumerable<Region> GetAllRegion();
        IPagedList<RegionDTO> SearchRegions(RegionFilter regionFilter);
        bool CheckNameIfExists(string name, int id);
        bool CheckAbbreviationIfExists(string abbreviation, int id);
        bool CheckRegionIfInUse(int id);
        Region GetById(int id);
        IEnumerable<RegionDTO> GetDataForExportingToExcel(RegionFilter filter);
    }
}
