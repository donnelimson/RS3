using System.Collections.Generic;
using Codebiz.Domain.Common.Model;
using PagedList;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DTOs.Purok;

namespace Codebiz.Domain.Repository
{
    public interface IPurokRepository : IRepositoryBase<Purok>
    {
        Purok GetById(int id);
        IEnumerable<Purok> GetAllPuroks();
        Purok GetAllById(int? purokId);
        IEnumerable<Purok> GetAllByBarangayId(int BarangayId);
        IPagedList<PurokDTO> SearchPurok(PurokFilter purokFilter);
        bool IsCodeExists(string code, int id);
        bool IsNameExists(string name, int? barangayId,int id);
        IEnumerable<PurokDTO> GetDataForExportingToExcel(PurokFilter filter);

    }

}
