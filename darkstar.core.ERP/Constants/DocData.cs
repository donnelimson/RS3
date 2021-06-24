using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Constants
{
    public static class DocData
    {
        #region Doc statuses
        public const string Open = "O";
        public const string Printed = "P";
        public const string Closed = "C";
        public const string Cancelled = "N";
        #endregion
        #region Doc Item Type
        public const string Item = "I";
        public const string Service = "S";
        #endregion
    }
    public static class DocType
    {
        public const string SIN = "Sales Invoice";
        public const string JE = "Journal Entry";
        public const string Invoice = "IN";
        public const string Payment = "PY";
        public const string SalesOder = "SOR";
        public const string SalesDelivery = "SDN";
        public const string SalesInvoice = "SIN";
        public const string SalesCreditNote = "SCR";

        public const string PurchaseOder = "POR";
        public const string PurchaseDelivery = "PDN";
        public const string PurchaseInvoice = "PIN";
        public const string PurchaseCreditNote = "PCR";
        
        public const string JournalEntry = "JE";

        public const string ElectricBill = "EB";
    }
}
