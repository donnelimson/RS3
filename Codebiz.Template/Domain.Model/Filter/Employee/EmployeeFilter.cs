using Codebiz.Domain.Common.Model.Filter;
using System;

namespace Codebiz.Domain.Common.Model
{
    public class EmployeeFilter : FilterBase
    {
        public string Name { get; set; }
        public string EmployeeNo { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? ExpDate { get; set; }
    }
}
