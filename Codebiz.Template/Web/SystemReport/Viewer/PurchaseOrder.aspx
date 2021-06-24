<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="Web.Views.Ordering.Reports.PurchaseOrder" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Web" %>
<%@ Import Namespace="Microsoft.Reporting.WebForms" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Web.Models.ViewModels.Ordering" %>
<%@ Import Namespace="Infrastructure.Services" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <script src="Script/PurchaseOrderController.js"></script>  
    <title></title>
            <script runat="server">
                void Page_Load(object sender, EventArgs e)
                {
                    if (!Page.IsPostBack)
                    {
                        int orderId = int.Parse(Server.UrlDecode(Request.QueryString["orderId"]));
                        int supplierId = int.Parse(Server.UrlDecode(Request.QueryString["supplierId"]));
                        string purchaseOrderCode = Server.UrlDecode(Request.QueryString["purchaseOrderCode"]);
                        makePdf(purchaseOrderCode);
                    }
                }
                private void makePdf(string purchaseOrderCode)
                {
                    Response.Clear();
                     var _configSettingService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IConfigSettingService)) as IConfigSettingService;
                     var PdfFolder = _configSettingService.GetValueById((int)Codebiz.Domain.Common.Model.Enums.ConfigurationSettings.PurchaseOrderPdfPath);
                      //string filePath = "C:\\Users\\Donnel Imson\\Downloads\\PurchaseOrders\\"+purchaseOrderCode+".pdf";
                    string filePath =PdfFolder+purchaseOrderCode+".pdf";
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
                <LocalReport ReportPath="Views\Ordering\Reports\Report1.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
