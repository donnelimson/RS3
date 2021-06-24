using System;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class ConsumerDataDto
    {
        public int Id { get; set; }
        public string ConsumerNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string MembershipType { get; set; }
        public string MembershipSubType { get; set; }
        public string OwnershipType { get; set; }
        public DateTime Birthdate { get; set; }
        public string EmailAddress { get; set; }
        public string TIN { get; set; }
        public string SpoueFirstName { get; set; }
        public string SpouseMiddleName { get; set; }
        public string SpouseLastName { get; set; }
        public string SpouseSuffix { get; set; }
        public DateTime? MembershipDate { get; set; }
    }

    public class AccountDataDto
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string ConsumerNo { get; set; }
        public string InstitutionName { get; set; }
        public string ConsumerType { get; set; }
        public string ConsumerIdentificationType { get; set; }
        public string TenureType { get; set; }
        public string HouseMake { get; set; }
        public string BlkandLotNo { get; set; }
        public string StreetName { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string Barangay { get; set; }
        public string Purok { get; set; }
        public string Sitio { get; set; }
        public string Route { get; set; }
        public string BookNo { get; set; }
        public string NearAccountNo { get; set; }
        public string PoleID { get; set; }
        public string MeterSerialNo { get; set; }
        public string IsBAPA { get; set; }
        public string IsMAPA { get; set; }
        public string IsICERA { get; set; }
        public string IsGRAM { get; set; }
        public float? SCDiscount { get; set; }
        public DateTime? SCDiscountDate { get; set; }
        public DateTime? SCDiscountExpirationDate { get; set; }
        public string HasNetMetering { get; set; }
        public string Status { get; set; }
        public string SequenceNo { get; set; }
        public int? FlatRate { get; set; }
        public int? FlatDemand { get; set; }
        public int? MeterMultiplier { get; set; }
        public DateTime? PMOSDate { get; set; }
    }
}
