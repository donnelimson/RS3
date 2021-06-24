<%@ Import Namespace="Web.SystemReportDataSet" %>
<%@ Import Namespace="Infrastructure.Services" %>
<%@ Import Namespace="Infrastructure.Services.CSA" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConnectionOrderPerPage.aspx.cs" Inherits="Web.SystemReport.Rdlc.ConnectionOrderPerPage" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Material Request</title>
     <script runat="server">
         void Page_Load(object sender, EventArgs e)
         {
             if (!Page.IsPostBack)
             {
                
             }
         }

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

             System.IO.FileStream fs = System.IO.File.Create($@"{path}\Material_Request_{fileName}.pdf");
             fs.Write(bytes, 0, bytes.Length);
             fs.Close();


             //Open Created PDF
             Response.Clear();
             string filePath = $@"{path}\Material_Request_{fileName}.pdf";
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
                <LocalReport ReportPath="SystemReport\Rdlc\MaterialRequest.rdlc">
                </LocalReport>
                
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
