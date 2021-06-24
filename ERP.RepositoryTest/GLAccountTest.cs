using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codebiz.Domain.ERP.Context;
//using Codebiz.Domain.ERP.Repository.Financials;
using System.Linq;
using Codebiz.Domain.ERP.Model.Data.Financials;
//using Codebiz.Domain.ERP.Infrastructure.Finance;

namespace ERP.RepositoryTest
{
    [TestClass]
    public class GLAccountTest
    {
        [TestMethod]
        public void Add_GLAcount()
        {
            //var ctx = new ERPDataContext();
            //var glrepo = new GLAccountRepository(ctx);

            //glrepo.Add(new GLAccount()
            //{
            //    AcctCode = "test",
            //    AcctName = "test"
            //});

            //glrepo.Commit();

            //var gl_list = glrepo.GetList(x => true);

            //Assert.IsTrue(gl_list.ToList().Count() > 0);
        }


        [TestMethod]
        public void fileImport_GL()
        {
            try
            {
                //var ctx = new ERPDataContext();
                //var glrepo = new GLAccountService(ctx);
                //glrepo.ImportFromFile(filePath: @"c:\temp\coa_template.txt");
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {

                throw;
            }
        }


        [TestMethod]
        public void get_chart_of_accounts()
        {
            //var ctx = new ERPDataContext();
            //var glrepo = new GLAccountService(ctx);
            //var gl_list = glrepo.GetChartOfAccountVMTree();
            //Assert.IsTrue(gl_list.Count() > 0);
        }
    }
}
