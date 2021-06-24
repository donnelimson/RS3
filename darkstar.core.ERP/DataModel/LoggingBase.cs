using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Data
{
    public abstract class LoggingBase
    {
        [Column(TypeName = "nvarchar"), MaxLength(50)]
        public virtual string Datasource {get;set;}

        public virtual int? LogIntance { get; set; }

        [Column(TypeName = "nvarchar"), MaxLength(50)]
        public virtual string CreatedBy { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        [Column(TypeName = "nvarchar"), MaxLength(50)]
        public virtual string UpdatedBy { get; set; }

        public virtual DateTime? UpdatedDate { get; set; }
    }
}
