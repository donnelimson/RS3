namespace Codebiz.Domain.ERP.Context.InventoryContextMigration
{
    using System.Data.Entity.Migrations;

    internal sealed class InventoryContextConfig : DbMigrationsConfiguration<InventoryContext>
    {
        public InventoryContextConfig()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"InventoryContextMigration";
        }

        protected override void Seed(InventoryContext context)
        {
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

    }

}
