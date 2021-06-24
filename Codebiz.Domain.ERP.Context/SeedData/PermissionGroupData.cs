namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class PermissionGroupData
    {
        public const string Dashboard = "Dashboard";
        public const string Approval = "Approval";

        #region Admin

        public const string UserGroups = "User Groups";

        public const string Permissions = "Permissions";
        public const string PermissionGroups = "Permission Groups";

        public const string NavLinks = "Navigation Links";
        public const string ConfigSetting = "Config Setting";
        public const string AuditLogs = "Audit Logs";

        #endregion

        #region Management

        public const string CompanySetup = "CompanySetup";
        public const string GeneralSettings = "GeneralSetting";
        public const string CompanyDetails = "CompanyDetail";

        public const string User = "User";
        public const string Users = "Users";
        public const string Office = "Office";
        public const string Department = "Department";
        public const string Division = "Division";
        public const string Position = "Position";
        public const string Pole = "Pole";

        public const string Address = "Address";
        public const string Region = "Region";
        public const string Province = "Province";
        public const string CityTown = "City Town";
        public const string Barangay = "Barangay";
        public const string Purok = "Purok";
        public const string Sitio = "Sitio";
        public const string Route = "Route";

        public const string ConsumerType = "Consumer Type";
        public const string NoOfUnitsandKVARating = "No. Of Units and KVA Rating";
        //public const string WorkOrder = "Work Order";
        public const string SupportingDocuments = "Supporting Documents";
        public const string DocumentNumbering = "DocumentNumbering";
        public const string Substation = "Substation";
        public const string Purpose = "Purpose";
        //Approval
        public const string ApprovalProcedures = "Approval Procedures";
        public const string ApprovalStage = "Approval Stage";
        public const string ApprovalTemplate = "Approval Template";
        public const string ApproverLabel = "Approver Label";

        //Bank
        public const string Banks = "Banks";
        public const string CreditCard = "Credit Card";
        public const string HouseBankAccount = "HouseBankAccount";

        public const string ReportSignatory = "Report Signatory";

        #endregion

        #region CSA

        public const string Member = "Member";
        public const string Applicant = "Applicant";
        public const string Account = "Account";
        public const string HouseWiringInspection = "House Wiring Inspection";
        public const string PreMembershipOrientationSeminar = "Pre-Membership Orientation Seminar";

        //Connection
        public const string CSAConnections = "CSA Connections";
        public const string ForConnection = "For Connection";
        public const string ForDisconnection = "For Disconnection";
        public const string IssueForConnection = "Issue For Connection";

        #region Request/Application

        public const string CSARequestApplications = "CSA Request/Applications";
        public const string BurialAssistance = "Burial Assistance";
        public const string ChangeOfName = "Change Of Name";
        public const string OtherRequest = "Other Request";

        public const string Application = "Application";
        public const string DiscountApplication = "Discount Application";
        public const string NetMetering = "Net Metering";
        public const string ContestableApplication = "Contstable Application";
        public const string TransformerRental = "Transformer Rental";
        public const string ChangeOfMeter = "Change Of Meter";
        public const string BillingAdjustment = "Billing Adjustment";
        #endregion

        #endregion

        #region Job Order

        public const string CSAJobOrder = "CSA Job Order";
        public const string NatureTypes = "Nature Types";
        public const string TaskToBePerforms = "Task To Be Performs";
        public const string ComplaintType = "Complaint Type";
        public const string JobOrderRequestApplication = "Job Order Request or Application";
        public const string JobOrderComplaint = "Job Order Complaint";
        public const string AssignedJobOrderToEmployee = "Assigned JobOrder To Employee";
        public const string ProcessJobOrder = "Process JobOrder";

        #endregion

        #region Collection

        public const string Payment = "Payment";
        public const string PaymentEntry = "Payment Entry";
        public const string Surcharge = "Surcharge";
        public const string CounterSetup = "Counter Setup";
        #endregion

        #region Billing

        public const string BillingUnits = "Billing Units";
        public const string BillingUnbundledTransactions = "Billing Unbundled Transactions";
        public const string BillingCategories = "Billing Categories";
        public const string LifelineSubsidy = "Lifeline Subsidy";
        public const string BillingMonthlyRates = "Billing Monthly Rates";
        public const string MeterReadingRemarks = "Meter Reading Remarks";
        public const string MeterReading = "Meter Reading";
        public const string BillingTransactions = "Billing Transactions";
        public const string StatementOfAccount = "Statement Of Account";
        public const string BillingAnnouncementForSOA = "Billing Announcement For SOA";
        public const string BillingPeriod = "Billing Period";

        #endregion

        #region Inventory

        public const string Manufacturers = "Manufacturers";
        public const string PackagingTypes = "Packaging Types";
        public const string ShippingTypes = "Shipping Types";
        public const string Warehouses = "Warehouses";
        public const string CycleGroups = "Cycle Groups";
        public const string OrderIntervals = "Order Intervals"; 
        public const string ItemGroups = "Item Groups";

        public const string UnitOfMeasure = "Unit Of Measure";
        public const string LengthAndWidthUoM = "Length and Width UoM";
        public const string WeightUoM = "Weight UoM";

        public const string PriceList = "Price List";

        public const string ItemMasters = "Item Masters";
        public const string ItemSerialManagement = "Item Serial Management";


        public const string InventoryTransfer = "Inventory Transfer";
        public const string InventoryReceiving = "Inventory Receiving";

        public const string InventoryGoodsExit = "Inventory Goods (E)xit";
        public const string InventoryGoodsEntry = "Inventory Goods E(N)try";

        public const string InventoryQtyOB = "Inventory Opening Balances";
        public const string InventoryQtyTracking = "Inventory Tracking";
        public const string InventoryQtyPosting = "Inventory Posting";


        public const string InventoryRevaluation = "Inventory Revaluation";

        #endregion

        #region Business Partner

        public const string Country = "Country";
        public const string SalesEmployee = "Sales Employee";
        public const string Industry = "Industry";
        public const string CashDiscount = "Cash Discount";
        public const string VendorGroup = "Vendor Group";
        public const string CustomerGroup = "Customer Group";

        public const string PaymentTerms = "Payment Terms";

        public const string BPVendors = "BP Vendors";
        public const string BPCustomers = "BP Customers";
        public const string ReconcileBP = "ITRBP";

        #endregion

        #region Sales A/R

        public const string SalesQuotation = "Sales Quotation";
        public const string SalesOrder = "Sales Order";
        public const string SalesDelivery = "Sales Delivery";
        public const string SalesReturn = "Sales Return";
        public const string SalesDownPayment = "Sales Down Payment";
        public const string SalesInvoice = "Sales Invoice";
        public const string SalesInvoiceSPY = "A/R Invoice + Payment";
        public const string SalesReserveInvoice = "Sales Reserve Invoice";
        public const string SalesCreditNote = "Sales Credit Note";

        #endregion

        #region Purchase A/P

        public const string PurchaseQuotation = "Purchase Quotation";
        public const string PurchaseOrder = "Purchase Order";
        public const string PurchaseDelivery = "Purchase Delivery";
        public const string PurchaseReturn = "Purchase Return";
        public const string PurchaseDownPayment = "Purchase Down Payment";
        public const string PurchaseInvoice = "Purchase Invoice";
        public const string PurchaseInvoicePPY = "A/P Invoice + Payment";
        public const string PurchaseReserveInvoice = "Purchase Reserve Invoice";
        public const string PurchaseCreditNote = "Purchase Credit Note";

        #endregion


        #region Payment
        public const string PaymentAndCollection = "Payment/Collection";
        public const string OutgoingPayments = "Outgoing Payments";
        public const string OutgoingPaymentEntry = "Outgoing Payment Entry";
        public const string ChecksForPayment = "Checks for Payment";
        public const string VoidChecksForPayment = "Void Checks for Payment";

        public const string IncomingPayments = "Incoming Payments";
        public const string IncomingPaymentEntry = "Incoming Payment Entry";
        public const string CheckRegister = "Check Register";

        public const string Deposits = "Deposits";
        public const string Deposit = "Deposit";
        public const string PostDatedChkDeposit = "PostDated Check Deposit";
        public const string PostDatedCVDeposit = "PostDated CreditVoucher Deposit";
        #endregion

        #region Financial

        public const string Projects = "Projects";
        public const string Currency = "Currency";

        public const string TaxGroup = "Tax Group";
        public const string WithholdingTax = "Withholding Tax";

        public const string PostingPeriod = "Posting Period";
        public const string PeriodIndicator = "Period Indicator";

        public const string ChartOfAccount = "Chart Of Account";
        public const string JournalEntry = "Journal Entry";
        public const string JournalVoucher = "Journal Voucher";
        public const string GLAccoountSetup = "G/L Account Setup";
        public const string TransactionControlNumber = "Transaction Control Number";


        public const string FinancialReports = "Financial Reports";
        
        public const string Accounting = "Accounting";
        public const string GeneralLedger = "General Ledger";
        public const string TransactionJournal = "Transaction Journal";
        public const string TransactionByProject = "Transaction By Project";
        public const string DocumentJournal = "Document Journal";
        public const string VendorAging = "Vendor Aging Report";
        public const string CustomerAging = "Customer Aging Report";

        public const string Financial = "Financial";
        public const string BalanceSheet = "BalanceSheet";
        public const string TrialBalance = "Trial Balance";
        public const string PNL = "Profit & Loss Statement";
        public const string CashFlow = "Cash Flow";

        public const string ReconcileGL = "ITRGL";

        #endregion

        #region Production

        public const string BillOfMaterials = "Bill Of Materials";

        #endregion

        #region Vehicle

        public const string VehicleType = "Vehicle Type";
        public const string CoopVehicles = "Coop Vehicles";
        public const string GasStation = "Gas Station";
        public const string VehicleDriver = "Vehicle Driver";
        public const string VehicleRequest = "Vehicle Request";
        public const string VehicleInspection = "Vehicle Inspection";

        public const string RecordsOfTravel = "Records Of Travel";

        public const string MotorpolSection = "Motorpol Section";
        public const string TravelOrder = "Travel Order";

        #endregion

        #region Reports

        public const string CSAReports = "CSA Reports";
        public const string HouseWiringInspectionReport = "House Wiring Inspection Report";
        public const string ConnectionReport = "Connection Report";
        public const string ComplaintReport = "Complaint Report";
        public const string CashCountReport = "Cash Count Report";
        public const string MeterLabReport = "Meter Lab Report";
        public const string SeniorCitizenDiscountReport = "Senior Citizen Discount Report";
        public const string MemberReport = "Member Report";
        public const string ComplaintReceivedAndAttendedReport = "Complaint Received And Attended Report";
        public const string ConsumerForConnectionReport = "Consumer For Connection Report";
        public const string ConsumerForDisconnectionReport = "Consumer For Disconnection Report";

        #endregion

        #region Metering
        public const string KWHMeter = "KWH Meter";

        public const string MeterForCalibrating = "Meter for Calibrating";
        public const string WithdrawalKWHMeter = "Withdrawal KWH Meter";

        #endregion

        #region Transformer

        public const string TransformerForTesting = "Transformer for Testing";
        public const string TransformerForInventory = "Transformer for Inventory";
        public const string TransformerOutgoingDistribution = "Outgoing Distribution";

        #endregion

        #region Material

        //Material Request
        public const string MaterialRequest = "Material Request";
        //Stock Requisition Voucher
        public const string StockRequisitionVoucher = "Stock Requisition Voucher";
        //Stock Requisition Voucher
        public const string PurchaseRequisitionVoucher = "Purchase Requisition Voucher";

        #endregion
    }
}