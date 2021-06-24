using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class JobOrderNatureType : ModelBase
    {
        [Key]
        public int JobOrderNatureTypeId { get; set; }
        public string Description { get; set; }
        public bool ForJORequest { get; set; }
        public bool ForJOComplaint { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFixed { get; set; }

        public virtual ICollection<ComplaintType> ComplaintTypes { get; set; }
        public virtual ICollection<JobOrderTaskToBePerform> JobOrderTaskToBePerforms { get; set; }

    }
}
