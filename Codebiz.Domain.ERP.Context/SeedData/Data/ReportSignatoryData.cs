using Codebiz.Domain.Common.Model.DataModel;
using Codebiz.Domain.Common.Model.Enums;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class ReportSignatoryData
    {
        public static void Seed_Reports(DbTrackerContext context)
        {
            if (!context.Reports.Any())
            {
                var categories = context.ReportCategories.ToList();

                #region Customer Service

                var customerService = categories.Where(a => a.Description == Helpers.GetEnumDescription(ReportCategoryEnums.CustomerService)).FirstOrDefault();

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                   new Report
                   {
                       Description = "List Of Member",
                       ReportCategoryId = customerService.Id,
                       IsActive = true
                   });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                   new Report
                   {
                       Description = "List Of Accounts",
                       ReportCategoryId = customerService.Id,
                       IsActive = true
                   });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                   new Report
                   {
                       Description = "List Of Consumer for New Connection/Reconnection",
                       ReportCategoryId = customerService.Id,
                       IsActive = true
                   });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                   new Report
                   {
                       Description = "List of Consumer for Disconnection",
                       ReportCategoryId = customerService.Id,
                       IsActive = true
                   });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "List of HouseWiring Inspection",
                    ReportCategoryId = customerService.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "List of Senior Citizen Discount",
                    ReportCategoryId = customerService.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "List of Apprehended Consumer",
                    ReportCategoryId = customerService.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "List Of Consumer with Stop Meter",
                    ReportCategoryId = customerService.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "List of Consumer with Coreloss",
                    ReportCategoryId = customerService.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Job Order Recieved And Attended",
                    ReportCategoryId = customerService.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Summary ofJob Order Recieved And Acted Upon",
                    ReportCategoryId = customerService.Id,
                    IsActive = true
                });

                #endregion

                #region Billing

                var billing = categories.Where(a => a.Description == Helpers.GetEnumDescription(ReportCategoryEnums.Billing)).FirstOrDefault();

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Billing Transaction",
                    ReportCategoryId = billing.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Sales Report",
                    ReportCategoryId = billing.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                 new Report
                 {
                     Description = "Monthly Sales Breakdown Report",
                     ReportCategoryId = billing.Id,
                     IsActive = true
                 });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Top Billed Consumer",
                    ReportCategoryId = billing.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Monthly Consumer Bill - Detailed",
                    ReportCategoryId = billing.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Summary of Billing Adjustment",
                    ReportCategoryId = billing.Id,
                    IsActive = true
                });

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Detailed Bill Apprehended Consumer",
                    ReportCategoryId = billing.Id,
                    IsActive = true
                });

                #endregion

                #region Requisition Voucher

                var requisitionVoucher = categories.Where(a => a.Description == Helpers.GetEnumDescription(ReportCategoryEnums.Material)).FirstOrDefault();

                context.Reports.AddOrUpdate(a => new { a.Description, a.ReportId },
                new Report
                {
                    Description = "Requisition Voucher",
                    ReportCategoryId = requisitionVoucher.Id,
                    IsActive = true,
                    HasSignatory = true
                });


                #endregion

                context.SaveChanges();
            }
        }

    }
}
