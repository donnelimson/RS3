using System.Collections.Generic;
using Codebiz.Domain.Common.Model;
using PagedList;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DataModel.CSA;

namespace Codebiz.Domain.Repository
{
    public interface IPositionRepository : IRepositoryBase<Position>
    {
        Position GetById(int id);
        IEnumerable<Position> GetAllPositions();
        Position GetallById(int? positionId);
        bool IsCodeExist(string code, int id);
        bool IsNameExist(string name, int id);
        bool CheckIfPositionIsInUsed(int id);
        IPagedList<PositionTableDTO> SearchPosition(PositionFilter positionFilter);
        IEnumerable<PositionToExcel> GetDataForExportingToExcel(PositionFilter filter);
        IEnumerable<PositionLookUpDTO> GetPositionByOfficeAndDepartmentId(int? departmentId, int? officeId);
    }
}
