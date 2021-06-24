using Codebiz.Domain.ERP.Model.Data.Documents;
using Codebiz.Domain.ERP.Model.Data.Documents.Sales;
using Codebiz.Domain.ERP.Model.Data.Payments.IncommingPayments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codebiz.Domain.ERP.Infrastructure.AR.Model
{
    public class ARInvoicePaymentDTO : SalesInvoice 
    {
        public ICollection<IncomingPayment> Payments { get; set; }

        public ARInvoicePaymentDTO()
        {
            Payments = new List<IncomingPayment>();
        }
    }
}