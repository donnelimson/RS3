using Codebiz.Domain.Common.Model.DataModel.CSA.SupportingDocument;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System.Collections.Generic;

namespace Domain.Repository.CSA
{
    public interface ISupportingDocumentRepository : IRepositoryBase<SupportingDocument>
    {
        SupportingDocument GetById(int id);
        IPagedList<SupportingDocumentDTO> SearchSupportingDocument(SupportingDocumentFilter filter);
        bool IsTransactionTypeAndSubtypeExists(int supportingDocumentId, int transactionTypeId, int? transactionSubtypeId);
        IEnumerable<DocumentDetailsDTO> GetAllRequiredDocumentByTransactionTypeId(int transactionTypeId, int? transactionSubTypeId);
        IEnumerable<int> GetAllUsedTransactionTypeIdsInSupportingDocuments();
        IEnumerable<SupportingDocumentDTO> GetDataForExportingToExcel(SupportingDocumentFilter filter);
    }
}
