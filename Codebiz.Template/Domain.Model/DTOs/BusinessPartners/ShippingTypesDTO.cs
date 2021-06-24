using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class ShippingTypesDTO
    {
        public int ShippingTypesId { get; set; }

        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("WEBSITE")]
        public string Website { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }

        [DisplayName("ACTION ON")]
        public DateTime ActionOn { get; set; }
    }
}
