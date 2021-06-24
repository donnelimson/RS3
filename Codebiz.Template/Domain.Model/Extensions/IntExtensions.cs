using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public static class IntExtensions
    {
        public static string NumberToText(this int intVal)
        {
            if (intVal < 0)
                return "Minus " + NumberToText(-intVal);
            else if (intVal == 0)
                return "";
            else if (intVal <= 19)
                return new string[] {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight",
         "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
         "Seventeen", "Eighteen", "Nineteen"}[intVal - 1] + " ";
            else if (intVal <= 99)
                return new string[] {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy",
         "Eighty", "Ninety"}[intVal / 10 - 2] + " " + NumberToText(intVal % 10);
            else if (intVal <= 199)
                return "One Hundred " + NumberToText(intVal % 100);
            else if (intVal <= 999)
                return NumberToText(intVal / 100) + "Hundreds " + NumberToText(intVal % 100);
            else if (intVal <= 1999)
                return "One Thousand " + NumberToText(intVal % 1000);
            else if (intVal <= 999999)
                return NumberToText(intVal / 1000) + "Thousands " + NumberToText(intVal % 1000);
            else if (intVal <= 1999999)
                return "One Million " + NumberToText(intVal % 1000000);
            else if (intVal <= 999999999)
                return NumberToText(intVal / 1000000) + "Millions " + NumberToText(intVal % 1000000);
            else if (intVal <= 1999999999)
                return "One Billion " + NumberToText(intVal % 1000000000);
            else
                return NumberToText(intVal / 1000000000) + "Billions " + NumberToText(intVal % 1000000000);
        }
    }
}
