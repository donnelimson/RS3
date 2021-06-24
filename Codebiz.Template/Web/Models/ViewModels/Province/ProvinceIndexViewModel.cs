using PagedList;

namespace Web.Models.ViewModels.Province
{
    public class ProvinceIndexViewModel
    {
        public string SearchTerm { get; set; } 

        //public string SearchAbbreviation { get; set; } 

        //public string SearchRegion { get; set; } 

        //public string SearchAppUser { get; set; } 

        //public string SearchCreatedDate { get; set; } 

        public string SortBy { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public IPagedList<Codebiz.Domain.Common.Model.Province> Provinces { get; set; }
    }
}