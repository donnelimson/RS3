using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
   public class CityTownFilter: FilterBase
    {
        public string CityTown { get; set; }
        public string Province { get; set; }
    }
}
