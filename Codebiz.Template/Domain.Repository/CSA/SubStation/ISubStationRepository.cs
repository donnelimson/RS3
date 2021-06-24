using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel.CSARequest;
using Codebiz.Domain.Common.Model.DTOs.SubStation;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface ISubStationRepository : IRepositoryBase<SubStation>
    {
        SubStation GetById(int id);
        IPagedList<SubStationDTO> SearchSubStation(SubStationFilter filter);
        IEnumerable<SubStation> GetAllActive();
        IEnumerable<SubStationToExcel> GetDataForExportingToExcel(SubStationFilter filter);
        bool CheckIfDescriptionIsExist(string description, int id = 0);
        IEnumerable<Feeder> GetAllFeederBySubstationId(int id);
    }
}
