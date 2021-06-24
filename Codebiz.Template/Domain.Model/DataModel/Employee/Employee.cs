using Codebiz.Domain.Common.Model.DataModel.CSA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class Employee : ModelBase
    {
        public Employee()
        {
            this.EmployeePhotos = new HashSet<EmployeePhoto>();
            this.EmployeeSignatures = new HashSet<EmployeeSignature>();
        }

        [Key]
        public int EmployeeId { get; set; }

        [MaxLength(50)]
        public string EmployeeNo { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(255)]
        public string Suffix { get; set; }

        public string BadgeNo { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression("^([ña-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [ForeignKey("Position")]
        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }

        [ForeignKey("Office")]
        public int? OfficeId { get; set; }
        public virtual Office Office { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("Division")]
        public int? DivisionId { get; set; }
        public virtual Division Division { get; set; }

        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        #region License
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string LicenseRestriction { get; set; }
        #endregion

        public virtual ICollection<EmployeePhoto> EmployeePhotos { get; set; }
        public virtual ICollection<EmployeeSignature> EmployeeSignatures { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
