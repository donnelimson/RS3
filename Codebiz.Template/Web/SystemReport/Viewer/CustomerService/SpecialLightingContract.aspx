<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.CSA.Application" %>
<%@ Import Namespace="Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication" %>

<%@ Import Namespace="System.IO" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Transformer Rental Contract</title>
        <script runat="server">
            void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                    var accountService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAccountService)) as IAccountService;
                    var _contentFileService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IContentFileService)) as IContentFileService;
                    var _appUserService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAppUserServices)) as IAppUserServices;
                    var currentUserService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAppUserServices)) as IAppUserServices;
                    int appuserId = int.Parse(Server.UrlDecode(Request.QueryString["appuserId"]));
                    var dbSet = new CsaDataset();

                    int id = int.Parse(Server.UrlDecode(Request.QueryString["id"]));
                    var getData=accountService.GetDetailsForContract(id);
                    var approver = _appUserService.GetById(getData[0].ApproverId.Value);

                    var signatureImg = _contentFileService.GetCurrentCroppedSignatureByMemberId(getData[0].MemberId);
                    var signatureUrl = signatureImg != null
                        ? "file:\\\\" + signatureImg.Folder + "\\Cropped/" + signatureImg.FileName
                        : ""; //"file:\\\\" + Server.MapPath("~/assets/global/img") + "/" + "no-signature.jpg";

                    var approverSignatureImg = _contentFileService.GetCurrentCroppedAppUserSignatureByAppUserId(approver != null ? approver.AppUserId : 0);
                    var approverSignatureUrl = approverSignatureImg != null
                        ? "file:\\\\" + approverSignatureImg.Folder + "\\Cropped/" + approverSignatureImg.FileName
                        : ""; //"file:\\\\" + Server.MapPath("~/assets/global/img") + "/" + "no-signature.jpg";

                    var witness1sig=_contentFileService.GetCurrentCroppedWitnessSignatureByMemberId(getData[0].MemberId,0);
                    var witness1sigUrl = witness1sig != null
                     ? "file:\\\\" + witness1sig.Folder + "\\Cropped/" + witness1sig.FileName
                     : "";

                    var witness2sig=_contentFileService.GetCurrentCroppedWitnessSignatureByMemberId(getData[0].MemberId,1);
                    var witness2sigUrl = witness2sig != null
                    ? "file:\\\\" + witness2sig.Folder + "\\Cropped/" + witness2sig.FileName
                    : "";
                    var representativeSig=_contentFileService.GetCurrentCroppedSignatureByMemberId(getData[0].MemberId,(int)PersonTypes.Representative);
                    var representativesigUrl = representativeSig != null
                   ? "file:\\\\" + representativeSig.Folder + "\\Cropped/" + representativeSig.FileName
                    : "";
                    getData[0].ConsumerSignature = signatureUrl;
                    getData[0].ApproverSignature = approverSignatureUrl;
                    getData[0].ApproverName = approver != null ? approver.FullName.ToUpper() : "";
                    getData[0].ApproverPosition = approver != null ? approver.Position?.Name.ToUpper() : "";
                    getData[0].Witness1Signature = witness1sigUrl;
                    getData[0].Witness2Signature = witness2sigUrl;
                    getData[0].RepresentativeSignature = representativesigUrl;
                    getData[0].MonthYear = DateTime.Now.ToString("MMMM yyyy");
                    getData[0].DayMonthYear = DateTime.Now.ToString(" d")+GetDateSuffix(int.Parse(DateTime.Now.ToString("dd")))+" day of "+DateTime.Now.ToString("MMMM")+" "+DateTime.Now.ToString("yyyy");
                    getData[0].OfficeAddress = currentUserService.GetById(appuserId)?.Office?.OfficeInfo;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/CustomerService/SpecialLightingContract.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SpecialLightingContract", getData));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SpecialLightingItems", getData[0].Items));
                    ReportViewer1.LocalReport.EnableHyperlinks = true;
                    ReportViewer1.LocalReport.DisplayName = $"SpecialLightingAccountContract{DateTime.Now.ToString("yyyyMMddHHmmss")}";

                    this.ReportViewer1.LocalReport.Refresh();
                    this.ReportViewer1.Font.Name = "Tahoma";
                    this.ReportViewer1.KeepSessionAlive = false;
                }
            }
            private string GetDateSuffix(int day)
            {
                switch (day)
                {
                    case 1:
                    case 21:
                    case 31:
                        return "st";
                    case 2:
                    case 22:
                        return "nd";
                    case 3:
                    case 23:
                        return "rd";
                    default:
                        return "th";
                }
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="report-container">
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <rsweb:ReportViewer AsyncRendering="False" SizeToReportContent="True" ID="ReportViewer1" runat="server" PageCountMode="Actual" 
                Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="SystemReport/CustomerService/RDLC/SpecialLightingContract.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
