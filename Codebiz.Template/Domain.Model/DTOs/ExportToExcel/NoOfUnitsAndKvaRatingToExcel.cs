using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class NoOfUnitsAndKvaRatingToExcel
    {
        [DisplayName("NO. OF UNITS")]
        public int NoOfUnits { get; set; }

        [DisplayName("KVA RATING")]
        public decimal KvaRating { get; set; }

        [DisplayName("ENERGY DEPOSIT")]
        public decimal? EnergyDeposit { get; set; }

        [DisplayName("MONTHLY RENTAL")]
        public decimal? MonthlyRental { get; set; }

        [DisplayName("INSTALLATION FEE")]
        public decimal? InstallationFee { get; set; }

        [DisplayName("TRANSFORMER LOSS")]
        public decimal? TransformerLoss { get; set; }

        [DisplayName("DEMAND ENERGY")]
        public decimal? DemandEnergy { get; set; }

        [DisplayName("CORE LOSS")]
        public decimal? CoreLoss { get; set; }
        
        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
