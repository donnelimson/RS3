using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Core.ERP.Data.Documents;
using Codebiz.Core.ERP.Data.Documents.Sales;

namespace Codebiz.Infrastructure.SalesDataService
{
    public class SalesInvoiceDS
    {

        private SalesDataContext _salesdataContext;
        public SalesInvoiceDS()
        {
        }

        public long Add(SalesInvoice doc) {
            try
            {
                _salesdataContext.SalesInvoices.Add(doc);
                long docID = doc.DocID;

                double docTotal = doc.Document_Lines.Sum(x => x.GTotal);
                if (docID > 0) {
                    doc.Document_Lines.ToList().ForEach(x =>
                    {
                        x.DocID = docID;
                        _salesdataContext.SalesInvoice_Lines.Add(x);
                    });
                    
                }
                return docID;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
