<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="System.Drawing.Printing" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.CSA.Billing" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Threading" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Statement of Account</title>
        <script runat="server">
            void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                    var _reportService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IBillingTransactionSOAService)) as IBillingTransactionSOAService;
                    var _appUserService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAppUserServices)) as IAppUserServices;
                    var _routeMeterReadingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRouteMeterReadingService)) as IRouteMeterReadingService;
                    var currentContext = HttpContext.Current;
                    string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';
                    int accountId=int.Parse(Server.UrlDecode(Request.QueryString["accountId"]));
                    string billingPeriod=Server.UrlDecode(Request.QueryString["billingPeriod"]);
                    bool isDraft=bool.Parse(Server.UrlDecode(Request.QueryString["isDraft"]));
                    int? currentReading=int.Parse(Server.UrlDecode(Request.QueryString["currentReading"]));
                    var resultData =  new Codebiz.Domain.ERP.Model.DTO.CSA.Billing.BillingTransactionSOADTO();
                    if (isDraft)
                    {
                        int billingTransactionId=int.Parse(Server.UrlDecode(Request.QueryString["billingTransactionId"]));
                        int userId=int.Parse(Server.UrlDecode(Request.QueryString["userId"]));
                        var model = new Codebiz.Domain.ERP.Model.ViewModel.CSA.BillingTransaction.BillingTransactionValidationViewModel
                        {
                            BillingPeriod = billingPeriod,
                            CurrentReading = currentReading == null ? 0 : currentReading.Value,
                            IsDraft = isDraft,
                            BillingTransactionId = billingTransactionId,
                            ForRelease = true
                        };
                        resultData = _routeMeterReadingService.GenerateSOA(model,userId).BillingTransactionSOADTO;
                    }
                    else
                    {
                        resultData = _reportService.GenerateSOA(accountId, billingPeriod, isDraft, currentReading, new List<Codebiz.Domain.ERP.Model.DataModel.CSA.Billing.BillingTransactionSOA>());
                    }
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.AsyncRendering = false;
     
                    if (!Directory.Exists(_configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.StatementOfAccountFolder)))
                    Directory.CreateDirectory(_configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.StatementOfAccountFolder));

                    string filePath = _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.StatementOfAccountFolder) + "\\" +"SOA"+resultData.SOAData[0].AccountNo + "_"+resultData.SOAData[0].BillingMonth+".pdf";
                    if (!File.Exists(filePath) || isDraft)
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/SystemReport/RDLC/Billing/StatementOfAccount.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SOAData", resultData.SOAData));
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SOAUnbundledTransactionForBill", resultData.SOAUnbundledTransationForBill));
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SOAUnbundledTransactionNotBill", resultData.SOAUnbundledTransationNotBill));
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("WithholdingTaxForBilling", resultData.WithholdingTaxForBilling));
                        byte[] renderedByte = ReportViewer1.LocalReport.Render("PDF", "", out  mimeType, out  encoding, out extension, out streamIds, out warnings);
                        var fs = new FileStream(filePath, FileMode.OpenOrCreate);
                        fs.Write(renderedByte, 0, renderedByte.Length);
                        fs.Close();
                    }
                    Thread.Sleep(100);
                    Response.ContentType = "application/pdf";
                    Response.WriteFile(filePath);
                    Response.End();
                    //Response.ContentType = "application/pdf";
                    //Response.Clear();
                    //Response.ContentType = mimeType;
                    //Response.AddHeader("content-disposition", "attachment; ffilename=soa." + extension);
                    //Response.BinaryWrite(renderedByte); // create the file
                    //Response.Flush(); // send it to the client to download
                    //ReportViewer1.LocalReport.EnableHyperlinks = true;
                    //this.ReportViewer1.LocalReport.Refresh();
                    //this.ReportViewer1.Font.Name = "Tahoma";
                    //this.ReportViewer1.KeepSessionAlive = false;
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
                <LocalReport ReportPath="SystemReport\CSA\RDLC\Billing\StatementOfAccount.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
