using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class JobOrderTaskToBePerform : ModelBase
    {
        [Key]
        public virtual int JobOrderTaskToBePerformId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<JobOrderTaskToBePerFormDetail> NatureTypes { get; set; }
    }

    public class JobOrderTaskToBePerFormDetail
    {
        [Key]
        public virtual int JobOrderTaskToBePerFormDetailId { get; set; }

        [ForeignKey("JobOrderTaskToBePerform")]
        public int JobOrderTaskToBePerformId { get; set; }
        public virtual JobOrderTaskToBePerform JobOrderTaskToBePerform { get; set; }


        [ForeignKey("JobOrderNatureType")]
        public int JobOrderNatureTypeId { get; set; }
        public virtual JobOrderNatureType JobOrderNatureType { get; set; }

    }
}
