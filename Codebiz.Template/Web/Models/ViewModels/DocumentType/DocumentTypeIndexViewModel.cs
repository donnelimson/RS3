using PagedList;

namespace Web.Models.ViewModels.DocumentType
{
    public class DocumentTypeIndexViewModel
    {
        public string SearchTerm { get; set; } 

        public string SortBy { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        //public IPagedList<Codebiz.Domain.Common.Model.DocumentType> DocumentTypes { get; set; }
    }
}