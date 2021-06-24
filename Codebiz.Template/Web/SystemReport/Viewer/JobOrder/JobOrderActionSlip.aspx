<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.CSA"%>
<%@ Import Namespace="Codebiz.Domain.ERP.Model.DTO.CSA.Application"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job Order Action Slip</title>
    <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                var _jorderService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IJobOrderService)) as IJobOrderService;
                var _signatureService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ISignatureService)) as ISignatureService;

                var dbSet = new CsaDataset();

                var currentContext = HttpContext.Current;
                string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';

                int jobOrderRoutingId = int.Parse(Server.UrlDecode(Request.QueryString["jobOrderRoutingId"]));
                var details = new List<JobOrderDetailsASTO>();

                var getData = _jorderService.GetDetailsForActionSlip(jobOrderRoutingId,currentContext);
                details.Add(getData.Detail);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/JobOrder/JobOrderActionSlip.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("JobOrderActionSlipDataSet", details));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("JobOrderNaturesActionSlipDataSet", getData.Natures));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("JobOrderEndorseTosActionSlipDataSet", getData.EndorseTos));

                ReportViewer1.LocalReport.EnableHyperlinks = true;
                this.ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.DisplayName = $"JobOrderActionSlip_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
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
                <LocalReport ReportPath="SystemReport\CSA\RDLC\JobOrderComplaintActionSlip.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
