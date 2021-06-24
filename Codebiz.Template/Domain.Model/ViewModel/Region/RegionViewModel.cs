using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.Region
{
    public class RegionViewModel
    {
        public int RegionId { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
