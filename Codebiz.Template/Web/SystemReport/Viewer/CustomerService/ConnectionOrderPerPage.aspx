<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="Infrastructure.Services" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConnectionOrderPerPage.aspx.cs" Inherits="Web.SystemReport.Rdlc.ConnectionOrderPerPage" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Connection Order</title>
        <script runat="server">
            void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                    var _reportService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IForConnectionService)) as IForConnectionService;
                    var PdfFolder = _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.ConnectionOrderPdfFolder);
                    if (PdfFolder == null)
                        throw new ArgumentNullException("PdfFolder");

                    var dbSet = new CsaDataset();
                    var currentContext = HttpContext.Current;
                    string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';

                    int officeId = int.Parse(Server.UrlDecode(Request.QueryString["officeId"]));
                    var dateFrom = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateFrom"]));
                    var dateTo = DateTime.Parse(Server.UrlDecode(Request.QueryString["dateTo"])).Date;
                    int appUserId = int.Parse(Server.UrlDecode(Request.QueryString["appUserId"]));

                    var data = _reportService.GenerateConnectionOrderPerPage(officeId, dateFrom, dateTo.AddDays(1),appUserId);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/CustomerService/ConnectionOrderPerPageReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ConnectionOrderPerPageDataset", data));
                    ReportViewer1.LocalReport.EnableHyperlinks = true;
                    ReportViewer1.LocalReport.DisplayName = $"Connection_Order_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

                    this.ReportViewer1.LocalReport.Refresh();
                    this.ReportViewer1.Font.Name = "Tahoma";
                    exportToExcel(PdfFolder, $"Connection Order-{officeId.ToString()}");
                }
            }
            private void exportToExcel(string path, string fileName)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if (fi.Exists) fi.Delete();

                Warning[] warnings;
                string[] streamids;
                string mimeType, encoding, filenameExtension;
                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                System.IO.FileStream fs = System.IO.File.Create($@"{path}\Connection_Order_{fileName}.pdf");
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();


                //Open Created PDF
                Response.Clear();
                string filePath = $@"{path}\Connection_Order_{fileName}.pdf";
                Response.ContentType = "application/pdf";
                Response.WriteFile(filePath);
                Response.Flush();
                Response.End();
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="report-container">
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <rsweb:ReportViewer AsyncRendering="False" SizeToReportContent="True" ID="ReportViewer1" runat="server" PageCountMode="Actual" 
                Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="SystemReport\CSA\RDLC\ConnectionOrderPerPageReport.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
