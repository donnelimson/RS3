//using Codebiz.Domain.Common.Model;
//using Codebiz.Domain.Common.Model.Enums;
//using Infrastructure.Services;
//using Logging;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using Codebiz.Domain.Repository;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Web;

//namespace Web.Helpers
//{
//    public static class ReportHelper
//    {
//        private const string TEMPLATE_PATH = "ReportTemplateFile";
//        private const string DOWNLOAD_PATH = "Download";

//        public static string GenerateReport(HttpServerUtilityBase server, ReportData data, int runId, string fName = null)
//        {
//            // Copy Template
//            var sourcePath = "";
//            var destPath = "";
//            var fileName = String.IsNullOrWhiteSpace(fName) ? $"{data.Rule.Name.Replace(" ", "_")}_Run_OnDemand_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx" : $"{fName}.xlsx";
//            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + "#";
//            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
//            fileName = r.Replace(fileName, "");
//            if (data.Rule.RuleType.ToUpper().Equals("SINGLE"))
//            {
//                sourcePath = $"~\\{TEMPLATE_PATH}\\SingleTransactionsAnalysisReport.xlsx";
//                destPath = $"~\\{DOWNLOAD_PATH}\\{fileName}";
//            }
//            else if (data.Rule.RuleType.ToUpper().Equals("AGGREGATED"))
//            {
//                sourcePath = $"~\\{TEMPLATE_PATH}\\AggregatedTransactionsAnalysisReport.xlsx";
//                destPath = $"~\\{DOWNLOAD_PATH}\\{fileName}";
//            }

//            string source = server.MapPath(sourcePath);
//            string dest = server.MapPath(destPath);
//            try
//            {
//                File.Copy(source, dest, true);
//            }
//            catch (Exception ex)
//            {
//                return ex.Message;
//            }

//            // Prepare Data
//            var transactions = data.Data;
//            var transactionsSP = data.DataSP;

//            // Write to new Excel file
//            try
//            {
//                var fi = new FileInfo(dest);

//                using (var ep = new ExcelPackage(fi))
//                {
//                    var mySheet = ep.Workbook.Worksheets["Sheet1"];
//                    var rowOffset = 8;

//                    if (data.Rule.RuleType.ToUpper().Equals("SINGLE"))
//                    {
//                        // Heading
//                        mySheet.Cells[3, 1].Value = $"Rule : {data.Rule.Name}";
//                        mySheet.Cells[4, 1].Value = $"Category : {data.Type}";
//                        mySheet.Cells[5, 1].Value = $"Trigger : {data.Trigger}";
//                        mySheet.Cells[4, 6].Value = $"Transaction Date : {data.TransactionDate}";
//                        mySheet.Cells[5, 6].Value = $"Print Date and Time : {data.PrintDateTime}";

//                        for (var x = 0; x < transactions.Count; x++)
//                        {
//                            mySheet.Cells[rowOffset + x, 1].Value = transactions[x].Member.LastName + ", " + transactions[x].Member.FirstName;
//                            mySheet.Cells[rowOffset + x, 2].Value = transactions[x].Member.MemberNo;
//                            mySheet.Cells[rowOffset + x, 3].Value = transactions[x].Account.AccountNo;
//                            mySheet.Cells[rowOffset + x, 4].Value = transactions[x].TransactionDate.ToString("MM/dd/yyyy");
//                            mySheet.Cells[rowOffset + x, 5].Value = transactions[x].TraceNo;
//                            mySheet.Cells[rowOffset + x, 6].Value = transactions[x].TransactionType.Description;
//                            mySheet.Cells[rowOffset + x, 7].Value = transactions[x].TransactionType.TransactionCategory.Name;
//                            mySheet.Cells[rowOffset + x, 8].Value = transactions[x].TransactionAmount;
//                        }
//                    }
//                    else if (data.Rule.RuleType.ToUpper().Equals("AGGREGATED"))
//                    {
//                        // Heading
//                        mySheet.Cells[3, 1].Value = $"Rule : {data.Rule.Name}";
//                        mySheet.Cells[4, 1].Value = $"Category : {data.Type}";
//                        mySheet.Cells[5, 1].Value = $"Trigger : {data.Trigger}";
//                        mySheet.Cells[4, 7].Value = $"Transaction Date : {data.TransactionDate}";
//                        mySheet.Cells[5, 7].Value = $"Print Date and Time : {data.PrintDateTime}";

//                        // Percentage
//                        if (data.Rule.ValueType.ToUpper().Equals(ValueTypeDatas.Percentage.ToUpper()) && data.Rule.RuleType.ToUpper() == RuleTypeDatas.Aggregated.ToUpper())
//                        {
//                            var groups = data.DataSP
//                                .GroupBy(x => x.MemberId)
//                                .Select(y => new
//                                {
//                                    GroupId = y.Key,
//                                    TotalAmount = y.Sum(z => z.TransactionAmount)
//                                })
//                                .ToList();

//                            foreach (var group in groups)
//                            {
//                                var groupRowStart = rowOffset;

//                                // Top Border
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 1, groupRowStart, 9])
//                                {
//                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                                }

//                                // Member
//                                var member = transactionsSP.First(x => x.MemberId.Equals(group.GroupId));
//                                mySheet.Cells[groupRowStart, 1].Value = member.MemberName;

//                                // Member No.
//                                mySheet.Cells[groupRowStart, 2].Value = member.MemberNo;

//                                // Total Amount
//                                mySheet.Cells[groupRowStart, 9].Value = group.TotalAmount;

//                                var aggregated = data.DataSP
//                                    .Where(x => x.MemberId.Equals(group.GroupId))
//                                    .ToList();

//                                for (var x = 0; x < aggregated.Count; x++)
//                                {
//                                    mySheet.Cells[groupRowStart + x, 3].Value = aggregated[x].AccountNo;
//                                    mySheet.Cells[groupRowStart + x, 4].Value = aggregated[x].TransactionDate.ToString("MM/dd/yyyy");
//                                    mySheet.Cells[groupRowStart + x, 5].Value = aggregated[x].TraceNo;
//                                    mySheet.Cells[groupRowStart + x, 6].Value = aggregated[x].TransactionTypeDescription;
//                                    mySheet.Cells[groupRowStart + x, 7].Value = aggregated[x].TransactionCategoryName;
//                                    mySheet.Cells[groupRowStart + x, 8].Value = aggregated[x].TransactionAmount;
//                                    rowOffset = groupRowStart + x + 1;
//                                }

//                                // Merge Member Column
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 1, rowOffset - 1, 1])
//                                {
//                                    range.Merge = true;
//                                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//                                }

//                                // Merge Member No. Column
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 2, rowOffset - 1, 2])
//                                {
//                                    range.Merge = true;
//                                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//                                }

//                                // Merge Total Amount Column
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 9, rowOffset - 1, 9])
//                                {
//                                    range.Merge = true;
//                                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//                                }

//                                // Vertical Borders
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 1, rowOffset - 1, 9])
//                                {
//                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
//                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
//                                }

//                                // Bottom Border                    
//                                using (ExcelRange range = mySheet.Cells[rowOffset - 1, 1, rowOffset - 1, 9])
//                                {
//                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                                }
//                            }
//                        }
//                        // Amount
//                        else
//                        {
//                            var groups = data.Data
//                                .GroupBy(x => x.MemberId)
//                                .Select(y => new
//                                {
//                                    GroupId = y.Key,
//                                    TotalAmount = y.Sum(z => z.TransactionAmount)
//                                })
//                                .ToList();

//                            foreach (var group in groups)
//                            {
//                                var groupRowStart = rowOffset;

//                                // Top Border
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 1, groupRowStart, 9])
//                                {
//                                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                                }

//                                // Member
//                                var member = transactions.First(x => x.MemberId.Equals(group.GroupId)).Member;
//                                mySheet.Cells[groupRowStart, 1].Value = member.LastName + ", " + member.FirstName;

//                                // Member No.
//                                mySheet.Cells[groupRowStart, 2].Value = member.MemberNo;

//                                // Total Amount
//                                mySheet.Cells[groupRowStart, 9].Value = group.TotalAmount;

//                                var aggregated = data.Data
//                                    .Where(x => x.MemberId.Equals(group.GroupId))
//                                    .ToList();

//                                for (var x = 0; x < aggregated.Count; x++)
//                                {
//                                    mySheet.Cells[groupRowStart + x, 3].Value = aggregated[x].Account.AccountNo;
//                                    mySheet.Cells[groupRowStart + x, 4].Value = aggregated[x].TransactionDate.ToString("MM/dd/yyyy");
//                                    mySheet.Cells[groupRowStart + x, 5].Value = aggregated[x].TraceNo;
//                                    mySheet.Cells[groupRowStart + x, 6].Value = aggregated[x].TransactionType.Description;
//                                    mySheet.Cells[groupRowStart + x, 7].Value = aggregated[x].TransactionType.TransactionCategory.Name;
//                                    mySheet.Cells[groupRowStart + x, 8].Value = aggregated[x].TransactionAmount;
//                                    rowOffset = groupRowStart + x + 1;
//                                }

//                                // Merge Member Column
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 1, rowOffset - 1, 1])
//                                {
//                                    range.Merge = true;
//                                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//                                }

//                                // Merge Member No. Column
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 2, rowOffset - 1, 2])
//                                {
//                                    range.Merge = true;
//                                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//                                }

//                                // Merge Total Amount Column
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 9, rowOffset - 1, 9])
//                                {
//                                    range.Merge = true;
//                                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//                                }

//                                // Vertical Borders
//                                using (ExcelRange range = mySheet.Cells[groupRowStart, 1, rowOffset - 1, 9])
//                                {
//                                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
//                                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
//                                }

//                                // Bottom Border                    
//                                using (ExcelRange range = mySheet.Cells[rowOffset - 1, 1, rowOffset - 1, 9])
//                                {
//                                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                                }
//                            }
//                        }
//                    }

//                    // Auto-Size
//                    //mySheet.Cells[mySheet.Dimension.Address].AutoFitColumns();

//                    Logger.Info("Finished writing date to Excel.");

//                    ep.Save();
//                    Logger.Info("Excel save.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Error(ex.Message, ex);
//                return ex.Message;
//            }

//            return fileName;
//        }

//        public static string GenerateReportFromTemplate(HttpServerUtilityBase server, ReportTemplate reportTemplate, List<ReportTuple> data, int runId, string fName = null)
//        {
//            // Sanitize File Name
//            var fileName = $"{fName}.xlsx";
//            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + "#";
//            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
//            fileName = r.Replace(fileName, "");

//            var destPath = $"~\\{DOWNLOAD_PATH}\\{fileName}";
//            string dest = server.MapPath(destPath);

//            // Write to new Excel file
//            try
//            {
//                var fi = new FileInfo(dest);

//                using (var ep = new ExcelPackage())
//                {
//                    var fields = reportTemplate.ReportTemplateFields
//                        .OrderBy(f => f.Ordinal)
//                        .ToList();

//                    var mySheet = ep.Workbook.Worksheets.Add("Non comp w AMLC reporting proce");

//                    // Create Headers
//                    var headerRow = 0;
//                    if (!String.IsNullOrWhiteSpace(reportTemplate.Row1))
//                    {
//                        for (int i = 0; i < fields.Count; i++)
//                        {
//                            mySheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                            mySheet.Cells[1, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

//                            if (reportTemplate.Row1.ToUpper().Equals("NAME"))
//                            {
//                                mySheet.Row(1).Height = 60;
//                                mySheet.Row(1).Style.WrapText = true;
//                            }

//                            if (!reportTemplate.Row1.ToUpper().Equals("CODE"))
//                            {
//                                mySheet.Cells[1, i + 1].Style.Font.Size = 8;
//                            }

//                            mySheet.Cells[1, i + 1].Value = GetHeaderValue(fields[i].ReportField, reportTemplate.Row1);
//                        }

//                        headerRow = 1;

//                        // Row 2
//                        if (!String.IsNullOrWhiteSpace(reportTemplate.Row2))
//                        {
//                            for (int i = 0; i < fields.Count; i++)
//                            {
//                                mySheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                                mySheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

//                                if (reportTemplate.Row2.ToUpper().Equals("NAME"))
//                                {
//                                    mySheet.Row(2).Height = 60;
//                                    mySheet.Row(2).Style.WrapText = true;
//                                }

//                                if (!reportTemplate.Row2.ToUpper().Equals("CODE"))
//                                {
//                                    mySheet.Cells[2, i + 1].Style.Font.Size = 8;
//                                }

//                                mySheet.Cells[2, i + 1].Value = GetHeaderValue(fields[i].ReportField, reportTemplate.Row2);
//                            }

//                            headerRow = 2;

//                            // Row 3
//                            if (!String.IsNullOrWhiteSpace(reportTemplate.Row3))
//                            {
//                                for (int i = 0; i < fields.Count; i++)
//                                {
//                                    mySheet.Cells[3, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                                    mySheet.Cells[3, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

//                                    if (reportTemplate.Row3.ToUpper().Equals("NAME"))
//                                    {
//                                        mySheet.Row(3).Height = 60;
//                                        mySheet.Row(3).Style.WrapText = true;
//                                    }

//                                    if (!reportTemplate.Row3.ToUpper().Equals("CODE"))
//                                    {
//                                        mySheet.Cells[3, i + 1].Style.Font.Size = 8;
//                                    }

//                                    mySheet.Cells[3, i + 1].Value = GetHeaderValue(fields[i].ReportField, reportTemplate.Row3);
//                                }

//                                headerRow = 3;

//                                // Row 4
//                                if (!String.IsNullOrWhiteSpace(reportTemplate.Row4))
//                                {
//                                    for (int i = 0; i < fields.Count; i++)
//                                    {
//                                        mySheet.Cells[4, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                                        mySheet.Cells[4, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

//                                        if (reportTemplate.Row4.ToUpper().Equals("NAME"))
//                                        {
//                                            mySheet.Row(4).Height = 60;
//                                            mySheet.Row(4).Style.WrapText = true;
//                                        }

//                                        if (!reportTemplate.Row4.ToUpper().Equals("CODE"))
//                                        {
//                                            mySheet.Cells[4, i + 1].Style.Font.Size = 8;
//                                        }

//                                        mySheet.Cells[4, i + 1].Value = GetHeaderValue(fields[i].ReportField, reportTemplate.Row4);
//                                    }

//                                    headerRow = 4;
//                                }
//                            }
//                        }
//                    }

//                    if (headerRow > 0)
//                    {
//                        // Border
//                        using (ExcelRange range = mySheet.Cells[1, 1, headerRow, fields.Count])
//                        {
//                            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
//                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
//                        }

//                        // Create Filter
//                        mySheet.Cells[$"A{headerRow}:{GetColumnName(fields.Count - 1)}{headerRow}"].AutoFilter = true;
//                    }

//                    // Write Data Rows
//                    var rowOffset = headerRow + 1;
//                    var customHeaderUsed = (!String.IsNullOrWhiteSpace(reportTemplate.Row1) && reportTemplate.Row1.ToUpper().Equals("HEADER"))
//                        || (!String.IsNullOrWhiteSpace(reportTemplate.Row2) && reportTemplate.Row2.ToUpper().Equals("HEADER"))
//                        || (!String.IsNullOrWhiteSpace(reportTemplate.Row3) && reportTemplate.Row3.ToUpper().Equals("HEADER"))
//                        || (!String.IsNullOrWhiteSpace(reportTemplate.Row4) && reportTemplate.Row4.ToUpper().Equals("HEADER"));

//                    if (customHeaderUsed)
//                    {
//                        for (var y = 0; y < data.Count; y++)
//                        {
//                            foreach (var field in fields)
//                            {
//                                if (!String.IsNullOrEmpty(field.ReportField.Value))
//                                {
//                                    mySheet.Cells[rowOffset + y, field.Ordinal].Value = field.ReportField.Value;
//                                }
//                                else
//                                {
//                                    var val = typeof(ReportTuple).GetProperty(field.ReportField.UID).GetValue(data[y]);

//                                    if (field.ReportField.IsDateField)
//                                    {
//                                        try
//                                        {
//                                            DateTime date = DateTime.Parse(val?.ToString());
//                                            mySheet.Cells[rowOffset + y, field.Ordinal].Value = date.ToString(field.DateFormat);
//                                        }
//                                        catch (Exception)
//                                        {
//                                            // Value is not on date format
//                                        }
//                                    }
//                                    else
//                                    {
//                                        mySheet.Cells[rowOffset + y, field.Ordinal].Value = val?.ToString();
//                                    }
//                                }
//                            }
//                        }
//                    }
//                    else
//                    {
//                        for (var y = 0; y < data.Count; y++)
//                        {
//                            foreach (var field in fields)
//                            {
//                                var val = typeof(ReportTuple).GetProperty(field.ReportField.UID).GetValue(data[y]);

//                                if (field.ReportField.IsDateField)
//                                {
//                                    try
//                                    {
//                                        DateTime date = DateTime.Parse(val?.ToString());
//                                        mySheet.Cells[rowOffset + y, field.Ordinal].Value = date.ToString(field.DateFormat);
//                                    }
//                                    catch (Exception)
//                                    {
//                                        // Value is not on date format
//                                    }
//                                }
//                                else
//                                {
//                                    mySheet.Cells[rowOffset + y, field.Ordinal].Value = val?.ToString();
//                                }
//                            }
//                        }
//                    }

//                    // Footer
//                    var footerRow = rowOffset + data.Count;
//                    mySheet.Cells[footerRow, 1].Value = "T";
//                    mySheet.Cells[footerRow, 2].Value = data.Sum(x => x.D_7).ToString("#,#.00");
//                    mySheet.Cells[footerRow, 3].Value = data.Count;

//                    // Auto-Size
//                    mySheet.Cells[mySheet.Dimension.Address].AutoFitColumns();

//                    Logger.Info("Finished writing date to Excel.");

//                    mySheet.Protection.IsProtected = false;
//                    mySheet.Protection.AllowSelectLockedCells = false;
//                    ep.SaveAs(fi);
//                    Logger.Info("Excel save.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Error(ex.Message, ex);
//                return ex.Message;
//            }

//            return fileName;
//        }

//        private static string GetColumnName(int index)
//        {
//            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
//            var value = "";

//            if (index >= letters.Length)
//            {
//                value += letters[index / letters.Length - 1];
//            }

//            value += letters[index % letters.Length];

//            return value;
//        }

//        private static string GetHeaderValue(ReportField reportField, string header)
//        {
//            return reportField.GetType().GetProperty(header).GetValue(reportField, null)?.ToString();
//        }
//    }
//}