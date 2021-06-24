using Codebiz.Domain.ERP.Context;
using System;
using System.Data.Entity;

namespace Domain.Context
{
    /// <summary>
    /// The database initializer.
    /// </summary>
    public class DbTrackerContextInitializer : IDatabaseInitializer<DbTrackerContext>
    {
        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        /// <exception cref="System.InvalidOperationException">
        /// Database does not exist.
        /// or
        /// Model does not match the database.
        /// </exception>
        public void InitializeDatabase(DbTrackerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (!context.Database.Exists())
            {
                throw new InvalidOperationException("Database does not exist.");
            }

            if (!context.Database.CompatibleWithModel(true))
            {
                throw new InvalidOperationException("Model does not match the database.");
            }
        }
    }
}
