using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.NoOfUnitsAndKvaRating
{
    public class NoOfUnitsAndKvaRatingDTO
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
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class NoOfUnitsAndKvaRatingLookUpDTO
    {
        public int NoOfUnitsKvaId { get; set; }
        public int NoOfUnits { get; set; }
    }
}
