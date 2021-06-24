<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.CSA.Billing" %>
<%@ Import Namespace="PdfSharp.Pdf" %>
<%@ Import Namespace="PdfSharp.Pdf.IO" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="Web.Areas.CommercialServicesApplication.Models.DTOs" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Payment OR</title>
        <script runat="server">
            void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                
                    string filePath = "";
                    byte[] renderedByte = null;
                    PdfDocument result = new PdfDocument();
                    var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                    Response.Clear();
                    if (!PaymentORDTO.Existing)
                    {
                        string receiptFileName = _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.OfficialReceiptsFolder) + "\\" + PaymentORDTO.AccountNo + "_" + PaymentORDTO.OrDate.Value.ToString("yyyyMMddHHmmss");
                        var reports = PaymentORDTO.Reports;
                        Warning[] warnings;
                        string[] streamIds;
                        string mimeType = string.Empty;
                        string encoding = string.Empty;
                        string extension = string.Empty;
                        foreach (var report in reports.Where(x => x.LocalReport.DataSources.Count != 0).OrderByDescending(r=>r.CurrentSubPage).ThenByDescending(r=>r.CurrentPage))
                        {
                            report.ProcessingMode = ProcessingMode.Local;
                            report.SizeToReportContent = true;
                            report.AsyncRendering = false;
                            renderedByte = report.LocalReport.Render("PDF", "", out mimeType, out encoding, out extension, out streamIds, out warnings);
                            var fs = new FileStream(receiptFileName + report.LocalReport.DisplayName + ".pdf", FileMode.OpenOrCreate);
                            fs.Write(renderedByte, 0, renderedByte.Length);
                            fs.Close();
                            report.LocalReport.EnableHyperlinks = true;
                            report.LocalReport.Refresh();
                            report.Font.Name = "Tahoma";
                            report.KeepSessionAlive = false;
                            PdfDocument receipt = PdfReader.Open(receiptFileName + report.LocalReport.DisplayName + ".pdf", PdfDocumentOpenMode.Import);
                            BindPDF(receipt, result);
                            File.Delete(receiptFileName + report.LocalReport.DisplayName + ".pdf");
                        }
                        result.Save(receiptFileName+ ".pdf");
                        filePath = _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.OfficialReceiptsFolder) + "\\" + PaymentORDTO.AccountNo + "_" + PaymentORDTO.OrDate.Value.ToString("yyyyMMddHHmmss") + ".pdf";

                    }
                    else
                    {
                        filePath = _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.OfficialReceiptsFolder) + "\\" +PaymentORDTO.AccountNo + "_" + PaymentORDTO.OrDate.Value.ToString("yyyyMMddHHmmss") + ".pdf";
                    }
                    Response.ContentType = "application/pdf";
                    Thread.Sleep(100);
                    Response.WriteFile(filePath);
                    Response.End();
                }
            }
            private void BindPDF(PdfDocument from, PdfDocument to)
            {
                for (int i = 0; i < from.PageCount; i++)
                {
                     to.InsertPage(i, from.Pages[i]);
                }
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="report-container">
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            
        </div>
    </form>
</body>
</html>
