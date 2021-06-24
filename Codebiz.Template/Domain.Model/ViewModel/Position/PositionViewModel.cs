using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common.Position
{
    public class PositionViewModel
    {

        public int PositionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsManager { get; set; }
        public bool IsHeadOfficer { get; set; }
        public int? DepartmentId { get; set; }
    }
}
