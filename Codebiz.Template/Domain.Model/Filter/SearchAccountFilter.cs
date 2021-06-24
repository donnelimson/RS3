using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class SearchAccountFilter : FilterBase
    {
        public string AccountNo { get; set; }
        public string ConsumerType { get; set; }
        public string Name { get; set; }
        public string MemberNo { get; set; }
        public string Searcher { get; set; }
        public int? TransactionTypeId { get; set; }
        public string MemberNoSearch { get; set; }

        public int? RequiredFilter { get; set; }
        public int? MemberId { get; set; }
        public int? AccountId { get; set; }
        public bool IsSearchByMember { get; set; }
        public bool IsMotherAccount { get; set; }

        //For searching Near

        public bool ForNear { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityTownId { get; set; }
        public int? BarangayId { get; set; }
        public int? PurokId { get; set; }
        public int? SitioId { get; set; }
        public string AccountType { get; set; }
        public string Type { get; set; }
    }
}
