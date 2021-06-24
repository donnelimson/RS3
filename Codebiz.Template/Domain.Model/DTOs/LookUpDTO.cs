using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class LookUpDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }

    public class DocNumberingLookupDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}
