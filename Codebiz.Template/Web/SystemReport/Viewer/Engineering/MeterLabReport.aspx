<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services.CSA.Meter" %>
<%@ Import Namespace="Infrastructure.Services" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Meter Lab Report</title>
        <script runat="server">
            void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                    var _reportService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IIncomingMeterService)) as IIncomingMeterService;
                    var currentUserService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAppUserServices)) as IAppUserServices;

                    var dbSet = new CsaDataset();

                    var currentContext = HttpContext.Current;
                    string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';
                    var dateFrom = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateFrom"])).Date;
                    var dateTo = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateTo"])).Date;
                    int tempVal;
                    int? statusId = Int32.TryParse(Server.UrlDecode(Request.QueryString["statusId"]), out tempVal) ? Int32.Parse(Server.UrlDecode(Request.QueryString["statusId"])) : (int?)null;
                    int appUserId = int.Parse(Server.UrlDecode(Request.QueryString["appUserId"]));

                    var getData=_reportService.GetDetailsForReport(dateFrom, dateTo.AddDays(1), statusId);
                    getData[0].Period = "For the period of " + dateFrom.ToString("MMMM, dd yyyy")+ " - " + dateTo.ToString("MMMM, dd yyyy");
                    getData[0].Office = currentUserService.GetById(appUserId)?.CurrentOffice;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.AsyncRendering = false;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/Engineering/MeterLabReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("MeterLabReportDataSet", getData));
                    ReportViewer1.LocalReport.EnableHyperlinks = true;
                    ReportViewer1.LocalReport.DisplayName = $"MeterLabReport_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

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
                <LocalReport ReportPath="SystemReport\CSA\RDLC\Engineering\MeterLabReport.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
