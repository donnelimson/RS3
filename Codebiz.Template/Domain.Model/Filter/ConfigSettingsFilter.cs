using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class ConfigSettingsFilter : FilterBase
    {
        public string SearchTerm { get; set; }
        public IPagedList<Codebiz.Domain.Common.Model.ConfigSetting> ConfigSettings { get; set; }
    }
}
