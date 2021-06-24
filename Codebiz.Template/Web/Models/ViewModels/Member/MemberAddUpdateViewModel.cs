//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace Web.Models.ViewModels.Member
//{
//    public class MemberAddUpdateViewModel
//    {
//        public int MemberId { get; set; }

//        [MaxLength(255)]
//        public string LastName { get; set; }
//        public string FirstName { get; set; }
//        public string MiddleName { get; set; }
//        public string QualifierName { get; set; }
//        public DateTime? BirthDate { get; set; }
//        public string BirthPlace { get; set; }
//        public int MobileNumber { get; set; }
//        [MaxLength(15)]
//        public string Landline { get; set; }
//        public string BlkLotNumber { get; set; }
//        public string Email { get; set; }
//        public int? MemberTypeId { get; set; }
//        public int MembershipStatusId { get; set; }
//        public int? ConsumerTypeId { get; set; }
//        public int? RegionId { get; set; }
//        public int? ProvinceId { get; set; }
//        public int? CityTownId { get; set; }
//        public int? BarangayId { get; set; }
//        public List<MembersAttachmentViewModel> MembersAttachment { get; set; }


//        /////////////////////ACCOUNT
//        public int? MemberAccountsId_Account { get; set; }
//        public string BlkLotNumber_Account { get; set; }
//        public string Email_Account { get; set; }
//        public int? MemberTypeId_Account { get; set; }
//        public int? StatusId_Account { get; set; }
//        public int? ConsumerTypeId_Account { get; set; }
//        public int? RegionId_Account { get; set; }
//        public int? ProvinceId_Account { get; set; }
//        public int? CityTownId_Account { get; set; }
//        public int? BarangayId_Account { get; set; }
//    }
//}