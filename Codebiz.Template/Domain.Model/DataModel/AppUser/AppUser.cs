﻿using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Codebiz.Domain.Common.Model
{
    public class AppUser : ModelBase
    {
        public AppUser()
        {
            this.ApprovalStages = new HashSet<ApprovalStage>();
            this.LoginHistories = new HashSet<LoginHistory>();
        }

        [Key]
        public int AppUserId { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("UsernameIndex", IsUnique = true)]
        public string Username { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public int AccessFailedCount { get; set; }

        [MaxLength(100)]
        public string PasswordHash { get; set; }

        [MaxLength(2000)]
        public string ForgotPasswordUrlParam { get; set; }

        public DateTime? ForgotPasswordExpiryDate { get; set; }

        [ForeignKey("AccessLevel")]
        public int? AccessLevelId { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }

        //User Attempts
        public int FailedLoggedInAttempt { get; set; }

        [MaxLength(2000)]
        public string UnlockUrlParam { get; set; }
        public string VerificationCode { get; set; }

        public bool? IsLocalNetworkUser { get; set; }
        public bool EmailConfirmed { get; set; }
        public int AppUserStatus { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<ApprovalStage> ApprovalStages { get; set; }   
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return this.Employee != null
                    ? string.Format("{0} {1}", this.Employee.FirstName, this.Employee.LastName)
                    : this.Username;
            }
        }

        [NotMapped]
        public UserStatuses UserStatus
        {
            get
            {
                var value = (UserStatuses)this.AppUserStatus;
                return value;
            }
        }

        [NotMapped]
        public string CurrentOffice
        {
            get
            {
                return Employee ==null ? "" : Employee.OfficeId.Equals(null) ? "" : string.Format("{0}, {1}, {2}",
                    Employee.Office.Barangay.Name.ToUpper().Substring(0, 1) + Employee.Office.Barangay.Name.ToLower().Substring(1, Employee.Office.Barangay.Name.Length - 1),
                    Employee.Office.CityTown.Name.ToUpper().Substring(0, 1) + Employee.Office.CityTown.Name.ToLower().Substring(1, Employee.Office.CityTown.Name.Length - 1),
                    Employee.Office.Province.Name.ToUpper().Substring(0,1) + Employee.Office.Province.Name.ToLower().Substring(1, Employee.Office.Province.Name.Length - 1)).ToString();
            }
        }

        public void setAppUser(string firstName, string middleName, string lastName, string suffix, string username, int? positionId, int? officeId, bool isActive, bool sendActivationLink)
        {
            Employee.FirstName = firstName;
            Employee.MiddleName = middleName;
            Employee.LastName = lastName;
            Employee.Suffix = suffix;
            Username = username;
            Employee.PositionId = positionId;
            Employee.OfficeId = officeId;
            IsActive = isActive;
            IsLocalNetworkUser = sendActivationLink;
        }
    }
}
