namespace Codebiz.Domain.ERP.Context.migrations.FinanceContext
{
    using Codebiz.Domain.ERP.Model.Data.Financials;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Codebiz.Domain.ERP.Context.FinanceDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"migrations\FinanceContext";
        }

        protected override void Seed(Codebiz.Domain.ERP.Context.FinanceDataContext context)
        {

            ExecuteCustomSQL(context);
            SeedVatCodes(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        void SeedVatCodes(FinanceDataContext ctx)
        {
            ctx.TaxGroups.AddOrUpdate(
                new TaxGroup() { VatCode = "EI", VatName = "Exempted 0%", Rate = 0, EffecDate = DateTime.Parse("1990-01-01"), Category = "I", Account = "143030" },
                new TaxGroup() { VatCode = "EO", VatName = "Exempted 0%", Rate = 0, EffecDate = DateTime.Parse("1990-01-01"), Category = "O", Account = "207000" },
                new TaxGroup() { VatCode = "I1", VatName = "Input Tax", Rate = 0, EffecDate = DateTime.Parse("1990-01-01"), Category = "I", Account = "143030" },
                new TaxGroup() { VatCode = "NI", VatName = "Out of Scope 0%", Rate = 0, EffecDate = DateTime.Parse("1990-01-01"), Category = "I", Account = "143030" },
                new TaxGroup() { VatCode = "NO", VatName = "Out of Scope 0%", Rate = 0, EffecDate = DateTime.Parse("1990-01-01"), Category = "O", Account = "207000" },
                new TaxGroup() { VatCode = "O1", VatName = "Output Tax", Rate = 0, EffecDate = DateTime.Parse("1990-01-01"), Category = "O", Account = "207000" },
                new TaxGroup() { VatCode = "SI", VatName = "Standard Rated", Rate = 12, EffecDate = DateTime.Parse("2004-01-01"), Category = "I", Account = "143030" },
                new TaxGroup() { VatCode = "SO", VatName = "Standard Rated", Rate = 12, EffecDate = DateTime.Parse("2004-01-01"), Category = "O", Account = "207000" },
                new TaxGroup() { VatCode = "ZI", VatName = "Zero Rated 0%", Rate = 0, EffecDate = DateTime.Parse("1990-01-01"), Category = "I", Account = "143030" },
                new TaxGroup() { VatCode = "ZO", VatName = "Zero Rated 0%", Rate = 0, EffecDate = DateTime.Parse("1990-01-01"), Category = "O", Account = "207000" });
            ctx.SaveChanges();
        }

        void ExecuteCustomSQL(FinanceDataContext ctx)
        {
            ctx.Database.ExecuteSqlCommand(
                @"
ALTER TABLE GLAccount ADD CONSTRAINT DF_GLAccount_Active DEFAULT N'Y' FOR Active;

");
        }
    }
}
