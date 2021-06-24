using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class LoginHistory
    {
        [Key]
        public int LoginHistoryId { get; set; }

        [ForeignKey("AppUser")]
        public int? AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public DateTime? LoginDateTime { get; set; }

        public DateTime? LogoutDateTime { get; set; }

        public string SessionID { get; set; }

        public string UserIpAddress { get; set; }

       

    }
}
