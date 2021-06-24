using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs.Office
{
    public class OfficeDTO
    {
        public int OfficeId { get; set; }

        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("ADDRESS")]
        public string Address { get; set; }

        [DisplayName("AREA OFFICE")]
        public bool AreaOffice { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime CreatedDate { get; set; }

        public bool IsAreaOffice { get; set; }
        public bool HasMainOffice { get; set; }
    }

}
