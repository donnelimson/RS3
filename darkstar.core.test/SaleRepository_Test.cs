using System;
using System.Text;
using System.Collections.Generic;
using Codebiz.Core.ERP.Data;
using Codebiz.Core.ERP.Data.Documents;
using Codebiz.Core.ERP.Data.Documents.Sales;
using Codebiz.Core.ERP.Data.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codebiz.Core.Test
{
    [TestClass]
    public class SaleRepository_Test
    {
        ItemContext _itemCtx;
        SalesDataContext _salesContext;
        public SaleRepository_Test()
        {
            _itemCtx = new ItemContext();
            _salesContext = new SalesDataContext();
        }

        [TestMethod]
        public void add_item_test() {
            var _item_repo = new ItemRepository(_itemCtx);

            _item_repo.Add(new ItemMaster()
            {
                ItemCode = "ITM001",
                ItemName = "Item 1"
            });
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void add_so_test()
        {
            var _so_repo = new SalesOrderRepository(_salesContext);

            _so_repo.Add(new SalesOrder()
            {
                CardCode = "C0001",
                CardName = "Customer 1"
            });
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void add_si_test()
        {
            var _so_repo = new SalesInvoiceRepository(_salesContext);

            _so_repo.Add(new SalesInvoice()
            {
                CardCode = "C0001",
                CardName = "Customer 1"
            });
            Assert.IsTrue(true);
        }
    }
}
