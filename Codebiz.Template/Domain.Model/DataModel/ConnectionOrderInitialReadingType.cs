using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ConnectionOrderInitialReadingType
    {
        [Key]
        public int ConnectionOrderInitialReadingTypeId { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
    }
}
