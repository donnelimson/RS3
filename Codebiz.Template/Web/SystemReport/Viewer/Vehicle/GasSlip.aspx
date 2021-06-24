<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.Vehicles" %>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GasSlip.aspx.cs" Inherits="Web.SystemReport.Rdlc.GasSlip" %>--%>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gas Slip</title>
    <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                var _vehicleInspectionService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IVehicleInspectionService)) as IVehicleInspectionService;

                var PdfFolder = _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.GasSlipPdfFolder);
                if (PdfFolder == null)
                    throw new ArgumentNullException("PdfFolder");

                var dbset = new VehicleDataSet();

                var currentContext = HttpContext.Current;
                string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';

                int vehicleInspectionId =  int.Parse(Server.UrlDecode(Request.QueryString["vehicleInspectionId"]));
                var getData = _vehicleInspectionService.GetDetailsGasSlip(vehicleInspectionId);
                var articles = getData.SelectMany(a => a.Articles)
                    .Select(p => new
                    {
                        Article = p.Article,
                        Quantity = p.Quantity,
                        Remarks = p.Remarks
                    }).ToList();

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/Vehicle/GasSlip.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("GasSlipDataSet", getData));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ArticlesDataSet", articles));
                ReportViewer1.LocalReport.EnableHyperlinks = true;
                ReportViewer1.LocalReport.DisplayName = $"Gas_Slip_{DateTime.Now.ToString("yyyyMMddHHmmss")}";

                this.ReportViewer1.LocalReport.Refresh();
                this.ReportViewer1.Font.Name = "Tahoma";
                exportToExcel(PdfFolder, $"Gas Slip -{vehicleInspectionId.ToString()}");
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

            System.IO.FileStream fs = System.IO.File.Create($@"{path}\Gas_Slip{fileName}.pdf");
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();


            //Open Created PDF
            Response.Clear();
            string filePath = $@"{path}\Gas_Slip{fileName}.pdf";
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
            <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer AsyncRendering="false" SizeToReportContent="true" ID="ReportViewer1" runat="server" PageCountMode="Actual"
                Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                   <LocalReport ReportPath="SystemReport\Rdlc\GasSlip.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
