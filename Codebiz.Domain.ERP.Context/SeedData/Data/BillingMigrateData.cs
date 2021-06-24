using Codebiz.Domain.Common.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class BillingMigrateData
    {
        public static void Seed_BillingUnbundledTransactionCategory(DbTrackerContext context)
        {
            if (!context.UnbundledTransactionCategories.Any())
            {
                context.UnbundledTransactionCategories.AddOrUpdate(a => a.Code,
                    new BillingUnbundledTransactionCategory()
                    {
                        Code = "01",
                        Name = "Generation or Transmission",
                        Description = "GENERATION/TRANSMISSION",
                        IsActive = true,
                        CreatedByAppUserId = 1,
                        CreatedOn = DateTime.Now,
                        Ordinal = 1,
                        IsBill = true
                    });

                context.UnbundledTransactionCategories.AddOrUpdate(a => a.Code,
                    new BillingUnbundledTransactionCategory()
                    {
                        Code = "02",
                        Name = "Distribution Charges",
                        Description = "DISTRIBUTION CHARGES",
                        CreatedByAppUserId = 1,
                        IsActive = true,
                        CreatedOn = DateTime.Now,
                        Ordinal = 2,
                        IsBill = true
                    });

                context.UnbundledTransactionCategories.AddOrUpdate(a => a.Code,
                    new BillingUnbundledTransactionCategory()
                    {
                        Code = "03",
                        Name = "Universal Charges",
                        Description = "UNIVERSAL CHARGES",
                        IsActive = true,
                        CreatedByAppUserId = 1,
                        CreatedOn = DateTime.Now,
                        Ordinal = 3,
                        IsBill = true
                    });

                context.UnbundledTransactionCategories.AddOrUpdate(a => a.Code,
                    new BillingUnbundledTransactionCategory()
                    {
                        Code = "04",
                        Name = "Other Charges",
                        Description = "OTHER CHARGES",
                        IsActive = true,
                        CreatedByAppUserId = 1,
                        CreatedOn = DateTime.Now,
                        Ordinal = 4,
                        IsBill = true
                    });

                context.UnbundledTransactionCategories.AddOrUpdate(a => a.Code,
                    new BillingUnbundledTransactionCategory()
                    {
                        Code = "05",
                        Name = "Government Charges",
                        Description = "GOVERNMENT CHARGES",
                        IsActive = true,
                        CreatedByAppUserId = 1,
                        CreatedOn = DateTime.Now,
                        Ordinal = 5,
                        IsBill = false
                    });

                context.SaveChanges();
            }
        }
    }
}