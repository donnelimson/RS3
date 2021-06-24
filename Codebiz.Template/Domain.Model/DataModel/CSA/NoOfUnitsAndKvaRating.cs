using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.ViewModel.NoOfUnitsAndKVARating;

namespace Codebiz.Domain.Common.Model
{
   public class NoOfUnitsAndKvaRating : ModelBase
    {
        [Key]
        public int NoOfUnitsKvaId { get; set; }

        [Required]
        public int NoOfUnits { get; set; }

        [Required]
        public decimal KvaRating { get; set; }

        public decimal? EnergyDeposit { get; set; }
        public decimal? MonthlyRental { get; set; }
        public decimal? InatallationFee { get; set; }
        public decimal? TransformerLoss { get; set; }
        public decimal? DemandEnergy { get; set; }
        public decimal? CoreLoss { get; set; }
        public bool IsDeleted { get; set; }

        public void setNoOfUnitsAndKvaRating(NoOfUnitsAndKvaRatingViewModel model)
        {
            NoOfUnits = model.NoOfUnits;
            KvaRating = model.KvaRating;
            EnergyDeposit = model.EnergyDeposit;
            MonthlyRental = model.MonthlyRental;
            InatallationFee = model.InatallationFee;
            TransformerLoss = model.TransformerLoss;
            DemandEnergy = model.DemandEnergy;
            CoreLoss = model.CoreLoss;
        }
    }
}
