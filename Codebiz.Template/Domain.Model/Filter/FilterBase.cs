using System;
using System.Collections.Generic;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class FilterBase
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOnFrom { get; set; }
        public DateTime? CreatedOnTo { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string SortOrder { get; set; }
        public int TotalRecordCount { get; set; }
        public int FilteredRecordCount { get; set; }
        public bool IsForExcelExport { get; set; }
    }
    public class LookUpFilter : FilterBase
    {
        public string Searcher { get; set; }

        public int? AccountId { get; set; }

        public bool? ForVat { get; set; }
        public int? BillingUnbundledTransactionId { get; set; }
       
        public string CardType { get; set; }
        public List<int?> Ids { get; set; }
        public List<string> Codes { get; set; }
        public bool? NotAssigned { get; set; }
        public bool ExcludeBOM { get; set; }
        public string ItemType { get; set; }
        public string Postable { get; set; }

        public int? DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int? DivisionCategoryId { get; set; }

        public int? ProvinceId { get; set; }
        public int? CityTownId { get; set; }
        public int? BarangayId { get; set; }
        public long? BusinessPartnerId { get; set; }
    }
}