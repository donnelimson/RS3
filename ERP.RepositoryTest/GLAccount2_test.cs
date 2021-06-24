using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data.Financials;
//using Codebiz.Domain.ERP.Repository.FileUtils;
using Infrastructure;
using Infrastructure.Repository.FIN;
using Infrastructure.Repository.UTILS;
using Infrastructure.Services.FIN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.RepositoryTest
{
    [TestClass]
    public class GLAccount2_test
    {
        [TestMethod]
        public void Add_GLAcount()
        {
            try
            {
                var ctx = new ERPDataContext();
                var glrepo = new GLAccountRepository(ctx);
                var app = new ApplicationContext();
                app.Contexts = new List<DbContext>(){ ctx };

                var unitOfWork = new UnitOfWork(app);

                glrepo.Insert(new GLAccount()
                {
                    AcctCode = "test",
                    AcctName = "test"
                });

                unitOfWork.SaveChanges();


                var gl_list = glrepo.GetList(x => true);

                Assert.IsTrue(gl_list.ToList().Count() > 0);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //[TestMethod]
        //public void import_file_GLAcount()
        //{
        //    try
        //    {
        //        var ctx = new ERPDataContext();
        //        var glrepo = new GLAccountRepository(ctx);
        //        var app = new ApplicationContext();
        //        var fiRepo = new FileImportUtility();

        //        var fiService = new 
        //            FinanceDataService(
        //               glRepo: glrepo,
        //    fpRepo:null,
        //    piRepo: null,
        //    prjRepo: null,
        //    vatRepo: null,
        //    jeRepo: null,
        //    jvRepo: null,
        //    fileRepo : fiRepo,
        //    withholdingTaxRepository:null
        //            );

        //        app.Contexts = new List<DbContext>() { ctx };

        //        var unitOfWork = new UnitOfWork(app);

        //        fiService.ImportFromFile(filePath: @"c:\temp\coa_template.txt");
        //        //unitOfWork.SaveChanges();
        //         var gl_list = glrepo.GetList(x => true);

        //        Assert.IsTrue(gl_list.ToList().Count() > 0);
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}
    }
}
