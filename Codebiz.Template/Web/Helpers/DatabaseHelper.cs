using Codebiz.Domain.ERP.Context;
using Domain.Context;
using System;
using System.Data.Entity;

namespace Web.Helpers
{
    public static class DatabaseHelper
    {
        public static void DbTrackerContextInitialize()
        {
            Database.SetInitializer(new DbTrackerContextInitializer());
            var context = new DbTrackerContext();

            if (!context.Database.Exists())
            {
                DatabaseHelper.UpdateDatabase();
            }
            else
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    DatabaseHelper.UpdateDatabase();
                }

                if (!context.Database.CompatibleWithModel(true))
                {
                    throw new Exception("Database not updated.  Please contact your system administrator!");
                }
            }
        }

        private static void UpdateDatabase()
        {
            try
            {
                var configuration = new Codebiz.Domain.ERP.Context.DbTrackerMigrations.DbTrackerConfiguration();
                var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
                migrator.Update();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error encountered {0}. Please contact your system administrator!", ex.Message));
            }
        }
    }
}