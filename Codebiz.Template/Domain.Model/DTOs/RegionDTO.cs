using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class RegionDTO
    {
        public int RegionId { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("ABBREVIATION")]
        public string Abbreviation { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
