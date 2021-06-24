using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class NatureTypeDTO
    {
        public int Id { get; set; }
        public int JobOrderDataId { get; set; }

        [DisplayName("NATURE TYPE")]
        public string JobOrderData { get; set; }

        [DisplayName("JO REQUEST")]
        public bool ForJORequest { get; set; }

        [DisplayName("JO COMPLAINT")]
        public bool ForJOComplaint { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED ON")]
        public DateTime CreatedDate { get; set; }

        public bool IsInUsed { get; set; }

        public bool HasPermissionToEdit { get; set; }
        public bool HasPermissionToModifyStatus { get; set; }
        public bool IsFixed { get; set; }
    }
}
