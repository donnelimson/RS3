using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel.Notification
{
    public class Notification : ModelBase
    {
        public Notification()
        {
            NotificationDetails = new HashSet<NotificationDetail>();
        }

        [Key]
        public int NotificationId { get; set; }
        public string Transaction { get; set; }
        public string Destination { get; set; }
        public int? ReferenceId { get; set; }
        public string Message { get; set; }
        public virtual ICollection<NotificationDetail> NotificationDetails { get; set; }
    }
}
