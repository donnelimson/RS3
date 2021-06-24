using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs.ComplaintType;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IComplaintTypeRepository : IRepositoryBase<ComplaintType>
    {
        ComplaintType GetById(int id);
        IPagedList<ComplaintTypeDTO> SearchComplaintType(ComplaintTypeFilter filter);
        IEnumerable<ComplaintTypeToExcel> GetDataForExportingToExcel(ComplaintTypeFilter filter);
        IEnumerable<Codebiz.Domain.Common.Model.ComplaintType> GetAllActive();
        IEnumerable<ComplaintSubType> GetAllSubTypeByComplaintId(int complaintTypeId);
        bool IsComplaintTypeNameExist(string complaintTypeName, int complaintTypeId);
        bool ComplaintTypeIsInUsed(int id);
    }
}
