using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class ComplaintType : ModelBase
    {
        public ComplaintType()
        {
            this.ComplaintSubTypes = new HashSet<ComplaintSubType>();
        }

        [Key]
        public int ComplaintTypeId { get; set; }

        [ForeignKey("NatureType")]
        public int NatureTypeId { get; set; }
        public virtual JobOrderNatureType NatureType { get; set; }

        public bool IsDelete { get; set; }

        public virtual ICollection<ComplaintSubType> ComplaintSubTypes { get; set; }
    }
}
