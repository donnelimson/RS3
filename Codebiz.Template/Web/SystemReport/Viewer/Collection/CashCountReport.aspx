<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.CSA" %>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashCountReport.aspx.cs" Inherits="Web.SystemReport.CashCountReport" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cash Count Report</title>
      <script runat="server">
          void Page_Load(object sender, EventArgs e)
          {
              if (!Page.IsPostBack)
              {
                  var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                  var _reportService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ICollectionService)) as ICollectionService;

                  var dbSet = new CsaDataset();

                  var currentContext = HttpContext.Current;
                  string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';

                  var dateFrom = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateFrom"])).Date;
                  var dateTo = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateTo"])).Date;
                  int tempVal;
                  int? officeId = Int32.TryParse(Server.UrlDecode(Request.QueryString["officeId"]), out tempVal) ? Int32.Parse(Server.UrlDecode(Request.QueryString["officeId"])) : (int?)null;
                  int? tellerId = Int32.TryParse(Server.UrlDecode(Request.QueryString["tellerId"]), out tempVal) ? Int32.Parse(Server.UrlDecode(Request.QueryString["tellerId"])) : (int?)null;
                  int appUserId =Int32.Parse(Server.UrlDecode(Request.QueryString["appUserId"]));

                  var getData=_reportService.GetDetailsForCashCountReport(officeId,tellerId, dateFrom, dateTo.AddDays(1),appUserId);
                  getData[0].Header = "For the period of " + dateFrom.ToString("MMMM, dd yyyy") + " - " + dateTo.ToString("MMMM, dd yyyy");

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.AsyncRendering = false;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/Collection/CashCountReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CashCountReportDataSet", getData));
                    ReportViewer1.LocalReport.EnableHyperlinks = true;
                    ReportViewer1.LocalReport.DisplayName = $"CashCountReport_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

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
                <LocalReport ReportPath="SystemReport\CSA\RDLC\CashCountReport.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
