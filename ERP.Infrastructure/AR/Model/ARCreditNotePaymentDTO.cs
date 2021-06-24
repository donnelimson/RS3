using Codebiz.Domain.ERP.Model.Data.Documents;
using Codebiz.Domain.ERP.Model.Data.Documents.Sales;
using Codebiz.Domain.ERP.Model.Data.Payments.IncommingPayments;
using Codebiz.Domain.ERP.Model.Data.Payments.OutgoingPayments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codebiz.Domain.ERP.Infrastructure.AR.Model
{
    public class ARCreditNotePaymentDTO : SalesCreditNote 
    {
        public ICollection<OutgoingPayment> Payments { get; set; }

        public ARCreditNotePaymentDTO()
        {
            Payments = new List<OutgoingPayment>();
        }
    }
}