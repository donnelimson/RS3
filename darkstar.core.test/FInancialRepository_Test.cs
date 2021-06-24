using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Codebiz.Core.ERP.Data.Financials;

namespace Codebiz.Core.Test
{
    
    [TestClass]
    public class FInancialRepository_Test
    {

        FinanceDataContext _financeCtx ;
        public FInancialRepository_Test()
        {
            _financeCtx = new FinanceDataContext();
        }

        [TestMethod]
        public void Test_Journal_Entry()
        {
            JournalEntryRepository je_repo = new JournalEntryRepository(_financeCtx);
            JournalEntry_LineRepository je_line_repo = new JournalEntry_LineRepository(_financeCtx);


            var je = new JournalEntry()
            {
                TransType = "SIN",
                Ref1 = "Test",
                Ref2 = "Test",
                SysTotal = 1000,
                SysTotalFC = 1000,
                SysTotalLC = 1000,
            };

            je_repo.Add(je);

            je.JournalEntry_Lines.Add(new JournalEntry_Line()
            {
                JournalEntryID = je.JournalEntryID,
                LineID = 1,
                TransType = "SIN",
                GLAcctCode = "100001",
                ShortName = "smallkid",
                Debit = 1000,
                Credit = 0,
                DebitCredit = "D",
            });

            je.JournalEntry_Lines.Add(new JournalEntry_Line()
            {
                JournalEntryID = je.JournalEntryID,
                LineID = 2,
                TransType = "SIN",
                GLAcctCode = "110001",
                ShortName = "110001",
                Debit = 0,
                Credit = 1000,
                DebitCredit = "C",
            });

            if (je.JournalEntry_Lines.Count > 0) {
                je.JournalEntry_Lines.ToList().ForEach(x =>
                {
                    je_line_repo.Add(x);
                });
            }



            _financeCtx.SaveChanges();
            Assert.IsTrue(1 == 1);

            
        }
    }
}
