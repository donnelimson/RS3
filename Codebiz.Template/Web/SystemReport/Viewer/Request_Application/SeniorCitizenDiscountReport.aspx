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
    <title>Senior Citizen Discount Report</title>
      <script runat="server">
          void Page_Load(object sender, EventArgs e)
          {
              if (!Page.IsPostBack)
              {
                  var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                  var _reportService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IDiscountApplicationService)) as IDiscountApplicationService;
                  var _currentUserService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAppUserServices)) as IAppUserServices;
                  var dbSet = new CsaDataset();

                  var currentContext = HttpContext.Current;
                  string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';

                  var dateFrom = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateFrom"])).Date;
                  var dateTo = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateTo"])).Date;
                  int tempVal;
                  int? consumerTypeId = Int32.TryParse(Server.UrlDecode(Request.QueryString["consumerTypeId"]), out tempVal) ? Int32.Parse(Server.UrlDecode(Request.QueryString["consumerTypeId"])) : (int?)null;
                  int appUserId = int.Parse(Server.UrlDecode(Request.QueryString["appUserId"]));

                  var getData=_reportService.GetDetailsSeniorCitizenDiscountReport(consumerTypeId, dateFrom, dateTo.AddDays(1));
                  getData[0].Header = "For the period of " + dateFrom.ToString("MMMM, dd yyyy") + " - " + dateTo.ToString("MMMM, dd yyyy");
                  getData[0].Office = _currentUserService.GetById(appUserId)?.CurrentOffice;

                  ReportViewer1.ProcessingMode = ProcessingMode.Local;
                  ReportViewer1.SizeToReportContent = true;
                  ReportViewer1.AsyncRendering = false;
                  ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/Request_Application/SeniorCitizenDiscountReport.rdlc");
                  ReportViewer1.LocalReport.DataSources.Clear();
                  ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SeniorCitizenDiscountDataSet", getData));
                  ReportViewer1.LocalReport.EnableHyperlinks = true;
                  ReportViewer1.LocalReport.DisplayName = $"SeniorCitizenDiscountReport_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

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
                <LocalReport ReportPath="SystemReport\CSA\RDLC\SeniorCitizenDiscountReport.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
