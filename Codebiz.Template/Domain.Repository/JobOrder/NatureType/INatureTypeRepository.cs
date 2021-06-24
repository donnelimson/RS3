using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System.Collections.Generic;

namespace Domain.Repository.NatureType
{
    public interface INatureTypeRepository : IRepositoryBase<JobOrderNatureType>
    {
        JobOrderNatureType GetById(int id);
        JobOrderNatureType GetByDescription(string description);

        List<JobOrderNatureType> GetAllNotSelected(List<int> ids);

        IPagedList<NatureTypeDTO> SearchNatureTypes(JobOrderManagementFilter filter);

        IEnumerable<NatureTypeDTO> GetDataForExportingToExcel(JobOrderManagementFilter filter);

        IEnumerable<NatureTypeDTO> GetAllNatureTypes(bool permissionToEdit, bool permissionToModifyStatus);
        IEnumerable<NatureTypeDTO> GetAllNatureTypesByIdentification(bool forJOComplaint = false, bool forJORequest = false);

        bool CheckNatureTypeIfInUse(int id);
    }
}
