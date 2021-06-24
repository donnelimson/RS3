<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.CSA.Transformer"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incoming Distribution Transformer Slip</title>
    <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                var _incomingDistributionTransformerService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ITransformerTestingService)) as ITransformerTestingService;
            
                var currentContext = HttpContext.Current;
                string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';

                int incomingDistributionTransformerId = int.Parse(Server.UrlDecode(Request.QueryString["IncomingDistributionTransformerId"]));
                var getData = _incomingDistributionTransformerService.GetDetailsForSlip(incomingDistributionTransformerId);
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/Engineering/IncomingDistributionTransformerSlip.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("IncomingDistributionTransformerDataSet", getData));
                ReportViewer1.LocalReport.EnableHyperlinks = true;
                ReportViewer1.LocalReport.DisplayName = $"IncomingDistributionTransformerSlip_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
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
                <LocalReport ReportPath="SystemReport\RDLC\Engineering\IncomingDistributionTransformerSlip.rdlc">
                </LocalReport>

            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
