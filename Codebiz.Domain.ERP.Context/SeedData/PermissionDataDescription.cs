namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class PermissionDataDescription
    {
        #region Dashboard

        public const string UserCanViewDashboard = "UserCanViewDashboard";

        #endregion

        #region Approval

        public const string UserCanViewApprovalData = "UserCanViewApprovalData";
        public const string UserCanExportApprovalViewList = "UserCanExportApprovalData";
        public const string UserCanProcessApprovalData = "UserCanProcessApprovalData";
        public const string UserCanViewApprovalDetails = "UserCanViewApprovalDetails";
        public const string UserCanViewApprovalResult = "UserCanViewApprovalResult";

        #endregion

        #region Admin

        #region Permission

        public const string UserCanViewPermissionData = "UserCanViewPermissionData";
        public const string UserCanExportPermissionViewList = "UserCanExportPermissionData";
        public const string UserCanAddPermissionData = "UserCanAddPermissionData";
        public const string UserCanEditPermissionData = "UserCanEditPermissionData";
        public const string UserCanDeletePermissionData = "UserCanDeletePermissionData";

        #endregion

        #region Navlink

        public const string UserCanViewNavLink = "UserCanViewNavigationLinkData";
        public const string UserCanExportNavigationLinkViewList = "UserCanExportNavigationLinkData";
        public const string UserCanAddNavLink = "UserCanAddNavigationLinkData";
        public const string UserCanEditNavLink = "UserCanEditNavigationLinkData";
        public const string UserCanDeleteNavLink = "UserCanDeleteNavigaitonLinkData";

        #endregion

        #region Config Setting

        public const string UserCanViewConfigSettingData = "UserCanViewConfigurationSettingData";
        public const string UserCanEditConfigSettingData = "UserCanEditConfigurationSettingData";
        public const string UserCanExportConfigurationSettingViewList = "UserCanExportConfigurationSettingData";

        #endregion

        #region Logs

        public const string UserCanAuditViewLogs = "UserCanAuditViewLogs";

        #endregion

        #region User Group

        public const string UserCanViewUserGroup = "UserCanViewUserGroupData";
        public const string UserCanExportUserGroupViewList = "UserCanExportUserGroupData";
        public const string UserCanAddUserGroup = "UserCanAddUserGroupData";
        public const string UserCanEditUserGroup = "UserCanEditUserGroupData";
        public const string UserCanDeleteUserGroup = "UserCanDeleteUserGroupData";
        public const string UserCanDeactivateReactivateUserGroup = "UserCanDeactivateOrReactivateUserGroupData";

        #endregion

        #endregion

        #region Management

        #region Company Setup

        public const string UserCanViewGeneralSetting = "UserCanViewGeneralSetting";
        public const string UserCanEditGeneralSetting = "UserCanEditGeneralSetting";

        public const string UserCanViewCompanyDetail = "UserCanViewCompanyDetail";
        public const string UserCanEditCompanyDetail = "UserCanEditCompanyDetail";

        #endregion

        #region App User

        public const string UserCanViewAppUser = "UserCanViewUsersData";
        public const string UserCanExportUsers = "UserCanExportUsersData";
        public const string UserCanAddAppUser = "UserCanAddUsersData";
        public const string UserCanEditAppUser = "UserCanEditUsersData";
        public const string UserCanDeleteAppUser = "UserCanDeleteUsersData";
        public const string UserCanActivateDeactivateAppUser = "UserCanDeactivateOrReactivateUsersData";

        public const string UserCanResetPasswordOfAppUser = "UserCanResetPasswordOfAppUser";
        public const string UserCanActivateAppUserAccount = "UserCanActivateAppUserAccount";
        public const string UserCanUnlockAppUserAccount = "UserCanUnlockAppUserAccount";
        public const string UserCanViewAppUserProfile = "UserCanViewUsersDetails";
        #endregion

        #region Office

        public const string UserCanViewOfficeData = "UserCanViewOffice";
        public const string UserCanExportOfficesViewList = "UserCanExportOfficeData";
        public const string UserCanAddOfficeData = "UserCanAddOfficeData";
        public const string UserCanEditOfficeData = "UserCanEditOfficeData";
        public const string UserCanDeleteOfficeData = "UserCanDeleteOfficeData";
        public const string UserCanDeActivateReactivateOffice = "UserCanDeactivateOrReactivateOfficeData";
        public const string UserCanViewOfficeDetails = "UserCanViewOfficeDetails";

        #endregion

        #region Department

        public const string UserCanViewDepartmentData = "UserCanViewDepartmentData";
        public const string UserCanExportDepartmentViewList = "UserCanExportDepartmentData";
        public const string UserCanAddDepartmentData = "UserCanAddDepartmentData";
        public const string UserCanEditDepartmentData = "UserCanEditDepartmentData";
        public const string UserCanDeleteDepartmentData = "UserCanDeleteDepartmentData";
        public const string UserCanDeActivateReActivateDepartmentData = "UserCanDeactivateOrReactivateDepartmentData";

        #endregion

        #region Division

        public const string UserCanViewDivision = "UserCanViewDivision";
        public const string UserCanAddDivision = "UserCanAddDivision";
        public const string UserCanEditDivision = "UserCanEditDivision";
        public const string UserCanDeleteDivision = "UserCanDeleteDivision";
        public const string UserCanDeActivateReActivateDivision = "UserCanDeActivateReActivateDivision";
        public const string UserCanViewDivisionDetails = "UserCanViewDivisionDetails";
        public const string UserCanExportDivision = "UserCanExportDivision";

        #endregion

        #region Position

        public const string UserCanViewPosition = "UserCanViewPositionData";
        public const string UserCanExportPositions = "UserCanExportPositionData";
        public const string UserCanAddPosition = "UserCanAddPositionData";
        public const string UserCanEditPosition = "UserCanEditPositionData";
        public const string UserCanDeletePosition = "UserCanDeletePositionData";
        public const string UserCanDeActivateReActivatePosition = "UserCanDeactivateOrReactivatePositionData";

        #endregion

        #region Consumer Type
        public const string UserCanViewConsumerTypeData = "UserCanViewConsumerTypeData";
        public const string UserCanExportConsumerTypeViewList = "UserCanExportConsumerTypeData";
        public const string UserCanAddConsumerTypeData = "UserCanAddConsumerTypeData";
        public const string UserCanEditConsumerTypeData = "UserCanEditConsumerTypeData";
        public const string UserCanDeleteConsumerTypeData = "UserCanDeleteConsumerTypeData";
        public const string UserCanDeactivateReactivateConsumerTypeData = "UserCanDeactivateOrReactivateConsumerTypeData";

        #endregion

        #region Units And Kva Rating

        public const string UserCanViewNoOfUnitsAndKvaRatingData = "UserCanViewNoOfUnitsAndKvaRatingData";
        public const string UserCanExportNoOfUnitsAndKVARatingViewList = "UserCanExportNoOfUnitsAndKVARatingData";
        public const string UserCanAddNoOfUnitsAndKvaRatingData = "UserCanAddNoOfUnitsAndKvaRating";
        public const string UserCanEditNoOfUnitsAndKvaRatingData = "UserCanEditNoOfUnitsAndKvaRating";
        public const string UserCanDeleteNoOfUnitsAndKvaRatingData = "UserCanDeleteNoOfUnitsAndKvaRating";
        public const string UserCanDeActivateReActivateNoOfUnitsAndKvaRating = "UserCanDeactivateOrReactivateNoOfUnitsAndKvaRatingData";

        #endregion

        #region Work Order

        public const string UserCanViewWorkOrder = "UserCanViewWorkOrderData";
        public const string UserCanExportWorkOrder = "UserCanExportWorkOrderData";
        public const string UserCanAddWorkOrder = "UserCanAddWorkOrderData";
        public const string UserCanEditWorkOrder = "UserCanEditWorkOrderData";
        public const string UserCanDeleteWorkOrder = "UserCanDeleteWorkOrderData";
        public const string UserCanDeActivateReActivateWorkOrder = "UserCanDeactivateOrReactivateWorkOrderData";

        #endregion

        #region Supporting Documents

        public const string UserCanViewSupportingDocuments = "UserCanViewSupportingDocumentsData";
        public const string UserCanExportSupportingDocuments = "UserCanExportSupportingDocumentsData";
        public const string UserCanAddSupportingDocuments = "UserCanAddSupportingDocumentsData";
        public const string UserCanEditSupportingDocuments = "UserCanEditSupportingDocumentsData";
        public const string UserCanDeleteSupportingDocuments = "UserCanDeleteSupportingDocumentsData";
        public const string UserCanViewSupportingDocumentsDetails = "UserCanViewSupportingDocumentsDetails";

        #endregion

        #region Document Numbering

        public const string UserCanViewDocumentNumberingData = "UserCanViewDocumentNumberingData";
        public const string UserCanExportDocumentNumberingViewList = "UserCanExportDocumentNumberingData";
        public const string UserCanAddDocumentNumberingData = "UserCanAddDocumentNumberingData";
        public const string UserCanEditDocumentNumberingData = "UserCanEditDocumentNumberingData";
        public const string UserCanViewDetailsDocumentNumbering = "UserCanViewDetailsDocumentNumbering";

        #endregion

        #region Pole

        public const string UserCanViewPole = "UserCanViewPoleData";
        public const string UserCanExportPoles = "UserCanExportPoleData";
        public const string UserCanAddPole = "UserCanAddPoleData";
        public const string UserCanEditPole = "UserCanEditPoleData";
        public const string UserCanDeletePole = "UserCanDeletePoleData";
        public const string UserCanDeactivateReactivatePole = "UserCanDeactivateOrReactivatePoleData";

        #endregion

        #region SubStation

        public const string UserCanViewSubstation = "UserCanViewSubstationData";
        public const string UserCanExportSubstation = "UserCanExportSubstationData";
        public const string UserCanAddSubstation = "UserCanAddSubstationData";
        public const string UserCanEditSubstation = "UserCanEditSubstationData";
        public const string UserCanDeleteSubstation = "UserCanDeleteSubstationData";
        public const string UserCanDeactivateReactivateSubstation = "UserCanDeactivateOrReactivateSubstation";

        #endregion

        #region Lifeline Subsidy

        public const string UserCanViewLifelineSubsidy = "UserCanViewLifelineSubsidy";
        public const string UserCanExportLifelineSubsidies = "UserCanExportLifelineSubsidies";
        public const string UserCanAddLifelineSubsidy = "UserCanAddLifelineSubsidy";
        public const string UserCanEditLifelineSubsidy = "UserCanEditLifelineSubsidy";
        public const string UserCanDeleteLifelineSubsidy = "UserCanDeleteLifelineSubsidy";
        public const string UserCanDeActivateReActivateLifelineSubsidy = "UserCanDeactivateOrReactivateLifelineSubsidy";

        #endregion

        #region Region

        public const string UserCanViewRegion = "UserCanViewRegionData";
        public const string UserCanExportRegions = "UserCanExportRegionData";
        public const string UserCanAddRegion = "UserCanAddRegionData";
        public const string UserCanEditRegion = "UserCanEditRegionData";
        public const string UserCanDeActivateReActivateRegion = "UserCanDeactivateOrReactivateRegionData";

        #endregion

        #region Province

        public const string UserCanViewProvince = "UserCanViewProvinceData";
        public const string UserCanExportProvinces = "UserCanExportProvinceData";
        public const string UserCanAddProvince = "UserCanAddProvinceData";
        public const string UserCanEditProvince = "UserCanEditProvinceData";
        public const string UserCanDeleteProvince = "UserCanDeleteProvinceData";
        public const string UserCanDeActivateReActivateProvince = "UserCanDeactivateOrReactivateProvinceData";

        #endregion

        #region CityTown

        public const string UserCanViewCityTownData = "UserCanViewCityTownData";
        public const string UserCanExportCityTownViewList = "UserCanExportCityTownData";
        public const string UserCanAddCityTownData = "UserCanAddCityTownData";
        public const string UserCanEditCityTownData = "UserCanEditCityTownData";
        public const string UserCanDeleteCityTownData = "UserCanDeleteCityTownData";
        public const string UserCanReactivateDeactivateCityTownData = "UserCanDeactivateOrReactivateCityTown";

        #endregion

        #region Barangay

        public const string UserCanViewBarangayData = "UserCanViewBarangayData";
        public const string UserCanExportBarangayViewList = "UserCanExportBarangayData";
        public const string UserCanAddBarangayData = "UserCanAddBarangayData";
        public const string UserCanEditBarangayData = "UserCanEditBarangayData";
        public const string UserCanDeleteBarangayData = "UserCanDeleteBarangayData";
        public const string UserCanReactivateDeactivateBarangayData = "UserCanDeactivateOrReactivateBarangayData";
        #endregion

        #region Purok

        public const string UserCanViewPurok = "UserCanViewPurokData";
        public const string UserCanExportPuroks = "UserCanExportPurokData";
        public const string UserCanAddPurok = "UserCanAddPurokData";
        public const string UserCanEditPurok = "UserCanEditPurokData";
        public const string UserCanDeletePurok = "UserCanDeletePurokData";
        public const string UserCanDeActivateReActivatePurok = "UserCanDeactivateOrReactivatePurokData";

        #endregion

        #region Route

        public const string UserCanViewRoute = "UserCanViewRouteData";
        public const string UserCanExportRoute = "UserCanExportRouteData";
        public const string UserCanAddRoute = "UserCanAddRouteData";
        public const string UserCanEditRoute = "UserCanEditRouteData";
        public const string UserCanDeleteRoute = "UserCanDeleteRouteData";
        public const string UserCanDeactivateReactivateRoute = "UserCanDeactivateOrReactivateRouteData";

        #endregion

        #region Sitio

        public const string UserCanViewSitio = "UserCanViewSitioData";
        public const string UserCanExportSitios = "UserCanExportSitioData";
        public const string UserCanAddSitio = "UserCanAddSitioData";
        public const string UserCanEditSitio = "UserCanEditSitioData";
        public const string UserCanDeleteSitio = "UserCanDeleteSitioData";
        public const string UserCanDeActivateReActivateSitio = "UserCanDeactivateOrReactivateSitioData";

        #endregion

        #region Approval Stage

        public const string UserCanViewApprovalStageData = "UserCanViewApprovalStageData";
        public const string UserCanExportApprovalStageViewList = "UserCanExportApprovalStageData";
        public const string UserCanAddApprovalStageData = "UserCanAddApprovalStageData";
        public const string UserCanEditApprovalStageData = "UserCanEditApprovalStageData";
        public const string UserCanViewApprovalStageDetails = "UserCanViewApprovalStageDetails";
        public const string UserCanDeleteApprovalStageData = "UserCanDeleteApprovalStageData";
        public const string UserCanDeactivateReactivateApprovalStageData = "UserCanDeactivateOrReactivateApprovalStageData";

        #endregion

        #region Approval template

        public const string UserCanViewApprovalTemplateData = "UserCanViewApprovalTemplateData";
        public const string UserCanExportApprovalTemplateViewList = "UserCanExportApprovalTemplateData";
        public const string UserCanAddApprovalTemplateData = "UserCanAddApprovalTemplateData";
        public const string UserCanEditApprovalTemplateData = "UserCanEditApprovalTemplateData";
        public const string UserCanDeleteApprovalTemplateData = "UserCanDeleteApprovalTemplateData";
        public const string UserCanReactivateDeactivateApprovalTemplateData = "UserCanDeactivateOrReactivateApprovalTemplateData";
        public const string UserCanViewApprovalTemplateDetails = "UserCanViewApprovalTemplateDetails";

        #endregion

        #region Approval template

        public const string UserCanViewApproverLabelData = "UserCanViewApproverLabelData";
        public const string UserCanExportApproverLabelViewList = "UserCanExportApproverLabelData";
        public const string UserCanAddApproverLabelData = "UserCanAddApproverLabelData";
        public const string UserCanEditApproverLabelData = "UserCanEditApproverLabelData";
        public const string UserCanDeleteApproverLabelData = "UserCanDeleteApproverLabelData";
        public const string UserCanReactivateDeactivateApproverLabelData = "UserCanDeactivateOrReactivateApproverLabelData";

        #endregion

        #region Banking

        #region Credit Card

        public const string UserCanViewCreditCard = "UserCanViewCreditCard";
        public const string UserCanAddCreditCard = "UserCanAddCreditCard";
        public const string UserCanUpdateCreditCard = "UserCanUpdateCreditCard";
        public const string UserCanDeleteCreditCard = "UserCanDeleteCreditCard";
        public const string UserCanActivateDeactivateCreditCard = "UserCanActivateDeactivateCreditCard";
        public const string UserCanExportCreditCard = "UserCanExportCreditCard";

        #endregion

        #region Bank

        public const string UserCanViewBank = "UserCanViewBank";
        public const string UsercanEditBank = "UsercanEditBank";
        public const string UserCanDeleteBank = "UserCanDeleteBank";
        public const string UserCanAddBank = "UserCanAddBank";
        public const string UserCanExportBank = "UserCanExportBank";

        #endregion

        #region House Bank Account

        public const string UserCanViewHouseBankAccount = "UserCanViewHouseBankAccount";
        public const string UsercanEditHouseBankAccount = "UsercanEditHouseBankAccount";
        public const string UserCanDeleteHouseBankAccount = "UserCanDeleteHouseBankAccount";
        public const string UserCanAddHouseBankAccount = "UserCanAddHouseBankAccount";
        public const string UserCanExportHouseBankAccount = "UserCanExportHouseBankAccount";

        #endregion

        #endregion

        #region Purpose

        public const string UserCanViewPurpose = "UserCanViewPurpose";
        public const string UserCanAddPurpose = "UserCanAddPurpose";
        public const string UserCanEditPurpose = "UserCanEditPurpose";
        public const string UserCanExportPurpose = "UserCanExportPurpose";

        #endregion

        #region Report Signatory

        public const string UserCanViewReportSignatoryData = "UserCanViewReportSignatoryData";
        public const string UserCanExportReportSignatoryViewList = "UserCanExportReportSignatoryViewList";
        public const string UserCanAddReportSignatoryData = "UserCanAddReportSignatoryData";
        public const string UserCanEditReportSignatoryData = "UserCanEditReportSignatoryData";
        public const string UserCanDeleteReportSignatoryData = "UserCanDeleteReportSignatoryData";
        public const string UserCanDeActivateReactivateReportSignatory = "UserCanDeActivateReactivateReportSignatory";

        #endregion

        #endregion

        #region CSA - Customer Service

        #region Applicant

        public const string UserCanViewApplicantsData = "UserCanViewApplicantsData";
        public const string UserCanExportApplicantViewList = "UserCanExportApplicantData";
        public const string UserCanAddApplicantData = "UserCanAddApplicantData";
        public const string UserCanEditApplicantData = "UserCanEditApplicantData";
        public const string UserCanViewApplicantConsumerDetails = "UserCanViewApplicantConsumerDetails";
        public const string UserCanAddApplicantSpecialLighting = "UserCanAddApplicantSpecialLighting";
        public const string UserCanEditApplicantSpecialLighting = "UserCanEditApplicantSpecialLighting";
        public const string UserCanConductFeasibleConnectionSpecialLightingAccountData = "UserCanConductFeasibleConnectionSpecialLightingAccountData";
        
        #endregion

        #region Member

        public const string UserCanViewMemberData = "UserCanViewMemberData";
        public const string UserCanExportMemberViewList = "UserCanExportMemberData";
        public const string UserCanPrintMembershipId = "UserCanPrintMembershipId";

        #endregion

        #region Member Accounts

        public const string UserCanViewConsumerAccountsData = "UserCanViewConsumerAccountsData";
        public const string UserCanAddConsumerAccountData = "UserCanAddConsumerAccountData";
        public const string UserCanEditConsumerAccountData = "UserCanEditConsumerAccountData";
        public const string UserCanEndorseReassignToForHWInspection = "UserCanEndorse/reassignToHouseWiringInspection";
        public const string UserCanRecommendAccountForApproval = "UserCanRecommendAccountForApproval";
        public const string UserCanAddSubstationAndFeederToAccountData = "UserCanAddSubstationAndFeederToAccountData";
        public const string UserCanExportAccountsViewList = "UserCanExportAccountsData";
        public const string UserCanRecommendForPaymentMemberAccountApplication = "UserCanRecommendForPaymentMemberAccountApplication";
        public const string UserCanViewConsumerAccountDetails = "UserCanViewConsumerAccountDetails";
        public const string UserCanEndorseAccountToIssueForConnection = "UserCanEndorseAccountToIssueForConnection";
        public const string UserCanUpdateAccountMasterFile = "UserCanUpdateAccountMasterFile";
        public const string UserCanAddConsumerSpecialLightingAccountData = "UserCanAddConsumerSpecialLightingAccountData";
        public const string UserCanSetWitnessForSpecialLightingAccountData = "UserCanSetWitnessForSpecialLightingAccountData";
        public const string UserCanProcessSpecialLightingAccountData = "UserCanProcessSpecialLightingAccountData";
        public const string UserCanSetRenewSpecialLightingAccountData = "UserCanSetRenewSpecialLightingAccountData";
        public const string UserCanSetTerminateSpecialLightingAccountData = "UserCanSetTerminateSpecialLightingAccountData";
        #endregion

        #region Pre-Membership Orientation Seminar

        public const string UserCanViewPreMembershipOrientationSeminar = "UserCanViewPreMembershipOrientationSeminarData";
        public const string UserCanExportPMOS = "UserCanExportPreMembershipOrientationSeminarData";
        public const string UserCanEditPreMembershipOrientationSeminar = "UserCanEditPreMembershipOrientationSeminarData";

        #endregion

        #region House Wiring Inspection

        public const string UserCanViewHouseWiringInspectionData = "UserCanViewHouseWiringInspectionData";
        public const string UserCanExportHouseWiringInspectionViewList = "UserCanExportForHouseWiringInspectionData";
        public const string UserCanDisapproveHouseWiringInspectionData = "UserCanDisapproveHouseWiringInspectionData";
        public const string UserCanApproveHouseWiringInspectionData = "UserCanApproveHouseWiringInspectionData";
        public const string UseCanOverrideHouseWiringInspectionData = "UseCanOverrideHouseWiringInspectionData";

        #endregion

        #region Connections

        public const string UserCanViewConnections = "UserCanViewConnections";

        #region Issue For Connection

        public const string UserCanViewIssueForConnectionData = "UserCanViewIssueForConnectionData";
        public const string UserCanIssueForConnection = "UserCanIssue/reissueForConnectionData";
        public const string UserCanExportIssueForConnectionViewList = "UserCanExportIssueForConnectionData";

        #endregion

        #region For Connection

        public const string UserCanViewForConnectionData = "UserCanViewForConnectionData";
        public const string UserCanExportConnectionViewList = "UserCanExportForConnectionData";
        public const string UserCanAddActionTakenForConnection = "UserCanAddActionTakenForConnection";
        public const string UserCanPrintConnectionOrder = "UserCanPrintConnectionOrder";
        public const string UserCanOverrideAccountForConnection = "UserCanOverrideAccountForConnection";

        #endregion

        #region For Disconnection 

        public const string UserCanViewForDisconnectionList = "UserCanViewForDisconnectionList";
        public const string UserCanExportForDisconnectionList = "UserCanExportForDisconnectionList";
        public const string UserCanOverrideAccountForDisconnection = "UserCanOverrideAccountForDisconnection";
        public const string UserCanAddActionTakenForDisconnection = "UserCanAddActionTakenForDisconnection";

        #endregion

        #endregion

        #region Request/Applications

        #region Discount Application

        public const string UserCanViewDiscountApplicationData = "UserCanViewDiscountApplication";
        public const string UserCanExportDiscountApplicationViewList = "UserCanExportDiscountApplicationData";
        public const string UserCanAddDiscountApplicationData = "UserCanAddDiscountApplicationData";
        public const string UserCanEditDiscountApplicationData = "UserCanEditDiscountApplicationData";
        public const string UserCanDeleteDiscountApplicationData = "UserCanDeleteDiscountApplicationData";
        public const string UserCanViewDiscountApplicationDetails = "UserCanViewDiscountApplicationDetails";
        public const string UserCanRenewDiscountApplicationData = "UserCanRenewDiscountApplicationData";
        public const string UserCanRecommendDiscountApplicationForApproval = "UserCanRecommendDiscountApplicationForApproval";

        #endregion

        #region Net Metering

        public const string UserCanViewNetMeteringData = "UserCanViewNetMeteringData";
        public const string UserCanExportNetMeteringViewList = "UserCanExportNetMeteringData";
        public const string UserCanDownloadNetMeteringForm = "UserCanDownloadNetMeteringForm";
        public const string UserCanAddNetMeteringData = "UserCanAddNetMeteringData";
        public const string UserCanEditNetMeteringData = "UserCanEditNetMeteringData";
        public const string UserCanDeleteNetMeteringData = "UserCanDeleteNetMeteringData";
        public const string UserCanViewNetMeteringDetails = "UserCanViewNetMeteringDetails";
        public const string UserCanEndorseNetMeteringForPayment = "UserCanEndorseNetMeteringForPayment";
        public const string UserCanProcessNetMetering = "UserCanProcessNetMetering";
        public const string UserCanRecommendNetMeteringForApproval = "UserCanRecommendNetMeteringForApproval";


        #endregion

        #region Burial lAssistance

        public const string UserCanViewBurialAssistanceData = "UserCanViewBurialAssistanceData";
        public const string UserCanExportBurialAssistanceViewList = "UserCanExportBurialAssistanceData";
        public const string UserCanAddBurialAssistanceData = "UserCanAddBurialAssistanceData";
        public const string UserCanEditBurialAssistanceData = "UserCanEditBurialAssistanceData";
        public const string UserCanDeleteBurialAssistanceData = "UserCanDeleteBurialAssistanceData";
        public const string UserCanCoopEvaluationBurialAssistanceData = "UserCanCoopEvaluationBurialAssistanceData";
        public const string UserCanViewBurialAssistanceDetails = "UserCanViewBurialAssistanceDetails";
        public const string UserCanRecommendBurialAssistanceForApproval = "UserCanRecommendBurialAssistanceForApproval";

        #endregion

        #region Change Of Name

        public const string UserCanViewChangeOfNameData = "UserCanViewChangeOfNameData";
        public const string UserCanExportChangeOfNameViewList = "UserCanExportChangeOfNameData";
        public const string UserCanAddChangeOfNameData = "UserCanAddChangeOfNameData";
        public const string UserCanEditChangeOfNameData = "UserCanEditChangeOfNameData";
        public const string UserCanDeleteChangeOfNameData = "UserCanDeleteChangeOfNameData";
        public const string UserCanViewChangeOfNameDetails = "UserCanViewChangeOfNameDetails";
        public const string UserCanEndorseChangeOfNameData = "UserCanEndorseChangeOfNameData";
        public const string UserCanRecommendForPaymentChangeOfName = "UserCanRecommendForPaymentChangeOfName";
        public const string UserCanRecommendForApprovalChangeOfName = "UserCanRecommendForApprovalChangeOfName";
        public const string UserCanProcessChangeOfName = "UserCanProcessChangeOfName";

        #endregion

        #region Other Request

        public const string UserCanViewOtherRequestData = "UserCanViewOtherRequestData";
        public const string UserCanAddOtherRequestData = "UserCanAddOtherRequestData";
        public const string UserCanEditOtherRequestData = "UserCanEditOtherRequestData";
        public const string UserCanRecommendOtherRequestForApproval = "UserCanRecommendOtherRequestForApproval";
        public const string UserCanExportOtherRequestViewList = "UserCanExportOtherRequestData";
        public const string UserCanViewOtherRequestDetails = "UserCanViewOtherRequestDetails";
        public const string UserCanDeleteOtherRequestData = "UserCanDeleteOtherRequestData";
        public const string UserCanProcessOtherRequest = "UserCanProcessOtherRequest";

        #endregion

        #region Contestable

        public const string UserCanViewContestableApplication = "UserCanViewContestableApplication";
        public const string UserCanAddContestableApplication = "UserCanAddContestableApplication";
        public const string UserCanUpdateContestableApplication = "UserCanUpdateContestableApplication";
        public const string UserCanDeleteContestableApplication = "UserCanDeleteContestableApplication";
        public const string UserCanExportContestableApplication = "UserCanExportContestableApplication";
        public const string UserCanRecommendForApprovalContestableApplication = "UserCanRecommendForApprovalContestableApplication";

        #endregion

        #region Transformer Rental

        public const string UserCanViewTransformerRental = "UserCanViewTransformerRental";
        public const string UserCanSetTransformerRentalWitness = "UserCanSetTransformerRentalWitness";
        public const string UserCanExportTransformerRental = "UserCanExportTransformerRental";
        public const string UserCanAddTransformerRental = "UserCanAddTransformerRental";
        public const string UserCanEditTransformerRental = "UserCanEditTransformerRental";
        public const string UserCanDeleteTransformerRental = "UserCanDeleteTransformerRental";
        public const string UserCanRecommendForApprovalTransformerRental = "UserCanRecommendForApprovalTransformerRental";
        public const string UserCanViewContractTransformerRental = "UserCanViewContractTransformerRental";
        public const string UserCanTerminateTransformerRental = "UserCanTerminateTransformerRental";
        public const string UserCanRenewTransformerRental = "UserCanRenewTransformerRental";

        #endregion

        #region Change Of Meter

        public const string UserCanViewChangeOfMeter = "UserCanViewChangeOfMeter";
        public const string UserCanAddChangeOfMeter = "UserCanAddChangeOfMeter";
        public const string UserCanEditChangeOfMeter = "UserCanEditChangeOfMeter";
        public const string UserCanExportChangeOfMeter = "UserCanExportChangeOfMeter";
        public const string UserCanDeleteChangeOfMeter = "UserCanDeleteChangeOfMeter";
        public const string UserCanRecommendForApprovalChangeOfMeter = "UserCanRecommendChangeOfMeter";

        #endregion

        #region Billing Adjustment
        public const string UserCanViewBillingAdjustment = "UserCanViewBillingAdjustment";
        public const string UserCanAddBillingAdjustment = "UserCanAddBillingAdjustment";
        public const string UserCanEditBillingAdjustment = "UserCanEditBillingAdjustment";
        public const string UserCanDeleteBillingAdjustment = "UserCanDeleteBillingAdjustment";
        public const string UserCanRecommendForApprovalBillingAdjustment = "UserCanRecommendForApprovalBillingAdjustment";
        public const string UserCanExportBillingAdjustment = "UserCanExportBillingAdjustment";
        #endregion

        #endregion

        #endregion

        #region Job Order

        #region Task to be Perform / Nature Types

        public const string UserCanViewJOTaskToBePerform = "UserCanViewJobOrderTaskToBePerform";
        public const string UserCanAddJOTaskToBePerform = "UserCanAddJobOrderTaskToBePerform";
        public const string UserCanEditJOTaskToBePerform = "UserCanEditJobOrderTaskToBePerform";
        public const string UserCanDeleteJOTaskToBePerform = "UserCanDeleteJobOrderTaskToBePerform";
        public const string UserCanReactivateOrDeactivateJOTaskToBePerform = "UserCanReactivate/DeactivateJobOrderTaskToBePerform";
        public const string UserCanExportJOTaskToBePerform = "UserCanExportJOTaskToBePerform";

        public const string UserCanViewJONatureType = "UserCanViewJobOrderNatureType";
        public const string UserCanAddJONatureType = "UserCanAddJobOrderNatureType";
        public const string UserCanEditJONatureType = "UserCanEditJobOrderNatureType";
        public const string UserCanDeleteJONatureType = "UserCanDeleteJobOrderNatureType";
        public const string UserCanReactivateOrDeactivateJONatureType = "UserCanReactivate/DeactivateJobOrderNatureType";
        public const string UserCanExportJONatureType = "UserCanExportJobOrderNatureType";

        #endregion

        #region Complaint Type

        public const string UserCanViewComplaintTypeData = "UserCanViewComplaintTypeData";
        public const string UserCanExportComplaintTypeViewList = "UserCanExportComplaintTypeData";
        public const string UserCanAddComplaintTypeData = "UserCanAddComplaintTypeData";
        public const string UserCanEditComplaintTypeData = "UserCanEditComplaintTypeData";
        public const string UserCanDeleteComplaintTypeData = "UserCanDeleteComplaintTypeData";
        public const string UserCanDeActivateReActivateComplaintTypeData = "UserCanDeactivateOrReactivateComplaintTypeData";

        #endregion

        #region  Job Order Complaint

        public const string UserCanViewJobOrderComplaintData = "UserCanViewJobOrderComplaintData";
        public const string UserCanExportJobOrderComplaintData = "UserCanExportJobOrderComplaintData";
        public const string UserCanAddJobOrderComplaintData = "UserCanAddJobOrderComplaintData";
        public const string UserCanEditJobOrderComplaintData = "UserCanEditJobOrderComplaintData";
        public const string UserCanDeleteJobOrderComplaintData = "UserCanDeleteJobOrderComplaintData";
        public const string UserCanAssignJobOrderComplaintDataToDivision = "UserCanEndorseJobOrderComplaintData";
        public const string UserCanForwardJobOrderComplaint = "UserCanForwardJobOrderComplaintData";
        public const string UserCanReceiveJobOrderComplaint = "UserCanReceiveJobOrderComplaint";
        public const string UserCanEditJobOrderComplaintActedUponField = "UserCanEditJobOrderComplaintActedUponField";
        public const string UserCanCreateJobOrderComplaintDirect = "UserCanCreateJobOrderComplaintDirect";
        public const string UserCanCreateJobOrderComplaintIndirect = "UserCanCreateJobOrderComplaintIndirect";
        public const string UserCanViewJobOrderComplaintDetails = "UserCanViewJobOrderComplaintDetails";
        public const string UserCanRecommendJobOrderComplaintForApproval = "UserCanRecommendJobOrderComplaintForApproval";
        public const string UserCanReturnJobOrderComplaint = "UserCanReturnJobOrderComplaint";

        #endregion

        #region  Job Order Request/Application

        public const string UserCanViewJobOrderRequest = "UserCanViewJobOrderRequestOrApplicationData";
        public const string UserCanExportJobOrderRequest = "UserCanExportJobOrderRequestOrApplicationData";
        public const string UserCanAddJobOrderRequest = "UserCanAddJobOrderRequestOrApplicationData";
        public const string UserCanEditJobOrderRequest = "UserCanEditJobOrderRequestOrApplicationData";
        public const string UserCanDeleteJobOrderRequest = "UserCanDeleteJobOrderRequestOrApplicationData";
        public const string UserCanAssignJobOrderRequestToDivision = "UserCanAssignJobOrderRequestToDivision";
        public const string UserCanForwardJobOrderRequest = "UserCanForwardJobOrderRequestOrApplicationData";
        public const string UserCanReceiveJobOrderRequest = "UserCanReceiveJobOrderRequest";
        public const string UserCanEditJobOrderRequestApplicationActedUponField = "UserCanEditJobOrderRequestOrApplicationActedUponField";
        public const string UserCanCreateJobOrderRequestDirect = "UserCanCreateJobOrderRequestDirect";
        public const string UserCanCreateJobOrderRequestIndirect = "UserCanCreateJobOrderRequestIndirect";
        public const string UserCanViewJobOrderRequestDetails = "UserCanViewJobOrderRequestDetails";
        public const string UserCanRecommendJobOrderRequestForApproval = "UserCanRecommendJobOrderRequestForApproval";
        public const string UserCanReturnJobOrderRequest = "UserCanReturnJobOrderRequest";

        #endregion

        #region Assign Job Order

        public const string UserCanViewAssignedJobOrderToEmployee = "UserCanViewAssignedJobOrderToEmployee(s)";
        public const string UserCanExportAssignedJobOrderToEmployee = "UserCanExportAssignedJobOrderToEmployee(s)";
        public const string UserCanAssignJobOrderDataToEmployee = "UserCanAssignJobOrderDataToEmployee(s)";
        public const string UserCanReceiveForAssignedJobOrderToEmployee = "UserCanReceiveForAssignedJobOrderToEmployee(s)";
        public const string UserCanViewForAssignedJobOrderProcessHistory = "UserCanViewForAssignedJobOrderProcessHistory";

        #endregion

        #region Process Job Order

        public const string UserCanViewProcessJobOrder = "UserCanViewProcessJobOrderList";
        public const string UserCanExportProcessJobOrder = "UserCanExportProcessJobOrderList";
        public const string UserCanProcessJobOrder = "UserCanProcessJobOrder";
        public const string UserCanViewJobOrderProcessHistory = "UserCanViewJobOrderProcessHistory";

        #endregion

        #endregion

        #region Collection

        #region Payment

        public const string UserCanViewPaymentData = "UserCanViewPaymentData";
        public const string UserCanExportPaymentViewList = "UserCanExportPaymentData";
        public const string UserCanEndorseToForPayment = "UserCanEndorseToForPayment";

        public const string UserCanAddPaymentEntry = "UserCanAddPaymentEntryData";

        #endregion

        #region Surcharge

        public const string UserCanViewSurcharge = "UserCanViewSurchargeData";
        public const string UserCanExportSurcharge = "UserCanExportSurchargeData";
        public const string UserCanAddSurcharge = "UserCanAddSurchargeData";
        public const string UserCanEditSurcharge = "UserCanEditSurchargeData";
        public const string UserCanDeleteSurcharge = "UserCanDeleteSurchargeData";
        public const string UserCanDeActivateReActivateSurcharge = "UserCanDeactivateOrReactivateSurchargeData";

        #endregion

        #region Counter setup

        public const string UserCanViewCounterSetup = "UserCanViewCounterSetup";
        public const string UserCanAddCounterSetup = "UserCanAddCounterSetup";
        public const string UserCanEditCounterSetup = "UserCanEditCounterSetup";
        public const string UserCanExportCounterSetup = "UserCanExportCounterSetup";

        #endregion

        #endregion

        #region Billing

        #region Billing Unit

        public const string UserCanViewBillingUnit = "UserCanViewBillingUnit";
        public const string UserCanAddBillingUnit = "UserCanAddBillingUnit";
        public const string UserCanEditBillingUnit = "UserCanEditBillingUnit";
        public const string UserCanDeleteBillingUnit = "UserCanDeleteBillingUnit";
        public const string UserCanDeActivateReActivateBillingUnit = "UserCanDeActivateReActivateBillingUnit";
        public const string UserCanExportBillingUnit = "UserCanExportBillingUnit";

        #endregion

        #region Unbundled Transaction

        public const string UserCanViewBillingUnbundledTransactionsData = "UserCanViewBillingUnbundledTransactionsData";
        public const string UserCanExportBillingUnbundledTransactionsViewList = "UserCanExportBillingUnbundledTransactionsData";
        public const string UserCanAddBillingUnbundledTransactionsData = "UserCanAddBillingUnbundledTransactionsData";
        public const string UserCanEditBillingUnbundledTransactionsData = "UserCanEditBillingUnbundledTransactionsData";
        public const string UserCanDeleteBillingUnbundledTransactionsData = "UserCanDeleteBillingUnbundledTransactionsData";
        public const string UserCanDeActivateReActivateBillingUnbundledTransactionData = "UserCanDeactivateOrReactivateBillingUnbundledTransactionsData";

        #endregion

        #region Billing Category

        public const string UserCanViewBillingCategoriesData = "UserCanViewBillingCategoriesData";
        public const string UserCanExportBillingCategoriesViewList = "UserCanExportBillingCategoriesData";
        public const string UserCanAddUpdateBillingCategoriesData = "UserCanAdd/updateBillingCategoriesData";
        public const string UserCanDeleteBillingCategoriesData = "UserCanDeleteBillingCategoriesData";
        public const string UserCanDeActivateReActivateBillingCategoriesData = "UserCanDeactivateOrReactivateBillingCategoriesData";

        #endregion

        #region Billing Monthly Rates

        public const string UserCanViewBillingMonthlyRatesData = "UserCanViewBillingMonthlyRatesData";
        public const string UserCanExportBillingMonthlyRatesViewList = "UserCanExportBillingMonthlyRatesData";
        public const string UserCanAddBillingMonthlyRatesData = "UserCanAddBillingMonthlyRatesData";
        public const string UserCanEditBillingMonthlyRatesData = "UserCanEditBillingMonthlyRatesData";
        public const string UserCanViewDetailsBillingMonthlyRates = "UserCanViewDetailsBillingMonthlyRates";
        public const string UserCanExportBillingMonthlyRates = "UserCanExportBillingMonthlyRates";

        #endregion

        #region  Meter Reading Remarks

        public const string UserCanViewMeterReadingRemarksData = "UserCanViewMeterReadingRemarksData";
        public const string UserCanExportMeterReadingRemarksViewList = "UserCanExportMeterReadingRemarksData";
        public const string UserCanAddMeterReadingRemarksData = "UserCanAddMeterReadingRemarksData";
        public const string UserCanEditMeterReadingRemarksData = "UserCanEditMeterReadingRemarksData";
        public const string UserCanDeleteMeterReadingRemarksData = "UserCanDeleteMeterReadingRemarksData";
        public const string UserCanDeActivateReActivateMeterReadingRemarks = "UserCanDeactivateOrReactivateMeterReadingRemarksData";

        #endregion

        #region Meter Reading

        public const string UserCanViewMeterReadingData = "UserCanViewMeterReadingData";
        public const string UserCanAddMeterReadingUnitData = "UserCanAddMeterReadingUnitData";
        public const string UserCanAddMeterReadingRouteData = "UserCanAddMeterReadingRouteData";
        public const string UserCanAddMeterReadingDayData = "UserCanAddMeterReadingDayData";
        public const string UserCanEditMeterReadingOfficeField = "UserCanEditMeterReadingOfficeField";

        #endregion

        #region Billing Transaction

        public const string UserCanViewBillingTransactionData = "UserCanViewBillingTransactionData";
        public const string UserCanExportBillingTransactionViewList = "UserCanExportBillingTransactionData";
        public const string UserCanModifyBillingTransactionData = "UserCanModifyBillingTransactionData";
        public const string UserCanExportBillingTransactionModifyViewList = "UserCanExportBillingTransactionModifyData";
        public const string UserCanViewBillingTransactionDetails = "UserCanViewBillingTransactionDetails";
        public const string UserCanValidateBillingTransactionData = "UserCanValidateBillingTransactionData";
        public const string UserCanPostBillingTransactionData = "UserCanPostBillingTransactionData";
        public const string UserCanExportBillingTransactionDetails = "UserCanExportBillingTransactionDetails";

        #endregion

        #region SOA

        public const string UserCanGenerateSOA = "UserCanGenerateSOA";

        #endregion

        #region Billing Announcement for SOA
        public const string UserCanViewBillingAnnouncementForSOA = "UserCanViewBillingAnnouncementForSOA";
        public const string UserCanAddBillingAnnouncementForSOA = "UserCanAddBillingAnnouncementForSOA";
        public const string UserCanEditBillingAnnouncementForSOA = "UserCanEditBillingAnnouncementForSOA";
        public const string UserCanDeleteBillingAnnouncementForSOA = "UserCanDeleteBillingAnnouncementForSOA";
        public const string UserCanExportBillingAnnouncementForSOA = "UserCanExportBillingAnnouncementForSOA";
        #endregion

        #region Billing Period
        public const string UserCanViewBillingPeriod = "UserCanViewBillingPeriod";
        public const string UserCanExportBillingPeriod = "UserCanExportBillingPeriod";
        public const string UserCanAddBillingPeriod = "UserCanAddBillingPeriod";
        public const string UserCanUpdateBillingPeriod = "UserCanUpdateBillingPeriod";
        public const string UserCanDeleteBillingPeriod = "UserCanDeleteBillingPeriod";
        public const string UserCanReactivateDeactivateBillingPeriod = "UserCanReactivateDeactivateBillingPeriod";

        #endregion

        #endregion

        #region Inventory

        #region Package Type

        public const string UserCanViewPackageTypeList = "UserCanViewPackageTypeList";
        public const string UserCanExportPackageTypeList = "UserCanExportPackageTypeList";
        public const string UserCanAddPackageType = "UserCanAddPackageType";
        public const string UserCanEditPackageType = "UserCanEditPackageType";
        public const string UserCanDeletePackageType = "UserCanDeletePackageType";
        public const string UserCanDeActivateReActivatePackageType = "UserCanDeactivateOrReactivatePackageType";

        #endregion

        #region Manufacturer

        public const string UserCanViewManufacturer = "UserCanViewManufacturerData";
        public const string UserCanExportManufacturerViewList = "UserCanExportManufacturerData";
        public const string UserCanAddManufacturerData = "UserCanAddManufacturerData";
        public const string UserCanEditManufacturerData = "UserCanEditManufacturerData";
        public const string UserCanDeleteManufacturerData = "UserCanDeleteManufacturerData";
        public const string UserCanDeActivateReActivateManufacturer = "UserCanDeactivateOrReactivateManufacturerData";

        #endregion

        #region Shipping Types

        public const string UserCanViewShippingTypes = "UserCanViewShippingTypes";
        public const string UserCanExportShippingTypes = "UserCanExportShippingTypes";
        public const string UserCanAddShippingType = "UserCanAddShippingType";
        public const string UserCanEditShippingType = "UserCanEditShippingType";
        public const string UserCanDeleteShippingType = "UserCanDeleteShippingType";
        public const string UserCanDeActivateReActivateShippingType = "UserCanDeactivateOrReactivateShippingType";

        #endregion

        #region Warehouse

        public const string UserCanViewWarehouse = "UserCanViewWarehouse";
        public const string UserCanAddWarehouse = "UserCanAddWarehouse";
        public const string UserCanEditWarehouse = "UserCanEditWarehouse";
        public const string UserCanDeleteWarehouse = "UserCanDeleteWarehouse";

        #endregion

        #region Cycle Groups

        public const string UserCanViewCycleGroupList = "UserCanViewCycleGroupList";
        public const string UserCanExportCycleGroupList = "UserCanExportCycleGroupList";
        public const string UserCanAddCycleGroup = "UserCanAddCycleGroup";
        public const string UserCanEditCycleGroup = "UserCanEditCycleGroup";
        public const string UserCanDeleteCycleGroup = "UserCanDeleteCycleGroup";
        public const string UserCanDeActivateReActivateCycleGroup = "UserCanDeactivateOrReactivateCycleGroup";

        #endregion

        #region Order Intervals

        public const string UserCanViewOrderIntervalList = "UserCanViewOrderIntervalList";
        public const string UserCanExportOrderIntervalList = "UserCanExportOrderIntervalList";
        public const string UserCanAddOrderInterval = "UserCanAddOrderInterval";
        public const string UserCanEditOrderInterval = "UserCanEditOrderInterval";
        public const string UserCanDeleteOrderInterval = "UserCanDeleteOrderInterval";
        public const string UserCanDeActivateReActivateOrderInterval = "UserCanDeactivateOrReactivateOrderInterval";

        #endregion

        #region Item Groups

        public const string UserCanViewItemGroup = "UserCanViewItemGroup";
        public const string UserCanAddItemGroup = "UserCanAddItemGroup";
        public const string UserCanEditItemGroup = "UserCanEditItemGroup";
        public const string UserCanDeleteItemGroup = "UserCanDeleteItemGroup";
        public const string UserCanDeactivateReactivateItemGroup = "UserCanDeactivateReactivateItemGroup";
        public const string UserCanExportItemGroup = "UserCanExportItemGroup ";

        #endregion

        #region Unit of Measure

        public const string UserCanViewUnitOfMeasureList = "UserCanViewUnitOfMeasureList";
        public const string UserCanExportUnitOfMeasureList = "UserCanExportUnitOfMeasureList";
        public const string UserCanAddUnitOfMeasure = "UserCanAddUnitOfMeasure";
        public const string UserCanEditUnitOfMeasure = "UserCanEditUnitOfMeasure";
        public const string UserCanDeleteUnitOfMeasure = "UserCanDeleteUnitOfMeasure";
        public const string UserCanDeActivateReActivateUnitOfMeasure = "UserCanDeactivateOrReactivateUnitOfMeasure";

        #endregion

        #region Length and Width UOM

        public const string UserCanViewLengthAndWidthUoMList = "UserCanViewLengthAndWidthUoMList";
        public const string UserCanExportLengthAndWidthUoMList = "UserCanExportLengthAndWidthUoMList";
        public const string UserCanAddLengthAndWidthUoM = "UserCanAddLengthAndWidthUoM";
        public const string UserCanEditLengthAndWidthUoM = "UserCanEditLengthAndWidthUoM";
        public const string UserCanDeleteLengthAndWidthUoM = "UserCanDeleteLengthAndWidthUoM";

        #endregion

        #region Weight UOM

        public const string UserCanViewWeightUoMList = "UserCanViewWeightUoMList";
        public const string UserCanExportWeightUoMList = "UserCanExportWeightUoMList";
        public const string UserCanAddWeightUoM = "UserCanAddWeightUoM";
        public const string UserCanEditWeightUoM = "UserCanEditWeightUoM";
        public const string UserCanDeleteWeightUoM = "UserCanDeleteWeightUoM";

        #endregion

        #region Price Lists

        public const string UserCanViewPriceList = "UserCanViewPriceList";
        public const string UserCanAddPriceList = "UserCanAddPriceList";
        public const string UserCanEditPriceList = "UserCanEditPriceList";
        public const string UserCanDeletePriceList = "UserCanDeletePriceList";
        public const string UserCanExportPriceList = "UserCanExportPriceList";
        public const string UserCanDeactivateReactivatePriceList = "UserCanDeactivateReactivatePriceList";

        #endregion

        #region Item Masters

        public const string UserCanViewItemMaster = "UserCanViewItemMaster ";
        public const string UserCanAddItemMaster = "UserCanAddItemMaster";
        public const string UserCanEditItemMaster = "UserCanEditItemMaster";
        public const string UserCanDeleteItemMaster = "UserCanDeleteItemMaster ";
        public const string UserCanExportItemMaster = "UserCanExportItemMaster";

        #endregion

        #region Inventory Transactions

        #region Inventory Transfers

        public const string UserCanViewInventoryTransfer = "UserCanViewInventoryTransfer";
        public const string UserCanAddInventoryTransfer = "UserCanAddInventoryTransfer";
        public const string UserCanDeleteInventoryTransfer = "UserCanDeleteInventoryTransfer";
        public const string UserCanEditInventoryTransfer = "UserCanEditInventoryTransfer";
        public const string UserCanTransferInventoryTransfer = "UserCanTransferInventoryTransfer";
        public const string UserCanRecommendToApprovalInventoryTransfer = "UserCanRecommendToApprovalInventoryTransfer";
        public const string UserCanExportInventoryTransfer = "UserCanExportInventoryTransfer";

        #endregion

        #region Inventory Receivings

        public const string UserCanViewInventoryReceiving = "UserCanViewInventoryReceiving";
        public const string UserCanAddInventoryReceiving = "UserCanAddInventoryReceiving";
        public const string UserCanDeleteInventoryReceiving = "UserCanDeleteInventoryReceiving";
        public const string UserCanEditInventoryReceiving = "UserCanEditInventoryReceiving";
        public const string UserCanReceiveInventoryReceiving = "UserCanReceiveInventoryReceiving";
        public const string UserCanExportInventoryReceiving = "UserCanExportInventoryReceiving";
        public const string UserCanRecommendToApprovalInventoryReceiving = "UserCanRecommendToApprovalInventoryReceiving";

        #endregion

        //Inventory Goods (E)xit
        public const string UserCanViewIGE = "UserCanViewInventoryGoodsExit";
        public const string UserCanAddIGE = "UserCanAddInventoryGoodsExit";
        public const string UserCanEditIGE = "UserCanEditInventoryGoodsExit";
        public const string UserCanDeleteIGE = "UserCanDeleteInventoryGoodsExit";
        public const string UserCanExportIGE = "UserCanExportInventoryGoodsExit";

        //Inventory Goods E(N)try
        public const string UserCanViewIGN = "UserCanViewInventoryGoodsEntry";
        public const string UserCanAddIGN = "UserCanAddInventoryGoodsEntry";
        public const string UserCanEditIGN = "UserCanEditInventoryGoodsEntry";
        public const string UserCanDeleteIGN = "UserCanDeleteInventoryGoodsEntry";
        public const string UserCanExportIGN = "UserCanExportInventoryGoodsEntry";

        public const string UserCanViewIQI = "UserCanViewInventoryQtyOB";
        public const string UserCanAddIQI = "UserCanAddInventoryQtyOB";
        public const string UserCanEditIQI = "UserCanEditInventoryQtyOB";
        public const string UserCanDeleteIQI = "UserCanDeleteInventoryQtyOB";
        public const string UserCanExportIQI = "UserCanExportInventoryQtyOB";

        public const string UserCanViewIQC = "UserCanViewInventoryQtyTracking";
        public const string UserCanAddIQC = "UserCanAddInventoryQtyTracking";
        public const string UserCanEditIQC = "UserCanEditInventoryQtyTracking";
        public const string UserCanDeleteIQC = "UserCanDeleteInventoryQtyTracking";
        public const string UserCanExportIQC = "UserCanExportInventoryQtyTracking";

        public const string UserCanViewIQP = "UserCanViewInventoryQtyPosting";
        public const string UserCanAddIQP = "UserCanAddInventoryQtyPosting";
        public const string UserCanEditIQP = "UserCanEditInventoryQtyPosting";
        public const string UserCanDeleteIQP = "UserCanDeleteInventoryQtyPosting";
        public const string UserCanExportIQP = "UserCanExportInventoryQtyPosting";

        public const string UserCanViewIQR = "UserCanViewInventoryRevaluation";
        public const string UserCanAddIQR = "UserCanAddInventoryRevaluation";
        public const string UserCanEditIQR = "UserCanEditInventoryRevaluation";
        public const string UserCanDeleteIQR = "UserCanDeleteInventoryRevaluation";
        public const string UserCanExportIQR = "UserCanExportInventoryRevaluation";

        #endregion

        #region Item Serial Management

        public const string UserCanViewItemSerialManagement = "UserCanViewItemSerialManagement";
        public const string UserCanExportItemSerialManagement = "UserCanExportItemSerialManagement";
        public const string UserCanCreateItemSerialManagement = "UserCanCreateItemSerialManagement";
        public const string UserCanUpdateItemSerialManagement = "UserCanUpdateItemSerialManagement";

        #endregion

        #endregion

        #region Business Partner

        #region Country

        public const string UserCanViewCountry = "UserCanViewCountry";
        public const string UserCanExportCountries = "UserCanExportCountries";
        public const string UserCanAddCountry = "UserCanAddCountry";
        public const string UserCanEditCountry = "UserCanEditCountry";
        public const string UserCanDeleteCountry = "UserCanDeleteCountry";
        public const string UserCanDeActivateReActivateCountry = "UserCanDeactivateOrReactivateCountry";

        #endregion

        #region Vendor Group

        public const string UserCanViewVendorGroup = "UserCanViewVendorGroup";
        public const string UserCanAddVendorGroup = "UserCanAddVendorGroup";
        public const string UserCanEditVendorGroup = "UserCanEditVendorGroup";
        public const string UserCanDeleteVendorGroup = "UserCanDeleteVendorGroup";
        public const string UserCanExportVendorGroup = "UserCanExportVendorGroup";
        public const string UserCanActivateDeactivateVendorGroup = "UserCanActivateDeactivateVendorGroup";

        #endregion

        #region Customer Group

        public const string UserCanViewCustomerGroups = "UserCanViewCustomerGroups";
        public const string UserCanExportCustomerGroups = "UserCanExportCustomerGroups";
        public const string UserCanAddCustomerGroup = "UserCanAddCustomerGroup";
        public const string UserCanEditCustomerGroup = "UserCanEditCustomerGroup";
        public const string UserCanDeleteCustomerGroup = "UserCanDeleteCustomerGroup";
        public const string UserCanDeActivateReActivateCustomerGroup = "UserCanDeactivateOrReactivateCustomerGroup";

        #endregion

        #region Payment Terms

        public const string UserCanViewPaymentGroup = "UserCanViewPaymentGroup";
        public const string UserCanAddPaymentGroup = "UserCanAddPaymentGroup";
        public const string UserCanEditPaymentGroup = "UserCanEditPaymentGroup";
        public const string UserCanDeletePaymentGroup = "UserCanDeletePaymentGroup";
        public const string UserCanDeActivateReactivatePaymentGroup = "UserCanDeactivateOrReactivatePaymentGroup";
        public const string UserCanExportPaymentGroup = "UserCanExportPaymentGroup";

        #endregion

        #region BP Customer

        public const string UserCanViewCustomerData = "UserCanViewCustomerData";
        public const string UserCanAddCustomerData = "UserCanAddCustomerData";
        public const string UserCanEditCustomerData = "UserCanEditCustomerData";
        public const string UserCanDeleteCustomerData = "UserCanDeleteCustomerData";
        public const string UserCanDeActivateReactivateBPCustomerData = "UserCanDeactivateOrReactivateCustomerData";
        public const string UserCanExportCustomerData = "UserCanExportCustomerData";

        #endregion

        #region BP Vendor

        public const string UserCanViewVendorData = "UserCanViewVendorData";
        public const string UserCanAddVendorData = "UserCanAddVendorData";
        public const string UserCanEditVendorData = "UserCanEditVendorData";
        public const string UserCanDeleteVendorData = "UserCanDeleteVendorData";
        public const string UserCanDeActivateReactivateBPVendorData = "UserCanDeactivateOrReactivateVendorData";
        public const string UserCanExportVendorData = "UserCanExportVendorData";

        #endregion

        #region Sales Employee

        public const string UserCanViewSalesEmployees = "UserCanViewSalesEmployees";
        public const string UserCanExportSalesEmployees = "UserCanExportSalesEmployees";
        public const string UserCanAddSalesEmployees = "UserCanAddSalesEmployees";
        public const string UserCanEditSalesEmployees = "UserCanEditSalesEmployees";
        public const string UserCanDeleteSalesEmployees = "UserCanDeleteSalesEmployees";

        #endregion

        #region Industry

        public const string UserCanViewIndustryList = "UserCanViewIndustryList";
        public const string UserCanAddIndustry = "UserCanAddIndustry";
        public const string UserCanExportIndustryList = "UserCanExportIndustryList";
        public const string UserCanDeleteIndustry = "UserCanDeleteIndustry";
        public const string UserCanDeActivateReActivateIndustry = "UserCanDeActivateReActivateIndustry";
        public const string UserCanEditIndustry = "UserCanEditIndustry";

        #endregion

        #region Cash Discount

        public const string UserCanViewCashDiscountList = "UserCanViewCashDiscountList";
        public const string UserCanExportCashDiscountList = "UserCanExportCashDiscountList";
        public const string UserCanDeleteCashDiscount = "UserCanDeleteCashDiscount";
        public const string UserCanDeActivateReActivateCashDiscount = "UserCanDeActivateReActivateCashDiscount";
        public const string UserCanEditCashDiscount = "UserCanEditCashDiscount";
        public const string UserCanAddCashDiscount = "UserCanAddCashDiscount";

        #endregion


        #region Reconcile BP
        public const string UserCanViewITRBP = "ITRBP1";
        public const string UserCanAddITRBP = "ITRBP2";
        public const string UserCanEditITRBP = "ITRBP3"; 
        #endregion

        #endregion

        #region Sales A/R

        // Sales Quotation
        public const string UserCanViewSQU = "UserCanViewSalesQuotation";
        public const string UserCanAddSQU = "UserCanAddSalesQuotation";
        public const string UserCanEditSQU = "UserCanEditSalesQuotation";
        public const string UserCanDeleteSQU = "UserCanDeleteSalesQuotation";
        public const string UserCanExportSQU = "UserCanExportSalesQuotation";

        // Sales Order
        public const string UserCanViewSOR = "UserCanViewSalesOrder";
        public const string UserCanAddSOR = "UserCanAddSalesOrder";
        public const string UserCanEditSOR = "UserCanEditSalesOrder";
        public const string UserCanDeleteSOR = "UserCanDeleteSalesOrder";
        public const string UserCanExportSOR = "UserCanExportSalesOrder";

        // Sales Delivery
        public const string UserCanViewSDN = "UserCanViewSalesDelivery";
        public const string UserCanAddSDN = "UserCanAddSalesDelivery";
        public const string UserCanEditSDN = "UserCanEditSalesDelivery";
        public const string UserCanDeleteSDN = "UserCanDeleteSalesDelivery";
        public const string UserCanExportSDN = "UserCanExportSalesDelivery";

        // Sales Return
        public const string UserCanViewSRD = "UserCanViewSalesReturn";
        public const string UserCanAddSRD = "UserCanAddSalesReturn";
        public const string UserCanEditSRD = "UserCanEditSalesReturn";
        public const string UserCanDeleteSRD = "UserCanDeleteSalesReturn";
        public const string UserCanExportSRD = "UserCanExportSalesReturn";

        // Sales Invoice
        public const string UserCanViewSIN = "UserCanViewSalesInvoice";
        public const string UserCanAddSIN = "UserCanAddSalesInvoice";
        public const string UserCanEditSIN = "UserCanEditSalesInvoice";
        public const string UserCanDeleteSIN = "UserCanDeleteSalesInvoice";
        public const string UserCanExportSIN = "UserCanExportSalesInvoice";

        //Sales Reserve Invoice
        public const string UserCanViewSRI = "UserCanViewSalesReserveInvoice";
        public const string UserCanAddSRI = "UserCanAddSalesReserveInvoice";
        public const string UserCanEditSRI = "UserCanEditSalesReserveInvoice";
        public const string UserCanDeleteSRI = "UserCanDeleteSalesReserveInvoice";
        public const string UserCanExportSRI = "UserCanExportSalesReserveInvoice";

        //Sales Down Payment
        public const string UserCanViewSDP = "UserCanViewSalesDownPayment";
        public const string UserCanAddSDP = "UserCanAddSalesDownPayment";
        public const string UserCanEditSDP = "UserCanEditSalesDownPayment";
        public const string UserCanDeleteSDP = "UserCanDeleteSalesDownPayment";
        public const string UserCanExportSDP = "UserCanExportSalesDownPayment";

        // Sales Credit Note
        public const string UserCanViewSCR = "UserCanViewSalesCreditNote";
        public const string UserCanAddSCR = "UserCanAddSalesCreditNote";
        public const string UserCanEditSCR = "UserCanEditSalesCreditNote";
        public const string UserCanDeleteSCR = "UserCanDeleteSalesCreditNote";
        public const string UserCanExportSCR = "UserCanExportSalesCreditNote";

        // Sales Invoice with Payment
        public const string UserCanViewSIP = "UserCanViewSalesInvoiceWithPayment";
        public const string UserCanAddSIP = "UserCanAddSalesInvoiceWithPayment";
        public const string UserCanEditSIP = "UserCanEditSalesInvoiceWithPayment";
        public const string UserCanDeleteSIP = "UserCanDeleteSalesInvoiceWithPayment ";
        public const string UserCanExportSIP = "UserCanExportSalesInvoiceWithPayment";

        #endregion

        #region Purchase A/P

        // Purchase Qoutation
        public const string UserCanViewPQU = "UserCanViewPurchaseQuotation";
        public const string UserCanAddPQU = "UserCanAddPurchaseQuotation";
        public const string UserCanEditPQU = "UserCanEditPurchaseQuotation";
        public const string UserCanDeletePQU = "UserCanDeletePurchaseQuotation";
        public const string UserCanExportPQU = "UserCanExportPurchaseQuotation";

        // Purchase Order
        public const string UserCanViewPOR = "UserCanViewPurchaseOrder";
        public const string UserCanAddPOR = "UserCanAddPurchaseOrder";
        public const string UserCanEditPOR = "UserCanEditPurchaseOrder";
        public const string UserCanDeletePOR = "UserCanDeletePurchaseOrder";
        public const string UserCanExportPOR = "UserCanExportPurchaseOrder";

        // Purchase Delivery
        public const string UserCanViewPDN = "UserCanViewPurchaseDelivery";
        public const string UserCanAddPDN = "UserCanAddPurchaseDelivery";
        public const string UserCanEditPDN = "UserCanEditPurchaseDelivery";
        public const string UserCanDeletePDN = "UserCanDeletePurchaseDelivery";
        public const string UserCanExportPDN = "UserCanExportPurchaseDelivery";

        // Purchase Return
        public const string UserCanViewPRD = "UserCanViewPurchaseReturn";
        public const string UserCanAddPRD = "UserCanAddPurchaseReturn";
        public const string UserCanEditPRD = "UserCanEditPurchaseReturn";
        public const string UserCanDeletePRD = "UserCanDeletePurchaseReturn";
        public const string UserCanExportPRD = "UserCanExportPurchaseReturn";

        // Purchase Invoice
        public const string UserCanViewPIN = "UserCanViewPurchaseInvoice";
        public const string UserCanAddPIN = "UserCanAddPurchaseInvoice";
        public const string UserCanEditPIN = "UserCanEditPurchaseInvoice";
        public const string UserCanDeletePIN = "UserCanDeletePurchaseInvoice";
        public const string UserCanExportPIN = "UserCanExportPurchaseInvoice";

        //Purchase Reserve Invoice
        public const string UserCanViewPRI = "UserCanViewPurchaseReserveInvoice";
        public const string UserCanAddPRI = "UserCanAddPurchaseReserveInvoice";
        public const string UserCanEditPRI = "UserCanEditPurchaseReserveInvoice";
        public const string UserCanDeletePRI = "UserCanDeletePurchaseReserveInvoicee";
        public const string UserCanExportPRI = "UserCanExportPurchaseReserveInvoice";

        // Purchase Down Payment
        public const string UserCanViewPDP = "UserCanViewPurchaseDownPayment";
        public const string UserCanAddPDP = "UserCanAddPurchaseDownPayment";
        public const string UserCanEditPDP = "UserCanEditPurchaseDownPayment";
        public const string UserCanDeletePDP = "UserCanDeletePurchaseDownPayment";
        public const string UserCanExportPDP = "UserCanExportPurchaseDownPayment";

        // Purchase Credit Note
        public const string UserCanViewPCR = "UserCanViewPurchaseCreditNote";
        public const string UserCanAddPCR = "UserCanAddPurchaseCreditNote";
        public const string UserCanEditPCR = "UserCanEditPurchaseCreditNote";
        public const string UserCanDeletePCR = "UserCanDeletePurchaseCreditNote";
        public const string UserCanExportPCR = "UserCanExportPurchaseCreditNote";

        // Purchase Invoice with Payment
        public const string UserCanViewPIP = "UserCanViewPurchaseInvoiceWithPayment";
        public const string UserCanAddPIP = "UserCanAddPurchaseInvoiceWithPayment";
        public const string UserCanEditPIP = "UserCanEditPurchaseInvoiceWithPayment";
        public const string UserCanDeletePIP = "UserCanDeletePurchaseInvoiceWithPayment ";
        public const string UserCanExportPIP = "UserCanExportPurchaseInvoiceWithPayment";

        #endregion

        #region Payment

        //payment/collection
        public const string UserCanViewPaymentAndCollection = "UserCanViewPaymentAndCollection";
        public const string UserCanAddPaymentAndCollection = "UserCanAddPaymentAndCollection";
        public const string UserCanEditPaymentAndCollection = "UserCanEditPaymentAndCollection";
        public const string UserCanDeletePaymentAndCollection = "UserCanDeletePaymentAndCollection ";
        public const string UserCanExportPaymentAndCollection = "UserCanExportPaymentAndCollection";

        //outgoing payments
        public const string UserCanViewOutgoingPayments = "UserCanViewOutgoingPayments";
        public const string UserCanAddOutgoingPayments = "UserCanAddOutgoingPayments";
        public const string UserCanEditOutgoingPayments = "UserCanEditOutgoingPayments";
        public const string UserCanDeleteOutgoingPayments = "UserCanDeleteOutgoingPayments";
        public const string UserCanExportOutgoingPayments = "UserCanExportOutgoingPayments";

        //outgoing paymentEntry
        public const string UserCanViewOutgoingPaymentEntry = "UserCanViewOutgoingPaymentEntry";
        public const string UserCanAddOutgoingPaymentEntry = "UserCanAddOutgoingPaymentEntry";
        public const string UserCanEditOutgoingPaymentEntry = "UserCanEditOutgoingPaymentEntry";
        public const string UserCanDeleteOutgoingPaymentEntry = "UserCanDeleteOutgoingPaymentEntry";
        public const string UserCanExportOutgoingPaymentEntry = "UserCanExportOutgoingPaymentEntry";

        //checks for payment
        public const string UserCanViewChecksForPayment = "UserCanViewChecksForPayment";
        public const string UserCanAddChecksForPayment = "UserCanAddChecksForPayment";
        public const string UserCanEditChecksForPayment = "UserCanEditChecksForPayment";
        public const string UserCanDeleteChecksForPayment = "UserCanDeleteChecksForPayment";
        public const string UserCanExportChecksForPayment = "UserCanExportChecksForPayment";

        //void checks for payment
        public const string UserCanViewVoidChecksForPayment = "UserCanViewVoidChecksForPayment";
        public const string UserCanAddVoidChecksForPayment = "UserCanAddVoidChecksForPayment";
        public const string UserCanEditVoidChecksForPayment = "UserCanEditVoidChecksForPayment";
        public const string UserCanDeleteVoidChecksForPayment = "UserCanDeleteVoidChecksForPayment";
        public const string UserCanExportVoidChecksForPayment = "UserCanExportVoidChecksForPayment";

        //incoming payment parent
        public const string UserCanViewIncomingPayments = "UserCanViewIncomingPayments";
        public const string UserCanAddIncomingPayments = "UserCanAddIncomingPayments";
        public const string UserCanEditIncomingPayments = "UserCanEditIncomingPayments";
        public const string UserCanDeleteIncomingPayments = "UserCanDeleteIncomingPayments";
        public const string UserCanExportIncomingPayments = "UserCanExportIncomingPayments";

        //incoming payment entry 
        public const string UserCanViewIncomingPaymentEntry = "UserCanViewIncomingPaymentEntry";
        public const string UserCanAddIncomingPaymentEntry = "UserCanAddIncomingPaymentEntry";
        public const string UserCanEditIncomingPaymentEntry = "UserCanEditIncomingPaymentEntry";
        public const string UserCanDeleteIncomingPaymentEntry = "UserCanDeleteIncomingPaymentEntry";
        public const string UserCanExportIncomingPaymentEntry = "UserCanExportIncomingPaymentEntry";

        //check register
        public const string UserCanViewCheckRegister = "UserCanViewCheckRegister";
        public const string UserCanAddCheckRegister = "UserCanAddCheckRegister";
        public const string UserCanEditCheckRegister = "UserCanEditCheckRegister";
        public const string UserCanDeleteCheckRegister = "UserCanDeleteCheckRegister";
        public const string UserCanExportCheckRegister = "UserCanExportCheckRegister";


        //Deposit
        public const string UserCanViewDeposit = "UserCanViewDeposit";
        public const string UserCanAddDeposit = "UserCanAddDeposit";
        public const string UserCanEditDeposit = "UserCanEditDeposit";
        public const string UserCanDeleteDeposit = "UserCanDeleteDeposit";
        public const string UserCanExportDeposit = "UserCanExportDeposit";

        //Post dated Check Deposit
        public const string UserCanViewPDC = "UserCanViewPDC";
        public const string UserCanAddPDC = "UserCanAddPDC";
        public const string UserCanEditPDC = "UserCanEditPDC";
        public const string UserCanDeletePDC = "UserCanDeletePDC";
        public const string UserCanExportPDC = "UserCanExportPDC";

        //Post dated Credit Voucher Deposit
        public const string UserCanViewPDV = "UserCanViewPDV ";
        public const string UserCanAddPDV = "UserCanAddPDV";
        public const string UserCanEditPDV = "UserCanEditPDV";
        public const string UserCanDeletePDV = "UserCanDeletePDV";
        public const string UserCanExportPDV = "UserCanExportPDV";


        #endregion

        #region Financial

        #region  Project

        public const string UserCanViewProjectData = "UserCanViewProjectData";
        public const string UserCanExportProjectViewList = "UserCanExportProjectData";
        public const string UserCanAddProjectData = "UserCanAddProjectData";
        public const string UserCanEditProjectData = "UserCanEditProjectData";
        public const string UserCanDeleteProjectData = "UserCanDeleteProjectData";

        #endregion

        #region Currency

        public const string UserCanViewCurrencyData = "UserCanViewCurrencyData";
        public const string UserCanExportCurrencyViewList = "UserCanExportCurrencyData";
        public const string UserCanAddCurrencyData = "UserCanAddCurrencyData";
        public const string UserCanEditCurrencyData = "UserCanEditCurrencyData";
        public const string UserCanDeleteCurrencyData = "UserCanDeleteCurrencyData";
        public const string UserCanViewCurrencyDetails = "UserCanViewCurrencyDetails";

        #endregion

        #region Tax Group

        public const string UserCanViewTaxGroup = "UserCanViewTaxGroup";
        public const string UserCanAddTaxGroup = "UserCanAddTaxGroup";
        public const string UserCanEditTaxGroup = "UserCanEditTaxGroup";
        public const string UserCanDeleteTaxGroup = "UserCanDeleteTaxGroup";
        public const string UserCanDeActivateReActivateTaxGroup = "UserCanDeactivateOrReactivateTaxGroup";
        public const string UserCanExportTaxGroup = "UserCanExportTaxGroup";
        public const string UserCanViewTaxGroupDetails = "UserCanViewTaxGroupDetails";

        #endregion

        #region Withholding Tax

        public const string UserCanViewWithholdingTax = "UserCanViewWithholdingTax";
        public const string UserCanAddWithholdingTax = "UserCanAddWithholdingTax";
        public const string UserCanEditWithholdingTax = "UserCanEditWithholdingTax";
        public const string UserCanDeleteWithholdingTax = "UserCanDeleteWithholdingTax";
        public const string UserCanDeactivateReactivateWithholdingTax = "UserCanDeactivateReactivateWithholdingTax";
        public const string UserCanExportWithholdingTax = "UserCanExportWithholdingTax";

        #endregion

        // Posting Period
        public const string UserCanViewPostingPeriod = "UserCanViewPostingPeriod";
        public const string UserCanAddPostingPeriod = "UserCanAddPostingPeriod";
        public const string UserCanEditPostingPeriod = "UserCanEditPostingPeriod";
        public const string UserCanDeletePostingPeriod = "UserCanDeletePostingPeriod";

        // Posting Indocator
        public const string UserCanViewPeriodIndicator = "UserCanViewPeriodIndicator";
        public const string UserCanAddPeriodIndicator = "UserCanAddPeriodIndicator";
        public const string UserCanEditPeriodIndicator = "UserCanEditPeriodIndicator";
        public const string UserCanDeletePeriodIndicator = "UserCanDeletePeriodIndicator";

        // Chart Of Accounts
        public const string UserCanViewGLAccount = "UserCanViewGLAccount";
        public const string UserCanAddGLAccount = "UserCanAddGLAccount";
        public const string UserCanEditGLAccount = "UserCanEditGLAccount";
        public const string UserCanDeleteGLAccount = "UserCanDeleteGLAccount";

        // G/L Account Setup
        public const string UserCanViewGLAccountSetup = "UserCanViewGLAccountSetup";
        public const string UserCanAddGLAccountSetup = "UserCanAddGLAccountSetup";
        public const string UserCanEditGLAccountSetup = "UserCanEditGLAccountSetup";
        public const string UserCanDeleteGLAccountSetup = "UserCanDeleteGLAccountSetup";

        // Journal Entry
        public const string UserCanViewJournalEntry = "UserCanViewJournalEntry";
        public const string UserCanAddJournalEntry = "UserCanAddJournalEntry";
        public const string UserCanEditJournalEntry = "UserCanEditJournalEntry";
        public const string UserCanDeleteJournalEntry = "UserCanDeleteJournalEntry";

        // Journal Voucher Entry
        public const string UserCanViewJournalVoucher = "UserCanViewJournalVoucher";
        public const string UserCanAddJournalVoucher = "UserCanAddJournalVoucher";
        public const string UserCanEditJournalVoucher = "UserCanEditJournalVoucher";
        public const string UserCanDeleteJournalVoucher = "UserCanDeleteJournalVoucher";

        // Transaction Code
        public const string UserCanViewTransactionControlNumber = "UserCanViewTransactionControlNumber";
        public const string UserCanExportTransactionControlNumber = "UserCanExportTransactionControlNumber";
        public const string UserCanAddTransactionControlNumber = "UserCanAddTransactionControlNumber";
        public const string UserCanEditTransactionControlNumber = "UserCanEditTransactionControlNumber";
        public const string UserCanDeleteTransactionControlNumber = "UserCanDeleteTransactionControlNumber";
        public const string UserCanDeActivateReActivateTransactionControlNumber = "UserCanDeActivateReActivateTransactionControlNumber";
        public const string UserCanViewTransactionControlNumberData = "UserCanViewTransactionControlNumberData";


        public const string UserCanViewFinancialReports = "Financial Reports";
        public const string UserCanAddFinancialReports = "UserCanAddFinancialReports";
        public const string UserCanEditFinancialReports = "UserCanEditFinancialReports";
        public const string UserCanDeleteFinancialReports = "UserCanDeleteFinancialReports";

        public const string UserCanViewAccounting = "UserCanViewAccounting";
        public const string UserCanAddAccounting = "UserCanAddAccounting";
        public const string UserCanEditAccounting = "UserCanEditAccounting";
        public const string UserCanDeleteAccounting = "UserCanDeleteAccounting";


        public const string UserCanViewGeneralLedger = "UserCanViewGeneralLedger";
        public const string UserCanAddGeneralLedger = "UserCanAddGeneralLedger";
        public const string UserCanEditGeneralLedger = "UserCanEditGeneralLedger";
        public const string UserCanDeleteGeneralLedger = "UserCanDeleteGeneralLedger";

        public const string UserCanViewTransactionJournal = "UserCanViewTransactionJournal";
        public const string UserCanAddTransactionJournal = "UserCanAddTransactionJournal";
        public const string UserCanEditTransactionJournal = "UserCanEditTransactionJournal";
        public const string UserCanDeleteTransactionJournal = "UserCanDeleteTransactionJournal";

        public const string UserCanViewTransactionByProject = "UserCanViewTransactionByProject";
        public const string UserCanAddTransactionByProject = "UserCanAddTransactionByProject";
        public const string UserCanEditTransactionByProject = "UserCanEditTransactionByProject";
        public const string UserCanDeleteTransactionByProject = "UserCanDeleteTransactionByProject";

        public const string UserCanViewDocumentJournal = "UserCanViewDocumentJournal";
        public const string UserCanAddDocumentJournal = "UserCanAddDocumentJournal";
        public const string UserCanEditDocumentJournal = "UserCanEditDocumentJournal";
        public const string UserCanDeleteDocumentJournal = "UserCanDeleteDocumentJournal";

        public const string UserCanViewVendorAging = "UserCanViewVendorAging";
        public const string UserCanAddVendorAging = "UserCanAddVendorAging";
        public const string UserCanEditVendorAging = "UserCanEditVendorAging";
        public const string UserCanDeleteVendorAging = "UserCanDeleteVendorAging";

        public const string UserCanViewCustomerAging = "UserCanViewCustomerAging";
        public const string UserCanAddCustomerAging = "UserCanAddCustomerAging";
        public const string UserCanEditCustomerAging = "UserCanEditCustomerAging";
        public const string UserCanDeleteCustomerAging = "UserCanDeleteCustomerAging";

        public const string UserCanViewFinancial = "UserCanViewFinancial";
        public const string UserCanAddFinancial = "UserCanAddFinancial";
        public const string UserCanEditFinancial = "UserCanEditFinancial";
        public const string UserCanDeleteFinancial = "UserCanDeleteFinancial";

        public const string UserCanViewBalanceSheet = "UserCanViewBalanceSheet";
        public const string UserCanAddBalanceSheet = "UserCanAddBalanceSheet";
        public const string UserCanEditBalanceSheet = "UserCanEditBalanceSheet";
        public const string UserCanDeleteBalanceSheet = "UserCanDeleteBalanceSheet";

        public const string UserCanViewTrialBalance = "UserCanViewTrialBalance";
        public const string UserCanAddTrialBalance = "UserCanAddTrialBalance";
        public const string UserCanEditTrialBalance = "UserCanEditTrialBalance";
        public const string UserCanDeleteTrialBalance = "UserCanDeleteTrialBalance";

        public const string UserCanViewPNL = "UserCanViewPNL";
        public const string UserCanAddPNL = "UserCanAddPNL";
        public const string UserCanEditPNL = "UserCanEditPNL";
        public const string UserCanDeletePNL = "UserCanDeletePNL";

        public const string UserCanViewCashFlow = "UserCanViewCashFlow";
        public const string UserCanAddCashFlow = "UserCanAddCashFlow";
        public const string UserCanEditCashFlow = "UserCanEditCashFlow";
        public const string UserCanDeleteCashFlow = "UserCanDeleteCashFlow";


        #region Reconcile GL
        public const string UserCanViewITRGL = "ITRGL1";
        public const string UserCanAddITRGL = "ITRGL2";
        public const string UserCanEditITRGL = "ITRGL3";
        #endregion

        #endregion

        #region Production

        #region BOM

        public const string UserCanViewBillOfMaterials = "UserCanViewBillOfMaterials";
        public const string UserCanEditBillOfMaterials = "UserCanEditBillOfMaterials";
        public const string UserCanAddBillOfMaterials = "UserCanAddBillOfMaterials";
        public const string UserCanExportBillOfMaterials = "UserCanExportBillOfMaterials";
        public const string UserCanDeleteBillOfMaterials = "UserCanDeleteBillOfMaterials";
        public const string UserCanRecommendForApprovalBillOfMaterials = "UserCanRecommendForApprovalBillOfMaterials";
        
        #endregion

        #endregion

        #region Vehicle

        #region  Vehicle Types

        public const string UserCanViewVehicleTypeData = "UserCanViewVehicleTypeData";
        public const string UserCanExportVehicleTypeViewList = "UserCanExportVehicleTypeData";
        public const string UserCanAddVehicleTypeData = "UserCanAddVehicleTypeData";
        public const string UserCanEditVehicleTypeData = "UserCanEditVehicleTypeData";
        public const string UserCanDeleteVehicleTypeData = "UserCanDeleteVehicleTypeData";
        public const string UserCanDeActivateReActivateVehicleType = "UserCanDeactivateOrReactivateVehicleTypeData";

        #endregion

        #region  Coop Vehicles

        public const string UserCanViewCoopVehiclesData = "UserCanViewCoopVehiclesData";
        public const string UserCanExportCoopVehiclesViewList = "UserCanExportCoopVehiclesData";
        public const string UserCanAddCoopVehiclesData = "UserCanAddCoopVehiclesData";
        public const string UserCanEditCoopVehiclesData = "UserCanEditCoopVehiclesData";
        public const string UserCanDeleteCoopVehiclesData = "UserCanDeleteCoopVehiclesData";
        public const string UserCanAssignReassignCoopVehiclesData = "UserCanAssignReassignCoopVehiclesData";
        public const string UserCanEndorseCoopVehiclesForInspection = "UserCanEndorseCoopVehiclesForInspection";
        public const string UserCanEndorseCoopVehiclesForRepair = "UserCanEndorseCoopVehiclesForRepair";

        #endregion

        #region Gas Station

        public const string UserCanViewGasStationData = "UserCanViewGasStationData";
        public const string UserCanExportGasStationViewList = "UserCanExportGasStationData";
        public const string UserCanAddGasStationData = "UserCanAddGasStationData";
        public const string UserCanEditGasStationData = "UserCanEditGasStationData";
        public const string UserCanDeleteGasStationData = "UserCanDeleteGasStationData";
        public const string UserCanDeActivateReActivateGasStation = "UserCanDeactivateOrReactivateGasStationData";

        #endregion

        #region Employee With License No

        public const string UserCanViewEmployeeWithLicenseNoList = "UserCanViewEmployeeWithLicenseNoList";
        public const string UserCanExportEmployeeWithLicenseNoList = "UserCanExportEmployeeWithLicenseNoList";
        public const string UserCanAddEmployeeWithLicenseNo = "UserCanAddEmployeeWithLicenseNo";
        public const string UserCanEditEmployeeWithLicenseNo = "UserCanEditEmployeeWithLicenseNo";

        #endregion

        #region  Travel Order

        public const string UserCanViewTravelOrderData = "UserCanViewTravelOrderData";
        public const string UserCanExportTravelOrderViewList = "UserCanExportTravelOrderData";
        public const string UserCanAddTravelOrderData = "UserCanAddTravelOrderData";
        public const string UserCanEditTravelOrderData = "UserCanEditTravelOrderData";
        public const string UserCanDeleteTravelOrderData = "UserCanDeleteTravelOrderData";

        #endregion

        #region Vehicle Request

        public const string UserCanViewVehicleRequestList = "UserCanViewVehicleRequestList";
        public const string UserCanExportVehicleRequestList = "UserCanExportVehicleRequestList";
        public const string UserCanAddVehicleRequestData = "UserCanAddVehicleRequestData";
        public const string UserCanEditVehicleRequestData = "UserCanEditVehicleRequestData";
        public const string UserCanDeleteVehicleRequestData = "UserCanDeleteVehicleRequestData";

        #endregion

        #region Vehicle Inspection

        public const string UserCanViewVehicleInspectionList = "UserCanViewVehicleInspectionList";
        public const string UserCanExportVehicleInspectionList = "UserCanExportVehicleInspectionList";
        public const string UserCanEditVehicleInspectionData = "UserCanEditVehicleInspectionData";
        public const string UserCanInspectReInspectVehicleInspectionData = "UserCanInspectReInspectVehicleInspectionData";
        public const string UserCanRecommendVehicleInspectionForApproval = "UserCanRecommendVehicleInspectionForApproval";
        public const string UserCanRecommendVehicleInspectionForGasSlip = "UserCanRecommendVehicleInspectionForGasSlip";
        public const string UserCanEndorseToMotorpolSection = "UserCanEndorseToMotorpolSection";
        public const string UserCanIssueGasSlip = "UserCanIssueGasSlip";
        public const string UserCanPrintGasSlip = "UserCanPrintGasSlip";

        #endregion

        #region Records of Travel

        public const string UserCanViewRecordsOfTravelList = "UserCanViewRecordsOfTravelList";
        public const string UserCanExportRecordsOfTravelList = "UserCanExportRecordsOfTravelList";
        public const string UserCanProccessDeparture = "UserCanProccessDeparture";
        public const string UserCanProccessArrival = "UserCanProccessArrival";

        #endregion

        #region Motorpol Section

        public const string UserCanViewMotorpolSectionList = "UserCanViewMotorpolSectionList";
        public const string UserCanExportMotorpolSectionList = "UserCanExportMotorpolSectionList";
        public const string UserCanRepairInMotorpoSection = "UserCanRepairInMotorpoSection";

        #endregion

        #endregion

        #region Reports

        public const string UserCanViewMemberReport = "UserCanViewMemberReport";
        public const string UserCanViewHouseWiringReport = "UserCanViewHouseWiringReport";
        public const string UserCanViewConnectionReport = "UserCanViewConnectionReport";

        public const string UserCanViewCashCountReport = "UserCanViewCashCountReport";

        public const string UserCanViewSeniorCitizenDiscountReport = "UserCanViewSeniorCitizenDiscountReport";
        public const string UserCanViewConsumerForConnectionReport = "UserCanViewConsumerForConnectionReport";
        public const string UserCanViewConsumerForDisconnectionReport = "UserCanViewConsumerForDisconnectionReport";

        public const string UserCanViewComplaintReport = "UserCanViewComplaintReport";
        public const string UserCanViewComplaintReceivedAndAttendedReport = "UserCanViewComplaintReceivedAndAttendedReport";

        public const string UserCanViewMeterLabReport = "UserCanViewMeterLabReport";

        #endregion

        #region Transformer

        #region Testing

        public const string UserCanViewTransformerForTestingData = "UserCanViewTransformerForTestingData";
        public const string UserCanTestTransformerData = "UserCanTestTransformerData";
        public const string UserCanViewTransformerSlip = "UserCanViewTransformerSlip";
        public const string UserCanExportTransformerViewList = "UserCanExportTransformerData";

        #endregion

        #region Outgoing Distribution Transformer

        public const string UserCanViewOutgoingDistributionTransformer = "UserCanViewOutgoingDistributionTransformerData";
        public const string UserCanExportOutgoingDistributionTransformer = "UserCanExportOutgoingDistributionTransformerData";
        public const string UserCanViewOrPrintOutgoingDistributionTransformerSlip = "UserCanViewOrPrintOutgoingDistributionTransformerSlip";
        public const string UserCanAddOutgoingDistributionTransformer = "UserCanAddOutgoingDistributionTransformer";

        #endregion

        #region For Inventory

        public const string UserCanViewTransformerForInventoryData = "UserCanViewTransformerForInventoryData";
        public const string UserCanTestTransformerForInventoryData = "UserCanTestTransformerForInventoryData";
        public const string UserCanViewTransformerForInventorySlip = "UserCanViewTransformerForInventorySlip";
        public const string UserCanExportTransformerForInventoryViewList = "UserCanExportTransformerForInventoryData";

        #endregion

        #endregion

        #region Metering

        #region Meter

        public const string UserCanExportKWHMeterList = "UserCanExportKWHMeterList";
        public const string UserCanViewKWHMeterList = "UserCanViewKWHMeterList";
        public const string UserCaEditKHWMeter = "UserCaEditKHWMeter";
        public const string UserCaAddKHWMeter = "UserCaAddKHWMeter";
        public const string UserCaDeleteKHWMeter = "UserCaDeleteKHWMeter";

        #endregion

        #region Testing

        public const string UserCanExportCalibrateKWHMeter = "UserCanExportCalibrateKWHMeter";
        public const string UserCanViewCalibrateKWHMeter = "UserCanViewCalibrateKWHMeter";
        public const string UserCanCalibrateKHWMeter = "UserCanCalibrateKHWMeter";
        public const string UserCanPrintMeterForCalibratingWithdrawalSlip = "UserCanPrintMeterForCalibratingWithdrawalSlip";

        #endregion

        #region Withdrawal

        public const string UserCanViewWithdrawalKWHMeter = "UserCanViewWithdrawalKWHMeter";
        public const string UserCanWithdrawKHWMeter = "UserCanWithdrawKHWMeter";

        #endregion

        #endregion

        #region Material

        //Material Request
        public const string UserCanExportMaterialRequestList = "UserCanExportMaterialRequestList";
        public const string UserCanViewMaterialRequestList = "UserCanViewMaterialRequestList";
        public const string UserCanAddMaterialRequest = "UserCanAddMaterialRequest";
        public const string UserCanEditMaterialRequest = "UserCanEditMaterialRequest";
        public const string UserCanCancelMaterialRequest = "UserCanCancelMaterialRequest";
        public const string UserCanViewMaterialRequestData = "UserCanViewMaterialRequestData";
        public const string UserCanViewMaterialRequestHistory = "UserCanViewMaterialRequestHistory";
        public const string UserCanRecommendMaterialRequestForApproval = "UserCanRecommendMaterialRequestForApproval";
        public const string UserCanProcessMaterialRequestToPurchaseRV = "UserCanProcessMaterialRequestToPurchaseRV";
        public const string UserCanProcessMaterialRequestToStockRV = "UserCanProcessMaterialRequestToStockRV";
        public const string UserCanPrintMaterialRequest = "UserCanPrintMaterialRequest";

        //Stock Requisition Voucher
        public const string UserCanExportStockRequisitionVoucherList = "UserCanExportStockRequisitionVoucherList";
        public const string UserCanViewStockRequisitionVoucherList = "UserCanViewStockRequisitionVoucherList";
        public const string UserCanAddStockRequisitionVoucher = "UserCanAddStockRequisitionVoucher";
        public const string UserCanEditStockRequisitionVoucher = "UserCanEditStockRequisitionVoucher";
        public const string UserCanCancelStockRequisitionVoucher = "UserCanCancelStockRequisitionVoucher";
        public const string UserCanRecommendStockRequisitionVoucherForApproval = "UserCanRecommendStockRequisitionVoucherForApproval";
        public const string UserCanViewStockRequisitionVoucherData = "UserCanViewStockRequisitionVoucherData";
        public const string UserCanViewStockRequisitionVoucherHistory = "UserCanViewStockRequisitionVoucherHistory";
        public const string UserCanPrintStockRequisitionVoucher = "UserCanPrintStockRequisitionVoucher";

        //Purchase Requisition Voucher 
        public const string UserCanExportPurchaseRequisitionVoucherList = "UserCanExportPurchaseRequisitionVoucherList";
        public const string UserCanViewPurchaseRequisitionVoucherList = "UserCanViewPurchaseRequisitionVoucherList";
        public const string UserCanAddPurchaseRequisitionVoucher = "UserCanAddPurchaseRequisitionVoucher";
        public const string UserCanEditPurchaseRequisitionVoucher = "UserCanEditPurchaseRequisitionVoucher";
        public const string UserCanCancelPurchaseRequisitionVoucher = "UserCanCancelPurchaseRequisitionVoucher";
        public const string UserCanRecommendPurchaseRequisitionVoucherForApproval = "UserCanRecommendPurchaseRequisitionVoucherForApproval";
        public const string UserCanViewPurchaseRequisitionVoucherData = "UserCanViewPurchaseRequisitionVoucherData";
        public const string UserCanViewPurchaseRequisitionVoucherHistory = "UserCanViewPurchaseRequisitionVoucherHistory";
        public const string UserCanPrintPurchaseRequisitionVoucher = "UserCanPrintPurchaseRequisitionVoucher";

        #endregion
    }
}