using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.BillingUnits
{
    public class UnitsDTO
    {
        public int UnitsId { get; set; }
        public string Unit { get; set; }
        public string MeterReader { get; set; }
        public string Office { get; set; }
        public string AppUser { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MeterReaderDTO
    {
        public int AppUserId { get; set; }
        public string AppUser { get; set; }
    }
    public class UnitModalSearchDTO
    {
        public int Id { get; set; }
        public int UnitsId { get; set; }
        public string Unit { get; set; }
        public string MeterReader { get; set; }
        public bool CannotDelete { get; set; }
        public bool IsSelected { get; set; }
    }
}
