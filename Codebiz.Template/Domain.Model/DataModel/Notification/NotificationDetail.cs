using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel.Notification
{
    public class NotificationDetail
    {
        [Key]
        public int NotificationDetailId { get; set; }

        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; }

        [ForeignKey("AppUser")]
        public int AppUserId { get; set; } = 1;
        public virtual AppUser AppUser { get; set; }
        public bool IsRead { get; set; }
        public bool IsProcessed { get; set; }
    }
}
