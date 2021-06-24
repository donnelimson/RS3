
<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>


<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberReport.aspx.cs" Inherits="Web.SystemReport.MemberReport" %>--%>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Report</title>
    <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                var _reportService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IMemberServices)) as IMemberServices;
                var _appUserService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAppUserServices)) as IAppUserServices;

                var dbSet = new CsaDataset();
                var currentContext = HttpContext.Current;
                string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';

                var dateFrom = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateFrom"])).Date;
                var dateTo = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateTo"])).Date;

                int tempVal;
                int? statusId = Int32.TryParse(Server.UrlDecode(Request.QueryString["statusId"]), out tempVal) ? Int32.Parse(Server.UrlDecode(Request.QueryString["statusId"])) : (int?)null;
                
                int tempVal2;
                int? membershipTypeId = Int32.TryParse(Server.UrlDecode(Request.QueryString["membershipTypeId"]), out tempVal2) ? Int32.Parse(Server.UrlDecode(Request.QueryString["membershipTypeId"])) : (int?)null;
                
                int tempVal3;
                int? subCategoryId = Int32.TryParse(Server.UrlDecode(Request.QueryString["subCategoryId"]), out tempVal3) ? Int32.Parse(Server.UrlDecode(Request.QueryString["subCategoryId"])) : (int?)null;
                
                int appUserId = int.Parse(Server.UrlDecode(Request.QueryString["appUserId"]));

                var data = _reportService.GenerateMemberList(statusId,membershipTypeId,subCategoryId, dateFrom, dateTo.AddDays(1));
                data[0].Period = "For the period of " + dateFrom.ToString("MMMM dd, yyyy") + " - " + dateTo.ToString("MMMM dd, yyyy");
                data[0].Office = _appUserService.GetById(appUserId)?.CurrentOffice;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/CustomerService/Member/MemberReport.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("MemberReportDataSet", data));
                ReportViewer1.LocalReport.EnableHyperlinks = true;
                ReportViewer1.LocalReport.DisplayName = $"Member_Report_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

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
                <LocalReport ReportPath="SystemReport\CSA\RDLC\Member\MemberReport.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
