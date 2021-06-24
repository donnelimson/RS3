using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.FAMMS.Inventory.ItemMasterData
{
   public enum IssuedMethodEnum
    {
        [Description("Blackflush")]
        Blackflush = 1,

        [Description("Manual")]
        Manual = 2,
    }
}
