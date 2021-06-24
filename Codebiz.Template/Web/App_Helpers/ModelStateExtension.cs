using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    public static class ModelStateExtension
    {
        public static List<KeyValuePair<string, string>> ToKeyValuePair(this ModelStateDictionary modelState)
        {
            var errorList = modelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            var errorKeyValuepair = new List<KeyValuePair<string, string>>();
            foreach (var item in errorList)
            {
                foreach (var error in item.Value)
                {
                    errorKeyValuepair.Add(new KeyValuePair<string, string>(item.Key, error));
                }
            }

            return errorKeyValuepair;
        }
    }
}