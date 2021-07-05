using System.ComponentModel;

namespace Codebiz.Domain.Common.Model
{
    public enum LogEventTitles
    {
        [Description("Request Start")]
        RequestStart = 1,
        [Description("Request End")]
        RequestEnd = 2,
        [Description("Request Details")]
        RequestDetails = 3,
        [Description("Login")]
        Login = 4,
        [Description("Logout")]
        Logout = 5,

        #region CSA
        //Member
        [Description("Member Created")]
        MemberCreated = 6,
        [Description("Member Updated")]
        MemberUpdated = 7,
        [Description("Member Application Endorse")]
        MemberApplicationEndorse = 8,
        [Description("House Wiring Inspection Report Created")]
        HouseWiringInspectionReportCreated = 9,
        [Description("House Wiring Inspection Report Updated")]
        HouseWiringInspectionReportUpdated = 10,
        [Description("Connection Order Issued")]
        ConnectionOrderIssued = 11,
        [Description("Job Order Prepared")]
        JobOrderPrepared = 12,

        #endregion

        [Description("Create Brand")]
        CreateBrand = 13,
        [Description("Create Brand Type")]
        CreateBrandType = 14,
        [Description("Close And Tranfer Account")]
        CloseAndTransferAccount = 15,
        [Description("Sending Email")]
        SendingEmail = 16,
        [Description("Create Consumer Type")]
        CreateConsumerType = 17,
        [Description("Update Consumer Type")]
        UpdateConsumerType = 18,

        #region Purok

        [Description("Purok Created")]
        PurokCreated = 19,
        [Description("Purok Updated")]
        PurokUpdated = 20,
        [Description("Purok Deleted")]
        PurokDeleted = 21,
        [Description("Purok Deactivated")]
        PurokDeactivated = 22,
        [Description("Purok Activated")]
        PurokActivated = 23,

        #endregion

        #region Sitio

        [Description("Sitio Created")]
        SitioCreated = 24,

        [Description("Sitio Updated")]
        SitioUpdated = 25,

        [Description("Sitio Deleted")]
        SitioDeleted = 26,

        [Description("Sitio Deactivated")]
        SitioDeactivated = 27,

        [Description("Sitio Activated")]
        SitioActivated = 28,

        #endregion

        #region Route

        [Description("Route Created")]
        RouteCreated = 29,

        [Description("Route Updated")]
        RouteUpdated = 30,

        [Description("Route Deleted")]
        RouteDeleted = 31,

        [Description("Route Deactivated")]
        RouteDeactivated = 32,

        [Description("Route Activated")]
        RouteActivated = 33,

        #endregion

        #region Office

        [Description("Office Created")]
        OfficeCreated = 34,

        [Description("Office Updated")]
        OfficeUpdated = 35,

        [Description("Office Deleted")]
        OfficeDeleted = 36,

        [Description("Office Deactivated")]
        OfficeDeactivated = 38,

        [Description("Office Activated")]
        OfficeActivated = 39,

        #endregion

        #region Position

        [Description("Position Created")]
        PositionCreated = 40,

        [Description("Position Updated")]
        PositionUpdated = 41,

        [Description("Position Deleted")]
        PositionDeleted = 42,
        [Description("Position Deactivated")]
        PositionDeactivated = 43,

        [Description("Position Activated")]
        PositionActivated = 44,

        #endregion

        #region Approval Stage

        [Description("Approval Stage Created")]
        ApprovalStageCreated = 45,

        [Description("Approval Stage Updated")]
        ApprovalStageUpdated = 46,

        [Description("Approval Stage Deleted")]
        ApprovalStageDeleted = 47,

        [Description("Approval Stage Deactivated")]
        ApprovalStageDeactivated = 48,

        [Description("Approval Stage Activated")]
        ApprovalStageActivated = 49,

        #endregion

        #region Province

        [Description("Province Created")]
        ProvinceCreated = 51,

        [Description("Province Updated")]
        ProvinceUpdated = 52,

        [Description("Province Deleted")]
        ProvinceDeleted = 53,

        [Description("Province Deactivated")]
        ProvinceDeactivated = 54,

        [Description("Province Activated")]
        ProvinceActivated = 55,

        #endregion

        #region Region

        [Description("Region Created")]
        RegionCreated = 56,

        [Description("Region Updated")]
        RegionUpdated = 57,

        [Description("Region Deleted")]
        RegionDeleted = 58,

        [Description("Region Deactivated")]
        RegionDeactivated = 59,

        [Description("Region Activated")]
        RegionActivated = 60,

        #endregion

        #region Pole

        [Description("Pole Created")]
        PoleCreated = 62,

        [Description("Pole Updated")]
        PoleUpdated = 63,

        [Description("Pole Deleted")]
        PoleDeleted = 64,

        [Description("Pole Deactivated")]
        PoleDeactivated = 65,

        [Description("Pole Activated")]
        PoleActivated = 66,

        #endregion

        #region Department

        [Description("Department Created")]
        DepartmentCreated = 67,

        [Description("Department Updated")]
        DepartmentUpdated = 68,

        [Description("Department Deleted")]
        DepartmentDeleted = 69,
        [Description("Department Deactivated")]
        DepartmentDeactivated = 71,

        [Description("Department Activated")]
        DepartmentActivated = 72,

        #endregion

        #region No. Of Units And KVA Rating

        [Description("No. Of Units And KVA Rating Created")]
        NoOFUnitsAndKVARatingCreated = 73,


        [Description("No. Of Units And KVA Rating Updated")]
        NoOFUnitsAndKVARatingUpdated = 74,

        [Description("No. Of Units And KVA Rating Deleted")]
        NoOFUnitsAndKVARatingDeleted = 75,
        [Description("No. Of Units And KVA Rating Deactivated")]
        NoOFUnitsAndKVARatingDeactivated = 76,

        [Description("No. Of Units And KVA Rating Activated")]
        NoOFUnitsAndKVARatingActivated = 77,

        #endregion

        [Description("Update PMOSeminar")]
        UpdatePMOSeminar = 78,

        #region App User

        [Description("User created")]
        UserCreated = 79,

        [Description("User updated")]
        UserUpdated = 80,

        [Description("User reactivated")]
        UserReactivated = 81,

        [Description("User deactivated")]
        UserDeactivated = 82,

        [Description("Activation link sent")]
        ActivationLinkSent = 83,

        [Description("Reset password link sent")]
        ResetPasswordLinkSent = 84,

        #endregion

        [Description("Issue An Account For Connection")]
        IssueAnAccountForConnection = 85,
        [Description("Save For Connection Action Taken")]
        SaveForConnectionActionTaken = 86,
        [Description("Reassigned Inspector")]
        ReassignedInspector = 87,
        [Description("Reject House Wiring Inspection")]
        RejectHouseWiringInspection = 88,
        [Description("Disapprove House Wiring Inspection")]
        DisapproveHouseWiringInspection = 89,
        [Description("Approve House Wiring Inspection")]
        ApproveHouseWiringInspection = 90,
        [Description("Print MembershipID")]
        PrintMembershipID = 91,
        [Description("Approval Template Created")]
        ApprovalTemplateCreated = 92,
        [Description("Approval Template Updated")]
        ApprovalTemplateUpdated = 93,
        [Description("Approval Template Deactivated")]
        ApprovalTemplateDeactivated = 94,
        [Description("Approval Template Reactivated")]
        ApprovalTemplateReactivated = 95,
        [Description("Approval Template Deleted")]
        ApprovalTemplateDeleted = 96,

        #region Units

        [Description("Units Created")]
        UnitsCreated = 97,

        [Description("Units Updated")]
        UnitsUpdated = 98,

        [Description("Units Deleted")]
        UnitsDeleted = 99,
        [Description("Units Deactivated")]
        UnitsDeactivated = 100,

        [Description("Units Activated")]
        UnitsActivated = 101,

        #endregion

        #region Billing Unbundled Transaction

        [Description("Billing Unbundled Transaction Created")]
        BillingUnbundledTransactionCreated = 102,

        [Description("Billing Unbundled Transaction Updated")]
        BillingUnbundledTransactionUpdated = 103,

        [Description("Billing Unbundled Transaction Deleted")]
        BillingUnbundledTransactionDeleted = 104,
        [Description("Billing Unbundled Transaction Deactivated")]
        BillingUnbundledTransactionDeactivated = 105,

        [Description("Billing Unbundled Transaction Activated")]
        BillingUnbundledTransactionActivated = 106,

        #endregion

        #region Billing Monthly Rates

        [Description("Billing Monthly Rates Created")]
        BillingMonthlyRatesCreated = 107,

        [Description("Billing Monthly Rates Updated")]
        BillingMonthlyRatesUpdated = 108,

        #endregion

        #region Approval Account

        [Description("Approval")]
        Approval = 109,

        #endregion

        #region Route Meter Reading

        [Description("Route Meter Reading Added")]
        RouteMeterReadingAdded = 112,
        #endregion

        #region Discount Application

        [Description("Discount Application Created")]
        DiscountApplicationCreated = 113,

        [Description("Discount Application Updated")]
        DiscountApplicationUpdated = 114,

        [Description("Discount Application Deleted")]
        DiscountApplicationDeleted = 115,

        [Description("Discount Application Renewed")]
        DiscountApplicationRenewed = 116,
        #endregion

        #region Recommendation for Approval

        [Description("Recommendation for Approval")]
        RecommendationforApproval = 117,

        [Description("Batch Meter Reading Added")]
        BatchMeterReadingAdded = 118,
        [Description("Unit Meter Reading Added")]
        UnitMeterReadingAdded = 119,

        #endregion

        #region Supporting Documents

        [Description("Create Supporting Document")]
        CreateSupportingDocument = 120,
        [Description("Update Supporting Document")]
        UpdateSupportingDocument = 121,
        [Description("Delete Supporting Document")]
        DeleteSupportingDocument = 122,

        #endregion

        #region Burial Assistance
        [Description("Create Burial Assistance")]
        CreateBurialAssistance = 123,
        [Description("Delete Burial Assistance")]
        DeleteBurialAssistance = 124,
        [Description("Edit Burial Assistance")]
        EditBurialAssistance = 125,
        [Description("Coop Evaluation Burial Assistance")]
        CoopEvaluationBurialAssistance = 126,

        #endregion

        #region Change Of Name

        [Description("Change of Name Created")]
        ChangeOfNameCreated = 127,

        [Description("Change of Name Updated")]
        ChangeOfNameUpdated = 128,

        [Description("Change of Name Deleted")]
        ChangeOfNameDeleted = 129,

        #endregion

        #region Categories

        [Description("Create Category")]
        CreateCategory = 130,

        [Description("Edit Category")]
        EditCategory = 131,

        #endregion

        #region Reconnection

        [Description("Reconnection Created")]
        ReconnectionCreated = 132,

        [Description("Reconnection Updated")]
        ReconnectionUpdated = 133,

        [Description("Reconnection Deleted")]
        ReconnectionDeleted = 134,
        #endregion

        #region Surcharge

        [Description("Surcharge Created")]
        SurchargeCreated = 135,

        [Description("Surcharge Updated")]
        SurchargeUpdated = 136,

        [Description("Surcharge Deleted")]
        SurchargeDeleted = 137,

        [Description("Surcharge Activated")]
        SurchargeActivated = 138,

        [Description("Surcharge Deactivated")]
        SurchargeDeactivated = 139,
        #endregion

        #region Disconnection Request

        [Description("Disconnection Request Created")]
        CreateDisconnectionRequest = 140,

        [Description("Disconnection Request Updated")]
        EditDisconnectionRequest = 141,

        [Description("Disconnection Request Deleted")]
        DeleteDisconnectionRequest = 142,

        #endregion

        #region Request Close and Transfer
        [Description("Request Close and Transfer Created")]
        RequestCloseAndTransferCreated = 143,

        [Description("Request Close and Transfer Updated")]
        RequestCloseAndTransferUpdated = 144,

        [Description("Request Close and Transfer Deleted")]
        RequestCloseAndTransferDeleted = 145,
        #endregion

        #region Work Order

        [Description("Work Order Created")]
        WorkOrderCreated = 146,

        [Description("Work Order Updated")]
        WorkOrderUpdated = 147,

        [Description("Work Order Deleted")]
        WorkOrderDeleted = 148,

        [Description("Work Order Deactivated")]
        WorkOrderDeactivated = 149,

        [Description("Work Order Activated")]
        WorkOrderActivated = 150,

        #endregion

        #region Substation

        [Description("Substation Created")]
        SubstationCreated = 151,

        [Description("Substation Updated")]
        SubstationUpdated = 152,

        [Description("Substation Deleted")]
        SubstationDeleted = 153,

        [Description("Substation Deactivated")]
        SubstationDeactivated = 154,

        [Description("Substation Activated")]
        SubstationReactivated = 155,

        #endregion

        #region Export Data to Excel

        [Description("Exported Data to Excel File")]
        ExportedDataToExcelFile = 156,

        #endregion

        #region Complaint Type

        [Description("Complaint Type Created")]
        ComplaintTypeCreated = 158,

        [Description("Complaint Type Updated")]
        ComplaintTypetUpdated = 159,

        [Description("Complaint Type Deleted")]
        ComplaintTypeDeleted = 160,

        [Description("Complaint Type Deactivated")]
        ComplaintTypeDeactivated = 161,

        [Description("Complaint Type Activated")]
        ComplaintTypeActivated = 162,

        #endregion

        #region Net Metering

        [Description("Net Metering Added")]
        NetMeteringAdded = 163,

        [Description("Net Metering Updated")]
        NetMeteringUpdated = 164,

        [Description("Net Metering Deleted")]
        NetMeteringDeleted = 165,

        [Description("Net Metering Endorsed For Payment")]
        NetMeteringEndorsedForPayment = 166,

        #endregion

        #region Billing Transaction

        [Description("Post Billing Transaction")]
        PostBillingTransaction = 167,

        #region Job Order - Complaint

        [Description("Job Order - Complaint Added")]
        JobOrderComplaintAdded = 168,

        [Description("Job Order - Complaint Updated")]
        JobOrderComplaintUpdated = 169,

        [Description("Job Order Deleted")]
        JobOrderDeleted = 170,

        [Description("Job Order Endorsed")]
        JobOrderEndorsed = 173,

        #endregion

        [Description("Validate Billing Transaction")]
        ValidateBillingTransaction = 174,

        #endregion

        #region DocumentNumbering
        [Description("Add DocumentNumbering")]
        AddDocumentNumbering = 175,
        [Description("Edit DocumentNumbering")]
        EditDocumentNumbering = 176,
        #endregion

        #region Job Order Request

        [Description("Add Job Order Request")]
        AddJobOrderRequest = 177,

        [Description("Edit Job Order Request")]
        EditJobOrderRequest = 179,

        #endregion

        #region Payment

        [Description("Created Payment")]
        CreatedPayment = 180,

        #endregion

        [Description("Substation And Feeder Added")]
        SubstationAndFeederAdded = 181,

        #region 
        [Description("Job Order Category Added")]
        JobOrderCategoryAdded = 182,
        [Description("Job Order Category Updated")]
        JobOrderCategoryUpdated = 183,
        [Description("Job Order Category Deleted")]
        JobOrderCategoryDeleted = 184,
        [Description("Job Order Category Reactivated")]
        JobOrderCategoryReactivated = 185,
        [Description("Job Order Category Deactivated")]
        JobOrderCategoryDeactivated = 186,
        #endregion

        #region Incoming Distribution Transformer

        [Description("Incoming Distribution Transformer Tested")]
        IncomingDistributionTransformerTested = 187,

        #endregion

        #region Packaging Type

        [Description(" Packaging Type Added")]
        PackagingTypeAdded = 188,

        #endregion

        #region Shipping Type

        [Description(" Shipping Type Added")]
        ShippingTypeAdded = 189,

        #endregion

        #region Unit Of Measures

        [Description("Unit Of Measures Added")]
        UnitOfMeasureAdded = 190,

        #endregion

        #region 

        [Description("Incoming Meter Calibrated")]
        IncomingMeterCalibrated = 192,
        #endregion

        #region Outgoing Distribution Transformer

        [Description("Outgoing Distribution Transformer Tested")]
        OutgoingDistributionTransformerTested = 193,
        [Description("Outgoing Distribution Transformer Created")]
        OutgoingDistributionTransformerCreated = 194,

        #endregion

        [Description("Member Account Created")]
        MemberAccountCreated = 195,
        [Description("Member Account Update")]
        MemberAccountUpdated = 196,
        [Description("Consumer Type Deleted")]
        ConsumerTypeDeleted = 197,
        [Description("Consumer Type Activated")]
        ConsumerTypeActivated = 198,
        [Description("Consumer Type Deactivated")]
        ConsumerTypeDeactivated = 199,

        #region City Town
        [Description("City Town Added")]
        CityTownAdded = 200,
        [Description("City Town Updated")]
        CityTownUpdated = 201,
        [Description("City Town Deleted")]
        CityTownDeleted = 202,
        [Description("City Town Activated")]
        CityTownActivated = 203,
        [Description("City Town Deactivated")]
        CityTownDeactivated = 204,

        #endregion

        #region  Barangay
        [Description("Barangay Added")]
        BarangayAdded = 205,
        [Description("Barangay Updated")]
        BarangayUpdated = 206,
        [Description("BarangayDeleted")]
        BarangayDeleted = 207,
        [Description("Barangayn Activated")]
        BarangayActivated = 208,
        [Description("Barangay Deactivated")]
        BarangayDeactivated = 209,
        #endregion

        #region  Billing Meter Reading Remarks
        [Description("Meter Reading Remarks Added")]
        MeterReadingRemarksAdded = 210,
        [Description("Meter Reading Remarks Updated")]
        MeterReadingRemarksUpdated = 211,
        [Description("Meter Reading Remarks Deleted")]
        MeterReadingRemarksDeleted = 212,
        [Description("Meter Reading Remarks Activated")]
        MeterReadingRemarksActivated = 213,
        [Description("Meter Reading Remarks Deactivated")]
        MeterReadingRemarksDeactivated = 214,
        #endregion

        #region Credit Card
        [Description("Credit Card Deactivated")]
        CreditCardDeactivated = 215,
        [Description("Credit Card Activated")]
        CreditCardActivated = 216,
        [Description("Credit Card Added")]
        CreditCardAdded = 217,
        [Description("Credit Card Updated")]
        CreditCardUpdated = 218,
        [Description("Credit Card Deleted")]
        CreditCardDeleted = 219,
        #endregion

        #region  Currency

        [Description("Currency Added")]
        CurrencyAdded = 220,
        [Description("Currency Updated")]
        CurrencyUpdated = 221,
        [Description("Currency Deleted")]
        CurrencyDeleted = 222,

        #endregion

        #region Projects
        [Description("Projects Added")]
        ProjectsAdded = 223,
        [Description("Projects Updated")]
        ProjectsUpdated = 224,
        [Description("Projects Deleted")]
        ProjectsDeleted = 225,
        #endregion

        #region Division
        [Description("Division Added")]
        DivisionAdded = 226,
        [Description("Division Updated")]
        DivisionUpdated = 227,
        [Description("Division Deleted")]
        DivisionDeleted = 228,
        [Description("Division Activated")]
        DivisionActivated = 229,
        [Description("Division Deactivated")]
        DivisionDeactivated = 230,
        #endregion

        #region Nature Type

        [Description("Nature Type Created")]
        NatureTypeCreated = 231,
        [Description("Nature Type Updated")]
        NatureTypeUpdated = 232,
        [Description("Nature Type Deleted")]
        NatureTypeDeleted = 233,
        [Description("Nature Type Deactivated")]
        NatureTypeDeactivated = 234,
        [Description("Nature Type Activated")]
        NatureTypeActivated = 235,

        #endregion

        #region Task To Be Perform

        [Description("Task To Be Perform Created")]
        TaskToBePerformCreated = 236,
        [Description("Task To Be Perform Updated")]
        TaskToBePerformUpdated = 237,
        [Description("Task To Be Perform Deleted")]
        TaskToBePerformDeleted = 238,
        [Description("Task To Be Perform Deactivated")]
        TaskToBePerformDeactivated = 239,
        [Description("Task To Be Perform Activated")]
        TaskToBePerformActivated = 240,

        #endregion

        #region Bank
        [Description("Bank Created")]
        BankCreated = 241,
        [Description("Bank Updated")]
        BankUpdated = 242,
        [Description("Bank Deleted")]
        BankDeleted = 243,
        #endregion

        #region House Bank Account
        [Description("House Bank Account Created")]
        HouseBankAccountCreated = 244,
        [Description("House Bank Account Updated")]
        HouseBankAccountUpdated = 245,
        [Description("House Bank Account Deleted")]
        HouseBankAccountDeleted = 246,
        #endregion

        #region Country

        [Description("Country Created")]
        CountryCreated = 247,

        [Description("Country Updated")]
        CountryUpdated = 248,

        [Description("Country Deleted")]
        CountryDeleted = 249,
        [Description("Country Deactivated")]
        CountryDeactivated = 250,

        [Description("Country Activated")]
        CountryActivated = 251,

        #endregion

        #region  Lifeline Subsidy

        [Description("Lifeline Subsidy Added")]
        LifelineSubsidyAdded = 252,

        [Description("Lifeline Subsidy Updated")]
        LifelineSubsidyUpdated = 253,

        [Description("Lifeline Subsidy Deleted")]
        LifelineSubsidyDeleted = 254,

        [Description("Lifeline Subsidy Deactivated")]
        LifelineSubsidyDeactivated = 255,

        [Description("Lifeline Subsidy Activated")]
        LifelineSubsidyActivated = 256,

        #endregion

        [Description("Job Order Received")]
        JobOrderReceived = 257,

        #region Forward
        [Description("Job Order Forwarded")]
        JobOrderForwarded = 258,

        #endregion

        #region Vehicles

        #region Vehicle Type
        [Description("Vehicle Type Added")]
        VehicleTypeAdded = 259,
        [Description("Vehicle Type Updated")]
        VehicleTypeUpdated = 260,
        [Description("Vehicle Type Deleted")]
        VehicleTypeDeleted = 261,
        [Description("Vehicle Type Deactivated")]
        VehicleTypeDeactivated = 262,
        [Description("Vehicle Type Activated")]
        VehicleTypeActivated = 263,
        #endregion



        #endregion

        #region Vendor Group

        [Description("Vendor Group Created")]
        VendorGroupCreated = 264,
        [Description("Vendor Group Updated")]
        VendorGroupUpdated = 265,
        [Description("Vendor Group Deleted")]
        VendorGroupDeleted = 266,
        [Description("Vendor Group Reactivated")]
        VendorGroupReactivated = 267,
        [Description("Vendor Group Deactivated")]
        VendorGroupDeactivated = 268,

        #endregion

        #region Shipping Type

        [Description("Shipping Type Created")]
        ShippingTypeCreated = 269,
        [Description("Shipping Type Updated")]
        ShippingTypeUpdated = 270,
        [Description("Shipping Type Deleted")]
        ShippingTypeDeleted = 271,
        [Description("Shipping Type Reactivated")]
        ShippingTypeReactivated = 272,
        [Description("Shipping Type Deactivated")]
        ShippingTypeDeactivated = 273,

        #endregion

        #region Manufacturer
        [Description("Manufacturer Added")]
        ManufacturerAdd = 274,
        [Description("Manufacturer Updated")]
        ManufacturerUpdated = 275,
        [Description("Manufacturer Deleted")]
        ManufacturerDeleted = 276,
        [Description("Manufacturer Deactivated")]
        ManufacturerDeactivated = 277,
        [Description("Manufacturer Activated")]
        ManufacturerActivated = 278,
        #endregion

        #region Customer Group

        [Description("Customer Group Created")]
        CustomerGroupCreated = 279,
        [Description("Customer Group Updated")]
        CustomerGroupUpdated = 280,
        [Description("Customer Group Deleted")]
        CustomerGroupDeleted = 281,
        [Description("Customer Group Reactivated")]
        CustomerGroupReactivated = 282,
        [Description("Customer Group Deactivated")]
        CustomerGroupDeactivated = 283,

        #endregion

        #region Gas Station

        [Description("Gas Station Created")]
        GasStationCreated = 284,
        [Description("Gas Station Updated")]
        GasStationUpdated = 285,
        [Description("Gas Station Deleted")]
        GasStationDeleted = 286,
        [Description("Gas Station Reactivated")]
        GasStationReactivated = 287,
        [Description("Gas Station Deactivated")]
        GasStationDeactivated = 288,

        #endregion

        #region Sales Employee 

        [Description("Sales Employee Created")]
        SalesEmployeeCreated = 289,
        [Description("Sales Employee Updated")]
        SalesEmployeeUpdated = 290,
        [Description("Sales Employee Deleted")]
        SalesEmployeeDeleted = 291,

        #endregion

        #region Coop Vehicles

        [Description("Coop Vehicles Added")]
        CoopVehiclesAdded = 292,
        [Description("Coop Vehicles Updated")]
        CoopVehiclesUpdated = 293,
        [Description("Coop Vehicles Deleted")]
        CoopVehiclesDeleted = 294,
        [Description("Coop Vehicles Re-Assigned")]
        CoopVehiclesReassigned = 295,
        [Description("Coop Vehicles Assigned")]
        CoopVehiclesAssigned = 296,

        #endregion

        #region Collection denomination
        [Description("Collection Denomination Created")]
        CollectionDenominationCreated = 297,
        [Description("Collection Denomination Updated")]
        CollectionDenominationUpdated = 298,
        #endregion

        #region Drivers

        [Description("Employee With License No Created")]
        EmployeeWithLicenseNoCreated = 299,
        [Description("Employee With License No Updated")]
        EmployeeWithLicenseNoUpdated = 300,

        #endregion

        #region Vehicle Request

        [Description("Vehicle Request Created")]
        VehicleRequestCreated = 304,
        [Description("Vehicle Request Updated")]
        VehicleRequestUpdated = 305,
        [Description("Vehicle Request Deleted")]
        VehicleRequestDeleted = 306,

        #endregion

        #region Records Of Travel

        [Description("Records Of Travel For Departure Created")]
        RecordsOfTravelForDepartureCreated = 307,
        [Description("Records Of Travel For Arrival Updated")]
        RecordsOfTravelForArrivalCreated = 308,

        #endregion

        #region Vehicle Request

        [Description("Vehicle Inspection Inspected")]
        VehicleInspectionInspected = 309,
        [Description("Vehicle Request Re-Inspected")]
        VehicleInspectionReInspected = 310,
        #endregion

        [Description("Licence Renewed")]
        LicenceRenewed = 311,
        [Description("Gas Slip Issued")]
        GasSlipIssued = 312,
        [Description("Vehicle Endorsed To Motorpol Section")]
        VehicleEndorsedToMotorpolSection = 313,
        [Description("Vehicle Repair In Motorpol Section")]
        VehicleRepairInMotorpolSection = 314,

        #region Industry

        [Description("Industry Created")]
        IndustryCreated = 315,
        [Description("Industry Updated")]
        IndustryUpdated = 316,
        [Description("Industry Deleted")]
        IndustryDeleted = 317,
        [Description("Industry Deactivated")]
        IndustryDeactivated = 318,
        [Description("Industry Activated")]
        IndustryActivated = 319,

        [Description("Pricelist Created")]
        PriceListCreated = 320,
        [Description("Pricelist Updated")]
        PriceListUpdated = 321,
        [Description("Pricelist Deleted")]
        PricelistDeleted = 322,
        [Description("Pricelist Deactivated")]
        PricelistDeactivated = 323,
        [Description("Pricelist Activated")]
        PricelistActivated = 324,

        #endregion

        #region Travel Order
        [Description("Travel Order Created")]
        TravelOrderCreated = 325,
        [Description("Travel Order Updated")]
        TravelOrderUpdated = 326,
        [Description("Industry Deleted")]
        TravelOrderDeleted = 327,
        #endregion

        #region ItemGroup
        [Description("Item Group Added")]
        ItemGroupAdded = 328,
        [Description("Item Group Updated")]
        ItemGroupUpdated = 329,
        [Description("Item Group Deleted")]
        ItemGroupDeleted = 330,
        [Description("Item Group Reactivated")]
        ItemGroupReactivated = 331,
        [Description("Item Group Deactivated")]
        ItemGroupDeactivated = 332,
        #endregion

        #region Cycle Group 

        [Description("Cycle Group Created")]
        CycleGroupCreated = 334,
        [Description("Cycle Group Updated")]
        CycleGroupUpdated = 335,
        [Description("Cycle Group Deleted")]
        CycleGroupDeleted = 336,
        [Description("Cycle Group Deactivated")]
        CycleGroupDeactivated = 337,
        [Description("Cycle Group Activated")]
        CycleGroupActivated = 338,

        #endregion

        #region Industry

        [Description("Cash Discount Created")]
        CashDiscountCreated = 339,
        [Description("Cash Discount Updated")]
        CashDiscountUpdated = 340,
        [Description("Cash Discount Deleted")]
        CashDiscountDeleted = 341,
        [Description("Cash Discount Deactivated")]
        CashDiscountDeactivated = 342,
        [Description("Cash Discount Activated")]
        CashDiscountActivated = 343,
        #endregion

        #region Order Interval

        [Description("Order Interval Created")]
        OrderIntervalCreated = 344,
        [Description("Order Interval Updated")]
        OrderIntervalUpdated = 345,
        [Description("Order Interval Deleted")]
        OrderIntervalDeleted = 346,
        [Description("Order Interval Deactivated")]
        OrderIntervalDeactivated = 347,
        [Description("Order Interval Activated")]
        OrderIntervalActivated = 348,

        #endregion

        #region Item Master
        [Description("Item Master Created")]
        ItemMasterCreated = 349,
        [Description("Item Master Updated")]
        ItemMasterUpdated = 350,
        [Description("Item Master Deleted")]
        ItemMasterDeleted = 351,
        [Description("Item Master Deactivated")]
        ItemMasterDeactivated = 352,
        [Description("Item Master Activated")]
        ItemMasterActivated = 353,

        #endregion

        #region Package Types 

        [Description("Package Type Deactivated")]
        PackageTypeDeactivated = 354,
        [Description("Package Type Activated")]
        PackageTypeActivated = 355,
        [Description("Package Type List Updated")]
        PackageTypeListUpdated = 356,

        #endregion

        #region Inventory Transfer

        [Description("Inventory Transfer Updated")]
        InventoryTransferUpdated = 357,
        [Description("Inventory Transfer Deleted")]
        InventoryTransferDeleted = 358,
        [Description("Inventory Transfer Transferred")]
        InventoryTransferTransferred = 359,
        [Description("Inventory Transfer Recommended For Approval")]
        InventoryTransferRecommendedForApproval = 360,
        [Description("Inventory Transfer Created")]
        InventoryTransferCreated = 361,

        #endregion Inventory Transfer

        [Description("Save For Disconnection Action Taken")]
        SaveForDisconnectionActionTaken = 362,
        [Description("Payment End of Day")]
        PaymentEndOfDay = 363,
        [Description("Payment Start of Day")]
        PaymentStartOfDay = 364,

        #region BOM

        [Description("Bill Of Materials Added")]
        BillOfMaterialsAdded = 365,
        [Description("Bill Of Materials Updated")]
        BillOfMaterialsUpdated = 366,
        [Description("Bill Of Materials Deleted")]
        BillOfMaterialsDeleted = 367,
        #endregion

        #region Inventory Receiving
        [Description("Inventory Receiving Updated")]
        InventoryReceivingUpdated = 368,
        [Description("Inventory Receiving Deleted")]
        InventoryReceivingDeleted = 369,
        [Description("Inventory Receiving Received")]
        InventoryReceivingReceived = 370,
        [Description("Inventory Receiving Created")]
        InventoryReceivingCreated = 371,
        [Description("Inventory Receiving Recommended For Approval")]
        InventoryReceivingRecommendedForApproval = 372,

        [Description("User Group Created")]
        UserGroupCreated = 373,
        [Description("User Group Updated")]
        UserGroupUpdated = 374,
        [Description("User Group Reactivate Deactivate")]
        UserGroupReactivateDeactivate = 375,
        [Description("User Group Deleted")]
        UserGroupDeleted = 376,

        [Description("Configuration Settings Updated")]
        ConfigurationSettingsUpdated = 377,

        #region NavLink

        [Description("Nav Link Deleted")]
        NavLinkDeleted = 157,

        [Description("Nav Link Created")]
        NavlinkCreated = 378,
        [Description("Nav Link Updated")]
        NavlinkUpdated = 379,

        #endregion
        [Description("Permission Updated")]
        PermissionUpdated = 380,


        [Description("Warehouse Created")]
        WarehouseCreated = 381,
        [Description("Warehouse Updated")]
        WarehouseUpdated = 382,
        [Description("Warehouse Deactivate Reactivate")]
        WarehouseDeactivateReactivate = 383,
        [Description("Warehouse Deleted")]
        WarehouseDeleted = 384,
        #endregion

        #region Customer/Vendor
        [Description("Customer Created")]
        CustomerCreated = 385,
        [Description("Customer Updated")]
        CustomerUpdated = 386,
        [Description("Vendor Created")]
        VendorCreated = 387,
        [Description("Vendor Updated")]
        VendorUpdated = 388,
        #endregion

        [Description("Posting Period Created")]
        PostingPeriodCreated = 389,
        [Description("Posting Period Updated")]
        PostingPeriodUpdated = 390,
        [Description("Tax Group Created")]
        TaxGroupCreated = 391,
        [Description("Tax Group Updated")]
        TaxGroupUpdated = 392,
        [Description("G/L Accounts Created")]
        GLAccountCreated = 393,
        [Description("G/L Accounts Updated")]
        GLAccountUpdated = 394,
        [Description("Length And Width UoM Created")]
        LengthAndWidthUoMCreated = 395,
        [Description("Length And Width UoM Updated")]
        LengthAndWidthUoMUpdated = 396,
        [Description("Length And Width UoM Deleted")]
        LengthAndWidthUoMDeleted = 397,
        [Description("WeightUoM Created")]
        WeightUoMCreated = 398,
        [Description("WeightUoM Updated")]
        WeightUoMUpdated = 399,
        [Description("WeightUoM Deleted")]
        WeightUoMDeleted = 400,

        #region Unit Of Measure
        [Description("Unit of Measure Created")]
        UnitOfMeasureCreated = 401,
        [Description("Unit of Measure Updated")]
        UnitOfMeasureUpdated = 402,
        [Description("Unit of Measure Deleted")]
        UnitOfMeasureDeleted = 403,
        [Description("Unit of Measure Deactivated")]
        UnitOfMeasureDeactivated = 404,
        [Description("Unit of Measure Reactivated")]
        UnitOfMeasureReactivated = 405,
        #endregion

        #region WithholdingTax
        [Description("Withholding Tax Added")]
        WithholdingTaxAdded = 406,
        [Description("Withholding Tax Updated")]
        WithholdingTaxUpdated = 407,
        [Description("Withholding Tax Deleted")]
        WithholdingTaxDeleted = 408,
        [Description("Withholding Tax Deactivated")]
        WithholdingTaxDeactivated = 409,
        [Description("Withholding Tax Reactivated")]
        WithholdingTaxReactivated = 410,
        #endregion

        #region Other Request
        [Description("Other Request Created")]
        OtherRequestCreated = 411,
        [Description("Other Request Updated")]
        OtherRequestUpdated = 412,
        [Description("Other Request Deleted")]
        OtherRequestDeleted = 413,
        [Description("Other Request Processed")]
        OtherRequestProcessed = 414,
        #endregion

        #region Counter setup
        [Description("CounterSetupAdded")]
        CounterSetupAdded = 415,
        [Description("CounterSetupUpdated")]
        CounterSetupUpdated = 416,
        [Description("TellerCheckedIn")]
        TellerCheckedIn = 417,
        [Description("TellerLogOut")]
        TellerLogOut = 418,
        #endregion

        [Description("Avatar Changed")]
        ChangeAvatar = 419,

        [Description("Password Changed")]
        ChangePassword = 420,

        [Description("Approver Labels Updated")]
        ApproverLabelsUpdated = 421,

        [Description("Issue For Connection Endorsed")]
        IssueForConnectionEndorsed = 423,

        #region Material Request

        [Description("Material Request Added")]
        MaterialRequestAdded = 424,
        [Description("Material Request Updated")]
        MaterialRequestUpdated = 425,
        [Description("Material Request Canceled")]
        MaterialRequestCanceled = 426,
        [Description("Material Request Recommended ForApproval")]
        MaterialRequestRecommendedForApproval = 427,

        #endregion

        [Description("Change of Name Processed")]
        ChangeOfNameProcessed = 428,

        [Description("Change of Name Endorsed")]
        ChangeOfNameEndorsed = 429,

        #region Meter

        [Description("Meter Created")]
        MeterCreated = 430,

        [Description("Meter Updated")]
        MeterUpdated = 431,

        [Description("Meter Deleted")]
        MeterDeleted = 432,

        [Description("KHW Meter Withdrawn")]
        KHWMeterWithdrawn = 433,
        #endregion

        [Description("Net Metering Processed")]
        NetMeteringProcessed = 434,

        [Description("Billing Monthly Rates Cloned")]
        BillingMonthlyRatesCloned = 435,

        #region Billing Announcement For SOA
        [Description("Billing Announcement For SOA Created")]
        BillingAnnouncementForSOACreated = 436,

        [Description("Billing Announcement For SOA Updated")]
        BillingAnnouncementForSOAUpdated= 437,

        [Description("Billing Announcement For SOA Deleted")]
        BillingAnnouncementForSOADeleted = 438,

        #endregion

        #region Contestable Application

        [Description("Contestable Application Added")]
        ContestableApplicationAdded = 439,

        [Description("Contestable Application Updated")]
        ContestableApplicationUpdated= 440,

        [Description("Contestable Application Deleted")]
        ContestableApplicationDeleted = 441,

        [Description("Contestable Application Recommended")]
        ContestableApplicationRecommended= 442,

        #endregion

        [Description("Job Order Accomplished")]
        JobOrderAccomplished = 422,

        [Description("Job Order Unaccomplished")]
        JobOrderUnaccomplished = 443,

        [Description("Account Master File Updated")]
        AccountMasterFileUpdated = 444,
        [Description("Route Meter Reading Deleted")]
        RouteMeterReadingDeleted = 445,
        [Description("Route Meter Reading Day Updated")]
        RouteMeterReadingDayUpdated = 446,
        #region Billign Period
        [Description("Billing Period Added")]
        BillingPeriodAdded = 447,
        [Description("Billing Period Updated")]
        BillingPeriodUpdated = 448,
        [Description("Billing Period Deleted")]
        BillingPeriodDeleted = 449,
        [Description("Billing Period Deactivated")]
        BillingPeriodDeactivated = 450,
        [Description("Billing Period Reactivated")]
        BillingPeriodReactivated = 451,
        #endregion

        #region Transaction Code

        [Description("Transaction Control Number Created")]
        TransactionControlNumberCreated = 452,
        [Description("Transaction Control Number Updated")]
        TransactionControlNumberUpdated = 453,
        [Description("Transaction Control Number Deleted")]
        TransactionControlNumberDeleted = 454,
        [Description("Transaction Control Number Reactivated")]
        TransactionControlNumberReactivated = 456,
        [Description("Transaction Control Number Deactivated")]
        TransactionControlNumberDeactivated = 457,

        #endregion

        #region Payment Term
        [Description("Payment Term Created")]
        PaymentTermCreated = 458,
        [Description("Payment Term Updated")]
        PaymentTermUpdated = 459,
        [Description("Payment Term Deleted")]
        PaymentTermDeleted = 460,
        [Description("Payment Term Deactivated")]
        PaymentTermDeactivated = 461,
        [Description("Payment Term Activated")]
        PaymentTermActivated = 462,
        #endregion

        #region BP
        [Description("Business Partner Created")]
        BusinessPartnerCreated = 463,
        [Description("Business Partner Updated")]
        BusinessPartnerUpdated = 464,
        [Description("Business Partner Deleted")]
        BusinessPartnerDeleted = 465,
        [Description("Business Partner Deactivated")]
        BusinessPartnerDeactivated = 466,
        [Description("Business Partner Activated")]
        BusinessPartnerActivated = 467,
        #endregion

        #region Stock Requisition Voucher 

        [Description("Requisition Voucher Added")]
        RequisitionVoucherAdded = 468,
        [Description("Requisition Voucher Updated")]
        RequisitionVoucherUpdated = 469,
        [Description("Requisition Voucher Canceled")]
        RequisitionVoucherCanceled = 470,
        [Description("Requisition Voucher Recommended For Approval")]
        RequisitionVoucherRecommendedForApproval = 471,

        #endregion


        [Description("Tax Group Deleted")]
        TaxGroupDeleted = 472,
        [Description("Tax Group Deactivated")]
        TaxGroupDeactivated = 473,

        [Description("Tax Group Activated")]
        TaxGroupActivated = 474,

        #region Transformer Rental

        [Description("Transformer Rental Added")]
        TransformerRentalAdded = 475,

        [Description("Transformer Rental  Updated")]
        TransformerRentalUpdated = 476,

        [Description("Transformer Rental  Deleted")]
        TransformerRentalDeleted = 478,

        [Description("Transformer Rental  Recommended")]
        TransformerRentalRecommended = 479,

        [Description("Set Transformer Rental Witnesses")]
        SetTransformerRentalWitness = 480,
        [Description("Transformer Rental Terminated")]
        TransformerRentalTerminated = 481,
        [Description("Transformer Rental Renewed")]
        TransformerRentalRenewed= 482,
        #endregion

        #region Change Of Meter
        [Description("Change of Meter Added")]
        ChangeOfMeterAdded = 483,
        [Description("Change of Meter Updated")]
        ChangeOfMeterUpdated = 484,
        [Description("Change of Meter Deleted")]
        ChangeOfMeterDeleted = 485,
        [Description("Change of Meter Recommended")]
        ChangeOfMeterRecommended = 486,
        #endregion

        #region Purpose
        [Description("Purpose Added")]
        PurposeAdded = 487,

        [Description("Purpose Updated")]
        PurposeUpdated = 488,
        #endregion

        #region Special Lighting
        [Description("Set Special Lighting Account Witnesses")]
        SetSpecialLightingAccountWitnesses = 489,
        [Description("Process Special Lighting Account")]
        ProcessSpecialLightingAccount = 490,
        [Description("Special Lighting Account Renewal")]
        SpecialLightingAccountRenewal = 491,
        [Description("Special Lighting Account Termination")]
        SpecialLightingAccountTermination = 492,
        [Description("Special Lighting Account Conduct Feasib")]
        SpecialLightingAccountConductFeasib = 493,
        #endregion
        #region Billing Adjustment
        [Description("Billing Adjustment Created")]
        BillingAdjustmentCreated = 494,
        [Description("Billing Adjustment Updated")]
        BillingAdjustmentUpdated = 495,
        [Description("Billing Adjustment Deleted")]
        BillingAdjustmentDeleted = 496,
        [Description("Billing Adjustment Recommend For Approval")]
        BillingAdjustmentRecommendForApproval= 497,
        #endregion

        #region Report Signatory 

        [Description("Report Signatory Added")]
        ReportSignatoryAdded = 498,
        [Description("Report Signatory Updated")]
        ReportSignatoryUpdated = 499,
        [Description("Report Request Deleted")]
        ReportSignatoryDeleted = 500,
        [Description("Report Signatory Reactivated")]
        ReportSignatoryReactivated = 501,
        [Description("Report Signatory Deactivated")]
        ReportSignatoryDeactivated = 502,

        #endregion

        #region App User AppUser

        [Description("AppUser Created")]
        AppUserCreated = 503,
        [Description("AppUser Updated")]
        AppUserUpdated = 504,
        [Description("AppUser Deleted")]
        AppUserDeleted = 505,
        [Description("AppUser Deactivated")]
        AppUserDeactivated = 506,
        [Description("AppUser Activated")]
        AppUserActivated = 507,

        #endregion

        [Description("Job Order Returned")]
        JobOrderReturned = 508,
        [Description("Item Serial Number Created")]
        ItemSerialNumberCreated = 509,
        [Description("Item Serial Number Updated")]
        ItemSerialNumberUpdated = 510,

        #region Sales
        [Description("Sales Invoice Deleted")]
        SalesInvoiceDeleted = 511,

        [Description("Sales Invoice Deleted")]
        SalesDeliveryDeleted = 512, 
       
        [Description("Sales Invoice Deleted")]
        SalesOrderDeleted = 513,

        [Description("Sales Invoice Deleted")]
        SalesQuotationDeleted = 514,
        #endregion
        #region RS3
        [Description("Ticket Created")]
        TicketCreated = 508,
        [Description("Ticket Updated")]
        TicketUpdated = 509,
        [Description("Ticket Commented")]
        TicketCommented = 510,
        [Description("Ticket Commented and Resolved")]
        TicketCommentedAndResolved = 511,
        [Description("Ticket Resolved")]
        TicketResolved = 512,
        [Description("Ticket Reopened")]
        TicketReopened = 513,
        #endregion
    }
}