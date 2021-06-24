namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class NavLinkData
    {
        public const string Dashboard = "Dashboard";
        public const string Approval = "Approval";
        public const string Management = "Management";

        #region Admin

        public const string Admin = "Admin";
        public const string UserGroups = "User Groups";
        public const string Permissions = "Permissions";
        public const string NavLinks = "Navigation Links";
        public const string ConfigSetting = "Configuration Settings";
        public const string AuditLogs = "Audit Logs";

        #endregion

        #region Management

        public const string CompanySetup = "Company Setup";
        public const string GeneralSetting = "General Setting";
        public const string CompanyDetail = "Company Detail";

        public const string Users = "Users";
        public const string Offices = "Offices";
        public const string Departments = "Departments";
        public const string Divisions = "Divisions";
        public const string Positions = "Positions";
        public const string Poles = "Poles";

        public const string Address = "Address";
        public const string Regions = "Regions";
        public const string Provinces = "Provinces";
        public const string CitiesTowns = "Cities / Towns";
        public const string Barangays = "Barangays";
        public const string Puroks = "Puroks";
        public const string Sitios = "Sitios";
        public const string Routes = "Routes";

        public const string ConsumerTypes = "Consumer Types";
        public const string NoOfUnitsandKVARating = "No. Of Units and KVA Rating";
        //public const string WorkOrders = "Work Orders"; // TO REMOVE
        public const string Purpose = "Purposes";
        public const string SupportingDocuments = "Supporting Documents";
        public const string DocumentNumberings = "Document Numberings";
        public const string Substations = "Substations";
        public const string LifelineSubsidy = "Lifeline Subsidy";

        public const string ApprovalProcedures = "Approval Procedures";
        public const string ApprovalStages = "Approval Stages";
        public const string ApprovalTemplates = "Approval Templates";
        public const string ApproverLabels = "Approver Labels";
        

        public const string Banking = "Banking";
        public const string Banks = "Banks";
        public const string CreditCards = "Credit Cards";
        public const string HouseBankAccounts = "House Bank Accounts";

        public const string ReportSignatories = "Report Signatories";

        #endregion

        #region CSA

        public const string CommercialServicesApplications = "Customer Services";
        public const string Applicants = "Applicant";
        public const string Members = "Member";
        public const string PreMembershipOrientationSeminars = "Pre-Membership Orientation Seminars";
        public const string Accounts = "Accounts";
        public const string HouseWiringInspections = "House Wiring Inspections";

        #region Connection

        public const string Connections = "Connections";
        public const string IssueForConnections = "Issue For Connections";
        public const string ForConnections = "For Connections";
        public const string ForDisconnections = "For Disconnections";

        #endregion

        #region Request/Applications

        public const string Requests_Applications = "Requests/Applications";
        public const string DiscountApplications = "Discount Applications";
        public const string NetMeterings = "Net Meterings";
        public const string BurialAssistances = "Burial Assistance";
        public const string ChangeOfNames = "Change Of Name";
        public const string OtherRequests = "Other Requests";
        public const string ContestableApplications = "Contestable Applications";
        public const string TransformerRentals = "Transformer Rentals";
        public const string ChangeOfMeter = "Change Of Meter";
        public const string BillingAdjustment = "Billing Adjustments";
        #endregion

        #endregion

        #region Job Order

        public const string JobOrder = "Job Order";
        public const string NatureTypes = "Nature Types";
        public const string TaskToBePerforms = "Task To be Perform";
        public const string ComplaintTypes = "Complaint Types";
        public const string JobOrderComplaints = "Job Order Complaints";
        public const string JobOrderRequestApplications = "Job Order Requests";
        public const string AssignedJobOrderToEmployees = "For Assigned Job Order To Employee(s)";
        public const string ProcessJobOrder = "Process Job Order";

        #endregion

        #region Collection

        public const string Collection = "Collection";
        public const string Payments = "Payments";
        public const string PaymentEntry = "Payment Entry";
        public const string Surcharges = "Surcharges";
        public const string CounterSetup = "Counter Setup";

        #endregion

        #region Billing

        public const string Billing = "Billing";

        public const string MeterUnits = "Meter Units";
        public const string BillingUnbundledTransaction = "Unbundled Transactions";
        public const string BillingCategories = "Billing Categories";
        public const string BillingMonthlyRates = "Monthly Rates";
        public const string MeterReadingRemarks = "Meter Reading Remarks";
        public const string MeterReadings = "Meter Reading";
        public const string BillingTransactions = "Billing Transactions";
        public const string StatementOfAccounts = "Statement Of Accounts";
        public const string BillingAnnouncementsForSOA = "Billing Announcements For SOA";
        public const string BillingPeriod = "Billing Period";

        #endregion

        #region Inventory

        public const string Inventory = "Inventory";

        public const string Manufacturers = "Manufacturers";
        public const string PackageTypes = "Package Types";
        public const string ShippingTypes = "Shipping Types";
        public const string Warehouses = "Warehouses";
        public const string CycleGroups = "Cycle Groups";
        public const string OrderIntervals = "Order Intervals";
        public const string ItemGroups = "Item Groups";

        public const string UOMSetup = "Unit of Measure Setup";
        public const string UnitOfMeasure = "Unit of Measure";
        public const string LengthAndWidthUOM = "Length and Width UOM";
        public const string WeightUOM = "Weight UOM";

        public const string PriceLists = "Price Lists";

        public const string ItemMasters = "Item Masters";
        public const string ItemSerialManagement = "Serial Number Management";
        public const string ItemManagement = "Item Management";

        public const string InventoryTransactions = "Inventory Transactions";
        public const string InventoryTransfers = "Inventory Transfers";
        public const string InventoryReceivings = "Inventory Receivings";

        public const string InventoryGoodsExit = "Goods Issue";
        public const string InventoryGoodsEntry = "Goods Receipt";

        public const string InventoryQtyOB = "Opening Balances";
        public const string InventoryQtyTracking = "Inventory Tracking";
        public const string InventoryQtyPosting = "Inventory Posting";

        public const string InventoryRevaluation = "Inventory Revaluation ";


        #endregion

        #region Business Partner

        public const string BusinessPartner = "Business Partner";

        public const string Countries = "Countries"; // **
        public const string SalesPersons = "Sales Persons";
        public const string Industry = "Industry"; // **
        public const string CashDiscounts = "Cash Discounts"; // **
        public const string VendorGroups = "Vendor Groups"; // **
        public const string CustomerGroups = "Customer Groups"; // **
        //public const string BusinessPartnerGroups = "Business Partner Groups";

        public const string PaymentTerms = "Payment Terms";
        public const string BPVendors = "Vendors";
        public const string BPCustomers = "Customers";

        public const string ReconcileBP= "Internal Reconcilation(BP)";

        #endregion

        #region Sales A/R

        public const string SalesAR = "Sales A/R";

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

        #region Purchase A/R

        public const string PurchaseAP = "Purchase  A/P";

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

        public const string Financials = "Financials";

        public const string Projects = "Projects";
        public const string Currency = "Currency";

        public const string Tax = "Tax";
        public const string TaxGroup = "Tax Group";
        public const string WithholdingTax = "Withholding Tax";

        public const string PostingPeriod = "Posting Period";
        public const string PeriodIndicator = "Period Indicator";
        public const string ChartOfAccount = "Chart Of Account";
        public const string JournalEntry = "Journal Entry";
        public const string JournalVoucher = "Journal Voucher";
        public const string GLAccoountSetup = "G/L Account Setup";
        public const string TransactionControlNumber = "Transaction Control Number";


        //reports
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


        //GLreconcilation
        public const string ReconcileGL = "Internal Reconcilation(GL)";




        #endregion

        #region Production

        public const string Production = "Production";

        public const string BillOfMaterials = "Bill of Materials";

        #endregion

        #region Vehicle 

        public const string Vehicles = "Vehicles";

        public const string VehicleTypes = "Vehicle Types";
        public const string CoopVehicles = "Coop Vehicles";
        public const string GasStations = "Gas Stations";
        public const string EmployeesDriversLicense = "Employees Driver's License";
        public const string TravelOrders = "Travel Orders";
        public const string VehicleRequests = "Vehicle Requests";
        public const string VehicleInspections = "Vehicle Inspections";
        public const string RecordsofTravels = "Records of Travels";
        public const string MotorpolSections = "Motorpol Sections";

        #endregion

        #region Metering

        public const string Engineering = "Engineering";
        public const string Metering = "Metering";
        public const string Withdrawal = "Withdrawal KWH Meter";

        public const string MeterForCalibrating = "Meter for Calibrating";
        public const string Meter = "KWH Meter";

        #endregion

        #region Material

        public const string Material = "Material";
        public const string MaterialRequest = "Material Request";
        public const string StockRequisitionVoucher = "Stock Requisition Voucher";
        public const string PurchaseRequisitionVoucher = "Purchase Requisition Voucher";

        #endregion

        #region Transformer

        public const string Transformer = "Transformer";

        public const string TransformerForTesting = "Transformer for Testing";
        public const string TransformerForInventory = "Transformer for Inventory";
        public const string TransformerOutgoingDistribution = "Outgoing Distribution Transformer";

        #endregion

        #region Reports

        public const string Reports = "Reports";

        public const string MemberReport = "Member Report";
        public const string HouseWiringInspectionReport = "House Wiring Inspection Report";
        public const string ConnectionReport = "Connection Report";

        public const string ConsumerForConnectionReport = "Consumer for Connection Report";
        public const string ConsumerForDisconnectionReport = "Consumer for Disconnection Report";

        public const string ComplaintReport = "Complaint Report";
        public const string ComplaintReceivedAndAttendedReport = "Complaint Received and Attended Report";

        public const string CashCountReport = "Cash Count Report";
        public const string SeniorCitizenDiscountReport = "Senior Citizen Discount Report";
        public const string MeteringReport = "Metering Report";

        #endregion
    }
}