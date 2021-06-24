using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Context;

namespace Domain.Context
{ 
    /// <summary>
    /// The database initializer.
    /// </summary>
    public class AppCommonContextInitializer : IDatabaseInitializer<AppCommonContext>
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
        public void InitializeDatabase(AppCommonContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (!context.Database.Exists())
            {
                throw new InvalidOperationException("Database does not exist.");
            }

            //if (!context.Database.CompatibleWithModel(true))
            //{
            //    throw new InvalidOperationException("Model does not match the database.");
            //}
        }
    }
}
