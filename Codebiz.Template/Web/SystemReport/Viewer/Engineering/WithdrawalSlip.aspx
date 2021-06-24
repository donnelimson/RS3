<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.CSA" %>
<%@ Import Namespace="Codebiz.Domain.ERP.Model.DTO.CSA"%>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>KHW Meter - Withdrawal Slip</title>
        <script runat="server">
            void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                    var _reportService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IWithdrawalService)) as IWithdrawalService;
                    var _appUserService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAppUserServices)) as IAppUserServices;

                     var details = new List<WithdrawalSlipReportDTO>();

                    var PdfFolder = _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.WithdrawalSlipPDFFolder);
                    if (PdfFolder == null)
                        throw new ArgumentNullException("PdfFolder");

                    var currentContext = HttpContext.Current;
                    string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';
                    int meterWithdrawalId = int.Parse(Server.UrlDecode(Request.QueryString["mtrWithrawalId"]));
                    int userId = int.Parse(Server.UrlDecode(Request.QueryString["currentUserId"]));

                    var getData = _reportService.GetWithdrawalSlipReport(meterWithdrawalId, userId);
                     details.Add(getData);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.AsyncRendering = false;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/Engineering/WithdrawalSlip.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("WithdrawalSlipDataset", details));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("MetersWithdrawnDataset", getData.MetersWithdrawn));
                    ReportViewer1.LocalReport.EnableHyperlinks = true;
                    ReportViewer1.LocalReport.DisplayName = $"WithdrawalSlip{DateTime.Now.ToString("yyyyMMddHHmmss")}";

                    this.ReportViewer1.LocalReport.Refresh();
                    this.ReportViewer1.Font.Name = "Tahoma";
                    toPDF(PdfFolder, $"Withdrawal Slip-{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                }
            }

            //To PDF
            private void toPDF(string path, string fileName)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if (fi.Exists) fi.Delete();

                Warning[] warnings;
                string[] streamids;
                string mimeType, encoding, filenameExtension;
                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                System.IO.FileStream fs = System.IO.File.Create($@"{path}\Withdrawal_Slip_{fileName}.pdf");
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();


                //Open Created PDF
                Response.Clear();
                string filePath = $@"{path}\Withdrawal_Slip_{fileName}.pdf";
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
                <LocalReport ReportPath="SystemReport\CSA\RDLC\Engineering\WithdrawalSlip.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
