using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codebiz.Domain.Common.Model;
using PagedList;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Models.ViewModels.AccountsPayable
{
    public class AccountsPayableViewModel
    {
        public class AccountsPayableIndexModel
        {
            public IPagedList<Codebiz.Domain.ERP.Model.Data.FixedAssetsMaterialsManagementSystem.AccountsPayable> AccountsPayables { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public string AccountsPayableCode { get; set; }
           
        }
    }
}