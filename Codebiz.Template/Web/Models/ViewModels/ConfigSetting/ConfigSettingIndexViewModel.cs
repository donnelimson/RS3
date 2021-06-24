using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.ConfigSetting
{
    public class ConfigSettingIndexViewModel : FilterBase
    {
        public string SearchTerm { get; set; }
        public IPagedList<Codebiz.Domain.Common.Model.ConfigSetting> ConfigSettings { get; set; }
    }
}