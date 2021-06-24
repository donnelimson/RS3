using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Finance.Model
{
    public class PostingPeriodVM
    {
        public virtual string PeriodCategoryCode { get; set; }

        public virtual string PeriodCategoryName { get; set; }

        //period number
        public virtual int? PeriodNum { get; set; }  // Y =1 M =2 Q = 4

        // Months, Year, Quarter ,Days
        public virtual string SubType { get; set; }

        public virtual DateTime? FromRefDate { get; set; }

        public virtual DateTime? ToRefDate { get; set; }

        public virtual DateTime? FromDueDate { get; set; }

        public virtual DateTime? ToDueDate { get; set; }

        public virtual DateTime? FromTaxDate { get; set; }

        public virtual DateTime? ToTaxDate { get; set; }

        public virtual DateTime? FinancialYearStartDate { get; set; } //start of fiscal year

        public virtual int? FinancialYear { get; set; } // fiscal year

    }
}
