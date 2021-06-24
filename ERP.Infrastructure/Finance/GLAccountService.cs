using AutoMapper;
using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Infrastructure.Finance.DTO;
using Codebiz.Domain.ERP.Infrastructure.Finance.Model;
using Codebiz.Domain.ERP.Model.Data.Financials;
using Codebiz.Domain.ERP.Repository.FileUtils;
using Codebiz.Domain.ERP.Repository.Financials;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class GLAccountService : IDisposable
    {
        private FinanceDataContext _ctx;

        private GLAccountRepository _repository;

        private FinancialPeriodRepository _fpRepository;

        public GLAccountService(FinanceDataContext ctx)
        {
            _ctx = ctx;
            _repository = new GLAccountRepository(_ctx);
            _fpRepository = new FinancialPeriodRepository(_ctx);
        }

        public GLAccount Add(GLAccount p1) => _repository.Add(p1);

        public GLAccount SaveOrUpdate(GLAccount p1) => _repository.SaveOrUpdateEx(p1);

        public int Commit() => _repository.Commit();

        public IQueryable<GLAccount> GetGLAccountList(Expression<Func<GLAccount, bool>> q, params string[] _includes)
        {
            return _repository.GetListEx(q, _includes);
        }

        public IEnumerable<GLAccountVM> GetGLAccountVMList()
        {
            var res = new List<GLAccountVM>();

            var gl_list = GetGLAccountList(x => true).ToList();

            gl_list.ForEach(x =>
            {
                var g = new GLAccountVM()
                {
                    id = x.AcctCode,
                    text = $"{x.AcctCode} - {x.AcctName}",
                    parent = x.Level == 1 ? "#" : x.FatherCode,
                    icon = x.Postable == "Y" ? "fa fa-file icon-state-warning" : ""
                };

                res.Add(g);
            });
            return res;
        }

        public List<GLAccount> GetChartOfAccountTree()
        {
            try
            {
                var l1 = _repository.GetListEx(x => x.Level == 1).ToList();
                l1.ForEach(a =>
                {
                    var l2 = _repository.GetListEx(b => b.FatherCode == a.AcctCode).ToList();
                    l2.ForEach(b =>
                    {
                        var l3 = _repository.GetListEx(c => c.FatherCode == b.AcctCode).ToList();
                        l3.ForEach(c =>
                        {
                            var l4 = _repository.GetListEx(d => d.FatherCode == c.AcctCode).ToList();
                            l4.ForEach(e =>
                            {
                                var l5 = _repository.GetListEx(f => f.FatherCode == e.AcctCode).ToList();
                                l5.ForEach(f =>
                                {
                                    var l6 = _repository.GetListEx(g => g.FatherCode == f.AcctCode).ToList();
                                    l6.ForEach(g => f.ChildNodes.Add(g));
                                    e.ChildNodes.Add(f);
                                });
                                c.ChildNodes.Add(e);
                            });
                            b.ChildNodes.Add(c);
                        });
                        a.ChildNodes.Add(b);
                    });
                });

                return l1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GLAccountVM> GetChartOfAccountVMTree()
        {
            var gl_list = _repository.GetListEx(x => true).ToList();
            var res = new List<GLAccountVM>();
            gl_list.Where(x => x.Level == 1).ToList().ForEach(x =>
                {
                    var c = new GLAccountVM()
                    {
                        id = x.AcctCode,
                        text = x.AcctName
                    };
                    PopulateTree(ref c, gl_list);
                    res.Add(c);
                });
            return res;
        }

        public int ImportFromFile(string filePath, char separator = '\t')
        {
            try
            {
                var f = new FileImportUtility();
                var entity_list = (new FileImportUtility()).ReadAndMapFile<GLAccount, long>(filePath, separator);
                var batchCounter = 0;
                entity_list.ToList().ForEach(x =>
                {
                    var res = _repository.GetList(gl => gl.AcctCode == x.AcctCode);

                    if (res.Count() == 0)
                        _repository.Add(x);
                    else
                    {
                        _repository.SaveOrUpdateEx(x);
                    }

                    batchCounter += 10;
                    if (batchCounter % 100 == 0)
                    {
                        _repository.Commit();
                    }
                });


                _repository.Commit();
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FinancialPeriod GetFinancialPeriod(string catCode, string SubType)
        {
            var res = _fpRepository.GetList(x => x.PeriodCategoryCode == catCode && x.SubType == SubType).FirstOrDefault();
            return res != null ? res : new FinancialPeriod();
        }

        public int SaveOrUpdateGLAccountDetermination(FinancialPeriod fp)
        {
            var res = _fpRepository.GetListEx(x => x.PeriodCategoryCode == fp.PeriodCategoryCode).FirstOrDefault();

            if (res != null)
            {
                res.DFCashCard = fp.DFCashCard;
                res.SAcctLink_1 = fp.SAcctLink_1;
                res.SAcctLink_2 = fp.SAcctLink_2;
                res.SAcctLink_3 = fp.SAcctLink_3;
                res.SAcctLink_4 = fp.SAcctLink_4;
                res.SAcctLink_5 = fp.SAcctLink_5;
                res.SAcctLink_6 = fp.SAcctLink_6;
                res.SAcctLink_7 = fp.SAcctLink_7;
                res.SAcctLink_9 = fp.SAcctLink_9;
                res.SAcctLink_10 = fp.SAcctLink_10;
                res.SAcctLink_11 = fp.SAcctLink_11;
                res.SAcctLink_12 = fp.SAcctLink_12;
                res.SAcctLink_13 = fp.SAcctLink_13;
                res.SAcctLink_14 = fp.SAcctLink_14;
                res.SAcctLink_15 = fp.SAcctLink_15;
                res.SAcctLink_16 = fp.SAcctLink_16;
                res.SAcctLink_17 = fp.SAcctLink_17;
                res.SAcctLink_18 = fp.SAcctLink_18;
                res.SAcctLink_19 = fp.SAcctLink_19;
                res.SAcctLink_20 = fp.SAcctLink_20;
                res.PAcctLink_1 = fp.PAcctLink_1;
                res.PAcctLink_2 = fp.PAcctLink_2;
                res.PAcctLink_3 = fp.PAcctLink_3;
                res.PAcctLink_4 = fp.PAcctLink_4;
                res.PAcctLink_5 = fp.PAcctLink_5;
                res.PAcctLink_6 = fp.PAcctLink_6;
                res.PAcctLink_7 = fp.PAcctLink_7;
                res.PAcctLink_8 = fp.PAcctLink_8;
                res.PAcctLink_9 = fp.PAcctLink_9;
                res.PAcctLink_10 = fp.PAcctLink_10;
                res.PAcctLink_11 = fp.PAcctLink_11;
                res.PAcctLink_12 = fp.PAcctLink_12;
                res.PAcctLink_13 = fp.PAcctLink_13;
                res.PAcctLink_14 = fp.PAcctLink_14;
                res.PAcctLink_15 = fp.PAcctLink_15;
                res.PAcctLink_16 = fp.PAcctLink_16;
                res.PAcctLink_17 = fp.PAcctLink_17;
                res.PAcctLink_18 = fp.PAcctLink_18;
                res.PAcctLink_19 = fp.PAcctLink_19;
                res.PAcctLink_20 = fp.PAcctLink_20;
                res.IAcctLink_1 = fp.IAcctLink_1;
                res.IAcctLink_2 = fp.IAcctLink_2;
                res.IAcctLink_3 = fp.IAcctLink_3;
                res.IAcctLink_4 = fp.IAcctLink_4;
                res.IAcctLink_5 = fp.IAcctLink_5;
                res.IAcctLink_6 = fp.IAcctLink_6;
                res.IAcctLink_7 = fp.IAcctLink_7;
                res.IAcctLink_8 = fp.IAcctLink_8;
                res.IAcctLink_9 = fp.IAcctLink_9;
                res.IAcctLink_10 = fp.IAcctLink_10;
                res.IAcctLink_11 = fp.IAcctLink_11;
                res.IAcctLink_12 = fp.IAcctLink_12;
                res.IAcctLink_13 = fp.IAcctLink_13;
                res.IAcctLink_14 = fp.IAcctLink_14;
                res.IAcctLink_15 = fp.IAcctLink_15;
                res.IAcctLink_16 = fp.IAcctLink_16;
                res.GAcctLink_1 = fp.GAcctLink_1;
                res.GAcctLink_2 = fp.GAcctLink_2;
                res.GAcctLink_3 = fp.GAcctLink_3;
                res.GAcctLink_4 = fp.GAcctLink_4;
                res.GAcctLink_5 = fp.GAcctLink_5;
                res.GAcctLink_6 = fp.GAcctLink_6;
                res.GAcctLink_7 = fp.GAcctLink_7;
                res.GAcctLink_8 = fp.GAcctLink_8;

            }
            else
                _fpRepository.Add(fp);

            _fpRepository.Commit();

            return 0;
        }

        public IEnumerable<GLAccountSearchDTO> GetGLAccountListSeach(Expression<Func<GLAccount, bool>> q)
        {
            try
            {

                var res = _repository.GetListEx(q);

                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<List<GLAccount>, List<GLAccountSearchDTO>>();
                });

                var mapper = cfg.CreateMapper();

                var gl_list = mapper.Map<List<GLAccountSearchDTO>>(res);
                if (gl_list.Count > 0) return gl_list;

                return new List<GLAccountSearchDTO>();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #region Private Functions
        private void PopulateTree(ref GLAccountVM root, List<GLAccount> gl_list)
        {
            if (root == null) { }
            else
            {
                var id = root.id;
                var childs = gl_list.Where(x => x.FatherCode == id);
                foreach (var d in childs)
                {
                    var c = new GLAccountVM()
                    {
                        id = d.AcctCode,
                        text = d.AcctName
                    };
                    PopulateTree(ref c, gl_list);
                    root.children.Add(c);
                }
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
