using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class UserGroupDTO
    {
        public int UserGroupId { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("DESCRIPTION")]
        public string Description { get; set; }

        [DisplayName("IS ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("DATE CREATED")]
        public DateTime? CreatedOn { get; set; }
    }
}
