<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Withdrawal of Membership</title>
    <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                var _memberService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IMemberServices)) as IMemberServices;
                var _signatureService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ISignatureService)) as ISignatureService;

                var dbSet = new CsaDataset();

                var currentContext = HttpContext.Current;
                string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';

                int memberId = int.Parse(Server.UrlDecode(Request.QueryString["MemberId"]));
                //var photoUrl = "file:///C:/MemberSignatureFolder/Cropped/" + _signatureService.GetCurrentMemberPhotoByMemberId(memberId)?.CropImageContentFile.FileName;
                var photoUrl = "file:\\\\" + _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.MemberSignatureFolder) + "/Cropped/"+ _signatureService.GetCurrentMemberPhotoByMemberId(memberId)?.CropImageContentFile.FileName;
                var getData = _memberService.GetDetailsForWithdrawalReport(memberId);
                var newMemberId = getData[0].NewMemberId;
                var newMemberPhotoUrl = "file:\\\\" + _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.MemberSignatureFolder) + "/Cropped/" + _signatureService.GetCurrentMemberPhotoByMemberId(newMemberId)?.CropImageContentFile.FileName;
                getData[0].OldMemberSignature = photoUrl;
                getData[0].NewMemberSignature = newMemberPhotoUrl;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/CustomerService/Member/WithdrawalOfMembership.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("WithdrawalMembershipDataSet", getData));
                ReportViewer1.LocalReport.EnableHyperlinks = true;
                ReportViewer1.LocalReport.DisplayName = $"WithdrawalOfMembership_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
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
                <LocalReport ReportPath="SystemReport\CSA\RDLC\Member\WithdrawalOfMembership.rdlc">
                </LocalReport>

            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
