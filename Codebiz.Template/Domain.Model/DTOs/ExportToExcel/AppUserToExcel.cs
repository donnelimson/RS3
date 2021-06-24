using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class AppUserToExcel
    {
        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("USERNAME")]
        public string Username { get; set; }

        [DisplayName("E-MAIL")]
        public string Email { get; set; }

        [DisplayName("OFFICE")]
        public string Office { get; set; }

        [DisplayName("DEPARTMENT")]
        public string Department { get; set; }

        [DisplayName("POSITION")]
        public string Position { get; set; }

        [DisplayName("E-MAIL CONFIRMATION")]
        public bool IsEmailConfirmed { get; set; }

        [DisplayName("STATUS")]
        public string Status { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
