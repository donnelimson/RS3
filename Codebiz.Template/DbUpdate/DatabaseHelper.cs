using Domain.Context;
using System;
using System.Data.Entity.Infrastructure;

namespace DbUpdate
{
    public class DatabaseHelper
    {
        public static void Initialize()
        {
            // Update Configuration
            var config = new ConfigHelper();
            config.Start();

            System.Data.Entity.Database.SetInitializer(new ContextDbInitializer());
            var context = new AppCommonContext();

            // Use Connection String from Config
            context.Database.Connection.ConnectionString = config.ConnectionString;

            if (!context.Database.Exists())
            {
                Console.WriteLine("\nDatabase does not exist.");
                Console.Write("This will create a new database. Press Y to continue [Y/N] ");

                ConsoleKey response;
                do
                {
                    response = Console.ReadKey(false).Key;
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);

                if (response == ConsoleKey.Y)
                {
                    DatabaseHelper.UpdateDatabase(context.Database.Connection.ConnectionString);

                    if (context.Database.Exists())
                    {
                        Console.WriteLine("\nThe database was successfully created.");
                    }
                    else
                    {
                        throw new Exception("Database not created. Please contact your system administrator!");
                    }
                }
            }
            else
            {
                if (context.Database.CompatibleWithModel(true))
                {
                    Console.WriteLine("\n\nThe database is up-to-date.");
                }
                else
                {
                    Console.WriteLine("\n\nThis will update the PSSLAI-AML database. Press Y to continue [Y/N] ");

                    ConsoleKey response;
                    do
                    {
                        response = Console.ReadKey(false).Key;
                    } while (response != ConsoleKey.Y && response != ConsoleKey.N);

                    if (response == ConsoleKey.Y)
                    {
                        DatabaseHelper.UpdateDatabase(context.Database.Connection.ConnectionString);

                        if (context.Database.CompatibleWithModel(true))
                        {
                            Console.WriteLine("\nThe database was successfully updated.");
                        }
                        else
                        {
                            throw new Exception("Database not updated. Please contact your system administrator!");
                        }
                    }
                }
            }
        }

        private static void UpdateDatabase(string connectionString)
        {
            Console.Write("\n\nProcessing...");
            try
            {
                var configuration = new Domain.Context.Migrations.Configuration();
                configuration.AutomaticMigrationDataLossAllowed = true;
                configuration.TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient");
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
