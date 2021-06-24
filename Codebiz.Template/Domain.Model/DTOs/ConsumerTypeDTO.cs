using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class ConsumerTypeDTO
    {
        public int ConsumerTypeId { get; set; }
        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("SOLE USE")]
        public bool CanBeTagAsSoleUse { get; set; }

        [DisplayName("BAPA")]
        public bool BAPA { get; set; }

        [DisplayName("MAPA")]
        public bool MAPA { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime CreatedDate { get; set; }
        
    }
    public class ConsumerTypeForApplicantDetails
    {
        public int ConsumerTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? CanBeTagAsSoleUse { get; set; }
    }
}
