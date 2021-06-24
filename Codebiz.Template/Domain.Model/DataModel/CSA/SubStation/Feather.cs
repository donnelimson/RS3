using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class Feeder
    {
        [Key]
        public int FeederId { get; set; }
        public string Name { get; set; }

        [ForeignKey("SubStation")]
        public int SubStationId { get; set; }
        public virtual SubStation SubStation { get; set; }
        public bool IsDeleted { get; set; }
    }
}
