<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="System.IO" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Membership ID</title>
        <script runat="server">
            void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                    var _memberService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IMemberServices)) as IMemberServices;
                    var _contentFileService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IContentFileService)) as IContentFileService;
                    var _appUserService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAppUserServices)) as IAppUserServices;

                    var dbSet = new CsaDataset();

                    int memberId = int.Parse(Server.UrlDecode(Request.QueryString["memberId"]));
                    var consumerNo = Server.UrlDecode(Request.QueryString["consumerNo"]);
                    var getData=_memberService.GetMemberDetailsForMembershipIDPrinting(memberId);
                    var generalManger = _appUserService.GetGeneralManager();

                    var photoImg = _contentFileService.GetCurrentCroppedPhotoByMemberId(memberId);
                    var photoUrl = photoImg != null
                        ? "file:\\\\" + photoImg.Folder + "\\Cropped/" + photoImg.FileName
                        : ""; //"file:\\\\" + Server.MapPath("~/assets/global/img") + "/" + "photo-unavailable.jpg";

                    var signatureImg = _contentFileService.GetCurrentCroppedSignatureByMemberId(memberId);
                    var signatureUrl = signatureImg != null
                        ? "file:\\\\" + signatureImg.Folder + "\\Cropped/" + signatureImg.FileName
                        : ""; //"file:\\\\" + Server.MapPath("~/assets/global/img") + "/" + "no-signature.jpg";

                    var generalmanagersignatureImg = _contentFileService.GetCurrentCroppedAppUserSignatureByAppUserId(generalManger != null ? generalManger.AppUserId : 0);
                    var generalmanagersignatureUrl = generalmanagersignatureImg != null
                        ? "file:\\\\" + generalmanagersignatureImg.Folder + "\\Cropped/" + generalmanagersignatureImg.FileName
                        : ""; //"file:\\\\" + Server.MapPath("~/assets/global/img") + "/" + "no-signature.jpg";

                    string barcodeUrl="file:\\\\"+
                        _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.MembershipBarcodeFolder) + "/" +
                        consumerNo + ".gif";

                    getData[0].Barcode = barcodeUrl;
                    getData[0].MemberPhoto = photoUrl;
                    getData[0].Signature = signatureUrl;
                    getData[0].GeneralManagerSignature = generalmanagersignatureUrl;
                    getData[0].GeneralManager = generalManger != null ? generalManger.FullName.ToUpper() : "";

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/CustomerService/Member/MembershipID.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("MembershipIDDataset", getData));
                    ReportViewer1.LocalReport.EnableHyperlinks = true;
                    ReportViewer1.LocalReport.DisplayName = $"MembershipId{memberId}{DateTime.Now.ToString("yyyyMMddHHmmss")}";

                    this.ReportViewer1.LocalReport.Refresh();
                    this.ReportViewer1.Font.Name = "Tahoma";
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
                <LocalReport ReportPath="SystemReport/CSA/RDLC/Member/MembershipID.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
