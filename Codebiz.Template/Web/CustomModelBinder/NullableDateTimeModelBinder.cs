using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class NullableDateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value == null || string.IsNullOrWhiteSpace(value.AttemptedValue))
            {
                return null;
            }

            DateTime dateTime;

            var isDate = DateTime.TryParse(value.AttemptedValue, Thread.CurrentThread.CurrentUICulture, DateTimeStyles.None, out dateTime);

            if (!isDate)
            {
                //bindingContext.ModelState.AddModelError(bindingContext.ModelName, ModelValidationResources.InvalidDateTime);
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid datetime");
                return DateTime.UtcNow;
            }

            return dateTime;
        }
    }
}