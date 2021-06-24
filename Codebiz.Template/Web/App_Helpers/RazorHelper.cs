using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Enums;
using Infrastructure.Services;

namespace Web
{
    public class RazorHelper
    {
        public static string FormatDecimalOrShowZero(decimal amount)
        {
            if (amount == 0) return "0";

            return amount.ToString("##,###.##");
        }

        public static string FormatAsMoneyOrShowZero(decimal amount)
        {
            if (amount == 0) return "0.00";

            return string.Format("{0:#,###.00}", amount);
        }

        public static string FormatNumberAsMoneyOrShowZero(double amount)
        {
            if (amount == 0) return "0.00";

            return string.Format("{0:#,###.00}", amount);
        }
        
        public static string FormatDecimalOrShowZero(double amount)
        {
            if (amount == 0) return "0";

            return amount.ToString("##,###.##");
        }

        public static string FormatAsWeight(double amount)
        {
            if (amount == 0) return "0";

            return amount.ToString("##,###.#");
        }

        public static string FormatAsPercentage(decimal value)
        {
            if (value == 0) return "0 %";
            decimal result = (value * 100);
            return string.Format("{0} %", Math.Floor(result * (decimal)Math.Pow(10, 2)) / (decimal)Math.Pow(10, 2));
        }

        public static string FormatAsPercentage(double value)
        {
            if (value == 0) return "0 %";
            double result = value * 100;
            return string.Format("{0} %", Math.Floor(result * Math.Pow(10, 2)) / Math.Pow(10, 2));
        }

        public static string YesOrNo(bool value)
        {
            return value ? "YES" : "No";
        }

        public static string PaidOrNotPaid(bool value)
        {
            return value ? "PAID" : "NOT PAID";
        }

        public static string YesOrNo(DateTime? value)
        {
            return value != null ? "YES" : "No";
        }

        public static string FormatAsShortDate(DateTime dateTime)
        {
            return dateTime.ToString("MMM. dd, yyyy");
        }

        public static string FormatAsShortDate(DateTime? dateTime)
        {
            return dateTime != null ? dateTime.Value.ToString("MMM. dd, yyyy") : string.Empty;
        }
        public static string FormatDateRange(DateTime from, DateTime to)
        {
            if (from.Year != to.Year)
            {
                return $"{from.ToString("MMM. d, yyyy")} - {to.ToString("MMM. d, yyyy")}";
            }
            else if (from.Month != to.Month)
            {
                return $"{from.ToString("MMM. d")} - {to.ToString("MMM. d, yyyy")}";
            }
            else
            {
                return $"{from.ToString("MMM. d")} - {to.ToString("d, yyyy")}";
            }
        }
        public static string FormatAsLongDateTime(DateTime? dateTime)
        {
            return dateTime != null ? dateTime.Value.ToString("MMM. dd, yyyy hh:mm tt") : string.Empty;
        }
        public static string TruncateDisplay(string value, int maxStringLength = 100)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxStringLength)
                return value.Substring(0, maxStringLength) + " ...";

            return value;
        }

        public static MvcHtmlString ApprovalStatusToBootstrapLabel(ApprovalStatus approvalStatus)
        {
            //if(approvalStatus.Id == (int)ApprovalStatuses.ForApproval)
            //{
            //    return new MvcHtmlString($"<span class='label label-sm label-info'> {approvalStatus.Description}</span>");
            //}
            if (approvalStatus.Id == (int)ApprovalStatuses.Approved)
            {
                return new MvcHtmlString($"<span class='label label-sm label-success'> {approvalStatus.Description}</span>");
            }
            else if (approvalStatus.Id == (int)ApprovalStatuses.Rejected)
            {
                return new MvcHtmlString($"<span class='label label-sm label-danger'> {approvalStatus.Description}</span>");
            }
            //else if (approvalStatus.Id == (int)ApprovalStatuses.Cancelled)
            //{
            //    return new MvcHtmlString($"<span class='label label-sm label-warning'> {approvalStatus.Description}</span>");
            //}

            return new MvcHtmlString($"<span class='label label-sm label-default'> {approvalStatus.Description}</span>");
        }


        public static MvcHtmlString YearQuarterDisplay(int yearQuarter)
        {
            if (yearQuarter == 1)
            {
                return new MvcHtmlString($"Q1");
            }
            else if (yearQuarter == 2)
            {
                return new MvcHtmlString($"Q2");
            }
            else if (yearQuarter == 3)
            {
                return new MvcHtmlString($"Q3");
            }
            else if (yearQuarter == 4)
            {
                return new MvcHtmlString($"Q4");
            }

            return new MvcHtmlString("");
        }

        public static MvcHtmlString SpecialistClassDisplay(int _class)
        {
            if (_class == 1)
            {
                return new MvcHtmlString($"1st");
            }
            else if (_class == 2)
            {
                return new MvcHtmlString($"2nd");
            }
            else if (_class == 3)
            {
                return new MvcHtmlString($"3rd");
            }

            return new MvcHtmlString("");
        }

        public static MvcHtmlString YearMonthDayToDisplay(int year, int months, int days)
        {
            string result = string.Empty;

            if (year > 0)
            {
                result = $"{year.NumberToText()} ({year}) year{(year > 1 ? "s" : "")}";
            }
            else if (months > 0)
            {
                if (year > 0)
                    result = $"{result} and";

                result = $"{result} {months.NumberToText()} ({months}) month{(months > 1 ? "s" : "")}";
            }
            else if (days > 0)
            {
                if (months > 0)
                    result = $"{result} and ";

                result = $"{result} {days.NumberToText()} ({days}) day{(days > 1 ? "s" : "")}";
            }

            return new MvcHtmlString(result);
        }


    }
}