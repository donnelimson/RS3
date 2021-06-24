using Codebiz.Domain.Common.Model;
using Codebiz.Domain.ERP.Model.Data;
using Logging;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Infrastructure.Utilities
{
    public static class ExportToExcelFileHelper
    {
        public static string GenerateExcelFile<T>(object classDTO, List<T> data, string moduleNameDate, HttpServerUtilityBase server, string headerName, int currentAppUserId, string currentOffice = "")
        {
            
            var excelFileModel = new ExcelFileModel();
            var properties = classDTO.GetType().GetProperties()
                .Where(p => ((DisplayNameAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayNameAttribute))) != null)
                .OrderBy(p => ((ParameterOrderAttribute)Attribute.GetCustomAttribute(p, typeof(ParameterOrderAttribute))) == null ? 0
                              : ((ParameterOrderAttribute)Attribute.GetCustomAttribute(p, typeof(ParameterOrderAttribute))).Order);

            var propertyDisplayNames = properties.Select(p => ((DisplayNameAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayNameAttribute))).DisplayName).ToArray();
            var propertyNames = properties.Select(p => p.Name).ToArray();
            var propertyTypes = properties.Select(p => p.PropertyType).ToArray();
            var propertyDatas = properties.Select(p => new { 
                p.Name,
                DisplayName = ((DisplayNameAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayNameAttribute))) != null 
                    ? ((DisplayNameAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayNameAttribute))).DisplayName
                    : "",
                Width = ((WidthAttribute)Attribute.GetCustomAttribute(p, typeof(WidthAttribute))) == null ? (int?)null
                        : ((WidthAttribute)Attribute.GetCustomAttribute(p, typeof(WidthAttribute))).Width
            }).ToArray();
            var booleanColumns = new List<string>();
            var intDoubleColumns = new List<string>();
            var stringColumns = new List<string>();
            var dateTimeColumns = new List<string>();

            excelFileModel.Headers.AddRange(propertyDisplayNames);

            for (int i = 0; i < propertyDisplayNames.Length; i++)
            {
                excelFileModel.ColumnValues.Add(new List<string>());

                if (propertyTypes[i] == typeof(bool))
                {
                    booleanColumns.Add((NumberToString(i + 1, true)));
                }
                else if (propertyTypes[i] == typeof(int) || propertyTypes[i] == typeof(double) ||
                    propertyTypes[i] == typeof(int?) || propertyTypes[i] == typeof(double?))
                {
                    intDoubleColumns.Add((NumberToString(i + 1, true)));
                }
                else if (propertyTypes[i] == typeof(string))
                {
                    stringColumns.Add((NumberToString(i + 1, true)));
                }
                else if (propertyTypes[i] == typeof(DateTime))
                {
                    dateTimeColumns.Add((NumberToString(i + 1, true)));
                }

                for (int j = 0; j < data.Count; j++)
                {
                    var dataValue = data[j].GetType().GetProperty(propertyNames[i]).GetValue(data[j], null);

                    if (dataValue != null)
                    {
                        if (dataValue is DateTime || dataValue is DateTime?)
                        {
                            dataValue = ((DateTime)dataValue).ToString("MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                        }
                        else if (dataValue is bool)
                        {
                            if (propertyDisplayNames[i] == "E-MAIL CONFIRMATION")
                            {
                                dataValue = (bool)dataValue ? "Confirmed" : "For Confirmation";
                            }
                            else
                            {
                                dataValue = (bool)dataValue ? "Yes" : "No";
                            }
                        }
                        else
                        {
                            if (dataValue.ToString().Replace(" ", "").Replace(",", "") == "")
                            {
                                dataValue = "";
                            }
                        }
                    }
                    else
                    {
                        dataValue = "";
                    }

                    excelFileModel.ColumnValues[i].Add(dataValue.ToString());
                }
            }

            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Sheet1");
                var worksheet = excel.Workbook.Worksheets["Sheet1"];

                for (int i = 0; i < excelFileModel.Headers.Count; i++)
                {
                    worksheet.Cells[10, i + 1].Value = excelFileModel.Headers[i];

                    for (int j = 0; j < excelFileModel.ColumnValues[i].Count; j++)
                    {
                        worksheet.Cells[j + 11, i + 1].Value = excelFileModel.ColumnValues[i][j];
                    }
                }

                var rows = (data.Count + 10).ToString();
                var numberToString = NumberToString(excelFileModel.Headers.Count, true);

                //TARELCO 1 Header
                using (ExcelRange range = worksheet.Cells["A1:" + numberToString + "1"])
                {
                    range.Merge = true;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Color.SetColor(Color.White);
                }

                using (ExcelRange range = worksheet.Cells["A2:" + numberToString + "5"])
                {
                    range.Value = "TARLAC I ELECTRIC COOPERATIVE" + Environment.NewLine
                        + "(TARELCO I)" + Environment.NewLine
                        + currentOffice;
                    range.Merge = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Font.SetFromFont(new Font("Tahoma", 12, FontStyle.Bold));
                    range.Style.WrapText = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Color.SetColor(Color.White);
                }

                using (ExcelRange range = worksheet.Cells["A6:" + numberToString + "6"])
                {
                    range.Merge = true;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Color.SetColor(Color.White);
                }

                //List title e.g. APPLICANT LIST
                using (ExcelRange range = worksheet.Cells["A7:" + numberToString + "7"])
                {
                    range.Value = headerName.ToUpper();
                    range.Merge = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    range.Style.Font.SetFromFont(new Font("Tahoma", 11, FontStyle.Bold));
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 11;
                }

                //Date generated
                using (ExcelRange range = worksheet.Cells["A8:" + numberToString + "8"])
                {
                    range.Value = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    range.Merge = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    range.Style.Font.SetFromFont(new Font("Tahoma", 10));
                    range.Style.Font.Size = 11;
                    range.Style.WrapText = true;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                }

                using (ExcelRange range = worksheet.Cells["A9:" + numberToString + "9"])
                {
                    range.Merge = true;
                }

                //View List header/column names
                using (ExcelRange range = worksheet.Cells["A10:" + numberToString + "10"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 11;
                    range.Style.Font.Color.SetColor(Color.FromArgb(51, 122, 183));
                }

                //For boolean columns (Yes or No values)
                foreach (var column in booleanColumns)
                {
                    using (ExcelRange range = worksheet.Cells[column + "10:" + column + rows])
                    {
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        var cell = worksheet.Cells[column + (i + 11).ToString()];
                        var cellValueToString = cell.Value.ToString();

                        if (cellValueToString == "Yes" || cellValueToString == "Confirmed")
                        {
                            cell.Style.Font.Color.SetColor(Color.Green);
                        }
                        else
                        {
                            cell.Style.Font.Color.SetColor(Color.Red);
                        }
                    }
                }

                foreach(var column in stringColumns)
                {
                    using (ExcelRange range = worksheet.Cells[column + "11:" + column + rows])
                    {
                        range.Style.WrapText = true;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    }          
                }

                foreach (var column in dateTimeColumns)
                {
                    using (ExcelRange range = worksheet.Cells[column + "11:" + column + rows])
                    {
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    }
                }

                //For integer/double columns
                foreach (var column in intDoubleColumns)
                {
                    using (ExcelRange range = worksheet.Cells[column + "11:" + column + rows])
                    {
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                }

                //Assign borders
                using (ExcelRange range = worksheet.Cells["A10:" + numberToString + rows])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                var totalWidth = 0.00d;

                for (int i = 0; i < propertyDatas.Length; i++)
                {
                    if (propertyDatas[i].Width != null)
                    {
                        worksheet.Column(i + 1).Width = propertyDatas[i].Width.Value;
                    }
                    else{
                        if (propertyDatas[i].Name.ToUpper().Contains("MEMO") ||
                            propertyDatas[i].Name.ToUpper().Contains("DETAILS") ||
                            propertyDatas[i].Name.ToUpper().Contains("COMPLAINT") ||
                            propertyDatas[i].Name.ToUpper().Contains("REQUEST") ||
                            propertyDatas[i].Name.ToUpper().Contains("JOBORDER") ||
                            propertyDatas[i].Name.ToUpper().Contains("REMARKS") ||
                            propertyDatas[i].Name.ToUpper().Contains("ADDRESS") ||
                            propertyDatas[i].Name.ToUpper().Contains("CHANGETO") ||
                            propertyDatas[i].Name.ToUpper().Contains("CHANGEFROM"))
                        {
                            worksheet.Column(i + 1).Width = 60;
                        }
                        else if (propertyDatas[i].Name.ToUpper().Contains("ACCOUNT") ||
                                 propertyDatas[i].Name.ToUpper().Contains("STATUS") ||
                                 propertyDatas[i].Name.ToUpper().Contains("NAME") ||
                                 propertyDatas[i].Name.ToUpper().Contains("CREATEDBY") ||
                                 propertyDatas[i].Name.ToUpper().Contains("PROCESSEDBY") ||
                                 propertyDatas[i].Name.ToUpper().Contains("ACTIONBY") ||
                                 propertyDatas[i].Name.ToUpper().Contains("PHASE"))
                        {
                            worksheet.Column(i + 1).Width += 15;
                        }
                        else
                        {
                            worksheet.Column(i + 1).Width += 5;
                        }
                    }
                    
                    worksheet.Column(i + 1).Style.WrapText = true;
                    worksheet.Column(i + 1).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                    totalWidth += worksheet.Column(i + 1).Width;
                }

                using (ExcelRange range = worksheet.Cells["A1:" + numberToString + "5"])
                {
                    var totalWidthInPixels = GetWidthInPixels(totalWidth);
                    var titleWith = GetTextWidthInPixels("TARLAC I ELECTRIC COOPERATIVE");
                    var logo = new FileInfo(server.MapPath("~/Content/Image/tarelco_small_logo.png"));
                    var picture = worksheet.Drawings.AddPicture("tarelco_small_logo", logo);
                    picture.SetPosition(5, (totalWidthInPixels/2) - (titleWith/2) - 100);
                }

                var excelFile = new FileInfo(server.MapPath("~/Download/" + moduleNameDate + ".xlsx"));
                excel.SaveAs(excelFile);

                Logger.Info($"Successfully exported data to excel file. " +
                            $"List : [{headerName}]. " +
                            $"File name: [{moduleNameDate + ".xlsx"}] " +
                            $"UserId : [{currentAppUserId}]", LogEventTitles.ExportedDataToExcelFile);
            }

            return moduleNameDate + ".xlsx";
        }

        public static object CreateObjectBy(Type classDTO)
        {
            object theObject = Activator.CreateInstance(classDTO);
            return theObject;
        }

        private static string NumberToString(int number, bool isCaps)
        {
            var c = (char)((isCaps ? 65 : 97) + (number - 1));

            return c.ToString();
        }

        private static int GetWidthInPixels(double totalWidth)
        {
            Font font = new Font("Tahoma", 10);
            double pxBaseline = Math.Round(MeasureString("1234567890", font) / 10);
            return (int)(totalWidth * pxBaseline);
        }

        private static int GetTextWidthInPixels(string text)
        {
            Font font = new Font("Tahoma", 13, FontStyle.Bold);
            double pxBaseline = Math.Round(MeasureString(text, font));
            return (int)(pxBaseline);
        }

        private static float MeasureString(string s, Font font)
        {
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                return g.MeasureString(s, font, int.MaxValue, StringFormat.GenericTypographic).Width;
            }
        }
    }

    public class ExcelFileModel
    {
        public ExcelFileModel()
        {
            Headers = new List<string>();
            ColumnValues = new List<List<string>>();
        }

        public List<string> Headers { get; set; }
        public List<List<string>> ColumnValues { get; set; }
    }
}