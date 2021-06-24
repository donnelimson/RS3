using Atlas;
using Autofac;
using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using Domain.Context;

namespace BackgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseHelper.Initialize();

            //var configuration = Host.Configure<ImportService>()
            //                        .Named("Service",
            //                                displayName: "AML Background Worker",
            //                                description: "AML Background Worker")
            //                        .AllowMultipleInstances()
            //                        .WithRegistrations(b => b.RegisterModule(new AutofacModule()))
            //                        .BeforeStart(() => Logger.Configure("DebugLogger", "InfoLogger", "ErrorLogger", "DatabaseLogger"))
            //                        .WithArguments(args); // creates configuration with defaults

            //// then just start the configuration and away you go
            //Host.Start(configuration);
        }
    }

    public static class DatabaseHelper
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {

            Database.SetInitializer(new AppCommonContextInitializer());
            var context = new AppCommonContext();
            if (!context.Database.CompatibleWithModel(true))
            {
                Logger.Info("Updating Database...");

                try
                {
                    global::Domain.Context.Migrations.Configuration configuration = new global::Domain.Context.Migrations.Configuration();
                    var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
                    migrator.Update();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
                    throw new Exception(string.Format("Error encountered {0}. Please contact your system administrator!", ex.Message));
                }
            }

            if (!context.Database.CompatibleWithModel(true))
            {
                Logger.Error("Database is not updated...");
                throw new Exception("Database not updated.  Please contact your system administrator!");
            }
        }
    }
}
