using Domain.Context;
using System;
using System.Data.Entity;

namespace DbUpdate
{
    public class ContextDbInitializer : IDatabaseInitializer<AppCommonContext>
    {
        public void InitializeDatabase(AppCommonContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
        }
    }
}
