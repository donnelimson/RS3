using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
   public class PoleNumberSeed
    {
        [Key]
        public int PoleNumberSeedId { get; set; }
        public string PoleNo { get; set; }
    }
}
