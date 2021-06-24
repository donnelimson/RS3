using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class NoOfUnitsAndKvaRatingFilter : FilterBase
    {
        public int? NoOfUnits { get; set; }
        public decimal? KvaRating { get; set; }
    }
}
