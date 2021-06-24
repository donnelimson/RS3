using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.NoOfUnitsAndKVARating
{
    public class NoOfUnitsAndKvaRatingViewModel
    {
        public int NoOfUnitsKvaId { get; set; }
        public int NoOfUnits { get; set; }
        public decimal KvaRating { get; set; }
        public decimal? EnergyDeposit { get; set; }
        public decimal? MonthlyRental { get; set; }
        public decimal? InatallationFee { get; set; }
        public decimal? TransformerLoss { get; set; }
        public decimal? DemandEnergy { get; set; }
        public decimal? CoreLoss { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnFrom { get; set; }
        public DateTime? CreatedOnTo { get; set; }
        public bool IsInitial { get; set; }
    }
}
