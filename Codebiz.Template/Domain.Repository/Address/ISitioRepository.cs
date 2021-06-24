using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs.Sitio;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System.Collections.Generic;

namespace Codebiz.Domain.Repository
{
    public interface ISitioRepository : IRepositoryBase<Sitio>
    {
        Sitio GetById(int id);
        IEnumerable<Sitio> GetAllSitios();
        Sitio GetAllById(int officeId);
        bool IsSitioNameExists(string name, int? purokId, int id);
        bool IsSitioCodeExists(string code, int id);
        IEnumerable<Sitio> GetAllByBarangayId(int id);
        IPagedList<SitioDTO> SearchSitio(SitioFilter filter);
        IEnumerable<SitioDTO> GetDataForExportingToExcel(SitioFilter filter);
    }
}
