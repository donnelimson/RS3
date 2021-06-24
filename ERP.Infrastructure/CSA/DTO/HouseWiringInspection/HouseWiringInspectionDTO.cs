using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.CSA.DTO.HouseWiringInspection
{
    public class HouseWiringInspectionIndexDTO
    {
        public int MemberId { get; set; }
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public string ConsumerType { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int HouseWiringInspectionId { get; set; }
        public int MemberAccountsId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ActionOn { get; set; }
        public string TransactionType { get; set; }
    }
    public class HouseWiringInspectionRejectDTO
    {
        public string FullName { get; set; }
        public int MemberAccountId { get; set; }
        public int MemberId { get; set; }
        public string MemberNo { get; set; }
        public string AccountNo { get; set; }
        public int ConsumerTypeId { get; set; }
        public string ConsumerType { get; set; }
        public string Address { get; set; }
        public string SubscriptionType { get; set; }
        public string InstitutionName { get; set; }
        public string MembershipType { get; set; }
        public string Near { get; set; }
        public string PhotoUrl { get; set; }
        public string Route { get; set; }
        public string BookNo { get; set; }
        public string TransactionType { get; set; }
        public string Pole { get; set; }
    }
    public class ApproveHouseWiringInspectionDTO
    {
        public int HouseWiringInspectionId { get; set; }
        public string BarangayElectronicNumber { get; set; }
        public string AccreditedElectricianName { get; set; }
        public int ConnectionTypeId { get; set; }
        public string MeterSerialNo { get; set; }
        public int BrandId { get; set; }
        public string PoleNumber { get; set; }
        public float SwitchRating { get; set; }
        public float FuseRating { get; set; }
        public string BranchCircuitNumber { get; set; }
        public string LightingCircuitNumber { get; set; }
        public string ConvenienceOutletsNumber { get; set; }
        public int ListOfLoadsId { get; set; }
        public string Remarks { get; set; }
    }
    public class ApproveHouseWiringInspectionDetailsDTO
    {
        public int HouseWiringInspectionDataId { get; set; }
        public int NumberOfUnits { get; set; }
        public double KVARating { get; set; }
        public double BillDeposit { get; set; }
        public string BrandName { get; set; }
        public string ConnectionType { get; set; }
        public string PoleNo { get; set; }
        public double SafetySwitchRating { get; set; }
        public double FuseRating { get; set; }
        public string BranchCircuitNo { get; set; }
        public string LightingOutletsNo { get; set; }
        public string ConvenienceOutletsNo { get; set; }
        public string ServiceDropLength { get; set; }
        public string ListOfLoad { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
    public class HouseWiringInspectionDetailsMaterialsDTO
    {
        public string Supplier { get; set; }
        public string MasterItem { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public double TotalCost { get; set; }
        public double GrandTotalCost { get; set; }

    }
    public class HouseWiringInspectionStatusIdAndDesc
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class HouseWiringInspectionReportDTO
    {
        public string InspectorName { get; set; }
        public string AccountNo { get; set; }
        public string ConsumerType { get; set; }
        public string FullName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Header { get; set; }
    }
    public class HouseWiringInpsectionDataAttachmentDetailsDTO
    {
        public string PreviewThumbnailUrl { get; set; }
        public string FileName { get; set; }
        public long? FileSize { get; set; }
        public string DownloadUrl { get; set; }
        public string Url { get; set; }
        public bool IsPdf { get; set; }
        public bool IsWord { get; set; }
    }
    public class EndorseToHWIDTO
    {
        public int HouseWiringInspectionId { get; set; }
        public List<int?> Inspectors { get; set; }
        public string Notes { get; set; }
    }
}
